using System;
using aspnetcore3_demo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aspnetcore3._1_demo {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddScoped<ICompanyRepository, CompanyRepository> ();
            services.AddDbContext<aspnetcore3_demo.Data.RoutineDBContext> (option => {
                option.UseSqlite (Configuration["ConnectionStrings:Default"]);
            });
            services.AddAutoMapper (AppDomain.CurrentDomain.GetAssemblies ());
            services.AddControllers (options => {
                //如果请求的内容为不支持的格式(xml),则返回406
                //options.ReturnHttpNotAcceptable = true;
                //webAPI同时支持输出XML内容
                //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                //默认输出XML
                //options.OutputFormatters.Insert(0,new XmlDataContractSerializerOutputFormatter());
            }).AddXmlDataContractSerializerFormatters (); //3.X支持的输入输出XML
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                //生产环境自定义错误消息
                app.UseExceptionHandler (builder => {
                    builder.Run (async context => {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync ("Unexpected Error!");
                    });
                });
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}
