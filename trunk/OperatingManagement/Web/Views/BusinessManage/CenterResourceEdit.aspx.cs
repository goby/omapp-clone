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
    public partial class CenterResourceEdit : AspNetPage
    {
        #region 属性
        /// <summary>
        /// 中心资源ID
        /// </summary>
        protected int CRID
        {
            get
            {
                int crID = 0;
                if (Request.QueryString["crid"] != null)
                {
                    int.TryParse(Request.QueryString["crid"], out crID);
                }
                return crID;
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
                if (string.IsNullOrEmpty(txtEquipmentCode.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "设备编号不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(dplEquipmentType.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "设备类型不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtSupportTask.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "支持的任务不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtDataProcess.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "最大数据处理量不能为空";
                    return;
                }

                Framework.FieldVerifyResult result;
                CenterResource centerResource = new CenterResource();
                centerResource.Id = CRID;
                centerResource = centerResource.SelectByID();
                if (centerResource == null)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "修改的中心资源不存在";
                    return;
                }
                centerResource.EquipmentCode = txtEquipmentCode.Text.Trim();
                centerResource.EquipmentType = dplEquipmentType.SelectedValue;
                centerResource.SupportTask = txtSupportTask.Text.Trim();
                centerResource.DataProcess = txtDataProcess.Text.Trim();
                //centerResource.Status = 1;//正常
                //centerResource.CreatedTime = DateTime.Now;
                centerResource.UpdatedTime = DateTime.Now;

                if (centerResource.HaveActiveEquipmentCode())
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "设备编号已经存在";
                    return;
                }

                result = centerResource.Update();
                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "修改中心资源成功。";
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
                string url = @"~/Views/BusinessManage/ResourceManage.aspx?resourcetype=" + Server.UrlEncode("3");
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
            dplEquipmentType.Items.Clear();
            dplEquipmentType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.CenterResourceEquipmentType);
            dplEquipmentType.DataTextField = "key";
            dplEquipmentType.DataValueField = "value";
            dplEquipmentType.DataBind();
            dplEquipmentType.Items.Insert(0, new ListItem("请选择", ""));
        }
        /// <summary>
        /// 绑定控件值
        /// </summary>
        private void BindControls()
        {
            CenterResource centerResource = new CenterResource();
            centerResource.Id = CRID;
            centerResource = centerResource.SelectByID();
            if (centerResource != null)
            {
                txtEquipmentCode.Text = centerResource.EquipmentCode;
                dplEquipmentType.SelectedValue = centerResource.EquipmentType;
                txtSupportTask.Text = centerResource.SupportTask;
                txtDataProcess.Text = centerResource.DataProcess;
                lblCreatedTime.Text = centerResource.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                lblUpdatedTime.Text = centerResource.UpdatedTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        #endregion
    }
}