using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.Anosu
{
    public class AnosuClient : SetuAPIClient, IPixivPictureClient
    {
        public const string Document = "https://docs.anosu.top";

        public const string RedirectAPI = "https://image.anosu.top/pixiv/direct";
        public const string JsonAPI = "https://image.anosu.top/pixiv/json";
        public const string UniversalAPI = "https://image.anosu.top/pixiv";

        public override Task<string> GetJsonAsync(params KeyValuePair<string, string>[] patameters)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetJsonAsync(int num = 1)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetJsonAsync(int num, R18Type r18)
        {
            throw new NotImplementedException();
        }

        public override Task<HttpContent> GetPictureAsync(params KeyValuePair<string, string>[] patameters)
        {
            throw new NotImplementedException();
        }

        public override Task<HttpContent> GetPictureAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<HttpContent> GetPictureAsync(R18Type r18)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] patameters)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetPictureUrlAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetPictureUrlAsync(R18Type r18)
        {
            throw new NotImplementedException();
        }

        public override Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] patameters)
        {
            throw new NotImplementedException();
        }

        public override Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            throw new NotImplementedException();
        }

        public override Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            throw new NotImplementedException();
        }

        public Task<PixivInfo> GetPixivInfoAsync(params KeyValuePair<string, string>[] patameters)
        {
            throw new NotImplementedException();
        }

        public Task<PixivInfo> GetPixivInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PixivInfo> GetPixivInfoAsync(R18Type r18)
        {
            throw new NotImplementedException();
        }

        public Task<List<PixivInfo>> GetMultiplePixivInfoAsync(params KeyValuePair<string, string>[] patameters)
        {
            throw new NotImplementedException();
        }

        public Task<List<PixivInfo>> GetMultiplePixivInfoAsync(int num = 1)
        {
            throw new NotImplementedException();
        }

        public Task<List<PixivInfo>> GetMultiplePixivInfoAsync(int num, R18Type r18)
        {
            throw new NotImplementedException();
        }
    }
}
