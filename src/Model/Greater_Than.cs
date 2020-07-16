using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartThingsNet.Model
{
    public class Greater_Than
    {
        [DataMember(Name = "right", EmitDefaultValue = false)]
        public Right right { get; set; }

        [DataMember(Name = "left", EmitDefaultValue = false)]
        public Left left { get; set; }
    }
}
