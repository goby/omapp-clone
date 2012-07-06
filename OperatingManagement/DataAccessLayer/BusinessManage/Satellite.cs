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
        /// 状态，T为可用，F为不可用
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
                    GN = ds.Tables[0].Rows[0]["GN"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["GN"].ToString()
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
                        GN = dr["GN"] == DBNull.Value ? string.Empty : dr["GN"].ToString()
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
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
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
        }
        #endregion
    }
}
