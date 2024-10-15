using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class SearchHistoryController : Controller
    {
        // GET: SearchHistoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SearchHistoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchHistoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchHistoryController/Create
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

        // GET: SearchHistoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchHistoryController/Edit/5
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

        // GET: SearchHistoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchHistoryController/Delete/5
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
