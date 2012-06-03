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

        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_JHYB.Caculate";
            this.ShortTitle = "交会预报";
            this.SetTitle();
            //this.AddJavaScriptInclude("scripts/pages/businessmanage/OrbitDifferenceAnalysis.aspx.js");
        }
    }
}