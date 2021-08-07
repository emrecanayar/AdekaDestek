using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Business.Extensions;
using AdekaDestek.Entities.Concrete;
using AdekaDestek.IdentityAPI.AutoMapper.Profiles;
using AdekaDestek.IdentityAPI.CustomRules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AdekaDestek.IdentityAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdekaDestek.IdentityAPI", Version = "v1" });
            });

            //Oluþturulan MD5 þifreleme algoritmasýnýn konfigürasyonu.
            services.AddTransient<IPasswordHasher<User>, CustomPasswordHasher>();

            //AutoMapper kütüphanesinin yapýlandýrýlmasý.
            services.AddAutoMapper(typeof(UserProfile));
            //
            services.LoadMyServices(connectionString: "Data Source = 192.168.50.26; Initial Catalog = AdekaDestek; User Id=sa;Password = Ankara06;");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdekaDestek.IdentityAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
