using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.MirlKoi
{
    internal class MirlKoiClient : ISetuAPIClient
    {
        public string[] URLs = {
            "https://iw233.cn/api.php",
            "https://api.iw233.cn/api.php",
            "https://ap1.iw233.cn/api.php",
            "https://dev.iw233.cn/api.php",
            "https://mirlkoi.ifast3.vipnps.vip/api.php"
        };

        private HttpClient _httpClient;

        public HttpClient HttpClient
        {
            get => _httpClient;
            set => _httpClient = value ?? throw new ArgumentNullException(nameof(value));
        }

        public HttpResponseMessage GetJson(int num = 1, R18Type r18 = R18Type.NonR18)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetJsonAsync(int num = 1, R18Type r18 = R18Type.NonR18)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage GetPicture(R18Type r18 = R18Type.NonR18)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetPictureAsync(R18Type r18 = R18Type.NonR18)
        {
            throw new NotImplementedException();
        }
    }
}
