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

        [HttpGet]
        public ActionResult ProductOperations()
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
        public ActionResult ProductAdd(string productName, string productBarcode,
                                                          string categoryID, string SubCategoryID,
                                                          string productCost, string productStock,
                                                          string productFeatures, string singlePicture)
        {
            var message = "OK";
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
            SqlParameter pSubCategoryList = new SqlParameter("@subcategoryId", Convert.ToInt16(SubCategoryID));
            SqlParameter pProductStock = new SqlParameter("@productStock", productStock);
            SqlParameter pPersonelId = new SqlParameter("@personalId", personelId);
            SqlParameter pProductFeatures = new SqlParameter("@productEspecial", productFeatures);
            SqlParameter pProductImage = new SqlParameter("@productImage", singlePicture);
            db.Database.ExecuteSqlCommand("sp_ProductAdd @productName,@productCost,@productBarcode,@subcategoryId,@productStock,@personalId,@productEspecial,@productImage",
                                                                              pProductName, pProductCost, pProductBarcode, pSubCategoryList,
                                                                              pProductStock, pPersonelId, pProductFeatures, pProductImage);
            return Json(message);
        }

        [HttpPost]
        public ActionResult ProductSearch(string barcode)
        {
            SqlParameter pProductBarcode = new SqlParameter("@barcode", barcode);
            SqlParameter pOutProductName = new SqlParameter("@productName", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductSubCategory = new SqlParameter("@productSubCategory", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductCategory = new SqlParameter("@productCategory", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductPrice = new SqlParameter("@productCost", SqlDbType.Decimal, 18)
            {
                Precision = 18,
                Scale = 2,
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductStock = new SqlParameter("@productStock", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductFeatures = new SqlParameter("@productFeatures", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductImages = new SqlParameter("@productPicture", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            var product = new ProductSearchWithBarcode();
            db.Database.ExecuteSqlCommand("sp_SearchWithBarcode @barcode," +
               "@productName out," +
               "@productSubCategory out," +
               "@productCategory out," +
               "@productCost out," +
               "@productStock out," +
               "@productFeatures out," +
               "@productPicture out",
               pProductBarcode,
               pOutProductName,
               pOutProductSubCategory,
               pOutProductCategory,
               pOutProductPrice,
               pOutProductStock,
               pOutProductFeatures,
               pOutProductImages);

            product.productName = pOutProductName.Value.ToString();
            product.subCategoryName = pOutProductSubCategory.Value.ToString();
            product.stock = Convert.ToInt16(pOutProductStock.Value);
            product.price = Convert.ToDecimal(pOutProductPrice.Value);
            product.productImage = pOutProductImages.Value.ToString();
            product.categoryName = pOutProductCategory.Value.ToString();
            product.features = pOutProductFeatures.Value.ToString();
            product.barcode = barcode;
            return Json(product);
        }

        [HttpPost]
        public ActionResult ProductDelete(string barcode)
        {
            SqlParameter pProductBarcode = new SqlParameter("@barcode", barcode);

            var message = "OK";
            db.Database.ExecuteSqlCommand("sp_ProductDelete @barcode",pProductBarcode);
            return Json(message);
        }

        [HttpPost]
        public ActionResult ProductUpdate(string productName, string productBarcode,
                                                          string categoryID, string SubCategoryID,
                                                          string productCost, string productStock,
                                                          string productFeatures, string singlePicture,
                                                          string barcodeSearch)
        {
            var message = "OK";
            HttpCookie cookie = Request.Cookies["UserInformation"];
            int personelId = 1;
            if (cookie != null)
            {
                personelId = Convert.ToInt16(cookie["PersonelId"]);
                personelId = 1;
            }
            SqlParameter pProductBarcodeSearch = new SqlParameter("@barcodeSearch", barcodeSearch);
            SqlParameter pProductName = new SqlParameter("@productName", productName);
            SqlParameter pProductCost = new SqlParameter("@productCost", Convert.ToDecimal(productCost));
            SqlParameter pProductBarcode = new SqlParameter("@productBarcode", productBarcode);
            SqlParameter pSubCategoryList = new SqlParameter("@subcategoryId", Convert.ToInt16(SubCategoryID));
            SqlParameter pProductStock = new SqlParameter("@productStock", productStock);
            SqlParameter pPersonelId = new SqlParameter("@personalId", personelId);
            SqlParameter pProductFeatures = new SqlParameter("@productFeatures", productFeatures);
            SqlParameter pProductImage = new SqlParameter("@productImage", singlePicture);
            db.Database.ExecuteSqlCommand("sp_ProductUpdate @productName,@productBarcode,@productCost,@productStock,@productFeatures,@subcategoryId,@personalId,@productImage,@barcodeSearch",
                                                                              pProductName, pProductBarcode,pProductCost, pProductStock,
                                                                              pProductFeatures, pSubCategoryList, pPersonelId, pProductImage, pProductBarcodeSearch);
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