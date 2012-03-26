using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;
using System.Data;
using Oracle.DataAccess.Client;

namespace OperatingManagement.DataAccessLayer.PlanManage
{
    /// <summary>
    /// 地面站工作计划
    /// </summary>
    public class DMJH
    {
        #region -Properties-
        public int ID { get; set; }
        public DateTime CTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string TaskID { get; set; }
        public string InfoType { get; set; }
        public int LineCount { get; set; }
        public string Format1 { get; set; }
        public string Format2 { get; set; }
        public string DataSection { get; set; }
        public string FileIndex { get; set; }
        public string Reserve { get; set; }

        public string SatID { get; set; }
        public string Sequence { get; set; }
        public string DateTime { get; set; }
        public string StationName { get; set; }
        public string EquipmentID { get; set; }
        public string TaskCount { get; set; }

        public List<DMJH_Task> DMJHTasks { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">1:xml;3:DATA</param>
        /// <returns></returns>
        protected override string ToString(int format)
        {
            int iSYCount = 0;
            XDocument doc = new XDocument();
            doc.Add(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("注：地面站工作计划。"));
            XElement root = new XElement("地面站工作计划");
            root.Add(
                new XElement("编号", this.Sequence),
                new XComment("从1开始编号，用4位字符标识，范围为0001～9999"),
                new XElement("时间", DateTime.Now.ToString("YYYYMMDDHHMMSS")),
                new XComment("为信源生成该文件时的北京时日期和时间（24时制）"));
            if (this.SYJH_SY_List != null)
                iSYCount = this.SYJH_SY_List.Count;
            root.Add(new XElement("试验个数"), iSYCount);
            XElement xSY = new XElement("试验");
            if (iSYCount > 0)
            {
                int iIdx = 1;
                foreach (SYJH_SY sy in this.SYJH_SY_List)
                {
                    xSY.Add(new XElement("试验-" + iIdx.ToString()),
                                new XElement("卫星名称", sy.SYSatName),
                                new XComment("对于本任务填写“探索三号卫星”、“探索四号卫星”或“探索五号卫星"),
                                new XElement("试验类别", sy.SYType),
                                new XComment("根据卫星填写"),
                                new XElement("试验项目", sy.SYType),
                                new XComment("根据试验类别填写"),
                                new XElement("开始时间", sy.SYStartTime),
                                new XComment("为试验开始时的北京时日期和时间（24时制）"),
                                new XElement("结束时间", sy.SYEndTime),
                                new XComment("为试验结束时的北京时日期和时间（24时制）"),
                                new XElement("系统名称", sy.SYSysName),
                                new XComment("系统名称"),
                                new XElement("系统任务", sy.SYSysTask),
                                new XComment("系统任务"));

                    iIdx++;
                }
            }
            root.Add(xSY);
            doc.Add(root);
            return doc.ToString();
        }
    }

    [Serializable]
    public class DMJH_Task
    {
        #region -Properties-
        /// <summary>
        /// 任务标志
        /// </summary>
        public string TaskFlag { get; set; }
        /// <summary>
        /// 工作方式
        /// </summary>
        public string WorkWay { get; set; }
        /// <summary>
        /// 计划性质
        /// </summary>
        public string PlanPropertiy { get; set; }
        /// <summary>
        /// 工作模式
        /// </summary>
        public string WorkMode { get; set; }
        /// <summary>
        /// 任务准备开始时间
        /// </summary>
        public string PreStartTime { get; set; }
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 跟踪开始时间
        /// </summary>
        public string TrackStartTime { get; set; }
        /// <summary>
        /// 开上行载波时间
        /// </summary>
        public string WaveOnStartTime { get; set; }
        /// <summary>
        /// 关上行载波时间
        /// </summary>
        public string WaveOffStartTime { get; set; }
        /// <summary>
        /// 跟踪结束时间
        /// </summary>
        public string TrackEndTime { get; set; }
        /// <summary>
        /// 任务结束时间
        /// </summary>
        public string EndTime { get; set; }

        public List<DMJH_Task_ReakTimeTransfor> ReakTimeTransfors { get; set; }
        public List<DMJH_Task_AfterFeedBack> AfterFeedBacks { get; set; }
        #endregion
    }

    /// <summary>
    /// 地面站工作计划-实时传输
    /// </summary>
    [Serializable]
    public class DMJH_Task_ReakTimeTransfor
    {
        #region -Properties-
        /// <summary>
        /// 格式标志
        /// </summary>
        public string FormatFlag { get; set; }
        /// <summary>
        /// 信息流标志
        /// </summary>
        public string InfoFlowFlag { get; set; }
        /// <summary>
        /// 数据传输开始时间
        /// </summary>
        public string TransStartTime { get; set; }
        /// <summary>
        /// 数据传输结束时间
        /// </summary>
        public string TransEndTime { get; set; }
        /// <summary>
        /// 数据传输速率
        /// </summary>
        public string TransSpeedRate { get; set; }

        #endregion
    }

    /// <summary>
    /// 地面站工作计划-事后回放
    /// </summary>
    [Serializable]
    public class DMJH_Task_AfterFeedBack
    {
        #region -Properties-
        /// <summary>
        /// 格式标志
        /// </summary>
        public string FormatFlag { get; set; }
        /// <summary>
        /// 信息流标志
        /// </summary>
        public string InfoFlowFlag { get; set; }
        /// <summary>
        /// 数据起始时间
        /// </summary>
        public string DataStartTime { get; set; }
        /// <summary>
        /// 数据结束时间
        /// </summary>
        public string DataEndTime { get; set; }
        /// <summary>
        /// 数据传输开始时间
        /// </summary>
        public string TransStartTime { get; set; }
        /// <summary>
        /// 数据传输速率
        /// </summary>
        public string TransSpeedRate { get; set; }

        #endregion
    }
}
