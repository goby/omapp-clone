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
using System.Web.Security;
using System.Data;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class OribitalQuantityList : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnSend.Attributes.Add("onclick", "javascript:return confirm('确定要发送所选轨道数据吗?');");
                pnlDestination.Visible = false;
                pnlData.Visible = true;

                pnlAll1.Visible = false;
                pnlAll2.Visible = false;

                //ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>hideSelectAll();</script>");
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

            List<GD> listDatas = (new GD()).GetListByDate(startDate, endDate);
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
            this.PagePermission = "OribitalQuantity.List";
            this.ShortTitle = "查看卫星轨道根数";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/OribitalQuantityList.aspx.js");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath);
        }
    }
}