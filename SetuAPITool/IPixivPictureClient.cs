using System.Collections.Generic;
using System.Threading.Tasks;

namespace Myitian.SetuAPITool
{
    /// <summary>可获取Pixiv图像信息的客户端</summary>
    public interface IPixivPictureClient
    {
        #region GetPixivInfoAsync
        /// <summary>获取Pixiv图像信息</summary>
        /// <param name="parameters">请求参数</param>
        /// <returns>一个 <see cref="PixivInfo"/>，包含所请求的Pixiv图像信息</returns>
        Task<PixivInfo> GetPixivInfoAsync(params KeyValuePair<string, string>[] parameters);
        /// <summary>获取Pixiv图像信息</summary>
        /// <returns>一个 <see cref="PixivInfo"/>，包含所请求的Pixiv图像信息</returns>
        Task<PixivInfo> GetPixivInfoAsync();
        /// <summary>获取Pixiv图像信息</summary>
        /// <param name="r18">R18类型</param>
        /// <returns>一个 <see cref="PixivInfo"/>，包含所请求的Pixiv图像信息</returns>
        Task<PixivInfo> GetPixivInfoAsync(R18Type r18);
        #endregion GetPixivInfoAsync

        #region GetPixivInfoListAsync
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="parameters">请求参数</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        Task<List<PixivInfo>> GetPixivInfoListAsync(params KeyValuePair<string, string>[] parameters);
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="num">图像数量</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        Task<List<PixivInfo>> GetPixivInfoListAsync(int num = 1);
        /// <summary>获取Pixiv图像信息列表</summary>
        /// <param name="num">图像数量</param>
        /// <param name="r18">R18类型</param>
        /// <returns>一个 <see cref="List{T}"/>，包含所请求的Pixiv图像信息</returns>
        Task<List<PixivInfo>> GetPixivInfoListAsync(int num, R18Type r18);
        #endregion GetPixivInfoListAsync
    }
}
