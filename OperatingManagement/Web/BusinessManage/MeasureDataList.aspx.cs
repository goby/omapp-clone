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
using System.Web.Security;
using System.Data;

namespace OperatingManagement.Web.BusinessManage
{
    public partial class MeasureDataList : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "MeasureData.List";
            this.ShortTitle = "查看测角测速测距数据";
            this.SetTitle();
            //this.AddJavaScriptInclude("scripts/pages/");
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string dataType = ddlType.SelectedValue;
            BindGridView(dataType);
        }

        //绑定列表
        void BindGridView(string dtype)
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                startDate = Convert.ToDateTime(txtStartDate.Text);
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                endDate = Convert.ToDateTime(txtEndDate);
            }
            DataSet objDs = new DataSet();
            switch (dtype)
            {
                case "tb_ae":
                    objDs = (new AE()).GetListByDate(startDate, endDate);
                    break;
                case "tb_rr":
                    objDs = (new RR()).GetListByDate(startDate, endDate);
                    break;
                case "tb_r":
                    objDs = (new R()).GetListByDate(startDate, endDate);
                    break;
            }
            gvList.DataSource = objDs;
            gvList.DataBind();

        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ("ShowDetail" == e.CommandName)
            {
                int idx = Int32.Parse(e.CommandArgument.ToString());
                int ID = Convert.ToInt32(gvList.DataKeys[idx][0]);
                string dataType = ddlType.SelectedValue;

                Response.Redirect(string.Format("MeasureDataDetail.aspx?id={0}&dtype={1}", ID,dataType));
            }
        }
        //
    }
}