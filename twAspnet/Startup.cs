using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using twAspnet.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace TwAspnet
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
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
            services.AddEntityFrameworkSqlite().AddDbContext<TwaspDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                //TwitterAPIをDBから取得
                .AddTwitter(twitterOptions => {
                    var option = new DbContextOptionsBuilder<TwaspDbContext>();
                    var connectionString = "Twasp.db";
                    option.UseSqlite(connectionString);
                    using var context = new TwaspDbContext(option.Options);
                    var enviroment = context.Enviroment.Single();
                    var key = enviroment.Akey;
                    var secretKey = enviroment.ASecretKey;

                    twitterOptions.ConsumerKey = key;
                    twitterOptions.ConsumerSecret = secretKey;
                    twitterOptions.Events.OnCreatingTicket = async context =>
                    {
                        var identity = (ClaimsIdentity)context.Principal.Identity;
                        identity.AddClaim(new Claim("ScreenName", context.ScreenName));
                        identity.AddClaim(new Claim("UserId", context.UserId));
                    };
                }).AddCookie(options => 
            {
                options.LoginPath = "/Auth/Login";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
