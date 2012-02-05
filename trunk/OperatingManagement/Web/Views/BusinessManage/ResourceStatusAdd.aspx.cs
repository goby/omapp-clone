﻿#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:ResourceStatusAdd.cs
//Remark:资源状态添加类
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
    public partial class ResourceStatusAdd : AspNetPage
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
                    SetControlsVisible();
                }
            }
            catch
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
            }
        }
        /// <summary>
        /// 提交添加资源状态记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = string.Empty;
                //资源管理资源类型列表：地面站资源=1、通信资源=2、中心资源=3
                int resourceType = 0;
                int.TryParse(dplResourceType.SelectedValue, out resourceType);
                if (string.IsNullOrEmpty(txtResourceCode.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "资源编号不能为空";
                    return;
                }

                int resourceID = GetResourceID(resourceType, txtResourceCode.Text.Trim());
                if (resourceID < 1)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "资源不存在，请确认输入的资源编号是否正确";
                    return;
                }

                if (trHealthStatusFunctionType.Visible && string.IsNullOrEmpty(dplFunctionType.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择功能类型";
                    return;
                }

                if (trHealthStatus.Visible && string.IsNullOrEmpty(dplHealthStatus.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择健康状态";
                    return;
                }

                if (trUseStatusUsedType.Visible && string.IsNullOrEmpty(dplUsedType.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择占用类型";
                    return;
                }

                DateTime beginTime = DateTime.Now;
                DateTime endTime = DateTime.Now;

                if (!DateTime.TryParse(txtBeginTime.Text.Trim(), out beginTime))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "起始时间格式错误";
                    return;
                }

                if (!DateTime.TryParse(txtEndTime.Text.Trim(), out endTime))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "结束时间格式错误";
                    return;
                }
                endTime = endTime.AddSeconds(86399.9);//23:59:59
                if (beginTime > endTime)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "起始时间应小于结束时间";
                    return;
                }

                if (trUseStatusUsedBy.Visible && string.IsNullOrEmpty(txtUsedBy.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "服务对象不能为空";
                    return;
                }

                if (trUseStatusUsedCategory.Visible && string.IsNullOrEmpty(txtUsedCategory.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "服务种类不能为空";
                    return;
                }

                if (trUseStatusUsedFor.Visible && string.IsNullOrEmpty(txtUsedFor.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "占用原因不能为空";
                    return;
                }

                if (trUseStatusCanBeUsed.Visible && string.IsNullOrEmpty(dplCanBeUsed.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择是否可执行任务";
                    return;
                }
                //状态类型列表：健康状态=1、占用状态=2
                if (dplStatusType.SelectedValue == "1")
                {
                    Framework.FieldVerifyResult result;
                    HealthStatus healthStatus = new HealthStatus();
                    healthStatus.ResourceID = resourceID;
                    healthStatus.ResourceType = resourceType;
                    healthStatus.FunctionType = trHealthStatusFunctionType.Visible ? dplFunctionType.SelectedValue : string.Empty;
                    healthStatus.Status = trHealthStatusFunctionType.Visible ? Convert.ToInt32(dplHealthStatus.SelectedValue) : 0;
                    healthStatus.BeginTime = beginTime;
                    healthStatus.EndTime = endTime;
                    healthStatus.CreatedTime = DateTime.Now;
                    healthStatus.UpdatedTime = DateTime.Now;
                    result = healthStatus.Add();

                    switch (result)
                    {
                        case Framework.FieldVerifyResult.Error:
                            msg = "发生了数据错误，无法完成请求的操作。";
                            break;
                        case Framework.FieldVerifyResult.Success:
                            msg = "添加健康状态成功。";
                            ResetControls();
                            break;
                        default:
                            msg = "发生未知错误，操作失败。";
                            break;
                    }
                }
                //状态类型列表：健康状态=1、占用状态=2
                else if (dplStatusType.SelectedValue == "2")
                {
                    Framework.FieldVerifyResult result;
                    UseStatus useStatus = new UseStatus();
                    useStatus.ResourceID = resourceID;
                    useStatus.ResourceType = resourceType;
                    useStatus.UsedType = trUseStatusUsedType.Visible ? Convert.ToInt32(dplUsedType.SelectedValue) : 0;
                    useStatus.BeginTime = beginTime;
                    useStatus.EndTime = endTime;
                    useStatus.UsedBy = trUseStatusUsedBy.Visible ? txtUsedBy.Text : string.Empty;
                    useStatus.UsedCategory = trUseStatusUsedCategory.Visible ? txtUsedCategory.Text : string.Empty;
                    useStatus.UsedFor = trUseStatusUsedFor.Visible ? txtUsedCategory.Text : string.Empty;
                    useStatus.CanBeUsed = trUseStatusCanBeUsed.Visible ? Convert.ToInt32(dplCanBeUsed.SelectedValue) : 0;
                    useStatus.CreatedTime = DateTime.Now;
                    useStatus.UpdatedTime = DateTime.Now;
                    result = useStatus.Add();

                    switch (result)
                    {
                        case Framework.FieldVerifyResult.Error:
                            msg = "发生了数据错误，无法完成请求的操作。";
                            break;
                        case Framework.FieldVerifyResult.Success:
                            msg = "添加占用状态成功。";
                            ResetControls();
                            break;
                        default:
                            msg = "发生未知错误，操作失败。";
                            break;
                    }
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
                string url = @"~/Views/BusinessManage/ResourceStatusManage.aspx";
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 当状态类型发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplStatusType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //状态类型列表：健康状态=1、占用状态=2
            if (dplStatusType.SelectedValue == "1")
            {
                //资源管理资源类型列表：地面站资源=1、通信资源=2、中心资源=3
                dplResourceType.Items.Clear();
                dplResourceType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.ResourceType);
                dplResourceType.DataTextField = "key";
                dplResourceType.DataValueField = "value";
                dplResourceType.DataBind();

                dplFunctionType.SelectedIndex = 0;
            }
            else if (dplStatusType.SelectedValue == "2")
            {
                //通信资源没有占用状态
                dplResourceType.Items.Remove(dplResourceType.Items.FindByValue("2"));
                //占用状态占用类型列表：任务占用=1、维护占用=2、其他占用=3
                dplUsedType.SelectedValue = "1";
            }
            SetControlsVisible();
        }
        /// <summary>
        /// 当资源类型发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplResourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dplFunctionType.SelectedIndex = 0;
            dplUsedType.SelectedIndex = 0;

            SetControlsVisible();
        }
        /// <summary>
        /// 当占用类型发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplUsedType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsVisible();
        }

        #region Method
        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        private void BindDataSource()
        {
            //状态类型列表：健康状态=1、占用状态=2
            dplStatusType.Items.Clear();
            dplStatusType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.StatusType);
            dplStatusType.DataTextField = "key";
            dplStatusType.DataValueField = "value";
            dplStatusType.DataBind();

            //资源管理资源类型列表：地面站资源=1、通信资源=2、中心资源=3
            dplResourceType.Items.Clear();
            dplResourceType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.ResourceType);
            dplResourceType.DataTextField = "key";
            dplResourceType.DataValueField = "value";
            dplResourceType.DataBind();

            //健康状态功能类型列表：数传数据接收、遥测数据接收、遥控操作
            dplFunctionType.Items.Clear();
            dplFunctionType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.HealthStatusFunctionType);
            dplFunctionType.DataTextField = "key";
            dplFunctionType.DataValueField = "value";
            dplFunctionType.DataBind();
            dplFunctionType.Items.Insert(0, new ListItem("请选择", ""));

            //健康状态列表：正常=1、异常=2
            dplHealthStatus.Items.Clear();
            dplHealthStatus.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.HealthStatus);
            dplHealthStatus.DataTextField = "key";
            dplHealthStatus.DataValueField = "value";
            dplHealthStatus.DataBind();
            dplHealthStatus.Items.Insert(0, new ListItem("请选择", ""));
            dplHealthStatus.SelectedValue = "2";
            dplHealthStatus.Enabled = false;

            //占用状态占用类型列表：任务占用=1、维护占用=2、其他占用=3
            dplUsedType.Items.Clear();
            dplUsedType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.UseStatusUsedType);
            dplUsedType.DataTextField = "key";
            dplUsedType.DataValueField = "value";
            dplUsedType.DataBind();
            //dplUsedType.Items.Insert(0, new ListItem("请选择", ""));

            //占用状态是否可执行任务列表：是=1、否=2
            dplCanBeUsed.Items.Clear();
            dplCanBeUsed.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.UseStatusCanBeUsed);
            dplCanBeUsed.DataTextField = "key";
            dplCanBeUsed.DataValueField = "value";
            dplCanBeUsed.DataBind();
            dplCanBeUsed.Items.Insert(0, new ListItem("请选择", ""));
        }
        /// <summary>
        /// 设置控件是否可见
        /// </summary>
        private void SetControlsVisible()
        {
            //状态类型列表：健康状态=1、占用状态=2
            if (dplStatusType.SelectedValue == "1")
            {
                //与健康状态相关控件可见
                trHealthStatusFunctionType.Visible = true;
                trHealthStatus.Visible = true;
                //与占用状态相关控件不可见
                trUseStatusUsedType.Visible = false;
                trUseStatusUsedBy.Visible = false;
                trUseStatusUsedCategory.Visible = false;
                trUseStatusUsedFor.Visible = false;
                trUseStatusCanBeUsed.Visible = false;

                //资源管理资源类型列表：地面站资源=1、通信资源=2、中心资源=3
                if (dplResourceType.SelectedValue != "1")
                {
                    //只有地面站资源有健康状态功能类型列表
                    trHealthStatusFunctionType.Visible = false;
                }
            }
            else if (dplStatusType.SelectedValue == "2")
            {
                //与健康状态相关控件不可见
                trHealthStatusFunctionType.Visible = false;
                trHealthStatus.Visible = false;
                //与占用状态相关控件可见
                trUseStatusUsedType.Visible = true;
                trUseStatusUsedBy.Visible = true;
                trUseStatusUsedCategory.Visible = true;
                trUseStatusUsedFor.Visible = true;
                trUseStatusCanBeUsed.Visible = true;

                //占用状态占用类型列表：任务占用=1、维护占用=2、其他占用=3
                if (dplUsedType.SelectedValue == "1")
                {
                    trUseStatusUsedFor.Visible = false;
                    trUseStatusCanBeUsed.Visible = false;
                }
                else if (dplUsedType.SelectedValue == "2")
                {
                    trUseStatusUsedBy.Visible = false;
                    trUseStatusUsedCategory.Visible = false;
                    trUseStatusUsedFor.Visible = false;
                    trUseStatusCanBeUsed.Visible = false;
                }
                else if (dplUsedType.SelectedValue == "3")
                {
                    trUseStatusUsedFor.Visible = false;
                    trUseStatusCanBeUsed.Visible = false;
                }
            }
        }
        /// <summary>
        /// 添加完成后将控件设置为初始状态
        /// </summary>
        private void ResetControls()
        {
            txtResourceCode.Text = string.Empty;
            dplFunctionType.SelectedIndex = 0;
            dplHealthStatus.SelectedValue = "2";
            dplUsedType.SelectedIndex = 0;
            txtBeginTime.Text = string.Empty;
            txtEndTime.Text = string.Empty;
            txtUsedBy.Text = string.Empty;
            txtUsedCategory.Text = string.Empty;
            txtUsedFor.Text = string.Empty;
            dplCanBeUsed.Text = string.Empty;

            SetControlsVisible();
        }
        /// <summary>
        /// 获得资源ID
        /// </summary>
        /// <param name="resourceType">资源类型</param>
        /// <param name="resourceCode">资源编号</param>
        /// <returns>资源ID</returns>
        private int GetResourceID(int resourceType, string resourceCode)
        {
            int resourceID = 0;
            switch (resourceType)
            {
                //地面站资源
                case 1:
                    GroundResource groundResource = new GroundResource();
                    groundResource.GRCode = resourceCode;
                    groundResource = groundResource.SelectByCode();
                    if (groundResource != null && groundResource.Id > 0)
                        resourceID = groundResource.Id;
                    break;
                //通信资源
                case 2:
                    CommunicationResource communicationResource = new CommunicationResource();
                    communicationResource.RouteCode = resourceCode;
                    communicationResource = communicationResource.SelectByCode();
                    if (communicationResource != null && communicationResource.Id > 0)
                        resourceID = communicationResource.Id;
                    break;
                //中心资源
                case 3:
                    CenterResource centerResource = new CenterResource();
                    centerResource.EquipmentCode = resourceCode;
                    centerResource = centerResource.SelectByCode();
                    if (centerResource != null && centerResource.Id > 0)
                        resourceID = centerResource.Id;
                    break;
            }
            return resourceID;
        }
        #endregion

    }
}