using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ILogger<CheckoutController> _logger;
        private readonly ApplicationDbContext _context;
        public CheckoutController(ILogger<CheckoutController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var CheckoutList=_context.checkouts.Include("Member").Include("Book").ToList();
            return View(CheckoutList);
        }

        public IActionResult Create()
        {
            var books = _context.Books.ToList(); 
            ViewBag.BookID = books.Select(book => new SelectListItem
            {
                Value = book.BookID.ToString(), 
                Text = book.Title + " " + book.Author 
            }).ToList();

            var members = _context.members.ToList();
            ViewBag.MemberID = members.Select(member => new SelectListItem
            {
                Value = member.MemberID.ToString(),
                Text = member.FirstName + " " + member.LastName
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Checkout checkout)
        {
            
            if (ModelState.IsValid)
            {
                _context.checkouts.Add(checkout);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checkout);  // In case of validation errors, return the form with entered data
        }
    }
}
