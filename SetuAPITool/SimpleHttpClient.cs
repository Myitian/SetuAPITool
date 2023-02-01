using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Myitian.SetuAPITool
{
    /// <summary>简单HTTP客户端</summary>
    public class SimpleHttpClient
    {
        /// <summary>HTTP客户端</summary>
        protected HttpClient _httpClient;
        /// <summary>HTTP客户端</summary>
        public virtual HttpClient HttpClient
        {
            get => _httpClient;
            set => _httpClient = value ?? throw new ArgumentNullException(nameof(value));
        }

        ///
        public SimpleHttpClient() : this(null) { }
        /// <param name="httpClient">HTTP客户端</param>
        public SimpleHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
        }

        /// <summary>同步 GET</summary>
        /// <param name="url">请求的 URL</param>
        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }
        /// <summary>同步 GET</summary>
        /// <param name="url">请求的 URL</param>
        /// <param name="param">请求的查询参数</param>
        public HttpResponseMessage Get(string url, string param)
        {
            return GetAsync(url, param).Result;
        }
        /// <summary>异步 GET</summary>
        /// <param name="url">请求的 URL</param>
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }
        /// <summary>异步 GET</summary>
        /// <param name="url">请求的 URL</param>
        /// <param name="param">请求的查询参数</param>
        public async Task<HttpResponseMessage> GetAsync(string url, string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return await GetAsync(url);
            }
            else
            {
                return await _httpClient.GetAsync(url + "?" + param);
            }
        }

        /// <summary>同步 POST</summary>
        /// <param name="url">请求的 URL</param>
        /// <param name="body">请求正文</param>
        /// <param name="mime">媒体类型</param>
        public HttpResponseMessage Post(string url, string body, string mime)
        {
            return PostAsync(url, body, mime).Result;
        }
        /// <summary>同步 POST</summary>
        /// <param name="url">请求的 URL</param>
        public HttpResponseMessage Post(string url)
        {
            return PostAsync(url).Result;
        }
        /// <summary>同步 POST</summary>
        /// <param name="url">请求的 URL</param>
        /// <param name="param">请求的查询参数</param>
        public HttpResponseMessage Post(string url, string param)
        {
            return PostAsync(url, param).Result;
        }
        /// <summary>异步 POST</summary>
        /// <param name="url">请求的 URL</param>
        /// <param name="body">请求正文</param>
        /// <param name="mime">媒体类型</param>
        public async Task<HttpResponseMessage> PostAsync(string url, string body, string mime)
        {
            using (StringContent content = new StringContent(body))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(mime);
                return await _httpClient.PostAsync(url, content);
            }
        }
        /// <summary>异步 POST</summary>
        /// <param name="url">请求的 URL</param>
        public async Task<HttpResponseMessage> PostAsync(string url)
        {
            using (StringContent content = new StringContent(""))
            {
                return await _httpClient.PostAsync(url, content);
            }
        }
        /// <summary>异步 POST</summary>
        /// <param name="url">请求的 URL</param>
        /// <param name="param">请求的查询参数</param>
        public async Task<HttpResponseMessage> PostAsync(string url, string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return await PostAsync(url);
            }
            else
            {
                using (StringContent content = new StringContent(""))
                {
                    return await _httpClient.PostAsync(url + "?" + param, content);
                }
            }
        }
    }
}
