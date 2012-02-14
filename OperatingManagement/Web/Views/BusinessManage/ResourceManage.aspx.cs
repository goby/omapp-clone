#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:ResourceManage.cs
//Remark:资源管理类
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
    public partial class ResourceManage : AspNetPage
    {
        #region 属性
        /// <summary>
        /// 资源类型
        /// 地面站资源=1、通信资源=2、中心资源=3
        /// </summary>
        protected string ResourceType
        {
            get
            {
                //默认为地面站资源
                string resourceType = "1";
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
        /// <summary>
        /// 查询资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindRepeater();
            }
            catch
            { }
        }
        /// <summary>
        /// 添加资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string url = @"~/Views/BusinessManage/ResourceManage.aspx";
                string resourceType = dplResourceType.SelectedValue;
                switch (resourceType)
                {
                    //地面站资源
                    case "1":
                        url = @"~/Views/BusinessManage/GroundResourceAdd.aspx";//地面站资源添加页面
                        break;
                    //通信资源
                    case "2":
                        url = @"~/Views/BusinessManage/CommunicationResourceAdd.aspx";//通信资源添加页面
                        break;
                    //中心资源
                    case "3":
                        url = @"~/Views/BusinessManage/CenterResourceAdd.aspx";//中心资源添加页面
                        break;
                }
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 管理资源状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnManageResourceStatus_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnManageResourceStatus = (sender as LinkButton);
                if (lbtnManageResourceStatus == null)
                {
                    BindRepeater();
                    return;
                }
                string resourceCode = lbtnManageResourceStatus.CommandArgument;
                string resourceType = lbtnManageResourceStatus.CommandName;
                string url = string.Format(@"~/Views/BusinessManage/ResourceStatusManage.aspx?resourcecode={0}&resourcetype={1}", Server.UrlEncode(resourceCode), Server.UrlEncode(resourceType));

                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 编辑资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnEditResource_Click(object sender, EventArgs e)
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
                string resourceType = lbtnEdit.CommandName;
                switch (resourceType)
                {
                    //地面站资源
                    case "1":
                        url = @"~/Views/BusinessManage/GroundResourceEdit.aspx?grid=" + Server.UrlEncode(lbtnEdit.CommandArgument);//地面站资源编辑页面
                        break;
                    //通信资源
                    case "2":
                        url = @"~/Views/BusinessManage/CommunicationResourceEdit.aspx?crid=" + Server.UrlEncode(lbtnEdit.CommandArgument);//通信资源编辑页面
                        break;
                    //中心资源
                    case "3":
                        url = @"~/Views/BusinessManage/CenterResourceEdit.aspx?crid=" + Server.UrlEncode(lbtnEdit.CommandArgument);//中心资源编辑页面
                        break;
                }
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDeleteResource_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnDelete = (sender as LinkButton);
                if (lbtnDelete == null)
                {
                    BindRepeater();
                    return;
                }
                //资源ID
                int id = 0;
                int.TryParse(lbtnDelete.CommandArgument, out id);
                //资源类型
                string resourceType = lbtnDelete.CommandName;
                Framework.FieldVerifyResult result = DeleteResource(id, resourceType);
                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"发生了数据错误，无法完成请求的操作。\")", true);
                        break;
                    case Framework.FieldVerifyResult.Success:
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"删除资源成功。\")", true);
                        BindRepeater();
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"发生了数据错误，无法完成请求的操作。\")", true);
                        BindRepeater();
                        break;
                }
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_ResMan.View";
            this.ShortTitle = "资源管理";
            this.SetTitle();
        }

        #region ItemDataBound
        /// <summary>
        /// 地面站资源单条数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpGroundResourceList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lbtnManageResourceStatus = (e.Item.FindControl("lbtnManageResourceStatus") as LinkButton);
                LinkButton lbtnDeleteResource = (e.Item.FindControl("lbtnDeleteResource") as LinkButton);
                if (lbtnManageResourceStatus != null && lbtnDeleteResource != null)
                {
                    GroundResource groundResource = (e.Item.DataItem as GroundResource);
                    if (groundResource.Status == 2)
                    {
                        lbtnManageResourceStatus.Enabled = false;
                        lbtnDeleteResource.OnClientClick = string.Empty;
                        lbtnDeleteResource.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// 通信资源单条数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpCommunicationResourceList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lbtnManageResourceStatus = (e.Item.FindControl("lbtnManageResourceStatus") as LinkButton);
                LinkButton lbtnDeleteResource = (e.Item.FindControl("lbtnDeleteResource") as LinkButton);
                if (lbtnManageResourceStatus != null && lbtnDeleteResource != null)
                {
                    CommunicationResource communicationResource = (e.Item.DataItem as CommunicationResource);
                    if (communicationResource.Status == 2)
                    {
                        lbtnManageResourceStatus.Enabled = false;
                        lbtnDeleteResource.OnClientClick = string.Empty;
                        lbtnDeleteResource.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// 中心资源单条数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpCenterResourceList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lbtnManageResourceStatus = (e.Item.FindControl("lbtnManageResourceStatus") as LinkButton);
                LinkButton lbtnDeleteResource = (e.Item.FindControl("lbtnDeleteResource") as LinkButton);
                if (lbtnManageResourceStatus != null && lbtnDeleteResource != null)
                {
                    CenterResource centerResource = (e.Item.DataItem as CenterResource);
                    if (centerResource.Status == 2)
                    {
                        lbtnManageResourceStatus.Enabled = false;
                        lbtnDeleteResource.OnClientClick = string.Empty;
                        lbtnDeleteResource.Enabled = false;
                    }
                }
            }
        }
        #endregion

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
                //地面站资源
                case "1":
                    divGroundResource.Visible = true;
                    divCommunicationResource.Visible = false;
                    divCenterResource.Visible = false;
                    BindGroundResourceList();
                    break;
                //通信资源
                case "2":
                    divGroundResource.Visible = false;
                    divCommunicationResource.Visible = true;
                    divCenterResource.Visible = false;
                    BindCommunicationResource();
                    break;
                //中心资源
                case "3":
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
            cpGroundResourcePager.DataSource = groundResource.Search(resourceStatus, DateTime.Now);
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
            cpCommunicationResourcePager.DataSource = communicationResource.Search(resourceStatus, DateTime.Now);
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
            cpCenterResourcePager.DataSource = centerResource.Search(resourceStatus, DateTime.Now);
            cpCenterResourcePager.PageSize = this.SiteSetting.PageSize;
            cpCenterResourcePager.BindToControl = rpGroundResourceList;
            rpCenterResourceList.DataSource = cpCenterResourcePager.DataSourcePaged;
            rpCenterResourceList.DataBind();
        }
        /// <summary>
        /// 逻辑删除资源信息
        /// </summary>
        /// <param name="id">资源ID</param>
        /// <param name="resourceType">资源类型</param>
        /// <returns>删除结果</returns>
        private Framework.FieldVerifyResult DeleteResource(int id, string resourceType)
        {
            Framework.FieldVerifyResult result = 0;
            switch (resourceType)
            {
                //地面站资源
                case "1":
                    GroundResource groundResource = new GroundResource();
                    groundResource.Id = id;
                    groundResource = groundResource.SelectByID();
                    if (groundResource != null)
                    {
                        groundResource.Status = 2;//删除状态
                        groundResource.UpdatedTime = DateTime.Now;
                        result = groundResource.Update();
                    }
                    break;
                //通信资源
                case "2":
                    CommunicationResource communicationResource = new CommunicationResource();
                    communicationResource.Id = id;
                    communicationResource = communicationResource.SelectByID();
                    if (communicationResource != null)
                    {
                        communicationResource.Status = 2;//删除状态
                        communicationResource.UpdatedTime = DateTime.Now;
                        result = communicationResource.Update();
                    }
                    break;
                //中心资源
                case "3":
                    CenterResource centerResource = new CenterResource();
                    centerResource.Id = id;
                    centerResource = centerResource.SelectByID();
                    if (centerResource != null)
                    {
                        centerResource.Status = 2;//删除状态
                        centerResource.UpdatedTime = DateTime.Now;
                        result = centerResource.Update();
                    }
                    break;
            }
            return result;
        }
        /// <summary>
        /// 根据地面资源功能类型值获得文本
        /// </summary>
        /// <param name="valueString">功能类型字符串</param>
        /// <returns>地面资源功能类型文本</returns>
        protected string GetGroundResourceFunctionType(string valueString)
        {
            string textString = string.Empty;
            if (!string.IsNullOrEmpty(valueString))
            {
                string[] valueArray = valueString.Split(new char[] { ',', '，', ';', '；' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string value in valueArray)
                {
                    textString += SystemParameters.GetSystemParameterText(SystemParametersType.GroundResourceFunctionType, value) + ",";
                }
            }
            return textString.Trim(new char[] { ',' });
        }

        #endregion

    }
}