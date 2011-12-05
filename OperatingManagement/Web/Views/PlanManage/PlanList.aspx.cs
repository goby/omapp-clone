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
                pnlDestination.Visible = false;
                pnlData.Visible = true;
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


        //最终发送
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDestination.Visible = false;
            pnlData.Visible = true;
        }

        protected void btnHidden_Click(object sender, EventArgs e)
        {
            pnlDestination.Visible = true;
            pnlData.Visible = false;
            string plantype = txtPlanType.Text;
            switch (plantype)
            {
                case "YJJH":
                    rbtDestination.Items.Clear();
                    rbtDestination.Items.Add(new ListItem("天基目标观测应用研究分系统（GCYJ）", "GCYJ"));
                    rbtDestination.Items.Add(new ListItem("遥操作应用研究分系统（CZYJ）", "CZYJ"));
                    rbtDestination.Items.Add(new ListItem("空间机动应用研究分系统（JDYJ）", "JDYJ"));
                    rbtDestination.Items.Add(new ListItem("仿真推演分系统（FZTY）", "FZTY"));
                    break;
                case "XXXQ":
                    rbtDestination.Items.Clear();
                    rbtDestination.Items.Add(new ListItem("空间信息综合应用中心(XXZX)", "XXZX"));
                    break;
                case "GZJH":
                    rbtDestination.Items.Clear();
                    rbtDestination.Items.Add(new ListItem("西安中心（XSCC）", "XSCC"));
                    rbtDestination.Items.Add(new ListItem("总参二部信息处理中心（XXZX）", "XXZX"));
                    rbtDestination.Items.Add(new ListItem("总参三部技侦中心（JZZX）", "JZZX"));
                    rbtDestination.Items.Add(new ListItem("总参气象水文空间天气总站资料处理中心（ZLZX）", "ZLZX"));
                    rbtDestination.Items.Add(new ListItem("863-YZ4701遥科学综合站（JYZ1）", "JYZ1"));
                    rbtDestination.Items.Add(new ListItem("863-YZ4702遥科学综合站（JYZ2）", "JYZ2"));
                    break;
                case "ZXJH":
                    rbtDestination.Items.Clear();
                    break;
                case "TYSJ":
                    rbtDestination.Items.Clear();
                    rbtDestination.Items.Add(new ListItem("仿真推演分系统(FZTY)", "FZTY"));
                    break;
                case "SBJH":
                    rbtDestination.Items.Clear();
                    rbtDestination.Items.Add(new ListItem("运控评估中心YKZX(02 04 00 00)", "YKZX"));
                    break;

            }
            
        }
    }
}