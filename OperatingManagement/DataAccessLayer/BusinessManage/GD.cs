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
    /// 轨道信息数据
    /// </summary>
    public class GD : BaseEntity<int, GD>
    {
        private static readonly string GET_OribitalQuantityList_ByDate = "up_gd_getlist";
        //private static readonly string Insert = "up_gd_insert";
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

        public string Satid { get; set; }
        public string IType { get; set; }
        public string ICode { get; set; }
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
        public string Times { get; set; }
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
        public string Q { get; set; }
        /// <summary>
        /// 占4个字节，用无符号二进制整数表示，量化单位为2E-22度
        /// </summary>
        public string W{ get; set; }
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
        public string DELTP { get; set; }
        /// <summary>
        /// 4个字节，用无符号二进制整数表示，量化单位为0.1m
        /// </summary>
        public string Ra { get; set; }
        /// <summary>
        /// 4个字节，用无符号二进制整数表示，量化单位为0.1m
        /// </summary>
        public string Rp { get; set; }
        /// <summary>
        /// 占3个字节，无符号二进制整数表示，量化单位为2E-16米2/千克
        /// </summary>
        public string CDSM { get; set; }
        /// <summary>
        /// 占3个字节，无符号二进制整数表示，量化单位为2E-16米2/千克
        /// </summary>
        public string KSM { get; set; }
        /// <summary>
        /// 各占4个字节，备用；700任务不用，固定填“0”
        /// </summary>
        public string KZ1 { get; set; }
        /// <summary>
        /// 各占4个字节，备用；700任务不用，固定填“0”
        /// </summary>
        public string KZ2 { get; set; }
        #endregion 

        #region -Methods-
        /// <summary>
        /// 根据时间获取轨道根数列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<GD> GetListByDate(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;

                ds = new DataSet();
                ds.Tables.Add();
                OracleCommand command = _database.GetStoreProcCommand(GET_OribitalQuantityList_ByDate);
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

                List<GD> objDatas = new List<GD>();
                if (ds != null && ds.Tables.Count == 1)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objDatas.Add(new GD()
                        {
                            Id = Convert.ToInt32(dr["ID"].ToString()),
                            CTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                            Satid = dr["Satid"].ToString(),
                            IType = dr["itype"].ToString(),
                            ICode = dr["icode"].ToString(),
                            D = dr["D"].ToString(),
                            T = dr["T"].ToString(),
                            Times = dr["Times"].ToString(),
                            A = dr["A"].ToString(),
                            E = dr["E"].ToString(),
                            I = dr["I"].ToString(),
                            Q = dr["Q"].ToString(),
                            W = dr["W"].ToString(),
                            M = dr["M"].ToString(),
                            P = dr["P"].ToString(),
                            DELTP = dr["PP"].ToString(),
                            Ra = dr["Ra"].ToString(),
                            Rp = dr["Rp"].ToString(),
                            CDSM = dr["CDSM"].ToString(),
                            KSM = dr["KSM"].ToString(),
                            KZ1 = dr["KZ1"].ToString(),
                            KZ2 = dr["KZ2"].ToString()
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
                        Satid = dr["Satid"].ToString(),
                        IType = dr["itype"].ToString(),
                        ICode = dr["icode"].ToString(),
                        D = dr["D"].ToString(),
                        T = dr["T"].ToString(),
                        Times = dr["Times"].ToString(),
                        A = dr["A"].ToString(),
                        E = dr["E"].ToString(),
                        I = dr["I"].ToString(),
                        Q = dr["Q"].ToString(),
                        W = dr["W"].ToString(),
                        M = dr["M"].ToString(),
                        P = dr["P"].ToString(),
                        DELTP = dr["Pi"].ToString(),
                        Ra = dr["Ra"].ToString(),
                        Rp = dr["Rp"].ToString(),
                        CDSM = dr["CDSM"].ToString(),
                        KSM = dr["KSM"].ToString(),
                        KZ1 = dr["KZ1"].ToString(),
                        KZ2 = dr["KZ2"].ToString()
                    };
                }
            }
            return null;
        }

        #region  bakup
        /*
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
                new OracleParameter("p_Satid",this.Satid),
                new OracleParameter("p_IType",this.IType),
                new OracleParameter("p_ICode",this.ICode),
                new OracleParameter("p_Data_T",this.T),
                new OracleParameter("p_Times",this.Times),
                new OracleParameter("p_Data_D",this.D),
                new OracleParameter("p_Data_T",this.T),
                new OracleParameter("p_Times",this.Times),
                new OracleParameter("p_Data_A",this.A),
                new OracleParameter("p_Data_E",this.E),
                new OracleParameter("p_Data_I",this.I),
                new OracleParameter("p_Data_Ohm",this.Q),
                new OracleParameter("p_Data_Omega",this.W),
                new OracleParameter("p_Data_M",this.M),
                new OracleParameter("p_Data_P",this.P),
                new OracleParameter("p_Data_PI",this.DELTP),
                new OracleParameter("p_Data_RA",this.Ra),
                new OracleParameter("p_Data_RP",this.Rp),
                new OracleParameter("p_Data_CDSM",this.CDSM),
                new OracleParameter("p_Data_KSM",this.KSM),
                new OracleParameter("p_Data_KZ1",this.KZ1),
                new OracleParameter("p_Data_KZ2",this.KZ2),
                opId,
                p
            });
            if (opId.Value != null && opId.Value != DBNull.Value)
                this.Id = Convert.ToInt32(opId.Value);
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }
        */
        #endregion

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
