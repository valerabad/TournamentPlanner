using DAL.Interfaces;
using System.Collections.Generic;
using TournamentPlanner.DAL.EF;
using TournamentPlanner.DAL.Entities;
using TournamentPlanner.DAL.Interfaces;

namespace TournamentPlanner.DAL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private DBContext _context;

        public PlayerRepository(DBContext context)
        {
            this._context = context;
        }

        public IEnumerable<Player> GetAll()
        {
            return _context.Players;
        }

        public Player Get(int id)
        {
            return _context.Players.Find(id);
        }

        public void Create(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

    }
}
