using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Models
{
    public class ProductOperationsLists
    {
        [Display(Name = "Kategori Adı")]
        public int CategoryID { get; set; }

        [Display(Name = "Alt Kategori Adı")]
        public int SubCategoryID { get; set; }

        [Display(Name ="Raf Adı")]
        public int ShelfID { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public List<SelectListItem> SubCategories { get; set; }

        public List<SelectListItem> Shelfs { get; set; }
    }
}