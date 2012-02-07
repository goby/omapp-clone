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
using OperatingManagement.DataAccessLayer.PlanManage;
using System.Web.Security;
using System.Data;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class GetFileSInfos : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnHidRSendFile_Click(object sender, EventArgs e)
        {
            string strRID = txtRID.Text;
            string strStatus = txtStatus.Text;
            //status=0已提交发送；status=1发送中，这两种情况不能重发
            if (strStatus == "0" || strStatus == "1")
                return;

        }

        private void BindDataSource()
        {
        }

        private void SendFile(string fileName, string filePath)
        {
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_FSendInfo.View";
            this.ShortTitle = "查看文件发送记录";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/GetFileSInfos.aspx.js");
        }
    }
}