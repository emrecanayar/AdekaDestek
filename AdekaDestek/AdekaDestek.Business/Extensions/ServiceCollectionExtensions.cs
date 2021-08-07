using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Business.Abstract;
using AdekaDestek.Business.Adapters.SmsService;
using AdekaDestek.Business.Concrete;
using AdekaDestek.Core.Utilities.Helpers.Abstract;
using AdekaDestek.Core.Utilities.Helpers.Concrete;
using AdekaDestek.DataAccess.Concrete.EntityFramework.Contexts;
using AdekaDestek.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdekaDestek.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<AdekaDestekContext>(options => options.UseSqlServer(connectionString));
            serviceCollection.AddIdentity<User, Role>(options =>
            {
                //User Password Options
                options.Password.RequireDigit = true;                //Kullanıcı şifrelerinde rakam kısmının bulunması.
                options.Password.RequiredLength = 6;                 //Kullanıcının şifresinin en az 10 karakter olması gerekmektedir.
                options.Password.RequiredUniqueChars = 1;            //Kullanıcının şifresinde Unique karakterlenden kaçtane olması gerektiğini belirler.(!,?)
                options.Password.RequireNonAlphanumeric = false;     //Kullanıcının şifresinde unique karakterlerin kullanılmasını sağlar.
                options.Password.RequireLowercase = false;           //Kullanıcının şifrelerinde küçük harf kullanılmasına izin verir.
                options.Password.RequireUppercase = false;           //Kullanıcının şifrelerinde büyük harf kullanılmasına izin verir.

                //User Username and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$"; //Kullanıcı oluşturuken kullanılması gereken karakterler.
                options.User.RequireUniqueEmail = true; //Oluşturulan email veritabanında sadece bir kere bulunabilir.
            }).AddEntityFrameworkStores<AdekaDestekContext>().AddDefaultTokenProviders();

            serviceCollection.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(15);
            });


            serviceCollection.AddScoped<ITwoFactorService, TwoFactorManager>();
            serviceCollection.AddScoped<IMailService, MailManager>();
            serviceCollection.AddScoped<ISmsService, SmsManager>();

            return serviceCollection;
        }
    }
}
