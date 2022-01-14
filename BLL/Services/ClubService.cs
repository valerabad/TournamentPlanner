using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ClubService : IClubService
    {
        // UNitOFWorks is missed
        IClubRepository clubRepository { get; set; }
        public ClubService(IClubRepository repo)
        {
            clubRepository = repo;
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
    }
}
