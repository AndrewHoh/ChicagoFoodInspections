using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class Location
    {
        [JsonProperty(PropertyName = "Latitude")]
        public string Latitude { get; set; }

        [JsonProperty(PropertyName = "Longitude")]
        public string Longitude { get; set; }
    }
}