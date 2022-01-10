using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Interfaces;
using TournamentPlanner.DAL.Entities;

namespace BLL.Services
{
    public class PlayerService : IPlayerService
    {
        IUnitOfWork Database { get; set; }

        public PlayerService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<PlayerDTO> GetPlayers()
        {
            var players = Database.PlayersRepository.GetAll().Select(x => new PlayerDTO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                EntryMethod = x.EntryMethod,
                AddressId = x.AddressId,
                Birthday = x.Birthday,
                Gender = x.Gender,
                Notes = x.Notes,
                Id = x.Id
            });


            return players;

            // применяем автомаппер для проекции одной коллекции на другую
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Phone, PhoneDTO>()).CreateMapper();
            //return mapper.Map<IEnumerable<Phone>, List<PhoneDTO>>(Database.Phones.GetAll());
        }

        public PlayerDTO GetPlayer(int? id)
        {
            var player = Database.PlayersRepository.Get(id.Value);

            return new PlayerDTO
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                EntryMethod = player.EntryMethod,
                AddressId = player.AddressId,
                Birthday = player.Birthday,
                Gender = player.Gender,
                Notes = player.Notes,
                Id = player.Id
            };
        }

        public string Send()
        {
            return "I'm player!";
        }

        public void Create(PlayerDTO playerDTO)
        {
            var player = new Player()
            {
                Id = playerDTO.Id,
                AddressId = playerDTO.AddressId,
                FirstName = playerDTO.FirstName,
                Birthday = playerDTO.Birthday,
                ClubId = playerDTO.ClubId,
                Gender = playerDTO.Gender,
                EntryMethod = playerDTO.EntryMethod,
                LastName = playerDTO.LastName,
                Notes = playerDTO.Notes
            };
            Database.PlayersRepository.Create(player);
        }

        public void Edit(PlayerDTO playerDTO)
        {
            var updatedPlayer = new Player()
            {
                Id = playerDTO.Id,
                AddressId = playerDTO.AddressId,
                FirstName = playerDTO.FirstName,
                Birthday = playerDTO.Birthday,
                ClubId = playerDTO.ClubId,
                Gender = playerDTO.Gender,
                EntryMethod = playerDTO.EntryMethod,
                LastName = playerDTO.LastName,
                Notes = playerDTO.Notes
            };

            if (updatedPlayer != null)
                Database.PlayersRepository.Update(updatedPlayer);

        }

        public void Delete(int? id)
        {
            Database.PlayersRepository.Delete(id.Value);

        }
    }
}
