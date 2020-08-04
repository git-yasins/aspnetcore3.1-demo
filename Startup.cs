using System;
using System.IO;
using aspnetcore3_demo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace aspnetcore3._1_demo
{
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

            //swagger
            services.AddSwaggerGen (options => {
                options.SwaggerDoc ("v1", new OpenApiInfo {
                    Version = "v0.01",
                        Title = "aspnetcore3_demo API",
                        Description = "WebApi demo 说明文档",
                        TermsOfService = new Uri ("http://loalhost:5000"),
                        Contact = new OpenApiContact {
                            Name = "aspnetcore3_demo",
                                Email = "2389092255@qq.com",
                                Url = new Uri ("http://loalhost:5000/")
                        }
                });

                //添加注释说明
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                //var basePath2 = AppContext.BaseDirectory;

                var xmlPath = Path.Combine (basePath, "aspnetcore3_demo.xml");
                options.IncludeXmlComments (xmlPath, true); //第二个参数为TRUE表示显示控制器的注释
            });
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

            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "ApiHelp v1");
                c.RoutePrefix = ""; //localhost:5000直接访问API文档
            });

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}
