using System;
using aspnetcore3_demo.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace aspnetcore3._1_demo {
    public class Program {
        public static void Main (string[] args) {
            var host = CreateHostBuilder (args).Build ();
            using (var scope = host.Services.CreateScope ()) {
                try {
                    var dbContext = scope.ServiceProvider.GetService<RoutineDBContext> ();
                    dbContext.Database.EnsureDeleted ();
                    dbContext.Database.Migrate ();
                } catch (Exception ex) {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>> ();
                    logger.LogError (ex, "Database Migration Error!");
                }
            }
            host.Run ();
        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args)
            .ConfigureWebHostDefaults (webBuilder => {
                webBuilder.UseStartup<Startup> ();
            });
    }
}
