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

namespace OperatingManagement.Web.PlanManage
{
    public partial class YDSJDetail : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.QueryStringObserver("ydsjid"))
                {
                    //string sID = this.DecryptString(Request.QueryString["ydsjid"]);
                    string sID = Request.QueryString["ydsjid"];
                    int id = 0;
                    Int32.TryParse(sID, out id);
                    BindFileInfo();
                }
            }
        }

        void BindFileInfo()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(HttpContext.Current.Server.MapPath("~/file/test.xml"));
            txtContent.Text = xml.InnerText;
        }

        public override void OnPageLoaded()
        {
            this.ShortTitle = "引导数据明细";
            base.OnPageLoaded();
        }
    }
}