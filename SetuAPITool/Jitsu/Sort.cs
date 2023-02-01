namespace Myitian.SetuAPITool.Jitsu
{
    /// <summary>图片的分类</summary>
    public enum Sort
    {
        /// <summary>默认</summary>
        Default,

        /// <summary>全部图片</summary>
        All,
        /// <summary>手机壁纸</summary>
        MP,
        /// <summary>桌面壁纸</summary>
        PC,
        /// <summary>1920x1080</summary>
        _1080p,
        /// <summary>银发</summary>
        Silver,
        /// <summary>兽耳（这里多有歧义，我英文取的有问题，不要在意细节）</summary>
        Furry,
        /// <summary>星空</summary>
        Starry,
        /// <summary>涩图（不漏）</summary>
        Setu,
        /// <summary>白丝</summary>
        WS,

        /// <summary>p站图（不含18+）</summary>
        Pixiv = 0x10,
        /// <summary>顾名思义</summary>
        R18,
        /// <summary>我的p站收藏（不含18+）</summary>
        Jitsu,

        /// <summary>特殊的r18调用</summary>
        SpR18 = 0x20
    }
}
