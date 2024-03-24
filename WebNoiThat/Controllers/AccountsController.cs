using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebNoiThat.Models;
using System.Collections.Generic;


namespace WebNoiThat.Controllers
{
   
    public class AccountsController : Controller
    {
        // GET: Accounts
         QLNoiThat _context = new QLNoiThat();
       
        public ActionResult DangkyTaiKhoan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangkyTaiKhoan(Customer taikhoan)
        {
            try
            {
                // Kiểm tra email đã tồn tại chưa
                var existingUser = _context.Customers.FirstOrDefault(u => u.Email == taikhoan.Email);
                if (existingUser != null)
                {
                    ViewBag.RegError = "Email đã được sử dụng. Vui lòng sử dụng email khác.";
                    return View("DangkyTaiKhoan");
                }

                // Thêm người dùng mới
                _context.Customers.Add(taikhoan);
                // Lưu lại vào cơ sở dữ liệu
                _context.SaveChanges();
                // Nếu dữ liệu đúng thì trả về trang đăng nhập
                if (ModelState.IsValid)
                {
                    ViewBag.RegOk = "Đăng kí thành công. Đăng nhập ngay";
                    ViewBag.isReg = true;
                    return View("DangkyTaiKhoan");
                }
                else
                {
                    return View("DangkyTaiKhoan");
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();

        }


        [HttpPost]
        public ActionResult Login(FormCollection userlog)
        {
            string UserName = userlog["UserName"].ToString();
            string password = userlog["password"].ToString();
            var islogin = _context.Customers.SingleOrDefault(x => x.Email.Equals(UserName) && x.Password.Equals(password));
            Session["TaiKhoan"] = islogin;
            if (islogin != null)
            {
                if (UserName == "Admin@gmail.com")
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Admin/Home");
                }
                else
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
                return View("Login");
            }

        }
        public ActionResult Logout()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer taikhoan = _context.Customers.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer taikhoan = _context.Customers.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }

            return View(taikhoan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer taikhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(taikhoan).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Detail", new { id = taikhoan.CustomerID });
            }
            return View(taikhoan);
        }       
    }
}