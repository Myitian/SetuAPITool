using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SetuAPITool.Util
{
    public class EnumConverter<T> : JsonConverter<T> where T : Enum
    {
        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            writer.WriteValue(EnumConverter.ToString(value));
        }

        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return EnumConverter.ToEnum<T>(reader.Value.ToString());
        }
    }

    public static class EnumConverter
    {
        public static string ToString<T>(T value) where T : Enum
        {
            string s = value.ToString().ToLower();
            return s[0] == '_' ? s.Substring(1) : s;
        }
        public static T ToEnum<T>(string value) where T : Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                return (T)Enum.Parse(typeof(T), "_" + value, true);
            }
        }
    }
}
