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
    public partial class XiAnCeKongDataList : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "XiAnCeKongData.List";
            this.ShortTitle = "查看西安卫星测控中心数据";
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
                case "tb_gdxa":
                    objDs = (new GDXA()).GetListByDate(startDate, endDate);
                    break;
                case "tb_gdsh":
                    objDs = (new GDSH()).GetListByDate(startDate, endDate);
                    break;
                case "tb_xdsc":
                    objDs = (new XDSC()).GetListByDate(startDate, endDate);
                    break;
                case "tb_T0":
                    objDs = (new T0()).GetListByDate(startDate, endDate);
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

                Response.Redirect(string.Format("XiAnCeKongDataDetail.aspx?id={0}&dtype={1}", ID,dataType));
            }
        }
        //

    }
}