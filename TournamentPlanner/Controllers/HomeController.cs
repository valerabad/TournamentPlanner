using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentPlanner.Models
{
    public class HomeController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly UserManager<DAL.Entities.User> _userManager;

        public HomeController(IPlayerService playerService, UserManager<DAL.Entities.User> userManager)
        {
            this.playerService = playerService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.Name != null)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var userAdmin = await _userManager.FindByEmailAsync("admin@gmail.com");

                var adminPlayer = playerService.GetPlayer(6);
                if (adminPlayer != null && adminPlayer.UserId == null)
                {
                    adminPlayer.UserId = userAdmin.Id;
                    playerService.Edit(adminPlayer);
                }
                    
                    

                if (playerService.GetPlayers().Any(x => x.UserId == user.Id))
                    return RedirectToAction("EnabledTournamentsList", "Tournaments");
                else
                    return View();
            }
            else
                return View();
           
        }
    }
}
