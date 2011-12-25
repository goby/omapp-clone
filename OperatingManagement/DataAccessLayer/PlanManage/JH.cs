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

        #region -Properties-
        private OracleDatabase _database = null;

        public int ID { get; set; }
        public DateTime CTime { get; set; }
        public string TaskID { get; set; }
        public string PlanType { get; set; }
        public int PlanID { get; set; }
        public string PLANAGING { get; set; }//时效
        //public SYJHType PlanType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SRCType { get; set; } //计划源类型：空白计划0；试验程序1；设备工作计划2；
        public int SRCID { get; set; } //计划源编号：当计划源类型为1时，为试验程序编号；计划源类型为2时，为设备工作计划编号。
        public string FileIndex { get; set; }
        public string Reserve { get; set; }
        #endregion

        #region -Methods-
        /// <summary>
        /// 根据时间及类型获取计划列表
        /// </summary>
        /// <param name="planType"></param>
        /// <param name="planAging"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<JH> GetJHList(string planType, string planAging, DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;

                ds = new DataSet();
                ds.Tables.Add();
                OracleCommand command = _database.GetStoreProcCommand(GET_PlanList);
                _database.AddInParameter(command, "p_planType", OracleDbType.Varchar2, planType);
                _database.AddInParameter(command, "p_planAging", OracleDbType.Varchar2, planAging);
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
        /// 单个或多个ID，如("1,2,3")
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<JH> SelectByIDS(string ids)
        {
            OracleParameter p = PrepareRefCursor();
            DataSet ds = _database.SpExecuteDataSet("up_jh_selectinids", new OracleParameter[]{
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
            _database.SpExecuteNonQuery("up_jh_insert", new OracleParameter[]{
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
                this.Id = Convert.ToInt32(opId.Value);
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }

        public FieldVerifyResult Update()
        {
            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            _database.SpExecuteNonQuery("up_jh_update", new OracleParameter[]{
                new OracleParameter("v_Id",this.Id),
                new OracleParameter("p_TaskID",this.TaskID),
                new OracleParameter("p_StartTime",(DateTime)this.StartTime),
                new OracleParameter("p_EndTime",(DateTime)this.EndTime),
                p
            });
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }

        public JH SelectByPlanTypeAndPlanID(string plantype,int planid)
        {
            OracleParameter p = PrepareRefCursor();

            DataSet ds = _database.SpExecuteDataSet("up_jh_selectbyplantypeandid", new OracleParameter[]{
                new OracleParameter("p_PlanType", this.PlanType),
                new OracleParameter("p_PlanId", this.PlanID), 
                p
            });

            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    return new JH()
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
                    };
                }
            }
            return null;
        }

        public FieldVerifyResult UpdateFileIndex()
        {
            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            _database.SpExecuteNonQuery("up_jh_updatefileindex", new OracleParameter[]{
                new OracleParameter("v_Id",this.Id),
                new OracleParameter("p_FileIndex",this.FileIndex),
                p
            });
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
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
