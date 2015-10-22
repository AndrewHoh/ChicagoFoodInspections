using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class Risk
    {
        [JsonProperty(PropertyName = "Level")]
        public int Level { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
    }
}