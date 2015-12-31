using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ResponseMsg
{
    public class ResponseVideoMsg : BaseResponseMsg
    {
        public string MediaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public override string GetResponseStr()
        {
            return string.Format(@"<xml>
<ToUserName><![CDATA[{0}]]></ToUserName>
<FromUserName><![CDATA[{1}]]></FromUserName>
<CreateTime>{2}</CreateTime>
<MsgType><![CDATA[image]]></MsgType>
<Video>
<MediaId><![CDATA[{3}]]></MediaId>
<Title><![CDATA[{4}]]></Title>
<Description><![CDATA[{5}]]></Description>
</Video> 
</xml>", this.ToUserName, this.FromUserName, this.CreateTime, this.MediaId, this.Title, this.Description);
        }
    }
}
