using FindIt.Models;
using System;
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
            return View(products);
        }
    }
}