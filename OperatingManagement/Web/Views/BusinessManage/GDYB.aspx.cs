using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.Framework.Storage;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer.BusinessManage;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class GDYB : AspNetPage
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_GDYB.Caculate";
            this.ShortTitle = "轨道预报";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/BusinessManage/GDYB.aspx.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtFrom.Attributes.Add("readonly", "true");
                ucSatellite1.AllowBlankItem = false;
                InitPage();
            }
        }

        private void InitPage()
        {
            // Bind cblXyxs DataSource
            cblXyxs.Items.Clear();
            cblXyxs.DataSource = new XYXSInfo().SelectAll();
            cblXyxs.DataTextField = "AddrName";
            cblXyxs.DataValueField = "InCode";
            cblXyxs.DataBind();
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {

        }
    }
}