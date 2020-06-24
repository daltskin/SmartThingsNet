using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartThingsNet.Model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class CapabilityStatus :Dictionary<String, AttributeState>
    {
    }
}
