using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
