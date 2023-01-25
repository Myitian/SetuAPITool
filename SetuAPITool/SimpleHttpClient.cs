using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SetuAPITool
{
    public abstract class SimpleHttpClient
    {
        protected HttpClient _httpClient;
        public virtual HttpClient HttpClient
        {
            get => _httpClient;
            set => _httpClient = value ?? throw new ArgumentNullException(nameof(value));
        }

        public SimpleHttpClient() : this(null) { }

        public SimpleHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
        }

        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }
        public HttpResponseMessage Get(string url, string args)
        {
            return GetAsync(url, args).Result;
        }
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }
        public async Task<HttpResponseMessage> GetAsync(string url, string args)
        {
            return await _httpClient.GetAsync(url + "?" + args);
        }

        public HttpResponseMessage Post(string url, string body, string mime)
        {
            return PostAsync(url, body, mime).Result;
        }
        public HttpResponseMessage Post(string url)
        {
            return PostAsync(url).Result;
        }
        public async Task<HttpResponseMessage> PostAsync(string url, string body, string mime)
        {
            using (StringContent content = new StringContent(body))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(mime);
                return await _httpClient.PostAsync(url, content);
            }
        }
        public async Task<HttpResponseMessage> PostAsync(string url)
        {
            using (StringContent content = new StringContent(""))
            {
                return await _httpClient.PostAsync(url, content);
            }
        }
    }
}
