using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentPlanner.DAL.Entities
{
    public enum EntryMethodEnum { system, manual } // Where is the best location for global variable?
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Logo { get; set; } // how can we keep image?
        public EntryMethodEnum EntryMethod { get; set; }
        public string Events { get; set; } // how we can save selected events, E.g: ms, ms, wd, ws, xd
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int CourtsCount { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();

        // TODO
        //public int AddressID { get; set; }


    }
}
