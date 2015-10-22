using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class Feed
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Hashtag")]
        public string Hashtag { get; set; }

        [JsonProperty(PropertyName = "_ts")]
        public int Timestamp { get; set; }
    }
}