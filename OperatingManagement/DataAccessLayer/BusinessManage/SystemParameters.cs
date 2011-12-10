using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

using OperatingManagement.Framework.Core;

namespace OperatingManagement.DataAccessLayer.BusinessManage
{
    /// <summary>
    /// 参数类型
    /// </summary>
    public enum SystemParametersType
    {
        TaskList = 1,
        InfoSource = 2,
        InfoType = 3,
        Ddestination = 4
    }
    [Serializable]
    public class SystemParameters
    {
        /// <summary>
        /// 选择下拉列表参数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetSystemParameters(SystemParametersType type)
        {
            string filePath = GlobalSettings.MapPath(string.Format(AspNetConfig.Config["settingPattern"].ToString(), @"SystemParameters"));
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            string xmlPath = string.Format(@"//{0}/item", type.ToString());
            XmlNodeList xmlNodeList = doc.SelectNodes(xmlPath);

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (xmlNodeList != null)
            {
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    if (!dictionary.ContainsKey(xmlNode.Attributes["text"].Value) && !dictionary.ContainsValue(xmlNode.Attributes["value"].Value))
                    {
                        dictionary.Add(xmlNode.Attributes["text"].Value, xmlNode.Attributes["value"].Value);
                    }
                }
            }
            return dictionary;
        }
    }

}
