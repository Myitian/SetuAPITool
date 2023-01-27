using Newtonsoft.Json;
using SetuAPITool.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.Anosu
{
    public class AnosuClient : SetuAPIClient, IPixivPictureClient
    {
        public const string Document = "https://docs.anosu.top";

        public static string RedirectAPI = "https://image.anosu.top/pixiv/direct";
        public static string JsonAPI = "https://image.anosu.top/pixiv/json";
        public static string UniversalAPI = "https://image.anosu.top/pixiv";

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
            return await GetJsonAsync(new Request(num, r18));
        }
        public async Task<string> GetJsonAsync(int num, R18Type r18, string keyword)
        {
            return await GetJsonAsync(new Request(num, r18, keyword));
        }
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


        public override async Task<HttpResponseMessage> GetPictureAsync(params KeyValuePair<string, string>[] parameters)
        {
            return await GetPictureAsync(new Request(parameters));
        }
        public override async Task<HttpResponseMessage> GetPictureAsync()
        {
            return await GetPictureAsync(new Request());
        }
        public override async Task<HttpResponseMessage> GetPictureAsync(R18Type r18)
        {
            return await GetPictureAsync(new Request(r18: r18));
        }
        public async Task<HttpResponseMessage> GetPictureAsync(R18Type r18, string keyword)
        {
            return await GetPictureAsync(new Request(1, r18, keyword));
        }
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

        public override async Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetPixivInfoAsync(parameters)).Url;
        }
        public override async Task<string> GetPictureUrlAsync()
        {
            return (await GetPixivInfoAsync()).Url;
        }
        public override async Task<string> GetPictureUrlAsync(R18Type r18)
        {
            return (await GetPixivInfoAsync(r18)).Url;
        }
        public async Task<string> GetPictureUrlAsync(R18Type r18, string keyword)
        {
            return (await GetPixivInfoAsync(r18, keyword)).Url;
        }
        public async Task<string> GetPictureUrlAsync(Request request)
        {
            return (await GetPixivInfoAsync(request)).Url;
        }

        public override async Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] parameters)
        {
            return (await GetMultiplePixivInfoAsync(parameters)).Select(x => x.Url).ToList(); ;
        }
        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            return (await GetMultiplePixivInfoAsync()).Select(x => x.Url).ToList(); ;
        }
        public override async Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            return (await GetMultiplePixivInfoAsync(num, r18)).Select(x => x.Url).ToList();
        }
        public async Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18, string keyword)
        {
            return (await GetMultiplePixivInfoAsync(num, r18, keyword)).Select(x => x.Url).ToList(); ;
        }
        public async Task<List<string>> GetPictureUrlsAsync(Request request)
        {
            return (await GetMultiplePixivInfoAsync(request)).Select(x => x.Url).ToList(); ;
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
        public async Task<PixivInfo> GetPixivInfoAsync(Request request)
        {
            return (await GetMultiplePixivInfoAsync(request))[0];
        }

        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(params KeyValuePair<string, string>[] parameters)
        {
            return JsonConvert.DeserializeObject<List<PixivInfo>>(await GetJsonAsync(parameters));
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(int num = 1)
        {
            return JsonConvert.DeserializeObject<List<PixivInfo>>(await GetJsonAsync(num));
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(int num, R18Type r18)
        {
            return JsonConvert.DeserializeObject<List<PixivInfo>>(await GetJsonAsync(num, r18));
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(int num, R18Type r18, string keyword)
        {
            return JsonConvert.DeserializeObject<List<PixivInfo>>(await GetJsonAsync(num, r18, keyword));
        }
        public async Task<List<PixivInfo>> GetMultiplePixivInfoAsync(Request request)
        {
            return JsonConvert.DeserializeObject<List<PixivInfo>>(await GetJsonAsync(request));
        }
    }
}
