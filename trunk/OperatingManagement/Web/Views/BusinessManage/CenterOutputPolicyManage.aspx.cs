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
    public partial class CenterOutputPolicyManage : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                trMessage.Visible = false;
                lblMessage.Text = string.Empty;
                if (!IsPostBack)
                {
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
                Framework.FieldVerifyResult result;
                string msg = string.Empty;
                CenterOutputPolicy centerOutputPolicy = new CenterOutputPolicy();
                if (string.IsNullOrEmpty(hfCOPID.Value))
                {
                    centerOutputPolicy.InfoFrom = txtInfoFrom.Text.Trim();
                    centerOutputPolicy.InfoType = txtInfoType.Text.Trim();
                    centerOutputPolicy.InfoTo = txtInfoTo.Text.Trim();
                    centerOutputPolicy.Note = txtNote.Text.Trim();
                    centerOutputPolicy.CreatedTime = DateTime.Now;
                    centerOutputPolicy.UpdatedTime = DateTime.Now;
                    result = centerOutputPolicy.Add();

                    hfCOPID.Value = centerOutputPolicy.Id.ToString();
                    lblCreatedTime.Text = centerOutputPolicy.CreatedTime.ToString("yyyy-MM-dd hh:mm:ss");
                    lblUpdatedTime.Text = centerOutputPolicy.UpdatedTime.ToString("yyyy-MM-dd hh:mm:ss");
                }
                else
                {
                    centerOutputPolicy.Id = Convert.ToInt32(hfCOPID.Value);
                    centerOutputPolicy = centerOutputPolicy.SelectByID();

                    centerOutputPolicy.InfoFrom = txtInfoFrom.Text.Trim();
                    centerOutputPolicy.InfoType = txtInfoType.Text.Trim();
                    centerOutputPolicy.InfoTo = txtInfoTo.Text.Trim();
                    centerOutputPolicy.Note = txtNote.Text.Trim();
                    centerOutputPolicy.UpdatedTime = DateTime.Now;
                    result = centerOutputPolicy.Update();

                    lblUpdatedTime.Text = centerOutputPolicy.UpdatedTime.ToString("yyyy-MM-dd hh:mm:ss");
                }

                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "更新中心输出策略成功。";
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                BindControls();
            }
            catch
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
            }
        }

        #region Method

        protected void BindControls()
        {
            CenterOutputPolicy centerOutputPolicy = new CenterOutputPolicy();
            //目前中心输出策略只维护一条数据
            List<CenterOutputPolicy> infoList = centerOutputPolicy.SelectAll();
            if (infoList == null || infoList.Count == 0)
            {
                hfCOPID.Value = string.Empty;
                txtInfoFrom.Text = string.Empty;
                txtInfoType.Text = string.Empty;
                txtInfoTo.Text = string.Empty;
                txtNote.Text = string.Empty;
            }
            else
            {
                centerOutputPolicy = infoList[0];
                hfCOPID.Value = centerOutputPolicy.Id.ToString();
                txtInfoFrom.Text = centerOutputPolicy.InfoFrom;
                txtInfoType.Text = centerOutputPolicy.InfoType;
                txtInfoTo.Text = centerOutputPolicy.InfoTo;
                txtNote.Text = centerOutputPolicy.Note;
                lblCreatedTime.Text = centerOutputPolicy.CreatedTime.ToString("yyyy-MM-dd hh:mm:ss");
                lblUpdatedTime.Text = centerOutputPolicy.UpdatedTime.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }
        #endregion
    }
}