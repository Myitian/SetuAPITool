using SetuAPITool.Util;
using System.Collections.Generic;
using System.Linq;

namespace SetuAPITool.Jitsu
{
    public class Request
    {
        public Sort Sort { get; set; }
        public Size Size { get; set; }
        public int Num { get; set; }
        public string Proxy { get; set; }
        public Source Source { get; set; }
        public bool PostRequest { get; set; }

        public Request(int num = 1, Sort sort = Sort.Default)
        {
            Num = num;
            Sort = sort;
        }
        public Request(Request request)
        {
            Sort = request.Sort;
            Size = new Size(request.Size);
            Num = request.Num;
            Proxy = request.Proxy;
            Source = request.Source;
            PostRequest = request.PostRequest;
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
                    case "size":
                        Size = new Size(patameter.Value);
                        break;
                    case "num":
                        if (int.TryParse(patameter.Value, out int iResult))
                        {
                            Num = iResult;
                        }
                        break;
                    case "proxy":
                        Proxy = patameter.Value;
                        break;
                    case "source":
                        Source = EnumConverter.ToEnum<Source>(patameter.Value);
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
            if (Sort.Default < Sort && Sort < Sort.SpR18)
            {
                result["sort"] = EnumConverter.ToString(Sort);
            }
            if (Size.SizeType != SizeType.Original)
            {
                result["size"] = Size.SizeString;
            }
            if (!string.IsNullOrWhiteSpace(Proxy) && (Size.SizeType == SizeType.Original || Size.SizeType == SizeType.TypeB))
            {
                result["proxy"] = Proxy;
            }
            if (Num > 1)
            {
                result["num"] = Num.ToString();
            }
            if (!string.IsNullOrWhiteSpace(Proxy))
            {
                result["proxy"] = Proxy;
            }
            return result;
        }
    }
}
