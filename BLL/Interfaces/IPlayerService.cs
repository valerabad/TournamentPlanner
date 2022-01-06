using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    internal interface IPlayerService
    {
        PlayerDTO GetPlayer(int? id);
        IEnumerable<PlayerDTO> GetPlayers();
        void Dispose();
    }
}
