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

            CoilModel modelDetail = new CoilModel();
            modelDetail.ID = model.CoilDetails[0].Save;
            string input = modelDetail.ID;

            if (input == null)
            {
                return View(model);
            }

            List<string> coilIDs = new List<string>();

            //int splitAt = 9; // change 9 with the size of strings you want.
            //int coilIDCount = 0;
            //for (int i = 0; i < input.Length; i = i + splitAt)
            //{
            //    coilIDCount++;
            //    if (input.Length - i >= splitAt)
            //    {
            //        model.CoilDetails.Add(new CoilModel());
            //        coilIDs.Add(input.Substring(i, splitAt));
            //    }
            //    else
            //        coilIDs.Add(input.Substring(i, ((input.Length - i))));
            //}

            using (SqlConnection newCon = new SqlConnection(ConnStr))
            {
                if (modelDetail.Flag == "UPLOAD")
                {
                    SqlCommand newCmd2 = new SqlCommand(("IF NOT EXISTS (SELECT * FROM X_COIL_TEST WHERE COILID = '" + modelDetail.ID + "') BEGIN INSERT INTO X_COIL_TEST SELECT * FROM X_COIL_MASTER WHERE COILID = ('" + modelDetail.ID + "') END"), newCon);
                    newCon.Open();
                    SqlDataReader rdr2 = newCmd2.ExecuteReader();
                    ViewBag.Upload = "Data uploaded!";
                    modelDetail.ID = "";
                    newCon.Close();
                }
                else
                {
                    SqlCommand newCmd = new SqlCommand(("select * from X_COIL_MASTER where COILID = '" + modelDetail.ID + "'"), newCon);
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
                    }
                    else
                    {
                        ViewBag.Error = "No information found in the database.";
                    }
                    newCon.Close();
                }
            }
            model.CoilDetails.Add(modelDetail);

            return View(model);



            //using (SqlConnection newCon = new SqlConnection(ConnStr))
            //{
            //    for (int i = 0; i < coilIDCount; i++)
            //    {
            //        if (model.CoilDetails[0].Flag == "UPLOAD")
            //        {
            //            SqlCommand newCmd2 = new SqlCommand(("IF NOT EXISTS (SELECT * FROM X_COIL_TEST WHERE COILID = '" + coilIDs[i] + "') BEGIN INSERT INTO X_COIL_TEST SELECT * FROM X_COIL_MASTER WHERE COILID = ('" + coilIDs[i] + "') END"), newCon);
            //            newCon.Open();
            //            SqlDataReader rdr2 = newCmd2.ExecuteReader();
            //            ViewBag.Upload = "Data uploaded!";
            //            model.CoilDetails[0].ID = "";
            //            newCon.Close();
            //        }
            //        else
            //        {
            //            SqlCommand newCmd = new SqlCommand(("select * from X_COIL_MASTER where COILID = '" + coilIDs[i] + "'"), newCon);
            //            newCon.Open();
            //            SqlDataReader rdr = newCmd.ExecuteReader();
            //            if (rdr.HasRows) // If the sql command doesn't return any record, display a message
            //            {
            //                rdr.Read();
            //                if (!rdr.IsDBNull(0))
            //                    model.CoilDetails[i].ID = rdr.GetString(0);
            //                if (!rdr.IsDBNull(1))
            //                    model.CoilDetails[i].Type = rdr.GetString(1);
            //                if (!rdr.IsDBNull(2))
            //                    model.CoilDetails[i].Color = rdr.GetString(2);
            //                if (!rdr.IsDBNull(3))
            //                    model.CoilDetails[i].Weight = rdr.GetDouble(3);
            //                if (!rdr.IsDBNull(4))
            //                    model.CoilDetails[i].Gauge = rdr.GetDouble(4);
            //                if (!rdr.IsDBNull(5))
            //                    model.CoilDetails[i].Width = rdr.GetDouble(5);
            //                if (!rdr.IsDBNull(6))
            //                    model.CoilDetails[i].Order = rdr.GetInt32(6);
            //                if (!rdr.IsDBNull(7))
            //                    model.CoilDetails[i].P_order = rdr.GetString(7);
            //                if (!rdr.IsDBNull(8))
            //                    model.CoilDetails[i].Month_recd = rdr.GetString(8);
            //                if (!rdr.IsDBNull(9))
            //                    model.CoilDetails[i].Date_inwh = rdr.GetDateTime(9);
            //                if (!rdr.IsDBNull(10))
            //                    model.CoilDetails[i].Date_transfer = rdr.GetDateTime(10);
            //                if (!rdr.IsDBNull(11))
            //                    model.CoilDetails[i].Last_stocktake_date = rdr.GetDateTime(11);
            //                if (!rdr.IsDBNull(12))
            //                    model.CoilDetails[i].Status = rdr.GetString(12);
            //                if (!rdr.IsDBNull(13))
            //                    model.CoilDetails[i].Clength = rdr.GetInt32(13);
            //            }
            //            else
            //            {
            //                ViewBag.Error = "No information found in the database.";
            //            }
            //            newCon.Close();
            //        }
            //    }
            //}
            //return View(model);
        }
    }
}
