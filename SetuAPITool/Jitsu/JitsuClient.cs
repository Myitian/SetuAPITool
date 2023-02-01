using Myitian.SetuAPITool.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Myitian.SetuAPITool.Jitsu
{
    /// <summary>Jitsu 客户端</summary>
    public class JitsuClient : SetuAPIClient
    {
        /// <summary>使用文档</summary>
        public const string Document = "https://img.jitsu.top/";

        /// <summary>普通Pixiv图像随机权重</summary>
        public double RandomPixivNormalWeight = 0.2;
        /// <summary>Jitsu收藏图像随机权重</summary>
        public double RandomPixivJitsuWeight = 0.2;

        /// <summary>R18图像随机权重</summary>
        public double RandomR18Weight = 0.5;

        /// <summary>特殊R18图像图像随机权重</summary>
        public double RandomSpR18Weight = 0.2;

        /// <summary>Anosu /img/ API</summary>
        public string[] AnosuIMG = {
            "https://api.anosu.top/img/",
            "https://moe.anosu.top/img/",
        };
        /// <summary>Anosu /api/ API</summary>
        public string[] AnosuAPI = {
            "https://api.anosu.top/api/",
            "https://moe.anosu.top/api/",
        };
        /// <summary>Anosu /r18/ API</summary>
        public string[] AnosuR18 = {
            "https://api.anosu.top/r18/",
            "https://moe.anosu.top/r18/",
        };
        /// <summary>Jitsu /img/ API</summary>
        public string[] JitsuIMG = {
            "https://api.jitsu.top/img/",
            "https://moe.jitsu.top/img/",
        };
        /// <summary>Jitsu /api/ API</summary>
        public string[] JitsuAPI = {
            "https://api.jitsu.top/api/",
            "https://moe.jitsu.top/api/",
        };
        /// <summary>Jitsu /r18/ API</summary>
        public string[] JitsuR18 = {
            "https://api.jitsu.top/r18/",
            "https://moe.jitsu.top/r18/",
        };

        /// <summary>所有 /img/ API</summary>
        public string[] AllIMG;
        /// <summary>所有 /api/ API</summary>
        public string[] AllAPI;
        /// <summary>所有 /r18/ API</summary>
        public string[] AllR18;

        /// <summary>Source 到 /img/ API 的映射</summary>
        public Dictionary<Source, string[]> IMG;
        /// <summary>Source 到 /api/ API 的映射</summary>
        public Dictionary<Source, string[]> API;
        /// <summary>Source 到 /r18/ API 的映射</summary>
        public Dictionary<Source, string[]> R18;

        /// <summary>随机工具</summary>
        protected RandomUtil _randomUtil;
        /// <summary>随机工具</summary>
        public virtual RandomUtil RandomUtil
        {
            get => _randomUtil;
            set => _randomUtil = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc cref="SetuAPIClient()"/>
        public JitsuClient() : this(null, null) { }
        /// <inheritdoc cref="SetuAPIClient(HttpClient)"/>
        public JitsuClient(HttpClient httpClient) : this(httpClient, null) { }
        /// <param name="httpClient">HTTP客户端</param>
        /// <param name="random">随机工具</param>
        public JitsuClient(HttpClient httpClient, RandomUtil random) : base(httpClient)
        {
            _randomUtil = random ?? new RandomUtil();
            AllIMG = AnosuIMG.Concat(JitsuIMG).ToArray();
            AllAPI = AnosuAPI.Concat(JitsuAPI).ToArray();
            AllR18 = AnosuR18.Concat(JitsuR18).ToArray();
            IMG = new Dictionary<Source, string[]>
            {
                { Source.Anosu, AnosuIMG },
                { Source.Jitsu, JitsuIMG },
                { Source.All, AllIMG }
            };
            API = new Dictionary<Source, string[]>
            {
                { Source.Anosu, AnosuAPI },
                { Source.Jitsu, JitsuAPI },
                { Source.All, AllAPI }
            };
            R18 = new Dictionary<Source, string[]>
            {
                { Source.Anosu, AnosuR18 },
                { Source.Jitsu, JitsuR18 },
                { Source.All, AllR18 }
            };

        }

        #region GetJsonAsync
        /// <inheritdoc cref="SetuAPIClient.GetJsonAsync(KeyValuePair{string, string}[])"/>
        public override async Task<string> GetJsonAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetJsonAsync(new Request(parameters));
        }
        /// <inheritdoc cref="SetuAPIClient.GetJsonAsync(int)"/>
        public override async Task<string> GetJsonAsync(int num = 1)
        {
            return await GetJsonAsync(new Request(num));
        }
        /// <inheritdoc cref="SetuAPIClient.GetJsonAsync(int, R18Type)"/>
        public override async Task<string> GetJsonAsync(int num, R18Type r18)
        {
            switch (r18)
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
        /// <summary>获取JSON</summary>
        /// <param name="num">数量</param>
        /// <param name="sort">分类</param>
        /// <returns>字符串形式的JSON</returns>
        public async Task<string> GetJsonAsync(int num, Sort sort)
        {
            return await GetJsonAsync(new Request(num, sort));
        }
        /// <summary>获取JSON</summary>
        /// <param name="request">请求</param>
        /// <returns>字符串形式的JSON</returns>
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
                _urls = R18[src];
            }
            else if (request.Size.SizeType == SizeType.Original)
            {
                _urls = API[src];
            }
            else
            {
                _urls = IMG[src];
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
        #endregion GetJsonAsync

        #region GetResponseAsync
        /// <summary>获取响应</summary>
        /// <param name="parameters">请求参数</param>
        public async Task<Response> GetResponseAsync(params KeyValuePair<string, string>[] parameters)
        {
            return JsonConvert.DeserializeObject<Response>(await GetJsonAsync(parameters));
        }
        /// <summary>获取响应</summary>
        /// <param name="num">数量</param>
        public async Task<Response> GetResponseAsync(int num = 1)
        {
            return JsonConvert.DeserializeObject<Response>(await GetJsonAsync(num));
        }
        /// <summary>获取响应</summary>
        /// <param name="num">数量</param>
        /// <param name="r18">R18类型</param>
        public async Task<Response> GetResponseAsync(int num, R18Type r18)
        {
            return JsonConvert.DeserializeObject<Response>(await GetJsonAsync(num, r18));
        }
        /// <summary>获取响应</summary>
        /// <param name="num">数量</param>
        /// <param name="sort">分类</param>
        public async Task<Response> GetResponseAsync(int num, Sort sort)
        {
            return JsonConvert.DeserializeObject<Response>(await GetJsonAsync(num, sort));
        }
        /// <summary>获取响应</summary>
        /// <param name="request">请求</param>
        public async Task<Response> GetResponseAsync(Request request)
        {
            return JsonConvert.DeserializeObject<Response>(await GetJsonAsync(request));
        }
        #endregion GetResponseAsync

        #region GetPictureAsync
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync(KeyValuePair{string, string}[])"/>
        public override async Task<HttpResponseMessage> GetPictureAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetPictureAsync(new Request(parameters));
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync()"/>
        public override async Task<HttpResponseMessage> GetPictureAsync()
        {
            return await GetPictureAsync(R18Type.NonR18);
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync(R18Type)"/>
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
        /// <summary>获取图像</summary>
        /// <param name="sort">分类</param>
        public async Task<HttpResponseMessage> GetPictureAsync(Sort sort)
        {
            return await GetPictureAsync(new Request(sort: sort));
        }
        /// <summary>获取图像</summary>
        /// <param name="request">请求</param>
        public async Task<HttpResponseMessage> GetPictureAsync(Request request)
        {
            string[] _urls;
            Source src = request.Source;

            if (src == Source.Default)
            {
                if (request.Sort == Sort.R18)
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
                _urls = R18[src];
            }
            else
            {
                if (request.Size.SizeType == SizeType.Original)
                {
                    _urls = API[src];
                }
                else
                {
                    _urls = IMG[src];
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
        #endregion GetPictureAsync

        #region GetPictureUrlAsync
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlAsync(KeyValuePair{string, string}[])"/>
        public override async Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPictureUrlsAsync(parameters))[0];
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlAsync()"/>
        public override async Task<string> GetPictureUrlAsync()
        {
            return (await GetPictureUrlsAsync())[0];
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlAsync(R18Type)"/>
        public override async Task<string> GetPictureUrlAsync(R18Type r18)
        {
            return (await GetPictureUrlsAsync(1, r18))[0];
        }
        /// <summary>获取图像URL</summary>
        /// <param name="sort">分类</param>
        /// <returns>字符串形式的URL</returns>
        public async Task<string> GetPictureUrlAsync(Sort sort)
        {
            return (await GetPictureUrlsAsync(1, sort))[0];
        }
        /// <summary>获取图像URL</summary>
        /// <param name="request">请求</param>
        /// <returns>字符串形式的URL</returns>
        public async Task<string> GetPictureUrlAsync(Request request)
        {
            return (await GetPictureUrlsAsync(request))[0];
        }
        #endregion GetPictureUrlAsync

        #region GetPictureUrlsAsync
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(KeyValuePair{string, string}[])"/>
        public override async Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetResponseAsync(parameters)).Pics;
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(int)"/>
        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            return (await GetResponseAsync(num)).Pics;
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(int, R18Type)"/>
        public override async Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            return (await GetResponseAsync(num, r18)).Pics;
        }
        /// <summary>获取图像URL列表</summary>
        /// <param name="num">数量</param>
        /// <param name="sort">分类</param>
        /// <returns>字符串形式的URL的列表</returns>
        public async Task<List<string>> GetPictureUrlsAsync(int num, Sort sort)
        {
            return (await GetResponseAsync(num, sort)).Pics;
        }
        /// <summary>获取图像URL列表</summary>
        /// <param name="request">请求</param>
        /// <returns>字符串形式的URL的列表</returns>
        public async Task<List<string>> GetPictureUrlsAsync(Request request)
        {
            return (await GetResponseAsync(request)).Pics;
        }
        #endregion GetPictureUrlsAsync
    }
}
