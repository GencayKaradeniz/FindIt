using FindIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    public class MapController : Controller
    {
        FinditDb db = new FinditDb();
        // GET: Map
        [HttpGet]
        public ActionResult MapDetails()
        {
            var shelfs = new List<SelectListItem>();

            db.tbl_Raflar.OrderBy(s => s.Raf_Ad).ToList().ForEach(s => shelfs.Add(new SelectListItem
            {
                Text = s.Raf_Ad,
                Value = s.Raf_ID.ToString()
            }));

            var model = new ProductOperationsLists
            {
                Shelfs = shelfs
            };
            return View(model);
        }
    }
}