using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TournamentPlanner.Controllers
{
    public class IdentityController : Controller
    {
        public IActionResult Index()
        {
            Dictionary<string, object> data
               = new Dictionary<string, object>();
            data.Add("Ключ", "Значение");

            return View(data);
        }
    }
}
