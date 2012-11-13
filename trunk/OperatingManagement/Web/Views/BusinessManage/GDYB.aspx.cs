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
            rblDMZ.Items.Clear();
            //只选出DMZ
            rblDMZ.DataSource = new XYXSInfo().Cache.Where(a => a.Type == 0).ToList();
            rblDMZ.DataTextField = "ADDRName";
            rblDMZ.DataValueField = "ADDRMARK";
            rblDMZ.DataBind();
            rblDMZ.SelectedIndex = 0;

            //从非kongjian机动任务-GD来的
            string satid = Request.QueryString["satid"];
            if (satid != null)
            {
                ucSatellite1.SelectedIndex = ucSatellite1.Items.IndexOf(ucSatellite1.Items.FindByValue(satid));
                string strDmzid = Request.QueryString["dmzid"];
                if (!string.IsNullOrEmpty(strDmzid))
                {
                    string[] strDMZIDs = strDmzid.Split(new char[]{','});
                    for (int i = 0; i < strDMZIDs.Length; i++)
                    {
                        foreach (ListItem item in rblDMZ.Items)
                        {
                            if (item.Value == strDMZIDs[i])
                                item.Selected = true;
                        }
                    }
                }
            }
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
            int qc = int.Parse(txtQC.Text.Trim());
            
            strResult = oPrer.DoCaculate(dt, preDays, interval, ucSatellite1.SelectedValue
                , rblDMZ.SelectedValue, qcy, qc, out resultPath);
            if (!string.IsNullOrEmpty(strResult))
            {
                ShowMessage(strResult);
                return;
            }
            else
            {
                for (int i = 0; i < oPrer.ResultFileNames.Length; i++)
                {
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