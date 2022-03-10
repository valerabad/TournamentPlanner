using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    // copy model for transfer data, without field which not needed to view (for ex. - Collections)
    public class PlayerDTO
    {
            public int Id { get; set; }
            [Required]
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            [DataType(DataType.Date)]
            public DateTime? Birthday { get; set; }
            public string Notes { get; set; }
            public int AddressId { get; set; }
            public int? ClubId { get; set; }
            public string EntryMethod { get; set; }
            public string UserId { get; set; }
    }
}
