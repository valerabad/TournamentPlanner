using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TournamentPlanner.Models
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Notes { get; set; }

        public int AddressId { get; set; }
        public int? ClubId { get; set; }

        public IEnumerable<ClubViewModel> Clubs { get; set; }

        public string EntryMethod { get; set; }

    }
}
