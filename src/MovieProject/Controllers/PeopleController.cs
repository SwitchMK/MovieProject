using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieProject.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        public IActionResult People()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
