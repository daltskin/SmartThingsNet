using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartThingsNet.Model
{
    public class _Equals
    {
        [DataMember(Name = "left", EmitDefaultValue = false)]
        public Left left { get; set; }

        [DataMember(Name = "right", EmitDefaultValue = false)]
        public Right right { get; set; }
    }
}
