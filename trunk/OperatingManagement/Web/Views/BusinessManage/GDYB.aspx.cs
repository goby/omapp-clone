using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServiceBusAPI;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.Framework.Storage;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer.BusinessManage;
using ServicesKernel.GDFX;
using ServicesKernel;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class GDYB : AspNetPage
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_GDYB.Caculate";
            this.ShortTitle = "轨道预报";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/BusinessManage/GDYB.aspx.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtFrom.Attributes.Add("readonly", "true");
                ucSatellite1.AllowBlankItem = false;
                InitPage();
            }
        }

        private void InitPage()
        {
            // Bind cblXyxs DataSource
            cblXyxs1.Items.Clear();
            //rblDMZ.Items.Clear();
            //只选出DMZ
            cblXyxs1.DataSource = new XYXSInfo().Cache.Where(a => a.Type == 0).ToList();
            cblXyxs1.DataTextField = "ADDRName";
            cblXyxs1.DataValueField = "INCODE";
            cblXyxs1.DataBind();
            cblXyxs1.SelectedIndex = 0;
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            ObsPrer oPrer = new ObsPrer();
            string strResult = string.Empty;
            string strFullName = string.Empty;
            string resultPath = string.Empty;
            DateTime dt = DateTime.Parse(txtFrom.Text);
            int preDays = int.Parse(txtDays.Text.Trim());
            int interval = int.Parse(txtTimeSpan.Text.Trim());
            bool qcy = false;
            if (rb1.Checked)
                qcy = true;
            int qc = 0;
            List<string> dmzids = new List<string>();
            #region 检查各项条件
            foreach (ListItem item in cblXyxs1.Items)
            {
                if (item.Selected)
                    dmzids.Add(item.Value);
            }
            if (dmzids.Count() == 0)
            {
                ShowMessage("请选择至少一个地面站。");
                return;
            }
            if (qcy)
            {
                if (txtQC.Text.Trim() == "")
                {
                    ShowMessage("请设置圈次。");
                    return;
                }
                try
                {
                    qc = int.Parse(txtQC.Text.Trim());
                }
                catch (Exception ex)
                {
                    ShowMessage("请设置合法的圈次（整数）。");
                    return;
                }
            }
            #endregion

            strResult = oPrer.DoCaculate(dt, preDays, interval, ucSatellite1.SelectedValue
                , dmzids.ToArray(), qcy, qc, out resultPath);
            if (!string.IsNullOrEmpty(strResult))
            {
                ShowMessage(strResult);
                return;
            }
            else
            {
                ShowMessage("轨道预报成功，结果路径" + resultPath);
                string strFName = string.Empty;
                for (int i = 0; i < oPrer.ResultFileNames.Length; i++)
                {
                    strFName = oPrer.ResultFileNames[i];
                    strFName = strFName.Substring(strFName.LastIndexOf(@"\") + 1);
                    if (strFName.Substring(0, 5).ToUpper() == "MAPJ_")
                    {
                        strFullName = Path.Combine(Param.GDYBResultFilePath, resultPath.Substring(resultPath.LastIndexOf(@"\") + 1), strFName);
                        break;
                    }
                }
            }
        }

        private void ShowMessage(string msg)
        {
            ltMessage.Text = msg;
            ltMessage.Visible = true;
        }
    }
}