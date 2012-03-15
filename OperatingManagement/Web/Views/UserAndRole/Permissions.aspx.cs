using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.Views.UserAndRole
{
    public partial class Permissions : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitialPageData();
        }

        private void InitialPageData()
        {
            try
            {
                DataAccessLayer.System.Module r = new DataAccessLayer.System.Module();
                List<DataAccessLayer.System.Module> modules = r.SelectAll();
                BindDataSource(modules);
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void BindDataSource(List<DataAccessLayer.System.Module> modules)
        {
            cpPager.DataSource = modules;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpRermissions;
            rpRermissions.DataSource = cpPager.DataSourcePaged;
            rpRermissions.DataBind();
        }
        
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMRermissionManage.View";
            this.ShortTitle = "权限列表";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/usernrole/permissions.aspx.js");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataAccessLayer.System.Module r = new DataAccessLayer.System.Module();
            List<DataAccessLayer.System.Module> modules = null;
            try
            {
                if (txtKeyword.Text != string.Empty)
                    modules = r.Search(txtKeyword.Text);
                else
                    modules = r.SelectAll();
                BindDataSource(modules);
            }
            catch (Exception ex)
            {
            }
            finally { }
        }
    }
}