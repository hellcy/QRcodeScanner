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

            string ConnStr = ConfigurationManager.ConnectionStrings["GramScanner"].ToString();
            string code = model.ID;
            string coilID = code.Substring(0, 9).Trim(); //get the first 9 characters as the coilID

            string type = null;
            string color = null;
            Double? weight = null;
            Double? gauge = null;
            Double? width = null;
            Double? order = null;
            string p_order = null;
            Double? month_recd = null;
            DateTime? date_inwh = null;
            DateTime? date_transfer = null;
            DateTime? last_stocktake_date = null;
            string status = null;
            Double? clength = null;


            using (SqlConnection newCon = new SqlConnection(ConnStr))
            {
                SqlCommand newCmd = new SqlCommand(("select * from X_COIL_MASTER where COILID = '" + coilID + "'"), newCon);
                newCon.Open();
                SqlDataReader rdr = newCmd.ExecuteReader();
                rdr.Read();

                string trueCoilID = rdr.GetString(0);
                //if (trueCoilID.Equals(coilID))
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
                ViewBag.Weight = weight;
                ViewBag.Gauge = gauge;
                ViewBag.Width = width;
                ViewBag.Order = order;
                ViewBag.P_order = p_order;
                ViewBag.Month_recd = month_recd;
                ViewBag.Date_inwh = date_inwh;
                ViewBag.Date_transfer = date_transfer;
                ViewBag.Last_stocktake_date = last_stocktake_date;
                ViewBag.Status = status;
                ViewBag.Clength = clength;

                newCon.Close();
            }
            return View(model);
        }
    }
}