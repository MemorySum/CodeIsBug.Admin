using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace CodeIsBug.Admin.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLog.Web.NLogBuilder.ConfigureNLog("nlog.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                              .ConfigureLogging(logging =>
                                    {
                                        logging.ClearProviders(); //移除已经注册的其他日志处理程序
                                        logging.SetMinimumLevel(LogLevel.Trace); //设置最小的日志级别
                                    })
                                .UseNLog(); 
                });
    }
}
