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

namespace OperatingManagement.Web.PlanManage
{
    public partial class PlanList : AspNetPage, IRouteContext
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.List";
            this.ShortTitle = "查询计划";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanList.aspx.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnSend.Attributes.Add("onclick", "javascript:return confirm('确定要发送所选计划吗?');");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            BindGridView();
        }

        //绑定列表
        void BindGridView()
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                startDate = Convert.ToDateTime(txtStartDate.Text);
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                endDate = Convert.ToDateTime(txtEndDate);
            }
            string planType = rbtType.Text;
            string planAging = ddlAging.SelectedValue;
            DataSet objDs = new DataSet();
            //objDs = (new Plan()).GetSYJHList(planType,planAging, startDate, endDate);
            gvList.DataSource = objDs;
            gvList.DataBind();
            if (objDs.Tables[0].Rows.Count > 0)
            {
                btnSend.Visible = true;
            }
            else
            {
                btnSend.Visible = false;
            }
        }

        void BindRadDestination()
        { 
            
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            pnlData.Visible = false;
            pnlDestination.Visible = true;
            BindRadDestination();
        }
        //最终发送
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            pnlDestination.Visible = false;
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ("ShowDetail" == e.CommandName)
            {
                int idx = Int32.Parse(e.CommandArgument.ToString());
                int planID = Convert.ToInt32(gvList.DataKeys[idx][0]);

                Response.Redirect(string.Format("PlanDetail.aspx?planid={0}", planID));
            }
            else if ("EditPlan" == e.CommandName)
            {
                int idx = Int32.Parse(e.CommandArgument.ToString());
                string planID = gvList.DataKeys[idx][0].ToString();
                Response.Redirect(string.Format("PlanEdit.aspx?planid={0}", planID));
            }
        }
    }
}