using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.Framework.Core;

namespace OperatingManagement.Web.Views.UserAndRole
{
    public partial class Roles : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                BindRolers();
        }
        void BindRolers()
        {
            DataAccessLayer.System.Role r = new DataAccessLayer.System.Role();
            List<DataAccessLayer.System.Role> roles;
            try
            {
                roles = r.SelectAll();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("角色列表页面初始化出现异常，异常原因", ex));
            }
            finally { }
            cpPager.DataSource = roles;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpRoles;
            rpRoles.DataSource = cpPager.DataSourcePaged;
            rpRoles.DataBind();
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMRoleManage.View";
            this.ShortTitle = "角色列表";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/usernrole/roles.aspx.js");
        }
    }
}