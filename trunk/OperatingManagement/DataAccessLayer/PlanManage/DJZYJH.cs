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
    public class DJZYJH
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
        public string SNO { get; set; }    //申请序列号
        public string SJ { get; set; }    //时间
        public string HTQID { get; set; }    //航天器标识
        public string SNUM { get; set; }    //计划数量

        public List<DJZYJH_Plan> DJZYJHPlans { get; set; }
        #endregion
    }

    /// <summary>
    /// 设备工作计划-任务
    /// </summary>
    [Serializable]
    public class DJZYJH_Plan
    {
        #region -Properties-
        /// <summary>
        /// 计划序号
        /// </summary>
        public string SXH { get; set; }
        /// <summary>
        /// 答复标志
        /// 1－全部满足；
        /// 2－部分满足，时间安排有所调整；
        /// F－无法满足，相应时间参数填全0。
        /// </summary>
        public string DF { get; set; }
        /// <summary>
        /// 计划性质
        /// </summary>
        public string SXZ { get; set; }
        /// <summary>
        /// 任务类别
        /// </summary>
        public string MLB { get; set; }
        /// <summary>
        /// 工作方式
        /// </summary>
        public string FS { get; set; }
        /// <summary>
        /// 工作单元
        /// </summary>
        public string GZDY { get; set; }
        /// <summary>
        /// 设备代号
        /// </summary>
        public string SBDH { get; set; }
        /// <summary>
        /// 圈次
        /// </summary>
        public string QC { get; set; }
        /// <summary>
        /// 圈标
        /// </summary>
        public string QB { get; set; }
        /// <summary>
        /// 测控事件类型
        /// </summary>
        public string SHJ { get; set; }
        /// <summary>
        /// 工作点频
        /// </summary>
        public List<DJZYJH_GZDP> DJZYJHGZDPs { get; set; }
        /// <summary>
        /// 同时支持目标数
        /// </summary>
        public string TNUM { get; set; }
        /// <summary>
        /// 任务准备开始时间
        /// </summary>
        public string ZHB { get; set; }
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public string RK { get; set; }
        /// <summary>
        /// 跟踪开始时间
        /// </summary>
        public string GZK { get; set; }
        /// <summary>
        /// 开上行载波时间
        /// </summary>
        public string KSHX { get; set; }
        /// <summary>
        /// 关上行载波时间
        /// </summary>
        public string GSHX { get; set; }
        /// <summary>
        /// 跟踪结束时间
        /// </summary>
        public string GZJ { get; set; }
        /// <summary>
        /// 任务结束时间
        /// </summary>
        public string JS { get; set; }

        #endregion
    }

    /// <summary>
    /// 测控资源使用计划-工作点频
    /// </summary>
    public class DJZYJH_GZDP
    {
        /// <summary>
        /// 点频序号
        /// </summary>
        public string FXH { get; set; }
        /// <summary>
        /// 频段选择
        /// </summary>
        public string PDXZ { get; set; }
        /// <summary>
        /// 点频选择
        /// </summary>
        public string DPXZ { get; set; }
    }
}
