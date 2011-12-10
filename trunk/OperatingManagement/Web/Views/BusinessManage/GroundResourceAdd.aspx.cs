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
    public partial class GroundResourceAdd : AspNetPage
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
                if (string.IsNullOrEmpty(txtGRName.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "地面站名称不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtGRCode.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "地面站编号不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtEquipmentName.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "设备名称不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtEquipmentCode.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "设备编号不能为空";
                    return;
                }

                if (cblFunctionType.SelectedItem == null)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择功能类型";
                    return;
                }
                string functionType = string.Empty;
                foreach (ListItem item in cblFunctionType.Items)
                {
                    if (item.Selected)
                    {
                        functionType += string.IsNullOrEmpty(functionType) ? item.Value : ";" + item.Value;
                    }
                }

                Framework.FieldVerifyResult result;
                GroundResource groundResource = new GroundResource();
                groundResource.GRName = txtGRName.Text.Trim();
                groundResource.GRCode = txtGRCode.Text.Trim();
                groundResource.EquipmentName = txtEquipmentName.Text.Trim();
                groundResource.EquipmentCode = txtEquipmentCode.Text.Trim();
                groundResource.Owner = dplOwner.SelectedValue;
                groundResource.Coordinate = dplCoordinate.SelectedValue;
                groundResource.FunctionType = functionType;
                groundResource.Status = 0;
                groundResource.CreatedTime = DateTime.Now;
                groundResource.UpdatedTime = DateTime.Now;

                result = groundResource.Add();
                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "添加地面站资源成功。";
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
                string url = @"~/Views/BusinessManage/ResourceManage.aspx";
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
            dplOwner.Items.Clear();
            dplOwner.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.GroundResourceOwner);
            dplOwner.DataTextField = "key";
            dplOwner.DataValueField = "value";
            dplOwner.DataBind();
            dplOwner.Items.Insert(0, new ListItem("请选择", ""));

            dplCoordinate.Items.Clear();
            dplCoordinate.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.GroundResourceCoordinate);
            dplCoordinate.DataTextField = "key";
            dplCoordinate.DataValueField = "value";
            dplCoordinate.DataBind();
            dplCoordinate.Items.Insert(0, new ListItem("请选择", ""));

            cblFunctionType.Items.Clear();
            cblFunctionType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.GroundResourceFunctionType);
            cblFunctionType.DataTextField = "key";
            cblFunctionType.DataValueField = "value";
            cblFunctionType.DataBind();
        }
        /// <summary>
        /// 添加完成后将控件设置为初始状态
        /// </summary>
        private void ResetControls()
        {
            dplOwner.SelectedIndex = 0;
            dplCoordinate.SelectedIndex = 0;

            txtGRName.Text = string.Empty;
            txtGRCode.Text = string.Empty;
            txtEquipmentName.Text = string.Empty;
            txtEquipmentCode.Text = string.Empty;

            foreach (ListItem item in cblFunctionType.Items)
            {
                item.Selected = false;
            }
        }

        #endregion
    }
}