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
using System.Xml.Linq;
using System.Web.Security;
using System.Data;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class GetFileSInfos : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideMessage();
            if (!IsPostBack)
                InitialPageData();
        }

        protected void btnHidRSendFile_Click(object sender, EventArgs e)
        {
            string strRID = txtRID.Text;
            string strStatus = txtStatus.Text;
            //status=0已提交发送；status=1发送中，这两种情况不能重发
            if (strStatus == SendStatuss.Submitted.ToString() || strStatus == SendStatuss.Sending.ToString())
                return;
            if (strRID.Equals(string.Empty))
                return;
            ReSendFile(Convert.ToInt32(strRID));
        }

        protected void btnResend_Click(object sender, EventArgs e)
        {
            int iRID = 0;
            Button btnResend = (Button)sender;
            if (btnResend == null)
                return;
            if (int.TryParse(btnResend.CommandArgument, out iRID))
                return;
            FileSendInfo oSInfo = new FileSendInfo();
            oSInfo.Id = iRID;
            oSInfo = oSInfo.SelectById();
            if (oSInfo != null)
            {
                ReSendFile(iRID);
            }
        }

        protected bool CanReSend(object status)
        {
            string strStatus = status.ToString();
            if (strStatus == SendStatuss.Submitted.ToString() || strStatus == SendStatuss.Sending.ToString())
                return false;
            else
                return true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MinValue;
            if (txtFrom.Text != "")
                dtFrom = DateTime.Parse(txtFrom.Text);
            if (txtTo.Text != "")
                dtTo = DateTime.Parse(txtTo.Text);

            FileSendInfo oSend = new FileSendInfo();
            oSend.InfoTypeID = Convert.ToInt32(ddlInfoType.SelectedItem.Value);
            List<FileSendInfo> listDatas = oSend.Search(dtFrom, dtTo);
            if (listDatas.Count == 0)
                ShowMessage("没有符合条件的记录");
            else
                BindDataSource(listDatas);
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitialPageData()
        {
            ddlInfoType.AllowBlankItem = true;
            FileSendInfo oSend = new FileSendInfo();
            List<FileSendInfo> listDatas = oSend.SelectAll();
            BindDataSource(listDatas);
        }

        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        /// <param name="listDatas"></param>
        private void BindDataSource(List<FileSendInfo> listDatas)
        {
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();
        }

        /// <summary>
        /// 重新发送文件
        /// </summary>
        /// <param name="sendInfoID"></param>
        /// <returns></returns>
        private void ReSendFile(int sendInfoID)
        {
            string strResult = string.Empty;
            FileSender oSender = new FileSender();
            //strResult = oSender.ReSendFile(sendInfoID);
            if (strResult != string.Empty)
            {
                XElement root = XElement.Parse(strResult);
                int iResult = Convert.ToInt32(root.Element("result").Value);
                if (iResult == 0)
                    ShowMessage("文件重发请求提交成功，请求ID：" + root.Element("fileid").Value + "。");
                else
                    ShowMessage("文件重发请求提交失败，失败原因：" + root.Element("message").Value + "。");
            }
        }

        /// <summary>
        /// 初始化页面基本信息
        /// </summary>
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_FSendInfo.View";
            this.ShortTitle = "查看文件发送记录";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/BusinessManage/GetFileSInfos.aspx.js");
        }

        /// <summary>
        /// 隐藏提示信息
        /// </summary>
        private void HideMessage()
        {
            trMessage.Visible = false;
            lblMessage.Text = "";
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            trMessage.Visible = true;
            lblMessage.Text = message;
        }

        protected void rpDatas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FileSendInfo oFSInfo = null;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
            {
                oFSInfo = (FileSendInfo)e.Item.DataItem;
                if (oFSInfo.SendStatus != SendStatuss.Failed)
                {
                    ((Button)e.Item.FindControl("btnResend")).Enabled = false;
                    //((Button)e.Item.FindControl("btnResend")).ForeColor = System.Drawing.Color.Gray;
                }
            }
        }
    }
}