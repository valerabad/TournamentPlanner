using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Entities;

namespace DAL.Interfaces
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAll();
        Player Get(int id);
        //IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(Player item);
        //void Update(T item);
        //void Delete(int id);
    }
}
