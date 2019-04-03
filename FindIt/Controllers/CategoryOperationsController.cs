using FindIt.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    public class CategoryOperationsController : Controller
    {
        FinditDb db = new FinditDb();
        // GET: CategoryOperations
        [HttpGet]
        public ActionResult CategoryOperations()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryOperations(string categoryName)
        {
            db.Database.ExecuteSqlCommand("INSERT INTO tbl_Kategori SET (Kategori_Ad) VALUES "+categoryName+"");
            return View();
        }
    }
}