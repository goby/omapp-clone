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
using OperatingManagement.DataAccessLayer.PlanManage;
using OperatingManagement.Framework;
using System.Web.Security;
using System.Xml;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class PlanEdit : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlYJJH.Visible = false;
                pnlXXXQ.Visible = false;
                pnlGZJH.Visible = false;
                pnlTYSJ.Visible = false;
                pnl4.Visible = false;
               // if (this.QueryStringObserver("planID") && this.QueryStringObserver("infotype"))
                if (!string.IsNullOrEmpty(Request.QueryString["planid"]) && !string.IsNullOrEmpty(Request.QueryString["infotype"]))
                {
                    //string sID = this.DecryptString(Request.QueryString["roleID"]);
                    hfinfotype.Value = Request.QueryString["infotype"].ToUpper();
                    HfID.Value = Request.QueryString["planid"];
                    string sID = Request.QueryString["planid"];
                    int id = 0;
                    Int32.TryParse(sID, out id);
                    BindFileInfo();
                }
            }
        }

        void BindFileInfo()
        {
            //XmlDocument xml = new XmlDocument();
            //xml.Load(HttpContext.Current.Server.MapPath("~/file/test.xml"));
            //txtContent.Text = xml.InnerText;

            switch (hfinfotype.Value)
            { 
                case "YJJH":
                    pnlYJJH.Visible = true;
                    YJJH objYJJH = (new YJJH()).SelectById(Convert.ToInt32(HfID.Value));
                    ltPlanType.Text = objYJJH.InfoType;
                    txtSourceYJJH.Text = objYJJH.Source;
                    txtDesYJJH.Text = objYJJH.Destination;
                    txtFormatYJJH.Text = objYJJH.Format1;
                    txtTaskIDYJJH.Text = objYJJH.TaskID;
                    txtLinecountYJJH.Text = objYJJH.LineCount.ToString();
                    txtDataYJJH.Text = objYJJH.DataSection;
                    txtNoteYJJH.Text = objYJJH.Reserve;
                    break;
                case "XXXQ":
                    pnlXXXQ.Visible = true;
                    XXXQ objXXXQ = (new XXXQ()).SelectById(Convert.ToInt32(HfID.Value));
                    ltinfoTypeXXXQ.Text = objXXXQ.InfoType;
                    txtSourceXXXQ.Text = objXXXQ.Source;
                    txtDesXXXQ.Text = objXXXQ.Destination;
                    txtFormat1XXXQ.Text = objXXXQ.Format1;
                    txtFormat2XXXQ.Text = objXXXQ.Format2;
                    txtTaskIDXXXQ.Text = objXXXQ.TaskID;
                    txtLineCountXXXQ.Text = objXXXQ.LineCount.ToString();
                    txtDataXXXQ.Text = objXXXQ.DataSection;
                    txtNoteXXXQ.Text = objXXXQ.Reserve;
                    break;
                case "GZJH":
                    pnlGZJH.Visible = true;
                    GZJH objGZJH = (new GZJH()).SelectById(Convert.ToInt32(HfID.Value));
                    ltinfotypeGZJH.Text = objGZJH.InfoType;
                    txtSourceGZJH.Text = objGZJH.Source;
                    txtDesGZJH.Text = objGZJH.Destination;
                    txtFormat1GZJH.Text = objGZJH.Format1;
                    txtFormat2GZJH.Text = objGZJH.Format2;
                    txtTaskidGZJH.Text = objGZJH.TaskID;
                    txtLineCountGZJH.Text = objGZJH.LineCount.ToString();
                    txtDataGZJH.Text = objGZJH.DataSection;
                    txtNoteGZJH.Text = objGZJH.Reserve;
                    break;
                case "TYSJ":
                    pnlTYSJ.Visible = true;
                    TYSJ objTYSJ = (new TYSJ()).SelectById(Convert.ToInt32(HfID.Value));
                    ltinfotypeTYSJ.Text = objTYSJ.InfoType;
                    txtSourceTYSJ.Text = objTYSJ.Source;
                    txtDesTYSJ.Text = objTYSJ.Destination;
                    txtFormat1TYSJ.Text = objTYSJ.Format1;
                    txtTaskidTYSJ.Text = objTYSJ.TaskID;
                    txtLineCountTYSJ.Text = objTYSJ.LineCount.ToString();
                    txtDataTYSJ.Text = objTYSJ.DataSection;
                    txtNoteTYSJ.Text = objTYSJ.Reserve;
                    break;
                default:
                    pnl4.Visible = true;
                    break;
            }
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
            this.AddJavaScriptInclude("scripts/pages/PlanEdit.aspx.js");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            YJJH objYJJH = new YJJH();
            objYJJH.ID = Convert.ToInt32( HfID.Value);
            objYJJH.Source = txtSourceYJJH.Text.Trim();
            objYJJH.Destination = txtDesYJJH.Text.Trim();
            objYJJH.TaskID = txtTaskIDYJJH.Text.Trim();
            objYJJH.InfoType = ltPlanType.Text;
            objYJJH.Format1 = txtFormatYJJH.Text.Trim();
            objYJJH.LineCount = Convert.ToInt32(txtLinecountYJJH.Text);
            objYJJH.DataSection = txtDataYJJH.Text;
            objYJJH.Reserve = txtNoteYJJH.Text;

            var result = objYJJH.Update();
            JH objjh = new JH();
            objjh = objjh.SelectByPlanTypeAndPlanID("YJJH", Convert.ToInt32(HfID.Value));
            objjh.TaskID = txtTaskIDYJJH.Text.Trim();
            if (!string.IsNullOrEmpty(txtStartTimeYJJH.Text.Trim()))
            {
                objjh.StartTime = Convert.ToDateTime(txtStartTimeYJJH.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndTimeYJJH.Text.Trim()))
            {
                objjh.EndTime = Convert.ToDateTime(txtEndTimeYJJH.Text.Trim());
            }
            objjh.Update();
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>showMsgError();</script>");
                    break;
                case Framework.FieldVerifyResult.Success:
                    ClientScript.RegisterStartupScript(this.GetType(), "success", "<script type='text/javascript'>showMsgSuccess();</script>");
                    break;
            }
        }

        protected void btnXXXQ_Click(object sender, EventArgs e)
        {
            XXXQ objXXXQ = new XXXQ();
            objXXXQ.ID = Convert.ToInt32(HfID.Value);
            objXXXQ.Source = txtSourceXXXQ.Text.Trim();
            objXXXQ.Destination = txtDesXXXQ.Text.Trim();
            objXXXQ.TaskID = txtTaskIDXXXQ.Text.Trim();
            objXXXQ.InfoType = ltinfoTypeXXXQ.Text;
            objXXXQ.Format1 = txtFormat1XXXQ.Text.Trim();
            objXXXQ.Format2 = txtFormat2XXXQ.Text.Trim();
            objXXXQ.LineCount = Convert.ToInt32(txtLineCountXXXQ.Text);
            objXXXQ.DataSection = txtDataXXXQ.Text;
            objXXXQ.Reserve = txtNoteXXXQ.Text;

            var result= objXXXQ.Update();
            JH objjh = new JH();
            objjh = objjh.SelectByPlanTypeAndPlanID("XXXQ", Convert.ToInt32(HfID.Value));
            objjh.TaskID = txtTaskIDXXXQ.Text.Trim();
            if (!string.IsNullOrEmpty(txtStartTimeXXXQ.Text.Trim()))
            {
                objjh.StartTime = Convert.ToDateTime(txtStartTimeXXXQ.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndTimeXXXQ.Text.Trim()))
            {
                objjh.EndTime = Convert.ToDateTime(txtEndTimeXXXQ.Text.Trim());
            }
            objjh.Update();
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>showMsgError();</script>");
                    break;
                case Framework.FieldVerifyResult.Success:
                    ClientScript.RegisterStartupScript(this.GetType(), "success", "<script type='text/javascript'>showMsgSuccess();</script>");
                    break;
            }
        }

        protected void btnGZJH_Click(object sender, EventArgs e)
        {
            GZJH objGZJH = new GZJH();
            objGZJH.ID = Convert.ToInt32(HfID.Value);
            objGZJH.Source = txtSourceGZJH.Text.Trim();
            objGZJH.Destination = txtDesGZJH.Text.Trim();
            objGZJH.TaskID = txtTaskidGZJH.Text.Trim();
            objGZJH.InfoType = ltinfotypeGZJH.Text;
            objGZJH.Format1 = txtFormat1GZJH.Text.Trim();
            objGZJH.Format2 = txtFormat2GZJH.Text.Trim();
            objGZJH.LineCount = Convert.ToInt32(txtLineCountGZJH.Text);
            objGZJH.DataSection = txtDataGZJH.Text;
            objGZJH.Reserve = txtNoteGZJH.Text;

            var result = objGZJH.Update();
            JH objjh = new JH();
            objjh = objjh.SelectByPlanTypeAndPlanID("GZJH", Convert.ToInt32(HfID.Value));
            objjh.TaskID = txtTaskidGZJH.Text.Trim();
            if (!string.IsNullOrEmpty(txtStartTimeGZJH.Text.Trim()))
            {
                objjh.StartTime = Convert.ToDateTime(txtStartTimeGZJH.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndTimeGZJH.Text.Trim()))
            {
                objjh.EndTime = Convert.ToDateTime(txtEndTimeGZJH.Text.Trim());
            }
            objjh.Update();
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>showMsgError();</script>");
                    break;
                case Framework.FieldVerifyResult.Success:
                    ClientScript.RegisterStartupScript(this.GetType(), "success", "<script type='text/javascript'>showMsgSuccess();</script>");
                    break;
            }
        }

        protected void txtTYSJ_Click(object sender, EventArgs e)
        {
            TYSJ objTYSJ = new TYSJ();
            objTYSJ.ID = Convert.ToInt32(HfID.Value);
            objTYSJ.Source = txtSourceTYSJ.Text.Trim();
            objTYSJ.Destination = txtDesTYSJ.Text.Trim();
            objTYSJ.TaskID = txtTaskidTYSJ.Text.Trim();
            objTYSJ.InfoType = ltinfotypeTYSJ.Text;
            objTYSJ.Format1 = txtFormat1TYSJ.Text.Trim();
            objTYSJ.LineCount = Convert.ToInt32(txtLineCountTYSJ.Text);
            objTYSJ.DataSection = txtDataTYSJ.Text;
            objTYSJ.Reserve = txtNoteTYSJ.Text;

            var result = objTYSJ.Update();
            JH objjh = new JH();
            objjh = objjh.SelectByPlanTypeAndPlanID("TYSJ", Convert.ToInt32(HfID.Value));
            objjh.TaskID = txtTaskidTYSJ.Text.Trim();
            if (!string.IsNullOrEmpty(txtStartTimeTYSJ.Text.Trim()))
            {
                objjh.StartTime = Convert.ToDateTime(txtStartTimeTYSJ.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndTimeTYSJ.Text.Trim()))
            {
                objjh.EndTime = Convert.ToDateTime(txtEndTimeTYSJ.Text.Trim());
            }
            objjh.Update();
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>showMsgError();</script>");
                    break;
                case Framework.FieldVerifyResult.Success:
                    ClientScript.RegisterStartupScript(this.GetType(), "success", "<script type='text/javascript'>showMsgSuccess();</script>");
                    break;
            }
        }

        protected void txtSaveToYJJH_Click(object sender, EventArgs e)
        {
            YJJH objYJJH = new YJJH();
            //objYJJH.ID = Convert.ToInt32(HfID.Value);
            objYJJH.Source = txtSourceYJJH.Text.Trim();
            objYJJH.Destination = txtDesYJJH.Text.Trim();
            objYJJH.TaskID = txtTaskIDYJJH.Text.Trim();
            objYJJH.InfoType = ltPlanType.Text;
            objYJJH.Format1 = txtFormatYJJH.Text.Trim();
            objYJJH.LineCount = Convert.ToInt32(txtLinecountYJJH.Text);
            objYJJH.DataSection = txtDataYJJH.Text;
            objYJJH.Reserve = txtNoteYJJH.Text;

            var result = objYJJH.Add();

            JH objjh = new JH();
            objjh = objjh.SelectByPlanTypeAndPlanID("YJJH", Convert.ToInt32( HfID.Value));
            objjh.PlanID = objYJJH.Id;
            if (!string.IsNullOrEmpty(txtStartTimeYJJH.Text.Trim()))
            {
                objjh.StartTime = Convert.ToDateTime(txtStartTimeYJJH.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndTimeYJJH.Text.Trim()))
            {
                objjh.EndTime = Convert.ToDateTime(txtEndTimeYJJH.Text.Trim());
            }
            objjh.Add();
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>showMsgError();</script>");
                    break;
                case Framework.FieldVerifyResult.Success:
                    ClientScript.RegisterStartupScript(this.GetType(), "success", "<script type='text/javascript'>showMsgSuccess();</script>");
                    break;
            }
        }

        protected void txtSaveToXXXQ_Click(object sender, EventArgs e)
        {
            XXXQ objXXXQ = new XXXQ();
            objXXXQ.Source = txtSourceXXXQ.Text.Trim();
            objXXXQ.Destination = txtDesXXXQ.Text.Trim();
            objXXXQ.TaskID = txtTaskIDXXXQ.Text.Trim();
            objXXXQ.InfoType = ltinfoTypeXXXQ.Text;
            objXXXQ.Format1 = txtFormat1XXXQ.Text.Trim();
            objXXXQ.Format2 = txtFormat2XXXQ.Text.Trim();
            objXXXQ.LineCount = Convert.ToInt32(txtLineCountXXXQ.Text);
            objXXXQ.DataSection = txtDataXXXQ.Text;
            objXXXQ.Reserve = txtNoteXXXQ.Text;

            var result =objXXXQ.Add();

            JH objjh = new JH();
            objjh = objjh.SelectByPlanTypeAndPlanID("XXXQ", Convert.ToInt32(HfID.Value));
            objjh.PlanID = objXXXQ.Id;
            if (!string.IsNullOrEmpty(txtStartTimeXXXQ.Text.Trim()))
            {
                objjh.StartTime = Convert.ToDateTime(txtStartTimeXXXQ.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndTimeXXXQ.Text.Trim()))
            {
                objjh.EndTime = Convert.ToDateTime(txtEndTimeXXXQ.Text.Trim());
            }
            objjh.Add();
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>showMsgError();</script>");
                    break;
                case Framework.FieldVerifyResult.Success:
                    ClientScript.RegisterStartupScript(this.GetType(), "success", "<script type='text/javascript'>showMsgSuccess();</script>");
                    break;
            }
        }

        protected void txtSaveToGZJH_Click(object sender, EventArgs e)
        {
            GZJH objGZJH = new GZJH();
            objGZJH.Source = txtSourceGZJH.Text.Trim();
            objGZJH.Destination = txtDesGZJH.Text.Trim();
            objGZJH.TaskID = txtTaskidGZJH.Text.Trim();
            objGZJH.InfoType = ltinfotypeGZJH.Text;
            objGZJH.Format1 = txtFormat1GZJH.Text.Trim();
            objGZJH.Format2 = txtFormat2GZJH.Text.Trim();
            objGZJH.LineCount = Convert.ToInt32(txtLineCountGZJH.Text);
            objGZJH.DataSection = txtDataGZJH.Text;
            objGZJH.Reserve = txtNoteGZJH.Text;

            var result = objGZJH.Add();
            JH objjh = new JH();
            objjh = objjh.SelectByPlanTypeAndPlanID("GZJH", Convert.ToInt32(HfID.Value));
            objjh.PlanID = objGZJH.Id;
            if (!string.IsNullOrEmpty(txtStartTimeGZJH.Text.Trim()))
            {
                objjh.StartTime = Convert.ToDateTime(txtStartTimeGZJH.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndTimeGZJH.Text.Trim()))
            {
                objjh.EndTime = Convert.ToDateTime(txtEndTimeGZJH.Text.Trim());
            }
            objjh.Add();
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>showMsgError();</script>");
                    break;
                case Framework.FieldVerifyResult.Success:
                    ClientScript.RegisterStartupScript(this.GetType(), "success", "<script type='text/javascript'>showMsgSuccess();</script>");
                    break;
            }
        }

        protected void txtSaveToTYSJ_Click(object sender, EventArgs e)
        {
            TYSJ objTYSJ = new TYSJ();
            objTYSJ.Source = txtSourceTYSJ.Text.Trim();
            objTYSJ.Destination = txtDesTYSJ.Text.Trim();
            objTYSJ.TaskID = txtTaskidTYSJ.Text.Trim();
            objTYSJ.InfoType = ltinfotypeTYSJ.Text;
            objTYSJ.Format1 = txtFormat1TYSJ.Text.Trim();
            objTYSJ.LineCount = Convert.ToInt32(txtLineCountTYSJ.Text);
            objTYSJ.DataSection = txtDataTYSJ.Text;
            objTYSJ.Reserve = txtNoteTYSJ.Text;

            var result =objTYSJ.Add();
            JH objjh = new JH();
            objjh = objjh.SelectByPlanTypeAndPlanID("TYSJ", Convert.ToInt32(HfID.Value));
            objjh.PlanID = objTYSJ.Id;
            if (!string.IsNullOrEmpty(txtStartTimeTYSJ.Text.Trim()))
            {
                objjh.StartTime = Convert.ToDateTime(txtStartTimeTYSJ.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndTimeTYSJ.Text.Trim()))
            {
                objjh.EndTime = Convert.ToDateTime(txtEndTimeTYSJ.Text.Trim());
            }
            objjh.Add();
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>showMsgError();</script>");
                    break;
                case Framework.FieldVerifyResult.Success:
                    ClientScript.RegisterStartupScript(this.GetType(), "success", "<script type='text/javascript'>showMsgSuccess();</script>");
                    break;
            }
        }
    }
}