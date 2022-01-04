using AgileConfig.Client;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CodeIsBug.Admin.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //使用AutoFac做IOC和AOP
                .ConfigureAppConfiguration((HostBuilderContext,config)=>{
                    var envName = HostBuilderContext.HostingEnvironment.EnvironmentName;
                    var configClient = new ConfigClient($"appsettings.{envName}.json");
                    config.AddAgileConfig(configClient);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}