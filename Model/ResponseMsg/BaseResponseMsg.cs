using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ResponseMsg
{
    public class BaseResponseMsg : IGetResponseStr
    {
        [JsonIgnore]
        public string ToUserName { get; set; }
        [JsonIgnore]
        public string FromUserName { get; set; }
        [JsonIgnore]
        public string CreateTime { get; set; }

        public virtual string GetResponseStr()
        {
            return "";
        }

        public virtual string SendGuestMsg()
        {
            return "";
        }
    }
}
