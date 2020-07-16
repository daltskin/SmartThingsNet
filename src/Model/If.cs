
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace SmartThingsNet.Model
{
    public class If
    {
        [JsonProperty("equals")]
        [DataMember(Name = "equals", EmitDefaultValue = false)]
        public _Equals _equals { get; set; }

        [JsonProperty("then")]
        [DataMember(Name = "then", EmitDefaultValue = false)]
        public Then[] _then { get; set; }

        [JsonProperty("else")]
        [DataMember(Name = "else", EmitDefaultValue = false)]
        public Else[] _else { get; set; }

        [DataMember(Name = "less_than", EmitDefaultValue = false)]
        public Less_Than less_than { get; set; }

        [DataMember(Name = "greater_than", EmitDefaultValue = false)]
        public Greater_Than greater_than { get; set; }
    }
}
