using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentPlanner.DAL.Entities
{
    public class Event
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Fee { get; set; }

    }
}
