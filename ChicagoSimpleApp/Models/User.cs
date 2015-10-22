using System;
using System.Web;
using System.Linq;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "UserName")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "Company")]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "JobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty(PropertyName = "Picture")]
        public string Picture { get; set; }

        [JsonProperty(PropertyName = "Favorites")]
        public string[] Favorites { get; set; }

        [JsonProperty(PropertyName = "Comments")]
        public string[] Comments { get; set; }
    }
}
