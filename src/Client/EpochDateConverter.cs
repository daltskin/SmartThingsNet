using Newtonsoft.Json;
using System;

namespace SmartThingsNet.Client
{
    /// <summary>
    /// Converter for epoc format dates
    /// </summary>
    public class EpochDateConverter : Newtonsoft.Json.JsonConverter
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime retVal = _epoch;
            if (reader.Value != null)
            {
                if (!DateTime.TryParse(reader.Value.ToString(), out retVal))
                {
                    var t = (long)reader.Value;
                    retVal = _epoch.AddMilliseconds(t); // new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(t);
                }
            }
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if ((DateTime)value != _epoch)
            {
                writer.WriteRawValue(Convert.ToDateTime(value).ToUniversalTime().ToString());
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}
