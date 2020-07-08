using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartThingsNet.Model
{
    public class Left
    {
        [JsonProperty("string")]
        [DataMember(Name = "string", EmitDefaultValue = false)]
        public string _string { get; set; }

        [DataMember(Name = "location", EmitDefaultValue = false)]
        public Location? location { get; set; }

        [DataMember(Name = "device", EmitDefaultValue = false)]
        public Device device { get; set; }

        [JsonProperty("integer")]
        [DataMember(Name = "integer", EmitDefaultValue = false)]
        public int? _integer { get; set; }
    }
}
