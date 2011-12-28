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
    public class XXTYPE : BaseEntity<int, XXTYPE>
    {
        public XXTYPE()
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region Properties
        private OracleDatabase _dataBase = null;

        /// <summary>
        /// 信息名称
        /// </summary>
        public string DATANAME { get; set; }
        /// <summary>
        /// 内部标识
        /// </summary>
        public string INMARK { get; set; }
        /// <summary>
        /// 外部标识
        /// </summary>
        public string EXMARK { get; set; }
        /// <summary>
        /// 内部编码
        /// </summary>
        public string INCODE { get; set; }
        /// <summary>
        /// 外部编码
        /// </summary>
        public string EXCODE { get; set; }
        /// <summary>
        /// 信息类型：0-数据帧；1-文件
        /// </summary>
        public string DATATYPE { get; set; }

        //public List<XYXSInfo> XYXSInfoCache
        //{
        //    get
        //    {

        //    }
        //}
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
        /// 根据ID获得信息类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XXTYPE SelectByID()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_XXTYPE_SelectByID", new OracleParameter[] { new OracleParameter("p_RID", Id), o_Cursor });

            XXTYPE info = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                info = new XXTYPE()
                {
                    Id = Convert.ToInt32(ds.Tables[0].Rows[0]["RID"]),
                    DATANAME = ds.Tables[0].Rows[0]["DATANAME"].ToString(),
                    INMARK = ds.Tables[0].Rows[0]["INMARK"].ToString(),
                    EXMARK = ds.Tables[0].Rows[0]["EXMARK"].ToString(),
                    INCODE = ds.Tables[0].Rows[0]["INCODE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["INCODE"].ToString(),
                    EXCODE = ds.Tables[0].Rows[0]["EXCODE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EXCODE"].ToString(),
                    DATATYPE = ds.Tables[0].Rows[0]["DATATYPE"].ToString()
                };
            }
            return info;
        }

        /// <summary>
        /// 获得信息类型
        /// </summary>
        /// <returns></returns>
        public List<XXTYPE> SelectAll()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_XXTYPE_SelectAll", new OracleParameter[] { o_Cursor });

            List<XXTYPE> infoList = new List<XXTYPE>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    XXTYPE info = new XXTYPE()
                    {
                        Id = Convert.ToInt32(dr["RID"]),
                        DATANAME = dr["DATANAME"].ToString(),
                        INMARK = dr["INMARK"].ToString(),
                        EXMARK = dr["EXMARK"].ToString(),
                        INCODE = dr["INCODE"] == DBNull.Value ? string.Empty : dr["INCODE"].ToString(),
                        EXCODE = dr["EXCODE"] == DBNull.Value ? string.Empty : dr["EXCODE"].ToString(),
                        DATATYPE = dr["DATATYPE"].ToString()
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
        }
        #endregion
    }
}
