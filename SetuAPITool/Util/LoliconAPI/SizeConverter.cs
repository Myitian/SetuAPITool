using Newtonsoft.Json;
using System;

namespace SetuAPITool.Util.LoliconAPI
{

    public class SizeConverter : JsonConverter<PixivSize>
    {
        private readonly static string[] _sizeStr = { "original", "regular", "small", "thumb", "mini" };

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

        public override PixivSize ReadJson(JsonReader reader, Type objectType, PixivSize existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                return EnumConverter.ToEnum<PixivSize>(reader.Value.ToString());
            }
            throw new Exception("Wrong Token Type");
        }

    }
}
