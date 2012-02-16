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
using System.Xml;

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
                hfSBJHID.Value = "-1";
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
            PlanFileCreator fileCreater = new PlanFileCreator();
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
                    if (hfSBJHID.Value != "-1")
                    {
                        #region 从设备工作计划获取信息
                        List<JH> jh = (new JH()).SelectByIDS(hfSBJHID.Value);
                        objDMJH.TaskID = jh[0].TaskID;
                        string[] strTemp = jh[0].FileIndex.Split('_');
                        if (strTemp.Length >= 2)
                        {
                            objDMJH.SatID = strTemp[strTemp.Length - 2];
                        }
                        objDMJH.DMJHTasks.Clear();

                        DMJH_Task task;
                        DMJH_Task_ReakTimeTransfor rt;
                        DMJH_Task_AfterFeedBack afb;
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(jh[0].FileIndex);
                        XmlNode root = xmlDoc.SelectSingleNode("设备工作计划/编号");
                        objDMJH.Sequence= root.InnerText;
                        root = xmlDoc.SelectSingleNode("设备工作计划/时间");
                        objDMJH.DateTime = root.InnerText;
                        root = xmlDoc.SelectSingleNode("设备工作计划/工作单位");
                        objDMJH.StationName = root.InnerText;
                        root = xmlDoc.SelectSingleNode("设备工作计划/设备代号");
                        objDMJH.EquipmentID = root.InnerText;
                        root = xmlDoc.SelectSingleNode("设备工作计划/任务个数");
                        objDMJH.TaskCount= root.InnerText;

                        root = xmlDoc.SelectSingleNode("设备工作计划");
                        foreach (XmlNode n in root.ChildNodes)
                        {
                            if (n.Name == "任务")
                            {
                                task = new DMJH_Task();
                                task.TaskFlag = n["任务标志"].InnerText;
                                task.WorkWay = n["工作方式"].InnerText;
                                task.PlanPropertiy = n["计划性质"].InnerText;
                                task.WorkMode = n["工作模式"].InnerText;
                                task.PreStartTime = n["任务准备开始时间"].InnerText;
                                task.StartTime = n["任务开始时间"].InnerText;
                                task.TrackStartTime = n["跟踪开始时间"].InnerText;
                                task.WaveOnStartTime = n["开上行载波时间"].InnerText;
                                task.WaveOffStartTime = n["关上行载波时间"].InnerText;
                                task.TrackEndTime = n["跟踪结束时间"].InnerText;
                                task.EndTime = n["任务结束时间"].InnerText;
                                task.ReakTimeTransfors = new List<DMJH_Task_ReakTimeTransfor>();
                                task.AfterFeedBacks = new List<DMJH_Task_AfterFeedBack>();
                                foreach (XmlNode nd in n)
                                {
                                    if (nd.Name == "实时传输")
                                    {
                                        rt = new DMJH_Task_ReakTimeTransfor();
                                        rt.FormatFlag = nd["格式标志"].InnerText;
                                        rt.InfoFlowFlag = nd["信息流标志"].InnerText;
                                        rt.TransStartTime = nd["数据传输开始时间"].InnerText;
                                        rt.TransEndTime = nd["数据传输结束时间"].InnerText;
                                        rt.TransSpeedRate = nd["数据传输速率"].InnerText;
                                        task.ReakTimeTransfors.Add(rt);
                                    }
                                    if (nd.Name == "事后回放")
                                    {
                                        afb = new DMJH_Task_AfterFeedBack();
                                        afb.FormatFlag = nd["格式标志"].InnerText;
                                        afb.InfoFlowFlag = nd["信息流标志"].InnerText;
                                        afb.DataStartTime = nd["数据起始时间"].InnerText;
                                        afb.DataEndTime = nd["数据结束时间"].InnerText;
                                        afb.TransStartTime = nd["数据传输开始时间"].InnerText;
                                        afb.TransSpeedRate = nd["数据传输速率"].InnerText;
                                        task.AfterFeedBacks.Add(afb);
                                    }
                                }
                                objDMJH.DMJHTasks.Add(task);
                            }
                        }
                        #endregion
                    }
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
            this.AddJavaScriptInclude("scripts/pages/PlanAdd.aspx.js");
        }

        protected void txtGetPlanInfo_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        //绑定列表
        void BindGridView()
        {
            
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            startDate = new DateTime(1900, 1, 1);
            endDate = DateTime.Now.AddDays(1);
            string planType = "";

            List<JH> listDatas = (new JH()).GetJHList(planType, startDate, endDate);
            cpPager.DataSource = listDatas;
            cpPager.PageSize = 7;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();
        }

        public  string GetFileName(object objfilepath)
        {
            string filename = "";
            string filepath = Convert.ToString(objfilepath);
            string savepath = System.Configuration.ConfigurationManager.AppSettings["savepath"];
            filename=filepath.Replace(savepath, "");
            return filename;
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

        protected void btnSBJH_Click(object sender, EventArgs e)
        {
            hfSBJHID.Value = "-1";
            btnSBJH.Text = "";
        }
    }
}