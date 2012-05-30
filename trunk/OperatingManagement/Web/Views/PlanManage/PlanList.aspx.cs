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
                //pnlDestination.Visible = false;
                //pnlData.Visible = true;

                pnlAll1.Visible = false;
                pnlAll2.Visible = false;

                lblMessage.Text = ""; //文件发送消息清空
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.Text = ""; //文件发送消息清空
            BindGridView();
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        void BindGridView()
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
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
                endDate = Convert.ToDateTime(txtEndDate.Text);
            }
            else
            {
                endDate = DateTime.Now.AddDays(1);
            }
            string planType = null;
            if (ddlType.SelectedItem.Value != "0")
            { 
                planType = ddlType.SelectedItem.Value;
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

        /// <summary>
        /// 最终发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
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
                            SendingFilePaths = creater.CreateSendingYJJHFile(txtId.Text, li.Value, li.Text);
                            break;
                        case "XXXQ":
                            SendingFilePaths = creater.CreateSendingXXXQFile(txtId.Text, li.Value, li.Text);
                            break;
                        case "DMJH":
                        case "GZJH":
                            SendingFilePaths = creater.CreateSendingDMJHFile(txtId.Text, li.Value, li.Text);
                            break;
                        case "TYSJ":
                            SendingFilePaths = creater.CreateSendingTYSJFile(txtId.Text, li.Value, li.Text);
                            break;
                    }

                    XYXSInfo objXYXSInfo = new XYXSInfo();
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
                        boolResult = objSender.SendFile(GetFileNameByFilePath(filePaths[i]), filePaths[i], CommunicationWays.FEPwithTCP, senderid, reveiverid, infotypeid, true);
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
            txtPlanType.Text = plantype;
            switch (plantype)
            {
                case "YJJH":
                    ckbDestination.Items.Clear();
                    ckbDestination.Items.Add(new ListItem("天基目标观测应用研究分系统（GCYJ）", "GCYJ"));
                    ckbDestination.Items.Add(new ListItem("遥操作应用研究分系统（CZYJ）", "CZYJ"));
                    ckbDestination.Items.Add(new ListItem("空间机动应用研究分系统（JDYJ）", "JDYJ"));
                    ckbDestination.Items.Add(new ListItem("仿真推演分系统（FZTY）", "FZTY"));
                    break;
                case "XXXQ":
                    ckbDestination.Items.Clear();
                    ckbDestination.Items.Add(new ListItem("空间信息综合应用中心(XXZX)", "XXZX"));
                    break;
                case "DMJH":
                case "GZJH":
                    ckbDestination.Items.Clear();
                    ckbDestination.Items.Add(new ListItem("西安中心（XSCC）", "XSCC"));
                    ckbDestination.Items.Add(new ListItem("总参二部信息处理中心（XXZX）", "XXZX"));
                    ckbDestination.Items.Add(new ListItem("总参三部技侦中心（JZZX）", "JZZX"));
                    ckbDestination.Items.Add(new ListItem("总参气象水文空间天气总站资料处理中心（ZLZX）", "ZLZX"));
                    ckbDestination.Items.Add(new ListItem("863-YZ4701遥科学综合站（JYZ1）", "JYZ1"));
                    ckbDestination.Items.Add(new ListItem("863-YZ4702遥科学综合站（JYZ2）", "JYZ2"));
                    break;
                case "ZXJH":
                    ckbDestination.Items.Clear();
                    break;
                case "TYSJ":
                    ckbDestination.Items.Clear();
                    ckbDestination.Items.Add(new ListItem("仿真推演分系统(FZTY)", "FZTY"));
                    break;
                case "SBJH":
                    ckbDestination.Items.Clear();
                    //ckbDestination.Items.Add(new ListItem("运控评估中心YKZX(02 04 00 00)", "YKZX"));
                    break;
            }
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