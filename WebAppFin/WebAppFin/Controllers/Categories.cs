using Microsoft.AspNetCore.Mvc;

namespace WebAppFin.Controllers
{
    public class Categories : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
