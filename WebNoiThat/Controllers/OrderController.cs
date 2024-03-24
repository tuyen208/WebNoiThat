using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;
using WebNoiThat.ModelViews;

namespace WebNoiThat.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        private readonly QLNoiThat _context;
        public OrderController(QLNoiThat context)
        {
            _context = context;
        }
        public OrderController()
        {
            _context = new QLNoiThat();
        }
        public ActionResult Index()
        {
            //Kiểm tra đang đăng nhập
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Login", "Accounts");
            }
            Customer kh = (Customer)Session["use"];
            int maND = kh.CustomerID;
            var donhangs = _context.Orders.Include("Customer").Where(d => d.CustomerID == maND);
            return View(donhangs.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order donhang = _context.Orders.Find(id);
            var chitiet = _context.OrderDetails.Include("Product").Where(d => d.OrderID == id).ToList();
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
        }
    }
}