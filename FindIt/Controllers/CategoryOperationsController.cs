using FindIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    public class CategoryOperationsController : Controller
    {
        FinditDb db = new FinditDb();
        // GET: CategoryOperations
        public ActionResult CategoryOperations()
        {
            var categories = new List<SelectListItem>();

            var subCategories = new List<SelectListItem>();

            db.tbl_Kategori.OrderBy(s => s.Kategori_Ad).ToList().ForEach(s => categories.Add(new SelectListItem
            {
                Text = s.Kategori_Ad,
                Value = s.Kategori_ID.ToString()
            }));

            int categoryID = Convert.ToInt16(categories[0].Value);
            db.tbl_AltKategori.OrderBy(s => s.AltKategori_Ad).Where(s => s.Kategori_id == categoryID).ToList().ForEach(s => subCategories.Add(new SelectListItem
            {
                Text = s.AltKategori_Ad,
                Value = s.AltKategori_ID.ToString()
            }));

            var model = new ProductOperationsLists
            {
                Categories = categories,
                SubCategories = subCategories
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CategoryAdd(string categoryName)
        {
            var message = "OK";
            db.Database.ExecuteSqlCommand("INSERT INTO tbl_Kategori (Kategori_Ad) VALUES ('"+ categoryName + "')");
            return Json(message);
        }

        [HttpPost]
        public ActionResult CategoryDelete(string categoryID)
        {
            var message = "OK";
            db.Database.ExecuteSqlCommand("DELETE FROM tbl_AltKategori Where Kategori_id = " + Convert.ToInt16(categoryID) + "");

            db.Database.ExecuteSqlCommand("DELETE FROM tbl_Kategori Where Kategori_ID = "+Convert.ToInt16(categoryID)+"");
        
            return Json(message);
        }

        [HttpPost]
        public ActionResult SubCategoryAdd(string subCategoryName, string categoryID)
        {
            var message = "OK";
            db.Database.ExecuteSqlCommand("INSERT INTO tbl_AltKategori (AltKategori_Ad,Kategori_id) VALUES ('" + subCategoryName + "',"+Convert.ToInt16(categoryID)+")");
            return Json(message);
        }

        [HttpPost]
        public ActionResult SubCategoryDelete(string subCategoryID)
        {
            var message = "OK";
            db.Database.ExecuteSqlCommand("DELETE FROM tbl_AltKategori Where AltKategori_ID = " + Convert.ToInt16(subCategoryID) + "");
            return Json(message);
        }

        public JsonResult SubCategoriesByCategoryID(string categoryID)
        {
            int kategori = Convert.ToInt16(categoryID);
            var subCategories = db.tbl_AltKategori.Where(s => s.Kategori_id == kategori).OrderBy(s => s.AltKategori_Ad).Select(s => new
            {
                Id = s.AltKategori_ID,
                SubCategryName = s.AltKategori_Ad
            }).ToList();

            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }
    }
}