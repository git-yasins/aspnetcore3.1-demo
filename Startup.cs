using aspnetcore3_demo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace aspnetcore3_demo
{
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
            //var selfConfig = Configuration["SelfOptionConfig:FontBold"];

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            //mvc模式
            //services.AddControllersWithViews ();
            //RazorPage模式
            //services.AddRazorPages();
            //Signa
            services.AddControllers();
            services.AddSingleton<IDepartmentService, DepartmentService> ();
            services.AddSingleton<IEmployeeService, EmployeeService> ();
            // 获取自定义配置对象
            services.Configure<SelfOptionConfig>(Configuration.GetSection("SelfOptionConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Department/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                // mvc模式启用
                // endpoints.MapControllerRoute (
                //     name: "default",
                //     pattern: "{controller=Department}/{action=Index}/{id?}");
                //RazorPage模式启用
                endpoints.MapRazorPages();
            });
        }
    }
}
