using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieProject.Controllers
{
    [Authorize]
    public class FilmController : Controller
    {
        public IActionResult Films()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
