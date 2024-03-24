using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;
using WebNoiThat.ModelViews;

namespace WebNoiThat.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        public readonly QLNoiThat _context;
        public ActionResult Index()
        {
            HomeViewVM model = new HomeViewVM();
            using (var context = new QLNoiThat())
            {
                var lsProducts = context.Products.AsNoTracking()
                    .Where(x => x.Active == true && x.HomeFlag == true)
                    .OrderByDescending(x => x.DateCreated)
                    .ToList();

                List<ProductHomeVM> lsProductViews = new List<ProductHomeVM>();
                var lsCats = context.Categories
                    .AsNoTracking()
                    .Where(x => x.Published == true)
                    .OrderByDescending(x => x.Ordering)
                    .ToList();

                foreach (var item in lsCats)
                {
                    ProductHomeVM productHome = new ProductHomeVM();
                    productHome.category = item;
                    productHome.lsProducts = lsProducts.Where(x => x.CatId == item.CatID).ToList();
                    lsProductViews.Add(productHome);                
                    model.Products = lsProductViews;
                   
                    ViewBag.AllProducts = lsProducts;
                }
            }
            return View(model);
        }
        [Route("gioi-thieu.html", Name = "About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Route("lien-he.html", Name = "Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}