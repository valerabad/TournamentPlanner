using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPlayerService
    {
        IEnumerable<PlayerDTO> GetPlayers();
        PlayerDTO GetPlayer(int? id);
        void Create(PlayerDTO player);
        string Send();

        void Edit(PlayerDTO player);
        void Delete(int? id);

        //void Dispose();
    }
}
