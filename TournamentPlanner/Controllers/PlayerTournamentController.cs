using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TournamentPlanner.Models;

namespace TournamentPlanner.Controllers
{
    public class PlayerTournamentController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly ITournamentService tourService;
        private readonly UserManager<DAL.Entities.User> userManager;
        public PlayerTournamentController(IPlayerService _playerService, UserManager<DAL.Entities.User> _userManager, ITournamentService _tourService)
        {
            playerService = _playerService;
            userManager = _userManager;
            tourService = _tourService;
        }
        public async Task<IActionResult> AddPlayerToTour(int? id)
        {
            var currTour = tourService.GetTourById(id);

            TournamentViewModel tournamentViewModel = new TournamentViewModel()
            {
                Id = currTour.Id,
                Name = currTour.Name,
                Description = currTour.Description,
                Email = currTour.Email,
                WebSite = currTour.WebSite,
                Logo = currTour.Logo,
                DateEnd = currTour.DateEnd,
                DateStart = currTour.DateStart,
                EntryMethod = currTour.EntryMethod,
                Events = currTour.Events,
                CourtsCount = currTour.CourtsCount
            };

            var user = await userManager.FindByEmailAsync(User.Identity.Name);

            var currPlayer = playerService.GetPlayers().FirstOrDefault(x => x.UserId == user.Id);


            PlayerViewModel playerViewModel = new PlayerViewModel()
            {
                Id = currPlayer.Id,
                ClubId = currPlayer.ClubId,
                FirstName = currPlayer.FirstName,
                LastName = currPlayer.LastName,
                EntryMethod = currPlayer.EntryMethod,
                AddressId = currPlayer.AddressId,
                Birthday = currPlayer.Birthday,
                Gender = currPlayer.Gender,
                Notes = currPlayer.Notes
            };

            PlayerTournamentViewModel playerTournamentViewModel = new PlayerTournamentViewModel()
            {
                Player = playerViewModel,
                Tournament = tournamentViewModel
            };

            return View(playerTournamentViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddPlayerToTour(TournamentsController playerTournamentViewModel)
        {

            return View();
        }
    }
}
