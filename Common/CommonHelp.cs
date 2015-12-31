using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class CommonHelp
    {
        /// <summary>
        /// 保存获取到的access_token
        /// </summary>
        /// <param name="path"></param>
        /// <param name="access_token"></param>
        public static void SaveAccessToken(string path, string access_token)
        {
            System.IO.File.WriteAllText(path, access_token);
        }

        public static string GetAccessToken(string path)
        {
            return System.IO.File.ReadAllText(path);
        }

        #region 将微信的long格式的时间转换为日期
        public static DateTime FormatTime(String createTime)
        {

            // 将微信传入的CreateTime转换成long类型，再乘以1000  

            long totalSeconds = long.Parse(createTime);
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0);

            return sTime.AddSeconds(totalSeconds).AddHours(8);
        }
        #endregion

        #region 将时间转换为微信的long形式

        public static string TimeToLong(DateTime time)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan span = time.AddHours(-8) - sTime;
            return ((long)span.TotalSeconds).ToString();
        }
        #endregion
    }
}
