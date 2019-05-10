using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models
{
    public class ProductDetails
    {
        public decimal ProductPrice { get; set; }
        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public string ProductSpecial { get; set; }
        public string ProductImageURL { get; set; }
        public int ShelfID { get; set; }
        public int ProductID { get; set; }
    }
}