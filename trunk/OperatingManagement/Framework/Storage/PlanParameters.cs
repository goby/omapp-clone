using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatingManagement.Framework.Components;
using OperatingManagement.Framework.Cache;
using System.Xml.Linq;
using OperatingManagement.Framework.Core;

namespace OperatingManagement.Framework.Storage
{
    /// <summary>
    /// Orbit parameters collections.
    /// </summary>
    public class PlanParameters
    {
        const string _CacheKey = "Setting/PlanParameters";
        const string _FullFileName = "~/app_data/PlanParameters.xml";
        /// <summary>
        /// Read plan parameters data from xml file
        /// </summary>
        /// <param name="elementname">
        /// MBXQInfoName:空间目标信息需求产品名称; 
        /// HJXQInfoName:空间环境信息需求产品名称;
        /// DMJHFS:地面计划工作方式;
        /// DMJHJXZ:地面计划计划性质;
        /// DMJHMS:地面计划设备工作模式;
        /// DMJHBID:地面计划信息类别标志;
        /// TYSJSatName:仿真推演试验数据卫星名称;
        /// YDSJTargetList:引导数据发送目标列表;
        /// </param>
        /// <returns></returns>
        public static List<PlanParameter> ReadParameters(string elementname)
        {
            if (AspNetCache.Instance.Get(_CacheKey) != null)
                return AspNetCache.Instance.Get(_CacheKey) as List<PlanParameter>;
            string fullFileName = GlobalSettings.MapPath(_FullFileName);
            FileDependency fd = new FileDependency(fullFileName);
            XElement xe = XElement.Load(fullFileName);
            try
            {
                List<PlanParameter> items = (from q in xe.Elements(elementname)
                             from q1 in q.Elements("item")
                             select new PlanParameter()
                             {
                                 Text = q1.Attribute("text").Value,
                                 Value = q1.Attribute("value").Value
                                 //Hex = q1.Attribute("hex").Value
                             }).ToList();
                AspNetCache.Instance.Insert(_CacheKey, items, fd);
                return items;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 空间目标信息需求默认用户名称
        /// </summary>
        /// <returns></returns>
        public static string ReadMBXQDefaultUser()
        {
            string fullFileName = GlobalSettings.MapPath(_FullFileName);
            XElement xe = XElement.Load(fullFileName);
            string item;
            try
            {
                item = xe.Element("MBXQDefaultUser").Value;
                return item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 空间目标信息需求默认信息标志
        /// </summary>
        /// <returns></returns>
        public static string ReadMBXQDefaultTargetInfo()
        {
            string fullFileName = GlobalSettings.MapPath(_FullFileName);
            XElement xe = XElement.Load(fullFileName);
            string item;
            try
            {
                item = xe.Element("MBXQDefaultTargetInfo").Value;
                return item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 空间环境信息需求默认用户名称
        /// </summary>
        /// <returns></returns>
        public static string ReadHJXQDefaultUser()
        {
            string fullFileName = GlobalSettings.MapPath(_FullFileName);
            XElement xe = XElement.Load(fullFileName);
            string item;
            try
            {
                item = xe.Element("HJXQDefaultUser").Value;
                return item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 空间环境信息需求默认信息标志
        /// </summary>
        /// <returns></returns>
        public static string ReadHJXQHJXQDefaultEnvironInfo()
        {
            string fullFileName = GlobalSettings.MapPath(_FullFileName);
            XElement xe = XElement.Load(fullFileName);
            string item;
            try
            {
                item = xe.Element("HJXQDefaultEnvironInfo").Value;
                return item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 应用研究工作计划计划序号(用4位整型数表示)
        /// </summary>
        /// <returns></returns>
        public static string ReadYJJHJXH()
        {
            string fullFileName = GlobalSettings.MapPath(_FullFileName);
            XElement xe = XElement.Load(fullFileName);
            string item;
            try
            {
                item = xe.Element("YJJHJXH").Value;
                return item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 测控资源使用申请-任务类别
        /// </summary>
        /// <returns></returns>
        public static string ReadDJZYSQMLB()
        {
            string fullFileName = GlobalSettings.MapPath(_FullFileName);
            XElement xe = XElement.Load(fullFileName);
            string item;
            try
            {
                item = xe.Element("DJZYSQMLB").Value;
                return item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 测控资源使用申请-圈标
        /// </summary>
        /// <returns></returns>
        public static string ReadDJZYSQQB()
        {
            string fullFileName = GlobalSettings.MapPath(_FullFileName);
            XElement xe = XElement.Load(fullFileName);
            string item;
            try
            {
                item = xe.Element("DJZYSQQB").Value;
                return item;
            }
            catch
            {
                return null;
            }
        }

    }
}
