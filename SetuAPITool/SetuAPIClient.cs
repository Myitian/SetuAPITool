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

        public abstract Task<string> GetJsonAsync(params KeyValuePair<string, string>[] parameters);
        public abstract Task<string> GetJsonAsync(int num = 1);
        public abstract Task<string> GetJsonAsync(int num, R18Type r18);

        public abstract Task<HttpResponseMessage> GetPictureAsync(params KeyValuePair<string, string>[] parameters);
        public abstract Task<HttpResponseMessage> GetPictureAsync();
        public abstract Task<HttpResponseMessage> GetPictureAsync(R18Type r18);

        public abstract Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] parameters);
        public abstract Task<string> GetPictureUrlAsync();
        public abstract Task<string> GetPictureUrlAsync(R18Type r18);

        public abstract Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] parameters);
        public abstract Task<List<string>> GetPictureUrlsAsync(int num = 1);
        public abstract Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18);
    }
}
