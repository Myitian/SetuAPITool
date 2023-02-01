using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Myitian.SetuAPITool
{
    /// <summary>涩图API客户端</summary>
    public abstract class SetuAPIClient : SimpleHttpClient
    {
        /// <summary>是否支持R18</summary>
        public virtual bool SupportR18 => true;

        /// <inheritdoc cref="SimpleHttpClient()"/>
        public SetuAPIClient() : base() { }
        /// <inheritdoc cref="SimpleHttpClient(HttpClient)"/>
        public SetuAPIClient(HttpClient httpClient) : base(httpClient) { }

        #region GetJsonAsync
        /// <summary>获取JSON</summary>
        /// <param name="parameters">请求参数</param>
        /// <returns>字符串形式的JSON</returns>
        public abstract Task<string> GetJsonAsync(params KeyValuePair<string, string>[] parameters);
        /// <summary>获取JSON</summary>
        /// <param name="num">数量</param>
        /// <returns>字符串形式的JSON</returns>
        public abstract Task<string> GetJsonAsync(int num = 1);
        /// <summary>获取JSON</summary>
        /// <param name="num">数量</param>
        /// <param name="r18">R18类型</param>
        /// <returns>字符串形式的JSON</returns>
        public abstract Task<string> GetJsonAsync(int num, R18Type r18);
        #endregion GetJsonAsync

        #region GetPictureAsync
        /// <summary>获取图像</summary>
        /// <param name="parameters">请求参数</param>
        public abstract Task<HttpResponseMessage> GetPictureAsync(params KeyValuePair<string, string>[] parameters);
        /// <summary>获取图像</summary>
        public abstract Task<HttpResponseMessage> GetPictureAsync();
        /// <summary>获取图像</summary>
        /// <param name="r18">R18类型</param>
        public abstract Task<HttpResponseMessage> GetPictureAsync(R18Type r18);
        #endregion GetPictureAsync

        #region GetPictureUrlAsync
        /// <summary>获取图像URL</summary>
        /// <param name="parameters">请求参数</param>
        /// <returns>字符串形式的URL</returns>
        public abstract Task<string> GetPictureUrlAsync(params KeyValuePair<string, string>[] parameters);
        /// <summary>获取图像URL</summary>
        /// <returns>字符串形式的URL</returns>
        public abstract Task<string> GetPictureUrlAsync();
        /// <summary>获取图像URL</summary>
        /// <param name="r18">R18类型</param>
        /// <returns>字符串形式的URL</returns>
        public abstract Task<string> GetPictureUrlAsync(R18Type r18);
        #endregion GetPictureUrlAsync

        #region GetPictureUrlsAsync
        /// <summary>获取图像URL列表</summary>
        /// <param name="parameters">请求参数</param>
        /// <returns>字符串形式的URL的列表</returns>
        public abstract Task<List<string>> GetPictureUrlsAsync(params KeyValuePair<string, string>[] parameters);
        /// <summary>获取图像URL列表</summary>
        /// <param name="num">数量</param>
        /// <returns>字符串形式的URL的列表</returns>
        public abstract Task<List<string>> GetPictureUrlsAsync(int num = 1);
        /// <summary>获取图像URL列表</summary>
        /// <param name="num">数量</param>
        /// <param name="r18">R18类型</param>
        /// <returns>字符串形式的URL的列表</returns>
        public abstract Task<List<string>> GetPictureUrlsAsync(int num, R18Type r18);
        #endregion GetPictureUrlsAsync
    }
}
