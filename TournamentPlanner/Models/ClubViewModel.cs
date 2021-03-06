using System.Linq;
using System.Collections.Generic;

namespace TournamentPlanner.Models
{
    public class ClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public IEnumerable<PlayerViewModel> PlayersList { get; set; }
    }
}
