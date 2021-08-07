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
            //appsettings.json dosyasýndan veri okumak.
            services.Configure<TwoFactorOptions>(Configuration.GetSection("TwoFactorOptions"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.Configure<SmsSettings>(Configuration.GetSection("SmsSettings"));

            //appsettings.json dosyasýna veri yazmak.
            services.ConfigureWritable<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.ConfigureWritable<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.ConfigureWritable<TwoFactorOptions>(Configuration.GetSection("TwoFactorOptions"));
            services.ConfigureWritable<SmsSettings>(Configuration.GetSection("SmsSettings"));

            //Projemizin bir MVC projesi olduðunu belirtiyoruz. - FluentValidation kütüphanesini servisinide burada ekliyoruz.
            services.AddControllersWithViews(options =>
            {

                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu alan boþ geçilmemelidir.");
                options.Filters.Add<MvcExceptionFilter>();

            }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>()).AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {

                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); //Json Serializer ayarý. Ajax için bu ayarlar gerekli.
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }).AddNToastNotifyToastr(); //Toast mesajlarýný MVC üzerinden göndermek için.

            //Session oturum yapýsýnýn eklenmesi(Kullanýcý sisteme giriþ yaptýðýnda server'da oluþan oturum yapýsý.)
            services.AddSession();

            //Oluþturulan MD5 þifreleme algoritmasýnýn konfigürasyonu.
            services.AddTransient<IPasswordHasher<User>, CustomPasswordHasher>();

            //Bordro Görüntülemek için oluþturulmuþ servis konfigürasyonlarý.
            services.AddScoped<IPayrollService, PayrollManager>();

            //AutoMapper kütüphanesinin yapýlandýrýlmasý.
            services.AddAutoMapper(typeof(UserProfile));

            //Business katmanýnda oluþturduðumuz servis ve ayar tanýmlamalarý.
            services.LoadMyServices(connectionString: Configuration.GetConnectionString("LocalDB"));

            //Cookie iþlemleri
            services.ConfigureApplicationCookie(options =>
            {
                //Giriþ yapmadan bir sayfaya gitmek istersem sistem beni otomatik buraya yönlendirecek.
                options.LoginPath = new PathString("/Admin/Auth/Login");
                options.LogoutPath = new PathString("/Admin/Auth/Logout");

                options.Cookie = new CookieBuilder
                {
                    Name = "AdekaDestek",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict, //Güvenlik için Strict olmasý gerekir.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // Test aþamasýnda SameAsRequest olabilir. Canlý da Always olmalýdýr.
                };
                options.SlidingExpiration = true; //Kullanýcýya siteye giriþ yaptýðýnda ona zaman tanýmamýza olanak saðlar.
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //Kullanýcýnýn yetkisi yoksa bu sayfaya yönlendirilir.
            });

            //Sms gönderimi için süre tutmak adýna tanýmlanan sesssion
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
                app.UseStatusCodePagesWithReExecute("/Error/{0}"); //Custom HTTP kodlarý hata sayfalarý.
                //app.UseStatusCodePages(); //404 NotFound uyarýsý.
            }

            //Session yapýsýnýn oluþmasýný istediðimiz için middleware aktifleþtir.
            app.UseSession();
            //Image,Css ve JavaScript dosyalarý için.
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); //Kimlik doðrulamasý
            app.UseAuthorization(); //Yetki kontrolü doðrulamasý
            app.UseSession(); //Session katmaný.
            app.UseNToastNotify(); //Toast Mesajlarýnu MVC üzerinden göstermek için.

            app.UseEndpoints(endpoints =>
            {
                //Admin Area için Routing yapýlanmasý.
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
