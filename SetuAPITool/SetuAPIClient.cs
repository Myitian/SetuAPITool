using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool
{
    public abstract class SetuAPIClient : SimpleHttpClient
    {
        public SetuAPIClient() : base() { }
        public SetuAPIClient(HttpClient httpClient) : base(httpClient) { }

        public virtual string GetJson(params KeyValuePair<string, string>[] patameters)
            => GetJsonAsync(patameters).Result;
        public virtual string GetJson(int num = 1)
            => GetJsonAsync(num).Result;
        public virtual string GetPictureUrl(params KeyValuePair<string, string>[] patameters)
            => GetPictureUrlAsync(patameters).Result;
        public virtual string GetPictureUrl()
            => GetPictureUrlAsync().Result;
        public virtual List<string> GetPictureUrls(params KeyValuePair<string, string>[] patameters)
            => GetPictureUrlsAsync(patameters).Result;
        public virtual List<string> GetPictureUrls(int num = 1)
            => GetPictureUrlsAsync(num).Result;

        public abstract Task<string> GetJsonAsync(params KeyValuePair<string, string>[] patameters);
        public abstract Task<string> GetJsonAsync(int num = 1);
        public abstract Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] patameters);
        public abstract Task<string> GetPictureUrlAsync();
        public abstract Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] patameters);
        public abstract Task<List<string>> GetPictureUrlsAsync(int num = 1);
    }
}
