#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:ResourceRequirementAdd.cs
//Remark:资源需求添加类
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20120211    Create     
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
    public partial class ResourceRequirementAdd : AspNetPage
    {
        #region 属性
        /// <summary>
        /// 资源需求列表缓存Guid
        /// </summary>
        protected Guid CacheID
        {
            get
            {
                if (ViewState["cacheid"] == null)
                {
                    Guid cacheID = Guid.NewGuid();
                    if (Request.QueryString["cacheid"] != null)
                    {
                        Guid.TryParse(Request.QueryString["cacheid"], out cacheID);
                    }
                    ViewState["cacheid"] = cacheID;
                }
                return new Guid(ViewState["cacheid"].ToString());
            }
        }
        /// <summary>
        /// 不可用设备
        /// </summary>
        protected List<UnusedEquipment> UnusedEquipmentList
        {
            get
            {
                if (ViewState["UnusedEquipment"] == null)
                {
                    return new List<UnusedEquipment>();
                }
                else
                {
                    return (ViewState["UnusedEquipment"] as List<UnusedEquipment>);
                }
            }
        }
        /// <summary>
        /// 支持时段
        /// </summary>
        protected List<PeriodOfTime> PeriodOfTimeList
        {
            get
            {
                if (ViewState["PeriodOfTime"] == null)
                {
                    return new List<PeriodOfTime>();
                }
                else
                {
                    return (ViewState["PeriodOfTime"] as List<PeriodOfTime>);
                }
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
                    BindSatNameDataSource();
                    BindDataSource();
                }
            }
            catch
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
            }
        }

        /// <summary>
        /// 提交添加资源需求记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = string.Empty;
                if (string.IsNullOrEmpty(lblRequirementName.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "需求名称不能为空";
                    return;
                }
                if (string.IsNullOrEmpty(txtTimeBenchmark.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "时间基准不能为空";
                    return;
                }
                if (string.IsNullOrEmpty(txtPriority.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "优先级不能为空";
                    return;
                }
                int priority = 0;
                if (!int.TryParse(txtPriority.Text.Trim(), out priority))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "优先级格式错误";
                    return;
                }
                if (priority < 1 || priority > 100)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "优先级应为1-100的整数";
                    return;
                }
                if (string.IsNullOrEmpty(dplSatName.SelectedValue.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "卫星编码不能为空";
                    return;
                }
                if (string.IsNullOrEmpty(dplFunctionType.SelectedValue.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "功能类型不能为空";
                    return;
                }
                if (string.IsNullOrEmpty(txtPersistenceTime.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "持续时长不能为空";
                    return;
                }
                int persistenceTime = 0;
                if (!int.TryParse(txtPersistenceTime.Text.Trim(), out persistenceTime))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "持续时长格式错误";
                    return;
                }
                if (persistenceTime < 1)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "持续时长应为大于0的整数";
                    return;
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
        protected void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 添加不可用设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddUnusedEquipment_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGRCode.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "地面站编码不能为空";
                    return;
                }
                if (string.IsNullOrEmpty(txtGREquipmentCode.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "地面站设备编码不能为空";
                    return;
                }

                UnusedEquipment unusedEquipment = new UnusedEquipment() { GRCode = txtGRCode.Text.Trim(), EquipmentCode = txtGREquipmentCode.Text.Trim() };
                List<UnusedEquipment> unusedEquipmentList = UnusedEquipmentList;
                unusedEquipmentList.Add(unusedEquipment);
                ViewState["UnusedEquipment"] = unusedEquipmentList;
                BindUnusedEquipmentList();
                ResetUnusedEquipmentControls();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 删除不可用设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDeleteUnusedEquipment_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnDeleteUnusedEquipment = (sender as LinkButton);
                string grCode = lbtnDeleteUnusedEquipment.CommandArgument.Split('$')[0];
                string equipmentCode = lbtnDeleteUnusedEquipment.CommandArgument.Split('$')[1];
                List<UnusedEquipment> unusedEquipmentList = UnusedEquipmentList;
                int index = unusedEquipmentList.FindIndex(a => a.GRCode.ToLower() == grCode.ToLower() && a.EquipmentCode.ToLower() == equipmentCode.ToLower());
                if (index >= 0)
                    unusedEquipmentList.RemoveAt(index);

                ViewState["UnusedEquipment"] = unusedEquipmentList;
                BindUnusedEquipmentList();
                ResetUnusedEquipmentControls();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 添加支持时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddPeriodOfTime_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "开始时间不能为空";
                    return;
                }
                if (string.IsNullOrEmpty(txtEndTime.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "结束时间不能为空";
                    return;
                }

                DateTime beginTime = DateTime.Now;
                DateTime endTime = DateTime.Now;
                if (!DateTime.TryParse(txtBeginTime.Text.Trim(), out beginTime))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "开始时间格式错误";
                    return;
                }
                if (!DateTime.TryParse(txtEndTime.Text.Trim(), out endTime))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "结束时间格式错误";
                    return;
                }
                if (beginTime > endTime)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "开始时间应小于结束时间";
                    return;
                }

                PeriodOfTime periodOfTime = new PeriodOfTime() {  BeginTime = beginTime, EndTime = endTime };
                List<PeriodOfTime> periodOfTimeList = PeriodOfTimeList;
                periodOfTimeList.Add(periodOfTime);
                ViewState["PeriodOfTime"] = periodOfTimeList;
                BindPeriodOfTimeList();
                ResetPeriodOfTimeControls();

            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 删除支持时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDeletePeriodOfTime_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnDeletePeriodOfTime = (sender as LinkButton);
                DateTime beginTime = DateTime.Now;
                DateTime endTime = DateTime.Now;
                if (DateTime.TryParse(lbtnDeletePeriodOfTime.CommandArgument.Split('$')[0], out beginTime) && DateTime.TryParse(lbtnDeletePeriodOfTime.CommandArgument.Split('$')[1], out endTime))
                {
                    List<PeriodOfTime> periodOfTimeList = PeriodOfTimeList;
                    int index = periodOfTimeList.FindIndex(a => a.BeginTime == beginTime && a.EndTime == endTime);
                    if (index >= 0)
                        periodOfTimeList.RemoveAt(index);

                    ViewState["PeriodOfTime"] = periodOfTimeList;
                }

                BindPeriodOfTimeList();
                ResetPeriodOfTimeControls();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 当卫星编码发生变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplSatName_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidWXBMIndex.Value = GetWXBMIndex().ToString();
            lblRequirementName.Text = GetRequirementName();
        }

        public override void OnPageLoaded()
        {
            //this.PagePermission = "OMB_ResStaMan.View";
            this.ShortTitle = "资源需求添加";
            this.SetTitle();
        }

        #region Method
        /// <summary>
        /// 绑定卫星数据源
        /// </summary>
        private void BindSatNameDataSource()
        {
            dplSatName.Items.Clear();
            Satellite satellite = new Satellite();
            dplSatName.DataSource = satellite.SatelliteCache;
            dplSatName.DataTextField = "WXMC";
            dplSatName.DataValueField = "Id";
            dplSatName.DataBind();
            dplSatName.Items.Insert(0, new ListItem("请选择", ""));
        }
        /// <summary>
        /// 绑定控件数据源,暂时这样做,没有提供表结构
        /// </summary>
        private void BindDataSource()
        {
            dplFunctionType.Items.Clear();
            dplFunctionType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.GroundResourceFunctionType);
            dplFunctionType.DataTextField = "key";
            dplFunctionType.DataValueField = "value";
            dplFunctionType.DataBind();
        }
        /// <summary>
        /// 根据卫星编码生成卫星序号
        /// </summary>
        private int GetWXBMIndex()
        {
            int wxbmIndex = 0;
            //当没有选择卫星时
            if (string.IsNullOrEmpty(dplSatName.SelectedValue))
                return wxbmIndex;

            //当缓存中为空时
            if (HttpContext.Current.Cache[CacheID.ToString()] == null)
            {
                wxbmIndex = 1;
                return wxbmIndex;
            }

            //其他情况
            List<ResourceRequirement> resourceRequirementList = (HttpContext.Current.Cache[CacheID.ToString()] as List<ResourceRequirement>);
            var query = resourceRequirementList.Where(a => a.WXBM.ToLower() == dplSatName.SelectedValue.ToLower()).OrderBy(a => a.WXBMIndex);
            resourceRequirementList = query.ToList();
            wxbmIndex = resourceRequirementList.Count() + 1;

            for (int i = 0; i < resourceRequirementList.Count(); i++)
            {
                if (resourceRequirementList[i].WXBMIndex != i + 1)
                {
                    wxbmIndex = i + 1;
                    break;
                }
            }
           
            return wxbmIndex;
        }
        /// <summary>
        /// 根据卫星编码生成需求名称
        /// </summary>
        /// <returns></returns>
        public string GetRequirementName()
        {
            string requirementName = string.Empty;
            //当没有选择卫星时
            if (string.IsNullOrEmpty(dplSatName.SelectedValue) || hidWXBMIndex.Value == "0")
                return requirementName;

            switch (hidWXBMIndex.Value.Length)
            {
                case 1:
                    requirementName = "00" + hidWXBMIndex.Value;
                    break;
                case 2:
                    requirementName = "0" + hidWXBMIndex.Value;
                    break;
                case 3:
                    requirementName = hidWXBMIndex.Value;
                    break;
                default:
                    //添加单个卫星个数大于999
                    requirementName = hidWXBMIndex.Value;
                    break;
            }
            return dplSatName.SelectedValue + requirementName;
        }
        /// <summary>
        /// 绑定不可用设备列表
        /// </summary>
        private void BindUnusedEquipmentList()
        {
            cpUnusedEquipmentPager.DataSource = UnusedEquipmentList;
            cpUnusedEquipmentPager.PageSize = this.SiteSetting.PageSize;
            cpUnusedEquipmentPager.BindToControl = rpUnusedEquipmentList;
            rpUnusedEquipmentList.DataSource = cpUnusedEquipmentPager.DataSourcePaged;
            rpUnusedEquipmentList.DataBind();
        }
        /// <summary>
        /// 重置不可用设备控件
        /// </summary>
        private void ResetUnusedEquipmentControls()
        {
            txtGRCode.Text = string.Empty;
            txtGREquipmentCode.Text = string.Empty;
            trMessage.Visible = false;
            lblMessage.Text = string.Empty;
        }
        /// <summary>
        /// 绑定支持时段列表
        /// </summary>
        private void BindPeriodOfTimeList()
        {
            cpPeriodOfTimePager.DataSource = PeriodOfTimeList;
            cpPeriodOfTimePager.PageSize = this.SiteSetting.PageSize;
            cpPeriodOfTimePager.BindToControl = rpPeriodOfTimeList;
            rpPeriodOfTimeList.DataSource = cpPeriodOfTimePager.DataSourcePaged;
            rpPeriodOfTimeList.DataBind();
        }
        /// <summary>
        /// 重置支持时段控件
        /// </summary>
        private void ResetPeriodOfTimeControls()
        {
            txtBeginTime.Text = string.Empty;
            txtEndTime.Text = string.Empty;
            trMessage.Visible = false;
            lblMessage.Text = string.Empty;
        }
        #endregion
    }
}