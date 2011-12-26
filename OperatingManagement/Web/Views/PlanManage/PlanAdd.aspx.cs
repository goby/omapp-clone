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
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class PlanAdd : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnEdit.Visible = false;
                btnContinue.Visible = false;
                btnSubmit.Visible = true;
                btnGetPlanInfo.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string filepath = CreateFile();
            DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
            {
                TaskID = txtTaskID.Text.Trim(),
                PlanType = ddlPlanType.SelectedValue,
                PlanID = Convert.ToInt32(txtPlanID.Text.Trim()),
                StartTime  = Convert.ToDateTime(txtStartTime.Text.Trim()),
                EndTime = Convert.ToDateTime(txtEndTime.Text.Trim()),
                SRCType = 0, 
                FileIndex = filepath,
                SatID = ddlSat.SelectedValue,
                Reserve = txtNote.Text.Trim()
            };
            var result = jh.Add();
            string msg = string.Empty;
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    msg = "发生了数据错误，无法完成请求的操作。";
                    break;
                case Framework.FieldVerifyResult.Success:
                    msg = "新建计划已成功。";
                    hfID.Value = jh.Id.ToString();
                    hfPlanType.Value = jh.PlanType;
                    break;
            }
            ltMessage.Text = msg;

            btnEdit.Visible = true;
            btnContinue.Visible = true;
            btnSubmit.Visible = false;
            btnGetPlanInfo.Visible = false;
        }

        private string CreateFile()
        {
            string filepath="";
            CreatePlanFile fileCreater = new CreatePlanFile();
            switch (ddlPlanType.SelectedValue)
            { 
                case "YJJH":
                    YJJH objYJJH = new YJJH
                    {
                        TaskID = txtTaskID.Text.Trim(),
                        SatID = ddlSat.SelectedValue
                    };
                    filepath = fileCreater.CreateYJJHFile(objYJJH,0);
                    break;
                case "MBXQ":
                    MBXQ objMBXQ = new MBXQ
                    {
                        TaskID = txtTaskID.Text.Trim(),
                        SatID = ddlSat.SelectedValue,
                        SatInfos = new List<MBXQSatInfo> {new MBXQSatInfo() }
                    };
                    filepath = fileCreater.CreateMBXQFile(objMBXQ, 0);
                    break;
                case "HJXQ":
                    HJXQ objHJXQ = new HJXQ 
                    {
                        TaskID = txtTaskID.Text.Trim(),
                        SatID = ddlSat.SelectedValue,
                        SatInfos = new List<HJXQSatInfo> { new HJXQSatInfo ()}
                    };
                    filepath = fileCreater.CreateHJXQFile(objHJXQ, 0);
                    break;
                case "DMJH":
                    DMJH objDMJH = new DMJH
                    {
                        TaskID = txtTaskID.Text.Trim(),
                        SatID = ddlSat.SelectedValue,
                        DMJHTasks = new List<DMJH_Task> 
                        {
                            new DMJH_Task
                            {
                                ReakTimeTransfors = new List<DMJH_Task_ReakTimeTransfor>{new DMJH_Task_ReakTimeTransfor()},
                                AfterFeedBacks = new List<DMJH_Task_AfterFeedBack>{new DMJH_Task_AfterFeedBack()}
                            }
                        }
                    };
                    filepath = fileCreater.CreateDMJHFile(objDMJH, 0);
                    break;
                case "ZXJH":
                    ZXJH objZXJH = new ZXJH
                    {
                        TaskID = txtTaskID.Text.Trim(),
                        SatID = ddlSat.SelectedValue,
                        WorkContents = new List<ZXJH_WorkContent> { new ZXJH_WorkContent() },
                        SYDataHandles = new List<ZXJH_SYDataHandle> { new ZXJH_SYDataHandle() },
                        DirectAndMonitors = new List<ZXJH_DirectAndMonitor> { new ZXJH_DirectAndMonitor() },
                        RealTimeControls = new List<ZXJH_RealTimeControl> { new ZXJH_RealTimeControl() },
                        SYEstimates = new List<ZXJH_SYEstimate> { new ZXJH_SYEstimate() },
                        DataManages = new List<ZXJH_DataManage> { new ZXJH_DataManage()}
                    };
                    filepath = fileCreater.CreateZXJHFile(objZXJH, 0);
                    break;
                case "TYSJ":
                    TYSJ objTYSJ = new TYSJ 
                    {
                        TaskID = txtTaskID.Text.Trim(),
                        SatID = ddlSat.SelectedValue
                    };
                    filepath = fileCreater.CreateTYSJFile(objTYSJ, 0);
                    break;
            }
            return filepath;
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Add";
            this.ShortTitle = "新建计划";
            this.SetTitle();
            //this.AddJavaScriptInclude("scripts/pages/PlanAdd.aspx.js");
        }

        protected void txtGetPlanInfo_Click(object sender, EventArgs e)
        {

        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            txtTaskID.Text = "";
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            txtNote.Text = "";
            ddlPlanType.SelectedValue = "YJJH";
            ddlSat.SelectedValue = "TS3";
            hfID.Value = "";
            hfPlanType.Value = "";

            btnEdit.Visible = false;
            btnContinue.Visible = false;
            btnSubmit.Visible = true;
            btnGetPlanInfo.Visible = true;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string sID = hfID.Value;
            switch (hfPlanType.Value)
            {
                case "YJJH":
                    Response.Redirect("YJJHEdit.aspx?id=" + sID);
                    break;
                case "MBXQ":
                    Response.Redirect("MBXQEdit.aspx?id=" + sID);
                    break;
                case "HJXQ":
                    Response.Redirect("HJXQEdit.aspx?id=" + sID);
                    break;
                case "DMJH":
                    Response.Redirect("DMJHEdit.aspx?id=" + sID);
                    break;
                case "ZXJH":
                    Response.Redirect("ZXJHEdit.aspx?id=" + sID);
                    break;
                case "TYSJ":
                    Response.Redirect("TYSJEdit.aspx?id=" + sID);
                    break;
            }
        }
    }
}