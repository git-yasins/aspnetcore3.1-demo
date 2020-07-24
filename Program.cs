using aspnetcore3_demo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace aspnetcore3._1_demo {
    public class Program {
        public static void Main (string[] args) {
            CreateHostBuilder (args).Build ().Run ();
        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args)
            // .ConfigureAppConfiguration((context,configBuilder)=>{
            //不使用默认的APPSETTINGS
            //     configBuilder.Sources.Clear();
            //使用自定义JSON配置文件
            // })
            .ConfigureWebHostDefaults (webBuilder => {
                webBuilder.UseStartup<Startup> ();
            });
    }
}
