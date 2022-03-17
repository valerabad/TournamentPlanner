using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BLL.Interfaces;
using BLL.Services;
using TournamentPlanner.DAL.Interfaces;
using DAL.Repositories;
using TournamentPlanner.DAL.EF;
using DAL.Interfaces;
using TournamentPlanner.DAL.Repositories;
using System.Text;
using Microsoft.AspNetCore.Http;
using TournamentPlanner.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace TournamentPlanner
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();

            _services = services;

            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBContext")));

            services.AddControllersWithViews();
         
            services.AddIdentity<User, IdentityRole>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DBContext>();

            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IClubService, ClubService>();
            services.AddTransient<IExcelService, ExcelService>();

            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            services.AddTransient <IPlayerRepository, PlayerRepository>();
            services.AddTransient <IClubRepository, ClubRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
