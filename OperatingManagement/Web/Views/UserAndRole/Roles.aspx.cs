using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.Views.UserAndRole
{
    public partial class Roles : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                BindUsers();
        }
        void BindUsers()
        {
            DataAccessLayer.System.Role r = new DataAccessLayer.System.Role();
            List<DataAccessLayer.System.Role> roles = r.SelectAll();
            cpPager.DataSource = roles;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpRoles;
            rpRoles.DataSource = cpPager.DataSourcePaged;
            rpRoles.DataBind();
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "RoleManage.List";
            this.ShortTitle = "角色列表";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/roles.aspx.js");
        }
    }
}