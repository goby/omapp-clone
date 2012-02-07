#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:GroundResourceEdit.cs
//Remark:地面站资源编辑类
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20111015    Create     
//------------------------------------------------------
#endregion
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
    public partial class GroundResourceEdit : AspNetPage
    {
        #region 属性
        /// <summary>
        /// 地面站资源ID
        /// </summary>
        protected int GRID
        {
            get
            {
                int grID = 0;
                if (Request.QueryString["grid"] != null)
                {
                    int.TryParse(Request.QueryString["grid"], out grID);
                }
                return grID;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                trMessage.Visible = false;
                lblMessage.Text = string.Empty;
                if (!IsPostBack)
                {
                    BindDataSource();
                    BindControls();
                }
            }
            catch
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
            }
        }
        /// <summary>
        /// 提交更新地面站资源记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if (string.IsNullOrEmpty(dplOwner.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "管理单位不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(dplCoordinate.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "站址坐标不能为空";
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
                groundResource.Id = GRID;
                groundResource = groundResource.SelectByID();
                if (groundResource == null)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "修改的地面站资源不存在";
                    return;
                }
                groundResource.GRName = txtGRName.Text.Trim();
                groundResource.GRCode = txtGRCode.Text.Trim();
                groundResource.EquipmentName = txtEquipmentName.Text.Trim();
                groundResource.EquipmentCode = txtEquipmentCode.Text.Trim();
                groundResource.Owner = dplOwner.SelectedValue;
                groundResource.Coordinate = dplCoordinate.SelectedValue;
                groundResource.FunctionType = functionType;
                //groundResource.Status = 1;
                //groundResource.CreatedTime = DateTime.Now;
                groundResource.UpdatedTime = DateTime.Now;

                if (groundResource.HaveActiveGRCode())
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "地面站编号已经存在";
                    return;
                }

                result = groundResource.Update();
                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "修改地面站资源成功。";
                        BindControls();
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
                string url = @"~/Views/BusinessManage/ResourceManage.aspx?resourcetype=" + Server.UrlEncode("1");
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }

        public override void OnPageLoaded()
        {
            this.ShortTitle = "地面站资源编辑";
            this.SetTitle();
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
        /// 绑定控件值
        /// </summary>
        private void BindControls()
        {
            GroundResource groundResource = new GroundResource();
            groundResource.Id = GRID;
            groundResource = groundResource.SelectByID();
            if (groundResource != null)
            {
                txtGRName.Text = groundResource.GRName;
                txtGRCode.Text = groundResource.GRCode;
                txtEquipmentName.Text = groundResource.EquipmentName;
                txtEquipmentCode.Text = groundResource.EquipmentCode;
                dplOwner.SelectedValue = groundResource.Owner;
                dplCoordinate.SelectedValue = groundResource.Coordinate;
                lblCreatedTime.Text = groundResource.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                lblUpdatedTime.Text = groundResource.UpdatedTime.ToString("yyyy-MM-dd HH:mm:ss");

                string[] functionTypeArray = groundResource.FunctionType.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (ListItem item in cblFunctionType.Items)
                {
                    item.Selected = functionTypeArray.Contains(item.Value);
                }
            }
        }

        #endregion
    }
}