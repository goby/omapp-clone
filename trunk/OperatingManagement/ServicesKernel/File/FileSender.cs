using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using OperatingManagement.RemotingObjectInterface;
using OperatingManagement.Framework;


namespace ServicesKernel.File
{
    public class FileSender
    {
        /// <summary>
        /// return submit result: success or failed
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="sendway"></param>
        /// <param name="senderID"></param>
        /// <param name="receiverID"></param>
        /// <param name="infotypeID"></param>
        /// <param name="isAutoRetry"></param>
        /// <returns></returns>
        public bool SendFile(string fileName, string filePath, CommunicationWays sendway, int senderID, int receiverID, int infotypeID, bool isAutoRetry)
        {
            IFileSender fileSender = FileSenderClientAgent.GetObject<IFileSender>();
            string strResult = "";
            //0:ftp;1:udp;2:tcp
            strResult = fileSender.SendFile(fileName, filePath, (int)sendway, senderID, receiverID, infotypeID, isAutoRetry);
            if (strResult.Equals(string.Empty))
            {
                return false;
            }
            XElement root = XElement.Parse(strResult);
            //1，提交失败，0，提交成功
            if (root.Element("Code").Value == "1")
                return false;
            else
                return true;
        }
    }
}
