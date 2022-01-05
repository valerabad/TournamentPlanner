using System;

namespace TournamentPlanner.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Notes { get; set; }

        public int AddressId { get; set; }
        public int ClubId { get; set; }

    }
}
