using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentPlanner.DAL.Entities
{
    public class Club
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public Club()
        {
            Players = new List<Player>();
        }
    }
}
