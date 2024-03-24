using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;
using WebNoiThat.Areas.Admin.Model;
using WebShop.Extension;

namespace WebNoiThat.Areas.Admin.Controllers
{
   
    public class AdminAccountsController : Controller
    {
        private readonly QLNoiThat _context;

        public AdminAccountsController(QLNoiThat context)
        {
            _context = context;
        }
        public AdminAccountsController() 
        {
            _context = new QLNoiThat();
        }
        // GET: Admin/AdminAccounts
        public Task<ActionResult> Index()
        {
            ViewBag.QuyenTruyCap = new SelectList(_context.Roles, "RoleId", "Description");
            List<SelectListItem> lsTrangThai = new List<SelectListItem>();
            lsTrangThai.Add(new SelectListItem() { Text = "Hoạt động", Value = "1" });
            lsTrangThai.Add(new SelectListItem() { Text = "Khóa", Value = "0" });
            ViewBag.lsTrangThai = lsTrangThai;
            var QLNoiThat = _context.Accounts.Include(a => a.Role);
            return Task.FromResult<ActionResult>(View(QLNoiThat.ToList()));
        }
        // GET: Admin/AdminAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = _context.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Admin/AdminAccounts/Create
        public ActionResult Create()
        {
            ViewBag.QuyenTruyCap = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: Admin/AdminAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountId,Phone,Email,Password,Salt,Active,FullName,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Tạo mới tài khoản thành công";
                return RedirectToAction("Index");
            }
            ViewBag.QuyenTruyCap = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleID);
            return View(account);
        }

        // GET: Admin/AdminAccounts/Edit/5
        public ActionResult ChangePassword()
        {
            ViewBag.RoleID = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("PasswordNow") && ModelState.IsValidField("Password"))
            {
                var taikhoan = _context.Accounts.AsNoTracking().SingleOrDefault(x => x.Email == model.Email);
                if (taikhoan == null) return RedirectToAction("Login", "Accounts");
                var pass = (model.PasswordNow.Trim() + taikhoan.Salt.Trim()).ToMD5();
                {
                    string passnew = (model.Password.Trim() + taikhoan.Salt.Trim()).ToMD5();
                    taikhoan.Password = passnew;
                    taikhoan.LastLogin = DateTime.Now;
                    _context.Entry(taikhoan).State = EntityState.Modified;
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công";
                    return RedirectToAction("Login", "Accounts", new { area = "" });
                }
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = _context.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(_context.Roles, "RoleID", "RoleName", account.RoleID);
            return View(account);
        }

        // POST: Admin/AdminAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,Phone,Email,Password,Salt,Active,FullName,RoleID,LastLogin,CreateDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(_context.Roles, "RoleID", "RoleName", account.RoleID);
            return View(account);
        }

        // GET: Admin/AdminAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = _context.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = _context.Accounts.Find(id);
            _context.Accounts.Remove(account);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
