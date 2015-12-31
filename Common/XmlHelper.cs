using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Common
{
    public class XmlHelper
    {
        public static T ConvertToModel<T>(string xmlData) where T : class,new()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlData);
            Type type = typeof(T);
            object model = Activator.CreateInstance(type);
            var singleNode = doc.SelectSingleNode("xml");
            if (singleNode != null)
                foreach (XmlNode nodes in singleNode.ChildNodes)
                {
                    string propName = nodes.Name;
                    string propValue = nodes.InnerText;
                    PropertyInfo pInfo = type.GetProperty(propName);
                    if (pInfo == null)
                    {
                        continue;
                    }
                    pInfo.SetValue(model, propValue, null);
                }
            return model as T;
        }
    }
}
