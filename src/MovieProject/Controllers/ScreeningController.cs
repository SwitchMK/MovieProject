using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieProject.Controllers
{
    [Authorize(Roles = "User")]
    public class ScreeningController : Controller
    {
        public IActionResult Screenings()
        {
            return View();
        }

        [Authorize(Roles = "Editor")]
        public IActionResult ScreeningsManagement()
        {
            return View();
        }
    }
}
