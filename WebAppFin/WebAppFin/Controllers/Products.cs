using Microsoft.AspNetCore.Mvc;

namespace WebAppFin.Controllers
{
    public class Products : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
