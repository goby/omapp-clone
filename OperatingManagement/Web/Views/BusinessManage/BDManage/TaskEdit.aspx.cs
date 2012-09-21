using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.Framework.Core;

namespace OperatingManagement.Web.Views.BusinessManage.BDManage
{
    public partial class TaskEdit : AspNetPage
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_TaskMan.Edit";
            this.ShortTitle = "编辑任务";
            this.SetTitle();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ucCBLSats.RepeatColumns = 2;
                BindTask();
            }
        }

        void BindTask()
        {
            int taskId = Convert.ToInt32(Request.QueryString["Id"]);
            DataAccessLayer.BusinessManage.Task t = new DataAccessLayer.BusinessManage.Task() { Id = taskId };
            var task = new DataAccessLayer.BusinessManage.Task();
            try
            {
                task = t.SelectById();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("修改任务页面读取任务数据出现异常，异常原因", ex));
            }
            finally { }

            txtTaskName.Text = task.TaskName;
            txtTaskNo.Text = task.TaskNo;
            txtObjectFlag.Text = task.ObjectFlag;
            ucCBLSats.SelectedValues = task.SatID;
            rblCurTask.SelectedValue = task.IsCurTask;
            txtFrom.Text = task.BeginTime.ToString("yyyy-MM-dd");
            txtTo.Text = task.EndTime.ToString("yyyy-MM-dd");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            DataAccessLayer.BusinessManage.Task t = new DataAccessLayer.BusinessManage.Task()
            {
                Id = Convert.ToInt32(Request.QueryString["Id"]),
                TaskName = txtTaskName.Text.Trim(),
                TaskNo = txtTaskNo.Text.Trim(),
                ObjectFlag = txtObjectFlag.Text.Trim(),
                SatID = ucCBLSats.SelectedValues.ToString(),
                IsCurTask = rblCurTask.SelectedValue.ToString(),
                BeginTime = DateTime.ParseExact(txtFrom.Text.Trim(), "yyyy-MM-dd", provider),
                EndTime = DateTime.ParseExact(txtTo.Text.Trim(), "yyyy-MM-dd", provider)
            };
            var result = Framework.FieldVerifyResult.Error;
            try
            {
                result = t.Update();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("修改任务页面保存任务数据出现异常，异常原因", ex));
            }
            finally { }

            string msg = string.Empty;
            switch (result)
            {
                case Framework.FieldVerifyResult.NameDuplicated:
                    msg = "已存在相同名称，请输入其他“任务名称”。";
                    break;
                case Framework.FieldVerifyResult.Error:
                    msg = "发生了数据错误，无法完成请求的操作。";
                    break;
                case Framework.FieldVerifyResult.Success:
                    msg = "编辑任务已成功。";
                    break;
                case Framework.FieldVerifyResult.NameDuplicated2:
                    msg = "已存在相同名称，请输入其他“任务代号”。";
                    break;
                case Framework.FieldVerifyResult.NameDuplicated3:
                    msg = "已存在相同名称，请输入其他“任务标识”。";
                    break;
            }
            ltMessage.Text = msg;
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("taskmanage.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath + "?id=" + Request.QueryString["Id"]);
        }
    }
}