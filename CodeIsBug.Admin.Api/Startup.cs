using CodeIsBug.Admin.Common.Helper;
using CodeIsBug.Admin.Models.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Converters;

namespace CodeIsBug.Admin.Api
{
	public class Startup
	{
		readonly string CodeIsBugAdminPolicy = "CodeIsBug.Admin.Policy";
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//����api�������ܵ�
			services.AddControllers().AddNewtonsoftJson(options =>
			{
				// Use the default property (Pascal) casing
				options.SerializerSettings.ContractResolver = new DefaultContractResolver();

				// Configure a custom converter
				options.SerializerSettings.Converters.Add(new IsoDateTimeConverter(){DateTimeFormat = "yyyy-MM-dd HH:mm:ss"});
			});
			//����jwt��Ϣ ӳ�䵽�ڴ���
			services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
			var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
			//���ÿ�������
			services.AddCors(options =>
			{
				options.AddPolicy(name: CodeIsBugAdminPolicy,
					builder =>
					{
						builder.AllowAnyOrigin().WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS");

					});
			});

			//����swaggerDocment
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeIsBug.Admin.API", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Description = "���¿�����������ͷ����Ҫ���Jwt��ȨToken��Bearer Token",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
					{
						{
							new OpenApiSecurityScheme{
								Reference = new OpenApiReference {
											Type = ReferenceType.SecurityScheme,
											Id = "Bearer"}
						   },new string[] { }
						}
					});


				var basePath = PlatformServices.Default.Application.ApplicationBasePath;
				//var basePath = AppDomain.CurrentDomain.BaseDirectory;
				var xmlPath = Path.Combine(basePath, "CodeIsBug.Admin.Api.xml");
				c.IncludeXmlComments(xmlPath);
			});
			//����JWT��֤����
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				//Token Validation Parameters
				x.TokenValidationParameters = new TokenValidationParameters
				{
					//Token�䷢����
					ValidIssuer = jwtSettings.Issuer,
					//�䷢��˭
					ValidAudience = jwtSettings.Audience,
					//�����keyҪ���м��ܣ���Ҫ����Microsoft.IdentityModel.Tokens
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
					ValidateIssuerSigningKey = true,
					////�Ƿ���֤Token��Ч�ڣ�ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Ա�
					ValidateLifetime = true,
					////����ķ�����ʱ��ƫ����
					ClockSkew = TimeSpan.Zero
				};
			});
			//�������ݿ�������
			services.AddDbContext<CodeIsBugContext>(options =>
			{
				options.EnableSensitiveDataLogging(true);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseRouting();
			app.UseCors(CodeIsBugAdminPolicy);
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeIsBug.Admin.API V1");
			});
		}
	}
}
