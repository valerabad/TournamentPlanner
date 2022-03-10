using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Entities;
using TournamentPlanner.DAL.Interfaces;

namespace DAL.Interfaces
{
    // ??
    public interface IPlayerRepository //: IRepository<IPlayerRepository>
    {
        IEnumerable<Player> GetAll();
        Player Get(int id);
        //IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(Player item);
        void Update(Player item);   
        void Delete(int id);
        void UpdateClub(int? playerId, int? clubId);
    }
}
