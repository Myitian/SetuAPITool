using System.Collections.Generic;
using System.Threading.Tasks;

namespace SetuAPITool
{
    public interface IPixivPictureClient
    {
        Task<PixivInfo> GetPixivInfoAsync(params KeyValuePair<string, string>[] patameters);
        Task<PixivInfo> GetPixivInfoAsync();
        Task<PixivInfo> GetPixivInfoAsync(R18Type r18);

        Task<List<PixivInfo>> GetMultiplePixivInfoAsync(params KeyValuePair<string, string>[] patameters);
        Task<List<PixivInfo>> GetMultiplePixivInfoAsync(int num = 1);
        Task<List<PixivInfo>> GetMultiplePixivInfoAsync(int num, R18Type r18);
    }
}
