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
    public class SYCX : BaseEntity<int, SYCX>
    {
        private static readonly string GET_SYCXList_ByDate = "";

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
        public string FileIndex { get; set; }
        public string Reserve { get; set; }
        #endregion

        #region -Methods-
        /// <summary>
        /// 根据时间获取试验程序列表
        /// </summary>
        /// <param name="provinceCode"></param>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        public DataSet GetSYCXListByDate(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;
            try
            {
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
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
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
