using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ResponseMsg
{
    public class ResponseNewsMsg : BaseResponseMsg
    {
        public ResponseNewsMsg()
        {

        }
        public ResponseNewsMsg(string from)
        {
            FromUserName = from;
        }
        public int ArticleCount { get; set; }
        public IEnumerable<Article> Articles { get; set; }

        private string ArticlesString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Article item in Articles)
            {
                sb.Append("<item><Title><![CDATA[").Append(item.Title).Append("]]></Title><Description><![CDATA[")
                    .Append(item.Description)
                    .Append("]]></Description><PicUrl><![CDATA[")
                    .Append(item.PicUrl)
                    .Append("]]></PicUrl><Url><![CDATA[")
                    .Append(item.Url).Append("]]></Url></item>");
            }
            return sb.ToString();
        }

        public override string GetResponseStr()
        {
            return string.Format(@"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[news]]></MsgType>
                                <ArticleCount>{3}</ArticleCount>
                                <Articles>
                                {4}
                                </Articles>
                                </xml> ",
                                this.ToUserName, this.FromUserName, this.CreateTime, this.ArticleCount, ArticlesString());
        }

        public class Article
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("description")]
            public string Description { get; set; }
            [JsonProperty("picurl")]
            public string PicUrl { get; set; }
            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public override string SendGuestMsg()
        {
            return "{\"touser\":\"" + FromUserName + "\",\"msgtype\":\"news\",\"news\":{\"articles\": " + JsonConvert.SerializeObject(this.Articles) + "}}";
        }
    }
}
