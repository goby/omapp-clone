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
    /// 应用研究工作计划
    /// </summary>
    public class YJJH
    {
        #region -Properties-

        public int ID { get; set; }
        public DateTime CTime { get; set; }
        public string TaskID { get; set; }
        public string InfoType { get; set; }
        public string FileIndex { get; set; }

        public string SatID { get; set; }
        /// <summary>
        /// 信息分类
        /// </summary>
        public string XXFL { get; set; }
        /// <summary>
        /// 计划序号
        /// </summary>
        public string JXH { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SysName { get; set; }
        /// <summary>
        /// 试验开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 试验结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 系统任务
        /// </summary>
        public string Task { get; set; }
        #endregion
    }
}
