using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Entities;

namespace DAL.Interfaces
{
    public interface IClubRepository
    {
        IEnumerable<Club> GetAll();
        Club Get(int? id);
        IEnumerable<Player> GetPlayersByClubId(int id);
    }
}
