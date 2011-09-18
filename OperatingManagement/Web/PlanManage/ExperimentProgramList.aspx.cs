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
using OperatingManagement.DataAccessLayer.PlanManage;
using System.Web.Security;
using System.Data;


namespace OperatingManagement.Web.PlanManage
{
    public partial class ExperimentProgramList : AspNetPage, IRouteContext
    {
        public override void OnPageLoaded()
        {
            this.ShortTitle = "试验程序列表";
            this.SetTitle();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            BindGridView();
        }

        //绑定列表
        void BindGridView()
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            DataSet objDs = new DataSet(); 
            objDs = (new SYCX()).GetSYCXListByDate(startDate,endDate);
            gvList.DataSource = objDs;
            gvList.DataBind();
        }


    }
}