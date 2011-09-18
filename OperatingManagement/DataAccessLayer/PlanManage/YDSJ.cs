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
    public class YDSJ : BaseEntity<int, YDSJ>
    {
        private static readonly string GET_YDSJList_ByDate = "up_ydsj_getlist";

                /// <summary>
        /// Create a new instance of <see cref="SYCX"/> class.
        /// </summary>
        public YDSJ()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;

        public int YDSJID { get; set; }
        public DateTime CTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string TaskID { get; set; }
        public string InfoType { get; set; }
        /// <summary>
        /// SpaceType 1:空间机动任务;2:非空间机动任务;
        /// </summary>
        public string SpaceType { get; set; }
        public int LineCount { get; set; }
        public string FileIndex { get; set; }
        public string Reserve { get; set; }
        #endregion

        #region -Methods-
        /// <summary>
        /// 获取引导数据（空间机动任务，非空间机动任务）
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="spaceType">1:空间机动任务;2:非空间机动任务</param>
        /// <returns></returns>
        public DataSet GetYDSJListByDate(DateTime startDate, DateTime endDate,string spaceType)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                ds.Tables.Add();
                OracleCommand command = _database.GetStoreProcCommand(GET_YDSJList_ByDate);

                _database.AddInParameter(command, "p_spaceType", OracleDbType.Varchar2, spaceType);
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
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
            //this.AddValidRules("ID", "序号不能为空。", string.IsNullOrEmpty(ID));
        }
        #endregion
    }
}
