using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer;
using OperatingManagement.Framework;
using OperatingManagement.DataAccessLayer.BusinessManage;
using OperatingManagement.RemotingObjectInterface;
using OperatingManagement.RemotingClient;
using System.Xml.Linq;
using System.Configuration;
using System.Web.Security;
using System.Data;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class SendFile : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                InitialPageData();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (ddlSender.SelectedItem.Value == ddlReceiver.SelectedItem.Value)
            {
                ShowMessage("信源与信宿不能相同");
                return;
            }

            if (!fuToSend.HasFile)
            {
                ShowMessage("待发送文件不能为空。");
                return;
            }
            string strResult = string.Empty;
            string fileFullName = fuToSend.FileName;
            string fileName = fileFullName.Substring(fileFullName.LastIndexOf(@"\",0)+1);
            string filePath = fileFullName.Substring(0,fileFullName.LastIndexOf(@"\",0)+1);
            IFileSender oSender = FileSenderClientAgent.GetObject<IFileSender>();
            strResult = oSender.SendFile(fileName, filePath, Convert.ToInt32(rblSendWay.SelectedValue), Convert.ToInt32(ddlSender.SelectedValue)
                , Convert.ToInt32(ddlReceiver.SelectedValue), Convert.ToInt32(ddlInfoType.SelectedValue), rblAutoResend.SelectedValue == "1");
            //FileSender oSender = new FileSender();
            //strResult = oSender.SendFile(fileName, filePath, Convert.ToInt32(rblSendWay.SelectedValue),
            //    Convert.ToInt32(ddlSender.SelectedItem.Value), Convert.ToInt32(ddlReceiver.SelectedItem.Value),
            //    Convert.ToInt32(ddlInfoType.SelectedItem.Value), (rblAutoResend.SelectedValue == "1" ? true : false));
            if (strResult != string.Empty)
            {
                XElement root = XElement.Parse(strResult);
                int iResult = Convert.ToInt32(root.Element("result").Value);
                if (iResult == 0)
                    ShowMessage("文件发送请求提交成功，请求ID：" + root.Element("fileid").Value + "。");
                else
                    ShowMessage("文件发送请求提交失败，失败原因：" + root.Element("message").Value + "。");
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitialPageData()
        {
            HideMessage();
            this.ddlInfoType.AllowBlankItem = false;
            this.ddlSender.AllowBlankItem = false;
            this.ddlReceiver.AllowBlankItem = false;
        }

        /// <summary>
        /// 初始化页面基本信息
        /// </summary>
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_FSendFile.Do";
            this.ShortTitle = "发送文件";
            this.SetTitle();
        }
        
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }

        private void HideMessage()
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
        }
    }

    /// <summary>
    /// 发送文件
    /// </summary>
    public class FileSender
    {
        private static string strPath = "";

        public FileSender()
        {
            strPath = ConfigurationManager.AppSettings["FileServerPath"].ToString();
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="sendWay"></param>
        /// <param name="senderID"></param>
        /// <param name="receiverID"></param>
        /// <param name="infoTypeID"></param>
        /// <param name="autoResend"></param>
        /// <returns></returns>
        public string SendFile(string fileName, string filePath, int sendWay, int senderID, int receiverID, int infoTypeID, bool autoResend)
        {
            string strResult = string.Empty;
            if (strPath != string.Empty)
            {
                IFileSender oFileServer = RemotingActivator.GetObjectByConfig<IFileSender>(strPath);
                strResult = oFileServer.SendFile(fileName, filePath, sendWay, senderID, receiverID, infoTypeID, autoResend);
            }
            return strResult;
        }

        /// <summary>
        /// 获取文件发送状态
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public string GetSendStatus(int fileID)
        {
            string strResult = string.Empty;
            if (strPath != string.Empty)
            {
                IFileSender oFileServer = RemotingActivator.GetObjectByConfig<IFileSender>(strPath);
                strResult = oFileServer.GetSendStatus(fileID);
            }
            return strResult;
        }

        /// <summary>
        /// 重新发送文件
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public string ReSendFile(int fileID)
        {
            string strResult = string.Empty;
            if (strPath != string.Empty)
            {
                IFileSender oFileServer = RemotingActivator.GetObjectByConfig<IFileSender>(strPath);
                strResult = oFileServer.ReSendFile(fileID);
            }
            return strResult;
        }
    }
}