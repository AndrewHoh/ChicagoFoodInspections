using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class HeatMap
    {
        [JsonProperty(PropertyName = "Points")]
        public Point[] Points { get; set; }

        public class Point
        {
            [JsonProperty(PropertyName = "Latitude")]
            public string Latitude { get; set; }

            [JsonProperty(PropertyName = "Longitude")]
            public string Longitude { get; set; }

            [JsonProperty(PropertyName = "AverageRisk")]
            public string AverageRisk { get; set; }

            [JsonProperty(PropertyName = "Count")]
            public int Count { get; set; }
        }
    }
}