using Myitian.SetuAPITool.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Myitian.SetuAPITool.MirlKoi
{
    /// <summary>MirlKoi 客户端</summary>
    public class MirlKoiClient : SetuAPIClient
    {
        /// <summary>使用文档</summary>
        public const string Document = "https://iw233.cn/main.html";

        /// <inheritdoc cref="SetuAPIClient.SupportR18"/>
        public override bool SupportR18 => false;

        /// <summary>API地址</summary>
        public string[] URLs = {
            "https://iw233.cn/api.php",
            "https://api.iw233.cn/api.php",
            "https://ap1.iw233.cn/api.php",
            "https://dev.iw233.cn/api.php",
            "https://mirlkoi.ifast3.vipnps.vip/api.php"
        };

        /// <summary>随机工具</summary>
        protected RandomUtil _randomUtil;
        /// <summary>随机工具</summary>
        public virtual RandomUtil RandomUtil
        {
            get => _randomUtil;
            set => _randomUtil = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc cref="SetuAPIClient()"/>
        public MirlKoiClient() : this(null, null) { }
        /// <inheritdoc cref="SetuAPIClient(HttpClient)"/>
        public MirlKoiClient(HttpClient httpClient) : this(httpClient, null) { }
        /// <param name="httpClient">HTTP客户端</param>
        /// <param name="random">随机工具</param>
        public MirlKoiClient(HttpClient httpClient, RandomUtil random) : base(httpClient)
        {
            _randomUtil = random ?? new RandomUtil();
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
        public override Task<string> GetJsonAsync(int num, R18Type r18)
        {
            throw new NotSupportedException();
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
            Dictionary<string, string> paramDic = request.ToDictionary();
            paramDic["type"] = "json";
            using (HttpResponseMessage resp = await GetAsync(_randomUtil.GetRandom(URLs), ParametersConverter.UrlEncode(paramDic.ToArray())))
            {
                return await resp.Content.ReadAsStringAsync();
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
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                if (parameter.Key == "sort")
                {
                    return await GetAsync(_randomUtil.GetRandom(URLs), ParametersConverter.UrlEncode(parameter));
                }
            }
            return await GetPictureAsync();
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync()"/>
        public override async Task<HttpResponseMessage> GetPictureAsync()
        {
            return await GetPictureAsync(Sort.Random);
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync(R18Type)"/>
        public override Task<HttpResponseMessage> GetPictureAsync(R18Type r18)
        {
            throw new NotSupportedException();
        }
        /// <summary>获取图像</summary>
        /// <param name="sort">分类</param>
        public async Task<HttpResponseMessage> GetPictureAsync(Sort sort)
        {
            return await GetAsync(_randomUtil.GetRandom(URLs), "sort=" + EnumConverter.ToString(sort));
        }
        /// <summary>获取图像</summary>
        /// <param name="request">请求</param>
        public async Task<HttpResponseMessage> GetPictureAsync(Request request)
        {
            return await GetPictureAsync(request.Sort);
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
        public override Task<string> GetPictureUrlAsync(R18Type r18)
        {
            throw new NotSupportedException();
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
            return (await GetResponseAsync(parameters)).Pic;
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(int)"/>
        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            return (await GetResponseAsync(num)).Pic;
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(int, R18Type)"/>
        public override Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            throw new NotSupportedException();
        }
        /// <summary>获取图像URL列表</summary>
        /// <param name="num">数量</param>
        /// <param name="sort">分类</param>
        /// <returns>字符串形式的URL的列表</returns>
        public async Task<List<string>> GetPictureUrlsAsync(int num, Sort sort)
        {
            return (await GetResponseAsync(num, sort)).Pic;
        }
        /// <summary>获取图像URL列表</summary>
        /// <param name="request">请求</param>
        /// <returns>字符串形式的URL的列表</returns>
        public async Task<List<string>> GetPictureUrlsAsync(Request request)
        {
            return (await GetResponseAsync(request)).Pic;
        }
        #endregion GetPictureUrlsAsync
    }
}
