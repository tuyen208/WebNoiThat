using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNoiThat.Models;

namespace WebNoiThat.ModelViews
{
    public class ProductHomeVM
    {
        public Category category { get; set; }
        public List<Product> lsProducts { get; set; }
    }
}