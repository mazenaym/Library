using Library.Data;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class PenaltyController : Controller
    {
        private readonly ILogger<PenaltyController> _logger;
        private readonly ApplicationDbContext _context;
        public PenaltyController(ILogger<PenaltyController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var penaltyList=_context.penalties.ToList();
            return View(penaltyList);
        }
    }
}
