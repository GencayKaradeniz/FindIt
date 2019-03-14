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
        public ActionResult FilterPage(string productSearch)
        {
            SqlParameter pSearch = new SqlParameter("@search", "gübre");
            SqlParameter pStock = new SqlParameter("@stok", "yes");
            SqlParameter pSubCategory = new SqlParameter("@altkategori", 15);
            FinditDb db = new FinditDb();
            var products = db.Database.SqlQuery<SearchWithFilter>("sp_SearchProductList @search, @stok, @altkategori", pSearch, pStock,pSubCategory).ToList();
            return View(products);
        }
    }
}