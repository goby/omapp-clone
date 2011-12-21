using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class OrbitParaCalculate : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "OrbitParaCal.Edit";
            this.ShortTitle = "参数转换";
            this.SetTitle();
        }
    }
}