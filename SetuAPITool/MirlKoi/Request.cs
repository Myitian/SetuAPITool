using SetuAPITool.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SetuAPITool.MirlKoi
{
    public class Request
    {
        public Sort Sort { get; set; }
        public bool RequestJson { get; set; }
        public int Num { get; set; }

        public Request() { }
        public Request(params KeyValuePair<string, string>[] patameters)
        {
            foreach (KeyValuePair<string, string> patameter in patameters)
            {
                switch (patameter.Key)
                {
                    case "sort":
                        Sort = EnumConverter.ToEnum<Sort>(patameter.Value);
                        break;
                    case "type":
                        RequestJson = patameter.Value == "json";
                        break;
                    case "num":
                        if (int.TryParse(patameter.Value, out int iResult))
                        {
                            Num = iResult;
                        }
                        break;
                }
            }
        }

        public KeyValuePair<string, string>[] ToKeyValuePairs()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>(3)
            {
                new KeyValuePair<string, string>("sort", EnumConverter.ToString(Sort))
            };
            if (RequestJson)
            {
                result.Add(new KeyValuePair<string, string>("type", "json"));
            }
            if (Num > 1)
            {
                result.Add(new KeyValuePair<string, string>("num", Num.ToString()));
            }
            return result.ToArray();
        }
    }
}
