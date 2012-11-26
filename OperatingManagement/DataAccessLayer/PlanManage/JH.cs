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
    /// 计划表
    /// </summary>
    public class JH : BaseEntity<int, JH>
    {
        private static readonly string GET_PlanList = "UP_JH_GETLIST";

        /// <summary>
        /// Create a new instance of <see cref="JH"/> class.
        /// </summary>
        public JH()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        /// <summary>
        /// Create a new instance of <see cref="JH"/> class.
        /// </summary>
        /// <param name="istempjh">是否为临时计划: true-临时计划</param>
        public JH(bool istempjh)
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
            isTempJH = istempjh;
        }

        #region -Properties-
        private OracleDatabase _database = null;

        /// <summary>
        /// 是否为临时计划：true-临时计划；false-正式计划
        /// </summary>
        public bool isTempJH { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CTime { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        public string TaskID { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 计划类型
        /// </summary>
        public string PlanType { get; set; }
        public string PlanTypeName
        {
            get 
            {
                string returnValue = "";
                switch (PlanType)
                { 
                    case "YJJH":
                        returnValue = "应用研究工作计划";
                        break;
                    case "XXXQ":
                        returnValue = "空间信息需求";
                        break;
                    case "DJZYSQ":
                        returnValue = "测控资源使用申请";
                        break;
                    case "ZXJH":
                        returnValue = "中心运行计划";
                        break;
                    case "TYSJ":
                        returnValue = "仿真推演试验数据";
                        break;
                    case "DJZYJH":
                        returnValue = "测控资源使用计划";
                        break;
                    case "GZJH":
                        returnValue = "地面站工作计划";
                        break;
                    case "SYJH":
                        returnValue = "试验计划";
                        break;
                }
                return returnValue;
            }
        }
        /// <summary>
        /// 计划ID
        /// </summary>
        public int PlanID { get; set; }
        /// <summary>
        /// 时效
        /// </summary>
        public string PLANAGING { get; set; }
        //public SYJHType PlanType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// /计划源类型：空白计划0；试验程序1；设备工作计划2；
        /// </summary>
        public int SRCType { get; set; }
        /// <summary>
        /// 计划源编号：当计划源类型为1时，为试验程序编号；计划源类型为2时，为设备工作计划编号。
        /// </summary>
        public int SRCID { get; set; } 
        /// <summary>
        /// 文件索引
        /// </summary>
        public string FileIndex { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Reserve { get; set; }

        public string SatID { get; set; }
        #endregion

        #region -Methods-
        /// <summary>
        /// 根据时间及类型获取实验计划列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<JH> GetSYJHList(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;

            ds = new DataSet();
            ds.Tables.Add();
            
            OracleCommand command = _database.GetStoreProcCommand("up_jh_getsyjhlist");
            if (isTempJH)
            {
                command = _database.GetStoreProcCommand("up_jhtemp_getsyjhlist");
            }

            if (startDate == DateTime.MinValue)
            {
                _database.AddInParameter(command, "p_startDate", OracleDbType.Date, DBNull.Value);
            }
            else
            {
                _database.AddInParameter(command, "p_startDate", OracleDbType.Date, startDate);
            }
            if (endDate == DateTime.MinValue)
            {
                _database.AddInParameter(command, "p_endDate", OracleDbType.Date, DBNull.Value);
            }
            else
            {
                _database.AddInParameter(command, "p_endDate", OracleDbType.Date, endDate);
            }
            using (IDataReader reader = _database.ExecuteReader(command))
            {
                ds.Tables[0].Load(reader);
            }
            List<JH> objDatas = new List<JH>();
            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objDatas.Add(new JH()
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        CTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                        TaskID = dr["taskid"].ToString(),
                        PlanType = dr["plantype"].ToString(),
                        PlanID = Convert.ToInt32(dr["PlanID"].ToString()),
                        StartTime = Convert.ToDateTime(dr["StartTime"].ToString()),
                        EndTime = Convert.ToDateTime(dr["EndTime"].ToString()),
                        SRCType = Convert.ToInt32(dr["SRCType"].ToString()),
                        SRCID = Convert.ToInt32(dr["SRCID"].ToString()),
                        FileIndex = dr["FileIndex"].ToString(),
                        Reserve = dr["Reserve"].ToString()
                    });
                }
            }
            return objDatas;
        }

        /// <summary>
        /// 根据时间及类型获取计划列表
        /// </summary>
        /// <param name="planType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<JH> GetJHList(string planType,  DateTime startDate, DateTime endDate)
        {
            return GetJHList("", planType, startDate, endDate);
        }

        /// <summary>
        /// 根据时间及类型获取计划列表
        /// </summary>
        /// <param name="planType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<JH> GetJHList(string taskID, string planType, DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;

            ds = new DataSet();
            ds.Tables.Add();
            OracleCommand command = _database.GetStoreProcCommand(GET_PlanList);
            if (isTempJH)
            {
                command = _database.GetStoreProcCommand("UP_JHTemp_GETLIST");
            }

            if (!isTempJH)
                _database.AddInParameter(command, "p_taskID", OracleDbType.Varchar2, taskID);
            _database.AddInParameter(command, "p_planType", OracleDbType.Varchar2, planType);
            if (startDate == DateTime.MinValue)
            {
                _database.AddInParameter(command, "p_startDate", OracleDbType.Date, DBNull.Value);
            }
            else
            {
                _database.AddInParameter(command, "p_startDate", OracleDbType.Date, startDate);
            }
            if (endDate == DateTime.MinValue)
            {
                _database.AddInParameter(command, "p_endDate", OracleDbType.Date, DBNull.Value);
            }
            else
            {
                _database.AddInParameter(command, "p_endDate", OracleDbType.Date, endDate);
            }
            using (IDataReader reader = _database.ExecuteReader(command))
            {
                ds.Tables[0].Load(reader);
            }
            List<JH> objDatas = new List<JH>();
            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objDatas.Add(new JH()
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        CTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                        TaskID = dr["taskid"].ToString(),
                        TaskName = dr["taskname"].ToString(),
                        PlanType = dr["plantype"].ToString(),
                        PlanID = Convert.ToInt32(dr["PlanID"].ToString()),
                        StartTime = Convert.ToDateTime(dr["StartTime"].ToString()),
                        EndTime = Convert.ToDateTime(dr["EndTime"].ToString()),
                        SRCType = Convert.ToInt32(dr["SRCType"].ToString()),
                        SRCID = Convert.ToInt32(dr["SRCID"].ToString()),
                        FileIndex = dr["FileIndex"].ToString(),
                        Reserve = dr["Reserve"].ToString()
                    });
                }
            }
            return objDatas;
        }

        /// <summary>
        /// 单个或多个ID，如("1,2,3")
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<JH> SelectByIDS(string ids)
        {
            OracleParameter p = PrepareRefCursor();
            string sqlcommand = "up_jh_selectinids";
            if (isTempJH)
            {
                sqlcommand = "up_jhtemp_selectinids";
            }

            DataSet ds = _database.SpExecuteDataSet(sqlcommand, new OracleParameter[]{
                new OracleParameter("p_ids",ids),
                p
            });
            List<JH> jhs = new List<JH>();
            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    jhs.Add(new JH()
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        CTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                        TaskID = dr["taskid"].ToString(),
                        PlanType = dr["plantype"].ToString(),
                        PlanID = Convert.ToInt32(dr["PlanID"].ToString()),
                        StartTime = Convert.ToDateTime(dr["StartTime"].ToString()),
                        EndTime = Convert.ToDateTime(dr["EndTime"].ToString()),
                        SRCType = Convert.ToInt32(dr["SRCType"].ToString()),
                        SRCID = Convert.ToInt32(dr["SRCID"].ToString()),
                        FileIndex = dr["FileIndex"].ToString(),
                        Reserve = dr["Reserve"].ToString()
                    });
                }
            }
            return jhs;
        }

        /// <summary>
        /// Inserts a new record into database.
        /// </summary>
        /// <returns></returns>
        public FieldVerifyResult Add()
        {
            string sqlcommand = "up_jh_insert";
            if (isTempJH)
            {
                sqlcommand = "up_jhtemp_insert";
            }

            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            OracleParameter opId = new OracleParameter()
            {
                ParameterName = "v_Id",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            _database.SpExecuteNonQuery(sqlcommand, new OracleParameter[]{
                new OracleParameter("p_TaskID",this.TaskID),
                new OracleParameter("p_PlanType",this.PlanType),
                new OracleParameter("p_PlanID",(int)this.PlanID),
                new OracleParameter("p_StartTime",(DateTime)this.StartTime),
                new OracleParameter("p_EndTime",(DateTime)this.EndTime),
                new OracleParameter("p_SRCType",(int)this.SRCType),
                new OracleParameter("p_SRCID",this.SRCID),
                new OracleParameter("p_FileIndex",this.FileIndex),
                new OracleParameter("p_Reserve",this.Reserve),
                opId,
                p
            });
            if (opId.Value != null && opId.Value != DBNull.Value)
                this.Id = Convert.ToInt32(opId.Value.ToString());
            return (FieldVerifyResult)Convert.ToInt32(p.Value.ToString());
        }

        public FieldVerifyResult Update()
        {
            string sqlcommand = "up_jh_update";
            if (isTempJH)
            {
                sqlcommand = "up_jhtemp_update";
            }

            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            _database.SpExecuteNonQuery(sqlcommand, new OracleParameter[]{
                new OracleParameter("v_Id",this.Id),
                new OracleParameter("p_TaskID",this.TaskID),
                new OracleParameter("p_StartTime",(DateTime)this.StartTime),
                new OracleParameter("p_EndTime",(DateTime)this.EndTime),
                 new OracleParameter("p_FileIndex",this.FileIndex),
                p
            });
            return (FieldVerifyResult)Convert.ToInt32(p.Value.ToString());
        }


        public FieldVerifyResult DeleteTempJH()
        {
            string sqlcommand = "up_jhtemp_delete";
            //if (isTempJH)
            //{
            //    sqlcommand = "up_jhtemp_delete";
            //}

            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            _database.SpExecuteNonQuery(sqlcommand, new OracleParameter[]{
                new OracleParameter("v_Id",this.Id),
                p
            });
            return (FieldVerifyResult)Convert.ToInt32(p.Value.ToString());
        }

        public FieldVerifyResult UpdateFileIndex()
        {
            string sqlcommand = "up_jh_updatefileindex";
            if (isTempJH)
            {
                sqlcommand = "up_jhtemp_updatefileindex";
            }

            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            _database.SpExecuteNonQuery(sqlcommand, new OracleParameter[]{
                new OracleParameter("v_Id",this.Id),
                new OracleParameter("p_FileIndex",this.FileIndex),
                p
            });
            return (FieldVerifyResult)Convert.ToInt32(p.Value.ToString());
        }
        #endregion

        #region -Private methods-
        private OracleParameter PrepareRefCursor()
        {
            return new OracleParameter()
            {
                ParameterName = "o_cursor",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.RefCursor
            };
        }
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
            //this.AddValidRules("ID", "序号不能为空。", string.IsNullOrEmpty(ID));
        }
        #endregion
    }
}