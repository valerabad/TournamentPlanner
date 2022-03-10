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

        public IEnumerable<Player> GetPlayersWithoutClub()
        {
            return _context.Players.Where(x => x.ClubId == null);
        }

        //public void RemovePlayer(int playerId)
        //{
        //    var player = _context.Players.
        //        Where(x => x.Id == playerId).
        //        FirstOrDefault().ClubId = null;
        //    _context.SaveChanges();
        //}

        public void CreateClub(Club club)
        {
            _context.Clubs.Add(club);
            _context.SaveChanges();
        }

        //public IEnumerable<Player> GetPlayers()
        //{
        //    return _context.Clubs.FirstOrDefault().Players;
        //}
    }
}
