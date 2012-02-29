using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatingManagement.RemotingObjectInterface
{
    public interface IFileServer
    {
        //发送文件，返回结果为xml，提交结果，如果提交成功，返回FileID
        string SendFile(string fileName, string filePath, int sendWay, int senderID, int receiverID, int infoTypeID, int retryTimes);

        /// <summary>
        /// 查询文件发送状态，返回文件发送状态及备注
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        string GetSendStatus(int fileID);

        /// <summary>
        /// 重新发送文件，返回任务提交结果
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        string ReSendFile(int fileID);
    }
}
