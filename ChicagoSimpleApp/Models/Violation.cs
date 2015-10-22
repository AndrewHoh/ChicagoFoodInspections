using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class Violation
    {
        [JsonProperty(PropertyName = "Num")]
        public int Number { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Comments")]
        public string Comments { get; set; }
    }
}