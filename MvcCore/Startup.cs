using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCore.Data;
using MvcCore.Helpers;
using MvcCore.Repositories;

namespace MvcCore
{
    public class Startup
    {
        IConfiguration Configuration;

        public Startup (IConfiguration conf) {
            this.Configuration = conf;
        }

        public void ConfigureServices(IServiceCollection services){

            services.AddTransient<PathProvider>();

            #region CONECTION STRINGS
            //String conexionString = this.Configuration.GetConnectionString("CochesSQLServer");
            //String conexionString = this.Configuration.GetConnectionString("CochesMySql");
            #endregion

            #region REPOSITORIES
            //services.AddTransient<IRepositoryCoches, CochesRepositorySQLServer>();
            //services.AddTransient<IRepositoryCoches, CochesRepositoryMysql>();
            services.AddTransient<IRepositoryCoches, CochesRepositoryXML>();
            #endregion

            #region CONTEXT
            //services.AddDbContext<CochesContext>(options => options.UseSqlServer(conexionString));
            //services.AddDbContext<CochesContext>(options => options.UseMySql(conexionString, ServerVersion.AutoDetect(conexionString)));
            #endregion

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
