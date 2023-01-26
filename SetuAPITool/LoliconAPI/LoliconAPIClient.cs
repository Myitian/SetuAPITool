using Newtonsoft.Json;
using SetuAPITool.Util;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SetuAPITool.LoliconAPI
{
    public class LoliconAPIClient : SetuAPIClient, IPixivPictureClient
    {
        public const string Document = "https://api.lolicon.app";

        public const string V1 = "https://api.lolicon.app/setu/v1";
        public const string V2 = "https://api.lolicon.app/setu/v2";

        public LoliconAPIClient() : base() { }
        public LoliconAPIClient(HttpClient httpClient) : base(httpClient) { }


        public override async Task<string> GetJsonAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetJsonAsync(true, parameters);
        }
        public override async Task<string> GetJsonAsync(int num = 1)
        {
            return await GetJsonAsync(new KeyValuePair<string, string>("num", num.ToString()));
        }
        public override async Task<string> GetJsonAsync(int num, R18Type r18)
        {
            return await GetJsonAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
        }
        public async Task<string> GetJsonAsync(int num, R18Type r18, string keyword)
        {
            return await GetJsonAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()),
                new KeyValuePair<string, string>("keyword", keyword.ToString()));
        }
        public async Task<string> GetJsonAsync(bool v2 = true, params KeyValuePair<string, string>[] parameters)
        {
            return await (v2 ? GetJsonV2Async(parameters: parameters) : GetJsonV1Async(parameters));
        }
        public async Task<string> GetJsonV1Async(params KeyValuePair<string, string>[] parameters)
        {
            if (parameters == null)
            {
                return await (await GetAsync(V1)).Content.ReadAsStringAsync();
            }
            else
            {
                return await (await GetAsync(V1, ParametersConverter.UrlEncode(parameters))).Content.ReadAsStringAsync();
            }
        }
        public async Task<string> GetJsonV1Async(V1.Request request = null)
        {
            if (request == null)
            {
                return await (await GetAsync(V1)).Content.ReadAsStringAsync();
            }
            else
            {
                return await (await GetAsync(V1, ParametersConverter.UrlEncode(request.ToKeyValuePairs()))).Content.ReadAsStringAsync();
            }
        }
        public async Task<string> GetJsonV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            if (postRequest)
            {
                if (parameters == null)
                {
                    using (HttpResponseMessage resp = await PostAsync(V2))
                    {
                        return await resp.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    using (HttpResponseMessage resp = await PostAsync(V2, JsonConvert.SerializeObject(new V2.Request(parameters)), "application/json"))
                    {
                        return await resp.Content.ReadAsStringAsync();
                    }
                }
            }
            else
            {
                if (parameters == null)
                {
                    using (HttpResponseMessage resp = await GetAsync(V2))
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
        }
        public async Task<string> GetJsonV2Async(bool postRequest = false, V2.Request request = null)
        {
            if (postRequest)
            {
                if (request == null)
                {
                    using (HttpResponseMessage resp = await PostAsync(V2))
                    {
                        return await resp.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    using (HttpResponseMessage resp = await PostAsync(V2, JsonConvert.SerializeObject(request), "application/json"))
                    {
                        return await resp.Content.ReadAsStringAsync();
                    }
                }
            }
            else
            {
                if (request == null)
                {
                    using (HttpResponseMessage resp = await GetAsync(V2))
                    {
                        return await resp.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    using (HttpResponseMessage resp = await GetAsync(V2, ParametersConverter.UrlEncode(request.ToKeyValuePairs())))
                    {
                        return await resp.Content.ReadAsStringAsync();
                    }
                }
            }
        }

        public async Task<V1.Response> GetResponseV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return JsonConvert.DeserializeObject<V1.Response>(await GetJsonV1Async(parameters));
        }
        public async Task<V2.Response> GetResponseV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            return JsonConvert.DeserializeObject<V2.Response>(await GetJsonV2Async(postRequest, parameters));
        }
        public async Task<V1.Response> GetResponseV1Async(V1.Request request)
        {
            return JsonConvert.DeserializeObject<V1.Response>(await GetJsonV1Async(request));
        }
        public async Task<V2.Response> GetResponseV2Async(bool postRequest = false, V2.Request request = null)
        {
            return JsonConvert.DeserializeObject<V2.Response>(await GetJsonV2Async(postRequest, request));
        }

        public override async Task<HttpResponseMessage> GetPictureAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetPictureV2Async(parameters: parameters);
        }
        public override async Task<HttpResponseMessage> GetPictureAsync()
        {
            return await GetPictureAsync(null);
        }
        public override async Task<HttpResponseMessage> GetPictureAsync(R18Type r18)
        {
            return await GetPictureAsync(new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
        }
        public async Task<HttpResponseMessage> GetPictureAsync(int num, R18Type r18, string keyword)
        {
            return await GetPictureAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()),
                new KeyValuePair<string, string>("keyword", keyword.ToString()));
        }
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
        public async Task<HttpResponseMessage> GetPictureV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return await GetAsync(await GetPictureUrlV1Async(parameters));
        }
        public async Task<HttpResponseMessage> GetPictureV1Async(V1.Request request)
        {
            return await GetAsync(await GetPictureUrlV1Async(request));
        }
        public async Task<HttpResponseMessage> GetPictureV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            return await GetAsync(await GetPictureUrlV2Async(postRequest, parameters));
        }
        public async Task<HttpResponseMessage> GetPictureV2Async(bool postRequest = false, V2.Request request = null)
        {
            return await GetAsync(await GetPictureUrlV2Async(postRequest, request));
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
        public async Task<string> GetPictureUrlAsync(R18Type r18, string keyword)
        {
            return (await GetPictureUrlsAsync(1, r18, keyword))[0];
        }
        public async Task<string> GetPictureUrlAsync(bool v2, params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPictureUrlsAsync(v2, parameters))[0];
        }
        public async Task<string> GetPictureUrlV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPictureUrlsV1Async(parameters))[0];
        }
        public async Task<string> GetPictureUrlV1Async(V1.Request request)
        {
            return (await GetPictureUrlsV1Async(request))[0];
        }
        public async Task<string> GetPictureUrlV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPictureUrlsV2Async(postRequest, parameters))[0];
        }
        public async Task<string> GetPictureUrlV2Async(bool postRequest = false, V2.Request request = null)
        {
            return (await GetPictureUrlsV2Async(postRequest, request))[0];
        }

        public override async Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetPictureUrlsV2Async(parameters: parameters);
        }
        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            return await GetPictureUrlsAsync(
                new KeyValuePair<string, string>("num", num.ToString()));
        }
        public override async Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            return await GetPictureUrlsAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
        }
        public async Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18, string keyword)
        {
            return await GetPictureUrlsAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()),
                new KeyValuePair<string, string>("keyword", keyword.ToString()));
        }
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
        public async Task<List<string>> GetPictureUrlsV1Async(params KeyValuePair<string, string>[] parameters)
        {
            V1.Response Response = await GetResponseV1Async(parameters);
            return Response.Data.Select(x => x.Url).ToList();
        }
        public async Task<List<string>> GetPictureUrlsV1Async(V1.Request request)
        {
            V1.Response Response = await GetResponseV1Async(request);
            return Response.Data.Select(x => x.Url).ToList();
        }
        public async Task<List<string>> GetPictureUrlsV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            V2.Response Response = await GetResponseV2Async(postRequest, parameters);
            return Response.Data.Select(x => x.Urls.Values.ElementAt(0)).ToList();
        }
        public async Task<List<string>> GetPictureUrlsV2Async(bool postRequest = false, V2.Request request = null)
        {
            V2.Response Response = await GetResponseV2Async(postRequest, request);
            return Response.Data.Select(x => x.Urls.Values.ElementAt(0)).ToList();
        }

        public async Task<PixivInfo> GetPixivInfoAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetMultiplePixivInfoAsync(parameters))[0];
        }
        public async Task<PixivInfo> GetPixivInfoAsync()
        {
            return (await GetMultiplePixivInfoAsync())[0];
        }
        public async Task<PixivInfo> GetPixivInfoAsync(R18Type r18)
        {
            return (await GetMultiplePixivInfoAsync(1, r18))[0];
        }
        public async Task<PixivInfo> GetPixivInfoAsync(R18Type r18, string keyword)
        {
            return (await GetMultiplePixivInfoAsync(1, r18, keyword))[0];
        }
        public async Task<PixivInfo> GetPixivInfoAsync(bool v2, params KeyValuePair<string, string>[] parameters)
        {
            return (await GetMultiplePixivInfoAsync(v2, parameters))[0];
        }
        public async Task<PixivInfo> GetPixivInfoV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetMultiplePixivInfoV1Async(parameters))[0];
        }
        public async Task<PixivInfo> GetPixivInfoV1Async(V1.Request request)
        {
            return (await GetMultiplePixivInfoV1Async(request))[0];
        }
        public async Task<PixivInfo> GetPixivInfoV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            return (await GetMultiplePixivInfoV2Async(postRequest, parameters))[0];
        }
        public async Task<PixivInfo> GetPixivInfoV2Async(bool postRequest = false, V2.Request request = null)
        {
            return (await GetMultiplePixivInfoV2Async(postRequest, request))[0];
        }

        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetMultiplePixivInfoV2Async(parameters: parameters);
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(int num = 1)
        {
            return await GetMultiplePixivInfoV1Async(new KeyValuePair<string, string>("num", num.ToString()));
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(int num, R18Type r18)
        {
            return await GetMultiplePixivInfoV1Async(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(int num, R18Type r18, string keyword)
        {
            return await GetMultiplePixivInfoV1Async(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()),
                new KeyValuePair<string, string>("keyword", keyword.ToString()));
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(bool v2, params KeyValuePair<string, string>[] parameters)
        {
            if (v2)
            {
                return await GetMultiplePixivInfoV2Async(parameters: parameters);
            }
            else
            {
                return await GetMultiplePixivInfoV1Async(parameters);
            }
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoV1Async(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetResponseV1Async(parameters)).Data.ToList();
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoV1Async(V1.Request request)
        {
            return (await GetResponseV1Async(request)).Data.ToList();
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoV2Async(bool postRequest = false, params KeyValuePair<string, string>[] parameters)
        {
            return (await GetResponseV2Async(postRequest, parameters)).Data.Select(x => new PixivInfo(x)).ToList();
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoV2Async(bool postRequest = false, V2.Request request = null)
        {
            return (await GetResponseV2Async(postRequest, request)).Data.Select(x => new PixivInfo(x)).ToList();
        }
    }
}
