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
        private static readonly string SelectByID = "up_ydsj_selectByID";
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
        /// <summary>
        /// SpaceType 1:空间机动任务;2:非空间机动任务;
        /// </summary>
        public string SpaceType { get; set; }
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
        public string Omega { get; set; }
        /// <summary>
        /// 占4个字节，用无符号二进制整数表示，量化单位为2E-22度
        /// </summary>
        public string M { get; set; }
        #endregion

        #region -Methods-
        /// <summary>
        /// 获取引导数据（空间机动任务，非空间机动任务）
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="spaceType">1:空间机动任务;2:非空间机动任务</param>
        /// <returns></returns>
        public List<YDSJ> GetYDSJListByDate(DateTime startDate, DateTime endDate,string spaceType)
        {
            DataSet ds = null;

                ds = new DataSet();
                ds.Tables.Add();
                OracleCommand command = _database.GetStoreProcCommand(GET_YDSJList_ByDate);

                _database.AddInParameter(command, "p_spaceType", OracleDbType.Varchar2, spaceType);
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

                List<YDSJ> objDatas = new List<YDSJ>();
                if (ds != null && ds.Tables.Count == 1)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objDatas.Add(new YDSJ()
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
                            D = dr["D"].ToString(),
                            T = dr["T"].ToString(),
                            A = dr["A"].ToString(),
                            E = dr["E"].ToString(),
                            I = dr["I"].ToString(),
                            Ohm = dr["Ohm"].ToString(),
                            Omega = dr["Omega"].ToString(),
                            M = dr["M"].ToString()
                        });
                    }
                }
                return objDatas;
        }

        /// <summary>
        /// Selects the specific YDSJ by identification.
        /// </summary>
        /// <returns>YDSJ</returns>
        public YDSJ SelectById()
        {
            OracleParameter p = PrepareRefCursor();

            DataSet ds = _database.SpExecuteDataSet(SelectByID, new OracleParameter[]{
                new OracleParameter("p_Id", this.Id), 
                p
            });

            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    return new YDSJ()
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
                        D = dr["D"].ToString(),
                        T = dr["T"].ToString(),
                        A = dr["A"].ToString(),
                        E = dr["E"].ToString(),
                        I = dr["I"].ToString(),
                        Ohm = dr["Ohm"].ToString(),
                        Omega = dr["Omega"].ToString(),
                        M = dr["M"].ToString()
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
