using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.Framework.Core;

namespace OperatingManagement.Web.Views.BusinessManage.BDManage
{
    public partial class SatEdit : AspNetPage
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_SatManage.Edit";
            this.ShortTitle = "编辑卫星";
            this.SetTitle();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSat();
            }
        }

        void BindSat()
        {
            string wxbm = Request.QueryString["Id"];
            DataAccessLayer.BusinessManage.Satellite s = new DataAccessLayer.BusinessManage.Satellite() { WXBM = wxbm };
            var sat = new DataAccessLayer.BusinessManage.Satellite();
            try
            {
                sat = s.SelectByID();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("修改卫星页面读取卫星数据出现异常，异常原因", ex));
            }
            finally { }

            txtWXMC.Text = sat.WXMC;
            txtWXBM.Text = sat.WXBM;
            txtWXBS.Text = sat.WXBS;
            rblState.SelectedIndex = int.Parse(sat.State);
            txtMZB.Text = sat.MZB.ToString();
            txtBMFSXS.Text = sat.BMFSXS.ToString();
            /*
             * 属性及功能
             */
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            DataAccessLayer.BusinessManage.Satellite s = new DataAccessLayer.BusinessManage.Satellite()
            {
                WXBM = Request.QueryString["Id"],
                WXMC = txtWXMC.Text.Trim(),
                WXBS = txtWXBS.Text.Trim(),
                State = rblState.SelectedValue,
                MZB = int.Parse(txtMZB.Text.Trim()),
                BMFSXS = int.Parse(txtBMFSXS.Text.Trim())
                /*
                 * 属性及功能
                 */
            };
            var result = Framework.FieldVerifyResult.Error;
            try
            {
                result = s.Update();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("修改卫星页面保存卫星数据出现异常，异常原因", ex));
            }
            finally { }

            string msg = string.Empty;
            switch (result)
            {
                case Framework.FieldVerifyResult.NameDuplicated:
                    msg = "已存在相同名称，请输入其他“卫星名称”。";
                    break;
                case Framework.FieldVerifyResult.Error:
                    msg = "发生了数据错误，无法完成请求的操作。";
                    break;
                case Framework.FieldVerifyResult.Success:
                    msg = "编辑任务已成功。";
                    break;
                case Framework.FieldVerifyResult.NameDuplicated2:
                    msg = "已存在相同名称，请输入其他“卫星编码”。";
                    break;
                case Framework.FieldVerifyResult.NameDuplicated3:
                    msg = "已存在相同名称，请输入其他“卫星标识”。";
                    break;
            }
            ltMessage.Text = msg;
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("satmanage.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath + "?id=" + Request.QueryString["Id"]);
        }
    }
}