using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
                int j = 0;
                for (int i = (int)value & 0b11111; i != 0; i >>= 1)
                {
                    if ((i & 1) == 1)
                    {
                        writer.WriteValue(_sizeStr[j]);
                    }
                    j++;
                }
                writer.WriteEndArray();
            }
        }

        public override PixivSize ReadJson(JsonReader reader, Type objectType, PixivSize existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                return (PixivSize)(int)reader.Value;
            }
            throw new Exception("Wrong Token Type");
        }

    }
}
