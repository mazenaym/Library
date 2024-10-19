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

            if (ModelState.IsValid)
            {
                _context.checkouts.Add(checkout);

                #region Reduce No of Available Copies
                var book = _context.Books.FirstOrDefault(b => b.BookID == checkout.BookID);
                if (book != null)
                {
                    if(checkout.CheckoutDate > checkout.ReturnedDate)
                    {
                        TempData["DateValidationMessage"] = "Return Date should be greater than Checkout Date";
                        return View();
                    }

                    if (book.AvailableCopies > 0)
                    {
                        book.AvailableCopies -= 1;
                        _context.Books.Update(book);
                        _context.SaveChanges();
                    }
                    else
                    {
                        TempData["AvailableCopiesValidationMessage"] = "The book is currently out of stock.";
                        return View();
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "The book could not be found.";
                    return RedirectToAction("Index");
                }
                #endregion

                return RedirectToAction("Index");
            }

            return View(checkout);
        }
    }
}
