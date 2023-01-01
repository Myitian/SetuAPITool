using Newtonsoft.Json;
using SetuAPITool.LoliconAPI.V2;
using System;
using System.Collections.Generic;
using System.Text;

namespace SetuAPITool.Util.LoliconAPI
{

    public class SizeConverter : JsonConverter<Size>
    {
        private readonly static string[] _sizeStr = { "original", "regular", "small", "thumb", "mini" };
        public override void WriteJson(JsonWriter writer, Size value, JsonSerializer serializer)
        {
            if (value == Size.Default)
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

        public override Size ReadJson(JsonReader reader, Type objectType, Size existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                return (Size)(int)reader.Value;
            }
            throw new Exception("Wrong Token Type");
        }

    }
}
