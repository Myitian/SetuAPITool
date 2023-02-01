using Myitian.SetuAPITool.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Myitian.SetuAPITool.Anosu
{
    /// <summary>Anosu 客户端</summary>
    public class AnosuClient : SetuAPIClient, IPixivPictureClient
    {
        /// <summary>使用文档</summary>
        public const string Document = "https://docs.anosu.top";

        /// <summary>302跳转请求地址</summary>
        public static string RedirectAPI = "https://image.anosu.top/pixiv/direct";
        /// <summary>JSON请求地址</summary>
        public static string JsonAPI = "https://image.anosu.top/pixiv/json";
        /// <summary>通用请求地址</summary>
        public static string UniversalAPI = "https://image.anosu.top/pixiv";

        /// <inheritdoc cref="SetuAPIClient()"/>
        public AnosuClient() : base() { }
        /// <inheritdoc cref="SetuAPIClient(HttpClient)"/>
        public AnosuClient(HttpClient httpClient) : base(httpClient) { }


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
            return await GetJsonAsync(new Request(num, r18));
        }
        /// <summary>获取JSON</summary>
        /// <param name="num">数量</param>
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        /// <returns>字符串形式的JSON</returns>
        public async Task<string> GetJsonAsync(int num, R18Type r18, string keyword)
        {
            return await GetJsonAsync(new Request(num, r18, keyword));
        }
        /// <summary>获取JSON</summary>
        /// <param name="request">请求</param>
        /// <returns>字符串形式的JSON</returns>
        public async Task<string> GetJsonAsync(Request request)
        {
            if (request.PostRequest)
            {
                using (HttpResponseMessage resp = await PostAsync(JsonAPI, ParametersConverter.UrlEncode(request.ToKeyValuePairs())))
                {
                    return await resp.Content.ReadAsStringAsync();
                }
            }
            else
            {
                using (HttpResponseMessage resp = await GetAsync(JsonAPI, ParametersConverter.UrlEncode(request.ToKeyValuePairs())))
                {
                    return await resp.Content.ReadAsStringAsync();
                }
            }
        }
        #endregion GetJsonAsync

        #region GetPictureAsync
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync(KeyValuePair{string, string}[])"/>
        public override async Task<HttpResponseMessage> GetPictureAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetPictureAsync(new Request(parameters));
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync()"/>
        public override async Task<HttpResponseMessage> GetPictureAsync()
        {
            return await GetPictureAsync(new Request());
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync(R18Type)"/>
        public override async Task<HttpResponseMessage> GetPictureAsync(R18Type r18)
        {
            return await GetPictureAsync(new Request(r18: r18));
        }
        /// <summary>获取图像</summary>
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        public async Task<HttpResponseMessage> GetPictureAsync(R18Type r18, string keyword)
        {
            return await GetPictureAsync(new Request(1, r18, keyword));
        }
        /// <summary>获取图像</summary>
        /// <param name="request">请求</param>
        public async Task<HttpResponseMessage> GetPictureAsync(Request request)
        {
            if (request.PostRequest)
            {
                return await GetAsync(await GetPictureUrlAsync(request));
            }
            else
            {
                return await GetAsync(RedirectAPI, ParametersConverter.UrlEncode(request.ToKeyValuePairs()));
            }
        }
        #endregion GetPictureAsync

        #region GetPictureUrlAsync
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlAsync(KeyValuePair{string, string}[])"/>
        public override async Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPixivInfoAsync(parameters)).Url;
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlAsync()"/>
        public override async Task<string> GetPictureUrlAsync()
        {
            return (await GetPixivInfoAsync()).Url;
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlAsync(R18Type)"/>
        public override async Task<string> GetPictureUrlAsync(R18Type r18)
        {
            return (await GetPixivInfoAsync(r18)).Url;
        }
        /// <summary>获取图像URL</summary>
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        public async Task<string> GetPictureUrlAsync(R18Type r18, string keyword)
        {
            return (await GetPixivInfoAsync(r18, keyword)).Url;
        }
        /// <summary>获取图像URL</summary>
        /// <param name="request">请求</param>
        public async Task<string> GetPictureUrlAsync(Request request)
        {
            return (await GetPixivInfoAsync(request)).Url;
        }
        #endregion GetPictureUrlAsync

        #region GetPictureUrlsAsync
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(KeyValuePair{string, string}[])"/>
        public override async Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPixivInfoListAsync(parameters)).Select(x => x.Url).ToList(); ;
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(int)"/>
        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            return (await GetPixivInfoListAsync()).Select(x => x.Url).ToList(); ;
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(int, R18Type)"/>
        public override async Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            return (await GetPixivInfoListAsync(num, r18)).Select(x => x.Url).ToList();
        }
        /// <summary>获取图像URL列表</summary>
        /// <param name="num">数量</param>
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        public async Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18, string keyword)
        {
            return (await GetPixivInfoListAsync(num, r18, keyword)).Select(x => x.Url).ToList(); ;
        }
        /// <summary>获取图像URL列表</summary>
        /// <param name="request">请求</param>
        public async Task<List<string>> GetPictureUrlsAsync(Request request)
        {
            return (await GetPixivInfoListAsync(request)).Select(x => x.Url).ToList(); ;
        }
        #endregion GetPictureUrlsAsync

        #region GetPixivInfoAsync
        /// <inheritdoc cref="IPixivPictureClient.GetPixivInfoAsync(KeyValuePair{string, string}[])"/>
        public async Task<PixivInfo> GetPixivInfoAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPixivInfoListAsync(parameters))[0];
        }
        /// <inheritdoc cref="IPixivPictureClient.GetPixivInfoAsync()"/>
        public async Task<PixivInfo> GetPixivInfoAsync()
        {
            return (await GetPixivInfoListAsync())[0];
        }
        /// <inheritdoc cref="IPixivPictureClient.GetPixivInfoAsync(R18Type)"/>
        public async Task<PixivInfo> GetPixivInfoAsync(R18Type r18)
        {
            return (await GetPixivInfoListAsync(1, r18))[0];
        }
        /// <summary>获取Pixiv图像信息</summary>
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        /// <returns>一个 <see cref="PixivInfo"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<PixivInfo> GetPixivInfoAsync(R18Type r18, string keyword)
        {
            return (await GetPixivInfoListAsync(1, r18, keyword))[0];
        }
        /// <summary>获取Pixiv图像信息</summary>
        /// <param name="request">请求</param>
        /// <returns>一个 <see cref="PixivInfo"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<PixivInfo> GetPixivInfoAsync(Request request)
        {
            return (await GetPixivInfoListAsync(request))[0];
        }
        #endregion GetPixivInfoAsync

        #region GetPixivInfoListAsync
        /// <inheritdoc cref="IPixivPictureClient.GetPixivInfoListAsync(KeyValuePair{string, string}[])"/>
        public async Task<List<PixivInfo>> GetPixivInfoListAsync(params KeyValuePair<string, string>[] parameters)
        {
            return JsonConvert.DeserializeObject<List<PixivInfo>>(await GetJsonAsync(parameters));
        }
        /// <inheritdoc cref="IPixivPictureClient.GetPixivInfoListAsync(int)"/>
        public async Task<List<PixivInfo>> GetPixivInfoListAsync(int num = 1)
        {
            return JsonConvert.DeserializeObject<List<PixivInfo>>(await GetJsonAsync(num));
        }
        /// <inheritdoc cref="IPixivPictureClient.GetPixivInfoListAsync(int, R18Type)"/>
        public async Task<List<PixivInfo>> GetPixivInfoListAsync(int num, R18Type r18)
        {
            return JsonConvert.DeserializeObject<List<PixivInfo>>(await GetJsonAsync(num, r18));
        }
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="num">数量</param>
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<List<PixivInfo>> GetPixivInfoListAsync(int num, R18Type r18, string keyword)
        {
            return JsonConvert.DeserializeObject<List<PixivInfo>>(await GetJsonAsync(num, r18, keyword));
        }
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="request">请求</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<List<PixivInfo>> GetPixivInfoListAsync(Request request)
        {
            return JsonConvert.DeserializeObject<List<PixivInfo>>(await GetJsonAsync(request));
        }
        #endregion GetPixivInfoListAsync
    }
}
