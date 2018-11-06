using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Scanner.Models;
using System.Text;

namespace Scanner.Controllers
{
    public class ScannerController : Controller
    {
        // GET: /Scanner/ 

        public ActionResult Index()
        {
            CoilIDModel model = new CoilIDModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CoilIDModel model)
        {

            string a;
            string ConnStr = ConfigurationManager.ConnectionStrings["GramScanner"].ToString();
            string coilID = model.ID;
            string type = null;
            string color = null;
            Double weight = 0;
            Double gauge = 0;
            Double width = 0;
            Double order = 0;
            string p_order = null;
            Double month_recd = 0;
            DateTime? date_inwh = null;
            DateTime? date_transfer = null;
            DateTime? last_stocktake_date = null;
            string status = null;
            Double clength = 0;


            using (SqlConnection newCon = new SqlConnection(ConnStr))
            {
                SqlCommand newCmd = new SqlCommand(("select * from X_COIL_MASTER where COILID = '" + coilID + "'"), newCon);
                newCon.Open();
                SqlDataReader rdr = newCmd.ExecuteReader();
                rdr.Read();

                coilID = rdr.GetString(0);
                type = rdr.GetString(1);
                color = rdr.GetString(2);
                if (!rdr.IsDBNull(3))
                {
                    weight = rdr.GetDouble(3);
                }
                if (!rdr.IsDBNull(4))
                {
                    gauge = rdr.GetDouble(4);
                }
                if (!rdr.IsDBNull(5))
                {
                    width = rdr.GetDouble(5);
                }
                if (!rdr.IsDBNull(6))
                {
                    order = rdr.GetDouble(6);
                }
                if (!rdr.IsDBNull(7))
                {
                    p_order = rdr.GetString(7);
                }
                if (!rdr.IsDBNull(8))
                {
                    month_recd = rdr.GetDouble(8);
                }
                if (!rdr.IsDBNull(9))
                {
                    date_inwh = rdr.GetDateTime(9);
                }
                if (!rdr.IsDBNull(10))
                {
                    date_transfer = rdr.GetDateTime(10);
                }
                if (!rdr.IsDBNull(11))
                {
                    last_stocktake_date = rdr.GetDateTime(11);
                }
                if (!rdr.IsDBNull(12))
                {
                    status = rdr.GetString(12);
                }
                if (!rdr.IsDBNull(13))
                {
                    clength = rdr.GetDouble(13);
                }

                ViewBag.CoilID = coilID;
                ViewBag.Type = type;
                ViewBag.Color = color;
                if (weight >= 0)
                {
                    ViewBag.Weight = weight;
                }
                if (gauge >= 0)
                {
                    ViewBag.Gauge = gauge;
                }
                if (width >= 0)
                {
                    ViewBag.Width = width;
                }
                if (order >= 0)
                {
                    ViewBag.Order = order;
                }
                if (p_order != null)
                {
                    ViewBag.P_order = p_order;
                }
                if (month_recd >= 0)
                {
                    ViewBag.Month_recd = month_recd;
                }
                if (date_inwh != null)
                {
                    ViewBag.Date_inwh = date_inwh;
                }
                if (date_transfer != null)
                {
                    ViewBag.Date_transfer = date_transfer;
                }
                if (last_stocktake_date != null)
                {
                    ViewBag.Last_stocktake_date = last_stocktake_date;
                }
                if (status != null)
                {
                    ViewBag.Status = status;
                }
                if (clength >= 0)
                {
                    ViewBag.Clength = clength;
                }

                newCon.Close();
            }
            return View(model);
        }
    }
}