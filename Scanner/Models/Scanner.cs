using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Scanner.Models
{
    public class CoilModel
    {
        public CoilModel()
        {
            Weight = null;
            Width = null;
            Gauge = null;
            Order = null;
            Month_recd = null;
            Date_inwh = null;
            Date_transfer = null;
            Last_stocktake_date = null;
            Clength = null;
        }

        public string ID { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public double? Weight { get; set; }
        public double? Gauge { get; set; }
        public double? Width { get; set; }
        public double? Order { get; set; }
        public string P_order { get; set; }
        public double? Month_recd { get; set; }
        public DateTime? Date_inwh { get; set; }
        public DateTime? Date_transfer { get; set; }
        public DateTime? Last_stocktake_date { get; set; }
        public string Status { get; set; }
        public double? Clength { get; set; }
        public string Flag { get; set; }
    }

    public class CoilDetail
    {
        public List<CoilModel> CoilDetails { get; set; }
    }
}