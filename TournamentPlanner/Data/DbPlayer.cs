using System;
using System.ComponentModel.DataAnnotations;

namespace TournamentPlanner.Models
{
    public class DbPlayer
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
        public int ClubId { get; set; }

    }
}
