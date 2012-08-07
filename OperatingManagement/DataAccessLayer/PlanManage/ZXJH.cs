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
    /// 中心计划
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
        /// <summary>
        /// 计划对应日期
        /// </summary>
        public string Date { get; set; }

        //试验计划SY

        /// <summary>
        /// 对应日期的试验个数
        /// </summary>
        public string SYCount { get; set; }

        /// <summary>
        /// 试验计划-试验内容
        /// </summary>
        public List<ZXJH_SYContent> SYContents { get; set; }

        //工作计划
        /// <summary>
        /// 任务管理-工作内容
        /// </summary>
        public List<ZXJH_WorkContent> WorkContents { get; set; }
        /// <summary>
        /// 指令制作
        /// </summary>
        public List<ZXJH_CommandMake> CommandMakes { get; set; }
        /// <summary>
        /// 实时试验数据处理
        /// </summary>
        public List<ZXJH_SYDataHandle> SYDataHandles { get; set; }
        /// <summary>
        /// 指挥与监视
        /// </summary>
        public List<ZXJH_DirectAndMonitor> DirectAndMonitors { get; set; }
        /// <summary>
        /// 实时控制
        /// </summary>
        public List<ZXJH_RealTimeControl> RealTimeControls { get; set; }
        /// <summary>
        /// 处理评估
        /// </summary>
        public List<ZXJH_SYEstimate> SYEstimates { get; set; }
        #endregion
    }

    /// <summary>
    /// 试验计划-试验内容
    /// </summary>
    [Serializable]
    public class ZXJH_SYContent
    {
        /// <summary>
        /// 卫星代号
        /// </summary>
        public string SatID { get; set; }
        #region 试验
        /// <summary>
        /// 在当日计划中的ID
        /// </summary>
        public string SYID { get; set; }
        /// <summary>
        /// 试验项目名称
        /// </summary>
        public string SYName { get; set; }
        /// <summary>
        /// 试验开始时间
        /// </summary>
        public string SYStartTime { get; set; }
        /// <summary>
        /// 试验结束时间
        /// </summary>
        public string SYEndTime { get; set; }
        /// <summary>
        /// 试验运行的天数
        /// </summary>
        public string SYDays { get; set; }
        /// <summary>
        /// 试验-说明
        /// </summary>
        public string SYNote { get; set; }
        #endregion

        #region 数传
        /// <summary>
        /// 数传-站编号
        /// </summary>
        public string SY_SCStationNO { get; set; }
        /// <summary>
        /// 数传-设备编号
        /// </summary>
        public string SY_SCEquipmentNO { get; set; }
        /// <summary>
        /// 数传-频段; S或者X
        /// </summary>
        public string SY_SCFrequencyBand { get; set; }
        /// <summary>
        /// 数传-圈次
        /// </summary>
        public string SY_SCLaps { get; set; }
        /// <summary>
        /// 数传-开始时间
        /// </summary>
        public string SY_SCStartTime { get; set; }
        /// <summary>
        /// 数传-结束时间
        /// </summary>
        public string SY_SCEndTime { get; set; }
        #endregion

        #region 测控
        /// <summary>
        /// 测控-站编号
        /// </summary>
        public string SY_CKStationNO { get; set; }
        /// <summary>
        /// 测控-设备编号
        /// </summary>
        public string SY_CKEquipmentNO { get; set; }
        /// <summary>
        /// 测控-圈次
        /// </summary>
        public string SY_CKLaps { get; set; }
        /// <summary>
        /// 测控-开始时间
        /// </summary>
        public string SY_CKStartTime { get; set; }
        /// <summary>
        /// 测控-结束时间
        /// </summary>
        public string SY_CKEndTime { get; set; }
        #endregion

        #region 注数-(700任务不需填写)
        /// <summary>
        /// 注数-最早时间要求
        /// </summary>
        public string SY_ZSFirst { get; set; }
        /// <summary>
        /// 注数-最晚时间要求
        /// </summary>
        public string SY_ZSLast { get; set; }
        /// <summary>
        /// 注数-主要内容
        /// </summary>
        public string SY_ZSContent { get; set; }
        #endregion
    }

    /// <summary>
    /// 工作计划-任务管理
    /// </summary>
    [Serializable]
    public class ZXJH_WorkContent 
    { 
        #region -Properties-
        /// <summary>
        /// 工作: 试验规划、计划管理、试验数据处理
        /// </summary>
        public string Work { get; set; }
        /// <summary>
        /// 对应试验ID
        /// </summary>
        public string SYID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 最短持续时间
        /// </summary>
        public string MinTime { get; set; }
        /// <summary>
        /// 最长持续时间
        /// </summary>
        public string MaxTime { get; set; }
        #endregion
    }

    /// <summary>
    /// 工作计划-指令制作
    /// </summary>
    [Serializable]
    public class ZXJH_CommandMake
    {
        /// <summary>
        /// 指令制作-卫星代号
        /// </summary>
        public string Work_Command_SatID { get; set; }
        /// <summary>
        /// 指令制作-对应试验ID
        /// </summary>
        public string Work_Command_SYID { get; set; }
        /// <summary>
        /// 指令制作-对应控制程序
        /// </summary>
        public string Work_Command_Programe { get; set; }
        /// <summary>
        /// 指令制作-完成时间
        /// </summary>
        public string Work_Command_FinishTime { get; set; }
        /// <summary>
        /// 指令制作-上注方式
        /// </summary>
        public string Work_Command_UpWay { get; set; }
        /// <summary>
        /// 指令制作-上注时间/圈次
        /// </summary>
        public string Work_Command_UpTime { get; set; }
        /// <summary>
        /// 指令制作-说明
        /// </summary>
        public string Work_Command_Note { get; set; }
    }

    /// <summary>
    /// 工作计划-实时试验数据处理
    /// </summary>
    [Serializable]
    public class ZXJH_SYDataHandle
    {
        #region -Properties-
        /// <summary>
        /// 对应试验ID
        /// </summary>
        public string SYID { get; set; }
        /// <summary>
        /// 卫星代号
        /// </summary>
        public string SatID { get; set; }
        /// <summary>
        /// 圈次
        /// </summary>
        public string Laps { get; set; }
        /// <summary>
        /// 主站设备
        /// </summary>
        public string MainStationEquipment { get; set; }
        /// <summary>
        /// 备站设备
        /// </summary>
        public string BakStationEquipment { get; set; }
        /// <summary>
        /// 工作内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 实时开始处理时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 实时结束处理时间
        /// </summary>
        public string EndTime { get; set; }
        #endregion
    }

    /// <summary>
    /// 工作计划-指挥与监视
    /// </summary>
    [Serializable]
    public class ZXJH_DirectAndMonitor
    {
        #region -Properties-
        /// <summary>
        /// 对应试验ID
        /// </summary>
        public string SYID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 实时演示任务：有/无
        /// </summary>
        public string RealTimeDemoTask { get; set; }
        #endregion
    }

    /// <summary>
    /// 工作计划-实时控制
    /// </summary>
    [Serializable]
    public class ZXJH_RealTimeControl
    {
        #region -Properties-
        /// <summary>
        /// 工作
        /// </summary>
        public string Work { get; set; }
        /// <summary>
        /// 对应试验ID
        /// </summary>
        public string SYID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        #endregion
    }

    /// <summary>
    /// 工作计划-处理评估
    /// </summary>
    [Serializable]
    public class ZXJH_SYEstimate
    {
        #region -Properties-
        /// <summary>
        /// 对应试验ID
        /// </summary>
        public string SYID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public string EndTime { get; set; }
        #endregion
    }

}
