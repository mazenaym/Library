using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class MemberController : Controller
    {
        private readonly ILogger<MemberController> _logger;
        private readonly ApplicationDbContext _context;
        public MemberController(ILogger<MemberController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var memberList=_context.members.ToList();
            return View(memberList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                _context.members.Add(member);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);  // In case of validation errors, return the form with entered data
        }
    }
}
