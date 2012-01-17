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
    public partial class NotSpaceTaskList : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnSend.Attributes.Add("onclick", "javascript:return confirm('确定要发送所选数据吗?');");
                pnlAll1.Visible = false;
                pnlAll2.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>hideSelectAll();</script>");
               // ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>showMsgError();</script>");
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
                endDate = Convert.ToDateTime(txtEndDate.Text);
            }
            List<YDSJ> listDatas = (new YDSJ()).GetYDSJListByDate(startDate, endDate, "2");
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();

            if (listDatas.Count > 0)
            {
                pnlAll1.Visible = true;
                pnlAll2.Visible = true;
            }
            else
            {
                pnlAll1.Visible = false;
                pnlAll2.Visible = false;
            }
        }

        void BindRadDestination()
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            BindRadDestination();
        }
        //最终发送
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "NotSpaceTask.List";
            this.ShortTitle = "查看非空间机动任务";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/NotSpaceTaskList.aspx.js");
        }
    }
}