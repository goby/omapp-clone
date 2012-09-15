#region
//------------------------------------------------------
//Assembly:OperatingManagement.DataAccessLayer
//FileName:Satellite.cs
//Remark:卫星信息读取类
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20120114    Create     
//------------------------------------------------------
#endregion
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;
using Oracle.DataAccess.Client;

namespace OperatingManagement.DataAccessLayer.BusinessManage
{
    [Serializable]
    public class Satellite : BaseEntity<string, Satellite>
    {
        public Satellite()
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region Properties
        private OracleDatabase _dataBase = null;
        private const string s_up_sat_insert = "up_satellite_insert";
        private const string s_up_sat_update = "up_satellite_update";

        /// <summary>
        /// 卫星名称
        /// </summary>
        public string WXMC { get; set; }
        ///// <summary>
        ///// 卫星编号
        ///// </summary>
        public string WXBM { get; set; }
        ///// <summary>
        ///// 卫星标识
        ///// </summary>
        public string WXBS { get; set; }
        /// <summary>
        /// 状态，0为可用，1为不可用
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 面质比
        /// </summary>
        public int MZB { get; set; }
        /// <summary>
        /// 表面反射系数
        /// </summary>
        public int BMFSXS { get; set; }
        /// <summary>
        /// 按属性表顺序填写属性值
        /// </summary>
        public string SX { get; set; }
        /// <summary>
        /// 按功能表顺序，如果有该功能，没有的填0，之间“,”隔开
        /// </summary>
        public string GN { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CTime { get; set; }

        public static List<Satellite> _satelliteCache = null;
        public List<Satellite> Cache
        {
            get
            {
                if (_satelliteCache == null)
                {
                    _satelliteCache = SelectAll();
                }
                return _satelliteCache;
            }
            set{}
        }
        #endregion

        #region -Private Methods-
        private OracleParameter PrepareRefCursor()
        {
            return new OracleParameter()
            {
                ParameterName = "o_Cursor",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.RefCursor
            };
        }

        private OracleParameter PrepareOutputResult()
        {
            return new OracleParameter()
            {
                ParameterName = "v_Result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Int32,
            };
        }
        #endregion

        #region -Public Method-
        /// <summary>
        /// 根据ID获得卫星信息实体
        /// </summary>
        /// <returns>卫星信息实体</returns>
        public Satellite SelectByID()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_SATELLITE_SelectByID", new OracleParameter[] { new OracleParameter("p_WXBM", Id), o_Cursor });

            Satellite info = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                info = new Satellite()
                {
                    Id = ds.Tables[0].Rows[0]["WXBM"].ToString(),
                    WXBM = ds.Tables[0].Rows[0]["WXBM"].ToString(),
                    WXMC = ds.Tables[0].Rows[0]["WXMC"].ToString(),
                    WXBS = ds.Tables[0].Rows[0]["WXBS"].ToString(),
                    State = ds.Tables[0].Rows[0]["State"].ToString(),
                    MZB = Convert.ToInt32(ds.Tables[0].Rows[0]["MZB"]),
                    BMFSXS = Convert.ToInt32(ds.Tables[0].Rows[0]["BMFSXS"]),
                    SX = ds.Tables[0].Rows[0]["SX"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["SX"].ToString(),
                    GN = ds.Tables[0].Rows[0]["GN"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["GN"].ToString(),
                    CTime = ds.Tables[0].Rows[0]["CTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["CTime"].ToString())
                };
            }
            return info;
        }

        /// <summary>
        /// 获得卫星信息实体列表
        /// </summary>
        /// <returns>卫星信息实体列表</returns>
        public List<Satellite> SelectAll()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_SATELLITE_SelectAll", new OracleParameter[] { o_Cursor });

            List<Satellite> infoList = new List<Satellite>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Satellite info = new Satellite()
                    {
                        Id = dr["WXBM"].ToString(),
                        WXBM = dr["WXBM"].ToString(),     
                        WXMC = dr["WXMC"].ToString(),
                        WXBS = dr["WXBS"].ToString(),
                        State = dr["State"].ToString(),
                        MZB = Convert.ToInt32(dr["MZB"]),
                        BMFSXS = Convert.ToInt32(dr["BMFSXS"]),
                        SX = dr["SX"] == DBNull.Value ? string.Empty : dr["SX"].ToString(),
                        GN = dr["GN"] == DBNull.Value ? string.Empty : dr["GN"].ToString(),
                        CTime = dr["CTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["CTime"].ToString())
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }

        /// <summary>
        /// 根据条件查询卫星信息
        /// </summary>
        /// <param name="wxmc">卫星名称</param>
        /// <param name="wxbm">卫星编码</param>
        /// <param name="wxbs">卫星标识</param>
        /// <param name="state">状态；可用:0;不可用:1</param>
        /// <returns></returns>
        public List<Satellite> Search(string wxmc, string wxbm, string wxbs, int state)
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_Satellite_Search", new OracleParameter[] { 
                                                    new OracleParameter("p_WXMC", string.IsNullOrEmpty(wxmc) ? DBNull.Value as object : wxmc), 
                                                    new OracleParameter("p_WXBM", string.IsNullOrEmpty(wxbm) ? DBNull.Value as object : wxbm), 
                                                    new OracleParameter("p_WXBS", string.IsNullOrEmpty(wxbs) ? DBNull.Value as object : wxbs),
                                                    new OracleParameter("p_State", state < 0 ? DBNull.Value as object : state), 
                                                    o_Cursor });

            List<Satellite> infoList = new List<Satellite>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Satellite info = new Satellite()
                    {
                        Id = dr["WXBM"].ToString(),
                        WXBM = dr["WXBM"].ToString(),
                        WXMC = dr["WXMC"].ToString(),
                        WXBS = dr["WXBS"].ToString(),
                        State = dr["State"].ToString(),
                        MZB = Convert.ToInt32(dr["MZB"]),
                        BMFSXS = Convert.ToInt32(dr["BMFSXS"]),
                        SX = dr["SX"] == DBNull.Value ? string.Empty : dr["SX"].ToString(),
                        GN = dr["GN"] == DBNull.Value ? string.Empty : dr["GN"].ToString(),
                        CTime = dr["CTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["CTime"].ToString())
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }
        
        /// <summary>
        /// Insert a Task.
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
            _dataBase.SpExecuteNonQuery(s_up_sat_insert, new OracleParameter[]{
                new OracleParameter("p_WXMC",this.WXMC),
                new OracleParameter("p_WXBM",this.WXBM),
                new OracleParameter("p_WXBS",this.WXBS),
                new OracleParameter("p_State",this.State),
                new OracleParameter("p_MZB",this.MZB),
                new OracleParameter("p_BMFSXS",this.BMFSXS),
                new OracleParameter("p_SX",string.IsNullOrEmpty(this.SX) ? DBNull.Value as Object : this.SX),
                new OracleParameter("p_GN",string.IsNullOrEmpty(this.GN) ? DBNull.Value as Object : this.GN),
                new OracleParameter("p_CTime", CTime == DateTime.MinValue ? DateTime.Now : CTime),
                p
            });
            //清除缓存
            _satelliteCache = null;
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
            _dataBase.SpExecuteNonQuery(s_up_sat_update, new OracleParameter[]{
                new OracleParameter("p_WXMC",this.WXMC),
                new OracleParameter("p_WXBM",this.WXBM),
                new OracleParameter("p_WXBS",this.WXBS),
                new OracleParameter("p_State",this.State),
                new OracleParameter("p_MZB",this.MZB),
                new OracleParameter("p_BMFSXS",this.BMFSXS),
                new OracleParameter("p_SX",string.IsNullOrEmpty(this.SX) ? DBNull.Value as Object : this.SX),
                new OracleParameter("p_GN",string.IsNullOrEmpty(this.GN) ? DBNull.Value as Object : this.GN),
                //new OracleParameter("p_CTime", CTime == DateTime.MinValue ? DateTime.Now : CTime),
                p
            });
            //清除缓存
            _satelliteCache = null;
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }

        /// <summary>
        /// 根据WXBM获得卫星名称
        /// </summary>
        /// <param name="wxbm">卫星编码</param>
        /// <returns>卫星名称</returns>
        public string GetName(string wxbm)
        {
            string wxmc = string.Empty;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.WXBM == wxbm);
                if (query != null && query.Count() > 0)
                    wxmc = query.FirstOrDefault().WXMC;
            }
            return wxmc;
        }

        /// <summary>
        /// 根据WXBM获得卫星标识
        /// </summary>
        /// <param name="wxbm">卫星编码</param>
        /// <returns>卫星标识</returns>
        public string GetWXBS(string wxbm)
        {
            string wxbs = string.Empty;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.WXBM == wxbm);
                if (query != null && query.Count() > 0)
                    wxbs = query.FirstOrDefault().WXBS;
            }
            return wxbs;
        }

        /// <summary>
        /// 校验该卫星名称是否已经存在
        /// </summary>
        /// <returns>true:已经存在</returns>
        public bool HaveActiveWXMC()
        {
            List<Satellite> infoList = Search(WXMC, string.Empty, string.Empty, -1);
            var query = infoList.Where(a =>a.Id.ToLower() != (Id == null ? string.Empty : Id.ToLower()) && a.WXMC.ToLower() == WXMC.ToLower());
            if (query != null && query.Count() > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 校验该卫星编码是否已经存在
        /// </summary>
        /// <returns>true:已经存在</returns>
        public bool HaveActiveWXBM()
        {
            List<Satellite> infoList = Search(string.Empty, WXBM, string.Empty, -1);
            var query = infoList.Where(a => a.Id.ToLower() != (Id == null ? string.Empty : Id.ToLower()) && a.WXBM.ToLower() == WXBM.ToLower());
            if (query != null && query.Count() > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 校验该卫星标识是否已经存在
        /// </summary>
        /// <returns>true:已经存在</returns>
        public bool HaveActiveWXBS()
        {
            List<Satellite> infoList = Search(string.Empty, string.Empty, WXBS, -1);
            var query = infoList.Where(a => a.Id.ToLower() != (Id == null ? string.Empty : Id.ToLower()) && a.WXBS.ToLower() == WXBS.ToLower());
            if (query != null && query.Count() > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
        }
        #endregion
    }
}
