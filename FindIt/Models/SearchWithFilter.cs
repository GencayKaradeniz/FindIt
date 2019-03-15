using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models
{
    public class SearchWithFilter
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageURL { get; set; }
        public int ProductID { get; set; }
        public int ProductStock { get; set; }
        public string SubCategoryName { get; set; }
        public int SubCategoryID { get; set; }
    }
}