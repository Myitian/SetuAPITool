using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Myitian.SetuAPITool.Util
{
    /// <summary>Unix时间戳（毫秒）转换器</summary>
    public class UnixTimeMillisecondConverter : DateTimeConverterBase
    {
        /// <summary>写入对象的JSON形式</summary>
        /// <param name="writer">要写入的 <see cref="JsonWriter"/></param>
        /// <param name="value">值</param>
        /// <param name="serializer">调用的序列化器</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime time)
            {
                writer.WriteValue(Time.ToUnixTimeMillisecond(time));
            }
            else
            {
                throw new Exception("Expected date object value.");
            }
        }

        /// <summary> 读取对象的JSON形式</summary>
        /// <param name="reader">要读取的 <see cref="JsonReader"/></param>
        /// <param name="objectType">对象的类型</param>
        /// <param name="existingValue">被读取对象的现有值</param>
        /// <param name="serializer">调用的序列化器</param>
        /// <returns>对象的值</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                return Time.FromUnixTimeMillisecond((long)reader.Value);
            }
            throw new Exception("Wrong Token Type");
        }
    }
}
