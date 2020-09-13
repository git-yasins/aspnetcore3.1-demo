using System;
using System.IO;
using System.Linq;
using aspnetcore3_demo.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace aspnetcore3._1_demo {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            //  services.AddScoped<ICompanyRepository, CompanyRepository> ();
            services.AddDbContext<aspnetcore3_demo.Data.RoutineDBContext> (option => {
                option.UseSqlite (Configuration["ConnectionStrings:Default"]);
            });

            //添加缓存
            //marvin.cache.headers中间件
            services.AddHttpCacheHeaders (expires => {
                expires.MaxAge = 60;
                expires.CacheLocation = CacheLocation.Private;
            }, validation => { //缓存验证模型
                validation.MustRevalidate = true;
            });
            //系统
            services.AddResponseCaching ();

            //注册对象属性映射服务
            services.AddTransient<IPropertyMappingService, PropertyMappingService> ();
            //数据塑形字段检查
            services.AddTransient<IPropertyCheckService, PropertyCheckService> ();
            //Dto-Model 映射
            services.AddAutoMapper (AppDomain.CurrentDomain.GetAssemblies ());

            services.AddControllers (options => {
                    //如果请求的内容为不支持的格式(xml),则返回406
                    options.ReturnHttpNotAcceptable = true;
                    //webAPI同时支持输出XML内容
                    //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    //默认输出XML
                    //options.OutputFormatters.Insert(0,new XmlDataContractSerializerOutputFormatter());
                    //配置缓存
                    options.CacheProfiles.Add ("selfCacheProfile120",
                        new CacheProfile {
                            Duration = 120
                        });
                })
                .AddNewtonsoftJson (setup => { //添加[patch]局部更新JSON序列化  JsonPatchDocument 支持
                    //串行化设置
                    setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
                })
                .AddXmlDataContractSerializerFormatters () //3.X支持的输入输出XML
                .ConfigureApiBehaviorOptions (setupAction => { //7867规范 实体验证错误,自定义详细错误422状态码和消息内容
                    setupAction.InvalidModelStateResponseFactory = context => {
                    var problemDetails = new ValidationProblemDetails (context.ModelState) {
                    Type = "www.baidu.com",
                    Title = "错误!!!",
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Detail = "查看详细错误",
                    Instance = context.HttpContext.Request.Path
                        };
                        problemDetails.Extensions.Add ("traceId", context.HttpContext.TraceIdentifier);
                        return new UnprocessableEntityObjectResult (problemDetails) {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });

            //为webapi的headers-Accept参数添加自定义全局mediaType 处理
            services.Configure<MvcOptions> (config => {
                var newtonSoftJsonOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter> ()?.FirstOrDefault ();
                newtonSoftJsonOutputFormatter?.SupportedMediaTypes.Add ("application/vnd.company.hateoas+json");
            });

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
                //options.CustomSchemaIds (c => c.FullName); //根据全名生成
                options.ResolveConflictingActions (c => c.First ()); //解决同类型Action或重载的错误
            });
        }
        public ILifetimeScope AutofacContainer { get; private set; }
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

            #region Autofac test
            //获取命名注册的容器服务
            //var service = this.AutofacContainer.ResolveNamed<ICompanyRepository> ("company");

            //获取没有命名的实例
            //var service = AutofacContainer.Resolve<ICompanyRepository>();

            //获取属性注册的实例
            // this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            // var serviceName = this.AutofacContainer.Resolve<ICompanyRepository>();
            // serviceName.GetCompanyAsync(Guid.Parse("19d42960-7635-4360-b25a-76f65793f352"));

            //获取子容器
            // this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            // using (var myscope = AutofacContainer.BeginLifetimeScope ("myscope")) {
            //     var service0 = myscope.Resolve<CompanyRepository> ();
            //     using (var scope = myscope.BeginLifetimeScope ()) {
            //         var service1 = scope.Resolve<CompanyRepository> ();
            //         var service2 = scope.Resolve<CompanyRepository> ();
            //         System.Console.WriteLine ($"service1=service2:{service1 == service2}");
            //         System.Console.WriteLine ($"service1=service0:{service1 == service0}");
            //     }
            // }
            #endregion

            //使用缓存
            app.UseResponseCaching ();
            app.UseHttpCacheHeaders ();

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

        /// <summary>
        /// AutoFac 容器实例注册
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer (ContainerBuilder builder) {
            builder.RegisterType<CompanyRepository> ().As<ICompanyRepository> ();

            //属性注册
            //builder.RegisterType<PropertyCheckService> ().As<IPropertyCheckService> ().PropertiesAutowired ();

            //命名注册
            //builder.RegisterType<CompanyRepository> ().Named<ICompanyRepository> ("company");

            //AOP
            //注册拦截器
            //builder.RegisterType<MyInterceptor> ();
            //注册被拦截的实例
            // builder.RegisterType<CompanyRepository> ().As<ICompanyRepository> () //被拦截的实例
            // .PropertiesAutowired () //属性注入
            //    .InterceptedBy (typeof (MyInterceptor)) //拦截时注入的内容
            //   .EnableInterfaceInterceptors (); //接口拦截器

            //将实例注册到子容器
            //builder.RegisterType<CompanyRepository> ().InstancePerMatchingLifetimeScope ("myscope");
        }
    }
}
