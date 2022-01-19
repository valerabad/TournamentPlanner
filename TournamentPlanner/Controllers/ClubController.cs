using Microsoft.AspNetCore.Mvc;
using TournamentPlanner.Models;
using BLL.DTO;
using System.Linq;
using BLL.Interfaces;
using System.Threading.Tasks;
using BLL.Services;

namespace TournamentPlanner.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService clubService;
        private readonly IPlayerService playerService;
        public ClubController(IClubService clubService, IPlayerService playerService)
        {
            this.clubService = clubService;
            this.playerService = playerService;
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

        public async Task<IActionResult> Details(int? id) // asynk await
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = clubService.GetClub(id);
            ClubViewModel clubViewModel = new ClubViewModel()
            {
                Id = club.Id,
                Logo = club.Logo,
                Title = club.Title,
                Description = club.Description,
                PlayersList = clubService.GetPlayersByClubId((int)id).Select(x => new PlayerViewModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Birthday = x.Birthday,
                    ClubId = x.ClubId,
                    EntryMethod = x.EntryMethod,
                    Id = x.Id,
                    Gender = x.Gender,
                    AddressId = x.AddressId,
                    Notes = x.Notes
                })

            };

            if (club == null)
            {
                return NotFound();
            }

            ViewData["TotlaPlayers"] = clubService.GetCountPlayers((int)id);
            //ViewData["TotlaPlayers"] = playerService.GetPlayers().Where(x => x.ClubId == id).Count();
            return View(clubViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = clubService.GetClub(id);
            ClubViewModel clubViewModel = new ClubViewModel()
            {
                Id = club.Id,
                Logo = club.Logo,
                Title = club.Title,
                Description = club.Description,
                PlayersList = clubService.GetPlayersByClubId((int)id).Select(x => new PlayerViewModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Birthday = x.Birthday,
                    ClubId = x.ClubId,
                    EntryMethod = x.EntryMethod,
                    Id = x.Id,
                    Gender = x.Gender,
                    AddressId = x.AddressId,
                    Notes = x.Notes
                })

            };

            if (club == null)
            {
                return NotFound();
            }
            return View(clubViewModel);
        }

        public IActionResult DeletePlayer(int playerId)
        {
            clubService.DeletePlayer(playerId);
            return RedirectToAction(nameof(Index));

            //return RedirectToPage("/Edit?id=" + clubId);
        }
    }
}
