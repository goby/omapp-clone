#region
//------------------------------------------------------
//Assembly:OperatingManagement.DataAccessLayer
//FileName:ZYSX.cs
//Remark:资源属性管理类
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20120825    Create     
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
    public class ZYSX : BaseEntity<int, GroundResource>
    {
        public ZYSX()
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region Properties
        private OracleDatabase _dataBase = null;
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PName { get; set; }
        /// <summary>
        /// 属性编码
        /// </summary>
        public string PCode { get; set; }
        /// <summary>
        /// 属性类型,1(int);2(double);3(string);4(bool);5(enum);
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 属性值区间,1(0-xxxxxx);2(m.n);3(length);5(a,b,c)';
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// 属于,0(卫星),1(地面站),2(卫星和地面站),3(都不属于)
        /// </summary>
        public int Own { get; set; }
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
        /// 根据ID获得资源属性实体
        /// </summary>
        /// <returns>资源属性实体</returns>
        public ZYSX SelectByID()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_ZYSX_SelectByID", new OracleParameter[] { new OracleParameter("p_ID", Id), o_Cursor });

            ZYSX info = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                info = new ZYSX()
                {
                    Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]),
                    PName = ds.Tables[0].Rows[0]["PName"].ToString(),
                    PCode = ds.Tables[0].Rows[0]["PCode"].ToString(),
                    Type = ds.Tables[0].Rows[0]["Type"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["Type"]),
                    Scope = ds.Tables[0].Rows[0]["Scope"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["Scope"].ToString(),
                    Own = ds.Tables[0].Rows[0]["Own"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["Own"]),
                };
            }
            return info;
        }

        /// <summary>
        /// 获得所有资源属性实体列表
        /// </summary>
        /// <returns>资源属性实体列表</returns>
        public List<ZYSX> SelectAll()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_ZYSX_SelectAll", new OracleParameter[] { o_Cursor });

            List<ZYSX> infoList = new List<ZYSX>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ZYSX info = new ZYSX()
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        PName = dr["PName"].ToString(),
                        PCode = dr["PCode"].ToString(),
                        Type = dr["Type"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Type"]),
                        Scope = dr["Scope"] == DBNull.Value ? string.Empty : dr["Scope"].ToString(),
                        Own = dr["Own"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Own"]),
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }
        /// <summary>
        /// 获得与地面站相关的资源属性列表
        /// </summary>
        /// <returns></returns>
        public List<ZYSX> GetGroundStationZYSXList()
        {
            List<ZYSX> infoList = SelectAll().Where(a => a.Own == 1 || a.Own == 2).OrderBy(a=> a.PName).ToList<ZYSX>();
            return infoList;
        }
        /// <summary>
        /// 校验扩展属性值的类型
        /// 属性类型,1(int);2(double);3(string);4(bool);5(enum);
        /// </summary>
        /// <returns>true:通过</returns>
        public bool ValidateValueRegular(object pValue)
        {
            bool result = false;
            switch (Type)
            {
                case 1:
                    int value1;
                    result = int.TryParse(pValue.ToString(), out value1);
                    break;
                case 2:
                    double value2;
                    result = double.TryParse(pValue.ToString(), out value2);
                    break;
                case 3:
                    result = !string.IsNullOrWhiteSpace(pValue.ToString());
                    break;
                case 4:
                    bool value4;
                    result = bool.TryParse(pValue.ToString(), out value4);
                    break;
                case 5:
                    result = true;
                    break;
                default:
                    result = true;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 校验扩展属性值的范围
        /// 属性值区间,1(0-xxxxxx);2(m.n);3(length);5(a,b,c)';
        /// </summary>
        /// <returns>true:通过</returns>
        public bool ValidateValueRange(object pValue)
        {
            bool result = false;
            try
            {
                switch (Type)
                {
                    case 1:
                        int value1;
                        if (int.TryParse(pValue.ToString(), out value1))
                        {
                            string[] scopeArray = Scope.Split(new char[] { '-', '_', '—', }, StringSplitOptions.RemoveEmptyEntries);
                            if (scopeArray != null && scopeArray.Length == 2)
                            {
                                int minValue1 = 0;
                                int maxValue1 = 0;
                                if (int.TryParse(scopeArray[0], out minValue1) && int.TryParse(scopeArray[1], out maxValue1))
                                {
                                    result = (value1 >= minValue1 && value1 <= maxValue1);
                                }
                            }
                        }
                        break;
                    case 2:
                        double value2;
                        if (double.TryParse(pValue.ToString(), out value2))
                        {
                            string[] mnArray = Scope.Split(new char[] { '.', ',', ';', '，', '；' }, StringSplitOptions.RemoveEmptyEntries);
                            if (mnArray != null && mnArray.Length == 2)
                            {
                                value2 = Math.Abs(value2);
                                int m = 0;
                                int n = 0;
                                if (int.TryParse(mnArray[0], out m) && int.TryParse(mnArray[1], out n))
                                {
                                    result = (value2.ToString().Split('.')[0].Length <= m && value2.ToString().Split('.')[1].Length <= n);
                                }
                            }
                        }
                        break;
                    case 3:
                        int length = 0;
                        if (int.TryParse(Scope, out length))
                        {
                            result = (pValue.ToString().Length <= length);
                        }
                        break;
                    case 4:
                        result = true;
                        break;
                    case 5:
                        string[] enumArray = Scope.Split(new char[] { '.', ',', ';', '，', '；' });
                        if (enumArray != null && enumArray.Length > 0)
                        {
                            result = enumArray.Contains(pValue.ToString());
                        }
                        break;
                    default:
                        result = true;
                        break;
                }
            }
            catch
            { }
            return result;
        }

        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
        }
        #endregion
    }

    [Serializable]
    public class ZYSXExt
    {
        public ZYSXExt()
        { 
        }
        public ZYSXExt(ZYSX zysx)
        {
            if (zysx != null)
            {
                ID = zysx.Id;
                PName = zysx.PName;
                PCode = zysx.PCode;
                Type = zysx.Type;
                Scope = zysx.Scope;
                Own = zysx.Own;
            }
        }
        #region Properties
        /// <summary>
        /// 属性ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PName { get; set; }
        /// <summary>
        /// 属性编码
        /// </summary>
        public string PCode { get; set; }
        /// <summary>
        /// 属性类型,1(int);2(double);3(string);4(bool);5(enum);
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 属性值区间,1(0-xxxxxx);2(m.n);3(length);5(a,b,c)';
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// 属于,0(卫星),1(地面站),2(卫星和地面站),3(都不属于)
        /// </summary>
        public int Own { get; set; }

        /// <summary>
        /// 资源属性值唯一编码，扩展，与数据库不对应
        /// </summary>
        public string PValueID { get; set; }
        /// <summary>
        /// 资源属性值，扩展，与数据库不对应
        /// </summary>
        public string PValue { get; set; }
        #endregion
    }
}
