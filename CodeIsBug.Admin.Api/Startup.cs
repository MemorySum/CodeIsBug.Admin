using System;
using System.IO;
using System.Text;
using Autofac;
using CodeIsBug.Admin.Api.Extensions;
using CodeIsBug.Admin.Common.Config;
using CodeIsBug.Admin.Common.Helper;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
namespace CodeIsBug.Admin.Api
{
    public class Startup
    {
        private readonly string codeIsBugAdminPolicy = "CodeIsBug.Admin.Policy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //����api�������ܵ�
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Use the default property (Pascal) casing
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                // Configure a custom converter
                options.SerializerSettings.Converters.Add(new IsoDateTimeConverter
                    { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            }).AddControllersAsServices();
            //����jwt��Ϣ ӳ�䵽�ڴ���

            var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.Configure<EmailSmtpConfig>(Configuration.GetSection("EmailSmtpConfig"));
            //DBConfig.ConnectionString = Configuration.GetConnectionString("codeIsBug.Admin.MySQL").Trim();
            DBConfig.ConnectionString = Configuration.GetConnectionString("codeIsBug.Admin").Trim();
            //���ÿ�������
            services.AddCors(options =>
            {
                options.AddPolicy(codeIsBugAdminPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin().WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS");

                    });
            });

            //����swaggerDocument
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeIsBug.Admin.API", Version = "v1" });
                c.AddServer(new OpenApiServer
                {
                    Url = "",
                    Description = ""
                });
                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
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
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });



                //var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var apiXmlPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "CodeIsBug.Admin.Api.xml");
                var commonXmlPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "CodeIsBug.Admin.Common.xml");
                var modelXmlPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "CodeIsBug.Admin.Models.xml");

                c.IncludeXmlComments(apiXmlPath, true);
                c.IncludeXmlComments(commonXmlPath, true);
                c.IncludeXmlComments(modelXmlPath, true);
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

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            loggerFactory.AddLog4Net("Config/log4net.config");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(codeIsBugAdminPolicy);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseKnife4UI(c =>
            {
                c.RoutePrefix = ""; // serve the UI at root
                c.SwaggerEndpoint("/v1/api-docs", "V1-Docs");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger("{documentName}/api-docs");

            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }
    }
}
