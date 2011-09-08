using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;

namespace OperatingManagement.Web.Account
{
    public partial class Login : AspNetPage,IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public override void OnPageLoaded()
        {
            this.ShortTitle = "用户登录";
            base.OnPageLoaded();
        }
    }
}