using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TournamentPlanner.Data;

namespace TournamentPlanner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //var builder = new ConfigurationBuilder();
            //// ��������� ���� � �������� ��������
            //builder.SetBasePath(Directory.GetCurrentDirectory());
            //// �������� ������������ �� ����� appsettings.json
            //builder.AddJsonFile("appsettings.json");
            //// ������� ������������
            //var config = builder.Build();
            //// �������� ������ �����������
            //string connectionString = config.GetConnectionString("DefaultConnection");

            //var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            //var options = optionsBuilder
            //    .UseSqlServer(connectionString)
            //    .Options;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
