using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool
{
    public interface ISetuAPIClient
    {
        HttpClient HttpClient { get; set; }

        HttpResponseMessage GetPicture(R18Type r18 = R18Type.NonR18);
        HttpResponseMessage GetJson(int num = 1, R18Type r18 = R18Type.NonR18);
        Task<HttpResponseMessage> GetPictureAsync(R18Type r18 = R18Type.NonR18);
        Task<HttpResponseMessage> GetJsonAsync(int num = 1, R18Type r18 = R18Type.NonR18);
    }
}
