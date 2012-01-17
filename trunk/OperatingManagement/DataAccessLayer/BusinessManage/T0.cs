using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;
using System.Data;
using Oracle.DataAccess.Client;

namespace OperatingManagement.DataAccessLayer.BusinessManage
{
    /// <summary>
    /// 起飞零点
    /// </summary>
    public class T0 : BaseEntity<int, T0>
    {
        private static readonly string GET_T0List_ByDate = "up_t0_getlist";
        private static readonly string Insert = "up_t0_insert";

        /// <summary>
        /// Create a new instance of <see cref="SYCX"/> class.
        /// </summary>
        public T0()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;

        public int ID { get; set; }
        public DateTime CTime { get; set; }
        public string Version { get; set; }
        public string Flag { get; set; }
        public string MainType { get; set; }
        public string DataType { get; set; }
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
        public string MissionCode { get; set; }
        public string SatelliteCode { get; set; }
        public DateTime DataDate { get; set; }
        public string DataTime { get; set; }
        public string SequenceNumber { get; set; }
        public string ChildrenPackNumber { get; set; }
        public string UDPReserve { get; set; }
        public string DataLength { get; set; }
        public string DataClass { get; set; }
        public string Reserve { get; set; }
        /// <summary>
        /// 占4个字节，二进制整数补码表示，量化单位为0.1ms
        /// </summary>
        public string TZero { get; set; }
        #endregion

        #region -Methods-
        /// <summary>
        /// 获取起飞零点列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<T0> GetListByDate(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;

                ds = new DataSet();
                ds.Tables.Add();
                OracleCommand command = _database.GetStoreProcCommand(GET_T0List_ByDate);
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

                List<T0> objDatas = new List<T0>();
                if (ds != null && ds.Tables.Count == 1)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objDatas.Add(new T0()
                        {
                            Id = Convert.ToInt32(dr["ID"].ToString()),
                            CTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                            Version = dr["Version"].ToString(),
                            Flag = dr["Flag"].ToString(),
                            MainType = dr["MainType"].ToString(),
                            DataType = dr["DataType"].ToString(),
                            SourceAddress = dr["SourceAddress"].ToString(),
                            DestinationAddress = dr["DestinationAddress"].ToString(),
                            MissionCode = dr["MissionCode"].ToString(),
                            SatelliteCode = dr["SatelliteCode"].ToString(),
                            DataDate = Convert.ToDateTime(dr["DataDate"].ToString()),
                            DataTime = dr["DataTime"].ToString(),
                            SequenceNumber = dr["SequenceNumber"].ToString(),
                            ChildrenPackNumber = dr["ChildrenPackNumber"].ToString(),
                            UDPReserve = dr["UDPReserve"].ToString(),
                            DataLength = dr["DataLength"].ToString(),
                            DataClass = dr["DataClass"].ToString(),
                            Reserve = dr["RESERVE"].ToString(),
                            TZero = dr["TZero"].ToString()
                        });
                    }
                }


                return objDatas;
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
                new OracleParameter("p_Version",this.Version),
                new OracleParameter("p_Flag",this.Flag),
                new OracleParameter("p_Maintype",this.MainType),
                new OracleParameter("p_datatype",this.DataType),
                new OracleParameter("p_Source",this.SourceAddress),
                new OracleParameter("p_Destination",this.DestinationAddress),
                new OracleParameter("p_missioncode",this.MissionCode),
                new OracleParameter("p_satellitecode",this.SatelliteCode),
                new OracleParameter("p_datadate",(DateTime)this.DataDate),
                new OracleParameter("p_datatime",this.DataTime),
                new OracleParameter("p_sequencenumber",this.SequenceNumber),
                new OracleParameter("p_childrenpacknumber",this.ChildrenPackNumber),
                new OracleParameter("p_udpReserve",this.UDPReserve),
                new OracleParameter("p_datalength",this.DataLength),
                new OracleParameter("p_dataclass",this.DataClass),
                new OracleParameter("p_Reserve",this.Reserve),
                new OracleParameter("p_Data_TZero",this.TZero),
                opId,
                p
            });
            if (opId.Value != null && opId.Value != DBNull.Value)
                this.Id = Convert.ToInt32(opId.Value);
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
