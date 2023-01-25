using Newtonsoft.Json;
using SetuAPITool.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.LoliconAPI
{
    public class LoliconAPIClient : SetuAPIClient
    {
        public const string V1 = "https://api.lolicon.app/setu/v1";
        public const string V2 = "https://api.lolicon.app/setu/v2";

        public LoliconAPIClient() : base() { }
        public LoliconAPIClient(HttpClient httpClient) : base(httpClient) { }


        public override async Task<string> GetJsonAsync(params KeyValuePair<string, string>[] patameters)
        {
            return await GetJsonAsync(true, patameters);
        }
        public override async Task<string> GetJsonAsync(int num = 1, R18Type r18 = R18Type.NonR18)
        {
            return await GetJsonAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
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
        public async Task<string> GetJsonV1Async(V1.Request patameters = null)
        {
            if (patameters == null)
            {
                return await (await GetAsync(V1)).Content.ReadAsStringAsync();
            }
            else
            {
                return await (await GetAsync(V1, ParametersConverter.URLEncode(patameters.ToKeyValuePairs()))).Content.ReadAsStringAsync();
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
        public async Task<string> GetJsonV2Async(bool postRequest = false, V2.Request patameters = null)
        {
            if (postRequest)
            {
                if (patameters == null)
                {
                    return await (await PostAsync(V2)).Content.ReadAsStringAsync();
                }
                else
                {
                    return await (await PostAsync(V2, JsonConvert.SerializeObject(patameters), "application/json")).Content.ReadAsStringAsync();
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
                    return await (await GetAsync(V2, ParametersConverter.URLEncode(patameters.ToKeyValuePairs()))).Content.ReadAsStringAsync();
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
        public async Task<V1.Respond> GetRespondV1Async(V1.Request patameters)
        {
            return JsonConvert.DeserializeObject<V1.Respond>(await GetJsonV1Async(patameters));
        }
        public async Task<V2.Respond> GetRespondV2Async(bool postRequest = false, V2.Request patameters = null)
        {
            return JsonConvert.DeserializeObject<V2.Respond>(await GetJsonV2Async(postRequest, patameters));
        }

        public override async Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] patameters)
        {
            return await GetPictureUrlV2Async(patameters: patameters);
        }
        public override async Task<string> GetPictureUrlAsync(R18Type r18 = R18Type.NonR18)
        {
            return await GetPictureUrlAsync(new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
        }
        public async Task<string> GetPictureUrlAsync(bool v2, params KeyValuePair<string, string>[] patameters)
        {
            if (v2)
            {
                return await GetPictureUrlV2Async(patameters: patameters);
            }
            else
            {
                return await GetPictureUrlV1Async(patameters);
            }
        }
        public async Task<string> GetPictureUrlV1Async(params KeyValuePair<string, string>[] patameters)
        {
            return (await GetPictureUrlsV1Async(patameters))[0];
        }
        public async Task<string> GetPictureUrlV1Async(V1.Request patameters)
        {
            return (await GetPictureUrlsV1Async(patameters))[0];
        }
        public async Task<string> GetPictureUrlV2Async(bool postRequest = false, params KeyValuePair<string, string>[] patameters)
        {
            return (await GetPictureUrlsV2Async(postRequest, patameters))[0];
        }
        public async Task<string> GetPictureUrlV2Async(bool postRequest = false, V2.Request patameters = null)
        {
            return (await GetPictureUrlsV2Async(postRequest, patameters))[0];
        }

        public override async Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] patameters)
        {
            return await GetPictureUrlsV2Async(patameters: patameters);
        }
        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1, R18Type r18 = R18Type.NonR18)
        {
            return await GetPictureUrlsAsync(
                new KeyValuePair<string, string>("num", num.ToString()),
                new KeyValuePair<string, string>("r18", ((int)r18).ToString()));
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
        public async Task<List<string>> GetPictureUrlsV1Async(V1.Request patameters)
        {
            V1.Respond respond = await GetRespondV1Async(patameters);
            return respond.Data.Select(x => x.Url).ToList();
        }
        public async Task<List<string>> GetPictureUrlsV2Async(bool postRequest = false, params KeyValuePair<string, string>[] patameters)
        {
            V2.Respond respond = await GetRespondV2Async(postRequest, patameters);
            return respond.Data.Select(x => x.Urls.Values.ElementAt(0)).ToList();
        }
        public async Task<List<string>> GetPictureUrlsV2Async(bool postRequest = false, V2.Request patameters = null)
        {
            V2.Respond respond = await GetRespondV2Async(postRequest, patameters);
            return respond.Data.Select(x => x.Urls.Values.ElementAt(0)).ToList();
        }
    }
}
