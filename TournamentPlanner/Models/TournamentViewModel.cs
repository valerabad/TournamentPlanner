using BLL.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace TournamentPlanner.Models
{
    public class TournamentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Logo { get; set; } // how can we keep image?
        public EntryMethodEnum EntryMethod { get; set; }
        public string Events { get; set; } // how we can save selected events, E.g: ms, ms, wd, ws, xd
        //[DataType(DataType.DateTime)]
        public DateTime DateStart { get; set; }
        //[DataType(DataType.DateTime)]
        public DateTime DateEnd { get; set; }
        public int CourtsCount { get; set; }
    }
}
