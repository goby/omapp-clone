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
    public class CommunicationResource : BaseEntity<int, CenterOutputPolicy>
    {
        public CommunicationResource()
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region Properties
        private OracleDatabase _dataBase = null;
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteCode { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// 带宽
        /// </summary>
        public string BandWidth { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 扩展属性
        /// </summary>
        public string ExtProperties { get; set; }
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
        /// 根据ID获得通信资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommunicationResource SelectByID()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_ComRes_SelectByID", new OracleParameter[] { new OracleParameter("p_CRID", Id), o_Cursor });

            CommunicationResource info = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                info = new CommunicationResource()
                {
                    Id = Convert.ToInt32(ds.Tables[0].Rows[0]["CRID"]),
                    RouteName = ds.Tables[0].Rows[0]["RouteName"].ToString(),
                    RouteCode = ds.Tables[0].Rows[0]["RouteCode"].ToString(),
                    Direction = ds.Tables[0].Rows[0]["Direction"].ToString(),
                    BandWidth = ds.Tables[0].Rows[0]["BandWidth"].ToString(),
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]),
                    ExtProperties = ds.Tables[0].Rows[0]["ExtProperties"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ExtProperties"].ToString(),
                    CreatedTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedTime"]),
                    CreatedUserID = ds.Tables[0].Rows[0]["CreatedUserID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedUserID"]),
                    UpdatedTime = ds.Tables[0].Rows[0]["UpdatedTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedTime"]),
                    UpdatedUserID = ds.Tables[0].Rows[0]["UpdatedUserID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["UpdatedUserID"])
                };
            }
            return info;
        }

        /// <summary>
        /// 获得所有通信资源列表
        /// </summary>
        /// <returns></returns>
        public List<CommunicationResource> SelectAll()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_ComRes_SelectAll", new OracleParameter[] { o_Cursor });

            List<CommunicationResource> infoList = new List<CommunicationResource>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CommunicationResource info = new CommunicationResource()
                    {
                        Id = Convert.ToInt32(ds.Tables[0].Rows[0]["CRID"]),
                        RouteName = ds.Tables[0].Rows[0]["RouteName"].ToString(),
                        RouteCode = ds.Tables[0].Rows[0]["RouteCode"].ToString(),
                        Direction = ds.Tables[0].Rows[0]["Direction"].ToString(),
                        BandWidth = ds.Tables[0].Rows[0]["BandWidth"].ToString(),
                        Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]),
                        ExtProperties = ds.Tables[0].Rows[0]["ExtProperties"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ExtProperties"].ToString(),
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
        /// 根据通信资源在某个时间点状态做查询
        /// </summary>
        /// <param name="status">全部:"";正常:01;异常:02;占用中:03;已删除:04</param>
        /// <param name="timePoint">中心资源在某个时间点</param>
        /// <returns></returns>
        public List<CommunicationResource> Search(string status, DateTime timePoint)
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_ComRes_Search", new OracleParameter[] {  
                new OracleParameter("p_Status", status),
                new OracleParameter("p_TimePoint", timePoint),
                o_Cursor });

            List<CommunicationResource> infoList = new List<CommunicationResource>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CommunicationResource info = new CommunicationResource()
                    {
                        Id = Convert.ToInt32(ds.Tables[0].Rows[0]["CRID"]),
                        RouteName = ds.Tables[0].Rows[0]["RouteName"].ToString(),
                        RouteCode = ds.Tables[0].Rows[0]["RouteCode"].ToString(),
                        Direction = ds.Tables[0].Rows[0]["Direction"].ToString(),
                        BandWidth = ds.Tables[0].Rows[0]["BandWidth"].ToString(),
                        Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]),
                        ExtProperties = ds.Tables[0].Rows[0]["ExtProperties"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ExtProperties"].ToString(),
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
        /// 添加通信资源
        /// </summary>
        /// <returns></returns>
        public FieldVerifyResult Add()
        {
            OracleParameter v_Result = PrepareOutputResult();
            OracleParameter v_ID = new OracleParameter()
            {
                ParameterName = "v_CRID",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Int32
            };

            _dataBase.SpExecuteNonQuery("UP_ComRes_Insert", new OracleParameter[]{
                                        new OracleParameter("p_RouteName",RouteName),
                                        new OracleParameter("p_RouteCode",RouteCode),
                                        new OracleParameter("p_Direction",Direction),
                                        new OracleParameter("p_BandWidth",BandWidth),
                                        new OracleParameter("p_Status",Status),
                                        new OracleParameter("p_ExtProperties",string.IsNullOrEmpty(ExtProperties) ? DBNull.Value as object : ExtProperties),
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
        /// 根据ID更新通信资源
        /// </summary>
        /// <returns></returns>
        public FieldVerifyResult Update()
        {
            OracleParameter v_Result = PrepareOutputResult();

            _dataBase.SpExecuteNonQuery("UP_ComRes_Update", new OracleParameter[]{
                                        new OracleParameter("p_CRID",Id),
                                        new OracleParameter("p_RouteName",RouteName),
                                        new OracleParameter("p_RouteCode",RouteCode),
                                        new OracleParameter("p_Direction",Direction),
                                        new OracleParameter("p_BandWidth",BandWidth),
                                        new OracleParameter("p_Status",Status),
                                        new OracleParameter("p_ExtProperties",string.IsNullOrEmpty(ExtProperties) ? DBNull.Value as object : ExtProperties),
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
            this.AddValidRules("RouteName", "线路名称不能为空。", string.IsNullOrEmpty(RouteName));
            this.AddValidRules("RouteCode", "线路编号不能为空。", string.IsNullOrEmpty(RouteCode));
            this.AddValidRules("Direction", "方向不能为空。", string.IsNullOrEmpty(Direction));
            this.AddValidRules("BandWidth", "带宽不能为空。", string.IsNullOrEmpty(BandWidth));
        }
        #endregion
    }
}
