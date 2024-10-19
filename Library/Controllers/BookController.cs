using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    //[Authorize("AdminRole")]
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


        [HttpGet]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {

                var booklist = _context.Books.ToList();
                return View("Index", booklist);
            }


            var searchedbook = _context.Books
                .Where(q => EF.Functions.Like(q.Author, $"%{query}%") || EF.Functions.Like(q.Title, $"%{query}%") || EF.Functions.Like(q.Genre, $"%{query}%"))
                .ToList();

            return View("Index", searchedbook);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Book newBook)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(newBook);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newBook);  
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }


        [HttpPost]
        public IActionResult Edit(Book editedBook)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(editedBook).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editedBook); 
        }

        [HttpGet]
        [HttpPost]
        
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            if (Request.Method == HttpMethods.Post)
            {
               
                _context.Books.Remove(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

    }
}