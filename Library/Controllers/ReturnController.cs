using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class ReturnController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
