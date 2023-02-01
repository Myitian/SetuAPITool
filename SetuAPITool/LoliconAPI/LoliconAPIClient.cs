using Myitian.SetuAPITool.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Myitian.SetuAPITool.LoliconAPI
{
    /// <summary>Lolicon API 客户端</summary>
    public class LoliconAPIClient : SetuAPIClient, IPixivPictureClient
    {
        /// <summary>使用文档</summary>
        public const string Document = "https://api.lolicon.app";

        /// <summary>V1 API 地址</summary>
        public const string V1 = "https://api.lolicon.app/setu/v1";
        /// <summary>V2 API 地址</summary>
        public const string V2 = "https://api.lolicon.app/setu/v2";

        /// <inheritdoc cref="SetuAPIClient()"/>
        public LoliconAPIClient() : base() { }
        /// <inheritdoc cref="SetuAPIClient(HttpClient)"/>
        public LoliconAPIClient(HttpClient httpClient) : base(httpClient) { }


        #region GetJsonAsync
        /// <inheritdoc cref="SetuAPIClient.GetJsonAsync(KeyValuePair{string, string}[])"/>
        public override async Task<string> GetJsonAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetJsonAsync(true, parameters);
        }
        /// <inheritdoc cref="SetuAPIClient.GetJsonAsync(int)"/>
        public override async Task<string> GetJsonAsync(int num = 1)
        {
            return await GetJsonAsync(new KeyValuePair<string, string>("num", num.ToString()));
        }
        /// <inheritdoc cref="SetuAPIClient.GetJsonAsync(int, R18Type)"/>
        public override async Task<string> GetJsonAsync(int num, R18Type r18)
        {
            return await GetJsonAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
        }
        /// <summary>获取JSON</summary>
        /// <param name="num">数量</param>
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        /// <returns>字符串形式的JSON</returns>
        public async Task<string> GetJsonAsync(int num, R18Type r18, string keyword)
        {
            return await GetJsonAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()),
                new KeyValuePair<string, string>("keyword", keyword.ToString()));
        }
        /// <summary>获取JSON</summary>
        /// <param name="v2">使用 V2 API</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>字符串形式的JSON</returns>
        public async Task<string> GetJsonAsync(bool v2 = true, params KeyValuePair<string, string>[] parameters)
        {
            return await (v2 ? GetJsonV2Async(parameters: parameters) : GetJsonV1Async(parameters));
        }
        /// <summary>从 V1 API 获取JSON</summary>
        /// <param name="parameters">请求参数</param>
        /// <returns>字符串形式的JSON</returns>
        public async Task<string> GetJsonV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return await (await GetAsync(V1, ParametersConverter.UrlEncode(parameters))).Content.ReadAsStringAsync();
        }
        /// <summary>从 V1 API 获取JSON</summary>
        /// <param name="request">请求</param>
        /// <returns>字符串形式的JSON</returns>
        public async Task<string> GetJsonV1Async(V1.Request request = null)
        {
            return await (await GetAsync(V1, ParametersConverter.UrlEncode(request?.ToKeyValuePairs()))).Content.ReadAsStringAsync();
        }
        /// <summary>从 V2 API 获取JSON</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>字符串形式的JSON</returns>
        public async Task<string> GetJsonV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            if (postRequest)
            {
                using (HttpResponseMessage resp = await PostAsync(V2, JsonConvert.SerializeObject(new V2.Request(parameters)), "application/json"))
                {
                    return await resp.Content.ReadAsStringAsync();
                }
            }
            else
            {
                using (HttpResponseMessage resp = await GetAsync(V2, ParametersConverter.UrlEncode(parameters)))
                {
                    return await resp.Content.ReadAsStringAsync();
                }
            }
        }
        /// <summary>从 V2 API 获取JSON</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="request">请求</param>
        /// <returns>字符串形式的JSON</returns>
        public async Task<string> GetJsonV2Async(bool postRequest = false, V2.Request request = null)
        {
            if (postRequest)
            {
                using (HttpResponseMessage resp = await PostAsync(V2, JsonConvert.SerializeObject(request), "application/json"))
                {
                    return await resp.Content.ReadAsStringAsync();
                }
            }
            else
            {
                using (HttpResponseMessage resp = await GetAsync(V2, ParametersConverter.UrlEncode(request?.ToKeyValuePairs())))
                {
                    return await resp.Content.ReadAsStringAsync();
                }
            }
        }
        #endregion GetJsonAsync

        #region GetResponseAsync
        /// <summary>从 V1 API 获取响应</summary>
        /// <param name="parameters">请求参数</param>
        public async Task<V1.Response> GetResponseV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return JsonConvert.DeserializeObject<V1.Response>(await GetJsonV1Async(parameters));
        }
        /// <summary>从 V1 API 获取响应</summary>
        /// <param name="request">请求</param>
        public async Task<V1.Response> GetResponseV1Async(V1.Request request)
        {
            return JsonConvert.DeserializeObject<V1.Response>(await GetJsonV1Async(request));
        }
        /// <summary>从 V2 API 获取响应</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="parameters">请求参数</param>
        public async Task<V2.Response> GetResponseV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            return JsonConvert.DeserializeObject<V2.Response>(await GetJsonV2Async(postRequest, parameters));
        }
        /// <summary>从 V2 API 获取响应</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="request">请求</param>
        public async Task<V2.Response> GetResponseV2Async(bool postRequest = false, V2.Request request = null)
        {
            return JsonConvert.DeserializeObject<V2.Response>(await GetJsonV2Async(postRequest, request));
        }
        #endregion GetResponseAsync

        #region GetPictureAsync
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync(KeyValuePair{string, string}[])"/>
        public override async Task<HttpResponseMessage> GetPictureAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetPictureV2Async(parameters: parameters);
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync()"/>
        public override async Task<HttpResponseMessage> GetPictureAsync()
        {
            return await GetPictureAsync(null);
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync(R18Type)"/>
        public override async Task<HttpResponseMessage> GetPictureAsync(R18Type r18)
        {
            return await GetPictureAsync(new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
        }
        /// <summary>获取图像</summary>
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        public async Task<HttpResponseMessage> GetPictureAsync(R18Type r18, string keyword)
        {
            return await GetPictureAsync(
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()),
                new KeyValuePair<string, string>("keyword", keyword.ToString()));
        }
        /// <summary>获取图像</summary>
        /// <param name="v2">使用 V2 API</param>
        /// <param name="parameters">请求参数</param>
        public async Task<HttpResponseMessage> GetPictureAsync(bool v2, params KeyValuePair<string, string>[] parameters)
        {
            if (v2)
            {
                return await GetPictureV2Async(parameters: parameters);
            }
            else
            {
                return await GetPictureV1Async(parameters);
            }
        }
        /// <summary>从 V1 API 获取图像</summary>
        /// <param name="parameters">请求参数</param>
        public async Task<HttpResponseMessage> GetPictureV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return await GetAsync(await GetPictureUrlV1Async(parameters));
        }
        /// <summary>从 V1 API 获取图像</summary>
        /// <param name="request">请求</param>
        public async Task<HttpResponseMessage> GetPictureV1Async(V1.Request request)
        {
            return await GetAsync(await GetPictureUrlV1Async(request));
        }
        /// <summary>从 V2 API 获取图像</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="parameters">请求参数</param>
        public async Task<HttpResponseMessage> GetPictureV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            return await GetAsync(await GetPictureUrlV2Async(postRequest, parameters));
        }
        /// <summary>从 V2 API 获取图像</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="request">请求</param>
        public async Task<HttpResponseMessage> GetPictureV2Async(bool postRequest = false, V2.Request request = null)
        {
            return await GetAsync(await GetPictureUrlV2Async(postRequest, request));
        }
        #endregion GetPictureAsync

        #region GetPictureUrlAsync
        /// <inheritdoc cref="SetuAPIClient.GetPictureAsync(KeyValuePair{string, string}[])"/>
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
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        public async Task<string> GetPictureUrlAsync(R18Type r18, string keyword)
        {
            return (await GetPictureUrlsAsync(1, r18, keyword))[0];
        }
        /// <summary>获取图像URL</summary>
        /// <param name="v2">使用 V2 API</param>
        /// <param name="parameters">请求参数</param>
        public async Task<string> GetPictureUrlAsync(bool v2, params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPictureUrlsAsync(v2, parameters))[0];
        }
        /// <summary>从 V1 API 获取图像URL</summary>
        /// <param name="parameters">请求参数</param>
        public async Task<string> GetPictureUrlV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPictureUrlsV1Async(parameters))[0];
        }
        /// <summary>从 V1 API 获取图像URL</summary>
        /// <param name="request">请求</param>
        public async Task<string> GetPictureUrlV1Async(V1.Request request)
        {
            return (await GetPictureUrlsV1Async(request))[0];
        }
        /// <summary>从 V2 API 获取图像URL</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="parameters">请求参数</param>
        public async Task<string> GetPictureUrlV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPictureUrlsV2Async(postRequest, parameters))[0];
        }
        /// <summary>从 V2 API 获取图像URL</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="request">请求</param>
        public async Task<string> GetPictureUrlV2Async(bool postRequest = false, V2.Request request = null)
        {
            return (await GetPictureUrlsV2Async(postRequest, request))[0];
        }
        #endregion GetPictureUrlAsync

        #region GetPictureUrlsAsync
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(KeyValuePair{string, string}[])"/>
        public override async Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetPictureUrlsV2Async(parameters: parameters);
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(int)"/>
        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            return await GetPictureUrlsAsync(
                new KeyValuePair<string, string>("num", num.ToString()));
        }
        /// <inheritdoc cref="SetuAPIClient.GetPictureUrlsAsync(int, R18Type)"/>
        public override async Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            return await GetPictureUrlsAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
        }
        /// <summary>获取图像URL列表</summary>
        /// <param name="num">数量</param>
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        public async Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18, string keyword)
        {
            return await GetPictureUrlsAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()),
                new KeyValuePair<string, string>("keyword", keyword.ToString()));
        }
        /// <summary>获取图像URL列表</summary>
        /// <param name="v2">使用 V2 API</param>
        /// <param name="parameters">请求参数</param>
        public async Task<List<string>> GetPictureUrlsAsync(bool v2, params KeyValuePair<string, string>[] parameters)
        {
            if (v2)
            {
                return await GetPictureUrlsV2Async(parameters: parameters);
            }
            else
            {
                return await GetPictureUrlsV1Async(parameters);
            }
        }
        /// <summary>从 V1 API 获取图像URL列表</summary>
        /// <param name="parameters">请求参数</param>
        public async Task<List<string>> GetPictureUrlsV1Async(params KeyValuePair<string, string>[] parameters)
        {
            V1.Response Response = await GetResponseV1Async(parameters);
            return Response.Data.Select(x => x.Url).ToList();
        }
        /// <summary>从 V1 API 获取图像URL列表</summary>
        /// <param name="request">请求</param>
        public async Task<List<string>> GetPictureUrlsV1Async(V1.Request request)
        {
            V1.Response Response = await GetResponseV1Async(request);
            return Response.Data.Select(x => x.Url).ToList();
        }
        /// <summary>从 V2 API 获取图像URL列表</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="parameters">请求参数</param>
        public async Task<List<string>> GetPictureUrlsV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            V2.Response Response = await GetResponseV2Async(postRequest, parameters);
            return Response.Data.Select(x => x.Urls.Values.ElementAt(0)).ToList();
        }
        /// <summary>从 V2 API 获取图像URL列表</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="request">请求</param>
        public async Task<List<string>> GetPictureUrlsV2Async(bool postRequest = false, V2.Request request = null)
        {
            V2.Response Response = await GetResponseV2Async(postRequest, request);
            return Response.Data.Select(x => x.Urls.Values.ElementAt(0)).ToList();
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
        /// <param name="v2">使用 V2 API</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>一个 <see cref="PixivInfo"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<PixivInfo> GetPixivInfoAsync(bool v2, params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPixivInfoListAsync(v2, parameters))[0];
        }
        /// <summary>从 V1 API 获取Pixiv图像信息</summary>
        /// <param name="parameters">请求参数</param>
        /// <returns>一个 <see cref="PixivInfo"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<PixivInfo> GetPixivInfoV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPixivInfoListV1Async(parameters))[0];
        }
        /// <summary>从 V1 API 获取Pixiv图像信息</summary>
        /// <param name="request">请求</param>
        /// <returns>一个 <see cref="PixivInfo"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<PixivInfo> GetPixivInfoV1Async(V1.Request request)
        {
            return (await GetPixivInfoListV1Async(request))[0];
        }
        /// <summary>从 V2 API 获取Pixiv图像信息</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="parameters">请求参数</param>
        public async Task<PixivInfo> GetPixivInfoV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPixivInfoListV2Async(postRequest, parameters))[0];
        }
        /// <summary>从 V2 API 获取Pixiv图像信息</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="request">请求</param>
        public async Task<PixivInfo> GetPixivInfoV2Async(bool postRequest = false, V2.Request request = null)
        {
            return (await GetPixivInfoListV2Async(postRequest, request))[0];
        }
        #endregion GetPixivInfoAsync

        #region GetPixivInfoListAsync
        /// <inheritdoc cref="IPixivPictureClient.GetPixivInfoListAsync(KeyValuePair{string, string}[])"/>
        public async Task<List<PixivInfo>> GetPixivInfoListAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetPixivInfoListV2Async(parameters: parameters);
        }
        /// <inheritdoc cref="IPixivPictureClient.GetPixivInfoListAsync(int)"/>
        public async Task<List<PixivInfo>> GetPixivInfoListAsync(int num = 1)
        {
            return await GetPixivInfoListV1Async(new KeyValuePair<string, string>("num", num.ToString()));
        }
        /// <inheritdoc cref="IPixivPictureClient.GetPixivInfoListAsync(int, R18Type)"/>
        public async Task<List<PixivInfo>> GetPixivInfoListAsync(int num, R18Type r18)
        {
            return await GetPixivInfoListV1Async(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
        }
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="num">数量</param>
        /// <param name="r18">R18类型</param>
        /// <param name="keyword">关键词</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<List<PixivInfo>> GetPixivInfoListAsync(int num, R18Type r18, string keyword)
        {
            return await GetPixivInfoListV1Async(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()),
                new KeyValuePair<string, string>("keyword", keyword.ToString()));
        }
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="v2">使用 V2 API</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<List<PixivInfo>> GetPixivInfoListAsync(bool v2, params KeyValuePair<string, string>[] parameters)
        {
            if (v2)
            {
                return await GetPixivInfoListV2Async(parameters: parameters);
            }
            else
            {
                return await GetPixivInfoListV1Async(parameters);
            }
        }
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="parameters">请求参数</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<List<PixivInfo>> GetPixivInfoListV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetResponseV1Async(parameters)).Data.ToList();
        }
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="request">请求</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<List<PixivInfo>> GetPixivInfoListV1Async(V1.Request request)
        {
            return (await GetResponseV1Async(request)).Data.ToList();
        }
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<List<PixivInfo>> GetPixivInfoListV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            return (await GetResponseV2Async(postRequest, parameters)).Data.Select(x => new PixivInfo(x)).ToList();
        }
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="postRequest">使用POST请求</param>
        /// <param name="request">请求</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        public async Task<List<PixivInfo>> GetPixivInfoListV2Async(bool postRequest = false, V2.Request request = null)
        {
            return (await GetResponseV2Async(postRequest, request)).Data.Select(x => new PixivInfo(x)).ToList();
        }
        #endregion GetPixivInfoListAsync
    }
}
