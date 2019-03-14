using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindIt.Models;
using System.Data;

namespace FindIt.Controllers
{
    public class ProductDetailsController : Controller
    {
        FinditDb db = new FinditDb();
        // GET: ProductDetails
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int? id)
        {
            FinditDb db = new FinditDb();
            SqlParameter pOutProductPrice = new SqlParameter("@ProductPrice", SqlDbType.Decimal,18)
            {   
                Precision=18,
                Scale=2,
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductName = new SqlParameter("@ProductName", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductStock = new SqlParameter("@ProductStock", SqlDbType.Int, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductSpecial = new SqlParameter("@ProductSpecial", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductImageURL = new SqlParameter("@ProductImageURL", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutShelfID = new SqlParameter("@ShelfID", SqlDbType.Int, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pProductID = new SqlParameter("@ProductID", id);
           var output =  db.Database.ExecuteSqlCommand("sp_ProductDetails " +
                "@ProductPrice out," +
                "@ProductName out, " +
                "@ProductStock out, " +
                "@ProductSpecial out, " +
                "@ProductImageURL out, " +
                "@ShelfID out, " +
                "@ProductID", 
                pOutProductPrice, 
                pOutProductName, 
                pOutProductStock, 
                pOutProductSpecial,
                pOutProductImageURL,
                pOutShelfID,
                pProductID);
            ProductDetails p = new ProductDetails();
            p.ProductID = Convert.ToInt16(id);
            p.ProductImageURL = pOutProductImageURL.Value.ToString();
            p.ProductName = pOutProductName.Value.ToString();
            p.ProductPrice = Convert.ToDecimal(pOutProductPrice.Value);
            p.ProductSpecial = pOutProductSpecial.Value.ToString();
            p.ProductStock = Convert.ToInt16(pOutProductStock.Value);
            p.ShelfID = Convert.ToInt16(pOutShelfID.Value);            
            return View(p);
        }
       
    }
}