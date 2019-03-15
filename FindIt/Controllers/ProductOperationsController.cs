using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    public class ProductOperationsController : Controller
    {
        // GET: ProductOperations
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductOperations()
        {
            return View();
        }
    }
}