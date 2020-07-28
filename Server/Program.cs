using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace aspnetcore3._1_demo {
    public class Program {
        public static void Main (string[] args) {
            CreateHostBuilder (args).Build ().Run ();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args)
            .ConfigureWebHostDefaults (webBuilder => {
                 webBuilder.ConfigureKestrel (options => {
                    options.ListenLocalhost (5001, a => a.Protocols =
                        Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2);
                });
                webBuilder.UseStartup<Startup> ();

            });
    }
}
