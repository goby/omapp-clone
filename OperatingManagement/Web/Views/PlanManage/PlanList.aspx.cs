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
            else
            {
                startDate = DateTime.Now.AddDays(-14);
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                endDate = Convert.ToDateTime(txtEndDate.Text);
            }
            else
            {
                endDate = DateTime.Now;
            }
            string planType = rbtType.Text;
            string planAging = ddlAging.SelectedValue;

            List<JH> listDatas = (new JH()).GetJHList(planType, planAging, startDate, endDate);
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();
        }

        void BindRadDestination()
        { 
            
        }

        //最终发送
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}