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
        /// </summary>
        protected string ResourceType
        {
            get
            {
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
                    case "1"://地面站资源
                        url = @"~/Views/BusinessManage/GroundResourceAdd.aspx";
                        break;
                    case "2"://通信资源
                        url = @"~/Views/BusinessManage/CommunicationResourceAdd.aspx";
                        break;
                    case "3"://中心资源
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
                    case "1"://地面站资源
                        url = @"~/Views/BusinessManage/GroundResourceEdit.aspx?grid=" + Server.UrlEncode(lbtnEdit.CommandArgument);
                        break;
                    case "2"://通信资源
                        url = @"~/Views/BusinessManage/CommunicationResourceEdit.aspx?crid=" + Server.UrlEncode(lbtnEdit.CommandArgument);
                        break;
                    case "3"://中心资源
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
                int id = 0;
                int.TryParse(lbtnDelete.CommandArgument, out id);
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
                case "1"://地面站资源
                    divGroundResource.Visible = true;
                    divCommunicationResource.Visible = false;
                    divCenterResource.Visible = false;
                    BindGroundResourceList();
                    break;
                case "2"://通信资源
                    divGroundResource.Visible = false;
                    divCommunicationResource.Visible = true;
                    divCenterResource.Visible = false;
                    BindCommunicationResource();
                    break;
                case "3"://中心资源
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
        /// <param name="id"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        private Framework.FieldVerifyResult DeleteResource(int id, string resourceType)
        {
            Framework.FieldVerifyResult result = 0;
            switch (resourceType)
            {
                case "1"://地面站资源
                    GroundResource groundResource = new GroundResource();
                    groundResource.Id = id;
                    groundResource = groundResource.SelectByID();
                    if (groundResource != null)
                    {
                        groundResource.Status = 2;
                        groundResource.UpdatedTime = DateTime.Now;
                        result = groundResource.Update();
                    }
                    break;
                case "2"://通信资源
                    CommunicationResource communicationResource = new CommunicationResource();
                    communicationResource.Id = id;
                    communicationResource = communicationResource.SelectByID();
                    if (communicationResource != null)
                    {
                        communicationResource.Status = 2;
                        communicationResource.UpdatedTime = DateTime.Now;
                        result = communicationResource.Update();
                    }
                    break;
                case "3"://中心资源
                    CenterResource centerResource = new CenterResource();
                    centerResource.Id = id;
                    centerResource = centerResource.SelectByID();
                    if (centerResource != null)
                    {
                        centerResource.Status = 2;
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
        /// <param name="valueString"></param>
        /// <returns></returns>
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