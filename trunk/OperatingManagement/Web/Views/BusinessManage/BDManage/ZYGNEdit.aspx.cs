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
    public partial class ZYGNEdit : AspNetPage
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
                BindZYGN();
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

        void BindZYGN()
        {
            int zyId = Convert.ToInt32(Request.QueryString["Id"]);
            hfID.Value = zyId.ToString();
            DataAccessLayer.BusinessManage.ZYGN t = new DataAccessLayer.BusinessManage.ZYGN() { Id = zyId };
            var zy = new DataAccessLayer.BusinessManage.ZYGN();
            try
            {
                zy = t.SelectByID();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("修改资源属性页面读取资源属性数据出现异常，异常原因", ex));
            }
            finally { }

            txtName.Text = zy.FName;
            txtCode.Text = zy.FCode;
            ucSatellite1.SelectedValue = SpliteMatchRule( zy.MatchRule,1);
            ddlDMZ.SelectedValue = SpliteMatchRule(zy.MatchRule, 4);
            lbSat.SelectedValue = SpliteMatchRule(zy.MatchRule, 2);
            lbDMZ.SelectedValue = SpliteMatchRule(zy.MatchRule, 5);
            rblOwn.SelectedValue = SpliteMatchRule(zy.MatchRule, 3);
        }

        /// <summary>
        /// 返回匹配准则中的各部分值
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="index">1:satallite; 2: 卫星属性; 3:符号； 4:DMZ；5：DMZ属性</param>
        /// <returns></returns>
        private string SpliteMatchRule(string rule,int index)
        {
            string result = "";
            int pos = -1;
            if (rule.IndexOf("<") > 1)
            {
                pos = rule.IndexOf("<");
                result = "<";
            }
            if (rule.IndexOf("<=") > 1)
            {
                pos = rule.IndexOf("<=");
                rule = rule.Replace("<=", "#"); //符号为两位，换成一位的符号，便于计算长度
                result = "<=";
            }
            if (rule.IndexOf("=") > 1)
            {
                pos = rule.IndexOf("=");
                result = "=";
            }
            if (rule.IndexOf(">=") > 1)
            {
                pos = rule.IndexOf(">=");
                rule = rule.Replace(">=", "#"); //符号为两位，换成一位的符号，便于计算长度
                result = ">=";
            }
            if (rule.IndexOf(">") > 1)
            {
                pos = rule.IndexOf(">");
                result = ">";
            }
            switch (index)
            { 
                case 1: //Satellite
                    result = rule.Substring(0, rule.IndexOf("."));
                    break;
                case 2: //Satellite 属性
                    result = rule.Substring(rule.IndexOf(".") +1, pos - rule.IndexOf(".")-1 );
                    break;
                case 3: //符号
                    break;
                case 4: //DMZ
                    result = rule.Substring(pos, rule.LastIndexOf(".") - pos);
                    break;
                case 5: //DMZ 属性
                    result = rule.Substring(rule.LastIndexOf(".") + 1);
                    break;
            }

            return result;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            DataAccessLayer.BusinessManage.ZYGN t = new DataAccessLayer.BusinessManage.ZYGN()
            {
                Id = Convert.ToInt32( hfID.Value),
                FName = txtName.Text.Trim(),
                FCode = txtCode.Text.Trim(),
                MatchRule = ucSatellite1.SelectedValue + "." + lbSat.SelectedValue + rblOwn.SelectedValue
                                    + ddlDMZ.SelectedValue + "." + lbDMZ.SelectedValue

            };
            var result = Framework.FieldVerifyResult.Error;
            try
            {
                result = t.Update();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("修改资源功能页面保存资源功能出现异常，异常原因", ex));
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
                    msg = "修改资源功能已成功。";
                    break;
                case Framework.FieldVerifyResult.NameDuplicated2:
                    msg = "已存在相同编码，请输入其他“资源功能编码”。";
                    break;
            }
            ltMessage.Text = msg;
        }

        protected void btnEmpty_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath + "?id=" + Request.QueryString["Id"]);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("ZYGNManage.aspx");
        }
    }
}