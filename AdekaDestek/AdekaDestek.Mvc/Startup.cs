using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AdekaDestek.Business.Extensions;
using AdekaDestek.Core.Utilities.Extensions;
using AdekaDestek.Entities.Concrete;
using AdekaDestek.Mvc.AutoMapper.Profiles;
using AdekaDestek.Mvc.CustomRules;
using AdekaDestek.Mvc.Filters;
using AdekaDestek.Mvc.SapServices.Abstract;
using AdekaDestek.Mvc.SapServices.Concrete;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdekaDestek.Mvc
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940


        public void ConfigureServices(IServiceCollection services)
        {
            //appsettings.json dosyas�ndan veri okumak.
            services.Configure<TwoFactorOptions>(Configuration.GetSection("TwoFactorOptions"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.Configure<SmsSettings>(Configuration.GetSection("SmsSettings"));

            //appsettings.json dosyas�na veri yazmak.
            services.ConfigureWritable<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.ConfigureWritable<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.ConfigureWritable<TwoFactorOptions>(Configuration.GetSection("TwoFactorOptions"));
            services.ConfigureWritable<SmsSettings>(Configuration.GetSection("SmsSettings"));

            //Projemizin bir MVC projesi oldu�unu belirtiyoruz. - FluentValidation k�t�phanesini servisinide burada ekliyoruz.
            services.AddControllersWithViews(options =>
            {

                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu alan bo� ge�ilmemelidir.");
                options.Filters.Add<MvcExceptionFilter>();

            }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>()).AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {

                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); //Json Serializer ayar�. Ajax i�in bu ayarlar gerekli.
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }).AddNToastNotifyToastr(); //Toast mesajlar�n� MVC �zerinden g�ndermek i�in.

            //Session oturum yap�s�n�n eklenmesi(Kullan�c� sisteme giri� yapt���nda server'da olu�an oturum yap�s�.)
            services.AddSession();

            //Olu�turulan MD5 �ifreleme algoritmas�n�n konfig�rasyonu.
            services.AddTransient<IPasswordHasher<User>, CustomPasswordHasher>();

            //Bordro G�r�nt�lemek i�in olu�turulmu� servis konfig�rasyonlar�.
            services.AddScoped<IPayrollService, PayrollManager>();

            //AutoMapper k�t�phanesinin yap�land�r�lmas�.
            services.AddAutoMapper(typeof(UserProfile));

            //Business katman�nda olu�turdu�umuz servis ve ayar tan�mlamalar�.
            services.LoadMyServices(connectionString: Configuration.GetConnectionString("LocalDB"));

            //Cookie i�lemleri
            services.ConfigureApplicationCookie(options =>
            {
                //Giri� yapmadan bir sayfaya gitmek istersem sistem beni otomatik buraya y�nlendirecek.
                options.LoginPath = new PathString("/Admin/Auth/Login");
                options.LogoutPath = new PathString("/Admin/Auth/Logout");

                options.Cookie = new CookieBuilder
                {
                    Name = "AdekaDestek",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict, //G�venlik i�in Strict olmas� gerekir.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // Test a�amas�nda SameAsRequest olabilir. Canl� da Always olmal�d�r.
                };
                options.SlidingExpiration = true; //Kullan�c�ya siteye giri� yapt���nda ona zaman tan�mam�za olanak sa�lar.
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //Kullan�c�n�n yetkisi yoksa bu sayfaya y�nlendirilir.
            });

            //Sms g�nderimi i�in s�re tutmak ad�na tan�mlanan sesssion
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = "AdekaMainSession";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithReExecute("/Error/{0}"); //Custom HTTP kodlar� hata sayfalar�.
                //app.UseStatusCodePages(); //404 NotFound uyar�s�.
            }

            //Session yap�s�n�n olu�mas�n� istedi�imiz i�in middleware aktifle�tir.
            app.UseSession();
            //Image,Css ve JavaScript dosyalar� i�in.
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); //Kimlik do�rulamas�
            app.UseAuthorization(); //Yetki kontrol� do�rulamas�
            app.UseSession(); //Session katman�.
            app.UseNToastNotify(); //Toast Mesajlar�nu MVC �zerinden g�stermek i�in.

            app.UseEndpoints(endpoints =>
            {
                //Admin Area i�in Routing yap�lanmas�.
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
