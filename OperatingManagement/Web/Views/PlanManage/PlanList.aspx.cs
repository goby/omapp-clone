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
    public partial class PlanList : AspNetPage, IRouteContext
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.List";
            this.ShortTitle = "查询计划";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/PlanList.aspx.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //pnlDestination.Visible = false;
                    //pnlData.Visible = true;

                    txtStartDate.Attributes.Add("readonly", "true");
                    txtEndDate.Attributes.Add("readonly", "true");

                    pnlAll1.Visible = false;
                    pnlAll2.Visible = false;

                    lblMessage.Text = ""; //文件发送消息清空

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
                else
                {
                    startDate = DateTime.Now.AddDays(-14);  //默认查询14天的数据
                }
                if (!string.IsNullOrEmpty(txtEndDate.Text))
                {
                    //endDate = Convert.ToDateTime(txtEndDate.Text);
                    endDate = Convert.ToDateTime(txtEndDate.Text).AddDays(1).AddMilliseconds(-1); //查询时可查当天
                }
                else
                {
                    endDate = DateTime.Now.AddDays(1).AddMilliseconds(-1);
                }

                if (ddlType.SelectedItem.Value != "0")
                {
                    planType = ddlType.SelectedItem.Value;
                }
            }
            else
            {
                if (ViewState["_StartDate"] == null)
                {
                    startDate = DateTime.Now.AddDays(-14);
                }
                else
                {
                    startDate = Convert.ToDateTime(ViewState["_StartDate"].ToString());
                }
                if (ViewState["_EndDate"] == null)
                {
                    endDate = DateTime.Now.AddDays(1);
                }
                else
                {
                    endDate = Convert.ToDateTime(ViewState["_EndDate"].ToString());
                }
                if (ViewState["_PlanType"].ToString() != "0")
                {
                    planType = ViewState["_PlanType"].ToString();
                }
            }
            

            List<JH> listDatas = (new JH()).GetJHList(planType, startDate, endDate);
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();

            if (listDatas.Count > 0)
            {
                pnlAll1.Visible = true;
                pnlAll2.Visible = true;
            }
            else
            {
                pnlAll1.Visible = false;
                pnlAll2.Visible = false;
            }
        }

        protected void cpPager_PostBackPage(object sender, EventArgs e)
        {
            BindGridView(false);
        }

        /// <summary>
        /// 最终发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                PlanFileCreator creater = new PlanFileCreator();
                string SendingFilePaths = "";
                lblMessage.Text = "";//清空发送信息
                foreach (ListItem li in ckbDestination.Items)
                {
                    if (li.Selected)
                    {

                        switch (txtPlanType.Text)
                        {
                            case "YJJH":
                                SendingFilePaths = creater.CreateSendingYJJHFile(txtId.Text, li.Value);
                                break;
                            case "XXXQ":
                                SendingFilePaths = creater.CreateSendingXXXQFile(txtId.Text, li.Value);
                                break;
                            case "DMJH":
                            case "GZJH":
                                SendingFilePaths = creater.CreateSendingGZJHFile(txtId.Text, li.Value);
                                break;
                            case "TYSJ":
                                SendingFilePaths = creater.CreateSendingTYSJFile(txtId.Text, li.Value);
                                break;
                        }

                        XYXSInfo objXYXSInfo = new XYXSInfo();
                        //发送协议
                        CommunicationWays protocl = (CommunicationWays)Convert.ToInt32(rbtProtocl.SelectedValue);
                        //发送方ID （运控中心 YKZX）
                        int senderid = objXYXSInfo.GetIdByAddrMark(System.Configuration.ConfigurationManager.AppSettings["ZXBM"]);
                        //接收方ID 
                        int reveiverid = objXYXSInfo.GetIdByAddrMark(li.Value);
                        //信息类型id
                        int infotypeid = (new InfoType()).GetIDByExMark(txtPlanType.Text);
                        bool boolResult = true; //文件发送结果
                        FileSender objSender = new FileSender();
                        string[] filePaths = SendingFilePaths.Split(',');
                        for (int i = 0; i < filePaths.Length; i++)
                        {
                            //if (txtPlanType.Text == "XXXQ")
                            //{
                            //    if (filePaths[i].Contains("MBXQ"))
                            //    {
                            //        infotypeid = (new InfoType()).GetIDByExMark("MBXX");
                            //    }
                            //    else if (filePaths[i].Contains("HJXX"))
                            //    {
                            //        infotypeid = (new InfoType()).GetIDByExMark("HJXX");
                            //    }
                            //}
                            boolResult = objSender.SendFile(GetFileNameByFilePath(filePaths[i]), filePaths[i], protocl, senderid, reveiverid, infotypeid, true);
                            if (boolResult)
                            {
                                lblMessage.Text += GetFileNameByFilePath(filePaths[i]) + " 文件发送请求提交成功。" + "<br />";
                            }
                            else
                            {
                                lblMessage.Text += GetFileNameByFilePath(filePaths[i]) + " 文件发送请求提交失败。" + "<br />";
                            }
                        }

                    }//li
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("发送计划出现异常，异常原因", ex));
            }
            finally { }
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //pnlDestination.Visible = false;
            //pnlData.Visible = true;
        }

        protected void btnHidden_Click(object sender, EventArgs e)
        {
            //pnlDestination.Visible = true;
            //pnlData.Visible = false;
            //string plantype = txtPlanType.Text;
            string plantype = ddlType.SelectedValue;
            if (plantype =="DMJH")
            { plantype = "GZJH"; }
            txtPlanType.Text = plantype;
            List<string> targetList;
            ckbDestination.Items.Clear();
            targetList = FileExchangeConfig.GetTgtListForSending(plantype);
            foreach (string tgt in targetList)
            {
                ckbDestination.Items.Add(new ListItem(FileExchangeConfig.GetNameForType(tgt), tgt));
            }

            #region Add Destination 
            //switch (plantype)
            //{
            //    case "YJJH":
            //        ckbDestination.Items.Clear();
            //        ckbDestination.Items.Add(new ListItem("天基目标观测应用研究分系统（GCYJ）", "GCYJ"));
            //        ckbDestination.Items.Add(new ListItem("遥操作应用研究分系统（CZYJ）", "CZYJ"));
            //        ckbDestination.Items.Add(new ListItem("空间机动应用研究分系统（JDYJ）", "JDYJ"));
            //        ckbDestination.Items.Add(new ListItem("仿真推演分系统（FZTY）", "FZTY"));
            //        break;
            //    case "XXXQ":
            //        ckbDestination.Items.Clear();
            //        ckbDestination.Items.Add(new ListItem("空间信息综合应用中心(XXZX)", "XXZX"));
            //        break;
            //    case "DMJH":
            //    case "GZJH":
            //        ckbDestination.Items.Clear();
            //        ckbDestination.Items.Add(new ListItem("西安中心（XSCC）", "XSCC"));
            //        ckbDestination.Items.Add(new ListItem("总参二部信息处理中心（XXZX）", "XXZX"));
            //        ckbDestination.Items.Add(new ListItem("总参三部技侦中心（JZZX）", "JZZX"));
            //        ckbDestination.Items.Add(new ListItem("总参气象水文空间天气总站资料处理中心（ZLZX）", "ZLZX"));
            //        ckbDestination.Items.Add(new ListItem("863-YZ4701遥科学综合站（JYZ1）", "JYZ1"));
            //        ckbDestination.Items.Add(new ListItem("863-YZ4702遥科学综合站（JYZ2）", "JYZ2"));
            //        break;
            //    case "ZXJH":
            //        ckbDestination.Items.Clear();
            //        break;
            //    case "TYSJ":
            //        ckbDestination.Items.Clear();
            //        ckbDestination.Items.Add(new ListItem("仿真推演分系统(FZTY)", "FZTY"));
            //        break;
            //    case "SBJH":
            //        ckbDestination.Items.Clear();
            //        //ckbDestination.Items.Add(new ListItem("运控评估中心YKZX(02 04 00 00)", "YKZX"));
            //        break;
            //}
            #endregion
            if (ckbDestination.Items.Count > 0)
            {
                ckbDestination.SelectedIndex = 0;
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "<script type='text/javascript'>showPopSendForm();</script>");
            
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath);
        }

        /// <summary>
        /// 获取文件完整路径下的文件名
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private string GetFileNameByFilePath(string filepath)
        {
            return filepath.Substring(filepath.LastIndexOf("\\")+1);
        }
    }
}