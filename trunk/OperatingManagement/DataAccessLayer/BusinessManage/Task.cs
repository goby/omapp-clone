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
    public class Task : BaseEntity<int, Task>
    {
        public Task()
        {
            _dataBase = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region Properties
        private OracleDatabase _dataBase = null;
        private const string s_up_task_selectall = "up_task_selectall";

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        ///// <summary>
        ///// 任务代号
        ///// </summary>
        public string TaskNo { get; set; }
        /// <summary>
        /// 对象标识
        /// </summary>
        public string ObjectFlag { get; set; }
        /// <summary>
        /// 任务编号，可能多个，逗号,分隔
        /// </summary>
        public string SatID { get; set; }

        public static List<Task> _taskCache = null;
        public List<Task> Cache
        {
            get
            {
                if (_taskCache == null)
                {
                    _taskCache = SelectAll();
                }
                return _taskCache;
            }
            set { }
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

        #region -Public Methods


        /// <summary>
        /// 获得所有任务信息实体列表
        /// </summary>
        /// <returns>任务信息实体列表</returns>
        public List<Task> SelectAll()
        {
            OracleParameter o_Cursor = PrepareRefCursor();
            DataSet ds = _dataBase.SpExecuteDataSet(s_up_task_selectall, new OracleParameter[] { o_Cursor });

            List<Task> infoList = new List<Task>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Task info = new Task()
                    {
                        Id = Convert.ToInt32(dr["id"].ToString()),
                        TaskName = dr["TaskName"].ToString(),
                        TaskNo = dr["TaskNo"].ToString(),
                        ObjectFlag = dr["ObjectFlag"].ToString(),
                        SatID = dr["SatID"].ToString()
                    };

                    infoList.Add(info);
                }
            }
            return infoList;
        }

        /// <summary>
        /// 通过任务代号获取对象标识
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public string GetObjectFlagByTaskNo(string taskNo)
        {
            string strObjectCode = string.Empty;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.TaskNo == taskNo);
                if (query != null && query.Count() > 0)
                    strObjectCode = query.FirstOrDefault().ObjectFlag;
            }
            return strObjectCode;
        }

        /// <summary>
        /// 通过任务代号获取卫星编号
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public string GetSatIDByTaskNo(string taskNo)
        {
            string strSatIDs = string.Empty;
            if (Cache != null)
            {
                var query = Cache.Where(a => a.TaskNo == taskNo);
                if (query != null && query.Count() > 0)
                    strSatIDs = query.FirstOrDefault().SatID;
            }
            return strSatIDs;
        }
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
        }
        #endregion
    }
}
