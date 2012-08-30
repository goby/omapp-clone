#region
//------------------------------------------------------
//Assembly:OperatingManagement.DataAccessLayer
//FileName:GroundResource.cs
//Remark:地面资源管理类
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20111011    Create     
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
    public class GroundResource : BaseEntity<int, GroundResource>
    {
        public GroundResource()
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region Properties
        private OracleDatabase _dataBase = null;
        /// <summary>
        /// 地面站序号
        /// </summary>
        public int RID { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipmentCode { get; set; }
        /// <summary>
        /// 功能类型
        /// </summary>
        public string FunctionType { get; set; }
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
        public double CreatedUserID { get; set; }
        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public double UpdatedUserID { get; set; }

        /// <summary>
        /// 地面站名称
        /// </summary>
        public string AddrName { get; set; }
        /// <summary>
        /// 地面站编号
        /// </summary>
        public string AddrMark { get; set; }
        /// <summary>
        /// 管理单位
        /// </summary>
        public string Own { get; set; }
        /// <summary>
        /// 站址坐标
        /// </summary>
        public string Coordinate { get; set; }
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
        /// 根据ID获得地面站资源实体
        /// </summary>
        /// <returns>地面站资源实体</returns>
        public GroundResource SelectByID()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_GroundRes_SelectByID", new OracleParameter[] { new OracleParameter("p_GRID", Id), o_Cursor });

            GroundResource info = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                info = new GroundResource()
                {
                    Id = Convert.ToInt32(ds.Tables[0].Rows[0]["GRID"]),
                    RID = Convert.ToInt32(ds.Tables[0].Rows[0]["RID"]),
                    EquipmentName = ds.Tables[0].Rows[0]["EquipmentName"].ToString(),
                    EquipmentCode = ds.Tables[0].Rows[0]["EquipmentCode"].ToString(),
                    FunctionType = ds.Tables[0].Rows[0]["FunctionType"].ToString(),
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]),
                    ExtProperties = ds.Tables[0].Rows[0]["ExtProperties"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ExtProperties"].ToString(),
                    CreatedTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedTime"]),
                    CreatedUserID = ds.Tables[0].Rows[0]["CreatedUserID"] == DBNull.Value ? 0.0 : Convert.ToDouble(ds.Tables[0].Rows[0]["CreatedUserID"]),
                    UpdatedTime = ds.Tables[0].Rows[0]["UpdatedTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedTime"]),
                    UpdatedUserID = ds.Tables[0].Rows[0]["UpdatedUserID"] == DBNull.Value ? 0.0 : Convert.ToDouble(ds.Tables[0].Rows[0]["UpdatedUserID"]),

                    AddrName = ds.Tables[0].Rows[0]["AddrName"].ToString(),
                    AddrMark = ds.Tables[0].Rows[0]["AddrMark"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["AddrMark"].ToString(),
                    Own = ds.Tables[0].Rows[0]["Own"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["Own"].ToString(),
                    Coordinate = ds.Tables[0].Rows[0]["Coordinate"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["Coordinate"].ToString()
                };
            }
            return info;
        }

        /// <summary>
        /// 获得所有地面站资源实体列表
        /// </summary>
        /// <returns>地面站资源实体列表</returns>
        public List<GroundResource> SelectAll()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_GroundRes_SelectAll", new OracleParameter[] { o_Cursor });

            List<GroundResource> infoList = new List<GroundResource>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GroundResource info = new GroundResource()
                    {
                        Id = Convert.ToInt32(dr["GRID"]),
                        RID = Convert.ToInt32(dr["RID"]),
                        EquipmentName = dr["EquipmentName"].ToString(),
                        EquipmentCode = dr["EquipmentCode"].ToString(),
                        FunctionType = dr["FunctionType"].ToString(),
                        Status = Convert.ToInt32(dr["Status"]),
                        ExtProperties = dr["ExtProperties"] == DBNull.Value ? string.Empty : dr["ExtProperties"].ToString(),
                        CreatedTime = Convert.ToDateTime(dr["CreatedTime"]),
                        CreatedUserID = dr["CreatedUserID"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["CreatedUserID"]),
                        UpdatedTime = dr["UpdatedTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["UpdatedTime"]),
                        UpdatedUserID = dr["UpdatedUserID"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["UpdatedUserID"]),

                        AddrName = dr["AddrName"].ToString(),
                        AddrMark = dr["AddrMark"] == DBNull.Value ? string.Empty : dr["AddrMark"].ToString(),
                        Own = dr["Own"] == DBNull.Value ? string.Empty : dr["Own"].ToString(),
                        Coordinate = dr["Coordinate"] == DBNull.Value ? string.Empty : dr["Coordinate"].ToString()
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }

        /// <summary>
        /// 根据设备编号获得地面站资源实体
        /// </summary>
        /// <returns>地面站资源实体</returns>
        public GroundResource SelectByEquipmentCode()
        {
            return SelectAll().Where(a => a.Status == 1 && a.EquipmentCode.ToLower() == EquipmentCode.ToLower()).FirstOrDefault<GroundResource>();
        }

        /// <summary>
        /// 校验该地面站资源设备编号是否已经存在
        /// </summary>
        /// <returns>true:已经存在</returns>
        public bool HaveActiveEquipmentCode()
        {
            List<GroundResource> infoList = SelectAll();
            var query = infoList.Where(a => a.Id != Id && a.Status == 1 && a.EquipmentCode.ToLower() == EquipmentCode.ToLower());
            if (query != null && query.Count() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据地面站资源在某个时间点状态做查询
        /// </summary>
        /// <param name="status">全部:"";正常:1;异常:2;占用中:3;已删除:4</param>
        /// <param name="timePoint">地面站资源在某个时间点的状态</param>
        /// <returns>地面站资源实体列表</returns>
        public List<GroundResource> Search(string status, DateTime timePoint)
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_GroundRes_Search", new OracleParameter[] { 
                new OracleParameter("p_Status", status),
                new OracleParameter("p_TimePoint", timePoint),
                o_Cursor });

            List<GroundResource> infoList = new List<GroundResource>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GroundResource info = new GroundResource()
                    {
                        Id = Convert.ToInt32(dr["GRID"]),
                        RID = Convert.ToInt32(dr["RID"]),
                        EquipmentName = dr["EquipmentName"].ToString(),
                        EquipmentCode = dr["EquipmentCode"].ToString(),
                        FunctionType = dr["FunctionType"].ToString(),
                        Status = Convert.ToInt32(dr["Status"]),
                        ExtProperties = dr["ExtProperties"] == DBNull.Value ? string.Empty : dr["ExtProperties"].ToString(),
                        CreatedTime = Convert.ToDateTime(dr["CreatedTime"]),
                        CreatedUserID = dr["CreatedUserID"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["CreatedUserID"]),
                        UpdatedTime = dr["UpdatedTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["UpdatedTime"]),
                        UpdatedUserID = dr["UpdatedUserID"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["UpdatedUserID"]),

                        AddrName = dr["AddrName"].ToString(),
                        AddrMark = dr["AddrMark"] == DBNull.Value ? string.Empty : dr["AddrMark"].ToString(),
                        Own = dr["Own"] == DBNull.Value ? string.Empty : dr["Own"].ToString(),
                        Coordinate = dr["Coordinate"] == DBNull.Value ? string.Empty : dr["Coordinate"].ToString()
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }

        /// <summary>
        /// 根据地面站资源在某个时间段状态做查询
        /// </summary>
        /// <param name="status">全部:"";正常:1;异常:2;占用中:3;已删除:4</param>
        /// <param name="beginTime">地面站资源状态开始时间</param>
        /// <param name="endTime">地面站资源状态结束时间</param>
        /// <returns>地面站资源实体列表</returns>
        public List<GroundResource> Search(string status, DateTime beginTime, DateTime endTime)
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet("UP_GroundRes_SearchByPhase", new OracleParameter[] { 
                new OracleParameter("p_Status", status),
                new OracleParameter("p_BeginTime", beginTime),
                 new OracleParameter("p_EndTime", endTime),
                o_Cursor });

            List<GroundResource> infoList = new List<GroundResource>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GroundResource info = new GroundResource()
                    {
                        Id = Convert.ToInt32(dr["GRID"]),
                        RID = Convert.ToInt32(dr["RID"]),
                        EquipmentName = dr["EquipmentName"].ToString(),
                        EquipmentCode = dr["EquipmentCode"].ToString(),
                        FunctionType = dr["FunctionType"].ToString(),
                        Status = Convert.ToInt32(dr["Status"]),
                        ExtProperties = dr["ExtProperties"] == DBNull.Value ? string.Empty : dr["ExtProperties"].ToString(),
                        CreatedTime = Convert.ToDateTime(dr["CreatedTime"]),
                        CreatedUserID = dr["CreatedUserID"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["CreatedUserID"]),
                        UpdatedTime = dr["UpdatedTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["UpdatedTime"]),
                        UpdatedUserID = dr["UpdatedUserID"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["UpdatedUserID"]),

                        AddrName = dr["AddrName"].ToString(),
                        AddrMark = dr["AddrMark"] == DBNull.Value ? string.Empty : dr["AddrMark"].ToString(),
                        Own = dr["Own"] == DBNull.Value ? string.Empty : dr["Own"].ToString(),
                        Coordinate = dr["Coordinate"] == DBNull.Value ? string.Empty : dr["Coordinate"].ToString()
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }

        /// <summary>
        /// 添加地面站资源记录
        /// </summary>
        /// <returns>添加结果</returns>
        public FieldVerifyResult Add()
        {
            OracleParameter v_Result = PrepareOutputResult();
            OracleParameter v_ID = new OracleParameter()
            {
                ParameterName = "v_GRID",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Int32
            };

            _dataBase.SpExecuteNonQuery("UP_GroundRes_Insert", new OracleParameter[]{
                                        new OracleParameter("p_RID",RID),
                                        new OracleParameter("p_EquipmentName",EquipmentName),
                                        new OracleParameter("p_EquipmentCode",EquipmentCode),
                                        new OracleParameter("p_FunctionType",FunctionType),
                                        new OracleParameter("p_Status",Status),
                                        new OracleParameter("p_ExtProperties",string.IsNullOrEmpty(ExtProperties) ? DBNull.Value as object : ExtProperties),
                                        new OracleParameter("p_CreatedTime",CreatedTime),
                                        new OracleParameter("p_CreatedUserID",CreatedUserID == 0.0 ? DBNull.Value as object : CreatedUserID),
                                        new OracleParameter("p_UpdatedTime",UpdatedTime == DateTime.MinValue ? DBNull.Value as object : UpdatedTime),
                                        new OracleParameter("p_UpdatedUserID",UpdatedUserID == 0.0 ? DBNull.Value as object : UpdatedUserID),
                                        v_ID,
                                        v_Result});
            if (v_ID.Value != null && v_ID.Value != DBNull.Value)
                this.Id = Convert.ToInt32(v_ID.Value);
            return (FieldVerifyResult)Convert.ToInt32(v_Result.Value);
        }

        /// <summary>
        /// 根据ID更新地面站资源记录
        /// </summary>
        /// <returns>更新结果</returns>
        public FieldVerifyResult Update()
        {
            OracleParameter v_Result = PrepareOutputResult();

            _dataBase.SpExecuteNonQuery("UP_GroundRes_Update", new OracleParameter[]{
                                        new OracleParameter("p_GRID",Id),
                                        new OracleParameter("p_RID",RID),
                                        new OracleParameter("p_EquipmentName",EquipmentName),
                                        new OracleParameter("p_EquipmentCode",EquipmentCode),
                                        new OracleParameter("p_FunctionType",FunctionType),
                                        new OracleParameter("p_Status",Status),
                                        new OracleParameter("p_ExtProperties",string.IsNullOrEmpty(ExtProperties) ? DBNull.Value as object : ExtProperties),
                                        new OracleParameter("p_CreatedTime",CreatedTime),
                                        new OracleParameter("p_CreatedUserID",CreatedUserID == 0.0 ? DBNull.Value as object : CreatedUserID),
                                        new OracleParameter("p_UpdatedTime",UpdatedTime == DateTime.MinValue ? DBNull.Value as object : UpdatedTime),
                                        new OracleParameter("p_UpdatedUserID",UpdatedUserID == 0.0 ? DBNull.Value as object : UpdatedUserID),
                                        v_Result});
            return (FieldVerifyResult)Convert.ToInt32(v_Result.Value);
        }

        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
            this.AddValidRules("EquipmentName", "设备名称不能为空。", string.IsNullOrEmpty(EquipmentName));
            this.AddValidRules("EquipmentCode", "设备编号不能为空。", string.IsNullOrEmpty(EquipmentCode));
            this.AddValidRules("FunctionType", "功能类型不能为空。", string.IsNullOrEmpty(FunctionType));
        }
        #endregion
    }
}
