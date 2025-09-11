using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceStation.Models.Common;
using ServiceStation.Models.DBConnect;

namespace ServiceStation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });
            services.AddHttpClient(); //appcenter


            services.AddMvc().AddControllersAsServices();
            services.AddDbContext<HRMS>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HRMS")));
            services.AddDbContext<LAMP>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LAMP")));
            services.AddDbContext<IT>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IT")));
            services.AddDbContext<PrdInvBF_Prd>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PrdInvBf_Prd")));

            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.Cookie.Name = "Remember";
                    // x.ExpireTimeSpan = TimeSpan.FromDays(1);
                    x.ExpireTimeSpan = TimeSpan.FromMinutes(15); // อายุของ session
                    x.LoginPath = "/Login/Index"; //path login
                    x.LogoutPath = "/Login/Logout"; //path loout
                    x.AccessDeniedPath = "/ErrorCase/Index";
                    x.ReturnUrlParameter = "returnurl";
                    x.Cookie.IsEssential = true;
                    x.SlidingExpiration = true; // ✅ เปิดให้ session ต่ออายุถ้ามีการใช้งาน
                    x.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = context =>
                        {
                            // Only redirect on 401 Unauthorized
                            if (context.Request.Path.StartsWithSegments("/api"))
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            }
                            else
                            {
                                using (ITAppcenter _ITApp = new ITAppcenter())
                                {
                                    decimal target_program = _ITApp.Programs.Where(c => c.program_name == GlobalVariable.ProgramName).Select(s => s.id).FirstOrDefault();

                                    var loginUrl = $"http://thsweb/mvcpublish/appcenter/landing?appid={target_program}";
                                    context.Response.Redirect(loginUrl);
                                }

                            }
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthentication(); //app center

            //        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(15); // อายุของ session
            //    options.SlidingExpiration = true; // ✅ เปิดให้ session ต่ออายุถ้ามีการใช้งาน
            //    options.LoginPath = "/Login/Index"; //path login
            //    options.AccessDeniedPath = "/ErrorCase/Index";
            //});


            services.AddAuthorization(x =>
            {
                x.AddPolicy("Checked", y => { y.RequireClaim(ClaimTypes.Country, "ServiceStation"); });
                //x.AddPolicy("perUser", y => { y.RequireClaim(ClaimTypes.Role, GlobalVariable.perUser); });
                //x.AddPolicy("perAdmin", y => { y.RequireClaim(ClaimTypes.Role, GlobalVariable.perAdmin); });
                //x.AddPolicy("perHCM", y => { y.RequireClaim(ClaimTypes.Role, GlobalVariable.perHCM); });
                //x.AddPolicy("perGeneral", y => { y.RequireClaim(ClaimTypes.Role, GlobalVariable.perUser, GlobalVariable.perAdmin, GlobalVariable.perHCM); });
                //x.AddPolicy("perEmergency", y => { y.RequireClaim(ClaimTypes.Role, GlobalVariable.EmergencyPermission); });
            });


            services.AddMemoryCache();
            services.AddSession(x => x.Cookie.IsEssential = true);
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}");
            });
            app.UseCookiePolicy();
        }
    }
}
