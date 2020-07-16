using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartThingsNet.Model
{
    public class Else
    {
        [DataMember(Name = "command", EmitDefaultValue = false)]
        public CommandAction command { get; set; }
    }
}
