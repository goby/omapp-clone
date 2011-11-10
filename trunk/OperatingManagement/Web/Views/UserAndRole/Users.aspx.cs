using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.Views.UserAndRole
{
    public partial class Users : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindUsers();
        }
        void BindUsers()
        {
            DataAccessLayer.System.User u = new DataAccessLayer.System.User();
            List<DataAccessLayer.System.User> users = u.SelectAll();
            cpPager.DataSource = users;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpUsers;
            rpUsers.DataSource = cpPager.DataSourcePaged;
            rpUsers.DataBind();
        }
        protected bool IsAdminOrCurrent(object loginName, object userType)
        {
            bool isMyself = loginName.ToString().Equals(Profile.UserName, StringComparison.InvariantCultureIgnoreCase);
            
            bool isAdmin = userType.ToString().Equals(Framework.UserType.Admin.ToString(), StringComparison.InvariantCultureIgnoreCase);
            if (isMyself)
            {
                if (isAdmin) return false;
                else return true;
            }
            else
            {
                return isAdmin;
            }
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "UserManage.List";
            this.ShortTitle = "用户列表";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/users.aspx.js");
        }
    }
}