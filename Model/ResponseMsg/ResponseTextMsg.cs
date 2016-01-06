using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ResponseMsg
{
    public class ResponseTextMsg : BaseResponseMsg
    {
        public ResponseTextMsg()
        {

        }
        public ResponseTextMsg(string from)
        {
            FromUserName = from;
        }
        [JsonProperty("content")]
        public string Content { get; set; }

        public override string GetResponseStr()
        {
            return string.Format(@"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[text]]></MsgType>
                                <Content><![CDATA[{3}]]></Content>
                                </xml>",
                                this.ToUserName, this.FromUserName, this.CreateTime, this.Content);
        }

        public override string SendGuestMsg()
        {
            return "{\"touser\":\"" + FromUserName + "\",\"msgtype\":\"text\",\"text\":" + JsonConvert.SerializeObject(this) + "}";
        }
    }
}
