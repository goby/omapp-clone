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
    public class GZJH
    {
        /// <summary>
        /// 地面站工作计划
        /// </summary>
        public GZJH()
        {
        }

        #region -Properties-
        private OracleDatabase _database = null;

        public int ID { get; set; }
        public DateTime CTime { get; set; }
        public string TaskID { get; set; }
        public string SatID { get; set; }

        /// <summary>
        /// 计划序号
        /// </summary>
        public string JXH { get; set; }
        /// <summary>
        /// 信息分类，用2位字符表示，地面站工作周计划固定填“ZJ”，地面站工作日计划固定填“RJ”。
        /// </summary>
        public string XXFL { get; set; }
        /// <summary>
        /// 总圈数，用4位整型数表示
        /// </summary>
        public string QS { get; set; }

        public List<GZJH_Content> GZJHContents { get; set; }
        #endregion
    }

    [Serializable]
    public class GZJH_Content
    {
        #region -Properties-
        /// <summary>
        /// 工作单位，用2位整型数表示
        /// </summary>
        public string DW { get; set; }
        //设备代号，用2位整型数表示
        public string SB { get; set; }
        /// <summary>
        /// 本行计划对应的卫星飞行圈次
        /// </summary>
        public string QH { get; set; }
        /// <summary>
        /// 任务代号
        /// 具体含义为（可扩充）：0501—700任务，5701—TS-3卫星，
        /// 5702—TS-4-A卫星，5703—TS-4-B卫星，5704—TS-5-A卫星，5705—TS-5-B。
        /// </summary>
        public string DH { get; set; }
        /// <summary>
        /// 工作方式，用2位字符串表示，其含义为：SZ—实战，LT—联调，HL—全区合练
        /// </summary>
        public string FS { get; set; }
        /// <summary>
        /// 计划性质，用2位字符串表示，其含义为：ZC—正常计划，YJ—应急计划
        /// </summary>
        public string JXZ { get; set; }
        /// <summary>
        /// 设备工作模式，用2位整型数表示，其含义为（可扩充）：01—标准TTC模式，
        /// 02—扩频模式1，03—扩频模式2，04—S数传接收模式，05—X数传接收模式
        /// </summary>
        public string MS { get; set; }
        /// <summary>
        /// 本帧计划的圈标识，用2位字符串表示，其含义为（可扩充）：
        /// Q1—第一圈，Q2—第二圈，RJ—入境圈，CJ—出境圈，YB—一般圈。
        /// </summary>
        public string QB { get; set; }
        /// <summary>
        /// 工作性质，用1位字符串表示，，M—主站，B—备站。
        /// </summary>
        public string GXZ { get; set; }
        /// <summary>
        /// 任务准备开始时间,用14位字符串表示，年为4个字符，月日时分秒各为2个字符
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
        /// <summary>
        /// 信息类别标志，用2位整型数表示，其含义为（可扩充）：
        /// 01—数传数据，02—遥测数据，03—测距数据，04—测速数据，05—测角数据。
        /// </summary>
        public string BID { get; set; }
        /// <summary>
        /// 实时传送数据标志
        /// </summary>
        public string SBZ { get; set; }
        /// <summary>
        /// 数据传输开始时间
        /// </summary>
        public string RTs { get; set; }
        /// <summary>
        /// 数据传输结束时间
        /// </summary>
        public string RTe { get; set; }
        /// <summary>
        /// 数据传输速率
        /// </summary>
        public string SL { get; set; }
        /// <summary>
        /// 事后回放传送数据标志
        /// </summary>
        public string HBZ { get; set; }
        /// <summary>
        /// 数据起始时间
        /// </summary>
        public string Ts { get; set; }
        /// <summary>
        /// 数据结束时间
        /// </summary>
        public string Te { get; set; }
        #endregion
    }
}
