using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Interfaces;

namespace BLL.Services
{
    internal class PlayerService
    {
        IUnitOfWork Database { get; set; }

        public PlayerService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<PlayerDTO> GetPlayers()
        {
            return (IEnumerable<PlayerDTO>)Database.Players.GetAll();
            // применяем автомаппер для проекции одной коллекции на другую
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Phone, PhoneDTO>()).CreateMapper();
            //return mapper.Map<IEnumerable<Phone>, List<PhoneDTO>>(Database.Phones.GetAll());
        }

        public PlayerDTO GetPlayer(int? id)
        {
            var player = Database.Players.Get(id.Value);

            return new PlayerDTO { FirstName = player.FirstName, Id = player.Id }; // add all fields
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
