using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public enum EntryMethodEnum { system, manual } // TODO move to another place for global access
    public class TournamentDTO
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
        public virtual List<PlayerDTO> Players { get; set; } = new List<PlayerDTO>();


    }
}
