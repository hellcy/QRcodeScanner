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
            List<CoilModel> newDetails = new List<CoilModel>()
            {
                new CoilModel { ID = "67A122A10" },
                new CoilModel { ID = "67A580A30" }
            };

            CoilDetail newCoilDetails = new CoilDetail();
            newCoilDetails.CoilDetails = newDetails;

            return View(newCoilDetails);
        }

        [HttpPost]
        public ActionResult Index(CoilDetail model)
        {

            string ConnStr = ConfigurationManager.ConnectionStrings["GramScanner"].ToString();

            string input = model.CoilDetails[0].ID;

            if (input == null)
            {
                return View(model);
            }

            List<string> coilIDs = new List<string>();

            int splitAt = 9; // change 9 with the size of strings you want.
            int coilIDCount = 0;
            for (int i = 0; i < input.Length; i = i + splitAt)
            {
                coilIDCount++;
                if (input.Length - i >= splitAt)
                {
                    model.CoilDetails.Add(new CoilModel());
                    coilIDs.Add(input.Substring(i, splitAt));
                }
                else
                    coilIDs.Add(input.Substring(i, ((input.Length - i))));
            }

            using (SqlConnection newCon = new SqlConnection(ConnStr))
            {
                for (int i = 0; i < coilIDCount; i++)
                {
                    if (model.CoilDetails[0].Flag == "INSERT")
                    {
                        SqlCommand newCmd2 = new SqlCommand(("INSERT INTO X_COIL_TEST (COILID) values ('" + coilIDs[i] + "')"), newCon);
                        //SqlCommand newCmd2 = new SqlCommand(("INSERT INTO X_COIL_TEST (COILID) select (COILID) from X_COIL_MASTER where COILID = '" + coilIDs[i] + "'"), newCon);
                        newCon.Open();
                        SqlDataReader rdr2 = newCmd2.ExecuteReader();
                        newCon.Close();
                        return View(model);
                    }
                    else
                    {
                        SqlCommand newCmd = new SqlCommand(("select * from X_COIL_MASTER where COILID = '" + coilIDs[i] + "'"), newCon);
                        newCon.Open();
                        SqlDataReader rdr = newCmd.ExecuteReader();
                        if (rdr.HasRows) // If the sql command doesn't return any record, display a message
                        {
                            rdr.Read();
                            if (!rdr.IsDBNull(0))
                                model.CoilDetails[i].ID = rdr.GetString(0);
                            if (!rdr.IsDBNull(1))
                                model.CoilDetails[i].Type = rdr.GetString(1);
                            if (!rdr.IsDBNull(2))
                                model.CoilDetails[i].Color = rdr.GetString(2);
                            if (!rdr.IsDBNull(3))
                                model.CoilDetails[i].Weight = rdr.GetDouble(3);
                            if (!rdr.IsDBNull(4))
                                model.CoilDetails[i].Gauge = rdr.GetDouble(4);
                            if (!rdr.IsDBNull(5))
                                model.CoilDetails[i].Width = rdr.GetDouble(5);
                            if (!rdr.IsDBNull(6))
                                model.CoilDetails[i].Order = rdr.GetDouble(6);
                            if (!rdr.IsDBNull(7))
                                model.CoilDetails[i].P_order = rdr.GetString(7);
                            if (!rdr.IsDBNull(8))
                                model.CoilDetails[i].Month_recd = rdr.GetDouble(8);
                            if (!rdr.IsDBNull(9))
                                model.CoilDetails[i].Date_inwh = rdr.GetDateTime(9);
                            if (!rdr.IsDBNull(10))
                                model.CoilDetails[i].Date_transfer = rdr.GetDateTime(10);
                            if (!rdr.IsDBNull(11))
                                model.CoilDetails[i].Last_stocktake_date = rdr.GetDateTime(11);
                            if (!rdr.IsDBNull(12))
                                model.CoilDetails[i].Status = rdr.GetString(12);
                            if (!rdr.IsDBNull(13))
                                model.CoilDetails[i].Clength = rdr.GetDouble(13);
                        }
                        else
                        {
                            ViewBag.Error = "No information found in the database.";
                        }
                        newCon.Close();
                    }
                }
            }
            return View(model);
        }
    }
}
