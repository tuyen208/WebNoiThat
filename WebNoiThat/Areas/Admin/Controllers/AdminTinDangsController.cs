using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;
using WebShop.Helpper;

namespace WebNoiThat.Areas.Admin.Controllers
{
    public class AdminTinDangsController : Controller
    {
        private readonly QLNoiThat _context;
        public AdminTinDangsController(QLNoiThat context)
        {
            _context = context;

        }
        public AdminTinDangsController()
        {
            _context = new QLNoiThat();
        }

        // GET: Admin/AdminTinDangs
        public ActionResult Index(int? page)
        {
            var collection = _context.TinDangs.AsNoTracking().ToList();
            foreach (var item in collection)
            {
                if (item.CreatedDate == null)
                {
                    item.CreatedDate = DateTime.Now;
                    _context.Entry(collection).State = EntityState.Added;
                    _context.SaveChanges();
                }
            }
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var lsTinDangs = _context.TinDangs
                .AsQueryable()
                .OrderByDescending(x => x.CatID);
            PagedList.IPagedList<TinDang> models = new PagedList<TinDang>(lsTinDangs, pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminTinDangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinDang tinDang = _context.TinDangs.Find(id);
            if (tinDang == null)
            {
                return HttpNotFound();
            }
            return View(tinDang);
        }

        // GET: Admin/AdminTinDangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTinDangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PostID,Title,SContents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountID,Tags,CatID,isHot,isNewfeed,MetaKey,MetaDesc,Views")] TinDang tinDang, HttpPostedFileBase fThumb)
        {
            if (ModelState.IsValid)
            {

                tinDang.Title = Utilities.ToTitleCase(tinDang.Title);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(tinDang.Title) + extension;
                    tinDang.Thumb = await Utilities.UploadFile(fThumb, @"category", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(tinDang.Thumb)) tinDang.Thumb = "default.jpg";
                tinDang.Alias = Utilities.SEOUrl(tinDang.Title);
                _context.Entry(tinDang).State = EntityState.Added;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            return View(tinDang);
        }

        // GET: Admin/AdminTinDangs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinDang tinDang = await _context.TinDangs.FindAsync(id);
            if (tinDang == null)
            {
                return HttpNotFound();
            }
            return View(tinDang);
        }

        // POST: Admin/AdminTinDangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TinDang tinDang, HttpPostedFileBase fThumb, string imageName)
        {
            if (id != tinDang.PostID)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tinDang.Title = Utilities.ToTitleCase(tinDang.Title);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(tinDang.Title) + extension;
                        tinDang.Thumb = await Utilities.UploadFile(fThumb, @"news", image.ToLower());
                    }
                    else if (!string.IsNullOrEmpty(imageName))
                    {
                        tinDang.Thumb = imageName;
                    }
                    if (string.IsNullOrEmpty(tinDang.Thumb)) tinDang.Thumb = "default.jpg";
                    tinDang.Alias = Utilities.SEOUrl(tinDang.Title);
                    _context.Entry(tinDang).State = EntityState.Modified;
                    TempData["Success"] = "Cập nhật thành công";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinDangExists(tinDang.PostID))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tinDang);
        }

        // GET: Admin/AdminTinDangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinDang tinDang = _context.TinDangs.Find(id);
            if (tinDang == null)
            {
                return HttpNotFound();
            }
            return View(tinDang);
        }

        // POST: Admin/AdminTinDangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinDang tinDang = _context.TinDangs.Find(id);
            _context.TinDangs.Remove(tinDang);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        private bool TinDangExists(int id)
        {
            return _context.TinDangs.Any(e => e.PostID == id);
        }
    }
}
