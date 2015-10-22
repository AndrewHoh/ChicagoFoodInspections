using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class Letter
    {
        [JsonProperty(PropertyName = "Letter")]
        public string Character { get; set; }

        [JsonProperty(PropertyName = "Facilities")]
        public Facility[] Facilities { get; set; }

        public class Facility
        {
            [JsonProperty(PropertyName = "Facility")]
            public string facility { get; set; }

            [JsonProperty(PropertyName = "Address")]
            public string Address { get; set; }
        }
    }
}