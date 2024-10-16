using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class SearchHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
