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

using OperatingManagement.Framework.Core;
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
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("编辑地面站资源页面初始化出现异常，异常原因", ex));
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

                if (string.IsNullOrEmpty(txtLongitude.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "经度坐标值不能为空";
                    return;
                }
                double longitudeValue = 0.0;
                if (!double.TryParse(txtLongitude.Text.Trim(), out longitudeValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "经度坐标值格式错误";
                    return;
                }
                if (string.IsNullOrEmpty(txtLatitude.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "纬度坐标值不能为空";
                    return;
                }
                double latitudeValue = 0.0;
                if (!double.TryParse(txtLatitude.Text.Trim(), out latitudeValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "纬度坐标值格式错误";
                    return;
                }

                if (string.IsNullOrEmpty(txtGaoCheng.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "高程坐标值不能为空";
                    return;
                }
                double gaoCheng = 0.0;
                if (!double.TryParse(txtGaoCheng.Text.Trim(), out gaoCheng))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "高程坐标值格式错误";
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
                groundResource.Coordinate = longitudeValue.ToString() + "," + latitudeValue.ToString() + "," + gaoCheng.ToString();
                groundResource.FunctionType = functionType;
                //groundResource.Status = 1;
                //groundResource.CreatedTime = DateTime.Now;
                groundResource.UpdatedTime = DateTime.Now;
                groundResource.UpdatedUserID = LoginUserInfo.Id;

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
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("编辑地面站资源页面btnSubmit_Click方法出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 重置当前控件的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                BindControls();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("编辑地面站资源页面btnReset_Click方法出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                string url = @"~/Views/BusinessManage/ResourceManage.aspx?resourcetype=" + Server.UrlEncode("1");
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("编辑地面站资源页面btnReturn_Click方法出现异常，异常原因", ex));
            }
        }

        public override void OnPageLoaded()
        {
            this.ShortTitle = "编辑地面站资源";
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
            //dplOwner.Items.Insert(0, new ListItem("请选择", ""));

            dplCoordinate.Items.Clear();
            dplCoordinate.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.GroundResourceCoordinate);
            dplCoordinate.DataTextField = "key";
            dplCoordinate.DataValueField = "value";
            dplCoordinate.DataBind();
            //dplCoordinate.Items.Insert(0, new ListItem("请选择", ""));

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
                //dplCoordinate.SelectedValue = groundResource.Coordinate;
                lblCreatedTime.Text = groundResource.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                lblUpdatedTime.Text = groundResource.UpdatedTime == DateTime.MinValue ? groundResource.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss") : groundResource.UpdatedTime.ToString("yyyy-MM-dd HH:mm:ss");

                string[] coordinateInfo = groundResource.Coordinate.Split(new char[] { ',', '，', ':','：' }, StringSplitOptions.RemoveEmptyEntries);
                if (coordinateInfo != null && coordinateInfo.Length == 3)
                {
                    //dplCoordinate.SelectedValue = coordinateInfo[0];
                    txtLongitude.Text = coordinateInfo[0];
                    txtLatitude.Text = coordinateInfo[1];
                    txtGaoCheng.Text = coordinateInfo[2];
                }

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