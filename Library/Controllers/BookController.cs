using Library.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize("AdminRole")]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly ApplicationDbContext _context;
        public BookController(ILogger<BookController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var bookList = _context.Books.ToList();
            return View(bookList);
        }
    }
}
