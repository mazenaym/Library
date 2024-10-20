using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var memberList= _context.members.ToList();
            return View(memberList);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var member = _context.members.Find(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }


        [HttpPost]
        public IActionResult Edit(Member editedmember)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(editedmember).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editedmember);
        }

        [HttpGet]
        [HttpPost]

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = _context.members.Find(id);
            if (member == null)
            {
                return NotFound();
            }

            if (Request.Method == HttpMethods.Post)
            {

                _context.members.Remove(member);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }
    }
}
