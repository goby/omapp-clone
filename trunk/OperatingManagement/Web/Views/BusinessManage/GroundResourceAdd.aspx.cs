﻿#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:GroundResourceAdd.cs
//Remark:地面站资源添加类
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
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("新增地面站资源页面初始化出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 提交添加地面站资源记录
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
                groundResource.Coordinate = dplCoordinate.SelectedValue + "：" + longitudeValue.ToString() + "，" + latitudeValue.ToString() + "，" + txtGaoCheng.Text.Trim();
                groundResource.FunctionType = functionType;
                groundResource.Status = 1;//正常
                groundResource.CreatedTime = DateTime.Now;
                groundResource.CreatedUserID = LoginUserInfo.Id;

                if (groundResource.HaveActiveGRCode())
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "地面站编号已经存在";
                    return;
                }

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
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("新增地面站资源页面btnSubmit_Click方法出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 清除当前控件的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControls();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("新增地面站资源页面btnReset_Click方法出现异常，异常原因", ex));
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
                throw (new AspNetException("新增地面站资源页面btnReturn_Click方法出现异常，异常原因", ex));
            }
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_GRes.Add";
            this.ShortTitle = "新增地面站资源";
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