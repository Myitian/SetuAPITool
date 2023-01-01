using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.Util
{
    public static class TimeConvert
    {
        /// <summary>Unix时间戳起始秒</summary>
        public const long UnixTimeStartSecond = 62135596800;
        /// <summary>Unix时间戳起始毫秒</summary>
        public const long UnixTimeStartMillisecond = 62135596800000;
        /// <summary>Unix时间戳起始刻</summary>
        public const long UnixTimeStartTick = 621355968000000000;
        /// <summary>刻每毫秒</summary>
        public const long TicksPerMillisecond = 10000;
        /// <summary>刻每秒</summary>
        public const long TicksPerSecond = 10000000;
        /// <summary>将DateTime转换为Unix时间戳（刻）</summary>
        /// <param name="dateTime">要转换的DateTime</param>
        /// <returns>Unix时间戳（刻）</returns>
        public static long ToUnixTimeTick(DateTime dateTime)
            => dateTime.Ticks - UnixTimeStartTick;
        /// <summary>将DateTime转换为Unix时间戳（毫秒）</summary>
        /// <param name="dateTime">要转换的DateTime</param>
        /// <returns>Unix时间戳（毫秒）</returns>
        public static long ToUnixTimeMillisecond(DateTime dateTime)
            => (dateTime.Ticks - UnixTimeStartTick) / TicksPerMillisecond;
        /// <summary>将DateTime转换为Unix时间戳（秒）</summary>
        /// <param name="dateTime">要转换的DateTime</param>
        /// <returns>Unix时间戳（秒）</returns>
        public static long ToUnixTimeSecond(DateTime dateTime)
            => (dateTime.Ticks - UnixTimeStartTick) / TicksPerSecond;
        /// <summary>将Unix时间戳（刻）转换为DateTime</summary>
        /// <param name="unixTime">要转换的Unix时间戳（刻）</param>
        /// <returns>DateTime</returns>
        public static DateTime FromUnixTimeTick(long unixTime)
            => new DateTime(unixTime + UnixTimeStartTick);
        /// <summary>将Unix时间戳（毫秒）转换为DateTime</summary>
        /// <param name="unixTime">要转换的Unix时间戳（毫秒）</param>
        /// <returns>DateTime</returns>
        public static DateTime FromUnixTimeMillisecond(long unixTime)
            => new DateTime(unixTime * TicksPerMillisecond + UnixTimeStartTick);
        /// <summary>将Unix时间戳（秒）转换为DateTime</summary>
        /// <param name="unixTime">要转换的Unix时间戳（秒）</param>
        /// <returns>DateTime</returns>
        public static DateTime FromUnixTimeSecond(long unixTime)
            => new DateTime(unixTime * TicksPerSecond + UnixTimeStartTick);
    }
}
