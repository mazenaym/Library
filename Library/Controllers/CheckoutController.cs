using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
