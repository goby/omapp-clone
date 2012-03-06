﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;
using Oracle.DataAccess.Client;
using System.Data;

namespace OperatingManagement.DataAccessLayer.BusinessManage
{
    [Serializable]
    public class FileReceiveInfo : BaseEntity<int, GroundResource>
    {
        private OracleDatabase _database = null;
        private const string s_up_frcvinfo_selectall = "up_frcvinfo_selectall";
        private const string s_up_frcvinfo_search = "up_frcvinfo_search";
        private const string s_up_frcvinfo_selectbyid = "up_frcvinfo_selectbyid";
        private const string s_up_frcvinfo_insert = "up_frcvinfo_insert";
        private const string s_up_frcvinfo_update = "up_frcvinfo_update";
        private string strFileFullName = "";
        private string strFilePath = "";

        public FileReceiveInfo()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        public FileReceiveInfo(DataRow dr)
        {
            this.Id = Convert.ToInt32(dr["RID"].ToString());
            if (dr["FileName"] == DBNull.Value)
                this.FileName = string.Empty;
            else
                this.FileName = dr["FileName"].ToString();

            if (dr["FileCode"] == DBNull.Value)
                this.FileCode = string.Empty;
            else
                this.FileCode = dr["FileCode"].ToString();

            if (dr["FilePath"] == DBNull.Value)
                this.FilePath = string.Empty;
            else
                this.FilePath = dr["FilePath"].ToString();

            if (dr["FileSize"] == DBNull.Value)
                this.FileSize = 0;
            else
                this.FileSize = Convert.ToInt32(dr["FileSize"].ToString());

            if (dr["CurPosition"] == DBNull.Value)
                this.CurPosition = 0;
            else
                this.CurPosition = Convert.ToInt32(dr["CurPosition"].ToString());

            this.SenderID = Convert.ToInt32(dr["SenderID"].ToString());
            this.SenderName = dr["SenderName"].ToString();
            this.ReceiverID = Convert.ToInt32(dr["ReceiverID"].ToString());
            this.ReceiverName = dr["ReceiverName"].ToString();
            this.ReceiveStatus = (ReceiveStatuss)Convert.ToInt32(dr["ReceiveStatus"].ToString());
            if (dr["Remark"] == DBNull.Value)
                this.Remark = string.Empty;
            else
                this.Remark = dr["Remark"].ToString();
            this.RecvBeginTime = Convert.ToDateTime(dr["RecvBeginTime"].ToString());
            if (dr["RecvEndTime"] == DBNull.Value)
                this.RecvEndTime = DateTime.MinValue;
            else
                this.RecvEndTime = Convert.ToDateTime(dr["RecvEndTime"].ToString());
            this.ReceiveWay = (CommunicationWays)Convert.ToInt32(dr["ReceiveWay"].ToString());
            this.InfoTypeID = Convert.ToInt32(dr["InfoTypeID"].ToString());
            this.InfoTypeName = dr["InfoTypeName"].ToString();
        }
        
        #region -Properties-
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件标识
        /// </summary>
        public string FileCode { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get
            {
                return strFilePath;
            }
            set
            {
                strFilePath = value;
                if (!strFilePath.EndsWith(@"\"))
                {
                    strFilePath += @"\";
                }
            }
        }

        /// <summary>
        /// 文件全名，即文件路径+文件名
        /// </summary>
        public string FileFullName
        {
            get
            {
                if (strFileFullName.Equals(String.Empty))
                    return this.FilePath + this.FileName;
                else
                    return strFileFullName;
            }
            set
            {
                strFileFullName = value;
            }
        }
        /// <summary>
        /// 文件大小，单位字节
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// 当前位置
        /// </summary>
        public long CurPosition { get; set; }
        /// <summary>
        /// 发送方标识
        /// </summary>
        public int SenderID { get; set; }

        /// <summary>
        /// 发送方名称
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// 接收方标识
        /// </summary>
        public int ReceiverID { get; set; }

        /// 接收方名称
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 接收状态，使用Enum ReceiveStatus
        /// </summary>
        public ReceiveStatuss ReceiveStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 接收开始时间
        /// </summary>
        public DateTime RecvBeginTime { get; set; }
        /// <summary>
        /// 接收结束时间
        /// </summary>
        public DateTime RecvEndTime { get; set; }
        /// <summary>
        /// 信息类型标识
        /// </summary>
        public int InfoTypeID { get; set; }

        /// <summary>
        /// 信息类型名称
        /// </summary>
        public string InfoTypeName { get; set; }
        /// <summary>
        /// 接收方式，使用Enum CommunicationWay
        /// </summary>
        public CommunicationWays ReceiveWay { get; set; }

        /// <summary>
        /// 数据包类型，非数据表结构，仅供文件服务器使用
        /// </summary>
        public int PackageType { get; set; }

        /// <summary>
        /// 数据包的发送状态，非数据表结构，仅供文件服务器使用
        /// </summary>
        public int PackageSendStatus { get; set; }
        //public 
        #endregion

        #region -Private methods-
        private OracleParameter PrepareRefCursor()
        {
            return new OracleParameter()
            {
                ParameterName = "o_cursor",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.RefCursor
            };
        }

        /// <summary>
        /// get the receiveInfos by XXTYPEid, submittime and dataType.
        /// </summary>
        /// <returns></returns>
        private List<FileReceiveInfo> SelectRcvInfoByXXTypeTimeandDataType(int xXTypeID, DateTime beginTime, DateTime endTime)
        {
            OracleParameter p = PrepareRefCursor();

            object oBeginTime = null;
            object oEndTime = null;
            if (beginTime == DateTime.MinValue)
                oBeginTime = DBNull.Value;
            else
                oBeginTime = beginTime;
            if (endTime == DateTime.MinValue)
                oEndTime = DBNull.Value;
            else
                oEndTime = endTime;

            DataSet ds = _database.SpExecuteDataSet(s_up_frcvinfo_search, new OracleParameter[]{
                new OracleParameter("p_XXTypeID", OracleDbType.Int32, xXTypeID, ParameterDirection.Input),
                new OracleParameter("p_BeginTime", OracleDbType.Date, oBeginTime, ParameterDirection.Input),
                new OracleParameter("p_EndTime", OracleDbType.Date, oEndTime, ParameterDirection.Input),
                p
            });

            List<FileReceiveInfo> sinfos = new List<FileReceiveInfo>();
            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sinfos.Add(new FileReceiveInfo(dr));
                }
            }
            return sinfos;
        }
        #endregion

        #region -Public methods-

        /// <summary>
        /// Selects all the file ReceiveInfo from database.
        /// </summary>
        /// <returns>List of <see cref="User"/> collection.</returns>
        public List<FileReceiveInfo> SelectAll()
        {
            OracleParameter p = PrepareRefCursor();
            DataSet ds = _database.SpExecuteDataSet(s_up_frcvinfo_selectall, new OracleParameter[] {
                p
            });
            List<FileReceiveInfo> sinfos = new List<FileReceiveInfo>();
            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sinfos.Add(new FileReceiveInfo(dr));
                }
            }
            return sinfos;
        }

        /// <summary>
        /// get the ReceiveInfo by id.
        /// </summary>
        /// <returns></returns>
        public FileReceiveInfo SelectById()
        {
            OracleParameter p = PrepareRefCursor();
            DataSet ds = _database.SpExecuteDataSet(s_up_frcvinfo_selectbyid, new OracleParameter[]{
                new OracleParameter("p_RID", this.Id),
                p
            });
            FileReceiveInfo sinfo = null;
            if (ds != null && ds.Tables.Count == 1)
            {
                if (ds.Tables[0].Rows.Count == 1)
                    sinfo = new FileReceiveInfo(ds.Tables[0].Rows[0]);
            }
            return sinfo;
        }

        /// <summary>
        /// get the File ReceiveInfos by XXTYPE id and submittime.
        /// </summary>
        /// <returns></returns>
        public List<FileReceiveInfo> Search(DateTime beginTime, DateTime endTime)
        {
            return SelectRcvInfoByXXTypeTimeandDataType(this.InfoTypeID, beginTime, endTime);
        }

        /// <summary>
        /// Inserts a new ReceiveInfo into database.
        /// </summary>
        /// <returns></returns>
        public FieldVerifyResult Add()
        {
            #region -prepare for param
            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };
            OracleParameter opId = new OracleParameter()
            {
                ParameterName = "v_rid",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };

            object oFileName = null;
            object oFileCode = null;
            object oFilePath = null;
            object oFileSize = null;
            object oCurPostion = null;

            if (this.FileName.Equals(string.Empty))
            {
                oFileName = DBNull.Value;
                oFileCode = DBNull.Value;
                oFilePath = DBNull.Value;
                oFileSize = DBNull.Value;
                oCurPostion = DBNull.Value;
            }
            else
            {
                oFileName = this.FileName;
                oFileCode = this.FileCode;
                oFilePath = this.FilePath;
                oFileSize = this.FileSize;
                oCurPostion = this.CurPosition;
            }
            #endregion

            _database.SpExecuteNonQuery(s_up_frcvinfo_insert, new OracleParameter[]{
                new OracleParameter("p_FileName", oFileName),
                new OracleParameter("p_FileCode", oFileCode),
                new OracleParameter("p_FilePath", oFilePath),
                new OracleParameter("p_FileSize", oFileSize),
                new OracleParameter("p_CurPosition", oCurPostion),
                new OracleParameter("p_Sender", this.SenderID),
                new OracleParameter("p_Receiver", this.ReceiverID),
                new OracleParameter("p_ReceiveStatus", (int)this.ReceiveStatus),
                new OracleParameter("p_Remark", DBNull.Value),
                new OracleParameter("p_RecvBeginTime", OracleDbType.Date, this.RecvBeginTime, ParameterDirection.Input),
                new OracleParameter("p_RecvEndTime", OracleDbType.Date, this.RecvEndTime, ParameterDirection.Input),
                new OracleParameter("p_InfoType", this.InfoTypeID),
                new OracleParameter("p_ReceiveWay", (int)this.ReceiveWay),
                opId,
                p
            });

            if (opId.Value != null && opId.Value != DBNull.Value)
                this.Id = Convert.ToInt32(opId.Value);

            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }

        /// <summary>
        /// Updates the ReceiveInfo object in database.
        /// only curPosition、RcvEndTime、Receivetatus、Remark
        /// </summary>
        /// <returns></returns>
        public FieldVerifyResult Update()
        {
            OracleParameter p = new OracleParameter()
            {
                ParameterName = "v_result",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.Double
            };

            object oRecvEndTime = null;
            if (this.RecvEndTime == DateTime.MinValue)
                oRecvEndTime = DBNull.Value;
            else
                oRecvEndTime = this.RecvEndTime;

            _database.SpExecuteNonQuery(s_up_frcvinfo_update, new OracleParameter[]{
                new OracleParameter("p_RID", this.Id),
                new OracleParameter("p_CurPosition", this.CurPosition),
                new OracleParameter("p_ReceiveStatus", (int)this.ReceiveStatus),
                new OracleParameter("p_Remark", this.Remark),
                new OracleParameter("p_RecvEndTime", oRecvEndTime),
                p
            });
            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
        }
        #endregion
    }
}
