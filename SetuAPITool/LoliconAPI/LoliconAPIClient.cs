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


        public override async Task<string> GetJsonAsync(params KeyValuePair<string, string>[] patameters)
        {
            return await GetJsonAsync(true, patameters);
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
        public async Task<string> GetJsonAsync(bool v2 = true, params KeyValuePair<string, string>[] patameters)
        {
            return await (v2 ? GetJsonV2Async(patameters: patameters) : GetJsonV1Async(patameters));
        }
        public async Task<string> GetJsonV1Async(params KeyValuePair<string, string>[] patameters)
        {
            if (patameters == null)
            {
                return await (await GetAsync(V1)).Content.ReadAsStringAsync();
            }
            else
            {
                return await (await GetAsync(V1, ParametersConverter.URLEncode(patameters))).Content.ReadAsStringAsync();
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
                return await (await GetAsync(V1, ParametersConverter.URLEncode(request.ToKeyValuePairs()))).Content.ReadAsStringAsync();
            }
        }
        public async Task<string> GetJsonV2Async(bool postRequest = false, params KeyValuePair<string, string>[] patameters)
        {
            if (postRequest)
            {
                if (patameters == null)
                {
                    return await (await PostAsync(V2)).Content.ReadAsStringAsync();
                }
                else
                {
                    V2.Request request = new V2.Request(patameters);
                    return await (await PostAsync(V2, JsonConvert.SerializeObject(request), "application/json")).Content.ReadAsStringAsync();
                }
            }
            else
            {
                if (patameters == null)
                {
                    return await (await GetAsync(V2)).Content.ReadAsStringAsync();
                }
                else
                {
                    return await (await GetAsync(V2, ParametersConverter.URLEncode(patameters))).Content.ReadAsStringAsync();
                }
            }
        }
        public async Task<string> GetJsonV2Async(bool postRequest = false, V2.Request request = null)
        {
            if (postRequest)
            {
                if (request == null)
                {
                    return await (await PostAsync(V2)).Content.ReadAsStringAsync();
                }
                else
                {
                    return await (await PostAsync(V2, JsonConvert.SerializeObject(request), "application/json")).Content.ReadAsStringAsync();
                }
            }
            else
            {
                if (request == null)
                {
                    return await (await GetAsync(V2)).Content.ReadAsStringAsync();
                }
                else
                {
                    return await (await GetAsync(V2, ParametersConverter.URLEncode(request.ToKeyValuePairs()))).Content.ReadAsStringAsync();
                }
            }
        }

        public async Task<V1.Respond> GetRespondV1Async(params KeyValuePair<string, string>[] patameters)
        {
            return JsonConvert.DeserializeObject<V1.Respond>(await GetJsonV1Async(patameters));
        }
        public async Task<V2.Respond> GetRespondV2Async(bool postRequest = false, params KeyValuePair<string, string>[] patameters)
        {
            return JsonConvert.DeserializeObject<V2.Respond>(await GetJsonV2Async(postRequest, patameters));
        }
        public async Task<V1.Respond> GetRespondV1Async(V1.Request request)
        {
            return JsonConvert.DeserializeObject<V1.Respond>(await GetJsonV1Async(request));
        }
        public async Task<V2.Respond> GetRespondV2Async(bool postRequest = false, V2.Request request = null)
        {
            return JsonConvert.DeserializeObject<V2.Respond>(await GetJsonV2Async(postRequest, request));
        }

        public override async Task<HttpContent> GetPictureAsync(params KeyValuePair<string, string>[] patameters)
        {
            return await GetPictureV2Async(patameters: patameters);
        }
        public override async Task<HttpContent> GetPictureAsync()
        {
            return await GetPictureAsync(null);
        }
        public override async Task<HttpContent> GetPictureAsync(R18Type r18)
        {
            return await GetPictureAsync(new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
        }
        public async Task<HttpContent> GetPictureAsync(int num, R18Type r18, string keyword)
        {
            return await GetPictureAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()),
                new KeyValuePair<string, string>("keyword", keyword.ToString()));
        }
        public async Task<HttpContent> GetPictureAsync(bool v2, params KeyValuePair<string, string>[] patameters)
        {
            if (v2)
            {
                return await GetPictureV2Async(patameters: patameters);
            }
            else
            {
                return await GetPictureV1Async(patameters);
            }
        }
        public async Task<HttpContent> GetPictureV1Async(params KeyValuePair<string, string>[] patameters)
        {
            return (await GetAsync(await GetPictureUrlV1Async(patameters))).Content;
        }
        public async Task<HttpContent> GetPictureV1Async(V1.Request request)
        {
            return (await GetAsync(await GetPictureUrlV1Async(request))).Content;
        }
        public async Task<HttpContent> GetPictureV2Async(bool postRequest = false, params KeyValuePair<string, string>[] patameters)
        {
            return (await GetAsync(await GetPictureUrlV2Async(postRequest, patameters))).Content;
        }
        public async Task<HttpContent> GetPictureV2Async(bool postRequest = false, V2.Request request = null)
        {
            return (await GetAsync(await GetPictureUrlV2Async(postRequest, request))).Content;
        }

        public override async Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] patameters)
        {
            return (await GetPictureUrlsAsync(patameters))[0];
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
        public async Task<string> GetPictureUrlAsync(bool v2, params KeyValuePair<string, string>[] patameters)
        {
            return (await GetPictureUrlsAsync(v2, patameters))[0];
        }
        public async Task<string> GetPictureUrlV1Async(params KeyValuePair<string, string>[] patameters)
        {
            return (await GetPictureUrlsV1Async(patameters))[0];
        }
        public async Task<string> GetPictureUrlV1Async(V1.Request request)
        {
            return (await GetPictureUrlsV1Async(request))[0];
        }
        public async Task<string> GetPictureUrlV2Async(bool postRequest = false, params KeyValuePair<string, string>[] patameters)
        {
            return (await GetPictureUrlsV2Async(postRequest, patameters))[0];
        }
        public async Task<string> GetPictureUrlV2Async(bool postRequest = false, V2.Request request = null)
        {
            return (await GetPictureUrlsV2Async(postRequest, request))[0];
        }

        public override async Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] patameters)
        {
            return await GetPictureUrlsV2Async(patameters: patameters);
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
        public async Task<List<string>> GetPictureUrlsAsync(bool v2, params KeyValuePair<string, string>[] patameters)
        {
            if (v2)
            {
                return await GetPictureUrlsV2Async(patameters: patameters);
            }
            else
            {
                return await GetPictureUrlsV1Async(patameters);
            }
        }
        public async Task<List<string>> GetPictureUrlsV1Async(params KeyValuePair<string, string>[] patameters)
        {
            V1.Respond respond = await GetRespondV1Async(patameters);
            return respond.Data.Select(x => x.Url).ToList();
        }
        public async Task<List<string>> GetPictureUrlsV1Async(V1.Request request)
        {
            V1.Respond respond = await GetRespondV1Async(request);
            return respond.Data.Select(x => x.Url).ToList();
        }
        public async Task<List<string>> GetPictureUrlsV2Async(bool postRequest = false, params KeyValuePair<string, string>[] patameters)
        {
            V2.Respond respond = await GetRespondV2Async(postRequest, patameters);
            return respond.Data.Select(x => x.Urls.Values.ElementAt(0)).ToList();
        }
        public async Task<List<string>> GetPictureUrlsV2Async(bool postRequest = false, V2.Request request = null)
        {
            V2.Respond respond = await GetRespondV2Async(postRequest, request);
            return respond.Data.Select(x => x.Urls.Values.ElementAt(0)).ToList();
        }

        public async Task<PixivInfo> GetPixivInfoAsync(params KeyValuePair<string, string>[] patameters)
        {
            return (await GetMultiplePixivInfoAsync(patameters))[0];
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
        public async Task<PixivInfo> GetPixivInfoAsync(bool v2, params KeyValuePair<string, string>[] patameters)
        {
            return (await GetMultiplePixivInfoAsync(v2, patameters))[0];
        }
        public async Task<PixivInfo> GetPixivInfoV1Async(params KeyValuePair<string, string>[] patameters)
        {
            return (await GetMultiplePixivInfoV1Async(patameters))[0];
        }
        public async Task<PixivInfo> GetPixivInfoV1Async(V1.Request request)
        {
            return (await GetMultiplePixivInfoV1Async(request))[0];
        }
        public async Task<PixivInfo> GetPixivInfoV2Async(bool postRequest = false, params KeyValuePair<string, string>[] patameters)
        {
            return (await GetMultiplePixivInfoV2Async(postRequest, patameters))[0];
        }
        public async Task<PixivInfo> GetPixivInfoV2Async(bool postRequest = false, V2.Request request = null)
        {
            return (await GetMultiplePixivInfoV2Async(postRequest, request))[0];
        }

        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(params KeyValuePair<string, string>[] patameters)
        {
            return await GetMultiplePixivInfoV2Async(patameters: patameters);
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
        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(bool v2, params KeyValuePair<string, string>[] patameters)
        {
            if (v2)
            {
                return await GetMultiplePixivInfoV2Async(patameters: patameters);
            }
            else
            {
                return await GetMultiplePixivInfoV1Async(patameters);
            }
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoV1Async(params KeyValuePair<string, string>[] patameters)
        {
            return (await GetRespondV1Async(patameters)).Data.ToList();
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoV1Async(V1.Request request)
        {
            return (await GetRespondV1Async(request)).Data.ToList();
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoV2Async(bool postRequest = false, params KeyValuePair<string, string>[] patameters)
        {
            return (await GetRespondV2Async(postRequest, patameters)).Data.Select(x => new PixivInfo(x)).ToList();
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoV2Async(bool postRequest = false, V2.Request request = null)
        {
            return (await GetRespondV2Async(postRequest, request)).Data.Select(x => new PixivInfo(x)).ToList();
        }
    }
}
