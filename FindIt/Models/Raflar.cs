using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Models
{
    public class Raflar
    {
        public int RafID { get; set; }
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }
        public string RafAdi { get; set; }
        public string AltKategoriAdi { get; set; }
        public int BulunanKatNo { get; set; }

        [Display(Name = "Alt Kategori Adı")]
        public int AltKategoriID { get; set; }

        public List<SelectListItem> SubCategories { get; set; }

    }
}