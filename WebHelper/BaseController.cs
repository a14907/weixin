using Common;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebHelper
{
    public class BaseController : Controller
    {
        public IEventMsgBLL EventMsgBll;
        public IImageMsgBLL ImageMsgBll;
        public ILinkMsgBLL LinkBll;
        public ILocationMsgBLL LocationMsgBll;
        public ITextMsgBLL TextMsgBll;
        public IVideoMsgBLL VideoMsgBll;
        public IVoiceMsgBLL VoiceMsgBll;

        #region 2.0 封装ajax的响应方法

        public ActionResult WriteSuccess(string msg)
        {
            return Json(new { status = (int)Enums.EAjaxStatus.success, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WriteSuccess(string msg, object data)
        {
            return Json(new { status = (int)Enums.EAjaxStatus.success, msg = msg, datas = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WriteError(string errorMsg)
        {
            return Json(new { status = (int)Enums.EAjaxStatus.error, msg = errorMsg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WriteError(Exception ex)
        {
            return Json(new { status = (int)Enums.EAjaxStatus.error, msg = ex.Message }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
