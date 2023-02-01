using System;

namespace Myitian.SetuAPITool.Util
{
    /// <summary>随机工具</summary>
    public class RandomUtil
    {
        /// <summary>随机数生成器</summary>
        protected Random _rng;
        /// <summary>随机数生成器</summary>
        public virtual Random RNG
        {
            get => _rng;
            set => _rng = value ?? throw new ArgumentNullException(nameof(value));
        }

        ///
        public RandomUtil() : this(null) { }
        /// <summary>从随机数生成器创建随机工具</summary>
        public RandomUtil(Random rng)
        {
            _rng = rng ?? throw new ArgumentNullException(nameof(rng));
        }

        /// <summary>获取列表中随机项</summary>
        public T GetRandom<T>(params T[] values) => values[RNG.Next(values.Length)];
    }
}
