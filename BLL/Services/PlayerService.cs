using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Interfaces;

namespace BLL.Services
{
    public class PlayerService :IPlayerService
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

            return new PlayerDTO { FirstName = player.FirstName, Id = player.Id }; // add all fields
        }

       
    }
}
