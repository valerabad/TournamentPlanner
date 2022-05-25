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

        void Edit(TournamentDTO tour);
        void DeleteById(int? id);
        void Publish();
        //TODO method for several players
        //void AddPlayers(List<PlayerDTO> players);
        void AddPlayer(int tourId, int playerId);
        IEnumerable<TournamentDTO> GetByActualDate();

        public IEnumerable<EventDTO> GetEvetsList();
        public bool IsPlayerInTour(int tourId, int playerId);
    }
}
