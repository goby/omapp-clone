﻿using System;
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
using OperatingManagement.Framework.Storage;
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

            List<SYCX> listDatas = (new SYCX()).GetListByDate(startDate, endDate);
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();
        }

        /// <summary>
        /// 生成计划
        /// </summary>
        /// <param name="proid"></param>
        void CreatePlans(int proid)
        {
            JH objJH = new JH();
            objJH.SRCType = 1; //试验程序
            objJH.SRCID = proid;

            #region  应用程序计划
            YJJH objYJJH = new YJJH();
            objYJJH.TaskID = "700任务(0500)";
            objYJJH.SatID = "TS3";
            objYJJH.JXH = (new Sequence()).GetYJJHSequnce().ToString("0000");
            objJH.FileIndex=(new PlanFileCreator()).CreateYJJHFile(objYJJH,0);

            objJH.TaskID = objYJJH.TaskID;
            objJH.PlanType = "YJJH";
            objJH.PlanID = Convert.ToInt32(objYJJH.JXH);
            objJH.Add();
            #endregion

            #region  空间信息需求
            XXXQ objXXXQ = new XXXQ();
            objXXXQ.TaskID = "700任务(0501)";
            objXXXQ.SatID = "TS3";

            MBXQ objMBXQ = new MBXQ();
            objMBXQ.User = PlanParameters.ReadMBXQDefaultUser();
            objMBXQ.TargetInfo = PlanParameters.ReadMBXQDefaultTargetInfo();
            objMBXQ.SatInfos = new List<MBXQSatInfo> { new MBXQSatInfo() };

            HJXQ objHJXQ = new HJXQ();
            objHJXQ.User = PlanParameters.ReadHJXQDefaultUser();
            objHJXQ.EnvironInfo = PlanParameters.ReadHJXQHJXQDefaultEnvironInfo();
            objHJXQ.SatInfos = new List<HJXQSatInfo> { new HJXQSatInfo() };

            objXXXQ.objMBXQ = objMBXQ;
            objXXXQ.objHJXQ = objHJXQ;

            objJH.FileIndex = (new PlanFileCreator()).CreateXXXQFile(objXXXQ, 0);
            objJH.TaskID = objXXXQ.TaskID;
            objJH.PlanType = "XXXQ";
            objJH.PlanID = (new Sequence()).GetXXXQSequnce();
            objJH.Add();
            #endregion

            #region  地面站工作计划
            DMJH objGZJH = new DMJH();
            objGZJH.TaskID = "700任务(0501)";
            objGZJH.SatID = "TS3";
            objGZJH.DMJHTasks = new List<DMJH_Task> 
                        {
                            new DMJH_Task
                            {
                                ReakTimeTransfors = new List<DMJH_Task_ReakTimeTransfor>{new DMJH_Task_ReakTimeTransfor()},
                                AfterFeedBacks = new List<DMJH_Task_AfterFeedBack>{new DMJH_Task_AfterFeedBack()}
                            }
                        };
            objJH.FileIndex = (new PlanFileCreator()).CreateDMJHFile(objGZJH, 0);
            objJH.TaskID = objGZJH.TaskID;
            objJH.PlanType = "DMJH";
            objJH.PlanID = (new Sequence()).GetDMJHSequnce();
            objJH.Add();
            #endregion

            #region  中心运行计划
            ZXJH objZXJH = new ZXJH();
            objZXJH.TaskID = "700任务(0501)";
            objZXJH.SatID = "TS3";
            objZXJH.WorkContents = new List<ZXJH_WorkContent> { new ZXJH_WorkContent() };
            objZXJH.SYDataHandles = new List<ZXJH_SYDataHandle> { new ZXJH_SYDataHandle() };
            objZXJH.DirectAndMonitors = new List<ZXJH_DirectAndMonitor> { new ZXJH_DirectAndMonitor() };
            objZXJH.RealTimeControls = new List<ZXJH_RealTimeControl> { new ZXJH_RealTimeControl() };
            objZXJH.SYEstimates = new List<ZXJH_SYEstimate> { new ZXJH_SYEstimate() };
            objZXJH.DataManages = new List<ZXJH_DataManage> { new ZXJH_DataManage() };
            objJH.FileIndex = (new PlanFileCreator()).CreateZXJHFile(objZXJH, 0);
            objJH.TaskID = objZXJH.TaskID;
            objJH.PlanType = "ZXJH";
            objJH.PlanID = (new Sequence()).GetZXJHSequnce();
            objJH.Add();
            #endregion

            #region  仿真推演试验数据
            TYSJ objTYSJ = new TYSJ();
            objTYSJ.TaskID = "700任务(0501)";
            objTYSJ.SatID = "TS3";

            objJH.FileIndex = (new PlanFileCreator()).CreateTYSJFile(objTYSJ, 0);
            objJH.TaskID = objTYSJ.TaskID;
            objJH.PlanType = "TYSJ";
            objJH.PlanID = (new Sequence()).GetTYSJSequnce();
            objJH.Add();
            #endregion

        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "ExperimentProgram.List";
            this.ShortTitle = "查看试验程序";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/ExperimentProgramList.aspx.js");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath);
        }
    }
}