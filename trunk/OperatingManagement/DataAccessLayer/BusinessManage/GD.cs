﻿using System;
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
        private static readonly string SelectByID = "up_gd_selectByID";

        /// <summary>
        /// Create a new instance of <see cref="GD"/> class.
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
        public List<GD> GetGDListByDate(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;

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

                List<GD> objDatas = new List<GD>();
                if (ds != null && ds.Tables.Count == 1)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objDatas.Add(new GD()
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
                            M = dr["M"].ToString(),
                            P = dr["P"].ToString(),
                            Pi = dr["Pi"].ToString(),
                            Ra = dr["Ra"].ToString(),
                            Rp = dr["Rp"].ToString()
                        });
                    }
                }


                return objDatas;
        }

        /// <summary>
        /// Selects the specific GD by identification.
        /// </summary>
        /// <returns>GD</returns>
        public GD SelectById()
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
                    return new GD()
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
                        M = dr["M"].ToString(),
                        P = dr["P"].ToString(),
                        Pi = dr["Pi"].ToString(),
                        Ra = dr["Ra"].ToString(),
                        Rp = dr["Rp"].ToString()
                    };
                }
            }
            return null;
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
