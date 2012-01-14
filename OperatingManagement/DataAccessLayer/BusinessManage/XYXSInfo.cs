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
    public class XYXSInfo : BaseEntity<int, XYXSInfo>
    {
        public XYXSInfo()
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }
        #region Properties
        private OracleDatabase _dataBase = null;

        /// <summary>
        /// 地址名称
        /// </summary>
        public string ADDRName { get; set; }
        /// <summary>
        /// 地址标识
        /// </summary>
        public string ADDRMARK { get; set; }
        /// <summary>
        /// 内部十六进制编码
        /// </summary>
        public string INCODE { get; set; }
        /// <summary>
        /// 外部十六进制编码
        /// </summary>
        public string EXCODE { get; set; }

        public static List<XYXSInfo> _xyxsInfoCache = null;
        public List<XYXSInfo> XYXSInfoCache
        {
            get
            {
                if (_xyxsInfoCache == null)
                {
                    _xyxsInfoCache = SelectAll();
                }
                return _xyxsInfoCache;
            }
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
        /// 根据ID获得信源信宿
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XYXSInfo SelectByID()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_XYXSINFO_SelectByID", new OracleParameter[] { new OracleParameter("p_RID", Id), o_Cursor });

            XYXSInfo info = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                info = new XYXSInfo()
                {
                    Id = Convert.ToInt32(ds.Tables[0].Rows[0]["RID"]),
                    ADDRName = ds.Tables[0].Rows[0]["ADDRName"].ToString(),
                    ADDRMARK = ds.Tables[0].Rows[0]["ADDRMARK"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ADDRMARK"].ToString(),
                    INCODE = ds.Tables[0].Rows[0]["INCODE"].ToString(),
                    EXCODE = ds.Tables[0].Rows[0]["EXCODE"].ToString()
                };
            }
            return info;
        }

        /// <summary>
        /// 获得信源信宿
        /// </summary>
        /// <returns></returns>
        public List<XYXSInfo> SelectAll()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_XYXSINFO_SelectAll", new OracleParameter[] { o_Cursor });

            List<XYXSInfo> infoList = new List<XYXSInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    XYXSInfo info = new XYXSInfo()
                    {
                        Id = Convert.ToInt32(dr["RID"]),
                        ADDRName = dr["ADDRName"].ToString(),
                        ADDRMARK = dr["ADDRMARK"] == DBNull.Value ? string.Empty : dr["ADDRMARK"].ToString(),
                        INCODE = dr["INCODE"].ToString(),
                        EXCODE = dr["EXCODE"].ToString()
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }
        /// <summary>
        /// 根据inCode获得信源信宿的ADDRName
        /// </summary>
        /// <param name="inCode"></param>
        /// <returns>信源信宿的地址</returns>
        public string GetXYXSADDRName(string inCode)
        {
            string addrName = string.Empty;
            if (XYXSInfoCache != null)
            {
                var query = XYXSInfoCache.Where(a => a.INCODE.ToLower() == inCode.ToLower());
                if (query != null && query.Count() > 0)
                    addrName = query.FirstOrDefault().ADDRName;
            }
            return addrName;
        }
        /// <summary>
        /// 根据rid获得信源信宿的ADDRName
        /// </summary>
        /// <param name="rid">编号</param>
        /// <returns>信源信宿的地址</returns>
        public string GetXYXSADDRName(int rid)
        {
            string addrName = string.Empty;
            if (XYXSInfoCache != null)
            {
                var query = XYXSInfoCache.Where(a => a.Id == rid);
                if (query != null && query.Count() > 0)
                    addrName = query.FirstOrDefault().ADDRName;
            }
            return addrName;
        }
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
        }
        #endregion
    }
}
