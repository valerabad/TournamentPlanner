using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.EF;
using TournamentPlanner.DAL.Entities;
using TournamentPlanner.DAL.Interfaces;

namespace DAL.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        DBContext _context;
        public TournamentRepository(DBContext db)
        {
            _context = db;
        }
        public void Create(Tournament item)
        {
            _context.Tournaments.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Tournament item)
        {
            _context.Tournaments.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Tournaments.Find(id);
            _context.Tournaments.Remove(item);
            _context.SaveChanges();
        }

        public Tournament Get(int id)
        {
            return _context.Tournaments.Find(id);
        }

        public IEnumerable<Tournament> GetAll()
        {
            return _context.Tournaments;
        }

        public void Update(Tournament item)
        {
            _context.Tournaments.Update(item);
            _context.SaveChanges();
        }
    }
}
