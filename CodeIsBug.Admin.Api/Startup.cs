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
using SqlSugar.IOC;

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
            //配置api控制器管道
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Use the default property (Pascal) casing
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                // Configure a custom converter
                options.SerializerSettings.Converters.Add(new IsoDateTimeConverter
                    { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            }).AddControllersAsServices();
            //配置jwt信息 映射到内存中
            var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.Configure<EmailSmtpConfig>(Configuration.GetSection("EmailSmtpConfig"));
            //DBConfig.ConnectionString = Configuration.GetConnectionString("codeIsBug.Admin.MySQL").Trim();
            DBConfig.ConnectionString = Configuration.GetConnectionString("codeIsBug.Admin").Trim();
            //配置跨域请求
            services.AddCors(options =>
            {
                options.AddPolicy(codeIsBugAdminPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin().WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS");

                    });
            });
            //配置swaggerDocument
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
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
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
            //配置JWT验证规则
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
                    //Token颁发机构
                    ValidIssuer = jwtSettings.Issuer,
                    //颁发给谁
                    ValidAudience = jwtSettings.Audience,
                    //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true,
                    ////允许的服务器时间偏移量
                    ClockSkew = TimeSpan.Zero
                };
            });
            //配置SqlSugar IOC
            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = DBConfig.ConnectionString,
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
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
