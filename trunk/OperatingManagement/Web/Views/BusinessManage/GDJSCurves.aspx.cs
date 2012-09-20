using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.DataVisualization.Charting;
using ServicesKernel.GDFX;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer.BusinessManage;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class GDJSCurves : AspNetPage
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_GDYB.Caculate,OMB_JHFX.Caculate,OMB_JHYB.Caculate";
            this.ShortTitle = "查看轨道计算结果";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/BusinessManage/GDJSCurves.aspx.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["fp"] != null)
                {
                    txtPath.Text = Request.QueryString["fp"];
                    this.ViewState["filepath"] = Request.QueryString["fp"];
                    this.ViewState["filetype"] = Request.QueryString["ft"];
                }
            }
        }

        private void InitChart(string filePath)
        {
            string resultType = hdResultType.Value;//"GDYB_MapJ";
            string dataType = hdDataType.Value;
            List<DateTime> lstDate;
            List<double> lstDDatas;
            List<int> lstIDatas;
            double maxValue;
            double minValue;
            int dataCount;
            string strResult = string.Empty;
            ResultLoader oRLoader = new ResultLoader();
            strResult = oRLoader.LoadResultFile(filePath, resultType, dataType, true, out lstDate, out lstDDatas
                , out lstIDatas, out maxValue, out minValue, out dataCount);
            if (!string.IsNullOrEmpty(strResult))
            {
                HideMessage();
                ChartCurve.ChartAreas[0].AxisX.Maximum = lstDate[lstDate.Count() - 1].ToOADate();
                ChartCurve.ChartAreas[0].AxisX.Minimum = lstDate[0].ToOADate();
                ChartCurve.ChartAreas[0].AxisY.Maximum = maxValue;
                ChartCurve.ChartAreas[0].AxisY.Minimum = minValue;
                ChartCurve.ChartAreas[0].AxisX.Interval = 0;
                ChartCurve.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                ChartCurve.Series["Series1"].Points.DataBindXY(lstDate, lstDDatas);
            }
            else
                ShowMessage(strResult);
        }

        private void ShowMessage(string msg)
        {
            trMessage.Visible = true;
            lbMessage.Text = msg;
        }

        private void HideMessage()
        {
            lbMessage.Text = "";
            trMessage.Visible = false;
        }

        protected void btnCurve_Click(object sender, EventArgs e)
        {
            if (!fuPath.HasFile)
            {
                ShowMessage("结果文件不能为空");
                return;
            }

            if (!Path.GetExtension(fuPath.FileName).Trim('.').Equals("dat",
                StringComparison.InvariantCultureIgnoreCase))
            {
                ShowMessage("“结果文件”格式只能为dat。");
                return;
            }
            try
            {
                string filePath = SystemParameters.GetSystemParameterValue(SystemParametersType.GDJSResult, "result_path")
                    + SystemParameters.GetSystemParameterValue(SystemParametersType.GDJSResult, "upload_path");
                string fileNewPath = Path.Combine(filePath, fuPath.FileName);
                if (File.Exists(fileNewPath))
                    File.Delete(fileNewPath);
                fuPath.SaveAs(fileNewPath);
                InitChart(fileNewPath);
            }
            catch (Exception ex)
            {
                throw (new AspNetException("轨道分析 - 展示结果页面出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void btnCurve2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPath.Text.Trim()))
                InitChart(txtPath.Text.Trim());
        }

        protected void CurveChart_PreRender(object sender, EventArgs e)
        {
        }
    }
}