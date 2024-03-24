using Antlr.Runtime.Tree;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace WebNoiThat.Models.ViewModel
{
    public class ShoppingCart
    {

            QLNoiThat data = new QLNoiThat();
            public int ID { get; set; }
            [Display(Name = "Tên Sản Phẩm")]
            public string Name { get; set; }
            [Display(Name = "Ảnh Bìa")]
            public string Images { get; set; }
            [Display(Name = "Giá Bán")]
            public double Price { get; set; }
            [Display(Name = "Số Lượng")]
            public int Quanity { get; set; }
            [Display(Name = "Thành Tiền")]
            public double dThanhTien
            {
                get { return Price * Quanity; }
            }
            public ShoppingCart(int id)
            {
                ID = id;
                Product sp = data.Products.Single(n => n.ProductID == ID);
                Name = sp.ProductName;
                Images = sp.Thumb;
                Price = double.Parse(sp.Price.ToString());
                Quanity = 1;
            }
        }
}