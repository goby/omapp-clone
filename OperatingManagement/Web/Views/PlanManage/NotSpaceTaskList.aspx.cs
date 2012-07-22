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
    public partial class NotSpaceTaskList : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtStartDate.Attributes.Add("readonly", "true");
                txtEndDate.Attributes.Add("readonly", "true");
                //btnSend.Attributes.Add("onclick", "javascript:return confirm('确定要发送所选数据吗?');");

                pnlDestination.Visible = false;
                pnlData.Visible = true;

                pnlAll1.Visible = false;
                pnlAll2.Visible = false;
                BindCheckBoxDestination();
                DefaultSearch();
                ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>hideSelectAll();</script>");
               // ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>showMsgError();</script>");
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
            }
            catch (Exception ex)
            {
                throw (new AspNetException("非空间机动任务列表页面搜索出现异常，异常原因", ex));
            }
            finally { }
        }
        /// <summary>
        /// 默认查询两周内的数据
        /// </summary>
        private void DefaultSearch()
        {
            txtStartDate.Text = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            btnSearch_Click(new Object(), new EventArgs());
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
                    endDate = Convert.ToDateTime(txtEndDate.Text).AddDays(1).AddMilliseconds(-1);   //查询时可查当天
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
           
            //List<YDSJ> listDatas = (new YDSJ()).GetListByDate(startDate, endDate, "2");
            List<GD> listDatas = (new GD()).GetListByDate(startDate, endDate);
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
        /// 绑定发送目标
        /// </summary>
        void BindCheckBoxDestination()
        {
            List<string> targetList;
            ckbDestination.Items.Clear();
            targetList = FileExchangeConfig.GetTgtListForSending("GDGS");
            foreach (string tgt in targetList)
            {
                ckbDestination.Items.Add(new ListItem(FileExchangeConfig.GetNameForType(tgt), tgt));
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            BindCheckBoxDestination();
        }
        //最终发送
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
                        SendingFilePaths = creater.CreateSendingGDGSFile(txtId.Text, li.Value);

                        XYXSInfo objXYXSInfo = new XYXSInfo();
                        //发送协议
                        CommunicationWays protocl = (CommunicationWays)Convert.ToInt32(rbtProtocl.SelectedValue);
                        //发送方ID （运控中心 YKZX）
                        int senderid = objXYXSInfo.GetIdByAddrMark(System.Configuration.ConfigurationManager.AppSettings["ZXBM"]);
                        //接收方ID 
                        int reveiverid = objXYXSInfo.GetIdByAddrMark(li.Value);
                        //信息类型id
                        int infotypeid = (new InfoType()).GetIDByExMark("GD");
                        bool boolResult = true; //文件发送结果
                        FileSender objSender = new FileSender();
                        string[] filePaths = SendingFilePaths.Split(',');
                        for (int i = 0; i < filePaths.Length; i++)
                        {
                            boolResult = objSender.SendFile(GetFileNameByFilePath(filePaths[i]), GetFilePathByFilePath(filePaths[i]), protocl, senderid, reveiverid, infotypeid, true);
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
            pnlDestination.Visible = false;
            pnlData.Visible = true;
        }

        protected void btnHidden_Click(object sender, EventArgs e)
        {
            pnlDestination.Visible = true;
            pnlData.Visible = false;
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "NotSpaceTask.List";
            this.ShortTitle = "查看非空间机动任务";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/NotSpaceTaskList.aspx.js");
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
            return filepath.Substring(filepath.LastIndexOf("\\") + 1);
        }

        /// <summary>
        /// 获取文件完整路径下的文件路径
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private string GetFilePathByFilePath(string filepath)
        {
            return filepath.Substring(0, filepath.LastIndexOf("\\") + 1);
        }
    }
}