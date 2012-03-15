using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using Oracle.DataAccess.Client;
using System.Data;

namespace OperatingManagement.DataAccessLayer.BusinessManage
{
    [Serializable]
    public class DFRcvData : BaseEntity<int, DFRcvData>
    {
        public DFRcvData()
        {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region Properties
        private string s_up_dfrcvdata_insert = "up_dfrcvdata_insert";
        private string s_up_dfrcvdata_selectall = "up_dfrcvdata_selectall";
        private string s_up_dfrcvdata_search = "up_dfrcvdata_search";
        private string s_up_dfrcvdata_update = "up_dfrcvdata_update";
        
        private OracleDatabase _database = null;
        
        /// <summary>
        /// 数据主类别
        /// </summary>
        public string MainType { get; set; }

        /// <summary>
        /// 数据次类别
        /// </summary>
        public string SecondType { get; set; }

        /// <summary>
        /// 信源ID
        /// </summary>
        public int Source { get; set; }

        /// <summary>
        /// 信宿ID
        /// </summary>
        public int Destination { get; set; }

        /// <summary>
        /// 任务代号
        /// </summary>
        public string TaskCode { get; set; }

        /// <summary>
        /// 卫星代号
        /// </summary>
        public string SatelliteID { get; set; }

        /// <summary>
        /// 数据时间
        /// </summary>
        public DateTime DataTime { get; set; }

        /// <summary>
        /// 数据报顺序号，0～65535
        /// </summary>
        public int SequenceNo { get; set; }
        
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string DBlob { get; set; }
        
        /// <summary>
        /// 数据体字节
        /// </summary>
        public byte[] DataFlow { get; set; }

        /// <summary>
        /// 数据流向
        /// </summary>
        public DFDirections Direction { get; set; }

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CTime { get; set; }

        /// <summary>
        /// 数据通道
        /// </summary>
        public DFChannels Channel { get; set; }
        /// <summary>
        /// 数据帧是否已归档处理，0为处理，1已处理，2处理失败
        /// </summary>
        public int IsProcessed { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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

        #region -Public Methods-
        /// <summary>
        /// Inserts a new DFInfo into database.
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
            
            object oMainType = DBNull.Value;
            object oDataTime = DBNull.Value;
            object oSequenceNo = DBNull.Value;
            object oDataLength = DBNull.Value;
            object oDblob = DBNull.Value;

            if (!this.MainType.Equals(string.Empty))
            {
                oMainType = this.MainType;
                oDataTime = this.DataTime;
                oSequenceNo = this.SequenceNo;
                oDataLength = this.DataLength;
                oDblob = this.DBlob;
            }
            #endregion

            _database.SpExecuteNonQuery(s_up_dfrcvdata_insert, new OracleParameter[]{
                new OracleParameter("p_TaskCode", this.TaskCode),
                new OracleParameter("p_SatelliteID", this.SatelliteID),
                new OracleParameter("p_MainType", oMainType),
                new OracleParameter("p_SecondType", this.SecondType),
                new OracleParameter("p_Source", this.Source),
                new OracleParameter("p_Destination", this.Destination),
                new OracleParameter("p_DataTime", oDataTime),
                new OracleParameter("p_Dblob", oDblob),
                new OracleParameter("p_DataLength", oDataLength),
                new OracleParameter("p_Direction", (int)this.Direction),
                new OracleParameter("p_CTime", this.CTime),
                new OracleParameter("p_Channel", (int)this.Channel),
                new OracleParameter("p_IsProcessed", this.IsProcessed),
                new OracleParameter("p_Remark", this.Remark),
                opId,
                p
            });

            if (opId.Value != null && opId.Value != DBNull.Value)
                this.Id = Convert.ToInt32(opId.Value);

            return (FieldVerifyResult)Convert.ToInt32(p.Value);
        }

        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
        }
        #endregion

    }

    /// <summary>
    /// 数据帧方向
    /// </summary>
    public enum DFDirections
    {
        /// <summary>
        /// 发送0
        /// </summary>
        Send = 0,
        /// <summary>
        /// 接收1
        /// </summary>
        Receive = 1
    }

    /// <summary>
    /// 数据帧收发通道
    /// </summary>
    public enum DFChannels
    {
        /// <summary>
        /// 服务总线0
        /// </summary>
        ServiceBUS = 0,
        /// <summary>
        /// 消息总线1
        /// </summary>
        MessageServer = 1
    }
}
