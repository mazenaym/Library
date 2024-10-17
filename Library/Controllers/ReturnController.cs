using Library.Data;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class ReturnController : Controller
    {

        private readonly ILogger<ReturnController> _logger;
        private readonly ApplicationDbContext _context;
        public ReturnController(ILogger<ReturnController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var returnList = _context.returns.ToList();
            return View(returnList);
        }
    }
}
