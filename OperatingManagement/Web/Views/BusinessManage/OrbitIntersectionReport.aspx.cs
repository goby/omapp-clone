#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:OrbitIntersectionReport.cs
//Remark:业务管理-轨道分析-差值分析
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20120602     Create     
//------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.DataAccessLayer.BusinessManage;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class OrbitIntersectionReport : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //trMessage.Visible = false;
                //lblMessage.Text = string.Empty;
                if (!IsPostBack)
                {
                    BindDataSource();
                }
            }
            catch
            {
                //trMessage.Visible = true;
                //lblMessage.Text = "发生未知错误，操作失败。";
            }
        }

        protected void menuCut_MenuItemClick(object sender, MenuEventArgs e)
        {
            mvCut.ActiveViewIndex = Convert.ToInt32(menuCut.SelectedValue);
        }

        protected void rblCutMainFileOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            //手工录入
            if (rblCutMainFileOption.SelectedValue == "1")
            {
                tbCutMainUpload.Visible = false;
                tbCutMainFillIn.Visible = true;
            }
            //文件上传
            else if (rblCutMainFileOption.SelectedValue == "0")
            {
                tbCutMainUpload.Visible = true;
                tbCutMainFillIn.Visible = false;
            }
        }

        protected void rblCutMainKAE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //考虑KAE
            if (rblCutMainKAE.SelectedValue == "1")
            {
                txtCutMaindA.Enabled = true;
                txtCutMaindE.Enabled = true;
            }
            //不考虑KAE
            else if (rblCutMainKAE.SelectedValue == "0")
            {
                txtCutMaindA.Enabled = false;
                txtCutMaindE.Enabled = false;
                txtCutMaindA.Text = "0";
                txtCutMaindE.Text = "0";
            }
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_JHYB.Caculate";
            this.ShortTitle = "交会预报";
            this.SetTitle();
            //this.AddJavaScriptInclude("scripts/pages/businessmanage/OrbitDifferenceAnalysis.aspx.js");
        }

        #region Method
        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        private void BindDataSource()
        {
            dplCutMainReportBeginTimeHour.Items.Clear();
            dplCutMainLYTimeHour.Items.Clear();
            for (int i = 0; i < 24; i++)
            {
                dplCutMainReportBeginTimeHour.Items.Add(new ListItem(i.ToString() + "时", i.ToString()));
                dplCutMainLYTimeHour.Items.Add(new ListItem(i.ToString() + "时", i.ToString()));
            }
            dplCutMainReportBeginTimeMinute.Items.Clear();
            dplCutMainLYTimeMinute.Items.Clear();
            dplCutMainReportBeginTimeSecond.Items.Clear();
            dplCutMainLYTimeSecond.Items.Clear();
            for (int i = 0; i < 60; i++)
            {
                dplCutMainReportBeginTimeMinute.Items.Add(new ListItem(i.ToString() + "分", i.ToString()));
                dplCutMainLYTimeMinute.Items.Add(new ListItem(i.ToString() + "分", i.ToString()));

                dplCutMainReportBeginTimeSecond.Items.Add(new ListItem(i.ToString() + "秒", i.ToString()));
                dplCutMainLYTimeSecond.Items.Add(new ListItem(i.ToString() + "秒", i.ToString()));
            }

            dplCutMainSatellite.AllowBlankItem = false;
        }
        #endregion
    }
}