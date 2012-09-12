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
    public partial class SatAdd : AspNetPage
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_SatkMan.Add";
            this.ShortTitle = "新增卫星";
            this.SetTitle();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            DataAccessLayer.BusinessManage.Satellite s = new DataAccessLayer.BusinessManage.Satellite()
            {
                WXMC = txtWXMC.Text.Trim(),
                WXBM = txtWXBM.Text.Trim(),
                WXBS = txtWXBS.Text.Trim(),
                State = rblState.SelectedValue,
                MZB = int.Parse(txtMZB.Text.Trim()),
                BMFSXS = int.Parse(txtBMFSXS.Text.Trim()),
                /*
                 * 扩展属性
                 * 功能
                 */
                CTime = DateTime.Now
            };
            var result = Framework.FieldVerifyResult.Error;
            try
            {
                result = s.Add();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("新增卫星页面保存卫星出现异常，异常原因", ex));
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
                    msg = "新增任务已成功。";
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

        protected void btnEmpty_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath);
        }
    }
}