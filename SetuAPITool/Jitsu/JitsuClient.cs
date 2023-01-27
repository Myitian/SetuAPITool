using Newtonsoft.Json;
using SetuAPITool.MirlKoi;
using SetuAPITool.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SetuAPITool.Jitsu
{
    public class JitsuClient : SetuAPIClient
    {
        public const string Document = "https://img.jitsu.top/";

        public double RandomPixivNormalWeight = 0.2;
        public double RandomPixivJitsuWeight = 0.2;

        public double RandomR18Weight = 0.5;
        public double RandomSpR18Weight = 0.2;


        static string[] _anosuImg = {
            "https://api.anosu.top/img/",
            "https://moe.anosu.top/img/",
        };
        static string[] _anosuApi = {
            "https://api.anosu.top/api/",
            "https://moe.anosu.top/api/",
        };
        static string[] _anosuR18 = {
            "https://api.anosu.top/r18/",
            "https://moe.anosu.top/r18/",
        };
        static string[] _jitsuImg = {
            "https://api.jitsu.top/img/",
            "https://moe.jitsu.top/img/",
        };
        static string[] _jitsuApi = {
            "https://api.jitsu.top/api/",
            "https://moe.jitsu.top/api/",
        };
        static string[] _jitsuR18 = {
            "https://api.jitsu.top/r18/",
            "https://moe.jitsu.top/r18/",
        };

        static string[] _allImg = _anosuImg.Concat(_jitsuImg).ToArray();
        static string[] _allApi = _anosuApi.Concat(_jitsuApi).ToArray();
        static string[] _allR18 = _anosuImg.Concat(_jitsuR18).ToArray();

        static Dictionary<Source, string[]> _img = new Dictionary<Source, string[]>
        {
            { Source.Anosu, _anosuImg },
            { Source.Jitsu, _jitsuImg },
            { Source.All, _allImg }
        };
        static Dictionary<Source, string[]> _api = new Dictionary<Source, string[]>
        {
            { Source.Anosu, _anosuApi },
            { Source.Jitsu, _jitsuApi },
            { Source.All, _allApi }
        };
        static Dictionary<Source, string[]> _r18 = new Dictionary<Source, string[]>
        {
            { Source.Anosu, _anosuR18 },
            { Source.Jitsu, _jitsuR18 },
            { Source.All, _allR18 }
        };

        protected RandomUtil _randomUtil;
        public virtual RandomUtil RandomUtil
        {
            get => _randomUtil;
            set => _randomUtil = value ?? throw new ArgumentNullException(nameof(value));
        }

        public JitsuClient() : this(null, null) { }
        public JitsuClient(HttpClient httpClient) : this(httpClient, null) { }
        public JitsuClient(HttpClient httpClient, RandomUtil random) : base(httpClient)
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
        public override async Task<string> GetJsonAsync(int num, R18Type r18)
        {
            switch(r18)
            {
                case R18Type.NonR18:
                    double p = _randomUtil.RNG.NextDouble();
                    if (p < RandomPixivJitsuWeight)
                    {
                        return await GetJsonAsync(new Request(num, Sort.Jitsu));
                    }
                    else if (p < RandomPixivJitsuWeight + RandomPixivNormalWeight)
                    {
                        return await GetJsonAsync(new Request(num, Sort.Pixiv));
                    }
                    else
                    {
                        return await GetJsonAsync(new Request(num));
                    }
                case R18Type.R18:
                    if (_randomUtil.RNG.NextDouble() < RandomSpR18Weight)
                    {
                        return await GetJsonAsync(new Request(num, Sort.R18));
                    }
                    else
                    {
                        return await GetJsonAsync(new Request(num, Sort.SpR18));
                    }
                case R18Type.Random:
                    if (_randomUtil.RNG.NextDouble() < RandomR18Weight)
                    {
                        goto case R18Type.R18;
                    }
                    else
                    {
                        goto case R18Type.NonR18;
                    }
                default:
                    goto case R18Type.NonR18;
            }
        }
        public async Task<string> GetJsonAsync(int num, Sort sort)
        {
            return await GetJsonAsync(new Request(num, sort));
        }
        public async Task<string> GetJsonAsync(Request request)
        {
            string[] _urls;
            Source src = request.Source;
            if (src == Source.Default)
            {
                src = Source.Anosu;
            }
            if (request.Sort == Sort.SpR18)
            {
                _urls = _r18[src];
            }
            else if(request.Size.SizeType == SizeType.Original)
            {
                _urls = _api[src];
            }
            else
            {
                _urls = _img[src];
            }
            Dictionary<string, string> paramDic = request.ToRequestDictionary();
            paramDic["type"] = "json";
            if (request.PostRequest)
            {
                using (HttpResponseMessage resp = await PostAsync(_randomUtil.GetRandom(_urls), ParametersConverter.UrlEncode(paramDic.ToArray())))
                {
                    return await resp.Content.ReadAsStringAsync();
                }
            }
            else
            {
                using (HttpResponseMessage resp = await GetAsync(_randomUtil.GetRandom(_urls), ParametersConverter.UrlEncode(paramDic.ToArray())))
                {
                    return await resp.Content.ReadAsStringAsync();
                }
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
        public async Task<Response> GetResponseAsync(int num, R18Type r18)
        {
            return JsonConvert.DeserializeObject<Response>(await GetJsonAsync(num, r18));
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
            return await GetPictureAsync(new Request(parameters));
        }
        public override async Task<HttpResponseMessage> GetPictureAsync()
        {
            return await GetPictureAsync(R18Type.NonR18);
        }
        public override async Task<HttpResponseMessage> GetPictureAsync(R18Type r18)
        {
            switch (r18)
            {
                case R18Type.NonR18:
                    double p = _randomUtil.RNG.NextDouble();
                    if (p < RandomPixivJitsuWeight)
                    {
                        return await GetPictureAsync(new Request(sort: Sort.Jitsu));
                    }
                    else if (p < RandomPixivJitsuWeight + RandomPixivNormalWeight)
                    {
                        return await GetPictureAsync(new Request(sort: Sort.Pixiv));
                    }
                    else
                    {
                        return await GetPictureAsync();
                    }
                case R18Type.R18:
                    if (_randomUtil.RNG.NextDouble() < RandomSpR18Weight)
                    {
                        return await GetPictureAsync(new Request(sort: Sort.R18));
                    }
                    else
                    {
                        return await GetPictureAsync(new Request(sort: Sort.SpR18));
                    }
                case R18Type.Random:
                    if (_randomUtil.RNG.NextDouble() < RandomR18Weight)
                    {
                        goto case R18Type.R18;
                    }
                    else
                    {
                        goto case R18Type.NonR18;
                    }
                default:
                    goto case R18Type.NonR18;
            }
        }
        public async Task<HttpResponseMessage> GetPictureAsync(Sort sort)
        {
            return await GetPictureAsync(new Request(sort: sort));
        }
        public async Task<HttpResponseMessage> GetPictureAsync(Request request)
        {
            string[] _urls;
            Source src = request.Source;

            if (src == Source.Default)
            {
                if(request.Sort == Sort.R18)
                {
                    src = Source.Jitsu;
                }
                else
                {
                    src = Source.Anosu;
                }
            }
            if (request.Sort == Sort.SpR18)
            {
                _urls = _r18[src];
            }
            else
            {
                if (request.Size.SizeType == SizeType.Original)
                {
                    _urls = _api[src];
                }
                else
                {
                    _urls = _img[src];
                }
            }

            if (request.Sort == Sort.R18 && src == Source.Anosu)
            {
                return await GetAsync(await GetPictureUrlAsync(new Request(request) { Source = src }));
            }
            else
            {
                Dictionary<string, string> paramDic = request.ToRequestDictionary();
                paramDic["type"] = "json";
                if (request.PostRequest)
                {
                    return await GetAsync(_randomUtil.GetRandom(_urls), ParametersConverter.UrlEncode(paramDic.ToArray()));
                }
                else
                {
                    return await PostAsync(_randomUtil.GetRandom(_urls), ParametersConverter.UrlEncode(paramDic.ToArray()));
                }
            }
        }

        public override async Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPictureUrlsAsync(parameters))[0];
        }
        public override async Task<string> GetPictureUrlAsync()
        {
            return (await GetPictureUrlsAsync())[0];
        }
        public override async Task<string> GetPictureUrlAsync(R18Type r18)
        {
            return (await GetPictureUrlsAsync(1, r18))[0];
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
            return (await GetResponseAsync(parameters)).Pics;
        }
        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            return (await GetResponseAsync(num)).Pics;
        }
        public override async Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            return (await GetResponseAsync(num, r18)).Pics;
        }
        public async Task<List<string>> GetPictureUrlsAsync(int num, Sort sort)
        {
            return (await GetResponseAsync(num, sort)).Pics;
        }
        public async Task<List<string>> GetPictureUrlsAsync(Request request)
        {
            return (await GetResponseAsync(request)).Pics;
        }
    }
}
