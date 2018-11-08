using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Scanner.Models
{
    public class CoilIDModel
    {
        [Display(Name = "ID")]
        public string ID { get; set; }
    }
}