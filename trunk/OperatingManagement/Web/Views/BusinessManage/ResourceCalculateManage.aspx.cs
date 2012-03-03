#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:ResourceCalculateManage.cs
//Remark:资源计算管理类
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20120229    Create     
//------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OperatingManagement.DataAccessLayer.BusinessManage;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class ResourceCalculateManage : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDataSource();
                    BindResourceCalculateList();
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 查询资源调度计算结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"起始时间不能为空。\")", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtEndTime.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"结束时间不能为空。\")", true);
                    return;
                }
                DateTime beginTime = DateTime.MinValue;
                DateTime endTime = DateTime.MinValue;
                if (!DateTime.TryParse(txtBeginTime.Text.Trim(), out beginTime))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"起始时间格式错误。\")", true);
                    return;
                }

                if (!DateTime.TryParse(txtEndTime.Text.Trim(), out endTime))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"结束时间格式错误。\")", true);
                    return;
                }

                if (beginTime > endTime)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"起始时间应小于结束时间。\")", true);
                    return;
                }

                BindResourceCalculateList();
            }
            catch
            { }
        }
        /// <summary>
        /// 添加资源调度计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string url = @"~/Views/BusinessManage/ResourceRequirementAdd.aspx";
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 查看资源调度计算结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnCalculateResultView_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnCalculateResultView = (sender as LinkButton);
                if (lbtnCalculateResultView == null)
                {
                    BindResourceCalculateList();
                    return;
                }
                string url = @"~/Views/BusinessManage/ResourceCalculateView.aspx?rcid=" + Server.UrlEncode(lbtnCalculateResultView.CommandArgument);
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }

        /// <summary>
        /// 下载资源需求文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnRequirementFileDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnRequirementFileDownload = (sender as LinkButton);
                if (lbtnRequirementFileDownload == null)
                {
                    BindResourceCalculateList();
                    return;
                }
                int rcID = 0;
                if (!int.TryParse(lbtnRequirementFileDownload.CommandArgument, out rcID))
                {
                    BindResourceCalculateList();
                    return;
                }

                ResourceCalculate resourceCalculate = new ResourceCalculate();
                resourceCalculate.Id = rcID;
                resourceCalculate = resourceCalculate.SelectByID();
                if (resourceCalculate == null)
                {
                    BindResourceCalculateList();
                    return;
                }
                string fileName = resourceCalculate.RequirementFileDirectory.TrimEnd('\\') + '\\' + resourceCalculate.RequirementFileName;
                System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                xmlDocument.Load(fileName);
           
                Response.Clear();
                Response.Buffer = false;
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "attachment;filename=" + resourceCalculate.RequirementFileDisplayName + ";");
                Response.Write(xmlDocument.OuterXml);
                Response.Flush();
                Response.End();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"下载资源需求文件失败。\")", true);
            }
        }

        /// <summary>
        /// 下载计算结果文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnResultFileDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnResultFileDownload = (sender as LinkButton);
                if (lbtnResultFileDownload == null)
                {
                    BindResourceCalculateList();
                    return;
                }
                int rcID = 0;
                if (!int.TryParse(lbtnResultFileDownload.CommandArgument, out rcID))
                {
                    BindResourceCalculateList();
                    return;
                }

                ResourceCalculate resourceCalculate = new ResourceCalculate();
                resourceCalculate.Id = rcID;
                resourceCalculate = resourceCalculate.SelectByID();
                if (resourceCalculate == null)
                {
                    BindResourceCalculateList();
                    return;
                }
                string fileName = resourceCalculate.ResultFileDirectory.TrimEnd('\\') + '\\' + resourceCalculate.ResultFileName;
                System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                xmlDocument.Load(fileName);

                Response.Clear();
                Response.Buffer = false;
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "attachment;filename=" + resourceCalculate.ResultFileDisplayName + ";");
                Response.Write(xmlDocument.OuterXml);
                Response.Flush();
                Response.End();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"下载计算结果文件失败。\")", true);
            }
        }

        #region ItemDataBound
        /// <summary>
        /// 资源计算结果单条数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpResourceCalculateList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lbtnRequirementFileDownload = (e.Item.FindControl("lbtnRequirementFileDownload") as LinkButton);
                LinkButton lbtnResultFileDownload = (e.Item.FindControl("lbtnResultFileDownload") as LinkButton);
                LinkButton lbtnCalculateResultView = (e.Item.FindControl("lbtnCalculateResultView") as LinkButton);
                if (lbtnRequirementFileDownload != null && lbtnResultFileDownload != null && lbtnCalculateResultView != null)
                {
                    ResourceCalculate resourceCalculate = (e.Item.DataItem as ResourceCalculate);
                    lbtnRequirementFileDownload.Enabled = (resourceCalculate.ResultFileSource == 1);
                    lbtnResultFileDownload.Enabled = (resourceCalculate.ResultFileSource == 2 || resourceCalculate.Status == 2);
                    lbtnCalculateResultView.Enabled = (resourceCalculate.ResultFileSource == 2 || resourceCalculate.CalculateResult == 1);
                }
            }
        }

        #endregion

        public override void OnPageLoaded()
        {
            this.ShortTitle = "资源调度计算结果查询";
            this.SetTitle();
        }

        #region Method
        /// <summary>
        /// 绑定计算状态、计算结果文件来源数据源
        /// </summary>
        private void BindDataSource()
        {
            dplStatus.Items.Clear();
            dplStatus.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.ResourceCalculateStatus);
            dplStatus.DataTextField = "key";
            dplStatus.DataValueField = "value";
            dplStatus.DataBind();
            dplStatus.Items.Insert(0, new ListItem("全部", "0"));

            dplResultFileSource.Items.Clear();
            dplResultFileSource.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.ResourceCalculateResultFileSource);
            dplResultFileSource.DataTextField = "key";
            dplResultFileSource.DataValueField = "value";
            dplResultFileSource.DataBind();
            dplResultFileSource.Items.Insert(0, new ListItem("全部", "0"));

            txtBeginTime.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 绑定资源调度计算结果信息
        /// </summary>
        private void BindResourceCalculateList()
        {
            ResourceCalculate resourceCalculate = new ResourceCalculate();
            int resultFileSource = Convert.ToInt32(dplResultFileSource.SelectedValue);
            int status = Convert.ToInt32(dplStatus.SelectedValue);
            DateTime beginTime = Convert.ToDateTime(txtBeginTime.Text.Trim());
            DateTime endTime = Convert.ToDateTime(txtEndTime.Text.Trim()).AddSeconds(86399.9);//23:59:59

            cpResourceCalculatePager.DataSource = resourceCalculate.Search(resultFileSource, status, beginTime, endTime);
            cpResourceCalculatePager.PageSize = this.SiteSetting.PageSize;
            cpResourceCalculatePager.BindToControl = rpResourceCalculateList;
            rpResourceCalculateList.DataSource = cpResourceCalculatePager.DataSourcePaged;
            rpResourceCalculateList.DataBind();
        }

        #endregion
    }
}