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
    public partial class MBXQEdit : AspNetPage, IRouteContext
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
                }
            }
        }
        private void BindJhTable(string sID)
        {
            List<JH> jh = (new JH()).SelectByIDS(sID);
            txtPlanStartTime.Text = jh[0].StartTime.ToShortTimeString();
            txtPlanEndTime.Text = jh[0].EndTime.ToShortTimeString();
            HfFileIndex.Value = jh[0].FileIndex;
        }
        private void BindXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HfFileIndex.Value);
            XmlNode root = xmlDoc.SelectSingleNode("空间目标信息需求/User");
            txtMBUser.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("空间目标信息需求/Time");
            txtMBTime.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("空间目标信息需求/TargetInfo");
            txtMBTargetInfo.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("空间目标信息需求/TimeSection1");
            txtMBTimeSection1.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("空间目标信息需求/TimeSection2");
            txtMBTimeSection2.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("空间目标信息需求/Sum");
            txtMBSum.Text = root.InnerText;

            root = xmlDoc.SelectSingleNode("空间目标信息需求/卫星");
            List<MBXQSatInfo> list = new List<MBXQSatInfo>();
            MBXQSatInfo sat;
            foreach (XmlNode n in root.ChildNodes)
            {
                sat = new MBXQSatInfo();
                sat.SatName = n["SatName"].InnerText;
                sat.InfoName = n["InfoName"].InnerText;
                sat.InfoTime = n["InfoTime"].InnerText;
                list.Add(sat);
            }
            rpMB.DataSource = list;
            rpMB.DataBind();

        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
            this.AddJavaScriptInclude("scripts/pages/MBXQEdit.aspx.js");
        }

        protected void rpMB_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<MBXQSatInfo> list2 = new List<MBXQSatInfo>();
                MBXQSatInfo dm;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    dm = new MBXQSatInfo();
                    TextBox txtMBSatName = (TextBox)it.FindControl("txtMBSatName");
                    TextBox txtMBInfoName = (TextBox)it.FindControl("txtMBInfoName");
                    TextBox txtMBInfoTime = (TextBox)it.FindControl("txtMBInfoTime");

                    dm.SatName = txtMBSatName.Text;
                    dm.InfoName = txtMBInfoName.Text;
                    dm.InfoTime = txtMBInfoTime.Text;
                    list2.Add(dm);
                }
                dm = new MBXQSatInfo();
                dm.SatName = "";
                dm.InfoName = "";
                dm.InfoTime = "";
                list2.Add(dm);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<MBXQSatInfo> list2 = new List<MBXQSatInfo>();
                MBXQSatInfo dm;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        dm = new MBXQSatInfo();
                        TextBox txtMBSatName = (TextBox)it.FindControl("txtMBSatName");
                        TextBox txtMBInfoName = (TextBox)it.FindControl("txtMBInfoName");
                        TextBox txtMBInfoTime = (TextBox)it.FindControl("txtMBInfoTime");

                        dm.SatName = txtMBSatName.Text;
                        dm.InfoName = txtMBInfoName.Text;
                        dm.InfoTime = txtMBInfoTime.Text;
                        list2.Add(dm);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }
    }
}