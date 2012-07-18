using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicesKernel.GDFX;
using OperatingManagement.WebKernel.Basic;
using System.Web.UI.DataVisualization.Charting;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class GDJSCurves : AspNetPage
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_GDYB.Caculate,OMB_JHFX.Caculate,OMB_JHYB.Caculate";
            this.ShortTitle = "查看轨道计算结果";
            this.SetTitle();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["fp"] != null)
                {
                    lbPath.Text = Request.QueryString["fp"];
                    this.ViewState["filepath"] = Request.QueryString["fp"];
                    this.ViewState["filetype"] = Request.QueryString["ft"];
                }
                InitDdlResultType();
                InitDdlDataType();
            }
        }

        private void InitDdlResultType()
        {
            ddlResultType.Items.Clear();
            ddlResultType.DataSource = FormatXMLConfig.Results;
            ddlResultType.DataTextField = "displayname";
            ddlResultType.DataValueField = "name";
            ddlResultType.DataBind();
            if (this.ViewState["filetype"] != null)
                ddlResultType.SelectedValue = this.ViewState["filetype"].ToString();
        }

        private void InitDdlDataType()
        {
            ddlDataType.Items.Clear();
            ResultType oRType = FormatXMLConfig.GetTypeByName(ddlResultType.SelectedValue);
            if (oRType != null && oRType.Results != null)
            {
                ddlDataType.Enabled = true;
                ddlDataType.DataSource = oRType.Results;
                ddlDataType.DataTextField = "name";
                ddlDataType.DataValueField = "name";
                ddlDataType.DataBind();
            }
            else
                ddlDataType.Enabled = false;
        }

        private void InitChart()
        {
            string resultType = "GDYB_MapJ";
            string dataType = "X";
            List<DateTime> lstDate;
            List<double> lstDDatas;
            List<int> lstIDatas;
            double maxValue;
            double minValue;
            int dataCount;
            string strResult = string.Empty;
            ResultLoader oRLoader = new ResultLoader();
            strResult = oRLoader.LoadResultFile(@"D:\files\GDFX\ObsPre\Output\", resultType, dataType, true, out lstDate, out lstDDatas
                , out lstIDatas, out maxValue, out minValue, out dataCount);
            ChartCurve.ChartAreas[0].AxisX.Maximum = lstDate[lstDate.Count() - 1].ToOADate();
            ChartCurve.ChartAreas[0].AxisX.Minimum = lstDate[0].ToOADate();
            ChartCurve.ChartAreas[0].AxisY.Maximum = maxValue;
            ChartCurve.ChartAreas[0].AxisY.Minimum = minValue;
            ChartCurve.ChartAreas[0].AxisX.Interval = 0;
            ChartCurve.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            ChartCurve.Series["Series1"].Points.DataBindXY(lstDate, lstDDatas);
        }

        protected void btnCurve_Click(object sender, EventArgs e)
        {
            InitChart();
        }

        protected void btnCurve2_Click(object sender, EventArgs e)
        {

        }

        protected void CurveChart_PreRender(object sender, EventArgs e)
        {
        }

        protected void ddlResultType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitDdlDataType();
        }
    }
}