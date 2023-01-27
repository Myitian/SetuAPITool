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

        public override bool SupportR18 => false;

        public string[] URLs = {
            "https://iw233.cn/api.php",
            "https://api.iw233.cn/api.php",
            "https://ap1.iw233.cn/api.php",
            "https://dev.iw233.cn/api.php",
            "https://mirlkoi.ifast3.vipnps.vip/api.php"
        };

        protected RandomUtil _randomUtil;
        public virtual RandomUtil RandomUtil
        {
            get => _randomUtil;
            set => _randomUtil = value ?? throw new ArgumentNullException(nameof(value));
        }

        public MirlKoiClient() : this(null, null) { }
        public MirlKoiClient(HttpClient httpClient) : this(httpClient, null) { }
        public MirlKoiClient(HttpClient httpClient, RandomUtil random) : base(httpClient)
        {
            _randomUtil = random ?? new RandomUtil();
        }

        public override async Task<string> GetJsonAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetJsonAsync(new Request(parameters));
        }
        public override async Task<string> GetJsonAsync(int num = 1)
        {
            return await GetJsonAsync(new Request(num));
        }
        public override Task<string> GetJsonAsync(int num, R18Type r18)
        {
            throw new NotSupportedException();
        }
        public async Task<string> GetJsonAsync(int num, Sort sort)
        {
            return await GetJsonAsync(new Request(num, sort));
        }
        public async Task<string> GetJsonAsync(Request request)
        {
            Dictionary<string, string> paramDic = request.ToDictionary();
            paramDic["type"] = "json";
            using (HttpResponseMessage resp = await GetAsync(_randomUtil.GetRandom(URLs), ParametersConverter.UrlEncode(paramDic.ToArray())))
            {
                return await resp.Content.ReadAsStringAsync();
            }
        }

        public async Task<Response> GetResponseAsync(params KeyValuePair<string, string>[] parameters)
        {
            return JsonConvert.DeserializeObject<Response>(await GetJsonAsync(parameters));
        }
        public async Task<Response> GetResponseAsync(int num = 1)
        {
            return JsonConvert.DeserializeObject<Response>(await GetJsonAsync(num));
        }
        public async Task<Response> GetResponseAsync(int num, Sort sort)
        {
            return JsonConvert.DeserializeObject<Response>(await GetJsonAsync(num, sort));
        }
        public async Task<Response> GetResponseAsync(Request request)
        {
            return JsonConvert.DeserializeObject<Response>(await GetJsonAsync(request));
        }

        public override async Task<HttpResponseMessage> GetPictureAsync(params KeyValuePair<string, string>[] parameters)
        {
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                if (parameter.Key == "sort")
                {
                    return await GetAsync(_randomUtil.GetRandom(URLs), ParametersConverter.UrlEncode(parameter));
                }
            }
            return await GetPictureAsync();
        }
        public override async Task<HttpResponseMessage> GetPictureAsync()
        {
            return await GetPictureAsync(Sort.Random);
        }
        public override Task<HttpResponseMessage> GetPictureAsync(R18Type r18)
        {
            throw new NotSupportedException();
        }
        public async Task<HttpResponseMessage> GetPictureAsync(Sort sort)
        {
            return await GetAsync(_randomUtil.GetRandom(URLs), "sort=" + EnumConverter.ToString(sort));
        }
        public async Task<HttpResponseMessage> GetPictureAsync(Request request)
        {
            return await GetPictureAsync(request.Sort);
        }

        public override async Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPictureUrlsAsync(parameters))[0];
        }
        public override async Task<string> GetPictureUrlAsync()
        {
            return (await GetPictureUrlsAsync())[0];
        }
        public override Task<string> GetPictureUrlAsync(R18Type r18)
        {
            throw new NotSupportedException();
        }
        public async Task<string> GetPictureUrlAsync(Sort sort)
        {
            return (await GetPictureUrlsAsync(1, sort))[0];
        }
        public async Task<string> GetPictureUrlAsync(Request request)
        {
            return (await GetPictureUrlsAsync(request))[0];
        }

        public override async Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetResponseAsync(parameters)).Pic;
        }
        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            return (await GetResponseAsync(num)).Pic;
        }
        public override Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            throw new NotSupportedException();
        }
        public async Task<List<string>> GetPictureUrlsAsync(int num, Sort sort)
        {
            return (await GetResponseAsync(num, sort)).Pic;
        }
        public async Task<List<string>> GetPictureUrlsAsync(Request request)
        {
            return (await GetResponseAsync(request)).Pic;
        }
    }
}
