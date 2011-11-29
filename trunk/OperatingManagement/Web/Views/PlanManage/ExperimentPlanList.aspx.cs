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
    public partial class ExperimentPlanList : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             
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

            List<SYJH> listDatas= (new SYJH()).GetSYJHListByDate(startDate, endDate);
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "ExperimentPlan.List";
            this.ShortTitle = "查看试验计划";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/ExperimentPlanList.aspx.js");
        }
    }
}