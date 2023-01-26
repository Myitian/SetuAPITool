using Newtonsoft.Json;
using SetuAPITool.Util;
using SetuAPITool.Util.LoliconAPI;
using System;
using System.Collections.Generic;

namespace SetuAPITool.LoliconAPI.V2
{
    public class Request
    {
        [JsonProperty("r18", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public R18Type R18 { get; set; }

        [JsonProperty("num", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Num { get; set; } = 1;

        [JsonProperty("uid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int[] Uid { get; set; }

        [JsonProperty("keyword", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Keyword { get; set; }

        [JsonProperty("tag", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[][] Tag { get; set; }

        [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PixivSize Size { get; set; }

        [JsonProperty("proxy", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Proxy { get; set; }

        [JsonProperty("dateAfter", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(UnixTimeMillisecondConverter))]
        public DateTime DateAfter { get; set; }

        [JsonProperty("dateBefore", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(UnixTimeMillisecondConverter))]
        public DateTime DateBefore { get; set; }

        [JsonProperty("dsc", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Dsc { get; set; }

        [JsonProperty("excludeAI", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool ExcludeAI { get; set; }

        public Request() { }
        public Request(params KeyValuePair<string, string>[] parameters)
        {
            bool r18 = true;
            bool num = true;
            HashSet<int> uid = new HashSet<int>();
            bool keyword = true;
            HashSet<string> tag = new HashSet<string>();
            bool proxy = true;
            bool dateAfter = true;
            bool dateBefore = true;
            bool dsc = true;
            bool excludeAI = true;

            int iResult;
            long lResult;
            foreach (KeyValuePair<string, string> patameter in parameters)
            {
                switch (patameter.Key)
                {
                    case "r18" when r18:
                        if (int.TryParse(patameter.Value, out iResult))
                        {
                            R18 = (R18Type)iResult;
                        }
                        r18 = false;
                        break;
                    case "num" when num:
                        if (int.TryParse(patameter.Value, out iResult))
                        {
                            Num = iResult;
                        }
                        num = false;
                        break;
                    case "uid":
                        if (int.TryParse(patameter.Value, out iResult))
                        {
                            uid.Add(iResult);
                        }
                        break;
                    case "keyword" when keyword:
                        Keyword = patameter.Value;
                        keyword = false;
                        break;
                    case "tag":
                        if (patameter.Value != null)
                        {
                            tag.Add(patameter.Value);
                        }
                        break;
                    case "size":
                        Size |= EnumConverter.ToEnum<PixivSize>(patameter.Value);
                        break;
                    case "proxy" when proxy:
                        Proxy = patameter.Value;
                        proxy = false;
                        break;
                    case "dateAfter" when dateAfter:
                        if (long.TryParse(patameter.Value, out lResult))
                        {
                            DateAfter = TimeConvert.FromUnixTimeMillisecond(lResult);
                        }
                        dateAfter = false;
                        break;
                    case "dateBefore" when dateBefore:
                        if (long.TryParse(patameter.Value, out lResult))
                        {
                            DateBefore = TimeConvert.FromUnixTimeMillisecond(lResult);
                        }
                        dateBefore = false;
                        break;
                    case "dsc" when dsc:
                        Dsc = BoolConverter.ToBool(patameter.Value);
                        dsc = false;
                        break;
                    case "excludeAI" when excludeAI:
                        ExcludeAI = BoolConverter.ToBool(patameter.Value);
                        excludeAI = false;
                        break;
                }
            }

            Uid = new int[uid.Count];
            uid.CopyTo(Uid);

            string[] tag2d = new string[tag.Count];
            tag.CopyTo(tag2d);
            Tag = TagConverter.TwoDimensions2ThreeDimensions(tag2d);
        }

        public KeyValuePair<string, string>[] ToKeyValuePairs()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            //
            if (R18 != R18Type.NonR18)
            {
                result.Add(new KeyValuePair<string, string>("r18", EnumConverter.ToString(R18)));
            }
            //
            if (Num > 1)
            {
                result.Add(new KeyValuePair<string, string>("num", Num.ToString()));
            }
            //
            if (Uid != null && Uid.Length > 0)
            {
                foreach (int uid in Uid)
                {
                    result.Add(new KeyValuePair<string, string>("uid", uid.ToString()));
                }
            }
            //
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                result.Add(new KeyValuePair<string, string>("keyword", Keyword));
            }
            //
            if (Tag != null && Tag.Length > 0)
            {
                foreach (string tag in TagConverter.ThreeDimensions2TwoDimensions(Tag))
                {
                    if (!string.IsNullOrWhiteSpace(tag))
                    {
                        result.Add(new KeyValuePair<string, string>("tag", tag));
                    }
                }
            }
            //
            if (Size != PixivSize.Default || Size != PixivSize.Original)
            {
                string[] sizes = Size.ToStrings();
                foreach (string size in sizes)
                {
                    result.Add(new KeyValuePair<string, string>("size", size));
                }
            }
            //
            if (!string.IsNullOrWhiteSpace(Proxy))
            {
                result.Add(new KeyValuePair<string, string>("proxy", Proxy));
            }
            //
            long l = TimeConvert.ToUnixTimeMillisecond(DateAfter);
            if (l > 0)
            {
                result.Add(new KeyValuePair<string, string>("dateAfter", l.ToString()));
            }
            //
            l = TimeConvert.ToUnixTimeMillisecond(DateBefore);
            if (l > 0)
            {
                result.Add(new KeyValuePair<string, string>("dateBefore", l.ToString()));
            }
            //
            if (Dsc)
            {
                result.Add(new KeyValuePair<string, string>("dsc", BoolConverter.ToString(Dsc)));
            }
            //
            if (ExcludeAI)
            {
                result.Add(new KeyValuePair<string, string>("excludeAI", BoolConverter.ToString(ExcludeAI)));
            }
            //
            return result.ToArray();
        }
    }
}