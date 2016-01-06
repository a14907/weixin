using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ResponseMsg
{
    public class ResponseImageMsg : BaseResponseMsg
    {
        public ResponseImageMsg()
        {

        }
        public ResponseImageMsg(string from)
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
                                <MsgType><![CDATA[image]]></MsgType>
                                <Image>
                                <MediaId><![CDATA[{3}]]></MediaId>
                                </Image>
                                </xml>", this.ToUserName, this.FromUserName, this.CreateTime, this.MediaId);
        }

        public override string SendGuestMsg()
        {
            return "{\"touser\":\"" + FromUserName + "\",\"msgtype\":\"image\",\"image\":" + JsonConvert.SerializeObject(this) + "}";
        }
    }
}
