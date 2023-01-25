using SetuAPITool.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SetuAPITool.Jitsu
{
    public class JitsuClient : SetuAPIClient
    {
        public const string Document = "https://img.jitsu.top/";

        static string[] _anosu = {
            "https://moe.anosu.top/img/",
            "https://moe.anosu.top/api/",
            "https://api.anosu.top/img/",
            "https://api.anosu.top/api/"
        };
        static string[] _jitsu = {
            "https://moe.jitsu.top/img/",
            "https://moe.jitsu.top/api/",
            "https://api.jitsu.top/img/",
            "https://api.jitsu.top/api/"
        };
        static string[] _all = _anosu.Concat(_jitsu).ToArray();

        static string[] _specialR18anosu = {
            "https://moe.anosu.top/r18/",
            "https://api.anosu.top/r18/"
        };
        static string[] _specialR18jitsu = {
            "https://moe.jitsu.top/r18/",
            "https://api.jitsu.top/r18/"
        };
        static string[] _specialR18all = _specialR18anosu.Concat(_specialR18jitsu).ToArray();

        protected RandomUtil _randomUtil;
        public virtual RandomUtil RandomUtil
        {
            get => _randomUtil;
            set => _randomUtil = value ?? throw new ArgumentNullException(nameof(value));
        }

        public JitsuClient() : this(null, null) { }
        public JitsuClient(HttpClient httpClient) : this(httpClient, null) { }
        public JitsuClient(HttpClient httpClient, RandomUtil random) : base(httpClient)
        {
            _randomUtil = random ?? new RandomUtil();
        }

        public override Task<string> GetJsonAsync(params KeyValuePair<string, string>[] patameters)
        {
            throw new System.NotImplementedException();
        }
        public override Task<string> GetJsonAsync(int num = 1)
        {
            throw new System.NotImplementedException();
        }
        public override Task<string> GetJsonAsync(int num, R18Type r18)
        {
            throw new System.NotImplementedException();
        }

        public override Task<HttpContent> GetPictureAsync(params KeyValuePair<string, string>[] patameters)
        {
            throw new System.NotImplementedException();
        }
        public override Task<HttpContent> GetPictureAsync()
        {
            throw new System.NotImplementedException();
        }
        public override Task<HttpContent> GetPictureAsync(R18Type r18)
        {
            throw new System.NotImplementedException();
        }

        public override Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] patameters)
        {
            throw new System.NotImplementedException();
        }
        public override Task<string> GetPictureUrlAsync()
        {
            throw new System.NotImplementedException();
        }
        public override Task<string> GetPictureUrlAsync(R18Type r18)
        {
            throw new System.NotImplementedException();
        }

        public override Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] patameters)
        {
            throw new System.NotImplementedException();
        }
        public override Task<List<string>> GetPictureUrlsAsync(int num = 1)
        {
            throw new System.NotImplementedException();
        }
        public override Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18)
        {
            throw new System.NotImplementedException();
        }
    }
}
