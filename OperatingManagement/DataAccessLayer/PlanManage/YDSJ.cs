﻿using System;
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
    /// <summary>
    /// 引导数据
    /// </summary>
    public class YDSJ : BaseEntity<int, YDSJ>
    {
        private static readonly string GET_YDSJList_ByDate = "up_ydsj_getlist";
        private static readonly string SelectByID = "up_ydsj_selectByID";
        private static readonly string s_up_ydsj_insert = "up_ydsj_insert";

        /// <summary>
        /// Create a new instance of <see cref="YDSJ"/> class.
        /// </summary>
        public YDSJ()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CTime { get; set; }
        /// <summary>
        /// 任务代号
        /// </summary>
        public string TaskID { get; set; }
        /// <summary>
        /// 卫星名称
        /// </summary>
        public string SatName { get; set; }
        /// <summary>
        /// 历元日期
        /// </summary>
        public DateTime D { get; set; }
        /// <summary>
        /// 历元时间
        /// </summary>
        public string T { get; set; }
        /// <summary>
        /// 轨道半长径
        /// </summary>
        public double A { get; set; }
        /// <summary>
        /// 轨道偏心率
        /// </summary>
        public double E { get; set; }
        /// <summary>
        /// 轨道倾角
        /// </summary>
        public double I { get; set; }
        /// <summary>
        /// 轨道升交点赤径 
        /// </summary>
        public double O { get; set; }
        /// <summary>
        /// 轨道近地点幅角
        /// </summary>
        public double W { get; set; }
        /// <summary>
        /// 平近点角
        /// </summary>
        public double M { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Reserve { get; set; }
        #endregion

        #region -Methods-
        /// <summary>
        /// 获取引导数据（空间机动任务，非空间机动任务）
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<YDSJ> GetListByDate(DateTime startDate, DateTime endDate)
        {
            DataSet ds = null;

            ds = new DataSet();
            ds.Tables.Add();
            OracleCommand command = _database.GetStoreProcCommand(GET_YDSJList_ByDate);

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
                        CTime = DateTime.Parse(dr["CTIME"].ToString()),
                        TaskID = dr["TaskID"].ToString(),
                        SatName = dr["SatName"].ToString(),
                        D = DateTime.Parse(dr["D"].ToString()),
                        T = dr["T"].ToString(),
                        A = Convert.ToDouble(dr["A"].ToString()),
                        E = Convert.ToDouble(dr["E"].ToString()),
                        I = Convert.ToDouble(dr["I"].ToString()),
                        O = Convert.ToDouble(dr["O"].ToString()),
                        W = Convert.ToDouble(dr["W"].ToString()),
                        M = Convert.ToDouble(dr["M"].ToString()),
                        Reserve = dr["RESERVE"].ToString()
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
                        CTime = DateTime.Parse(dr["CTIME"].ToString()),
                        TaskID = dr["TaskID"].ToString(),
                        SatName = dr["SatName"].ToString(),
                        D = DateTime.Parse(dr["D"].ToString()),
                        T = dr["T"].ToString(),
                        A = Convert.ToDouble(dr["A"].ToString()),
                        E = Convert.ToDouble(dr["E"].ToString()),
                        I = Convert.ToDouble(dr["I"].ToString()),
                        O = Convert.ToDouble(dr["O"].ToString()),
                        W = Convert.ToDouble(dr["W"].ToString()),
                        M = Convert.ToDouble(dr["M"].ToString()),
                        Reserve = dr["RESERVE"].ToString()
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
            _database.SpExecuteNonQuery(s_up_ydsj_insert, new OracleParameter[]{
                new OracleParameter("p_CTime", DateTime.Now),
                new OracleParameter("p_TaskID", this.TaskID),
                new OracleParameter("p_SatName", this.SatName),
                new OracleParameter("p_D", this.D),
                new OracleParameter("p_T", this.T),
                new OracleParameter("p_A", this.A),
                new OracleParameter("p_E", this.E),
                new OracleParameter("p_I", this.I),
                new OracleParameter("p_O", this.O),
                new OracleParameter("p_W", this.W),
                new OracleParameter("p_Reserve", this.Reserve),
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
