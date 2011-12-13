﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OperatingManagement.DataAccessLayer.BusinessManage;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class ResourceManage : AspNetPage
    {
        #region 属性
        /// <summary>
        /// 资源类型
        /// </summary>
        protected string ResourceType
        {
            get
            {
                string resourceType = "01";
                if (Request.QueryString["resourcetype"] != null)
                {
                    resourceType = Request.QueryString["resourcetype"];
                }
                return resourceType;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDataSource();
                    BindRepeater();
                }
            }
            catch
            {
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindRepeater();
            }
            catch
            { }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string url = @"~/Views/BusinessManage/ResourceManage.aspx";
                string resourceType = dplResourceType.SelectedValue;
                switch (resourceType)
                {
                    case "01"://地面站资源
                        url = @"~/Views/BusinessManage/GroundResourceAdd.aspx";
                        break;
                    case "02"://通信资源
                        url = @"~/Views/BusinessManage/CommunicationResourceAdd.aspx";
                        break;
                    case "03"://中心资源
                        url = @"~/Views/BusinessManage/CenterResourceAdd.aspx";
                        break;
                }
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnEdit = (sender as LinkButton);
                if (lbtnEdit == null)
                {
                    BindRepeater();
                    return;
                }
                
                string url = @"~/Views/BusinessManage/ResourceManage.aspx";
                string resourceType = dplResourceType.SelectedValue;
                switch (resourceType)
                {
                    case "01"://地面站资源
                        url = @"~/Views/BusinessManage/GroundResourceEdit.aspx?grid=" + Server.UrlEncode(lbtnEdit.CommandArgument);
                        break;
                    case "02"://通信资源
                        url = @"~/Views/BusinessManage/CommunicationResourceEdit.aspx?crid=" + Server.UrlEncode(lbtnEdit.CommandArgument);
                        break;
                    case "03"://中心资源
                        url = @"~/Views/BusinessManage/CenterResourceEdit.aspx?crid=" + Server.UrlEncode(lbtnEdit.CommandArgument);
                        break;
                }
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
            dplResourceType.Items.Clear();
            dplResourceType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.ResourceType);
            dplResourceType.DataTextField = "key";
            dplResourceType.DataValueField = "value";
            dplResourceType.DataBind();
            dplResourceType.SelectedValue = ResourceType;

            dplResourceStatus.Items.Clear();
            dplResourceStatus.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.ResourceStatus);
            dplResourceStatus.DataTextField = "key";
            dplResourceStatus.DataValueField = "value";
            dplResourceStatus.DataBind();
            dplResourceStatus.Items.Insert(0, new ListItem("全部", ""));
        }
        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void BindRepeater()
        {
            string resourceType = dplResourceType.SelectedValue;
            switch (resourceType)
            {
                case "01"://地面站资源
                    divGroundResource.Visible = true;
                    divCommunicationResource.Visible = false;
                    divCenterResource.Visible = false;
                    BindGroundResourceList();
                    break;
                case "02"://通信资源
                    divGroundResource.Visible = false;
                    divCommunicationResource.Visible = true;
                    divCenterResource.Visible = false;
                    BindCommunicationResource();
                    break;
                case "03"://中心资源
                    divGroundResource.Visible = false;
                    divCommunicationResource.Visible = false;
                    divCenterResource.Visible = true;
                    BindCenterResource();
                    break;
                default:
                    divGroundResource.Visible = false;
                    divCommunicationResource.Visible = false;
                    divCenterResource.Visible = false;
                    break;
            }
        }
        /// <summary>
        /// 绑定地面站资源
        /// </summary>
        private void BindGroundResourceList()
        {
            string resourceStatus = dplResourceStatus.SelectedValue;

            GroundResource groundResource = new GroundResource();
            cpGroundResourcePager.DataSource = groundResource.SelectAll();
            cpGroundResourcePager.PageSize = this.SiteSetting.PageSize;
            cpGroundResourcePager.BindToControl = rpGroundResourceList;
            rpGroundResourceList.DataSource = cpGroundResourcePager.DataSourcePaged;
            rpGroundResourceList.DataBind();
        }
        /// <summary>
        /// 绑定通信资源
        /// </summary>
        private void BindCommunicationResource()
        {
            string resourceStatus = dplResourceStatus.SelectedValue;

            CommunicationResource communicationResource = new CommunicationResource();
            cpCommunicationResourcePager.DataSource = communicationResource.SelectAll();
            cpCommunicationResourcePager.PageSize = this.SiteSetting.PageSize;
            cpCommunicationResourcePager.BindToControl = rpCommunicationResourceList;
            rpCommunicationResourceList.DataSource = cpCommunicationResourcePager.DataSourcePaged;
            rpCommunicationResourceList.DataBind();
        }
        /// <summary>
        /// 绑定中心资源
        /// </summary>
        private void BindCenterResource()
        {
            string resourceStatus = dplResourceStatus.SelectedValue;

            CenterResource centerResource = new CenterResource();
            cpCenterResourcePager.DataSource = centerResource.SelectAll();
            cpCenterResourcePager.PageSize = this.SiteSetting.PageSize;
            cpCenterResourcePager.BindToControl = rpGroundResourceList;
            rpCenterResourceList.DataSource = cpCenterResourcePager.DataSourcePaged;
            rpCenterResourceList.DataBind();
        }

        #endregion
    }
}