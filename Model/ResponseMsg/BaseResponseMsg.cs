using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ResponseMsg
{
    public class BaseResponseMsg : IGetResponseStr
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string CreateTime { get; set; }

        public virtual string GetResponseStr()
        {
            return "";
        }
    }
}
