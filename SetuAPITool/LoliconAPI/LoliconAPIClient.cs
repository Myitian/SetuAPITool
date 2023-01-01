using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.LoliconAPI
{
    public class LoliconAPIClient
    {
        private HttpClient _httpClient;

        public HttpClient HttpClient
        {
            get => _httpClient;
            set => _httpClient = value ?? throw new ArgumentNullException(nameof(value));
        }

        public LoliconAPIClient() : this(null) { }

        public LoliconAPIClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
        }
    }
}
