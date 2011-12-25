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
    public partial class HJXQEdit : AspNetPage, IRouteContext
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
            XmlNode root = xmlDoc.SelectSingleNode("空间环境信息需求/User");
            txtHJUser.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("空间环境信息需求/Time");
            txtHJTime.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("空间环境信息需求/EnvironInfo");
            txtHJEnvironInfo.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("空间环境信息需求/TimeSection1");
            txtHJTimeSection1.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("空间环境信息需求/TimeSection2");
            txtHJTimeSection2.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("空间环境信息需求/Sum");
            txtHJSum.Text = root.InnerText;

            root = xmlDoc.SelectSingleNode("空间环境信息需求/卫星");
            List<HJXQSatInfo> list = new List<HJXQSatInfo>();
            HJXQSatInfo sat;
            foreach (XmlNode n in root.ChildNodes)
            {
                sat = new HJXQSatInfo();
                sat.SatName = n["SatName"].InnerText;
                sat.InfoName = n["InfoName"].InnerText;
                sat.InfoArea = n["InfoArea"].InnerText;
                sat.InfoTime = n["InfoTime"].InnerText;
                list.Add(sat);
            }
            rpHJ.DataSource = list;
            rpHJ.DataBind();


        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
            this.AddJavaScriptInclude("scripts/pages/HYXQEdit.aspx.js");
        }

        protected void rpHJ_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<HJXQSatInfo> list2 = new List<HJXQSatInfo>();
                HJXQSatInfo dm;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    dm = new HJXQSatInfo();
                    TextBox txtHJSatName = (TextBox)it.FindControl("txtHJSatName");
                    TextBox txtHJInfoName = (TextBox)it.FindControl("txtHJInfoName");
                    TextBox txtHJInfoArea = (TextBox)it.FindControl("txtHJInfoArea");
                    TextBox txtHJInfoTime = (TextBox)it.FindControl("txtHJInfoTime");

                    dm.SatName = txtHJSatName.Text;
                    dm.InfoName = txtHJInfoName.Text;
                    dm.InfoArea = txtHJInfoArea.Text;
                    dm.InfoTime = txtHJInfoTime.Text;
                    list2.Add(dm);
                }
                dm = new HJXQSatInfo();
                dm.SatName = "";
                dm.InfoName = "";
                dm.InfoArea = "";
                dm.InfoTime = "";
                list2.Add(dm);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<HJXQSatInfo> list2 = new List<HJXQSatInfo>();
                HJXQSatInfo dm;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        dm = new HJXQSatInfo();
                        TextBox txtHJSatName = (TextBox)it.FindControl("txtHJSatName");
                        TextBox txtHJInfoName = (TextBox)it.FindControl("txtHJInfoName");
                        TextBox txtHJInfoArea = (TextBox)it.FindControl("txtHJInfoArea");
                        TextBox txtHJInfoTime = (TextBox)it.FindControl("txtHJInfoTime");

                        dm.SatName = txtHJSatName.Text;
                        dm.InfoName = txtHJInfoName.Text;
                        dm.InfoArea = txtHJInfoArea.Text;
                        dm.InfoTime = txtHJInfoTime.Text;
                        list2.Add(dm);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }
    }
}