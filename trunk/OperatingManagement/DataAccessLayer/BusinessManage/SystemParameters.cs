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
        CenterOutputPolicyTaskList = 1,
        CenterOutputPolicyInfoSource = 2,
        CenterOutputPolicyInfoType = 3,
        CenterOutputPolicyDdestination = 4,
        ResourceType = 5,
        ResourceStatus = 6,
        GroundResourceOwner = 7,
        GroundResourceCoordinate = 8,
        GroundResourceFunctionType = 9,
        CommunicationResourceDirection = 10,
        CenterResourceEquipmentType = 11,
        StatusType = 12,
        HealthStatus =13,
        HealthStatusFunctionType = 14,
        UseStatusUsedType = 15,
        UseStatusCanBeUsed = 16
    }

    [Serializable]
    public class SystemParameters
    {
        /// <summary>
        /// 根据参数类型获得参数列表
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

        /// <summary>
        /// 根据参数值获得参数文本
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSystemParameterText(SystemParametersType type, string value)
        {
            string filePath = GlobalSettings.MapPath(string.Format(AspNetConfig.Config["settingPattern"].ToString(), @"SystemParameters"));
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            string xmlPath = string.Format(@"//{0}/item", type.ToString());
            XmlNodeList xmlNodeList = doc.SelectNodes(xmlPath);

            string text = string.Empty;
            if (xmlNodeList != null)
            {
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    if (xmlNode.Attributes["value"].Value == value)
                    {
                        text = xmlNode.Attributes["text"].Value;
                        break;
                    }
                }
            }
            return text;
        }
    }

}
