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

        public Club Get(int? id)
        {
            return _context.Clubs.Find(id);
        }

        public IEnumerable<Player> GetPlayersByClubId(int id)
        {
            return _context.Players.Where(x=>x.ClubId == id);
        }
    }
}
