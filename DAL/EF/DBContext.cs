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
            //Database.EnsureDeleted();
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
                new Player { Id = 1, FirstName = "Valeriy", ClubId = 1 },
                new Player { Id = 2, FirstName = "Anton", ClubId = 1 },
                new Player { Id = 3, FirstName = "Elena", ClubId = 2 },
                new Player { Id = 4, FirstName = "Kateryna", ClubId = 3 },
                new Player { Id = 5, FirstName = "Sergey", ClubId = 3 }
                );


        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Club> Clubs { get; set; }
    }
}
