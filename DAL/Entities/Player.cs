using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace TournamentPlanner.DAL.Entities
{
    public class Player //: IdentityUser
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; } // nullable
        //public DateTime Birthday { get; set; } // not null
        public string Notes { get; set; }
        public int AddressId { get; set; }
        public string EntryMethod { get; set; }
        public int? ClubId { get; set; }
        public virtual Club Club { get; set; }

    }
}
