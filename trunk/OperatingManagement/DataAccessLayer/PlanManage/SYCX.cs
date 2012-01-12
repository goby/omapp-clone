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
    /// 试验程序
    /// </summary>
    public class SYCX : BaseEntity<int, SYCX>
    {
        private static readonly string GET_SYCXList_ByDate = "UP_SYCX_GETLIST";
        private static readonly string GET_SYCX_ByID = "UP_SYCX_SELECTBYID";

        /// <summary>
        /// Create a new instance of <see cref="SYCX"/> class.
        /// </summary>
        public SYCX()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;

        public int ID {get;set;}
        public DateTime CTime {get;set;}
        public string TaskID { get; set; }
        public SYCXType PType { get; set; }
        public string PName { get; set; }
        public int PNID { get; set; }
        public int PlanID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string InfoType { get; set; }
        public int LineCount { get; set; }
        public string FileIndex { get; set; }
        public string Reserve { get; set; }
        #endregion

        #region -Methods-
        /// <summary>
        /// 根据时间获取试验程序列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SYCX> GetSYCXListByDate(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;

                ds = new DataSet();
                ds.Tables.Add();
                OracleCommand command = _database.GetStoreProcCommand(GET_SYCXList_ByDate);
                if (startDate != DateTime.MinValue )
                {
                    _database.AddInParameter(command, "p_startDate", OracleDbType.Date, DBNull.Value);
                }
                else
                {
                    _database.AddInParameter(command, "p_startDate", OracleDbType.Date, startDate);
                }
                if (endDate != DateTime.MinValue)
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

                List<SYCX> objDatas = new List<SYCX>();
                if (ds != null && ds.Tables.Count == 1)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objDatas.Add(new SYCX()
                        {
                            Id = Convert.ToInt32(dr["ID"].ToString()),
                            CTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                            TaskID = dr["TASKID"].ToString(),
                            PType = (SYCXType)(Convert.ToInt32( dr["PTYPE"].ToString())),
                            PName = dr["PNAME"].ToString(),
                            PNID = Convert.ToInt32(dr["PNID"].ToString()),
                            PlanID = Convert.ToInt32(dr["PLANID"].ToString()),
                            StartTime = Convert.ToDateTime(dr["STARTTIME"].ToString()),
                            EndTime = Convert.ToDateTime(dr["ENDTIME"].ToString()),
                            InfoType = dr["INFOTYPE"].ToString(),
                            LineCount = Convert.ToInt32(dr["LINECOUNT"].ToString()),
                            FileIndex = dr["FILEINDEX"].ToString(),
                            Reserve = dr["RESERVE"].ToString()
                        });
                    }
                }

                return objDatas;
        }

        /// <summary>
        /// Selects the specific SYCX by identification.
        /// </summary>
        /// <returns>SYCX</returns>
        public SYCX SelectById()
        {
            OracleParameter p = PrepareRefCursor();

            DataSet ds = _database.SpExecuteDataSet(GET_SYCX_ByID, new OracleParameter[]{
                new OracleParameter("p_Id", this.Id), 
                p
            });

            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    return new SYCX()
                    {
                        Id = Convert.ToInt32(dr["ID"].ToString()),
                        CTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                        TaskID = dr["TASKID"].ToString(),
                        PType = (SYCXType)(Convert.ToInt32(dr["PTYPE"].ToString())),
                        PName = dr["PNAME"].ToString(),
                        PNID = Convert.ToInt32(dr["PNID"].ToString()),
                        PlanID = Convert.ToInt32(dr["PLANID"].ToString()),
                        StartTime = Convert.ToDateTime(dr["STARTTIME"].ToString()),
                        EndTime = Convert.ToDateTime(dr["ENDTIME"].ToString()),
                        InfoType = dr["INFOTYPE"].ToString(),
                        LineCount = Convert.ToInt32(dr["LINECOUNT"].ToString()),
                        FileIndex = dr["FILEINDEX"].ToString(),
                        Reserve = dr["RESERVE"].ToString()
                    };
                }
            }
            return null;
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
