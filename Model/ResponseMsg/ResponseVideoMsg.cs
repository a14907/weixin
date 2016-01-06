using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ResponseMsg
{
    public class ResponseVideoMsg : BaseResponseMsg
    {
        public ResponseVideoMsg()
        {

        }
        public ResponseVideoMsg(string from)
        {
            FromUserName = from;
        }
        [JsonProperty("media_id")]
        public string MediaId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("thumb_media_id")]
        private string thumb_media_id { get; set; }

        public override string GetResponseStr()
        {
            return string.Format(@"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[video]]></MsgType>
                                <Video>
                                <MediaId><![CDATA[{3}]]></MediaId>
                                <Title><![CDATA[{4}]]></Title>
                                <Description><![CDATA[{5}]]></Description>
                                </Video> 
                                </xml>",
                                    this.ToUserName, this.FromUserName, this.CreateTime, this.MediaId, this.Title, this.Description);
        }

        public override string SendGuestMsg()
        {
            return "{\"touser\":\"" + FromUserName + "\",\"msgtype\":\"video\",\"video\":" + JsonConvert.SerializeObject(this) + "}";
        }
    }
}
