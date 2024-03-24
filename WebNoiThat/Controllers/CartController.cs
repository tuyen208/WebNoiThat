using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;
using WebNoiThat.Models.ViewModel;

namespace WebNoiThat.Controllers
{
    public class CartController : Controller
    {
        QLNoiThat data = new QLNoiThat();
        public List<ShoppingCart> Laygiohang()
        {
            List<ShoppingCart> lstGiohang = Session["Giohang"] as List<ShoppingCart>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<ShoppingCart>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<ShoppingCart> lstGiohang = Laygiohang();
            var sp = data.Products.Where(x => x.ProductID == id).First();
            ShoppingCart sanpham = lstGiohang.Find(n => n.ID == id);
            if (sanpham == null)
            {
                sanpham = new ShoppingCart(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.Quanity++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<ShoppingCart> lstGiohang = Session["Giohang"] as List<ShoppingCart>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(x => x.Quanity);
            }
            return tsl;
        }
        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<ShoppingCart> lstGiohang = Session["Giohang"] as List<ShoppingCart>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }
        private double TongTien()
        {
            double tt = 0;
            List<ShoppingCart> lstGiohang = Session["Giohang"] as List<ShoppingCart>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(x => x.dThanhTien);
            }
            return tt;
        }
        public ActionResult GioHang()
        {
            List<ShoppingCart> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }
        public ActionResult XoaGioHang(int id)
        {
            List<ShoppingCart> lstGiohang = Laygiohang();
            ShoppingCart sanpham = lstGiohang.SingleOrDefault(n => n.ID == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.ID == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<ShoppingCart> lstGiohang = Laygiohang();
            ShoppingCart sanpham = lstGiohang.SingleOrDefault(n => n.ID == id);
            if (sanpham != null)
            {
                sanpham.Quanity = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<ShoppingCart> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Accounts");
            }

            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Product");
            }
            List<ShoppingCart> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            Order dh = new Order();
            Customer kh = (Customer)Session["TaiKhoan"];
            Product s = new Product();
            List<ShoppingCart> gh = Laygiohang();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            dh.CustomerID = kh.CustomerID;
            dh.Address = kh.Address;
            dh.District = kh.District;
            dh.Ward = kh.Ward;
            dh.OrderDate = DateTime.Now;
            dh.ShipDate = DateTime.Parse(ngaygiao);
            dh.Paid = false;
            dh.Deleted = false;
            int tongtien = 0;
            foreach (var item in gh)
            {
                int dthanhtien = item.Quanity * (int)item.Price;
                tongtien += dthanhtien;
            }
            dh.TotalMoney = tongtien;
            data.Orders.Add(dh);
            data.SaveChanges();
            foreach (var item in gh)
            {
                OrderDetail ctdh = new OrderDetail();
                ctdh.ProductID = item.ID;
                ctdh.OrderID = dh.OrderID;
                ctdh.Amount = item.Quanity;
                ctdh.CreateDate = DateTime.Now;
                ctdh.Price = (int)item.Price;
                ctdh.TotalMoney = item.Quanity * (int)item.Price;
                s = data.Products.Single(n => n.ProductID == item.ID);
                s.UnitsInStock -= ctdh.Amount;
                data.OrderDetails.Add(ctdh);
            }      
            data.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("XacnhanDonhang", "Cart");
        }
        public ActionResult XacnhanDonhang()
        {
            return View();
        }
    }
}