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

        public XYXSInfo(DataRow dr)
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
            this.Id = Convert.ToInt32(dr["RID"]);
            this.ADDRName = dr["ADDRName"].ToString();
            this.ADDRMARK = dr["ADDRMARK"] == DBNull.Value ? string.Empty : dr["ADDRMARK"].ToString();
            this.INCODE = dr["INCODE"].ToString();
            this.EXCODE = dr["EXCODE"].ToString();
            this.MainIP = dr["MainIP"] == DBNull.Value ? string.Empty : dr["MainIP"].ToString();
            this.TCPPort = dr["TCPPort"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TCPPort"].ToString());
            this.BakIP = dr["BakIP"] == DBNull.Value ? string.Empty : dr["BakIP"].ToString();
            this.UDPPort = dr["UDPPort"] == DBNull.Value ? 0 : Convert.ToInt32(dr["UDPPort"].ToString());

            if (dr["FTPPath"] != null)
            {
                string ftps = dr["FTPPath"].ToString();
                if (!String.IsNullOrEmpty(ftps))
                {
                    string[] strFTP = ftps.Split(new char[] { '@' });
                    this.FTPPath = strFTP[0];
                    this.FTPUser = strFTP[1];
                    this.FTPPwd = strFTP[2];
                }

            }
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
        /// TCP端口
        /// </summary>
        public int TCPPort { get; set; }

        /// <summary>
        /// 副IP地址
        /// </summary>
        public string BakIP { get; set; }

        /// <summary>
        /// UDP端口
        /// </summary>
        public int UDPPort { get; set; }

        /// <summary>
        /// FTPPath
        /// </summary>
        public string FTPPath { get; set; }

        /// <summary>
        /// FTP User Name
        /// </summary>
        public string FTPUser { get; set; }

        /// <summary>
        /// FTP User Password
        /// </summary>
        public string FTPPwd { get; set; }

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
                info = new XYXSInfo(ds.Tables[0].Rows[0]);
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
                    XYXSInfo info = new XYXSInfo(dr);

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
        /// 根据inCode获得信源信宿的Id
        /// </summary>
        /// <param name="addrMark">编号</param>
        /// <returns>信源信宿的地址</returns>
        public int GetIdByInCode(string inCode)
        {
            int xid = 0;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.INCODE == inCode);
                if (query != null && query.Count() > 0)
                    xid = query.FirstOrDefault().Id;
            }
            return xid;
        }

        /// <summary>
        /// 根据ADDRMark获得信源信宿的Id
        /// </summary>
        /// <param name="addrMark">编号</param>
        /// <returns>信源信宿的地址</returns>
        public int GetIdByAddrMark(string addrMark)
        {
            int xid = 0;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.ADDRMARK == addrMark);
                if (query != null && query.Count() > 0)
                    xid = query.FirstOrDefault().Id;
            }
            return xid;
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
        /// 通过IP和port获取信源信宿信息(test)
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public XYXSInfo GetByIPAndPort(string ipAddress, int port)
        {
            if (Cache != null)
            {
                var query = Cache.Where(a => (a.MainIP == ipAddress || a.BakIP == ipAddress)
                    && (a.TCPPort == port || a.UDPPort == port));
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
        /// 根据AddrMark获取信源信息
        /// </summary>
        /// <param name="addrMark"></param>
        /// <returns></returns>
        public XYXSInfo GetByAddrMark(string addrMark)
        {
            if (Cache != null)
            {
                var query = Cache.Where(a => a.ADDRMARK == addrMark);
                if (query != null && query.Count() > 0)
                    return (XYXSInfo)query.FirstOrDefault();
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取信源信宿的IP地址和端口（tcp/udp）
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="tcpport"></param>
        /// <param name="udpport"></param>
        /// <returns></returns>
        public bool GetIPandPort(out string ip, out int tcpport, out int udpport)
        {
            ip = "";
            tcpport = 0;
            udpport = 0;
            if (this != null)
            {
                ip = !string.IsNullOrEmpty(this.MainIP) ? this.MainIP : this.BakIP;
                tcpport = this.TCPPort;
                udpport = this.UDPPort;
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
