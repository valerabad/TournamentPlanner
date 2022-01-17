using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IClubService
    {
        IEnumerable<ClubDTO> GetClubs();

        ClubDTO GetClub(int? id);
        IEnumerable<PlayerDTO> GetPlayersByClubId(int id);
        int GetCountPlayers(int id);
    }
}
