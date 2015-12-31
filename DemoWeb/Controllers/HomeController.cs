using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Common;
using DemoWeb.Models;
using Model.ResponseMsg;
using WebHelper;

namespace DemoWeb.Controllers
{
    public class HomeController : BaseController
    {
        private string _path = AppDomain.CurrentDomain.BaseDirectory;

        #region 注入
        public HomeController(IEventMsgBLL eventMsgBll, IImageMsgBLL imageMsgBll, ILinkMsgBLL linkBll, ILocationMsgBLL locationMsgBll, ITextMsgBLL textMsgBll, IVideoMsgBLL videoMsgBll, IVoiceMsgBLL voiceMsgBll)
        {
            EventMsgBll = eventMsgBll; ;
            ImageMsgBll = imageMsgBll; ;
            LinkBll = linkBll; ;
            LocationMsgBll = locationMsgBll; ;
            TextMsgBll = textMsgBll; ;
            VideoMsgBll = videoMsgBll; ;
            VoiceMsgBll = voiceMsgBll; ;
        }
        #endregion

        #region 接入验证
        [HttpGet]
        public string Index(Access model, TextMsg model2)
        {

            if (string.IsNullOrEmpty(model.Echostr) || string.IsNullOrEmpty(model.Nonce) || string.IsNullOrEmpty(model.Signature) || string.IsNullOrEmpty(model.Timestamp))
            {
                return "参数错误le !";
            }

            string token = ConfigHelper.GetValueByKey("Token");
            List<string> list = new List<string> { token, model.Timestamp, model.Nonce };
            list.Sort();
            if (EncryptHelper.Sha1Encrypt(string.Join("", list)).ToUpper().Equals(model.Signature.ToUpper()))
            {
                return model.Echostr;
            }

            return "Error";
        }
        #endregion

        #region 接收消息
        [HttpPost]
        public string Index()
        {
            StreamReader reader = new StreamReader(Request.InputStream);
            string xmlData = reader.ReadToEnd();
            BaseMsg baseModel = XmlHelper.ConvertToModel<BaseMsg>(xmlData);

            BaseResponseMsg response = new BaseResponseMsg
            {
                CreateTime = CommonHelp.TimeToLong(DateTime.Now),
                ToUserName = baseModel.FromUserName,
                FromUserName = baseModel.ToUserName
            };//响应对象

            if (baseModel.MsgType == "event")
            {
                EventMsg model = XmlHelper.ConvertToModel<EventMsg>(xmlData);
                //EventMsgBll.HandlerTheEvent(model);
                EventMsgBll.Add(model);
                EventMsgBll.SaveChanges();
                return "";
            }
            var res = (MsgType)Enum.Parse(typeof(MsgType), baseModel.MsgType);
            switch (res)
            {
                case MsgType.text:
                    {
                        TextMsg model = XmlHelper.ConvertToModel<TextMsg>(xmlData);
                        TextMsgBll.Add(model);
                        //TODO 处理用户的文字信息
                        response = new ResponseTextMsg
                        {
                            CreateTime = CommonHelp.TimeToLong(DateTime.Now),
                            ToUserName = baseModel.FromUserName,
                            FromUserName = baseModel.ToUserName,
                            Content = "欢迎关注我的微信!我是wdq!"
                        };
                    }
                    break;
                case MsgType.image:
                    {
                        ImageMsg model = XmlHelper.ConvertToModel<ImageMsg>(xmlData);
                        ImageMsgBll.Add(model);
                        //TODO 处理用户的图片信息
                    }
                    break;
                case MsgType.voice:
                    {
                        VoiceMsg model = XmlHelper.ConvertToModel<VoiceMsg>(xmlData);
                        VoiceMsgBll.Add(model);
                        if (model.Recognition != null)
                        {
                            //TODO 处理用户的语音信息
                        }
                    }
                    break;
                case MsgType.video:
                case MsgType.shortvideo:
                    {
                        VideoMsg model = XmlHelper.ConvertToModel<VideoMsg>(xmlData);
                        VideoMsgBll.Add(model);
                        //TODO 处理用户的小视频信息
                    }
                    break;
                case MsgType.location:
                    {
                        LocationMsg model = XmlHelper.ConvertToModel<LocationMsg>(xmlData);
                        LocationMsgBll.Add(model);
                        //TODO 处理用户的地理位置信息
                    }
                    break;
                case MsgType.link:
                    {
                        LinkMsg model = XmlHelper.ConvertToModel<LinkMsg>(xmlData);
                        LinkBll.Add(model);
                        //TODO 处理用户的连接信息
                    }
                    break;
            }
            string result = "";
            try
            {
                EventMsgBll.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                result = dbEx.EntityValidationErrors.SelectMany(item => item.ValidationErrors).Aggregate(result, (current, item2) => current + string.Format("{0}:{1}\r\n", item2.PropertyName, item2.ErrorMessage));

            }
            return response.ToUserName == null ? "" : response.GetResponseStr();
        }
        #endregion

        #region 获取access token
        public string GetAccessToken()
        {
            WebRequest req = WebRequest.Create("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + ConfigHelper.AppId + "&secret=" + ConfigHelper.AppSecret);
            req.Method = "GET";
            var res = req.GetResponse();
            var stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string jsonStr = reader.ReadToEnd();
            dynamic stocks = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonStr);
            reader.Close();
            stream.Close();
            res.Close();
            CommonHelp.SaveAccessToken(Path.Combine(_path, ConfigHelper.GetValueByKey("accessToken")), stocks.access_token.ToString());
            return stocks.access_token.ToString();
        }
        #endregion

        #region 获取微信服务器IP地址
        public string GetHostIp()
        {
            WebRequest req = WebRequest.Create("https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token=" + CommonHelp.GetAccessToken(Path.Combine(_path, ConfigHelper.GetValueByKey("accessToken"))));
            req.Method = "GET";
            var res = req.GetResponse();
            var stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string jsonStr = reader.ReadToEnd();
            dynamic stocks = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonStr);
            reader.Close();
            stream.Close();
            res.Close();
            return stocks.ip_list.ToString();
        }
        #endregion
    }
}
