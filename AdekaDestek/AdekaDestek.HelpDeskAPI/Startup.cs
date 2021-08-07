using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Business.Abstract;
using AdekaDestek.Business.Concrete;
using AdekaDestek.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProgrammersBlog.DataAccess.Abstract;
using ProgrammersBlog.DataAccess.Concrete;

namespace AdekaDestek.HelpDeskAPI
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdekaDestek.HelpDeskAPI", Version = "v1" });
            });

            services.AddDbContext<HelpDeskContext>(options => options.UseSqlServer(connectionString: "Data Source = 192.168.50.26; Initial Catalog = ADEKAHELPDESK; User Id=sa;Password = Ankara06;").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddScoped<AdekaDestek.HelpDeskAPI.DataAccess.Abstract.IUnitOfWork, AdekaDestek.HelpDeskAPI.DataAccess.Concrete.UnitOfWork>();
            services.AddScoped<AdekaDestek.HelpDeskAPI.Business.Abstract.IUserService, AdekaDestek.HelpDeskAPI.Business.Concrete.UserManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdekaDestek.HelpDeskAPI v1"));
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
