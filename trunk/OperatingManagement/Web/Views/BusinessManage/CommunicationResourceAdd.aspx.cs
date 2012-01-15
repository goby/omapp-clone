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
    public partial class CommunicationResourceAdd : AspNetPage
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
                if (string.IsNullOrEmpty(txtRouteName.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "线路名称不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtRouteCode.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "线路编码不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(dplDirection.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "方向不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtBandWidth.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "带宽不能为空";
                    return;
                }

                Framework.FieldVerifyResult result;
                CommunicationResource communicationResource = new CommunicationResource();
                communicationResource.RouteName = txtRouteName.Text.Trim();
                communicationResource.RouteCode = txtRouteCode.Text.Trim();
                communicationResource.Direction = dplDirection.SelectedValue;
                communicationResource.BandWidth = txtBandWidth.Text.Trim();
                communicationResource.Status = 1;//正常
                communicationResource.CreatedTime = DateTime.Now;
                communicationResource.UpdatedTime = DateTime.Now;

                if (communicationResource.HaveActiveRouteCode())
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "线路编码已经存在";
                    return;
                }

                result = communicationResource.Add();
                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "添加通信资源成功。";
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
                string url = @"~/Views/BusinessManage/ResourceManage.aspx?resourcetype=" + Server.UrlEncode("2");
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
            dplDirection.Items.Clear();
            dplDirection.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.CommunicationResourceDirection);
            dplDirection.DataTextField = "key";
            dplDirection.DataValueField = "value";
            dplDirection.DataBind();
            dplDirection.Items.Insert(0, new ListItem("请选择", ""));
        }
        /// <summary>
        /// 添加完成后将控件设置为初始状态
        /// </summary>
        private void ResetControls()
        {
            dplDirection.SelectedIndex = 0;

            txtRouteName.Text = string.Empty;
            txtRouteCode.Text = string.Empty;
            txtBandWidth.Text = string.Empty;
        }

        #endregion
    }
}