using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieProject.Controllers
{
    [Authorize]
    public class ScreeningController : Controller
    {
        public IActionResult Screenings()
        {
            return View();
        }

        [Authorize(Roles = "Editor,Administrator")]
        public IActionResult ScreeningsManagement()
        {
            return View();
        }
    }
}
