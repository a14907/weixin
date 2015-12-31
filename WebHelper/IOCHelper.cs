using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebHelper
{
    public class IOCHelper
    {
        public static T GetIOCObj<T>()
        {
            var container = DependencyResolver.Current.GetService<AutofacDependencyResolver>();
            return container.GetService<T>();
        }
    }
}
