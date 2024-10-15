using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class ReturnController : Controller
    {
        // GET: ReturnController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReturnController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReturnController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReturnController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReturnController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReturnController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReturnController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReturnController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
