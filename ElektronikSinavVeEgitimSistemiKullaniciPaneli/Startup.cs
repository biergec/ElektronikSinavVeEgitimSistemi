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

            services.AddIdentityCore<AppUser>(options => { });

            services.AddDefaultIdentity<AppUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 1;
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EfContext>();


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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                EnsureRolesAsync(roleManager).Wait();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Serilog'u LoggerFactory'e ekleyip uygulamanın serilog üzerinden logging yapacağını belirtiyoruz
            loggerFactory.AddSerilog();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        // Rollerin development anında eklenmesi 
        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roleAlreadyExistAdmin = await roleManager.RoleExistsAsync(UyelikTuru.Admin.ToString());
            if (!roleAlreadyExistAdmin)
                await roleManager.CreateAsync(new IdentityRole(UyelikTuru.Admin.ToString()));

            var roleAlreadyExistEgitmen = await roleManager.RoleExistsAsync(UyelikTuru.Egitmen.ToString());
            if (!roleAlreadyExistEgitmen)
                await roleManager.CreateAsync(new IdentityRole(UyelikTuru.Egitmen.ToString()));

            var roleAlreadyExistOgrenci = await roleManager.RoleExistsAsync(UyelikTuru.Ogrenci.ToString());
            if (!roleAlreadyExistOgrenci)
                await roleManager.CreateAsync(new IdentityRole(UyelikTuru.Ogrenci.ToString()));
        }
    }
}
