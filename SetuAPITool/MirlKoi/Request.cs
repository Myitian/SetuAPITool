using SetuAPITool.Util;
using System.Collections.Generic;
using System.Linq;

namespace SetuAPITool.MirlKoi
{
    public class Request
    {
        public Sort Sort { get; set; }
        public int Num { get; set; }

        public Request(int num = 1, Sort sort = Sort.Random)
        {
            Num = num;
            Sort = sort;
        }
        public Request(params KeyValuePair<string, string>[] parameters)
        {
            foreach (KeyValuePair<string, string> patameter in parameters)
            {
                switch (patameter.Key)
                {
                    case "sort":
                        Sort = EnumConverter.ToEnum<Sort>(patameter.Value);
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
            return ToDictionary().ToArray();
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>
            {
                { "sort", EnumConverter.ToString(Sort) }
            };
            if (Num > 1)
            {
                result["num"] = Num.ToString();
            }
            return result;
        }
    }
}
