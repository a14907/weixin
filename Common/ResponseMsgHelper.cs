using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ResponseMsg;

namespace Common
{
    public class ResponseMsgHelper<T> where T : IGetResponseStr
    {
        public static string GetResponseMsgHelper(T item)
        {
            return item.GetResponseStr();
        }
    }
}
