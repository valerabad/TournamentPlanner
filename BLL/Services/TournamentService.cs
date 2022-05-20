using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Entities;
using TournamentPlanner.DAL.Interfaces;

namespace BLL.Services
{
    public class TournamentService : ITournamentService
    {
        private ITournamentRepository tournamentRepository;
        public TournamentService(ITournamentRepository repo)
        {
            tournamentRepository = repo;
        }

        public void Create(TournamentDTO tourDTO)
        {
            var tournament = new Tournament()
            {
                Id = tourDTO.Id,
                Name = tourDTO.Name,
                Description = tourDTO.Description,
                Logo = tourDTO.Logo,
                WebSite = tourDTO.WebSite,
                CourtsCount = tourDTO.CourtsCount,
                DateEnd = tourDTO.DateEnd,
                DateStart = tourDTO.DateStart,
                Email = tourDTO.Email,
                EntryMethod = (TournamentPlanner.DAL.Entities.EntryMethodEnum)tourDTO.EntryMethod,
                Events = tourDTO.Events
            };
            tournamentRepository.Create(tournament);
        }

        public void DeleteById(int? id)
        {
            tournamentRepository.Delete(id.Value);
        }

        public void Edit(TournamentDTO tourDTO)
        {
            var tour = new Tournament()
            {
                Id = tourDTO.Id,
                Name = tourDTO.Name,
                Description = tourDTO.Description,
                Logo = tourDTO.Logo,
                WebSite = tourDTO.WebSite,
                CourtsCount = tourDTO.CourtsCount,
                DateEnd = tourDTO.DateEnd,
                DateStart = tourDTO.DateStart,
                Email = tourDTO.Email,
                EntryMethod = (TournamentPlanner.DAL.Entities.EntryMethodEnum)tourDTO.EntryMethod,
                Events = tourDTO.Events
            };
            tournamentRepository.Update(tour);
        }

        public IEnumerable<TournamentDTO> GetAll()
        {
            return tournamentRepository.GetAll().Select(x=>new TournamentDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Logo = x.Logo,
                WebSite = x.WebSite,
                CourtsCount = x.CourtsCount,
                DateEnd = x.DateEnd,
                DateStart = x.DateStart,
                Email = x.Email,
                EntryMethod = (DTO.EntryMethodEnum)x.EntryMethod,
                Events = x.Events
            });
        }

        public TournamentDTO GetTourById(int? id)
        {
            var tour = tournamentRepository.Get(id.Value);
            return new TournamentDTO()
            {
                Id=tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                Logo = tour.Logo,
                WebSite = tour.WebSite,
                CourtsCount = tour.CourtsCount,
                DateEnd = tour.DateEnd,
                DateStart = tour.DateStart,
                Email = tour.Email,
                EntryMethod = (DTO.EntryMethodEnum)tour.EntryMethod,
                Events = tour.Events
            };
        }

        public void Publish()
        {
            throw new NotImplementedException();
        }

        public void AddPlayer(int tourId, PlayerDTO _player)
        {
            Player player = new Player()
            {
                Id = _player.Id,
                FirstName = _player.FirstName,
                LastName = _player.LastName,
                Birthday = _player.Birthday,
                ClubId = _player.ClubId,
                EntryMethod = _player.EntryMethod,
                Gender = _player.Gender,
                Notes = _player.Notes,
                UserId = _player.UserId,
                //Tournaments
                //Clubs
            };

            tournamentRepository.AddPlayer(tourId, player);
        }

        public IEnumerable<TournamentDTO> GetByActualDate()
        {
            var tours = tournamentRepository.GetAll()
                .Where(x=>x.DateStart >= DateTime.Now)
                .Select(x => new TournamentDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Logo = x.Logo,
                    WebSite = x.WebSite,
                    CourtsCount = x.CourtsCount,
                    DateEnd = x.DateEnd,
                    DateStart = x.DateStart,
                    Email = x.Email,
                    EntryMethod = (DTO.EntryMethodEnum)x.EntryMethod,
                    Events = x.Events
                }); 
            return tours;
        }

        public IEnumerable<EventDTO> GetEvetsList()
        {
            return tournamentRepository.GetEvents().Select(x => new EventDTO()
            {
                Id=x.Id,
                Title = x.Title,
                Gender = x.Gender,
                Type = x.Type,
                Fee = x.Fee
            });
        }
    }
}
