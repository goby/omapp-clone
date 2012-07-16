using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using OperatingManagement.RemotingObjectInterface;
using OperatingManagement.Framework;

namespace ServicesKernel.DataFrame
{
    public class DFSender
    {
        /// <summary>
        /// 发送数据帧
        /// </summary>
        /// <param name="data"></param>
        /// <param name="taskCode"></param>
        /// <param name="satelliteID"></param>
        /// <param name="infoTypeID"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public string SendDF(byte[] data, string taskCode, string satelliteID, int infoTypeID, int source, int destination, DateTime dataTime)
        {
            string strResult = string.Empty;
            IDFSender oSender = DFSenderClientAgent.GetObject<IDFSender>();
            strResult = oSender.SendDF(data, taskCode, satelliteID, infoTypeID, source, destination, dataTime);
            XElement root = XElement.Parse(strResult);
            //1，提交失败，0，提交成功
            if (root.Element("code").Value == "0")
                strResult = string.Empty;
            else
                strResult = root.Element("msg").Value;
            return strResult;
        }
    }
}
