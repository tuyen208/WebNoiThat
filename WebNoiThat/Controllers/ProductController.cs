using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebNoiThat.Models;

namespace WebNoiThat.Controllers
{
    [RoutePrefix("")]
    public class ProductController : Controller
    {
        // GET: Product
        private readonly QLNoiThat _context;
        public ProductController(QLNoiThat context)
        {
            _context = context;
        }
        public ProductController()
        {
            _context = new QLNoiThat();
        }
        [Route("shop.html", Name = ("ShopProduct"))]
        public ActionResult Index(int? page, string SearchString)
        {
            IEnumerable<Product> items = _context.Products.OrderByDescending(x => x.ProductID);
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                char[] charArray = SearchString.ToCharArray();
                bool foundSpace = true;
                //sử dụng vòng lặp for lặp từng phần tử trong mảng
                for (int i = 0; i < charArray.Length; i++)
                {
                    //sử dụng phương thức IsLetter() để kiểm tra từng phần tử có phải là một chữ cái
                    if (Char.IsLetter(charArray[i]))
                    {
                        if (foundSpace)
                        {
                            //nếu phải thì sử dụng phương thức ToUpper() để in hoa ký tự đầu
                            charArray[i] = Char.ToUpper(charArray[i]);
                            foundSpace = false;
                        }
                    }
                    else
                    {
                        foundSpace = true;
                    }
                }
                //chuyển đổi kiểu mảng char thàng string
                SearchString = new string(charArray);
                items = items.Where(x => x.Alias.Contains(SearchString) || x.Title.Contains(SearchString));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include("Cat").FirstOrDefault(x => x.ProductID == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsProduct = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == product.CatId && x.ProductID != id && x.Active == true)
                    .Take(4)
                    .OrderByDescending(x => x.DateCreated)
                    .ToList();
                ViewBag.SanPham = lsProduct;
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Search(string SearchString)
        {
            var lsProduct = _context.Products.Where(n => n.ProductName.Contains(SearchString)).ToList();
            return View(lsProduct);
        }
    }
}
