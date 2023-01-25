using System;

namespace SetuAPITool.Util
{
    public class RandomUtil
    {
        protected Random _rng;
        public virtual Random RNG
        {
            get => _rng;
            set => _rng = value ?? throw new ArgumentNullException(nameof(value));
        }

        public RandomUtil() : this(null) { }
        public RandomUtil(Random rng)
        {
            _rng = rng ?? throw new ArgumentNullException(nameof(rng));
        }

        public T GetRandom<T>(params T[] values) => values[RNG.Next(values.Length)];
    }
}
