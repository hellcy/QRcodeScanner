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
        CoilIDModel model = new CoilIDModel();

        public ActionResult Index()
        {

                CoilInfo2(model);

            
            return View(model);
        }

        public ActionResult SameIndex(string results)
        {
            CoilIDModel model = new CoilIDModel();
            model.returnValue = results;
            return View(model);
        }


        [HttpPost]
        public ActionResult CoilInfo(CoilIDModel model)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["GramScanner"].ToString();
            string coilID = model.ID;
            string type;

            using (SqlConnection newCon = new SqlConnection(ConnStr))
            {
                SqlCommand newCmd = new SqlCommand(("select * from X_COIL_MASTER where COILID = '" + coilID + "'"), newCon);

                newCon.Open();
                SqlDataReader rdr = newCmd.ExecuteReader();
                rdr.Read();
                coilID = rdr.GetString(0);
                type = rdr.GetString(1);
                ViewBag.CoilID = coilID;
                ViewBag.Type = type;


                newCon.Close();

            }
            StringBuilder results = new StringBuilder();

            results.Append("<b>CoilID :</b> " + model.ID + "<br/>");
            results.Append("<b>Type :</b> " + ViewBag.Type + "<br/>");

            return Content(results.ToString());
            //return SameIndex(results.ToString());

        }

        public void CoilInfo2(CoilIDModel model)
        {
            if (model.ID != null)
            {


                string ConnStr = ConfigurationManager.ConnectionStrings["GramScanner"].ToString();
                string coilID = model.ID;
                string type;

                using (SqlConnection newCon = new SqlConnection(ConnStr))
                {
                    SqlCommand newCmd = new SqlCommand(("select * from X_COIL_MASTER where COILID = '" + coilID + "'"), newCon);

                    newCon.Open();
                    SqlDataReader rdr = newCmd.ExecuteReader();
                    rdr.Read();

                    coilID = rdr.GetString(0);
                    type = rdr.GetString(1);
                    ViewBag.CoilID = coilID;
                    ViewBag.Type = type;


                    newCon.Close();

                }
                StringBuilder results = new StringBuilder();

                results.Append("<b>CoilID :</b> " + model.ID + "<br/>");
                results.Append("<b>Type :</b> " + ViewBag.Type + "<br/>");
            }
        }
    }
}