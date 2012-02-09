#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:ResourceStatusChartManage.cs
//Remark:资源状态管理类
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20120204    Create     
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
    public partial class ResourceStatusChartManage : AspNetPage
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
        /// <summary>
        /// 健康状态列表
        /// </summary>
        protected List<HealthStatus> _healthStatusList = null;
        protected List<HealthStatus> HealthStatusList
        {
            get { return _healthStatusList; }

            set { _healthStatusList = value; }
        }
        /// <summary>
        /// 占用状态列表
        /// </summary>
        protected List<UseStatus> _useStatusList = null;
        protected List<UseStatus> UseStatusList
        {
            get { return _useStatusList; }

            set { _useStatusList = value; }
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
                    BindResourceStatusChart();
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
                BindResourceStatusChart();
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

        protected void chartResourceStatus_PreRender(object sender, EventArgs e)
        {
            string healthStatusStr = @"资源类型：{0}，资源名称：{1}，资源编码：{2}，{3}健康状态：{4}，开始时间：{5}，结束时间：{6}";
            for (int i = 0; i < chartResourceStatus.Series["seriesHealthStatus"].Points.Count; i++)
            {
                string resourceType = SystemParameters.GetSystemParameterText(SystemParametersType.ResourceType, HealthStatusList[i].ResourceType.ToString());
                string resourceName = HealthStatusList[i].ResourceName;
                string resourceCode = HealthStatusList[i].ResourceCode;
                string functionType = HealthStatusList[i].ResourceType.ToString() == "1" ? ("功能类型：" + SystemParameters.GetSystemParameterText(SystemParametersType.HealthStatusFunctionType, HealthStatusList[i].FunctionType) + "，") : "";
                string healthStatus = SystemParameters.GetSystemParameterText(SystemParametersType.HealthStatus, HealthStatusList[i].Status.ToString());
                string beginTime = HealthStatusList[i].BeginTime.ToString("yyyy-MM-dd");
                string endTime = HealthStatusList[i].EndTime.ToString("yyyy-MM-dd");
                chartResourceStatus.Series["seriesHealthStatus"].Points[i].ToolTip = string.Format(healthStatusStr, resourceType, resourceName, resourceCode, functionType, healthStatus, beginTime, endTime);
            }

            string useStatusStr = @"资源类型：{0}，资源名称：{1}，资源编码：{2}，占用类型：{3}，开始时间：{4}，结束时间：{5}{6}{7}{8}{9}";
            for (int i = 0; i < chartResourceStatus.Series["seriesUseStatus"].Points.Count; i++)
            {
                string resourceType = SystemParameters.GetSystemParameterText(SystemParametersType.ResourceType, UseStatusList[i].ResourceType.ToString());
                string resourceName = UseStatusList[i].ResourceName;
                string resourceCode = UseStatusList[i].ResourceCode;
                string usedType = SystemParameters.GetSystemParameterText(SystemParametersType.UseStatusUsedType, UseStatusList[i].UsedType.ToString());
                string beginTime = UseStatusList[i].BeginTime.ToString("yyyy-MM-dd");
                string endTime = UseStatusList[i].EndTime.ToString("yyyy-MM-dd");
                string usedBy = UseStatusList[i].UsedType == 1 ? "，服务对象：" + UseStatusList[i].UsedBy : "";
                string usedCategory = UseStatusList[i].UsedType == 1 ? "，服务种类：" + UseStatusList[i].UsedCategory : "";
                string usedFor = UseStatusList[i].UsedType == 3 ? "，占用原因：" + UseStatusList[i].UsedFor : "";
                string canBeUsed = UseStatusList[i].UsedType == 3 ? "，是否可执行任务：" + SystemParameters.GetSystemParameterText(SystemParametersType.UseStatusCanBeUsed, UseStatusList[i].CanBeUsed.ToString()) : "";
                chartResourceStatus.Series["seriesUseStatus"].Points[i].ToolTip = string.Format(useStatusStr, resourceType, resourceName, resourceCode, usedType, beginTime, endTime, usedBy, usedCategory, usedFor, canBeUsed);
            }
        }

        public override void OnPageLoaded()
        {
            this.ShortTitle = "资源状态图形显示";
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
        private void BindResourceStatusChart()
        {
            int resourceType = 1;
            int.TryParse(dplResourceType.SelectedValue, out resourceType);
            if (string.IsNullOrEmpty(txtResourceCode.Text.Trim()))
            {
                //资源编号不能为空
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"资源编号不能为空。\")", true);
                return;
            }
            int resourceID = GetResourceID(resourceType, txtResourceCode.Text.Trim());
            if (resourceID < 1)
            {
                //资源不存在
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"资源不存在，请确认输入的资源编号是否正确。\")", true);
                return;
            }
            if (string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
            {
                //起始时间不能为空
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"起始时间不能为空。\")", true);
                return;
            }
            if (string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                //结束时间不能为空
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"结束时间应大于起始时间。\")", true);
                return;
            }

            //查询健康状态
            HealthStatus healthStatus = new HealthStatus();
            HealthStatusList = healthStatus.Search(resourceType, resourceID, beginTime, endTime);
            chartResourceStatus.Series["seriesHealthStatus"].Points.DataBindXY(HealthStatusList, "ResourceName", HealthStatusList, "BeginTime,EndTime");

            //查询占用状态
            UseStatus useStatus = new UseStatus();
            UseStatusList = useStatus.Search(resourceType, resourceID, beginTime, endTime);
            chartResourceStatus.Series["seriesUseStatus"].Points.DataBindXY(UseStatusList, "ResourceName", UseStatusList, "BeginTime,EndTime");
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