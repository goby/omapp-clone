using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.Views.UserAndRole
{
    public partial class UserRoleEdit : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRole();
                BindUser();
            }
        }
        void BindUser() {
            DataAccessLayer.System.User u = new DataAccessLayer.System.User()
            {
                Id = Convert.ToDouble(Request.QueryString["Id"])
            };
            var user = u.SelectById();
            ltUserName.Text = user.DisplayName;
            var roles = u.SelectRolesById();
            string strRoles = string.Empty;
            if (roles.Count > 0)
            {
                foreach (var r in roles)
                {
                    strRoles += "[" + r.Id.ToString() + "]";
                }
            }
            hfRoles.Value = strRoles;
        }
        void BindRole()
        {
            DataAccessLayer.System.Role r = new DataAccessLayer.System.Role();
            var roles = r.SelectAll();
            rpRoles.DataSource = roles;
            rpRoles.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataAccessLayer.System.User u = new DataAccessLayer.System.User()
            {
                Id = Convert.ToDouble(Request.QueryString["Id"])
            };
            string[] roles = hfRoles.Value.Split(new string[] { "][" }, StringSplitOptions.RemoveEmptyEntries);
            string strRoles = string.Empty;
            if (roles.Length > 0)
            {
                foreach (var s in roles)
                {
                    strRoles += s.TrimStart('[').TrimEnd(']') + ",";
                }
                if (strRoles != string.Empty)
                {
                    strRoles = strRoles.Substring(0, strRoles.Length - 1);
                }
            }
            bool result = u.AddToRoles(strRoles);
            string msg = string.Empty;
            if (result)
            {
                msg = "已成功为指定用户分配角色。";
            }
            else
            {
                msg = "发生了数据错误，无法完成请求的操作。";
            }
            ltMessage.Text = msg;
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMUserManage.Add,OMUserManage.Edit";
            this.ShortTitle = "分配角色";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/usernrole/userroleedit.aspx.js");
        }

    }
}