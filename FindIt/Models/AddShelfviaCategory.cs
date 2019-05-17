using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models
{
    public class AddShelfviaCategory
    {
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string ShelfName { get; set; }
        public int ShelfID { get; set; }
    }
}