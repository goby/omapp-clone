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
using System.Web.Security;
using System.Xml;
using System.IO;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class ExperimentPlanDetail : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.QueryStringObserver("id"))
            {
                string sID = Request.QueryString["id"];
                int id = 0;
                Int32.TryParse(sID, out id);
                id = 1;
                BindFileInfo( id );
            }
        }

        void BindFileInfo(int id)
        {
            DataAccessLayer.PlanManage.SYJH jh = new DataAccessLayer.PlanManage.SYJH{ Id = id };
            DataAccessLayer.PlanManage.SYJH obj = jh.SelectById();
            //XmlDocument xml = new XmlDocument();
            //xml.Load(HttpContext.Current.Server.MapPath(obj.FileIndex));
            StreamReader sr = new StreamReader(obj.FileIndex,System.Text.Encoding.Default);
            txtContent.Text= sr.ReadToEnd();
            //txtContent.Text = xml.InnerText;
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "ExperimentPlan.Detail";
            this.ShortTitle = "实验计划明细";
            base.OnPageLoaded();
        }


    }
}