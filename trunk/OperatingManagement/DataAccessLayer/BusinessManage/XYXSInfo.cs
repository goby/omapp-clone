#region
//------------------------------------------------------
//Assembly:OperatingManagement.DataAccessLayer
//FileName:XYXSInfo.cs
//Remark:信源信宿读取类
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
        /// <summary>
        /// 主IP地址
        /// </summary>
        public string MainIP { get; set; }
        /// <summary>
        /// 主端口
        /// </summary>
        public int MainPort { get; set; }
        /// <summary>
        /// 副IP地址
        /// </summary>
        public string BakIP { get; set; }
        /// <summary>
        /// 副端口
        /// </summary>
        public int BakPort { get; set; }

        public static List<XYXSInfo> _xyxsInfoCache = null;
        public List<XYXSInfo> Cache
        {
            get
            {
                if (_xyxsInfoCache == null)
                {
                    _xyxsInfoCache = SelectAll();
                }
                return _xyxsInfoCache;
            }
            set
            {
                _xyxsInfoCache = value;
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
                    EXCODE = ds.Tables[0].Rows[0]["EXCODE"].ToString(),
                    MainIP = ds.Tables[0].Rows[0]["MainIP"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MainIP"].ToString(),
                    MainPort = Convert.ToInt32(ds.Tables[0].Rows[0]["MainPort"].ToString()),
                    BakIP = ds.Tables[0].Rows[0]["BakIP"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["BakIP"].ToString(),
                    BakPort = Convert.ToInt32(ds.Tables[0].Rows[0]["BakPort"].ToString())
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
                        EXCODE = dr["EXCODE"].ToString(),
                        MainIP = ds.Tables[0].Rows[0]["MainIP"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["MainIP"].ToString(),
                        MainPort = ds.Tables[0].Rows[0]["MainPort"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["MainPort"].ToString()),
                        BakIP = ds.Tables[0].Rows[0]["BakIP"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["BakIP"].ToString(),
                        BakPort = ds.Tables[0].Rows[0]["BakPort"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["BakPort"].ToString())
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
        public string GetName(string inCode)
        {
            string addrName = string.Empty;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.INCODE.ToLower() == inCode.ToLower());
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
        public string GetName(int rid)
        {
            string addrName = string.Empty;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.Id == rid);
                if (query != null && query.Count() > 0)
                    addrName = query.FirstOrDefault().ADDRName;
            }
            return addrName;
        }

        /// <summary>
        /// 通过IP地址获取信源信宿信息
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public XYXSInfo GetByIP(string ipAddress)
        {
            if (Cache != null)
            {
                var query = Cache.Where(a => a.MainIP == ipAddress || a.BakIP == ipAddress);
                if (query != null && query.Count() > 0)
                    return (XYXSInfo)query.FirstOrDefault();
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// 通过id获取信源信宿信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XYXSInfo GetByID(int id)
        {
            if (Cache != null)
            {
                var query = Cache.Where(a => a.Id == id);
                if (query != null && query.Count() > 0)
                    return (XYXSInfo)query.FirstOrDefault();
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取信源信宿的IP地址和端口
        /// </summary>
        /// <param name="id"></param>
        public bool GetIPandPort(out string ip, out int port)
        {
            ip = "";
            port = 0;
            if (this != null)
            {
                ip = string.IsNullOrEmpty(this.MainIP) ? this.MainIP : this.BakIP;
                port = (this.MainPort == 0) ? this.MainPort : this.BakPort;
                return true;
            }
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
