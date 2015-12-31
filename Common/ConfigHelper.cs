using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Common
{
    public class ConfigHelper
    {
        public static string GetValueByKey(string key)
        {
            string value = ConfigurationManager.AppSettings[key] == null ? "" : ConfigurationManager.AppSettings[key].ToString();
            return value;
        }

        public static string ToKenStr()
        {
            return GetValueByKey("Token");
        }
        public static string EncodingAesKeyStr()
        {
            return GetValueByKey("Token");
        }

        public static string AppId
        {
            get
            {
                return GetValueByKey("appId");
            }
        }

        public static string AppSecret
        {
            get
            {
                return GetValueByKey("appSecret");
            }
        }

    }
}
