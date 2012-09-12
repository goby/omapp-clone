using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer;
using OperatingManagement.Framework;
using OperatingManagement.DataAccessLayer.BusinessManage;
using ServicesKernel.File;


namespace OperatingManagement.Web.Views.BusinessManage.BDManage
{
    public partial class SatManage : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitialPageData();
            cpPager.PostBackPage += new EventHandler(cpPager_PostBackPage);
        }

        private void InitialPageData()
        {
            try
            {
                DataAccessLayer.BusinessManage.Satellite s = new DataAccessLayer.BusinessManage.Satellite();
                List<DataAccessLayer.BusinessManage.Satellite> satellites = s.SelectAll();
                BindSats(satellites);
            }
            catch (Exception ex)
            {
                throw (new AspNetException("任务列表页面初始化出现异常，异常原因", ex));
            }
            finally { }
        }

        void BindSats(List<DataAccessLayer.BusinessManage.Satellite> satellites)
        {
            cpPager.DataSource = satellites;
            cpPager.PageSize = this.SiteSetting.PageSize;
            if (satellites.Count > this.SiteSetting.PageSize)
                cpPager.Visible = true;
            cpPager.BindToControl = rpTasks;
            rpTasks.DataSource = cpPager.DataSourcePaged;
            rpTasks.DataBind();
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_SatMan.View";
            this.ShortTitle = "卫星列表";
            this.SetTitle();
        }

        protected void cpPager_PostBackPage(object sender, EventArgs e)
        {
            SearchSats(false);
        }

        private void SearchSats(bool fromSearch)
        {
            DataAccessLayer.BusinessManage.Satellite t = new DataAccessLayer.BusinessManage.Satellite();
            List<DataAccessLayer.BusinessManage.Satellite> satellites = null;
            try
            {
                satellites = t.SelectAll();
                BindSats(satellites);
            }
            catch (Exception ex)
            {
                throw (new AspNetException("卫星列表页面翻页出现异常，异常原因", ex));
            }
            finally { }
        }
    }
}