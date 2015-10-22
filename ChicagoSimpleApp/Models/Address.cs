using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class Address
    {
        [JsonProperty(PropertyName = "Street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "City")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "State")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "Zip")]
        public string Zip { get; set; }
    }
}