#region
//------------------------------------------------------
//Assembly:OperatingManagement.DataAccessLayer
//FileName:ResourceRequirement.cs
//Remark:资源需求实体对象类
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20120211    Create     
//------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;

namespace OperatingManagement.DataAccessLayer.BusinessManage
{
    [Serializable]
    public class ResourceRequirement
    {
        #region Properties
        /// <summary>
        /// 需求名称
        /// </summary>
        public string RequirementName { get; set; }
        /// <summary>
        /// 时间基准
        /// </summary>
        public string TimeBenchmark { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 卫星编码
        /// </summary>
        public string WXBM { get; set; }
        /// <summary>
        /// 功能类型
        /// </summary>
        public string FunctionType { get; set; }
        /// <summary>
        /// 不可用设备
        /// </summary>
        public List<UnusedEquipment> UnusedEquipmentList { get; set; }
        /// <summary>
        /// 持续时长/秒
        /// </summary>
        public int PersistenceTime{ get; set; }
        /// <summary>
        /// 支持时段
        /// </summary>
        public List<PeriodOfTime> PeriodOfTimeList { get; set; }
        /// <summary>
        /// 卫星序号,与卫星编码组成需求名称,格式为:卫星编码+卫星序号,其中卫星序号三位数字如001,073,102
        /// </summary>
        public int WXBMIndex { get; set; }
        #endregion
    }

    [Serializable]
    public class UnusedEquipment
    {
        /// <summary>
        /// 地面站编号
        /// </summary>
        public string GRCode { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipmentCode { get; set; }
    }

    [Serializable]
    public class PeriodOfTime
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }

}
