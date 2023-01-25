using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SetuAPITool
{
    public abstract class SetuAPIClient : SimpleHttpClient
    {
        public virtual bool SupportR18 => true;

        public SetuAPIClient() : base() { }
        public SetuAPIClient(HttpClient httpClient) : base(httpClient) { }

        public virtual string GetJson(params KeyValuePair<string, string>[] patameters)
            => GetJsonAsync(patameters).Result;
        public virtual string GetJson(int num = 1)
            => GetJsonAsync(num).Result;
        public virtual string GetJson(int num, R18Type r18)
            => GetJsonAsync(num, r18).Result;

        public virtual HttpContent GetPicture(params KeyValuePair<string, string>[] patameters)
            => GetPictureAsync(patameters).Result;
        public virtual HttpContent GetPicture()
            => GetPictureAsync().Result;
        public virtual HttpContent GetPicture(R18Type r18 = R18Type.NonR18)
            => GetPictureAsync(r18).Result;

        public virtual string GetPictureUrl(params KeyValuePair<string, string>[] patameters)
            => GetPictureUrlAsync(patameters).Result;
        public virtual string GetPictureUrl()
            => GetPictureUrlAsync().Result;
        public virtual string GetPictureUrl(R18Type r18 = R18Type.NonR18)
            => GetPictureUrlAsync(r18).Result;

        public virtual List<string> GetPictureUrls(params KeyValuePair<string, string>[] patameters)
            => GetPictureUrlsAsync(patameters).Result;
        public virtual List<string> GetPictureUrls(int num = 1)
            => GetPictureUrlsAsync(num).Result;
        public virtual List<string> GetPictureUrls(int num, R18Type r18)
            => GetPictureUrlsAsync(num, r18).Result;


        public abstract Task<string> GetJsonAsync(params KeyValuePair<string, string>[] patameters);
        public abstract Task<string> GetJsonAsync(int num = 1);
        public abstract Task<string> GetJsonAsync(int num, R18Type r18);

        public abstract Task<HttpContent> GetPictureAsync(params KeyValuePair<string, string>[] patameters);
        public abstract Task<HttpContent> GetPictureAsync();
        public abstract Task<HttpContent> GetPictureAsync(R18Type r18);

        public abstract Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] patameters);
        public abstract Task<string> GetPictureUrlAsync();
        public abstract Task<string> GetPictureUrlAsync(R18Type r18);

        public abstract Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] patameters);
        public abstract Task<List<string>> GetPictureUrlsAsync(int num = 1);
        public abstract Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18);
    }
}
