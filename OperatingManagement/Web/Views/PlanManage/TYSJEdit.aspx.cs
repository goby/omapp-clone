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
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class TYSJEdit : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    string sID = Request.QueryString["id"];
                    HfID.Value = sID;
                    BindJhTable(sID);
                    BindXML();

                    if ("detail" == Request.QueryString["op"])
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script type='text/javascript'>hideAllButton();</script>");
                    }
                }
            }
        }
        private void BindJhTable(string sID)
        {
            List<JH> jh = (new JH()).SelectByIDS(sID);
            txtPlanStartTime.Text = jh[0].StartTime.ToString("yyyy-MM-dd HH:mm");
            txtPlanEndTime.Text = jh[0].EndTime.ToString("yyyy-MM-dd HH:mm");
            HfFileIndex.Value = jh[0].FileIndex;
            hfTaskID.Value = jh[0].TaskID.ToString();
            string[] strTemp = jh[0].FileIndex.Split('_');
            if (strTemp.Length >= 2)
            {
                hfSatID.Value = strTemp[strTemp.Length - 2];
            }
            if (DateTime.Now > jh[0].StartTime)
            {
                btnSubmit.Visible = false;
                hfOverDate.Value = "true";
            }
        }
        private void BindXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HfFileIndex.Value);
            XmlNode root = xmlDoc.SelectSingleNode("仿真推演试验数据/SatName");
            txtSatName.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("仿真推演试验数据/Type");
            txtType.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("仿真推演试验数据/TestItem");
            txtTestItem.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("仿真推演试验数据/StartTime");
            txtStartTime.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("仿真推演试验数据/EndTime");
            txtEndTime.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("仿真推演试验数据/Condition");
            txtCondition.Text = root.InnerText;

        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
            this.AddJavaScriptInclude("scripts/pages/TYSJEdit.aspx.js");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TYSJ objTYSJ = new TYSJ();
            objTYSJ.SatName = txtSatName.Text;
            objTYSJ.Type = txtType.Text;
            objTYSJ.TestItem = txtTestItem.Text;
            objTYSJ.StartTime = txtStartTime.Text;
            objTYSJ.EndTime = txtEndTime.Text;
            objTYSJ.Condition = txtCondition.Text;

            CreatePlanFile creater = new CreatePlanFile();
            if (hfOverDate.Value == "true")
            {
                objTYSJ.TaskID = hfTaskID.Value;
                objTYSJ.SatID = hfSatID.Value;
                string filepath = creater.CreateTYSJFile(objTYSJ, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                {
                    TaskID = objTYSJ.TaskID,
                    PlanType = "TYSJ",
                    PlanID = 0,
                    StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                    EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = objTYSJ.SatID,
                    Reserve = ""
                };
                var result = jh.Add();
            }
            else
            {
                creater.FilePath = HfFileIndex.Value;
                creater.CreateTYSJFile(objTYSJ, 1);
            }

            ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>alert('计划保存成功');</script>");
        }
    }
}