using CodeIsBug.Admin.Models.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System.IO;

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
			services.AddCors(options =>
			{
				options.AddPolicy(name: CodeIsBugAdminPolicy,
					builder =>
					{
						builder.AllowAnyOrigin().WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS");

					});
			});

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeIsBug.Admin.API", Version = "v1" });
				var basePath = PlatformServices.Default.Application.ApplicationBasePath;
				//var basePath = AppDomain.CurrentDomain.BaseDirectory;
				var xmlPath = Path.Combine(basePath, "CodeIsBug.Admin.Api.xml");
				c.IncludeXmlComments(xmlPath);
			});
			
			services.AddDbContext<CodeIsBugContext>(option => {
				option.UseSqlServer(Configuration.GetConnectionString("codeIsBug.Admin"));
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
