#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:CenterOutputPolicyEdit.cs
//Remark:中心输出策略编辑类
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
    public partial class CenterOutputPolicyEdit : AspNetPage
    {
        #region 属性
        /// <summary>
        /// 中心策略ID
        /// </summary>
        protected int COPID
        {
            get
            {
                int copID = 0;
                if (Request.QueryString["copid"] != null)
                {
                    int.TryParse(Request.QueryString["copid"], out copID);
                }
                return copID;
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
                    BindSatNameDataSource();
                    BindControls();
                }
            }
            catch
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
            }
        }
        /// <summary>
        /// 提交更新中心输出策略记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = string.Empty;
                if (string.IsNullOrEmpty(dplTask.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择任务代号";
                    return;
                }

                if (string.IsNullOrEmpty(dplSatName.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择卫星名称";
                    return;
                }

                if (string.IsNullOrEmpty(dplInfoSource.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择信源";
                    return;
                }

                if (string.IsNullOrEmpty(dplInfoType.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择信息类别";
                    return;
                }

                if (string.IsNullOrEmpty(dplDdestination.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择信宿";
                    return;
                }

                if (dplInfoSource.SelectedValue == dplDdestination.SelectedValue)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "信源与信宿不能相同";
                    return;
                }

                if (string.IsNullOrEmpty(txtEffectTime.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "生效时间不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtDefectTime.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "失效时间不能为空";
                    return;
                }

                DateTime effectTime = DateTime.Now;
                DateTime defectTime = DateTime.Now;

                if (!DateTime.TryParse(txtEffectTime.Text.Trim(), out effectTime))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "生效时间格式错误";
                    return;
                }

                if (!DateTime.TryParse(txtDefectTime.Text.Trim(), out defectTime))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "失效时间格式错误";
                    return;
                }
                defectTime = defectTime.AddSeconds(86399.9);//23:59:59
                if (effectTime > defectTime)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "失效时间应大于生效时间";
                    return;
                }

                Framework.FieldVerifyResult result;
                CenterOutputPolicy centerOutputPolicy = new CenterOutputPolicy();
                centerOutputPolicy.Id = COPID;
                centerOutputPolicy = centerOutputPolicy.SelectByID();
                if (centerOutputPolicy == null)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "修改的中心输出策略不存在";
                    return;
                }
                //centerOutputPolicy.TaskID = dplTask.SelectedValue;
                //centerOutputPolicy.SatName = dplSatName.SelectedValue;
                centerOutputPolicy.InfoSource = Convert.ToInt32(dplInfoSource.SelectedValue);
                centerOutputPolicy.InfoType = Convert.ToInt32(dplInfoType.SelectedValue);
                centerOutputPolicy.Ddestination = Convert.ToInt32(dplDdestination.SelectedValue);
                centerOutputPolicy.EffectTime = effectTime;
                centerOutputPolicy.DefectTime = defectTime;
                centerOutputPolicy.Note = txtNote.Text.Trim();
                //centerOutputPolicy.CreatedTime = DateTime.Now;
                centerOutputPolicy.UpdatedTime = DateTime.Now;
                if (centerOutputPolicy.HaveEffectivePolicy())
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "任务" + dplTask.SelectedItem.Text + "在该时间范围已经存在中心输出策略";
                    return;
                }

                result = centerOutputPolicy.Update();
                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "修改中心输出策略成功。";
                        break;
                    default:
                        msg = "发生未知错误，操作失败。";
                        break;
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
                string url = @"~/Views/BusinessManage/CenterOutputPolicyManage.aspx";
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }

        public override void OnPageLoaded()
        {
            this.ShortTitle = "中心输出策略编辑";
            this.SetTitle();
        }

        #region Method
        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        private void BindDataSource()
        {
            //绑定任务列表数据源
            dplTask.Items.Clear();
            dplTask.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.CenterOutputPolicyTaskList);
            dplTask.DataTextField = "key";
            dplTask.DataValueField = "value";
            dplTask.DataBind();
            dplTask.Items.Insert(0, new ListItem("请选择", ""));
            dplTask.Enabled = false;

            //绑定信源数据源
            XYXSInfo xyxsInfo = new XYXSInfo();
            dplInfoSource.Items.Clear();
            //dplInfoSource.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.CenterOutputPolicyInfoSource);
            dplInfoSource.DataSource = xyxsInfo.XYXSInfoCache;
            dplInfoSource.DataTextField = "ADDRName";
            dplInfoSource.DataValueField = "Id";
            dplInfoSource.DataBind();
            dplInfoSource.Items.Insert(0, new ListItem("请选择", ""));

            //绑定信息类型数据源
            InfoType xxType = new InfoType();
            dplInfoType.Items.Clear();
            //dplInfoType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.CenterOutputPolicyInfoType);
            dplInfoType.DataSource = xxType.XXTYPECache;
            dplInfoType.DataTextField = "DATANAME";
            dplInfoType.DataValueField = "Id";
            dplInfoType.DataBind();
            dplInfoType.Items.Insert(0, new ListItem("请选择", ""));

            //绑定信宿数据源
            dplDdestination.Items.Clear();
            //dplDdestination.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.CenterOutputPolicyDdestination);
            dplDdestination.DataSource = xyxsInfo.XYXSInfoCache;
            dplDdestination.DataTextField = "ADDRName";
            dplDdestination.DataValueField = "Id";
            dplDdestination.DataBind();
            dplDdestination.Items.Insert(0, new ListItem("请选择", ""));
        }
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
            dplSatName.Enabled = false;
        }

        /// <summary>
        /// 为控件绑定值
        /// </summary>
        private void BindControls()
        {
            CenterOutputPolicy centerOutputPolicy = new CenterOutputPolicy();
            centerOutputPolicy.Id = COPID;
            centerOutputPolicy = centerOutputPolicy.SelectByID();
            if (centerOutputPolicy != null)
            {
                dplTask.SelectedIndex = dplTask.Items.IndexOf(dplTask.Items.FindByValue(centerOutputPolicy.TaskID));
                dplSatName.SelectedIndex = dplSatName.Items.IndexOf(dplSatName.Items.FindByValue(centerOutputPolicy.SatName));
                dplInfoSource.SelectedIndex = dplInfoSource.Items.IndexOf(dplInfoSource.Items.FindByValue(centerOutputPolicy.InfoSource.ToString()));
                dplInfoType.SelectedIndex = dplInfoType.Items.IndexOf(dplInfoType.Items.FindByValue(centerOutputPolicy.InfoType.ToString()));
                dplDdestination.SelectedIndex = dplDdestination.Items.IndexOf(dplDdestination.Items.FindByValue(centerOutputPolicy.Ddestination.ToString()));
                txtEffectTime.Text = centerOutputPolicy.EffectTime.ToString("yyyy-MM-dd");
                txtDefectTime.Text = centerOutputPolicy.DefectTime.ToString("yyyy-MM-dd");
                txtNote.Text = centerOutputPolicy.Note;
                lblCreatedTime.Text = centerOutputPolicy.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                lblUpdatedTime.Text = centerOutputPolicy.UpdatedTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        #endregion
    }
}