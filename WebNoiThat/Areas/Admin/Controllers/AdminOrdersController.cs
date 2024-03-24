using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;

namespace WebNoiThat.Areas.Admin.Controllers
{
    public class AdminOrdersController : Controller
    {
        // GET: Admin/AdminOrders
        private QLNoiThat db = new QLNoiThat();
        public ActionResult Index()
        {
            var donhangs = db.Orders.Include("Customer");
            return View(donhangs.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order donhang = db.Orders.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }
        public ActionResult Xacnhan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order donhang = db.Orders.Find(id);
            donhang.Paid = true;
            db.SaveChanges();
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
    }
}