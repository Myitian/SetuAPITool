using Newtonsoft.Json;
using SetuAPITool.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SetuAPITool.MirlKoi
{
    internal class MirlKoiClient : SetuAPIClient
    {
        public const string Document = "https://iw233.cn/main.html";

        protected RandomUtil _randomUtil;
        public virtual RandomUtil RandomUtil
        {
            get => _randomUtil;
            set => _randomUtil = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override bool SupportR18 => false;

        public string[] URLs = {
            "https://iw233.cn/api.php",
            "https://api.iw233.cn/api.php",
            "https://ap1.iw233.cn/api.php",
            "https://dev.iw233.cn/api.php",
            "https://mirlkoi.ifast3.vipnps.vip/api.php"
        };

        public MirlKoiClient() : this(null, null) { }
        public MirlKoiClient(HttpClient httpClient) : this(httpClient, null) { }
        public MirlKoiClient(HttpClient httpClient, RandomUtil random) : base(httpClient)
        {
            _randomUtil = random ?? new RandomUtil();
        }

        public override async Task<string> GetJsonAsync(params KeyValuePair<string, string>[] patameters)
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> parameter in patameters)
            {
                paramDic[parameter.Key] = parameter.Value;
            }
            paramDic["type"] = "json";
            return await (await GetAsync(_randomUtil.GetRandom(URLs), ParametersConverter.URLEncode(paramDic.ToArray()))).Content.ReadAsStringAsync();
        }
        public override async Task<string> GetJsonAsync(int num = 1)
        {
            return await GetJsonAsync(new KeyValuePair<string, string>("num", num.ToString()));
        }
        public override Task<string> GetJsonAsync(int num, R18Type r18)
        {
            throw new NotSupportedException();
        }
        public async Task<string> GetJsonAsync(Sort sort = Sort.Random, int num = 1)
        {
            return await GetJsonAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("sort", EnumConverter.ToString(sort)));
        }
        public async Task<string> GetJsonAsync(Request request)
        {
            return await GetJsonAsync(request.ToKeyValuePairs());
        }

        public override async Task<HttpContent> GetPictureAsync(params KeyValuePair<string, string>[] patameters)
        {
            foreach (KeyValuePair<string, string> parameter in patameters)
            {
                if (parameter.Key == "sort")
                {
                    return (await GetAsync(_randomUtil.GetRandom(URLs), ParametersConverter.URLEncode(parameter))).Content;
                }
            }
            return await GetPictureAsync();
        }
        public override async Task<HttpContent> GetPictureAsync()
        {
            return await GetPictureAsync(Sort.Random);
        }
        public override Task<HttpContent> GetPictureAsync(R18Type r18)
        {
            throw new NotSupportedException();
        }
        public async Task<HttpContent> GetPictureAsync(Sort sort)
        {
            return (await GetAsync(_randomUtil.GetRandom(URLs), "sort=" + EnumConverter.ToString(sort))).Content;
        }
        public async Task<HttpContent> GetPictureAsync(Request request)
        {
            return await GetPictureAsync(request.Sort);
        }

        public override async Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] patameters)
        {
            return JsonConvert.DeserializeObject<Respond>(await GetJsonAsync(patameters)).Pic[0];
        }
        public override async Task<string> GetPictureUrlAsync()
        {
            return JsonConvert.DeserializeObject<Respond>(await GetJsonAsync()).Pic[0];
        }
        public override Task<string> GetPictureUrlAsync(R18Type r18)
        {
            throw new NotSupportedException();
        }
        public async Task<string> GetPictureUrlAsync(Sort sort)
        {
            return JsonConvert.DeserializeObject<Respond>(await GetJsonAsync(sort)).Pic[0];
        }
        public async Task<string> GetPictureUrlAsync(Request request)
        {
            return JsonConvert.DeserializeObject<Respond>(await GetJsonAsync(request)).Pic[0];
        }

        public override async Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] patameters)
        {
            return JsonConvert.DeserializeObject<Respond>(await GetJsonAsync(patameters)).Pic.ToList();
        }
        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            return JsonConvert.DeserializeObject<Respond>(await GetJsonAsync()).Pic.ToList();
        }
        public override Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            throw new NotSupportedException();
        }
        public async Task<List<string>> GetPictureUrlsAsync(Sort sort)
        {
            return JsonConvert.DeserializeObject<Respond>(await GetJsonAsync(sort)).Pic.ToList();
        }
        public async Task<List<string>> GetPictureUrlsAsync(Request request)
        {
            return JsonConvert.DeserializeObject<Respond>(await GetJsonAsync(request)).Pic.ToList();
        }
    }
}
