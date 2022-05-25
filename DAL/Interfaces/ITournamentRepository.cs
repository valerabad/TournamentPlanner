using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Entities;

namespace TournamentPlanner.DAL.Interfaces
{
    public interface ITournamentRepository : IRepository<Tournament>
    {
        public IEnumerable<Event> GetEvents();
        public void AddPlayer(int id, int playerId);
        public bool IsPlayerInTour(int tourId, int playerId);
    }
}
