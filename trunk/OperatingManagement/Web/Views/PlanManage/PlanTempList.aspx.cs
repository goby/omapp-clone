using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer;
using OperatingManagement.Framework;
using OperatingManagement.DataAccessLayer.PlanManage;
using OperatingManagement.DataAccessLayer.BusinessManage;
using System.Web.Security;
using System.Data;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class PlanTempList : AspNetPage, IRouteContext
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMPLAN_Plan.View";
            this.ShortTitle = "查询计划";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/PlanTempList.aspx.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    txtStartDate.Attributes.Add("readonly", "true");
                    txtEndDate.Attributes.Add("readonly", "true");

                    pnlAll2.Visible = false;

                    lblMessage.Text = ""; //文件发送消息清空
                    lblMessage.Visible = false;
                    DefaultSearch();
                    //由计划页面返回时，重新载入之前的查询结果
                    if (Request.QueryString["startDate"] != null || Request.QueryString["endDate"] != null || Request.QueryString["type"] != null)
                    {
                        if (Request.QueryString["startDate"] != null)
                        { txtStartDate.Text = Request.QueryString["startDate"]; }
                        if (Request.QueryString["endDate"] != null)
                        { txtEndDate.Text = Request.QueryString["endDate"]; }
                        ddlType.SelectedValue = Request.QueryString["type"];
                        btnSearch_Click(new object(), new EventArgs());
                    }
                }
                catch (Exception ex)
                {
                    throw (new AspNetException("计划列表页面初始化出现异常，异常原因", ex));
                }
                finally { }
            }
            cpPager.PostBackPage += new EventHandler(cpPager_PostBackPage);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = ""; //文件发送消息清空
                lblMessage.Visible = false;
                SaveCondition();
                cpPager.CurrentPage = 1;
                BindGridView(true);
            }
            catch (Exception ex)
            {
                throw (new AspNetException("计划列表页面搜索出现异常，异常原因", ex));
            }
            finally { }
        }

        private void SaveCondition()
        {
            if (string.IsNullOrEmpty(txtStartDate.Text))
            { ViewState["_StartDate"] = null; }
            else
            { ViewState["_StartDate"] = txtStartDate.Text.Trim(); }
            if (string.IsNullOrEmpty(txtEndDate.Text))
            { ViewState["_EndDate"] = null; }
            else
            { ViewState["_EndDate"] = Convert.ToDateTime(txtEndDate.Text.Trim()).AddDays(1).AddMilliseconds(-1); }
            ViewState["_PlanType"] = ddlType.SelectedItem.Value;
        }
        /// <summary>
        /// 默认查询两周内的数据
        /// </summary>
        private void DefaultSearch()
        {
            txtStartDate.Text = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //btnSearch_Click(new Object(), new EventArgs());
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        void BindGridView(bool fromSearch)
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            string planType = null;
            if (fromSearch)
            {
                if (!string.IsNullOrEmpty(txtStartDate.Text))
                {
                    startDate = Convert.ToDateTime(txtStartDate.Text);
                }

                if (!string.IsNullOrEmpty(txtEndDate.Text))
                {
                    endDate = Convert.ToDateTime(txtEndDate.Text).AddDays(1).AddMilliseconds(-1); //查询时可查当天
                }

                if (ddlType.SelectedItem.Value != "0")
                {
                    planType = ddlType.SelectedItem.Value;
                }
            }
            else
            {
                if (ViewState["_StartDate"] != null)
                {
                    startDate = Convert.ToDateTime(ViewState["_StartDate"].ToString());
                }
                if (ViewState["_EndDate"] != null)
                {
                    endDate = Convert.ToDateTime(ViewState["_EndDate"].ToString());
                }

                if (ViewState["_PlanType"].ToString() != "0")
                {
                    planType = ViewState["_PlanType"].ToString();
                }
            }


            List<JH> listDatas = (new JH(true)).GetJHList(planType, startDate, endDate);
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();

            if (listDatas.Count > 0)
            {
                pnlAll2.Visible = true;
            }
            else
            {
                pnlAll2.Visible = false;
            }
        }

        protected void cpPager_PostBackPage(object sender, EventArgs e)
        {
            BindGridView(false);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath);
        }

    }
}