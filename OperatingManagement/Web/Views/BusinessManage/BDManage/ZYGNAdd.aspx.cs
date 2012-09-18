using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer.BusinessManage;


namespace OperatingManagement.Web.Views.BusinessManage.BDManage
{
    public partial class ZYGNAdd : AspNetPage
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_ZYGNMan.Add";
            this.ShortTitle = "新增资源功能";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/BusinessManage/ZYGNAdd.aspx.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        protected void InitData()
        {
            ZYSX zy = new ZYSX();
            lbDMZ.DataSource = zy.GetGroundStationZYSXList();
            lbDMZ.DataTextField = "PName";
            lbDMZ.DataValueField = "PCode";
            lbDMZ.DataBind();

            lbSat.DataSource = zy.GetSatelliteZYSXList();
            lbSat.DataTextField = "PName";
            lbSat.DataValueField = "PCode";
            lbSat.DataBind();

            GroundResource g = new GroundResource();
            ddlDMZ.DataSource = g.SelectAll();
            ddlDMZ.DataTextField = "EquipmentName";
            ddlDMZ.DataValueField = "EquipmentCode";
            ddlDMZ.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            DataAccessLayer.BusinessManage.ZYGN t = new DataAccessLayer.BusinessManage.ZYGN()
            {
                FName = txtName.Text.Trim(),
                FCode = txtCode.Text.Trim(),
                MatchRule = ucSatellite1.SelectedValue + "."+lbSat.SelectedValue + rblOwn.SelectedValue
                                    + ddlDMZ.SelectedValue + "." + lbDMZ.SelectedValue

            };
            var result = Framework.FieldVerifyResult.Error;
            try
            {
                result = t.Add();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("新增资源功能页面保存任务出现异常，异常原因", ex));
            }
            finally { }
            string msg = string.Empty;
            switch (result)
            {
                case Framework.FieldVerifyResult.NameDuplicated:
                    msg = "已存在相同名称，请输入其他“名称”。";
                    break;
                case Framework.FieldVerifyResult.Error:
                    msg = "发生了数据错误，无法完成请求的操作。";
                    break;
                case Framework.FieldVerifyResult.Success:
                    msg = "新增资源功能已成功。";
                    break;
                case Framework.FieldVerifyResult.NameDuplicated2:
                    msg = "已存在相同编码，请输入其他“资源功能编码”。";
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