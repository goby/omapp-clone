using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OperatingManagement.DataAccessLayer.BusinessManage;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.BusinessManage
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = string.Empty;
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
                centerOutputPolicy.InfoSource = dplInfoSource.SelectedValue;
                centerOutputPolicy.InfoType = dplInfoType.SelectedValue;
                centerOutputPolicy.Ddestination = dplDdestination.SelectedValue;
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
                string url = @"~/BusinessManage/CenterOutputPolicyManage.aspx";
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
            CenterOutputPolicy centerOutputPolicy = new CenterOutputPolicy();

            dplTask.Items.Clear();
            dplTask.DataSource = centerOutputPolicy.GetSystemParameters(SystemParametersType.TaskList);
            dplTask.DataTextField = "key";
            dplTask.DataValueField = "value";
            dplTask.DataBind();
            dplTask.Items.Insert(0, new ListItem("请选择", ""));
            dplTask.Enabled = false;

            dplInfoSource.Items.Clear();
            dplInfoSource.DataSource = centerOutputPolicy.GetSystemParameters(SystemParametersType.InfoSource);
            dplInfoSource.DataTextField = "key";
            dplInfoSource.DataValueField = "value";
            dplInfoSource.DataBind();
            dplInfoSource.Items.Insert(0, new ListItem("请选择", ""));

            dplInfoType.Items.Clear();
            dplInfoType.DataSource = centerOutputPolicy.GetSystemParameters(SystemParametersType.InfoType);
            dplInfoType.DataTextField = "key";
            dplInfoType.DataValueField = "value";
            dplInfoType.DataBind();
            dplInfoType.Items.Insert(0, new ListItem("请选择", ""));

            dplDdestination.Items.Clear();
            dplDdestination.DataSource = centerOutputPolicy.GetSystemParameters(SystemParametersType.Ddestination);
            dplDdestination.DataTextField = "key";
            dplDdestination.DataValueField = "value";
            dplDdestination.DataBind();
            dplDdestination.Items.Insert(0, new ListItem("请选择", ""));
        }
        /// <summary>
        /// 绑定卫星数据源
        /// 等确定卫星表结构及来源后替换
        /// </summary>
        private void BindSatNameDataSource()
        {
            dplSatName.Items.Clear();
            for (int i = 1; i <= 5; i++)
            {
                dplSatName.Items.Add(new ListItem("卫星" + i.ToString(), i.ToString()));
            }
            dplSatName.Items.Insert(0, new ListItem("请选择", ""));
            dplSatName.Enabled = false;
        }

        private void BindControls()
        {
            CenterOutputPolicy centerOutputPolicy = new CenterOutputPolicy();
            centerOutputPolicy.Id = COPID;
            centerOutputPolicy = centerOutputPolicy.SelectByID();
            if (centerOutputPolicy != null)
            {
                dplTask.SelectedIndex = dplTask.Items.IndexOf(dplTask.Items.FindByValue(centerOutputPolicy.TaskID));
                dplSatName.SelectedIndex = dplSatName.Items.IndexOf(dplSatName.Items.FindByValue(centerOutputPolicy.SatName));
                dplInfoSource.SelectedIndex = dplInfoSource.Items.IndexOf(dplInfoSource.Items.FindByValue(centerOutputPolicy.InfoSource));
                dplInfoType.SelectedIndex = dplInfoType.Items.IndexOf(dplInfoType.Items.FindByValue(centerOutputPolicy.InfoType));
                dplDdestination.SelectedIndex = dplDdestination.Items.IndexOf(dplDdestination.Items.FindByValue(centerOutputPolicy.Ddestination));
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