using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ResponseMsg
{
    public class ResponseVoiceMsg : BaseResponseMsg
    {
        public ResponseVoiceMsg()
        {

        }
        public ResponseVoiceMsg(string from)
        {
            FromUserName = from;
        }
        [JsonProperty("media_id")]
        public string MediaId { get; set; }
        public override string GetResponseStr()
        {
            return string.Format(@"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[voice]]></MsgType>
                                <Voice>
                                <MediaId><![CDATA[{3}]]></MediaId>
                                </Voice>
                                </xml>",
                                    this.ToUserName, this.FromUserName, this.CreateTime, this.MediaId);
        }

        public override string SendGuestMsg()
        {
            return "{\"touser\":\"" + FromUserName + "\",\"msgtype\":\"voice\",\"voice\":" + JsonConvert.SerializeObject(this) + "}";
        }
    }
}
