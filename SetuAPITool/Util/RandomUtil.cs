using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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

        public T GetRandom<T>(T[] values) => values[RNG.Next(values.Length)];

    }
}
