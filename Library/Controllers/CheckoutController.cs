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
            var CheckoutList = _context.checkouts.Include("Member").Include("Book").ToList();
            return View(CheckoutList);
        }

        // GET: Checkouts/Details/5
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

                return RedirectToAction("Index");
            }

            return View(checkout);
        }

        // GET: Checkouts/Edit/5
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

        // POST: Checkouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CheckoutID,BookID,MemberID,CheckoutDate,DueDate,ReturnedDate,Penalty")] Checkout checkout)
        {
            if (id != checkout.CheckoutID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (checkout.CheckoutDate > checkout.ReturnedDate)
                {
                    TempData["DateValidationMessage"] = "Return Date should be greater than Checkout Date";
                    return View();
                }
                
                try
                {
                    _context.Update(checkout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckoutExists(checkout.CheckoutID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title", checkout.BookID);
            ViewData["MemberID"] = new SelectList(_context.members, "MemberID", "FullName", checkout.MemberID);
            return View(checkout);
        }

        private bool CheckoutExists(int id)
        {
            return _context.checkouts.Any(e => e.CheckoutID == id);
        }
    }
}
