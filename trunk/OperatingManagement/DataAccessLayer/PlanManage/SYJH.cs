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
    public class SYJH : BaseEntity<int, SYJH>
    {
        private static readonly string GET_SYJHList_ByDate = "UP_SYJH_GETLIST";
        private static readonly string GET_SYJH_ByID = "UP_SYJH_SELECTBYID";

        /// <summary>
        /// Create a new instance of <see cref="SYJH"/> class.
        /// </summary>
        public SYJH()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;

        public int JHID { get; set; }
        public DateTime CTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string TaskID { get; set; }
        public string InfoType { get; set; }
        public int LineCount { get; set; }
        public SYJHType PlanType { get; set; }
        public int PlanID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string FileIndex { get; set; }
        public string Reserve { get; set; }
        #endregion

        #region -Methods-
        /// <summary>
        /// 根据时间获取试验计划列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SYJH> GetSYJHListByDate(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;

            ds = new DataSet();
            ds.Tables.Add();
            OracleCommand command = _database.GetStoreProcCommand(GET_SYJHList_ByDate);
            if (startDate != DateTime.MinValue)
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
            _database.AddOutParameter(command, "o_cursor", OracleDbType.RefCursor, 0);
            using (IDataReader reader = _database.ExecuteReader(command))
            {
                ds.Tables[0].Load(reader);
            }

            List<SYJH> objDatas = new List<SYJH>();
            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objDatas.Add(new SYJH()
                    {
                        Id = Convert.ToInt32(dr["JHID"].ToString()),
                        CTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                        TaskID = dr["TASKID"].ToString(),
                        Source = dr["SOURCE"].ToString(),
                        Destination = dr["DESTINATION"].ToString(),
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
        /// Selects the specific SYJH by identification.
        /// </summary>
        /// <returns>SYJH</returns>
        public SYJH SelectById()
        {
            OracleParameter p = PrepareRefCursor();

            DataSet ds = _database.SpExecuteDataSet(GET_SYJH_ByID, new OracleParameter[]{
                new OracleParameter("p_Id", this.Id), 
                p
            });

            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    return new SYJH()
                    {
                        Id = Convert.ToInt32(dr["JHID"].ToString()),
                        CTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                        TaskID = dr["TASKID"].ToString(),
                        Source = dr["SOURCE"].ToString(),
                        Destination = dr["DESTINATION"].ToString(),
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
        //
    }
}
