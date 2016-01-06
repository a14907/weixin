
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ResponseMsg
{
    public class ResponseMusicMsg : BaseResponseMsg
    {
        public ResponseMusicMsg()
        {

        }
        public ResponseMusicMsg(string from)
        {
            FromUserName = from;
        }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("musicurl")]
        public string MusicUrl { get; set; }
        [JsonProperty("hqmusicurl")]
        public string HQMusicUrl { get; set; }
        [JsonProperty("thumb_media_id")]
        public string ThumbMediaId { get; set; }
        public override string GetResponseStr()
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[music]]></MsgType>
                                    <Music>
                                    <Title><![CDATA[{3}]]></Title>
                                    <Description><![CDATA[{4}]]></Description>
                                    <MusicUrl><![CDATA[{5}]]></MusicUrl>
                                    <HQMusicUrl><![CDATA[{6}]]></HQMusicUrl>
                                    <ThumbMediaId><![CDATA[{7}]]></ThumbMediaId>
                                    </Music>
                                    </xml>",
                                    this.ToUserName, this.FromUserName, this.CreateTime, this.Title, this.Description, this.MusicUrl, this.HQMusicUrl, this.ThumbMediaId);
        }

        public override string SendGuestMsg()
        {
            return "{\"touser\":\"" + FromUserName + "\",\"msgtype\":\"music\",\"music\":" + JsonConvert.SerializeObject(this) + "}";
        }
    }
}
