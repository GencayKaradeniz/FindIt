using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models
{
    public class ProductSearchWithBarcode
    {
        public string barcode { get; set; }
        public string productName { get; set; }
        public string categoryName { get; set; }
        public string subCategoryName { get; set; }
        public string features { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string productImage { get; set; }
    }
}