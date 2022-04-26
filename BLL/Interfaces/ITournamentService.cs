using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITournamentService
    {
        IEnumerable<TournamentDTO> GetAll();
        TournamentDTO GetTourById(int? id);
        void Create(TournamentDTO tour);

        void Edit(TournamentDTO player);
        void DeleteById(int? id);
        void Publish();
        void AddPlayers(List<PlayerDTO> players);
    }
}
