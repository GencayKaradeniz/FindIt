using FindIt.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    public class FilterController : Controller
    {
        FinditDb Db = new FinditDb();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FinditDb"].ConnectionString);
        SqlDataReader reader;
        SqlCommand command;
        // GET: Filter
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FilterPage(string search, string subCategory, string productStock)
        {
            SqlParameter pSearch = new SqlParameter("@search", search);
            SqlParameter pStock = new SqlParameter("@stok", productStock);
            SqlParameter pSubCategory = new SqlParameter("@altkategori", Convert.ToInt16(subCategory));
            var products = Db.Database.SqlQuery<SearchWithFilter>("sp_SearchProductList @search, @stok, @altkategori", pSearch, pStock, pSubCategory).ToList();

            string[] productID = new string[3];

            HttpCookie cookie1 = Request.Cookies["mapRouteCoordinates1"];
            HttpCookie cookie2 = Request.Cookies["mapRouteCoordinates2"];
            HttpCookie cookie3 = Request.Cookies["mapRouteCoordinates3"];
            if (cookie3 != null)
            {
                productID[0] = cookie1["ID"].ToString();
                productID[1] = cookie2["ID"].ToString();
                productID[2] = cookie3["ID"].ToString();

            }
            else if (cookie2 != null)
            {
                productID[0] = cookie1["ID"].ToString();
                productID[1] = cookie2["ID"].ToString();
            }
            else if (cookie1 != null)
            {
                productID[0] = cookie1["ID"].ToString();
            }
            var model = new List<ProductRouteList>();
            if (cookie1 != null)
            {
                int i = 0;
                while (productID[i] != null)
                {
                    SqlParameter pProductID = new SqlParameter("@productID", Convert.ToInt16(productID[i]));
                    var items = Db.Database.SqlQuery<ProductRouteList>("sp_RouteListProducts @productID", pProductID).ToList();
                    model.Add(new ProductRouteList { productName = items[0].productName, productImage = items[0].productImage, subCategoryName = items[0].subCategoryName });
                    i++;
                    if (i == 3)
                    {
                        break;
                    }
                }
            }
            return View(Tuple.Create(products, model));
        }
    }
}