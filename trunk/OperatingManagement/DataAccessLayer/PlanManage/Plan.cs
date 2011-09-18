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
    public class Plan : BaseEntity<int, SYCX>
    {
        private static readonly string GET_PlanList = "";

        /// <summary>
        /// Create a new instance of <see cref="Plan"/> class.
        /// </summary>
        public Plan()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;

        public int PLANID { get; set; }
        public DateTime CTime { get; set; }
        public string PLANTYPE { get; set; }
        public string PLANAGING { get; set; }//时效
        public SYJHType PlanType { get; set; }
        public int SYCXID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
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
        public DataSet GetSYJHList(string planType,string planAging,DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                ds.Tables.Add();
                OracleCommand command = _database.GetStoreProcCommand(GET_PlanList);
                _database.AddInParameter(command, "p_planType", OracleDbType.Varchar2, planType);
                _database.AddInParameter(command, "p_planAging", OracleDbType.Varchar2, planAging);
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

        public string GenarateFileName()
        {
            string fileName="";

            return fileName;
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
