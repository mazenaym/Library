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
            var CheckoutList = _context.checkouts
        .Include(c => c.Member)
        .Include(c => c.Book)
        .ToList();
           
            return View(CheckoutList);
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.checkouts
                .Include(c => c.Book)
                .Include(c => c.Member)
                .FirstOrDefaultAsync(m => m.CheckoutID == id);
            if (checkout == null)
            {
                return NotFound();
            }

            return View(checkout);
        }

        public IActionResult Create()
        {
            var books = _context.Books.ToList();
            ViewBag.BookID = books.Select(book => new SelectListItem
            {
                Value = book.BookID.ToString(),
                Text = book.Title
            }).ToList();

            var members = _context.members.ToList();
            ViewBag.MemberID = members.Select(member => new SelectListItem
            {
                Value = member.MemberID.ToString(),
                Text = member.FullName
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
                Text = book.Title
            }).ToList();

            var members = _context.members.ToList();
            ViewBag.MemberID = members.Select(member => new SelectListItem
            {
                Value = member.MemberID.ToString(),
                Text = member.FullName
            }).ToList();

            if (ModelState.IsValid)
            {
               
                checkout.Penalty = CalculatePenalty(checkout.CheckoutDate, checkout.DueDate);

                _context.checkouts.Add(checkout);

                #region Reduce No of Available Copies
                var book = _context.Books.FirstOrDefault(b => b.BookID == checkout.BookID);
                if (book != null)
                {
                    if (checkout.CheckoutDate > checkout.ReturnedDate)
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

                return RedirectToAction("Done");
            }

            return View(checkout);
        }
        private decimal? CalculatePenalty(DateTime checkoutDate, DateTime dueDate)
        {
            if (DateTime.Now <= dueDate)
            {
                return null;
            }

            var daysLate = (DateTime.Now - dueDate).Days;
            return daysLate * 1.00m; 
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.checkouts.FindAsync(id);
            if (checkout == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title", checkout.BookID);
            ViewData["MemberID"] = new SelectList(_context.members, "MemberID", "FullName", checkout.MemberID);
            return View(checkout);
        }



        [HttpPost]
        public async Task<IActionResult> Return(int checkoutId)
        {
            
            var checkout = await _context.checkouts
                                         .Include(c => c.Book)
                                         .FirstOrDefaultAsync(c => c.CheckoutID == checkoutId);

            if (checkout == null)
            {
                return NotFound(); 
            }

            if (checkout.ReturnedDate != null)
            {
                TempData["ErrorMessage"] = "This book has already been returned.";
                return RedirectToAction(nameof(Index));
            }

            
            checkout.ReturnedDate = DateTime.Now;

            checkout.Penalty = CalculatePenalty(checkout.CheckoutDate, checkout.DueDate); 


            if (checkout.Book != null)
            {
                checkout.Book.AvailableCopies++;
            }
            else
            {
                TempData["ErrorMessage"] = "Book record not found.";
                return RedirectToAction(nameof(Index));
            }

            
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Book returned successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Done()
        {
            return View();
        }
    }
}
