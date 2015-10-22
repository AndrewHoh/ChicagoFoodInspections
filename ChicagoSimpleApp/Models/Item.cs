using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Models
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "DBA_Name")]
        public string DBA_Name { get; set; }

        [JsonProperty(PropertyName = "AKA_Name")]
        public string AKA_Name { get; set; }

        [JsonProperty(PropertyName = "License")]
        public int License { get; set; }

        [JsonProperty(PropertyName = "Facility_Type")]
        public string FacilityType { get; set; }

        [JsonProperty(PropertyName = "Risk")]
        public Risk Risk { get; set; }

        [JsonProperty(PropertyName = "Address")]
        public Address Address { get; set; }

        [JsonProperty(PropertyName = "Inspection")]
        public Inspection Inspection { get; set; }

        [JsonProperty(PropertyName = "Results")]
        public string Results { get; set; }

        [JsonProperty(PropertyName = "Violations")]
        public Violation[] Violations { get; set; }

        [JsonProperty(PropertyName = "Location")]
        public Location Location { get; set; }
    }
}
