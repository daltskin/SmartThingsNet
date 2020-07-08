using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartThingsNet.Model
{
    public class Then
    {
        [DataMember(Name = "command", EmitDefaultValue = false)]
        public CommandAction command { get; set; }

        [DataMember(Name = "sleep", EmitDefaultValue = false)]
        public SleepAction sleep { get; set; }
    }
}
