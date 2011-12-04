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
    //空间环境信息
    public class XXXQ : BaseEntity<int, XXXQ>
    {
        private static readonly string GET_List_ByDate = "up_XXXQ_getlist";
        private static readonly string Insert = "up_XXXQ_insert";

                /// <summary>
        /// Create a new instance of <see cref="SYCX"/> class.
        /// </summary>
        public XXXQ()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;

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
        #endregion

        #region -Methods-
        /// <summary>
        /// 应用研究工作计划
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetListByDate(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                ds.Tables.Add();
                OracleCommand command = _database.GetStoreProcCommand(GET_List_ByDate);
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
                using (IDataReader reader = _database.ExecuteReader(command))
                {
                    ds.Tables[0].Load(reader);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
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
            _database.SpExecuteNonQuery(Insert, new OracleParameter[]{
                new OracleParameter("p_CTime",this.CTime),
                new OracleParameter("p_Source",this.Source),
                new OracleParameter("p_Destination",this.Destination),
                new OracleParameter("p_TaskID",this.TaskID),
                new OracleParameter("p_InfoType",this.InfoType),
                new OracleParameter("p_LineCount",(int)this.LineCount),
                new OracleParameter("p_Format1",this.LineCount),
                new OracleParameter("p_Format2",this.LineCount),
                new OracleParameter("p_DataSection",this.LineCount),
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
            _database.SpExecuteNonQuery("up_xxxq_update", new OracleParameter[]{
                new OracleParameter("p_Id",this.ID),
                //new OracleParameter("p_CTime",this.CTime),
                new OracleParameter("p_Source",this.Source),
                new OracleParameter("p_Destination",this.Destination),
                new OracleParameter("p_TaskID",this.TaskID),
                new OracleParameter("p_InfoType",this.InfoType),
                new OracleParameter("p_LineCount",(int)this.LineCount),
                new OracleParameter("p_Format1",this.Format1),
                new OracleParameter("p_Format2",this.Format2),
                new OracleParameter("p_DataSection",this.DataSection),
               // new OracleParameter("p_FileIndex",this.FileIndex),
                new OracleParameter("p_Reserve",this.Reserve),
                p
            });
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
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
