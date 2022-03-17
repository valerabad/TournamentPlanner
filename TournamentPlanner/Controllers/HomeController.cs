using Microsoft.AspNetCore.Mvc;

namespace TournamentPlanner.Models
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
