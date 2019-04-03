using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        FinditDb db = new FinditDb();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Test(string categoryName)
        {
            db.Database.ExecuteSqlCommand("INSERT INTO tbl_Kategori SET (Kategori_Ad) VALUES " + categoryName + "");
            return View();
        }
    }
}