using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace aspnetcore3_demo {
    public class Program {
        public static void Main (string[] args) {
            CreateHostBuilder(args).Build().Run();

            // var redis = ConnectionMultiplexer.Connect ("localhost:6379,password=12345678");
            // var db = redis.GetDatabase ();

            // db.StringSet ("name", "Michael Jackson");
            // var name = db.StringGet ("name");
            // System.Console.WriteLine (name);
        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args)
            .ConfigureWebHostDefaults (webBuilder => {
                webBuilder.UseUrls("http://localhost:5000");
                webBuilder.UseStartup<Startup> ();
            });
    }
}
