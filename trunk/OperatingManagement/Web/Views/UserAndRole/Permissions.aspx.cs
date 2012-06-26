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
                throw (new AspNetException("权限列表页面初始化出现异常，异常原因", ex));
            }
            finally { }
        }

        private void BindDataSource(List<DataAccessLayer.System.Module> modules)
        {
            cpPager.DataSource = modules;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpRermissions;
            if (modules.Count > this.SiteSetting.PageSize)
                cpPager.Visible = true;
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
                throw (new AspNetException("权限搜索过程中出现异常，异常原因", ex));
            }
            finally { }
        }
    }
}