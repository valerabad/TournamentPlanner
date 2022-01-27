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
            services.AddControllersWithViews();

            services.AddDbContext<DBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DBContext")));

            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IClubService, ClubService>();

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

            //app.UseMiddleware<PlayerMiddleware>();

            //app.Run(async (context) =>
            //{
            //    //IPlayerService plService = context.RequestServices.GetService<IPlayerService>();
            //    IPlayerService plService = app.ApplicationServices.GetService<IPlayerService>();
            //    //context.Response.ContentType = "text/html;charset=utf-8";
            //    await context.Response.WriteAsync(plService.Send());
            //});

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Players}/{action=Index}/{id?}");
            });

         

            //app.Run(async context =>
            //{
            //    var sb = new StringBuilder();
            //    sb.Append("<h1>Все сервисы</h1>");
            //    sb.Append("<table>");
            //    sb.Append("<tr><th>Тип</th><th>Lifetime</th><th>Реализация</th></tr>");
            //    foreach (var svc in _services)
            //    {
            //        sb.Append("<tr>");
            //        sb.Append($"<td>{svc.ServiceType.FullName}</td>");
            //        sb.Append($"<td>{svc.Lifetime}</td>");
            //        sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
            //        sb.Append("</tr>");
            //    }
            //    sb.Append("</table>");
            //    context.Response.ContentType = "text/html;charset=utf-8";
            //    await context.Response.WriteAsync(sb.ToString());
            //});
        }
    }
}
