namespace Myitian.SetuAPITool.LoliconAPI.V1
{
    /// <summary>返回码</summary>
    public enum Code
    {
        /// <summary>成功</summary>
        OK = 0,
        /// <summary>找不到符合关键字的色图</summary>
        NotFound = 404,
        /// <summary>内部错误，请向 <see href="mailto:i@loli.best"/> 反馈</summary>
        InternalError = -1
    }
}
