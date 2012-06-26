using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer.PlanManage;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Components;
using OperatingManagement.DataAccessLayer.BusinessManage;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class SYSJFF : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideMessage();
            if (!Page.IsPostBack)
                HidControl();
        }

        /// <summary>
        /// 绑定YC控件数据源
        /// </summary>
        /// <param name="listDatas"></param>
        private void BindYCDataSource(List<YCPG> listDatas)
        {
            vYCData.Visible = true;
            cpYCData.DataSource = listDatas;
            cpYCData.PageSize = this.SiteSetting.PageSize;
            if (listDatas.Count > this.SiteSetting.PageSize)
                cpYCData.Visible = true;
            cpYCData.BindToControl = rpYCData;
            rpYCData.DataSource = cpYCData.DataSourcePaged;
            rpYCData.DataBind();
        }

        /// <summary>
        /// 绑定UF控件数据源
        /// </summary>
        /// <param name="listDatas"></param>
        private void BindUFDataSource(List<UserFrame> listDatas)
        {
            vUFData.Visible = true;
            cpUFData.DataSource = listDatas;
            cpUFData.PageSize = this.SiteSetting.PageSize;
            if (listDatas.Count > this.SiteSetting.PageSize)
                cpUFData.Visible = true;
            cpUFData.BindToControl = rpUFData;
            rpUFData.DataSource = cpUFData.DataSourcePaged;
            rpUFData.DataBind();
        }

        /// <summary>
        /// 绑定FZ控件数据源
        /// </summary>
        /// <param name="listDatas"></param>
        private void BindFZDataSource(List<JH> listDatas)
        {
            vFZData.Visible = true;
            cpFZData.DataSource = listDatas;
            cpFZData.PageSize = this.SiteSetting.PageSize;
            if (listDatas.Count > this.SiteSetting.PageSize)
                cpFZData.Visible = true;
            cpFZData.BindToControl = rpFZData;
            rpFZData.DataSource = cpFZData.DataSourcePaged;
            rpFZData.DataBind();
        }

        /// <summary>
        /// 隐藏提示信息
        /// </summary>
        private void HideMessage()
        {
            trMessage.Visible = false;
            lblMessage.Text = "";
        }

        private void HidControl()
        {
            vUFData.Visible = false;
            vYCData.Visible = false;
            vFZData.Visible = false;
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            trMessage.Visible = true;
            lblMessage.Text = message;
        }


        /// <summary>
        /// 初始化页面基本信息
        /// </summary>
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_SYSJFF.View";
            this.ShortTitle = "试验数据分发";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/BusinessManage/SYSJFF.aspx.js");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {
            List<YCPG> listYCData = null;
            List<UserFrame> listUFData = null;
            HidControl();

            #region 取查询条件
            string taskNo = ucTask1.SelectedValue;
            string satID = ucSatellite1.SelectedValue;
            string sType = string.Empty;
            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MinValue;
            if (txtFrom.Text != "")
            {
                if (!DateTime.TryParse(txtFrom.Text.Trim(), out dtFrom))
                {
                    ShowMessage("开始日期格式非法");
                    return;
                }
            }
            if (txtTo.Text != "")
            {
                if (!DateTime.TryParse(txtTo.Text.Trim(), out dtTo))
                {
                    ShowMessage("结束日期格式非法");
                    return;
                }
            }

            if (dtFrom > dtTo)
            {
                ShowMessage("结束日期必须大于开始日期");
                return;
            }
            #endregion

            switch (ddlDataType.SelectedValue)
            {
                case "0"://TJ
                    sType = "0";
                    listYCData = new YCPG().GetSYSJ(dtFrom, dtTo, sType, taskNo, satID);
                    BindYCDataSource(listYCData);
                    listUFData = new UserFrame().GetSYSJ(dtFrom, dtTo, taskNo, satID);
                    BindUFDataSource(listUFData);
                    break;
                case "1"://KJ
                    sType = "1";
                    listYCData = new YCPG().GetSYSJ(dtFrom, dtTo, sType, taskNo, satID);
                    break;
                case "2"://FZ
                    List<JH> listFZData = new JH().GetJHList("TYSJ", dtFrom, dtTo);
                    BindFZDataSource(listFZData);
                    break;
            }
        }

    }
}