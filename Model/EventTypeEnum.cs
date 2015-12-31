using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public enum EventTypeEnum
    {
        //加关注
        subscribe,
        //取消关注
        unsubscribe,
        //扫描带参数二维码事件用户已关注时的事件推送
        SCAN,
        //上报地理位置
        LOCATION,
        //自定义菜单
        CLICK,
        //点击菜单跳转链接时的事件推送
        VIEW
    }
}
