using Newtonsoft.Json;
using SetuAPITool.Util;
using SetuAPITool.Util.LoliconAPI;
using System.Collections.Generic;

namespace SetuAPITool.LoliconAPI.V1
{
    public class Request
    {
        [JsonProperty("r18")]
        public R18Type R18 { get; set; }

        [JsonProperty("keyword")]
        public string Keyword { get; set; }

        [JsonProperty("num")]
        public int Num { get; set; }

        [JsonProperty("proxy")]
        public string Proxy { get; set; }

        [JsonProperty("size1200")]
        public bool Size1200 { get; set; }

        public Request() { }
        public Request(params KeyValuePair<string, string>[] parameters)
        {
            bool r18 = true;
            bool num = true;
            bool keyword = true;
            bool size1200 = true;
            bool proxy = true;

            int iResult;
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
                    case "keyword" when keyword:
                        Keyword = patameter.Value;
                        keyword = false;
                        break;
                    case "size1200" when size1200:
                        Size1200 = BoolConverter.ToBool(patameter.Value);
                        size1200 = false;
                        break;
                    case "proxy" when proxy:
                        Proxy = patameter.Value;
                        proxy = false;
                        break;
                }
            }
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
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                result.Add(new KeyValuePair<string, string>("keyword", Keyword));
            }
            //
            if (Size1200)
            {
                result.Add(new KeyValuePair<string, string>("size1200", BoolConverter.ToString(Size1200)));
            }
            //
            if (!string.IsNullOrWhiteSpace(Proxy))
            {
                result.Add(new KeyValuePair<string, string>("proxy", Proxy));
            }
            //
            return result.ToArray();
        }
    }
}
