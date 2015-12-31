using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac.Integration.Mvc;

namespace DemoWeb
{
    public class ActoFacConfig
    {
        public static void Register()
        {
            var container = new ContainerBuilder();

            Assembly uiAss = Assembly.Load("DemoWeb");
            container.RegisterControllers(uiAss);

            Assembly bllAss = Assembly.Load("BLL");
            container.RegisterTypes(bllAss.GetTypes()).AsImplementedInterfaces();

            Assembly dalAss = Assembly.Load("DAL");
            container.RegisterTypes(dalAss.GetTypes()).AsImplementedInterfaces();

            var builder = container.Build();
            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(builder));
        }
    }
}