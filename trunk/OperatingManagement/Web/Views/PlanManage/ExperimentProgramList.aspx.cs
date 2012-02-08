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
    public partial class ExperimentProgramList : AspNetPage, IRouteContext
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

            List<SYCX> listDatas = (new SYCX()).GetSYCXListByDate(startDate, endDate);
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();
        }

        void CreatePlans(int proid)
        {
            JH objJH = new JH();
            objJH.SRCType = 1; //试验程序
            objJH.SRCID = proid;

            #region  应用程序计划
            YJJH objYJJH = new YJJH();
            objYJJH.Source = "运控评估中心YKZX(02 04 00 00)";
            objYJJH.Destination = "仿真推演分系统FZTY(02 E7 00 00)";
            objYJJH.TaskID = "700任务(0500)";
            objYJJH.InfoType = "应用研究计划(00 70 06 00)";
            objYJJH.SatID = "TS3";
            objJH.FileIndex=(new CreatePlanFile()).CreateYJJHFile(objYJJH,0);

            objJH.TaskID = objYJJH.TaskID;
            objJH.PlanType = "YJJH";
            objJH.PlanID = objYJJH.Id;
            objJH.Add();
            #endregion

            #region  空间信息需求
            //XXXQ objXXXQ = new XXXQ();
            //objXXXQ.ID = 6;
            //objXXXQ.Source = "111运控评估中心YKZX(02 04 00 00)";
            //objXXXQ.Destination = "空间信息综合应用中心ZCZX(02 6F 00 00)";
            //objXXXQ.TaskID = "700任务(0501)";
            //objXXXQ.InfoType = "空间目标信息需求(00 70 60 00)";
            //objXXXQ.Format1 = "User  Time  TargetInfo  TimeSection1  TimeSection2  Sum";
            //objXXXQ.Format2 = "SatName  InfoName  InfoTime";
            //objXXXQ.SatID = "TS3";
            MBXQ objMBXQ = new MBXQ();
            objMBXQ.TaskID = "700任务(0501)";
            objMBXQ.SatID = "TS3";
            objJH.FileIndex = (new CreatePlanFile()).CreateMBXQFile(objMBXQ,0);
            objJH.TaskID = objMBXQ.TaskID;
            objJH.PlanType = "MBXQ";
            objJH.PlanID = objMBXQ.ID;
            objJH.Add();

            HJXQ objHJXQ = new HJXQ();
            objHJXQ.TaskID = "700任务(0501)";
            objHJXQ.SatID = "TS3";
            objJH.FileIndex = (new CreatePlanFile()).CreateHJXQFile(objHJXQ, 0);
            objJH.TaskID = objHJXQ.TaskID;
            objJH.PlanType = "HJXQ";
            objJH.PlanID = objHJXQ.ID;
            objJH.Add();
            #endregion

            #region  地面站工作计划
            DMJH objGZJH = new DMJH();
            objGZJH.Source = "运控评估中心YKZX(02 04 00 00)";
            objGZJH.Destination = "空间信息综合应用中心ZCZX(02 6F 00 00)";
            objGZJH.TaskID = "700任务(0501)";
            objGZJH.InfoType = "地面站工作计划(00 70 60 00)";
            objGZJH.Format1 = "JXH  XXFL  DW  SB  QS";
            objGZJH.Format2 = "QH  DH  FS  JXZ  MS  QB  GXZ  ZHB  RK  GZK  KSHX  GSHX  GZJ  JS  BID  SBZ  RTs  RTe  SL  BID  HBZ  Ts  Te  RTs  SL";
            objGZJH.SatID = "TS3";
            objJH.FileIndex = (new CreatePlanFile()).CreateDMJHFile(objGZJH, 0);
            objJH.TaskID = objGZJH.TaskID;
            objJH.PlanType = "DMJH";
            objJH.PlanID = objGZJH.ID;
            objJH.Add();
            #endregion

            #region  中心运行计划
            ZXJH objZXJH = new ZXJH();
            objZXJH.TaskID = "700任务(0501)";
            objZXJH.SatID = "TS3";
            objJH.FileIndex = (new CreatePlanFile()).CreateZXJHFile(objZXJH, 0);
            objJH.TaskID = objZXJH.TaskID;
            objJH.PlanType = "ZXJH";
            objJH.PlanID = objZXJH.ID;
            objJH.Add();
            #endregion

            #region  仿真推演试验数据
            TYSJ objTYSJ = new TYSJ();
            objTYSJ.Source = "运控评估中心YKZX(02 04 00 00)";
            objTYSJ.Destination = "仿真推演分系统FZTY(02 E7 00 00)";
            objTYSJ.TaskID = "700任务(0501)";
            objTYSJ.InfoType = "仿真推演数据(00 70 32 00)";
            objTYSJ.Format1 = "SatName  Type  TestItem  StartTime  EndTime  Condition";
            objTYSJ.SatID = "TS3";

            objJH.FileIndex = (new CreatePlanFile()).CreateTYSJFile(objTYSJ, 0);
            objJH.TaskID = objTYSJ.TaskID;
            objJH.PlanType = "TYSJ";
            objJH.PlanID = objTYSJ.Id;
            objJH.Add();
            #endregion

        }

        
        public override void OnPageLoaded()
        {
            this.PagePermission = "ExperimentProgram.List";
            this.ShortTitle = "查看试验程序";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/ExperimentProgramList.aspx.js");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath);
        }
    }
}