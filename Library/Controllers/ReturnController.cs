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

        public IActionResult ReturnBook(int checkoutId)
        {
            var checkout = _context.checkouts.Find(checkoutId);
            checkout.ReturnedDate = DateTime.Now;

            if (checkout.DueDate < checkout.ReturnedDate)
            {
                TimeSpan overdueDays = checkout.ReturnedDate.Value - checkout.DueDate;  
                checkout.Penalty = CalculatePenalty(overdueDays);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");   
        }

        private int CalculatePenalty(TimeSpan overdueDays)
        {
            return overdueDays.Days * 5;
        }
    }
}
