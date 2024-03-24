using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;

namespace WebNoiThat.Controllers
{
    [RoutePrefix("")]
    public class PagesController : Controller
    {
        // GET: Pages
        private readonly QLNoiThat _context;
        public PagesController(QLNoiThat context)
        {
            _context = context;
        }
        public PagesController()
        {
            _context = new QLNoiThat();
        }
        [Route("/page/{Alias}", Name = "PageDetails")]
        public ActionResult Details(string Alias)
        {
            if (string.IsNullOrEmpty(Alias)) return RedirectToAction("Index", "Home");

            var page = _context.Pages.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
            if (page == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(page);
        }
    }
}