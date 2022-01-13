
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TournamentPlanner.DAL.EF;

namespace TournamentPlanner
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DBContext>>()))
            {
                //// Look for any movies.
                //if (context.Players.Any())
                //{
                //    return;   // DB has been seeded
                //}

                //context.SaveChanges();
            }
        }
    }
}
