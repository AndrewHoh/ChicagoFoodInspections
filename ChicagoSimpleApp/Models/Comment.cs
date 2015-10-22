using System;
using System.Web;
using System.Linq;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class Comment
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "Comment")]
        public string msg { get; set; }

        [JsonProperty(PropertyName = "Tags")]
        public string[] tags { get; set; }

        [JsonProperty(PropertyName = "User")]
        public string user { get; set; }

        [JsonProperty(PropertyName = "Date")]
        public string date { get; set; }
    }
}
