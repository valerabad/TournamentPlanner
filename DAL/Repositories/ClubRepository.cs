using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.EF;
using TournamentPlanner.DAL.Entities;

namespace DAL.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private DBContext _context;
        public ClubRepository(DBContext db)
        {
            _context = db;
        }
        public IEnumerable<Club> GetAll()
        {
            return _context.Clubs;
        }
    }
}
