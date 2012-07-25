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
        /// 日期
        /// </summary>
        public string Date { get; set; }

        //实验内容SY

        /// <summary>
        /// 对应日期的试验个数
        /// </summary>
        public string SYCount { get; set; }
        /// <summary>
        /// 在当日计划中的ID
        /// </summary>
        public string SYID { get; set; }
        /// <summary>
        /// 试验项目名称
        /// </summary>
        public string SYName { get; set; }
        /// <summary>
        /// 试验开始日期及时间
        /// </summary>
        public string SYDateTime{ get; set; }
        /// <summary>
        /// 试验运行的天数
        /// </summary>
        public string SYDays{ get; set; }
        /// <summary>
        /// 载荷-载荷名称
        /// </summary>
        public string SYLoadName { get; set; }
        /// <summary>
        /// 载荷-开始时间
        /// </summary>
        public string SYLoadStartTime{ get; set; }
        /// <summary>
        /// 载荷-结束时间
        /// </summary>
        public string SYLoadEndTime { get; set; }
        /// <summary>
        /// 载荷-动作内容
        /// </summary>
        public string SYLoadContent { get; set; }

        //数传
        /// <summary>
        /// 数传-站编号
        /// </summary>
        public string SY_SCStationNO { get; set; }
        /// <summary>
        /// 数传-设备编号
        /// </summary>
        public string SY_SCEquipmentNO { get; set; }
        /// <summary>
        /// 数传-频段
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

        //测控
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

        //注数
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

        //工作计划
        //任务管理-工作内容
        public List<ZXJH_WorkContent> WorkContents { get; set; }
        //载荷管理
        /// <summary>
        /// 载荷管理-对应试验ID
        /// </summary>
        public string Work_Load_SYID { get; set; }
        /// <summary>
        /// 载荷管理-卫星代号
        /// </summary>
        public string Work_Load_SatID { get; set; }
        /// <summary>
        /// 载荷管理-载荷名称
        /// </summary>
        public string Work_Load_Name { get; set; }
        /// <summary>
        /// 载荷管理-进程
        /// </summary>
        public string Work_Load_Process { get; set; }
        /// <summary>
        /// 载荷管理-事件
        /// </summary>
        public string Work_Load_Event { get; set; }
        /// <summary>
        /// 载荷管理-动作
        /// </summary>
        public string Work_Load_Action { get; set; }
        /// <summary>
        /// 载荷管理-开始时间
        /// </summary>
        public string Work_Load_StartTime { get; set; }
        /// <summary>
        /// 载荷管理-结束时间
        /// </summary>
        public string Work_Load_EndTime { get; set; }

        //指令制作
        /// <summary>
        /// 指令制作-对应试验ID
        /// </summary>
        public string Work_Command_SYID { get; set; }
        /// <summary>
        /// 指令制作-试验项目
        /// </summary>
        public string Work_Command_SYItem { get; set; }
        /// <summary>
        /// 指令制作-卫星代号
        /// </summary>
        public string Work_Command_SatID { get; set; }
        /// <summary>
        /// 指令制作-作业
        /// </summary>
        public string Work_Command_Content { get; set; }
        /// <summary>
        /// 指令制作-上注要求
        /// </summary>
        public string Work_Command_UpRequire { get; set; }
        /// <summary>
        /// 指令制作-指令发送方向
        /// </summary>
        public string Work_Command_Direction { get; set; }
        /// <summary>
        /// 指令制作-上注要求
        /// </summary>
        public string Work_Command_StartTime { get; set; }
        /// <summary>
        /// 指令制作-指令发送方向
        /// </summary>
        public string Work_Command_EndTime { get; set; }
        /// <summary>
        /// 指令制作-特殊需求
        /// </summary>
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
    /// 中心计划-工作内容
    /// </summary>
    [Serializable]
    public class ZXJH_WorkContent 
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
    /// 中心计划-试验数据处理
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
        /// 主站名称
        /// </summary>
        public string MainStationName { get; set; }
        /// <summary>
        /// 主站设备
        /// </summary>
        public string MainStationEquipment { get; set; }
        /// <summary>
        /// 备站名称
        /// </summary>
        public string BakStationName { get; set; }
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
        /// <summary>
        /// 事后数据处理
        /// </summary>
        public string AfterWardsDataHandle { get; set; }
        #endregion
    }

    /// <summary>
    /// 中心计划-指挥与监视
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
        /// 时间段
        /// </summary>
        public string DateSection { get; set; }
        /// <summary>
        /// 指挥与监视任务
        /// </summary>
        public string Task { get; set; }
        /// <summary>
        /// 实时显示任务
        /// </summary>
        public string RealTimeShowTask { get; set; }
        #endregion
    }

    /// <summary>
    /// 中心计划-实时控制
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
    /// 中心计划-试验评估
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

    /// <summary>
    /// 中心计划-数据管理
    /// </summary>
    [Serializable]
    public class ZXJH_DataManage
    {
        #region -Properties-
        /// <summary>
        /// 对应试验ID
        /// </summary>
        public string SYID { get; set; }
        /// <summary>
        /// 工作
        /// </summary>
        public string Work { get; set; }
        /// <summary>
        /// 对应数据描述
        /// </summary>
        public string Description { get; set; }
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
}
