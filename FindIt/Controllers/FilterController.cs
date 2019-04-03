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

            SqlParameter bos = new SqlParameter("@search", search);

            var list = Db.Database.SqlQuery<tbl_AltKategori>("sp_GetFilteringSubCategory @search",bos).ToList();
            
            //connection.Open();
            //command = new SqlCommand("sp_GetFilteringSubCategory", connection);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@search", search);
            //command.ExecuteNonQuery();
            //reader = command.ExecuteReader();
            //var SubCategoriesInf = new List<tbl_AltKategori>();

            //while (reader.Read())
            //{
            //    SubCategoriesInf.Add(new tbl_AltKategori {  AltKategori_ID = Convert.ToInt16(reader["SubCategoryID"]),
            //                                                AltKategori_Ad = reader["SubCategoryName"].ToString(),
            //                                                Kategori_id = Convert.ToInt16(reader["CategoryID"]) });
            //}
            //connection.Close();
            //reader.Close();

            return View(Tuple.Create(products, list));
        }

    }
}