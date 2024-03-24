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
    public class AdminPagesController : Controller
    {
        private readonly QLNoiThat _context;

        public AdminPagesController(QLNoiThat context)
        {
            _context = context;
        }
        public AdminPagesController()
        {
            _context = new QLNoiThat();
        }
        // GET: Admin/AdminPages
        public ActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var lsPages = _context.Pages
                .AsQueryable()
                .OrderByDescending(x => x.PageID);
            PagedList.IPagedList<Page> models = new PagedList<Page>(lsPages, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminPages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = _context.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // GET: Admin/AdminPages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PageID,PageName,Contents,Thumb,Published,Title,MetaDesc,MetaKey,Alias,CreatedDate,Ordering")] Page page, HttpPostedFileBase fThumb)
        {
            if (ModelState.IsValid)
            {

                page.PageName = Utilities.ToTitleCase(page.PageName);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(page.PageName) + extension;
                    page.Thumb = await Utilities.UploadFile(fThumb, @"pages", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(page.Thumb)) page.Thumb = "default.jpg";
                page.Alias = Utilities.SEOUrl(page.PageName);
                _context.Entry(page).State = EntityState.Added;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            return View(page);
        }

        // GET: Admin/AdminPages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Admin/AdminPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Page page, HttpPostedFileBase fThumb, string imageName)
        {
            if (id != page.PageID)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    page.PageName = Utilities.ToTitleCase(page.PageName);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(page.PageName) + extension;
                        page.Thumb = await Utilities.UploadFile(fThumb, @"pages", image.ToLower());
                    }
                    else if (!string.IsNullOrEmpty(imageName))
                    {
                        page.Thumb = imageName;
                    }
                    if (string.IsNullOrEmpty(page.Thumb)) page.Thumb = "default.jpg";
                    page.Alias = Utilities.SEOUrl(page.PageName);
                    _context.Entry(page).State = EntityState.Modified;
                    TempData["Success"] = "Cập nhật thành công";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.PageID))
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
            return View(page);
        }

        // GET: Admin/AdminPages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = _context.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Admin/AdminPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Page page = _context.Pages.Find(id);
            _context.Pages.Remove(page);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.PageID == id);
        }
    }
}
