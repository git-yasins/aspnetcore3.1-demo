using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore3_demo.gRpcService;
using aspnetcore3_demo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aspnetcore3._1_demo {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services) {
            services.AddGrpc ();
            services.AddSingleton<IDepartmentRepositories, DepartmentRepositories> ();
            services.AddSingleton<IEmployeeRepositories, EmployeeRepositories> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseRouting ();

            app.UseEndpoints (endpoints => {
                endpoints.MapGrpcService<DepartmentService> ();
                endpoints.MapGrpcService<EmployeeService> ();
            });
        }
    }
}
