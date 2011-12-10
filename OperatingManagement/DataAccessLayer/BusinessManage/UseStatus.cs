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
    public class UseStatus : BaseEntity<int, CenterOutputPolicy>
    {
        public UseStatus()
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region Properties
        private OracleDatabase _dataBase = null;
        /// <summary>
        /// 资源序号
        /// </summary>
        public int ResourceID { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public int ResourceType { get; set; }
        /// <summary>
        /// 占用类型
        /// </summary>
        public int UseType { get; set; }
        /// <summary>
        /// 占用起始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 占用结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 服务对象
        /// 可空
        /// </summary>
        public string UsedBy { get; set; }
        /// <summary>
        /// 服务种类
        /// 可空
        /// </summary>
        public string UsedCategory { get; set; }
        /// <summary>
        /// 占用原因
        /// 可空
        /// </summary>
        public string UsedFor { get; set; }
        /// <summary>
        /// 是否可执行任务
        /// 可空
        /// </summary>
        public int CanBeUsed { get; set; }
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
        /// 根据ID获得占用状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UseStatus SelectByID()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_UseStatus_SelectByID", new OracleParameter[] { new OracleParameter("p_USID", Id), o_Cursor });

            UseStatus info = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                info = new UseStatus()
                {
                    Id = Convert.ToInt32(ds.Tables[0].Rows[0]["USID"]),
                    ResourceID = Convert.ToInt32(ds.Tables[0].Rows[0]["ResourceID"]),
                    ResourceType = Convert.ToInt32(ds.Tables[0].Rows[0]["ResourceType"]),
                    UseType = Convert.ToInt32(ds.Tables[0].Rows[0]["UseType"]),
                    BeginTime = ds.Tables[0].Rows[0]["BeginTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["BeginTime"]),
                    EndTime = ds.Tables[0].Rows[0]["EndTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["EndTime"]),
                    UsedBy = ds.Tables[0].Rows[0]["UsedBy"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["UsedBy"].ToString(),
                    UsedCategory = ds.Tables[0].Rows[0]["UsedCategory"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["UsedCategory"].ToString(),
                    UsedFor = ds.Tables[0].Rows[0]["UsedFor"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["UsedFor"].ToString(),
                    CanBeUsed = ds.Tables[0].Rows[0]["CanBeUsed"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["CanBeUsed"]),
                    CreatedTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedTime"]),
                    CreatedUserID = ds.Tables[0].Rows[0]["CreatedUserID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedUserID"]),
                    UpdatedTime = ds.Tables[0].Rows[0]["UpdatedTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedTime"]),
                    UpdatedUserID = ds.Tables[0].Rows[0]["UpdatedUserID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["UpdatedUserID"])
                };
            }
            return info;
        }

        /// <summary>
        /// 获得所有占用状态列表
        /// </summary>
        /// <returns></returns>
        public List<UseStatus> SelectAll()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_UseStatus_SelectAll", new OracleParameter[] { o_Cursor });

            List<UseStatus> infoList = new List<UseStatus>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UseStatus info = new UseStatus()
                    {
                        Id = Convert.ToInt32(ds.Tables[0].Rows[0]["USID"]),
                        ResourceID = Convert.ToInt32(ds.Tables[0].Rows[0]["ResourceID"]),
                        ResourceType = Convert.ToInt32(ds.Tables[0].Rows[0]["ResourceType"]),
                        UseType = Convert.ToInt32(ds.Tables[0].Rows[0]["UseType"]),
                        BeginTime = ds.Tables[0].Rows[0]["BeginTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["BeginTime"]),
                        EndTime = ds.Tables[0].Rows[0]["EndTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["EndTime"]),
                        UsedBy = ds.Tables[0].Rows[0]["UsedBy"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["UsedBy"].ToString(),
                        UsedCategory = ds.Tables[0].Rows[0]["UsedCategory"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["UsedCategory"].ToString(),
                        UsedFor = ds.Tables[0].Rows[0]["UsedFor"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["UsedFor"].ToString(),
                        CanBeUsed = ds.Tables[0].Rows[0]["CanBeUsed"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["CanBeUsed"]),
                        CreatedTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedTime"]),
                        CreatedUserID = ds.Tables[0].Rows[0]["CreatedUserID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedUserID"]),
                        UpdatedTime = ds.Tables[0].Rows[0]["UpdatedTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedTime"]),
                        UpdatedUserID = ds.Tables[0].Rows[0]["UpdatedUserID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["UpdatedUserID"])
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }

        /// <summary>
        /// 添加占用状态
        /// </summary>
        /// <returns></returns>
        public FieldVerifyResult Add()
        {
            OracleParameter v_Result = PrepareOutputResult();
            OracleParameter v_ID = new OracleParameter()
            {
                ParameterName = "v_USID",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Int32
            };

            _dataBase.SpExecuteNonQuery("UP_UseStatus_Insert", new OracleParameter[]{
                                        new OracleParameter("p_ResourceID",ResourceID),
                                        new OracleParameter("p_ResourceType",ResourceType),
                                        new OracleParameter("p_UseType", UseType),
                                        new OracleParameter("p_BeginTime",BeginTime == DateTime.MinValue ? DBNull.Value as object : BeginTime),
                                        new OracleParameter("p_EndTime", EndTime == DateTime.MinValue ? DBNull.Value as object : EndTime),
                                        new OracleParameter("p_UsedBy",string.IsNullOrEmpty(UsedBy) ?  DBNull.Value as object : UsedBy),
                                        new OracleParameter("p_UsedCategory",string.IsNullOrEmpty(UsedCategory) ?  DBNull.Value as object : UsedCategory),
                                        new OracleParameter("p_UsedFor",string.IsNullOrEmpty(UsedFor) ?  DBNull.Value as object : UsedFor),
                                        new OracleParameter("p_CanBeUsed",CanBeUsed == 0 ?  DBNull.Value as object : CanBeUsed),
                                        new OracleParameter("p_CreatedTime",CreatedTime),
                                        new OracleParameter("p_CreatedUserID",CreatedUserID == 0 ? DBNull.Value as object : CreatedUserID),
                                        new OracleParameter("p_UpdatedTime",UpdatedTime == DateTime.MinValue ? DBNull.Value as object : UpdatedTime),
                                        new OracleParameter("p_UpdatedUserID",UpdatedUserID == 0 ? DBNull.Value as object : UpdatedUserID),
                                        v_ID,
                                        v_Result});
            if (v_ID.Value != null && v_ID.Value != DBNull.Value)
                this.Id = Convert.ToInt32(v_ID.Value);
            return (FieldVerifyResult)Convert.ToInt32(v_Result.Value);
        }

        /// <summary>
        /// 根据ID更新占用状态
        /// </summary>
        /// <returns></returns>
        public FieldVerifyResult Update()
        {
            OracleParameter v_Result = PrepareOutputResult();

            _dataBase.SpExecuteNonQuery("UP_UseStatus_Update", new OracleParameter[]{
                                        new OracleParameter("p_USID",Id),
                                        new OracleParameter("p_ResourceID",ResourceID),
                                        new OracleParameter("p_ResourceType",ResourceType),
                                        new OracleParameter("p_UseType", UseType),
                                        new OracleParameter("p_BeginTime",BeginTime == DateTime.MinValue ? DBNull.Value as object : BeginTime),
                                        new OracleParameter("p_EndTime", EndTime == DateTime.MinValue ? DBNull.Value as object : EndTime),
                                        new OracleParameter("p_UsedBy",string.IsNullOrEmpty(UsedBy) ?  DBNull.Value as object : UsedBy),
                                        new OracleParameter("p_UsedCategory",string.IsNullOrEmpty(UsedCategory) ?  DBNull.Value as object : UsedCategory),
                                        new OracleParameter("p_UsedFor",string.IsNullOrEmpty(UsedFor) ?  DBNull.Value as object : UsedFor),
                                        new OracleParameter("p_CanBeUsed",CanBeUsed == 0 ?  DBNull.Value as object : CanBeUsed),
                                        new OracleParameter("p_CreatedTime",CreatedTime),
                                        new OracleParameter("p_CreatedUserID",CreatedUserID == 0 ? DBNull.Value as object : CreatedUserID),
                                        new OracleParameter("p_UpdatedTime",UpdatedTime == DateTime.MinValue ? DBNull.Value as object : UpdatedTime),
                                        new OracleParameter("p_UpdatedUserID",UpdatedUserID == 0 ? DBNull.Value as object : UpdatedUserID),
                                        v_Result});
            return (FieldVerifyResult)Convert.ToInt32(v_Result.Value);
        }

        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
            this.AddValidRules("ResourceID", "资源序号不能为空。", (ResourceID == 0));
            this.AddValidRules("ResourceType", "资源类型不能为空。", (ResourceType == 0));
            this.AddValidRules("UseType", "占用类型不能为空。", (UseType == 0));
            this.AddValidRules("BeginTime", "占用起始时间不能为空。", (BeginTime == DateTime.MinValue));
            this.AddValidRules("EndTime", "占用结束时间不能为空。", (EndTime == DateTime.MinValue));
        }
        #endregion
    }
}
