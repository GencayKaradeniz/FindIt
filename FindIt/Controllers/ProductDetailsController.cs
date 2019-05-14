using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using FindIt.Models;
using System.Data;
using System.Configuration;
using System.Web;

namespace FindIt.Controllers
{
    public class ProductDetailsController : Controller
    {
        FinditDb db = new FinditDb();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FinditDb"].ConnectionString);
        SqlDataReader reader;
        SqlCommand command;
        // GET: ProductDetails
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int? id)
        {
            SqlParameter pOutProductPrice = new SqlParameter("@ProductPrice", SqlDbType.Decimal, 18)
            {
                Precision = 18,
                Scale = 2,
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
            SqlParameter pProductID = new SqlParameter("@ProductID", id);
            var output = db.Database.ExecuteSqlCommand("sp_ProductDetails " +
                 "@ProductPrice out," +
                 "@ProductName out, " +
                 "@ProductStock out, " +
                 "@ProductSpecial out, " +
                 "@ProductImageURL out, " +
                 "@ProductID",
                 pOutProductPrice,
                 pOutProductName,
                 pOutProductStock,
                 pOutProductSpecial,
                 pOutProductImageURL,
                 pProductID);
            ProductDetails p = new ProductDetails();
            p.ProductID = Convert.ToInt16(id);
            p.ProductImageURL = pOutProductImageURL.Value.ToString();
            p.ProductName = pOutProductName.Value.ToString();
            p.ProductPrice = Convert.ToDecimal(pOutProductPrice.Value);
            p.ProductSpecial = pOutProductSpecial.Value.ToString();
            p.ProductStock = Convert.ToInt16(pOutProductStock.Value);
            return View(p);
        }

        [HttpPost]
        public ActionResult ShowProductShelfInformation(string productID)
        {
            SqlParameter pProductID = new SqlParameter("@productID", Convert.ToInt16(productID));

            SqlParameter pOutProductName = new SqlParameter("@productName", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter pOutCategoryName = new SqlParameter("@categoryName", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter pOutSubCategoryName = new SqlParameter("@subCategoryName", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter pOutSubCategoryID = new SqlParameter("@subCategoryID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            db.Database.ExecuteSqlCommand("sp_ProductShelfInformation " +
                "@productID," +
                "@productName out," +
                "@categoryName out," +
                "@subCategoryName out," +
                "@subCategoryID out",
                pProductID,
                pOutProductName,
                pOutCategoryName,
                pOutSubCategoryName,
                pOutSubCategoryID);

            var model = new ProductShelfModal();

            connection.Open();
            command = new SqlCommand("Select Bulunan_Kat_No From tbl_RafKatlar Where AltKategori_id = " + Convert.ToInt16(pOutSubCategoryID.Value) + "", connection);
            command.ExecuteNonQuery();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                model.shelfRow = model.shelfRow + reader["Bulunan_Kat_No"].ToString() + ",";
            }
            reader.Close();
            connection.Close();
            connection.Dispose();

            model.shelfRow = model.shelfRow.Substring(0, model.shelfRow.Length - 1);
            model.productName = pOutProductName.Value.ToString();
            model.categoryName = pOutCategoryName.Value.ToString();
            model.subCategoryName = pOutSubCategoryName.Value.ToString();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult MapShown(string id)
        {
            var mapData = db.Database.SqlQuery<ProductDetails>("Select Harita_En as 'mapWidth', Harita_Boy as 'mapHeight', Harita_Raf_Uzunluk as 'shelfLength' From tbl_Harita").ToList();
            ProductDetails p = GetProductCoordinates(id);
            mapData[0].xStartCoordinate = p.xStartCoordinate;
            mapData[0].xEndCoordinate = p.xEndCoordinate;
            mapData[0].yCoordinate = p.yCoordinate;
            mapData[0].subCategoryName = p.subCategoryName;
            return Json(mapData);
        }

        public ProductDetails GetProductCoordinates(string id)
        {
            ProductDetails p = new ProductDetails();

            SqlParameter pProductID = new SqlParameter("@productID", Convert.ToInt16(id));

            SqlParameter pOutProductStartX = new SqlParameter("@productXCoordinateStart", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductEndX = new SqlParameter("@productXCoordinateEnd", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter pOutProductY = new SqlParameter("@productYCoordinate", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter pOutProductSubCategoryName = new SqlParameter("@subCategoryName", SqlDbType.NVarChar, int.MaxValue)
            {
                Direction = ParameterDirection.Output
            };
            db.Database.ExecuteSqlCommand("sp_getProductLocation " +
                "@productID," +
                "@productXCoordinateStart out," +
                "@productXCoordinateEnd out," +
                "@productYCoordinate out," +
                "@subCategoryName out",
                pProductID,
                pOutProductStartX,
                pOutProductEndX,
                pOutProductY,
                pOutProductSubCategoryName);

            p.xStartCoordinate = Convert.ToInt16(pOutProductStartX.Value);
            p.xEndCoordinate = Convert.ToInt16(pOutProductEndX.Value);
            p.yCoordinate = Convert.ToInt16(pOutProductY.Value);
            p.subCategoryName = pOutProductSubCategoryName.Value.ToString();
            return p;
        }

        [HttpPost]
        public void CreateRoute(string id)
        {
            ProductDetails p = GetProductCoordinates(id);

            HttpCookie cookie1 = Request.Cookies["mapRouteCoordinates1"];
            HttpCookie cookie2 = Request.Cookies["mapRouteCoordinates2"];
            HttpCookie cookie3 = Request.Cookies["mapRouteCoordinates3"];

            if (cookie1 == null)
            {
                HttpCookie cookies = new HttpCookie("mapRouteCoordinates1");
                cookies["name"] = p.subCategoryName;
                cookies["startXCoordinate"] = p.xStartCoordinate.ToString();
                cookies["endXCoordinate"] = p.xEndCoordinate.ToString();
                cookies["YCoordinate"] = p.yCoordinate.ToString();
                cookies.Expires = DateTime.Now.AddDays(10);
                Response.Cookies.Add(cookies);
            }
            else if(cookie2 == null)
            {
                HttpCookie cookies = new HttpCookie("mapRouteCoordinates2");
                cookies["name"] = p.subCategoryName;
                cookies["startXCoordinate"] = p.xStartCoordinate.ToString();
                cookies["endXCoordinate"] = p.xEndCoordinate.ToString();
                cookies["YCoordinate"] = p.yCoordinate.ToString();
                cookies.Expires = DateTime.Now.AddDays(10);
                Response.Cookies.Add(cookies);
            }
            else if(cookie3 == null)
            {
                HttpCookie cookies = new HttpCookie("mapRouteCoordinates3");
                cookies["name"] = p.subCategoryName;
                cookies["startXCoordinate"] = p.xStartCoordinate.ToString();
                cookies["endXCoordinate"] = p.xEndCoordinate.ToString();
                cookies["YCoordinate"] = p.yCoordinate.ToString();
                cookies.Expires = DateTime.Now.AddDays(10);
                Response.Cookies.Add(cookies);
            }
        }
    }
}