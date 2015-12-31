using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Enums
    {
        public enum EAjaxStatus
        {
            /// <summary>
            /// 成功
            /// </summary>
            success = 0,
            /// <summary>
            /// 错误
            /// </summary>
            error = 1,
            /// <summary>
            /// 未登录
            /// </summary>
            nologin = 2,
            /// <summary>
            /// 没有权限
            /// </summary>
            nopermiss = 3
        }
    }
}
