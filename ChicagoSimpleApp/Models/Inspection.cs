using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class Inspection
    {
        [JsonProperty(PropertyName = "Date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "Month")]
        public int Month { get; set; }

        [JsonProperty(PropertyName = "Day")]
        public int Day { get; set; }

        [JsonProperty(PropertyName = "Year")]
        public int Year { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }
    }
}