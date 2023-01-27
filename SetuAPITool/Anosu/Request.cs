using Newtonsoft.Json;
using SetuAPITool.Jitsu;
using SetuAPITool.Util;
using SetuAPITool.Util.LoliconAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SetuAPITool.Anosu
{
    public class Request
    {
        public int Num { get; set; }
        public R18Type R18 { get; set; }
        public PixivSize Size { get; set; }
        public string Keyword { get; set; }
        public string Proxy { get; set; }
        public int Db { get; set; }
        public bool PostRequest { get; set; }

        public Request(int num = 1, R18Type r18 = R18Type.NonR18, string keyword = null)
        {
            Num = num;
            R18 = r18;
            Keyword = keyword;
        }
        public Request(Request request)
        {
            Num = request.Num;
            R18 = request.R18;
            Size = request.Size;
            Proxy = request.Proxy;
            Keyword = request.Keyword;
            Db = request.Db;
        }
        public Request(params KeyValuePair<string, string>[] parameters)
        {
            int iResult;
            foreach (KeyValuePair<string, string> patameter in parameters)
            {
                switch (patameter.Key)
                {
                    case "r18":
                        R18 = EnumConverter.ToEnum<R18Type>(patameter.Value);
                        break;
                    case "size":
                        Size = EnumConverter.ToEnum<PixivSize>(patameter.Value);
                        break;
                    case "num":
                        if (int.TryParse(patameter.Value, out iResult))
                        {
                            Num = iResult;
                        }
                        break;
                    case "proxy":
                        Proxy = patameter.Value;
                        break;
                    case "keyword":
                        Keyword = patameter.Value;
                        break;
                    case "db":
                        if (int.TryParse(patameter.Value, out iResult))
                        {
                            Db = iResult;
                        }
                        break;
                    case "postrequest":
                        PostRequest = BoolConverter.ToBool(patameter.Value);
                        break;
                }
            }
        }
        public KeyValuePair<string, string>[] ToKeyValuePairs()
        {
            return ToRequestDictionary().ToArray();
        }
        public Dictionary<string, string> ToRequestDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (Num > 1)
            {
                result["num"] = Num.ToString();
            }
            if (R18 != R18Type.NonR18)
            {
                result["r18"] = ((int)R18).ToString();
            }
            if (Size != PixivSize.Default)
            {
                result["size"] = EnumConverter.ToString(Size);
            }
            if (!string.IsNullOrWhiteSpace(Proxy))
            {
                result["proxy"] = Proxy;
            }
            if (Db > 0)
            {
                result["db"] = Db.ToString();
            }
            return result;
        }
    }
}
