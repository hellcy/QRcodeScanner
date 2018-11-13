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
        CoilDetail details = new CoilDetail();

        // GET: /Scanner/ 

        public ActionResult Index()
        {
            return View(details);
        }

        [HttpPost]
        public ActionResult Index(CoilDetail model)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["GramScanner"].ToString();

            string input;

            if (model.CoilDetails[0].Flag == "UPLOAD")
                input = model.CoilDetails[0].Save;
            else
                input = model.CoilDetails[0].Input;

            if (input == null)
            {
                return View(model);
            }

            List<string> coilIDs = new List<string>();

            int splitAt = 33; // change 9 with the size of strings you want.
            int coilIDCount = 0;
            for (int i = 0; i < input.Length; i = i + splitAt)
            {
                coilIDCount++;
                if (input.Length - i >= splitAt)
                {
                    //model.CoilDetails.Add(new CoilModel());
                    coilIDs.Add(input.Substring(i, splitAt - 24));
                }
                else
                    coilIDs.Add(input.Substring(i, ((input.Length - i))));
            }

            for (int i = 0; i < coilIDCount; i++)
            {
                using (SqlConnection newCon = new SqlConnection(ConnStr))
                {
                    if (model.CoilDetails[0].Flag == "UPLOAD")
                    {
                        SqlCommand newCmd2 = new SqlCommand(("IF NOT EXISTS (SELECT * FROM X_COIL_TEST WHERE COILID = '" + coilIDs[i] + "') BEGIN INSERT INTO X_COIL_TEST SELECT * FROM X_COIL_MASTER WHERE COILID = ('" + coilIDs[i] + "') END"), newCon);
                        newCon.Open();
                        SqlDataReader rdr2 = newCmd2.ExecuteReader();

                        ViewBag.Upload = "Data uploaded!";
                        model.CoilDetails[0].Save = "";
                        model.CoilDetails[0].Flag = "";
                        newCon.Close();
                    }
                    else
                    {
                        CoilModel modelDetail = new CoilModel();
                        modelDetail.ID = coilIDs[i];
                        modelDetail.Save = model.CoilDetails[0].Save;
                        SqlCommand newCmd = new SqlCommand(("select * from X_COIL_MASTER where COILID = '" + coilIDs[i] + "'"), newCon);
                        newCon.Open();
                        SqlDataReader rdr = newCmd.ExecuteReader();
                        if (rdr.HasRows) // If the sql command doesn't return any record, display a message
                        {
                            rdr.Read();
                            if (!rdr.IsDBNull(0))
                                modelDetail.ID = rdr.GetString(0);
                            if (!rdr.IsDBNull(1))
                                modelDetail.Type = rdr.GetString(1);
                            if (!rdr.IsDBNull(2))
                                modelDetail.Color = rdr.GetString(2);
                            if (!rdr.IsDBNull(3))
                                modelDetail.Weight = rdr.GetDouble(3);
                            if (!rdr.IsDBNull(4))
                                modelDetail.Gauge = rdr.GetDouble(4);
                            if (!rdr.IsDBNull(5))
                                modelDetail.Width = rdr.GetDouble(5);
                            if (!rdr.IsDBNull(6))
                                modelDetail.Order = rdr.GetInt32(6);
                            if (!rdr.IsDBNull(7))
                                modelDetail.P_order = rdr.GetString(7);
                            if (!rdr.IsDBNull(8))
                                modelDetail.Month_recd = rdr.GetString(8);
                            if (!rdr.IsDBNull(9))
                                modelDetail.Date_inwh = rdr.GetDateTime(9);
                            if (!rdr.IsDBNull(10))
                                modelDetail.Date_transfer = rdr.GetDateTime(10);
                            if (!rdr.IsDBNull(11))
                                modelDetail.Last_stocktake_date = rdr.GetDateTime(11);
                            if (!rdr.IsDBNull(12))
                                modelDetail.Status = rdr.GetString(12);
                            if (!rdr.IsDBNull(13))
                                modelDetail.Clength = rdr.GetInt32(13);
                            model.CoilDetails.Add(modelDetail);
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
