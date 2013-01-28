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
using OperatingManagement.Framework.Storage;
using OperatingManagement.DataAccessLayer.PlanManage;
using System.Web.Security;
using System.Data;
using ServicesKernel.File;
using OperatingManagement.ServicesKernel.File;


namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class ExperimentProgramList : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Attributes.Add("readonly", "true");
                txtEndDate.Attributes.Add("readonly", "true");
                DefaultSearch();
                HideMessage();
            }
            cpPager.PostBackPage += new EventHandler(cpPager_PostBackPage);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCondition();
                cpPager.CurrentPage = 1;
                BindGridView(true);
                HideMessage();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("实验程序列表页面搜索出现异常，异常原因", ex));
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

        //绑定列表
        void BindGridView(bool fromSearch)
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

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
            }

            List<SYCX> listDatas = (new SYCX()).GetListByDate(startDate, endDate);
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            if (listDatas.Count > this.SiteSetting.PageSize)
                cpPager.Visible = true;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();
        }

        protected void cpPager_PostBackPage(object sender, EventArgs e)
        {
            BindGridView(false);
            HideMessage();
        }

        /// <summary>
        /// 生成计划
        /// </summary>
        /// <param name="proid"></param>
        void CreatePlans(int proid)
        {
            try
            {
                HideMessage();
                string ConfigSYCXPath = System.Configuration.ConfigurationManager.AppSettings["SYCXPath"];
                string fileIndex = "";  //文件路径
                PlanFileCreator creater = new PlanFileCreator(false);
                SYCX objSYCX = new SYCX() { Id=proid };
                objSYCX = objSYCX.SelectById();
                fileIndex = ConfigSYCXPath +objSYCX.FileIndex.Substring(objSYCX.FileIndex.LastIndexOf("\\")+1);
                string savefilepath = "";

                string result="";
                PlanProcessor pp = new PlanProcessor();
                string xxfl = radBtnXXFL.SelectedValue;
                DateTime beginTime = Convert.ToDateTime(txtStartTime.Text);
                DateTime endTime = Convert.ToDateTime(txtEndTime.Text);

                JH objJH = new JH(true);
                objJH.SRCType = 1; //试验程序
                objJH.SRCID = proid;
                objJH.StartTime = beginTime;
                objJH.EndTime = endTime;

                #region  地面站工作计划
                List<GZJH> gjhs= pp.SYCXFile2ZCDMZGZJHs(fileIndex, xxfl, beginTime, endTime, out result);
                if (!result.Equals(string.Empty))
                {
                    ShowMessage(string.Format("生成总参地面站工作计划出错，原因：<br>{0}", result), false);
                    //return;
                }
                if (gjhs.Count > 0)
                {
                    objJH.PlanType = "GZJH";
                    for (int i = 0; i < gjhs.Count(); i++)
                    {
                        objJH.PlanID = (new Sequence()).GetGZJHSequnce();
                        gjhs[i].JXH = objJH.PlanID.ToString();

                        savefilepath = creater.CreateGZJHFile(gjhs[i], 0);

                        objJH.TaskID = gjhs[i].TaskID;
                        objJH.FileIndex = savefilepath;
                        objJH.SatID = gjhs[i].SatID;
                        objJH.Add();
                    }
                }
                else
                {
                    ShowMessage("未能生成地面站工作计划<br>", true);
                    //return;
                }
                #endregion

                #region  中心运行计划
                ZXJH zjh = pp.SYCXFile2ZXJH(fileIndex, xxfl, beginTime, endTime, out result);
                if (!result.Equals(string.Empty))
                {
                    ShowMessage(string.Format("生成中心运行计划出错，原因：<br>{0}", result), true);
                    //return;
                }

                if (null != zjh && zjh.SYContents != null)
                {
                    savefilepath = creater.CreateZXJHFile(zjh, 0);

                    objJH.PlanType = "ZXJH";
                    objJH.PlanID = (new Sequence()).GetZXJHSequnce();
                    objJH.TaskID = zjh.TaskID;
                    objJH.FileIndex = savefilepath;
                    objJH.SatID = zjh.SatID;
                    objJH.Add();
                }
                else
                {
                    ShowMessage("未能生成中心运行计划<br>", true);
                    //return;
                }
                #endregion

                #region 测控资源申请
                List<DJZYSQ> djhs = pp.SYCXFile2CKZYSYSQ(fileIndex, xxfl, beginTime, endTime, out result);
                if (!result.Equals(string.Empty))
                {
                    ShowMessage(string.Format("生成测控资源申请出错，原因：<br>{0}", result), true);
                    //return;
                }
                if (djhs.Count > 0)
                {
                    objJH.PlanType = "DJZYSQ";
                    for (int i = 0; i < djhs.Count(); i++)
                    {
                        objJH.PlanID = (new Sequence()).GetDJZYSQSequnce();
                        djhs[i].SNO = objJH.PlanID.ToString();
                        savefilepath = creater.CreateDMJHFile(djhs[i], 0);

                        objJH.TaskID = djhs[i].TaskID;
                        objJH.FileIndex = savefilepath;
                        objJH.SatID = djhs[i].SatID;
                        objJH.Add();
                    }
                }
                else
                {
                    ShowMessage("未能生成测控资源使用申请", true);
                    return;
                }

                #endregion
                ShowMessage("计划生成成功。", false);
            }
            catch (Exception ex)
            {
                throw (new AspNetException("生成计划出现异常，异常原因", ex));
            }
            finally { }
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMPLAN_ExProgram.View";
            this.ShortTitle = "查看试验程序";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/ExperimentProgramList.aspx.js");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath);
            HideMessage();
        }

        protected void btnCreatePlan_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);
            CreatePlans(id);
            BindGridView(true);
        }

        private void ShowMessage(string msg, bool join)
        {
            trMessage.Visible = true;
            if (join)
                ltMessage.Text += msg;
            else
                ltMessage.Text = msg;
        }

        private void HideMessage()
        {
            trMessage.Visible = false;
            ltMessage.Text = "";
        }
    }
}