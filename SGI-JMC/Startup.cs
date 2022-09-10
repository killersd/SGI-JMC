using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SGI_JMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SGI_JMC.Data;
using SGI_JMC.Settings;
using SGI_JMC.Services;

namespace SGI_JMC
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true; //false
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; //idem
                options.Password.RequireNonAlphanumeric = false; //true
                options.Password.RequireUppercase = false; //true;
                options.Password.RequireLowercase = false; //true;
                options.Password.RequireDigit = false; //true;
                options.Password.RequiredUniqueChars = 1; //1;
                options.Password.RequiredLength = 6; //6;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); //5
                options.Lockout.MaxFailedAccessAttempts = 5; //5
                options.Lockout.AllowedForNewUsers = true; //true		
                options.SignIn.RequireConfirmedEmail = false; //false
                options.SignIn.RequireConfirmedPhoneNumber = false; //false
                options.SignIn.RequireConfirmedAccount = false; //false
            })
             .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<GmailSettings>(Configuration.GetSection(nameof(GmailSettings)));
            services.AddSingleton<IEmailService, GmailService>();
            //services.AddIdentityCore<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "AppControleUsuarios"; //AspNetCore.Cookies
                options.Cookie.HttpOnly = true; //true
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5); //14 dias
                options.LoginPath = "/Usuario/Login"; // /Account/Login
                options.LogoutPath = "/Home/Index";  // /Account/Logout
                options.AccessDeniedPath = "/Usuario/AcessoRestrito"; // /Account/AccessDenied
                options.SlidingExpiration = true; //true - gera um novo cookie a cada requisição se o cookie estiver com menos de meia vida
                options.ReturnUrlParameter = "returnUrl"; //returnUrl
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuario}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            Inicializador.InicializarIdentity(userManager,roleManager);
        }
    }
}
