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
            //appsettings.json dosyasından veri okumak.
            services.Configure<TwoFactorOptions>(Configuration.GetSection("TwoFactorOptions"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.Configure<SmsSettings>(Configuration.GetSection("SmsSettings"));

            //appsettings.json dosyasına veri yazmak.
            services.ConfigureWritable<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.ConfigureWritable<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.ConfigureWritable<TwoFactorOptions>(Configuration.GetSection("TwoFactorOptions"));
            services.ConfigureWritable<SmsSettings>(Configuration.GetSection("SmsSettings"));

            //Projemizin bir MVC projesi olduğunu belirtiyoruz. - FluentValidation kütüphanesini servisinide burada ekliyoruz.
            services.AddControllersWithViews(options =>
            {

                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu alan boş geçilmemelidir.");
                options.Filters.Add<MvcExceptionFilter>();

            }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>()).AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {

                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); //Json Serializer ayarı. Ajax için bu ayarlar gerekli.
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }).AddNToastNotifyToastr(); //Toast mesajlarını MVC üzerinden göndermek için.

            //Session oturum yapısının eklenmesi(Kullanıcı sisteme giriş yaptığında server'da oluşan oturum yapısı.)
            services.AddSession();

            //Oluşturulan MD5 şifreleme algoritmasının konfigürasyonu.
            services.AddTransient<IPasswordHasher<User>, CustomPasswordHasher>();

            //Bordro Görüntülemek için oluşturulmuş servis konfigürasyonları.
            services.AddScoped<IPayrollService, PayrollManager>();

            //AutoMapper kütüphanesinin yapılandırılması.
            services.AddAutoMapper(typeof(UserProfile));

            //Business katmanında oluşturduğumuz servis ve ayar tanımlamaları.
            services.LoadMyServices(connectionString: Configuration.GetConnectionString("LocalDB"));

            //Cookie işlemleri
            services.ConfigureApplicationCookie(options =>
            {
                //Giriş yapmadan bir sayfaya gitmek istersem sistem beni otomatik buraya yönlendirecek.
                options.LoginPath = new PathString("/Admin/Auth/Login");
                options.LogoutPath = new PathString("/Admin/Auth/Logout");

                options.Cookie = new CookieBuilder
                {
                    Name = "AdekaDestek",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict, //Güvenlik için Strict olması gerekir.
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // Test aşamasında SameAsRequest olabilir. Canlı da Always olmalıdır.
                };
                options.SlidingExpiration = true; //Kullanıcıya siteye giriş yaptığında ona zaman tanımamıza olanak sağlar.
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //Kullanıcının yetkisi yoksa bu sayfaya yönlendirilir.
            });

            //Sms gönderimi için süre tutmak adına tanımlanan sesssion
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
                app.UseStatusCodePagesWithReExecute("/Error/{0}"); //Custom HTTP kodları hata sayfaları.
                //app.UseStatusCodePages(); //404 NotFound uyarısı.
            }

            //Session yapısının oluşmasını istediğimiz için middleware aktifleştir.
            app.UseSession();
            //Image,Css ve JavaScript dosyaları için.
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); //Kimlik doğrulaması
            app.UseAuthorization(); //Yetki kontrolü doğrulaması
            app.UseSession(); //Session katmanı.
            app.UseNToastNotify(); //Toast Mesajlarınu MVC üzerinden göstermek için.

            app.UseEndpoints(endpoints =>
            {
                //Admin Area için Routing yapılanması.
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
