#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:CenterOutputPolicyAdd.cs
//Remark:中心输出策略添加类
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
    public partial class CenterOutputPolicyAdd : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                trMessage.Visible = false;
                lblMessage.Text = string.Empty;
                if (!IsPostBack)
                {
                    InitialPageData();
                }
            }
            catch
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
            }
        }
        /// <summary>
        /// 提交添加中心输出策略记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = string.Empty;
                if (string.IsNullOrEmpty(ddlTask.SelectedItem.Value))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择任务代号";
                    return;
                }

                if (string.IsNullOrEmpty(ddlSatellite.SelectedItem.Value))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择卫星名称";
                    return;
                }

                if (string.IsNullOrEmpty(ddlSource.SelectedItem.Value))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择信源";
                    return;
                }

                if (string.IsNullOrEmpty(ddlInfoType.SelectedItem.Value))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择信息类别";
                    return;
                }

                if (string.IsNullOrEmpty(ddlDdestination.SelectedItem.Value))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择信宿";
                    return;
                }

                if (ddlSource.SelectedItem.Value == ddlDdestination.SelectedItem.Value)
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
                //23:59:59
                defectTime = defectTime.AddSeconds(86399.9);
                if (effectTime > defectTime)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "失效时间应大于生效时间";
                    return;
                }
                
                Framework.FieldVerifyResult result;
                CenterOutputPolicy centerOutputPolicy = new CenterOutputPolicy();
                centerOutputPolicy.TaskID = ddlTask.SelectedItem.Value;
                centerOutputPolicy.SatName = ddlSatellite.SelectedItem.Value;
                centerOutputPolicy.InfoSource = Convert.ToInt32(ddlSource.SelectedItem.Value);
                centerOutputPolicy.InfoType = Convert.ToInt32(ddlInfoType.SelectedItem.Value);
                centerOutputPolicy.Ddestination = Convert.ToInt32(ddlDdestination.SelectedItem.Value);
                centerOutputPolicy.EffectTime = effectTime;
                centerOutputPolicy.DefectTime = defectTime;
                centerOutputPolicy.Note = txtNote.Text.Trim();
                centerOutputPolicy.CreatedTime = DateTime.Now;
                centerOutputPolicy.UpdatedTime = DateTime.Now;
                if (centerOutputPolicy.HaveEffectivePolicy())
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "任务" + ddlTask.SelectedItem.Text + "在该时间范围已经存在中心输出策略";
                    return;
                }

                result = centerOutputPolicy.Add();
                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "添加中心输出策略成功。";
                        ResetControls();
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
            this.ShortTitle = "中心输出策略添加";
            this.SetTitle();
        }

        #region Method
        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        private void InitialPageData()
        {
            this.ddlDdestination.AllowBlankItem = false;
            this.ddlInfoType.AllowBlankItem = false;
            this.ddlSatellite.AllowBlankItem = false;
            this.ddlSource.AllowBlankItem = false;
            this.ddlTask.AllowBlankItem = false;
        }
        /// <summary>
        /// 添加完成后将控件设置为初始状态
        /// </summary>
        private void ResetControls()
        {
            ddlTask.SelectedIndex = 0;
            ddlSource.SelectedIndex = 0;
            ddlSatellite.SelectedIndex = 0;
            ddlInfoType.SelectedIndex = 0;
            ddlDdestination.SelectedIndex = 0;
            txtEffectTime.Text = string.Empty;
            txtDefectTime.Text = string.Empty;
            txtNote.Text = string.Empty;
        }

        #endregion
    }
}