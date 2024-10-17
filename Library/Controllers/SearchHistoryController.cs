using Library.Data;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class SearchHistoryController : Controller
    {
        private readonly ILogger<SearchHistoryController> _logger;
        private readonly ApplicationDbContext _context;
        public SearchHistoryController(ILogger<SearchHistoryController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var searchhistoryList = _context.searchHistories.ToList(); 
            return View(searchhistoryList);
        }
    }
}
