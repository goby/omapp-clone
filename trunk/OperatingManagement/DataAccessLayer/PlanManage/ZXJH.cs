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
    /// 中心运行计划
    /// </summary>
    public class ZXJH 
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
        public string Date { get; set; }
        //实验内容SY
        public string SYCount { get; set; }

        public string SYID { get; set; }
        public string SYName { get; set; }
        public string SYDateTime{ get; set; }
        public string SYDays{ get; set; }
        public string SYLoadStartTime{ get; set; }
        public string SYLoadEndTime { get; set; }
        public string SYLoadContent { get; set; }
        //数传
        public string SY_SCLaps { get; set; }
        public string SY_SCStartTime { get; set; }
        public string SY_SCEndTime { get; set; }
        //测控
        public string SY_CKLaps { get; set; }
        public string SY_CKStartTime { get; set; }
        public string SY_CKEndTime { get; set; }
        //注数
        public string SY_ZSFirst { get; set; }
        public string SY_ZSLast { get; set; }
        public string SY_ZSContent { get; set; }

        //工作计划
        //工作内容
        public List<ZXJH_WorkContent> WorkContents { get; set; }
        //载荷管理
        public string Work_Load_SYID { get; set; }
        public string Work_Load_SatID { get; set; }
        public string Work_Load_Process { get; set; }
        public string Work_Load_Event { get; set; }
        public string Work_Load_Action { get; set; }
        public string Work_Load_StartTime { get; set; }
        public string Work_Load_EndTime { get; set; }
        //指令制作
        public string Work_Command_SYID { get; set; }
        public string Work_Command_SYItem { get; set; }
        public string Work_Command_SatID { get; set; }
        public string Work_Command_Content { get; set; }
        public string Work_Command_UpRequire { get; set; }
        public string Work_Command_Direction { get; set; }
        public string Work_Command_SpecialRequire { get; set; }
        //试验数据处理
        public List<ZXJH_SYDataHandle> SYDataHandles { get; set; }
        //指挥与监视
        public List<ZXJH_DirectAndMonitor> DirectAndMonitors { get; set; }
        //实时控制
        public List<ZXJH_RealTimeControl> RealTimeControls { get; set; }
        //试验评估
        public List<ZXJH_SYEstimate> SYEstimates { get; set; }
        //数据管理
        public List<ZXJH_DataManage> DataManages { get; set; }
        #endregion
    }

    /// <summary>
    /// 工作内容
    /// </summary>
    [Serializable]
    public class ZXJH_WorkContent 
    { 
        #region -Properties-
        public string Work { get; set; }
        public string SYID { get; set; }
        public string StartTime { get; set; }
        public string MinTime { get; set; }
        public string MaxTime { get; set; }
        #endregion
    }

    /// <summary>
    /// 试验数据处理
    /// </summary>
    [Serializable]
    public class ZXJH_SYDataHandle
    {
        #region -Properties-
        public string SYID { get; set; }
        public string SatID { get; set; }
        public string Laps { get; set; }
        public string MainStationName { get; set; }
        public string BakStationName { get; set; }
        public string Content { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string AfterWardsDataHandle { get; set; }
        #endregion
    }

    /// <summary>
    /// 指挥与监视
    /// </summary>
    [Serializable]
    public class ZXJH_DirectAndMonitor
    {
        #region -Properties-
        public string SYID { get; set; }
        public string DateSection { get; set; }
        public string Task { get; set; }
        public string RealTimeShowTask { get; set; }
        #endregion
    }

    /// <summary>
    /// 实时控制
    /// </summary>
    [Serializable]
    public class ZXJH_RealTimeControl
    {
        #region -Properties-
        public string Work { get; set; }
        public string SYID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        #endregion
    }

    /// <summary>
    /// 试验评估
    /// </summary>
    [Serializable]
    public class ZXJH_SYEstimate
    {
        #region -Properties-
        public string SYID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        #endregion
    }

    /// <summary>
    /// 数据管理
    /// </summary>
    [Serializable]
    public class ZXJH_DataManage
    {
        #region -Properties-
        public string Work { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        #endregion
    }
}
