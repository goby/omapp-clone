using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OperatingManagement.DataAccessLayer.BusinessManage;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class CenterResourceAdd : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                trMessage.Visible = false;
                lblMessage.Text = string.Empty;
                if (!IsPostBack)
                {
                    BindDataSource();
                }
            }
            catch
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = string.Empty;
                if (string.IsNullOrEmpty(txtEquipmentCode.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "设备编号不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtSupportTask.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "支持的任务不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtDataProcess.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "最大数据处理量不能为空";
                    return;
                }

                Framework.FieldVerifyResult result;
                CenterResource centerResource = new CenterResource();
                centerResource.EquipmentCode = txtEquipmentCode.Text.Trim();
                centerResource.EquipmentType = dplEquipmentType.SelectedValue;
                centerResource.SupportTask = txtSupportTask.Text.Trim();
                centerResource.DataProcess = txtDataProcess.Text.Trim();
                centerResource.Status = 1;//正常
                centerResource.CreatedTime = DateTime.Now;
                centerResource.UpdatedTime = DateTime.Now;

                result = centerResource.Add();
                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "添加中心资源成功。";
                        ResetControls();
                        break;
                    default:
                        msg = "发生未知错误，操作失败。";
                        break;
                }
                trMessage.Visible = true;
                lblMessage.Text = msg;
            }
            catch
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                string url = @"~/Views/BusinessManage/ResourceManage.aspx?resourcetype=" + Server.UrlEncode("03");
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }

        #region Method
        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        private void BindDataSource()
        {
            dplEquipmentType.Items.Clear();
            dplEquipmentType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.CenterResourceEquipmentType);
            dplEquipmentType.DataTextField = "key";
            dplEquipmentType.DataValueField = "value";
            dplEquipmentType.DataBind();
            dplEquipmentType.Items.Insert(0, new ListItem("请选择", ""));
        }
        /// <summary>
        /// 添加完成后将控件设置为初始状态
        /// </summary>
        private void ResetControls()
        {
            dplEquipmentType.SelectedIndex = 0;

            txtEquipmentCode.Text = string.Empty;
            txtSupportTask.Text = string.Empty;
            txtDataProcess.Text = string.Empty;
        }

        #endregion
    }
}