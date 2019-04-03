using FindIt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    public class ProductOperationsController : Controller
    {
        FinditDb db = new FinditDb();
        // GET: ProductOperations
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult ProductOperations()
        {
            var categories = new List<SelectListItem>();

            var subCategories = new List<SelectListItem>();

            var shelfs = new List<SelectListItem>();

            db.tbl_Raflar.OrderBy(s => s.Raf_Ad).ToList().ForEach(s => shelfs.Add(new SelectListItem
            {
                Text = s.Raf_Ad,
                Value = s.Raf_ID.ToString()
            }));

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
                SubCategories = subCategories,
                Shelfs = shelfs
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ProductOperations(ProductOperationsLists model,
                                                         string productName, string productBarcode,
                                                         string singlePicture,
                                                         string productCost, string productStock,
                                                         string productFeatures)
        {
            model.Categories = new List<SelectListItem>();
            model.SubCategories = new List<SelectListItem>();
            model.Shelfs = new List<SelectListItem>();

            db.tbl_Kategori.OrderBy(s => s.Kategori_Ad).ToList().ForEach(s => model.Categories.Add(new SelectListItem
            {
                Text = s.Kategori_Ad,
                Value = s.Kategori_ID.ToString()
            }));

            db.tbl_AltKategori.OrderBy(s => s.AltKategori_Ad).Where(s => s.Kategori_id == model.CategoryID).ToList().ForEach(s => model.SubCategories.Add(new SelectListItem
            {
                Text = s.AltKategori_Ad,
                Value = s.AltKategori_ID.ToString()
            }));

            db.tbl_Raflar.OrderBy(s => s.Raf_Ad).Where(s => s.Raf_ID == model.ShelfID).ToList().ForEach(s => model.Shelfs.Add(new SelectListItem
            {
                Text = s.Raf_Ad,
                Value = s.Raf_ID.ToString()
            }));

            HttpCookie cookie = Request.Cookies["UserInformation"];
            int personelId = 1;
            if (cookie != null)
            {
                personelId = Convert.ToInt16(cookie["PersonelId"]);
                personelId = 1;
            }

            SqlParameter pProductName = new SqlParameter("@productName", productName);
            SqlParameter pProductCost = new SqlParameter("@productCost", Convert.ToDecimal(productCost));
            SqlParameter pProductBarcode = new SqlParameter("@productBarcode", productBarcode);
            SqlParameter pSubCategoryList = new SqlParameter("@subcategoryId", Convert.ToInt16(model.SubCategoryID));
            SqlParameter pProductStock = new SqlParameter("@productStock", productStock);
            SqlParameter pPersonelId = new SqlParameter("@personalId", personelId);
            SqlParameter pProductFeatures = new SqlParameter("@productEspecial", productFeatures);
            SqlParameter pProductShelf = new SqlParameter("@productShelf", Convert.ToInt16(model.ShelfID));
            db.Database.ExecuteSqlCommand("sp_ProductAdd @productName,@productCost,@productBarcode,@subcategoryId,@productStock,@personalId,@productEspecial,@productShelf",
                                                                              pProductName, pProductCost, pProductBarcode, pSubCategoryList,
                                                                              pProductStock, pPersonelId, pProductFeatures, pProductShelf);

            return View(model);
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

        //public ActionResult ProductOperations(string productName, string productBarcode,
        //                                                             string subCategories, string singlePicture,
        //                                                             string productCost, string productStock,
        //                                                             string productFeatures, string shelfList)
        //{
        //    HttpCookie cookie = Request.Cookies["UserInformation"];
        //    int personelId = 1;
        //    if (cookie != null)
        //    {
        //        personelId = Convert.ToInt16(cookie["PersonelId"]);
        //        personelId = 1;
        //    }

        //    SqlParameter pProductName = new SqlParameter("@productName", productName);
        //    SqlParameter pProductCost = new SqlParameter("@productCost", Convert.ToDecimal(productCost));
        //    SqlParameter pProductBarcode = new SqlParameter("@productBarcode", productBarcode);
        //    SqlParameter pSubCategoryList = new SqlParameter("@subcategoryId", Convert.ToInt16(subCategories));
        //    SqlParameter pProductStock = new SqlParameter("@productStock", productStock);
        //    SqlParameter pPersonelId = new SqlParameter("@personalId", personelId);
        //    SqlParameter pProductFeatures = new SqlParameter("@productEspecial", productFeatures);
        //    SqlParameter pProductShelf = new SqlParameter("@productShelf", Convert.ToInt16(shelfList));
        //    db.Database.ExecuteSqlCommand("sp_ProductAdd @productName,@productCost,@productBarcode,@subcategoryId,@productStock,@personalId,@productEspecial,@productShelf",
        //                                                          pProductName, pProductCost, pProductBarcode, pSubCategoryList,
        //                                                          pProductStock, pPersonelId, pProductFeatures, pProductShelf);

        //    return View();
        //}
    }
}