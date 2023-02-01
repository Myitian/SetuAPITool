using Newtonsoft.Json;
using System;

namespace Myitian.SetuAPITool.Util.LoliconAPI
{
    /// <summary>PixivSize 转换器</summary>
    public class PixivSizeConverter : JsonConverter<PixivSize>
    {
        /// <summary>写入对象的JSON形式</summary>
        /// <param name="writer">要写入的 <see cref="JsonWriter"/></param>
        /// <param name="value">值</param>
        /// <param name="serializer">调用的序列化器</param>
        public override void WriteJson(JsonWriter writer, PixivSize value, JsonSerializer serializer)
        {
            if (value == PixivSize.Default)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteStartArray();
                string[] sizes = value.ToStrings();
                foreach (string size in sizes)
                {
                    writer.WriteValue(size);
                }
                writer.WriteEndArray();
            }
        }
        /// <summary>读取对象的JSON形式</summary>
        /// <param name="reader">要读取的 <see cref="JsonReader"/></param>
        /// <param name="objectType">对象的类型</param>
        /// <param name="existingValue">被读取对象的现有值</param>
        /// <param name="hasExistingValue">现有值有一个值</param>
        /// <param name="serializer">调用的序列化器</param>
        /// <returns>对象的值</returns>
        public override PixivSize ReadJson(JsonReader reader, Type objectType, PixivSize existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return EnumConverter.ToEnum<PixivSize>(reader.Value.ToString());
        }
    }
}
