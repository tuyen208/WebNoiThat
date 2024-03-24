using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;

namespace WebNoiThat.Controllers
{
    [RoutePrefix("")]
    public class BlogController : Controller
    {
        private readonly QLNoiThat _context;
        public BlogController(QLNoiThat context)
        {
            _context = context;
        }
        public BlogController()
        {
            _context = new QLNoiThat();
        }
        // GET: Blog/Index
        [Route("blogs.html", Name = ("Blog"))]
        public ActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var lsTinDangs = _context.TinDangs
                .AsQueryable()
                .OrderByDescending(x => x.PostID);
            PagedList.IPagedList<TinDang> models = new PagedList<TinDang>(lsTinDangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }
        [Route("/tin-tuc/{Alias}-{id}.html", Name = "TinChiTiet")]
        public ActionResult Details(int? id)
        {
            var tindang = _context.TinDangs.AsNoTracking().SingleOrDefault(x => x.PostID == id);
            if (tindang == null)
            {
                return RedirectToAction("Index");
            }
            var lsBaivietlienquan = _context.TinDangs
                .AsNoTracking()
                .Where(x => x.Published == true && x.PostID != id)
                .Take(3)
                .OrderByDescending(x => x.CreatedDate).ToList();
            ViewBag.Baivietlienquan = lsBaivietlienquan;
            return View(tindang);
        }
    }
}