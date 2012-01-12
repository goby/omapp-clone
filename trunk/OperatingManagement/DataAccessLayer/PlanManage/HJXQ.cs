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
    /// 空间目标信息需求？？
    /// </summary>
    public class HJXQ
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
        public string User { get; set; }
        public string Time { get; set; }
        public string EnvironInfo { get; set; }
        public string  TimeSection1{ get; set; }
        public string  TimeSection2{ get; set; }
        public string  Sum{ get; set; }

        public List<HJXQSatInfo> SatInfos
        {
            get;
            set;
        }
        #endregion

    }

    public class HJXQSatInfo 
    { 
        #region -Properties-
        public string SatName { get; set; }
        public string InfoName { get; set; }
        public string InfoArea { get; set; }
        public string InfoTime { get; set; }
        #endregion
    }
}
