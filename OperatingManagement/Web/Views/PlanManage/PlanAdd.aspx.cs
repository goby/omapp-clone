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

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class PlanAdd : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
            {
                TaskID = txtTaskID.Text.Trim(),
                PlanType = ddlPlanType.SelectedValue,
                PlanID = Convert.ToInt32(txtPlanID.Text.Trim()),
                StartTime  = Convert.ToDateTime(txtStartTime.Text.Trim()),
                EndTime = Convert.ToDateTime(txtEndTime.Text.Trim()),
                Reserve = txtNote.Text.Trim()
            };
            var result = jh.Add();
            string msg = string.Empty;
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    msg = "发生了数据错误，无法完成请求的操作。";
                    break;
                case Framework.FieldVerifyResult.Success:
                    msg = "新建计划已成功。";
                    hfUserId.Value = jh.Id.ToString();
                    break;
            }
            ltMessage.Text = msg;
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Add";
            this.ShortTitle = "新建计划";
            this.SetTitle();
            //this.AddJavaScriptInclude("scripts/pages/PlanAdd.aspx.js");
        }
    }
}