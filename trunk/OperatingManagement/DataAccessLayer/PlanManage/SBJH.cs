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
        public string Sequence { get; set; }
        public string DateTime { get; set; }
        public string StationName { get; set; }
        public string EquipmentID { get; set; }
        public string TaskCount { get; set; }

        public List<SBJH_Task> SBJHTasks { get; set; }
        #endregion
    }

    [Serializable]
    public class SBJH_Task
    {
        #region -Properties-

        public string TaskFlag { get; set; }
        public string WorkWay { get; set; }
        public string PlanPropertiy { get; set; }
        public string WorkMode { get; set; }
        public string NetCenterStartTime { get; set; }
        public string PreStartTime { get; set; }
        public string StartTime { get; set; }
        public string TrackStartTime { get; set; }
        public string WaveOnStartTime { get; set; }
        public string WaveOffStartTime { get; set; }
        public string TrackEndTime { get; set; }
        public string EndTime { get; set; }

        public List<SBJH_Task_ReakTimeTransfor> ReakTimeTransfors { get; set; }
        public List<SBJH_Task_AfterFeedBack> AfterFeedBacks { get; set; }
        public List<SBJH_Task_DataReSend> DataReSends { get; set; }
        #endregion
    }

    [Serializable]
    public class SBJH_Task_ReakTimeTransfor
    {
        #region -Properties-

        public string FormatFlag { get; set; }
        public string InfoFlowFlag { get; set; }
        public string TransStartTime { get; set; }
        public string TransEndTime { get; set; }
        public string TransSpeedRate { get; set; }

        #endregion
    }

    [Serializable]
    public class SBJH_Task_AfterFeedBack
    {
        #region -Properties-

        public string FormatFlag { get; set; }
        public string InfoFlowFlag { get; set; }
        public string DataStartTime { get; set; }
        public string DataEndTime { get; set; }
        public string TransStartTime { get; set; }
        public string TransSpeedRate { get; set; }

        #endregion
    }

    [Serializable]
    public class SBJH_Task_DataReSend
    {
        #region -Properties-

        public string FileFlag { get; set; }
        public string DataStartTime { get; set; }
        public string DataEndTime { get; set; }
        public string FileReSendStartTime { get; set; }
        public string FileTransSpeedRate { get; set; }

        #endregion
    }
}
