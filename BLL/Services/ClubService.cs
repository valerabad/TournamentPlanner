using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Entities;

namespace BLL.Services
{
    public class ClubService : IClubService
    {
        // UNitOFWorks is missed
        IClubRepository clubRepository { get; set; }
        IPlayerRepository playerRepository { get; set; }
        public ClubService(IClubRepository repo, IPlayerRepository playerRepository)
        {
            clubRepository = repo;
            this.playerRepository = playerRepository;
        }

        public IEnumerable<ClubDTO> GetClubs()
        {
            var clubs = clubRepository.GetAll().Select(x => new ClubDTO 
            { 
               Title = x.Title,
               Description = x.Description,
               Id = x.Id,
               Logo =x.Logo
            });
            return clubs;
        }

        public ClubDTO GetClub(int? id)
        {
            var foundClub = clubRepository.Get(id);
            return new ClubDTO { 
                Title = foundClub.Title, 
                Description = foundClub.Description, 
                Id = foundClub.Id, 
                Logo =foundClub.Logo
            };
        }

        public IEnumerable<PlayerDTO> GetPlayersByClubId(int id)
        {
            return clubRepository.GetPlayersByClubId(id).Select(x => new PlayerDTO
            {
                Id = x.Id,
                AddressId = x.AddressId,
                Birthday = x.Birthday,
                ClubId = x.ClubId,
                EntryMethod = x.EntryMethod,
                FirstName = x.FirstName,
                Gender = x.Gender,
                LastName = x.LastName,
                Notes = x.Notes
            });
        }

        public int GetCountPlayers(int id)
        {
            return GetPlayersByClubId(id).Count();
        }

        public void DeletePlayer(int? playerId, int? clubId)
        {
            playerRepository.UpdateClub(playerId, null);
        }

        public void Create(ClubDTO clubDTO)
        {
            var club = new Club()
            {
                Title = clubDTO.Title,
                Id = clubDTO.Id,
                Logo = clubDTO.Logo
            };
            clubRepository.CreateClub(club);
        }
    }
}
