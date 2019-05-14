using FindIt.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            var shelfs = db.Database.SqlQuery<Raflar>("SELECT Raf_ID as 'RafID', Raf_Ad as 'RafAdi',Kategori_Ad as 'KategoriAdi', Kategori_id as 'KategoriID' FROM	tbl_Raflar r, tbl_Kategori k WHERE r.Kategori_id = k.Kategori_ID").ToList();
            return View(shelfs);
        }


        [HttpPost]
        public ActionResult CategoriesForMap(string rafID)
        {
            SqlParameter pRafID = new SqlParameter("@rafID", Convert.ToInt16(rafID));
            var model = db.Database.SqlQuery<Raflar>("sp_GetShelfs @rafID", pRafID).ToList();

            int categoryID = Convert.ToInt16(model[0].KategoriID);

            for (int i = 0; i < model.Count; i++)
            {
                var subCategories = new List<SelectListItem>();
                db.tbl_AltKategori.Where(s => s.Kategori_id == categoryID).ToList().ForEach(s => subCategories.Add(new SelectListItem
                {
                    Text = s.AltKategori_Ad,
                    Value = s.AltKategori_ID.ToString(),
                }));
                model[i].SubCategories = subCategories;
                for (int j = 0; j < model[i].SubCategories.Count; j++)
                {
                    if (model[i].AltKategoriID == Convert.ToInt16(model[i].SubCategories[j].Value))
                    {
                        model[i].SubCategories[j].Selected = true;
                    }
                    else
                    {
                        model[i].SubCategories[j].Selected = false;
                    }
                }
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult MapDetails(string shopHeight, string shopWidth, string shelfLength)
        {
            SqlParameter pShopHeight = new SqlParameter("@shopHeight", shopHeight);
            SqlParameter pShopWidth = new SqlParameter("@shopWidth", shopWidth);
            SqlParameter pShelfLength = new SqlParameter("@shelfLength", shelfLength);
            db.Database.ExecuteSqlCommand("sp_MapAdd @shopHeight, @shopWidth, @shelfLength", pShopHeight, pShopWidth, pShelfLength);
            var shelfs = db.Database.SqlQuery<Raflar>("SELECT Raf_ID as 'RafID', Raf_Ad as 'RafAdi',Kategori_Ad as 'KategoriAdi', Kategori_id as 'KategoriID' FROM	tbl_Raflar r, tbl_Kategori k WHERE r.Kategori_id = k.Kategori_ID").ToList();
            return View(shelfs);
        }

        [HttpPost]
        public ActionResult ShelfsUpdate(int? rafID)
        {
            return PartialView(db.tbl_RafKatlar.FirstOrDefault(x => x.Raf_id == rafID));
        }

        public ActionResult MapCreate(string[] shelfs)
        {
            for (int i = 0; i < shelfs.Length; i++)
            {
                string[] row = shelfs[i].Split(',');
                int rafBasX = Convert.ToInt16(row[1]),
                     rafBitX = Convert.ToInt16(row[2]),
                     rafY = Convert.ToInt16(row[3]);

                SqlParameter pShelfStartX = new SqlParameter("@shelfStartX", rafBasX);
                SqlParameter pShelfEndX = new SqlParameter("@shelfEndX", rafBitX);
                SqlParameter pShelfY = new SqlParameter("@shelfY", rafY);
                SqlParameter pShelfName = new SqlParameter("@shelfName", row[0]);

                db.Database.ExecuteSqlCommand("sp_MapCreater @shelfName,@shelfStartX,@shelfEndX,@shelfY", pShelfName, pShelfStartX, pShelfEndX, pShelfY);
            }
            return Json(new { message = "OK" });
        }
    }
}