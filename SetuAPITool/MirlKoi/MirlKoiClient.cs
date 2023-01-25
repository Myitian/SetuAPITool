using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.MirlKoi
{
    internal class MirlKoiClient : SetuAPIClient
    {
        public string[] URLs = {
            "https://iw233.cn/api.php",
            "https://api.iw233.cn/api.php",
            "https://ap1.iw233.cn/api.php",
            "https://dev.iw233.cn/api.php",
            "https://mirlkoi.ifast3.vipnps.vip/api.php"
        };

        public override async Task<HttpResponseMessage> GetJsonAsync(int num = 1, R18Type r18 = R18Type.NonR18)
        {
            _httpClient.GetAsync()
        }

        public override async Task<HttpResponseMessage> GetPictureUrlAsync(R18Type r18 = R18Type.NonR18)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<string>> GetPictureUrlsAsync(int num = 1, R18Type r18 = R18Type.NonR18)
        {
            throw new NotImplementedException();
        }
    }
}
