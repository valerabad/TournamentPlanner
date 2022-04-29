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

// TODO remove
using TournamentPlanner.Controllers;

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
                .AddEntityFrameworkStores<DBContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IClubService, ClubService>();
            services.AddTransient<IExcelService, ExcelService>();
            services.AddTransient<ITournamentService, TournamentService>();

            //services.AddTransient<TournamentsController>();

            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
                
            services.AddTransient <IPlayerRepository, PlayerRepository>();
            services.AddTransient <IClubRepository, ClubRepository>();
            services.AddTransient<ITournamentRepository, TournamentRepository>();

            services.Configure<EmailOptions>(Configuration.GetSection(EmailOptions.Settings));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //env.EnvironmentName = "Production";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseStatusCodePages();

            app.Map("/error", ap => ap.Run(async context =>
            {
                await context.Response.WriteAsync("Midleware Error: something has wong");
            }));

            //app.Run(async (context) =>
            //{
            //    int x = 0;
            //    int y = 8 / x;
            //    await context.Response.WriteAsync($"Result = {y}");
            //});

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
