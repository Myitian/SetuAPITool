﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SetuAPITool.Util
{
    public class UnixTimeMillisecondConverter : DateTimeConverterBase
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime time)
            {
                writer.WriteValue(TimeConvert.ToUnixTimeMillisecond(time));
            }
            else
            {
                throw new Exception("Expected date object value.");
            }
        }

        /// <summary>
        ///   Reads the JSON representation of the object.
        /// </summary>
        /// <param name = "reader">The <see cref = "JsonReader" /> to read from.</param>
        /// <param name = "objectType">Type of the object.</param>
        /// <param name = "existingValue">The existing value of object being read.</param>
        /// <param name = "serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                return TimeConvert.FromUnixTimeMillisecond((long)reader.Value);
            }
            throw new Exception("Wrong Token Type");
        }
    }
}
