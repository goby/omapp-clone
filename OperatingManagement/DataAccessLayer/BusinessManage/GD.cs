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
    public class GD : BaseEntity<int, GD>
    {
        private static readonly string GET_OribitalQuantityList_ByDate = "up_gd_getlist";
        private static readonly string Insert = "up_gd_insert";

        /// <summary>
        /// Create a new instance of <see cref="SYCX"/> class.
        /// </summary>
        public GD()
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
        /// 占2个字节，用无符号二进制整数表示，
        /// 量化单位为1天，北京时2000年1月1日计为第1天
        /// </summary>
        public string D { get; set; }
        /// <summary>
        /// 占4个字节，用无符号二进制整数表示，
        /// 量化单位为0.1ms
        /// </summary>
        public string T { get; set; }
        /// <summary>
        /// 4个字节，用无符号二进制整数表示，
        /// 量化单位为0.1m
        /// </summary>
        public string A { get; set; }
        /// <summary>
        /// 占4个字节，用无符号二进制整数表示，量化单位 为2E-31
        /// </summary>
        public string E { get; set; }
        /// <summary>
        /// 占4个字节，用无符号二进制整数表示，量化单位为2E-24度
        /// </summary>
        public string I { get; set; }
        /// <summary>
        /// 占4个字节，用无符号二进制整数表示，量化单位为2E-22度
        /// </summary>
        public string Ohm { get; set; }
        /// <summary>
        /// 占4个字节，用无符号二进制整数表示，量化单位为2E-22度
        /// </summary>
        public string Omega{ get; set; }
        /// <summary>
        /// 占4个字节，用无符号二进制整数表示，量化单位为2E-22度
        /// </summary>
        public string M { get; set; }
        /// <summary>
        /// 占4个字节，用二进制整数补码表示，量化单位为2E-20分钟
        /// </summary>
        public string P { get; set; }
        /// <summary>
        /// 占4个字节，用二进制整数补码表示，量化单位为2E-20秒/天
        /// </summary>
        public string Pi { get; set; }
        /// <summary>
        /// 4个字节，用无符号二进制整数表示，量化单位为0.1m
        /// </summary>
        public string Ra { get; set; }
        /// <summary>
        /// 4个字节，用无符号二进制整数表示，量化单位为0.1m
        /// </summary>
        public string Rp { get; set; }
        #endregion 

        #region -Methods-
        /// <summary>
        /// 获取轨道根数列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetGDListByDate(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                ds.Tables.Add();
                OracleCommand command = _database.GetStoreProcCommand(GET_OribitalQuantityList_ByDate);
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
                new OracleParameter("p_Data_D",this.D),
                new OracleParameter("p_Data_T",this.T),
                new OracleParameter("p_Data_A",this.A),
                new OracleParameter("p_Data_E",this.E),
                new OracleParameter("p_Data_I",this.I),
                new OracleParameter("p_Data_Ohm",this.Ohm),
                new OracleParameter("p_Data_Omega",this.Omega),
                new OracleParameter("p_Data_M",this.M),
                new OracleParameter("p_Data_P",this.P),
                new OracleParameter("p_Data_PI",this.Pi),
                new OracleParameter("p_Data_RA",this.Ra),
                new OracleParameter("p_Data_RP",this.Rp),
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
