using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ChicagoSimpleApp.Models
{
    public class Result
    {
        [Required]
        [Display(Name = "Documents")]
        public List<string> docs { get; set; }
    }
}