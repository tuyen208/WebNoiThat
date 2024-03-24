using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;

namespace WebNoiThat.Areas.Admin.Controllers
{
    public class ThongkesController : Controller
    {
        // GET: Admin/Thongkes
        QLNoiThat db = new QLNoiThat();
        public ActionResult Index()
        {
            var oders = db.Orders.ToList();
            var dataThongke = (from s in db.Orders
                               join x in db.Customers on s.CustomerID equals x.CustomerID
                               group s by s.CustomerID into g
                               select new Thongke
                               {
                                   Tennguoidung = g.FirstOrDefault().Customer.FullName,
                                   Tongtien = g.Sum(x => x.TotalMoney),
                                   Dienthoai = g.FirstOrDefault().Customer.Phone,
                                   Soluong = g.Count()

                               });
            var dataFinal = dataThongke.OrderByDescending(s => s.Tongtien).Take(5).ToList();
            return View(dataFinal);
        }
    }
}