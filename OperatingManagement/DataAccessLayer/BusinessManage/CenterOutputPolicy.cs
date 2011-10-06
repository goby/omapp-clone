using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;
using Oracle.DataAccess.Client;

namespace OperatingManagement.DataAccessLayer.BusinessManage
{
    [Serializable]
    public class CenterOutputPolicy : BaseEntity<int, CenterOutputPolicy>
    {
        public CenterOutputPolicy()
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region Properties
        private OracleDatabase _dataBase = null;

        /// <summary>
        /// 信源
        /// </summary>
        public string InfoFrom { get; set; }
        /// <summary>
        /// 信息类别
        /// </summary>
        public string InfoType { get; set; }
        /// <summary>
        /// 信宿
        /// </summary>
        public string InfoTo { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public int CreatedUserID { get; set; }
        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public int UpdatedUserID { get; set; }
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
        /// 根据ID获得中心输出策略
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CenterOutputPolicy SelectByID()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_COP_SelectByID", new OracleParameter[] { new OracleParameter("p_COPID", this.Id), o_Cursor });

            CenterOutputPolicy info = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                info = new CenterOutputPolicy()
                {
                    Id = Convert.ToInt32(ds.Tables[0].Rows[0]["COPID"]),
                    InfoFrom = ds.Tables[0].Rows[0]["InfoFrom"].ToString(),
                    InfoType = ds.Tables[0].Rows[0]["InfoType"].ToString(),
                    InfoTo = ds.Tables[0].Rows[0]["InfoTo"].ToString(),
                    Note = ds.Tables[0].Rows[0]["Note"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["Note"].ToString(),
                    CreatedTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedTime"]),
                    CreatedUserID = ds.Tables[0].Rows[0]["CreatedUserID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedUserID"]),
                    UpdatedTime = ds.Tables[0].Rows[0]["UpdatedTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedTime"]),
                    UpdatedUserID = ds.Tables[0].Rows[0]["UpdatedUserID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["UpdatedUserID"])
                };
            }
            return info;
        }

        /// <summary>
        /// 获得中心输出策略
        /// </summary>
        /// <returns></returns>
        public List<CenterOutputPolicy> SelectAll()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_COP_SelectAll", new OracleParameter[] { o_Cursor });

            List<CenterOutputPolicy> infoList = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                infoList = new List<CenterOutputPolicy>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    infoList.Add(new CenterOutputPolicy()
                    {
                        Id = Convert.ToInt32(dr["COPID"]),
                        InfoFrom = dr["InfoFrom"].ToString(),
                        InfoType = dr["InfoType"].ToString(),
                        InfoTo = dr["InfoTo"].ToString(),
                        Note = dr["Note"] == DBNull.Value ? string.Empty : dr["Note"].ToString(),
                        CreatedTime = Convert.ToDateTime(dr["CreatedTime"]),
                        CreatedUserID = dr["CreatedUserID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedUserID"]),
                        UpdatedTime = dr["UpdatedTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["UpdatedTime"]),
                        UpdatedUserID = dr["UpdatedUserID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["UpdatedUserID"])
                    });
                }
            }
            return infoList;
        }

        /// <summary>
        /// 添加中心输出策略
        /// </summary>
        /// <returns></returns>
        public FieldVerifyResult Add()
        {
            OracleParameter v_Result = PrepareOutputResult();
            OracleParameter v_ID = new OracleParameter()
            {
                ParameterName = "v_COPID",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Int32
            };

            _dataBase.SpExecuteNonQuery("UP_COP_Insert", new OracleParameter[]{
                                        new OracleParameter("p_InfoFrom",this.InfoFrom),
                                        new OracleParameter("p_InfoType", this.InfoType),
                                        new OracleParameter("p_InfoTo",this.InfoTo),
                                        new OracleParameter("p_Note", string.IsNullOrEmpty(this.Note) ? DBNull.Value as object : this.Note),
                                        new OracleParameter("p_CreatedTime",this.CreatedTime),
                                        new OracleParameter("p_CreatedUserID",this.CreatedUserID == 0 ? DBNull.Value as object : this.CreatedUserID),
                                        new OracleParameter("p_UpdatedTime",this.UpdatedTime == DateTime.MinValue ? DBNull.Value as object : this.UpdatedTime),
                                        new OracleParameter("p_UpdatedUserID",this.UpdatedUserID == 0 ? DBNull.Value as object : this.UpdatedUserID),
                                        v_ID,
                                        v_Result});
            if (v_ID.Value != null && v_ID.Value != DBNull.Value)
                this.Id = Convert.ToInt32(v_ID.Value);
            return (FieldVerifyResult)Convert.ToInt32(v_Result.Value);
        }

        /// <summary>
        /// 根据ID更新中心输出策略
        /// </summary>
        /// <returns></returns>
        public FieldVerifyResult Update()
        {
            OracleParameter v_Result = PrepareOutputResult();

            _dataBase.SpExecuteNonQuery("UP_COP_Update", new OracleParameter[]{
                                        new OracleParameter("p_COPID",this.Id),
                                        new OracleParameter("p_InfoFrom",this.InfoFrom),
                                        new OracleParameter("p_InfoType", this.InfoType),
                                        new OracleParameter("p_InfoTo",this.InfoTo),
                                        new OracleParameter("p_Note", string.IsNullOrEmpty(this.Note) ? DBNull.Value as object : this.Note),
                                        new OracleParameter("p_CreatedTime",this.CreatedTime),
                                        new OracleParameter("p_CreatedUserID",this.CreatedUserID == 0 ? DBNull.Value as object : this.CreatedUserID),
                                        new OracleParameter("p_UpdatedTime",this.UpdatedTime == DateTime.MinValue ? DBNull.Value as object : this.UpdatedTime),
                                        new OracleParameter("p_UpdatedUserID",this.UpdatedUserID == 0 ? DBNull.Value as object : this.UpdatedUserID),
                                        v_Result});
            return (FieldVerifyResult)Convert.ToInt32(v_Result.Value);
        }

        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {

        }
        #endregion

    }
}
