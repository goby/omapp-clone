using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;
using System.Data;
using Oracle.DataAccess.Client;

namespace OperatingManagement.DataAccessLayer.PlanManage
{
    /// <summary>
    /// 设备工作计划
    /// </summary>
    public class SBJH
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
        /// <summary>
        /// 编号
        /// </summary>
        public string Sequence { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string DateTime { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// 设备代号
        /// </summary>
        public string EquipmentID { get; set; }
        /// <summary>
        /// 任务个数
        /// </summary>
        public string TaskCount { get; set; }

        public List<SBJH_Task> SBJHTasks { get; set; }
        #endregion
    }

    /// <summary>
    /// 设备工作计划-任务
    /// </summary>
    [Serializable]
    public class SBJH_Task
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
        /// 网管开始时间
        /// </summary>
        public string NetCenterStartTime { get; set; }
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

        public List<SBJH_Task_ReakTimeTransfor> ReakTimeTransfors { get; set; }
        public List<SBJH_Task_AfterFeedBack> AfterFeedBacks { get; set; }
        public List<SBJH_Task_DataReSend> DataReSends { get; set; }
        #endregion
    }

    /// <summary>
    /// 设备工作计划-实时传输
    /// </summary>
    [Serializable]
    public class SBJH_Task_ReakTimeTransfor
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
    /// 设备工作计划-事后回放
    /// </summary>
    [Serializable]
    public class SBJH_Task_AfterFeedBack
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

    /// <summary>
    /// 设备工作计划-数据重发
    /// </summary>
    [Serializable]
    public class SBJH_Task_DataReSend
    {
        #region -Properties-
        /// <summary>
        /// 文件标志
        /// </summary>
        public string FileFlag { get; set; }
        /// <summary>
        /// 数据起始时间
        /// </summary>
        public string DataStartTime { get; set; }
        /// <summary>
        /// 数据结束时间
        /// </summary>
        public string DataEndTime { get; set; }
        /// <summary>
        /// 重发文件开始时间
        /// </summary>
        public string FileReSendStartTime { get; set; }
        /// <summary>
        /// 文件传输速率
        /// </summary>
        public string FileTransSpeedRate { get; set; }

        #endregion
    }
}
