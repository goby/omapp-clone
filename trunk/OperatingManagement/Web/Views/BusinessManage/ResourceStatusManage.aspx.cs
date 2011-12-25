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
    public partial class ResourceStatusManage : AspNetPage
    {
        #region 属性
        /// <summary>
        /// 资源类型
        /// </summary>
        protected int ResourceType
        {
            get
            {
                int resourceType = 1;
                if (Request.QueryString["resourcetype"] != null)
                {
                    int.TryParse(Request.QueryString["resourcetype"], out resourceType);
                }
                return resourceType;
            }
        }
        /// <summary>
        /// 资源编号
        /// </summary>
        protected string ResourceCode
        {
            get
            {
                string resourceCode = string.Empty;
                if (Request.QueryString["resourcecode"] != null)
                {
                    resourceCode = Request.QueryString["resourcecode"];
                }
                return resourceCode;
            }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        protected DateTime BeginTime
        {
            get
            {
                DateTime beginTime = DateTime.Now;
                if (Request.QueryString["begintime"] != null)
                {
                    DateTime.TryParse(Request.QueryString["begintime"], out beginTime);
                }
                return beginTime;
            }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        protected DateTime EndTime
        {
            get
            {
                DateTime endTime = DateTime.Now.AddDays(6);
                if (Request.QueryString["endtime"] != null)
                {
                    DateTime.TryParse(Request.QueryString["endtime"], out endTime);
                }
                return endTime;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataSource();
                //判断是否从资源管理页面跳转过来
                if (!string.IsNullOrEmpty(ResourceCode))
                {
                    BindResourceStatusList();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindResourceStatusList();
            }
            catch
            { }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string url = @"~/Views/BusinessManage/ResourceStatusAdd.aspx";
                Response.Redirect(url);
            }
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
            dplResourceType.SelectedValue = ResourceType.ToString();

            txtResourceCode.Text = ResourceCode;

            txtBeginTime.Text = BeginTime.ToString("yyyy-MM-dd");
            txtEndTime.Text = EndTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 绑定资源状态列表
        /// </summary>
        private void BindResourceStatusList()
        {
            int resourceType = 1;
            int.TryParse(dplResourceType.SelectedValue, out resourceType);
            if (string.IsNullOrEmpty(txtResourceCode.Text.Trim()))
            {
                //todo 资源编号不能为空
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"资源编号不能为空。\")", true);
                return;
            }
            int resourceID = GetResourceID(resourceType, txtResourceCode.Text.Trim());
            if (resourceID < 1)
            {
                //todo 资源不存在
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"资源不存在，请确认输入的资源编号是否正确。\")", true);
                return;
            }
            if (string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
            {
                //todo 起始时间不能为空
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"起始时间不能为空。\")", true);
                return;
            }
            if (string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                //todo 结束时间不能为空
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"结束时间不能为空。\")", true);
                return;
            }
            DateTime beginTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddDays(7);
            if (!DateTime.TryParse(txtBeginTime.Text, out beginTime))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"起始时间格式错误，请正确输入时间（yyyy-MM-dd）。\")", true);
                return;
            }
            if (!DateTime.TryParse(txtEndTime.Text, out endTime))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"结束时间格式错误，请正确输入时间（yyyy-MM-dd）。\")", true);
                return;
            }
            endTime = endTime.AddSeconds(86399.9);//23:59:59
            if (beginTime > endTime)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"起始时间应小于结束时间。\")", true);
                return;
            }

            HealthStatus healthStatus = new HealthStatus();
            cpResourceHealthStatusPager.DataSource = healthStatus.Search(resourceType, resourceID, beginTime, endTime);
            cpResourceHealthStatusPager.PageSize = this.SiteSetting.PageSize;
            cpResourceHealthStatusPager.BindToControl = rpResourceHealthStatusList;
            rpResourceHealthStatusList.DataSource = cpResourceHealthStatusPager.DataSourcePaged;
            rpResourceHealthStatusList.DataBind();

            UseStatus useStatus = new UseStatus();
            cpResourceUseStatusPager.DataSource = useStatus.Search(resourceType, resourceID, beginTime, endTime);
            cpResourceUseStatusPager.PageSize = this.SiteSetting.PageSize;
            cpResourceUseStatusPager.BindToControl = rpResourceUseStatusList;
            rpResourceUseStatusList.DataSource = cpResourceUseStatusPager.DataSourcePaged;
            rpResourceUseStatusList.DataBind();
        }
        /// <summary>
        /// 获得资源ID
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceCode"></param>
        /// <returns></returns>
        private int GetResourceID(int resourceType, string resourceCode)
        {
            int resourceID = 0;
            switch (resourceType)
            {
                case 1://地面站资源
                    GroundResource groundResource = new GroundResource();
                    groundResource.GRCode = resourceCode;
                    groundResource = groundResource.SelectByCode();
                    if (groundResource != null && groundResource.Id > 0)
                        resourceID = groundResource.Id;
                    break;
                case 2://通信资源
                    CommunicationResource communicationResource = new CommunicationResource();
                    communicationResource.RouteCode = resourceCode;
                    communicationResource = communicationResource.SelectByCode();
                    if (communicationResource != null && communicationResource.Id > 0)
                        resourceID = communicationResource.Id;
                    break;
                case 3://中心资源
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