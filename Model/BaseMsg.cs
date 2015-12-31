using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model
{
    public class BaseMsg
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string CreateTime { get; set; }
        public string MsgType { get; set; }
    }
}
