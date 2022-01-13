using Microsoft.AspNetCore.Mvc;
using TournamentPlanner.Models;
using BLL.DTO;
using System.Linq;
using BLL.Interfaces;

namespace TournamentPlanner.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService clubService;
        public ClubController(IClubService clubService)
        {
            this.clubService = clubService;
        }
        public IActionResult Index()
        {
            var clubs = clubService.GetClubs().Select(x => new ClubViewModel
            {
                Logo = x.Logo,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            });

            return View(clubs);
        }
    }
}
