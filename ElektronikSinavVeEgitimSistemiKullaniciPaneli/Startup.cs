using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.CanliYayin;
using BusinessLayer.DersIslemleri;
using BusinessLayer.Login;
using BusinessLayer.Sinav;
using BusinessLayer.SinavGiris;
using DAL.Context;
using DAL.UnitOfWork;
using EntityLayer.Login;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli
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
            // Serilog ve serilog'un storage olarak elasticsearch'ü kullanacağını belirtiyoruz
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration["ElasticConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                }).CreateLogger();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<EfContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection"),
                    optionsBuilder =>
                        optionsBuilder.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name)));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<EfContext>()
                .AddDefaultTokenProviders();

            //Password Strength Setting
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            //Setting the Account Login page
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Home/Index"; // If the LoginPath is not set here,
                // ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Login/Index"; // If the LogoutPath is not set here,
                // ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Pages/AccessDenied"; // If the AccessDeniedPath is
                // not set here, ASP.NET Core 
                // will default to 
                // /Account/AccessDenied
                options.SlidingExpiration = true;
            });

            //services.AddDefaultIdentity<AppUser>().AddRoles<IdentityRole>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IKayitOl, KayitOlManager>();
            services.AddScoped<ISinavOlustur, SinavOlustur>();
            services.AddScoped<IEgitmenSinavBilgileri, EgitmenSinavBilgileri>();
            services.AddScoped<IDersIslemleri, DersIslemleri>();
            services.AddScoped<IKayitliDerslerim, KayitliDerslerimManger>();
            services.AddScoped<IDersListesiSelectListItem, DersListesiSelectListItem>();
            services.AddScoped<IOgrenciGirebilecegiSinavlar, OgrenciGirebilecegiSinavlar>();
            services.AddScoped<ISinavBilgileri, SinavBilgileri>();
            services.AddScoped<ISinavKayit, SinavKayit>();
            services.AddScoped<ISinavNotlandir, SinavNotlandir>();
            services.AddScoped<ICanliYayinIslemleri, CanliYayinIslemleri>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(10);
                options.LoginPath = "/Login/Index";
                options.SlidingExpiration = true;
            });

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Serilog'u LoggerFactory'e ekleyip uygulamanın serilog üzerinden logging yapacağını belirtiyoruz
            loggerFactory.AddSerilog();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateUserRoles(serviceProvider).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var roleCheck2 = await RoleManager.RoleExistsAsync("Egitmen");
            if (!roleCheck2)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Egitmen"));
            }

            var roleCheck3 = await RoleManager.RoleExistsAsync("Ogrenci");
            if (!roleCheck3)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Ogrenci"));
            }

            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
            var user = await UserManager.FindByEmailAsync("mustafaergec225@gmail.com");
            await UserManager.AddToRoleAsync(user, "Egitmen");
            await UserManager.AddToRoleAsync(user, "Admin");
        }

    }
}
