#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:ResourceStatusManage.cs
//Remark:资源状态管理类
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
                //默认地面站资源
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
                //从资源管理页面跳转过来需要绑定健康、占用状态
                if (!string.IsNullOrEmpty(ResourceCode))
                {
                    BindResourceStatusList();
                }
            }
        }
        /// <summary>
        /// 查询资源状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindResourceStatusList();
            }
            catch
            { }
        }
        /// <summary>
        /// 查看资源状态图形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnViewChart_Click(object sender, EventArgs e)
        {
            try
            {
                string url = @"~/Views/BusinessManage/ResourceStatusChartManage.aspx?resourcetype={0}&resourcecode={1}&begintime={2}&endtime={3}";
                url = string.Format(url, Server.UrlEncode(dplResourceType.SelectedValue), Server.UrlEncode(txtResourceCode.Text.Trim()), Server.UrlEncode(txtBeginTime.Text), Server.UrlEncode(txtEndTime.Text));
                Response.Redirect(url);
            }
            catch
            { }
        }
        /// <summary>
        /// 添加资源状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_ResStaMan.View";
            this.ShortTitle = "查询资源状态";
            this.SetTitle();
        }

        #region Method
        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        private void BindDataSource()
        {
            //资源管理资源类型列表：地面站资源=1、通信资源=2、中心资源=3
            dplResourceType.Items.Clear();
            dplResourceType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.ResourceType);
            dplResourceType.DataTextField = "key";
            dplResourceType.DataValueField = "value";
            dplResourceType.DataBind();
            dplResourceType.SelectedValue = ResourceType.ToString();

            //资源编号
            txtResourceCode.Text = ResourceCode;

            //开始时间
            txtBeginTime.Text = BeginTime.ToString("yyyy-MM-dd");
            //结束时间
            txtEndTime.Text = EndTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 绑定资源状态列表
        /// </summary>
        private void BindResourceStatusList()
        {
            bool validateResult = true;
            int resourceType = 1;
            int.TryParse(dplResourceType.SelectedValue, out resourceType);
            if (validateResult && string.IsNullOrEmpty(txtResourceCode.Text.Trim()))
            {
                //资源编号不能为空
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"资源编号不能为空。\")", true);
                validateResult = false;
            }
            int resourceID = GetResourceID(resourceType, txtResourceCode.Text.Trim());
            if (validateResult && resourceID < 1)
            {
                //资源不存在
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"资源不存在，请确认输入的资源编号是否正确。\")", true);
                validateResult = false;
            }
            if (validateResult && string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
            {
                //起始时间不能为空
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"起始时间不能为空。\")", true);
                validateResult = false;
            }
            if (validateResult && string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                //结束时间不能为空
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"结束时间不能为空。\")", true);
                validateResult = false;
            }
            DateTime beginTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddDays(7);
            if (validateResult && !DateTime.TryParse(txtBeginTime.Text, out beginTime))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"起始时间格式错误，请正确输入时间（yyyy-MM-dd）。\")", true);
                validateResult = false;
            }
            if (validateResult && !DateTime.TryParse(txtEndTime.Text, out endTime))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"结束时间格式错误，请正确输入时间（yyyy-MM-dd）。\")", true);
                validateResult = false;
            }
            endTime = endTime.AddSeconds(86399.9);//23:59:59
            if (validateResult && beginTime > endTime)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"结束时间应大于起始时间。\")", true);
                validateResult = false;
            }

            //查询健康状态
            HealthStatus healthStatus = new HealthStatus();
            cpResourceHealthStatusPager.DataSource = validateResult ? healthStatus.Search(resourceType, resourceID, beginTime, endTime) : new List<HealthStatus>();
            cpResourceHealthStatusPager.PageSize = this.SiteSetting.PageSize;
            cpResourceHealthStatusPager.BindToControl = rpResourceHealthStatusList;
            rpResourceHealthStatusList.DataSource = cpResourceHealthStatusPager.DataSourcePaged;
            rpResourceHealthStatusList.DataBind();

            //查询占用状态
            UseStatus useStatus = new UseStatus();
            cpResourceUseStatusPager.DataSource = validateResult ? useStatus.Search(resourceType, resourceID, beginTime, endTime) : new List<UseStatus>();
            cpResourceUseStatusPager.PageSize = this.SiteSetting.PageSize;
            cpResourceUseStatusPager.BindToControl = rpResourceUseStatusList;
            rpResourceUseStatusList.DataSource = cpResourceUseStatusPager.DataSourcePaged;
            rpResourceUseStatusList.DataBind();
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