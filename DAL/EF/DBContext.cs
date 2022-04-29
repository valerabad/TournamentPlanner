using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TournamentPlanner.DAL.Entities;

namespace TournamentPlanner.DAL.EF
{
    public class DBContext : IdentityDbContext<User>
    {
        public DBContext (DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Club>().HasData(
                new Club { Id = 1, Title = "Meteor" },
                new Club { Id = 2, Title = "Dynamo" },
                new Club { Id = 3, Title = "Wave" });

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 1, FirstName = "Valeriy", ClubId = 1, EntryMethod = "System" },
                new Player { Id = 2, FirstName = "Anton", ClubId = 1 , EntryMethod = "System" },
                new Player { Id = 3, FirstName = "Elena", ClubId = 2 , EntryMethod = "System" },
                new Player { Id = 4, FirstName = "Kateryna", ClubId = 3 , EntryMethod = "System" },
                new Player { Id = 5, FirstName = "Sergey", ClubId = 3 , EntryMethod = "System" }
                );

            modelBuilder.Entity<Tournament>().HasData(
                new Tournament
                {
                    Id = 1,
                    Name = "Dnipro Open 2022",
                    Description = "Test",
                    CourtsCount = 8,
                    DateStart = DateTime.Now.AddDays(1),
                    DateEnd = DateTime.Now.AddDays(4),
                    Email = "tournamentTest@gmail.com",
                    Events = "ms ws md wd xd ms30+",
                    WebSite = "testWebSite"
                },
                 new Tournament
                 {
                     Id = 2,
                     Name = "Kyiv Open 2022",
                     Description = "Test",
                     CourtsCount = 8,
                     DateStart = DateTime.Now.AddDays(4),
                     DateEnd = DateTime.Now.AddDays(8),
                     Email = "tournamentTest@gmail.com",
                     Events = "ms ws md wd xd ms30+",
                     WebSite = "testWebSite"
                 },
                  new Tournament
                  {
                      Id = 3,
                      Name = "Kharkiv Open 2022",
                      Description = "Test",
                      CourtsCount = 8,
                      DateStart = DateTime.Now.AddDays(38),
                      DateEnd = DateTime.Now.AddDays(42),
                      Email = "tournamentTest@gmail.com",
                      Events = "ms ws md wd xd ms30+",
                      WebSite = "testWebSite"
                  }
                );
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
