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
using System.Collections;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class ZXJHEdit : AspNetPage, IRouteContext
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
            List<ZXJH_WorkContent> listWC = new List<ZXJH_WorkContent>();
            List<ZXJH_SYDataHandle> listDH = new List<ZXJH_SYDataHandle>();
            List<ZXJH_DirectAndMonitor> listDM = new List<ZXJH_DirectAndMonitor>();
            List<ZXJH_RealTimeControl> listRC = new List<ZXJH_RealTimeControl>();
            List<ZXJH_SYEstimate> listE = new List<ZXJH_SYEstimate>();
            List<ZXJH_DataManage> listM = new List<ZXJH_DataManage>();

            ZXJH_WorkContent wc;
            ZXJH_SYDataHandle dh;
            ZXJH_DirectAndMonitor dam;
            ZXJH_RealTimeControl rc;
            ZXJH_SYEstimate sye;
            ZXJH_DataManage dm;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HfFileIndex.Value);
            #region 实验内容
            XmlNode root = xmlDoc.SelectSingleNode("中心运行计划/日期");
            txtDate.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("中心运行计划/试验内容/对应日期的试验个数");
            txtSYCount.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("中心运行计划/试验内容/试验项");
            foreach (XmlNode n in root.ChildNodes)
            {
                switch (n.Name)
                {
                    case "在当日计划中的ID":
                        txtSYID.Text = n.InnerText;
                        break;
                    case "试验项目名称":
                        txtSYName.Text = n.InnerText;
                        break;
                    case "试验开始日期及时间":
                        txtSYDateTime.Text = n.InnerText;
                        break;
                    case "试验运行的天数":
                        txtSYDays.Text = n.InnerText;
                        break;
                    case "载荷":
                        txtLoadStartTime.Text = n["开始时间"].InnerText;
                        txtLoadEndTime.Text = n["结束时间"].InnerText;
                        txtLoadContent.Text = n["动作内容"].InnerText;
                        break;
                    case "数传":
                        txtSCLaps.Text = n["圈次"].InnerText;
                        txtSCStartTime.Text = n["开始时间"].InnerText;
                        txtSCEndTime.Text = n["结束时间"].InnerText;
                        break;
                    case "测控":
                        txtCKLaps.Text = n["圈次"].InnerText;
                        txtCKStartTime.Text = n["开始时间"].InnerText;
                        txtCKEndTime.Text = n["结束时间"].InnerText;
                        break;
                    case "注数":
                        txtZSFirst.Text = n["最早时间要求"].InnerText;
                        txtZSLast.Text = n["最晚时间要求"].InnerText;
                        txtZSContent.Text = n["主要内容"].InnerText;
                        break;
                }
            }
            #endregion
            #region 载荷管理
            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/载荷管理");
            txtSYCount.Text = root.InnerText;
            foreach (XmlNode n in root.ChildNodes)
            {
                switch (n.Name)
                {
                    case "载荷管理":
                        txtWork_Load_SYID.Text = n["对应试验ID"].InnerText;
                        txtWork_Load_SatID.Text = n["卫星代号"].InnerText;
                        txtWork_Load_Process.Text = n["进程"].InnerText;
                        txtWork_Load_Event.Text = n["事件"].InnerText;
                        txtWork_Load_Action.Text = n["动作内容"].InnerText;
                        txtWork_Load_StartTime.Text = n["开始时间"].InnerText;
                        txtWork_Load_EndTime.Text = n["结束时间"].InnerText;
                        break;
                    case "指令管理":
                        txtWork_Command_SYID.Text = n["对应试验ID"].InnerText;
                        txtWork_Command_SYItem.Text = n["试验项目"].InnerText;
                        txtWork_Command_SatID.Text = n["卫星代号"].InnerText;
                        txtWork_Command_Content.Text = n["指令内容"].InnerText;
                        txtWork_Command_UpRequire.Text = n["上注要求"].InnerText;
                        txtWork_Command_Direction.Text = n["指令发送方向"].InnerText;
                        txtWork_Command_SpecialRequire.Text = n["特殊需求"].InnerText;
                        break;
                }
            }
            #endregion
            #region Repeater
            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/工作内容");
            foreach (XmlNode n in root.ChildNodes)
            {
                wc = new ZXJH_WorkContent();
                wc.Work = n["工作"].InnerText;
                wc.SYID = n["对应试验ID"].InnerText;
                wc.StartTime = n["开始时间"].InnerText;
                wc.MinTime = n["最短持续时间"].InnerText;
                wc.MaxTime = n["最长持续时间"].InnerText;
                listWC.Add(wc);
            }
            rpWork.DataSource = listWC;
            rpWork.DataBind();

            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/试验数据处理");
            foreach (XmlNode n in root.ChildNodes)
            {
                dh = new ZXJH_SYDataHandle();
                dh.SYID = n["对应试验ID"].InnerText;
                dh.SatID = n["卫星代号"].InnerText;
                dh.Laps = n["圈次"].InnerText;
                dh.MainStationName = n["主站名称"].InnerText;
                dh.BakStationName = n["备站名称"].InnerText;
                dh.Content = n["工作内容"].InnerText;
                dh.StartTime = n["实时开始处理时间"].InnerText;
                dh.EndTime = n["实时结束处理时间"].InnerText;
                dh.AfterWardsDataHandle = n["事后数据处理"].InnerText;
                listDH.Add(dh);
            }
            rpSYDataHandle.DataSource = listDH;
            rpSYDataHandle.DataBind();

            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/指挥与监视");
            foreach (XmlNode n in root.ChildNodes)
            {
                dam = new ZXJH_DirectAndMonitor();
                dam.SYID = n["对应试验ID"].InnerText;
                dam.DateSection = n["时间段"].InnerText;
                dam.Task = n["指挥与监视任务"].InnerText;
                dam.RealTimeShowTask = n["实时显示任务"].InnerText;
                listDM.Add(dam);
            }
            rpDirectAndMonitor.DataSource = listDM;
            rpDirectAndMonitor.DataBind();

            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/实时控制");
            foreach (XmlNode n in root.ChildNodes)
            {
                rc = new ZXJH_RealTimeControl();
                rc.Work = n["工作"].InnerText;
                rc.SYID = n["对应试验ID"].InnerText;
                rc.StartTime = n["开始时间"].InnerText;
                rc.EndTime = n["结束时间"].InnerText;
                listRC.Add(rc);
            }
            rpRealTimeControl.DataSource = listRC;
            rpRealTimeControl.DataBind();

            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/试验评估");
            foreach (XmlNode n in root.ChildNodes)
            {
                sye = new ZXJH_SYEstimate();
                sye.SYID = n["对应试验ID"].InnerText;
                sye.StartTime = n["开始时间"].InnerText;
                sye.EndTime = n["结束时间"].InnerText;
                listE.Add(sye);
            }
            rpSYEstimate.DataSource = listE;
            rpSYEstimate.DataBind();

            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/数据管理");
            foreach (XmlNode n in root.ChildNodes)
            {
                dm = new ZXJH_DataManage();
                dm.Work = n["工作"].InnerText;
                dm.Description = n["对应数据描述"].InnerText;
                dm.EndTime = n["开始时间"].InnerText;
                dm.EndTime = n["结束时间"].InnerText;
                listM.Add(dm);
            }
            rpDataManage.DataSource = listM;
            rpDataManage.DataBind();
            #endregion
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
            this.AddJavaScriptInclude("scripts/pages/ZXJHEdit.aspx.js");
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["ar"] != null && ViewState["op"] != null)
                {
                    ArrayList ar = (ArrayList)ViewState["ar"];
                    Repeater rp = e.Item.FindControl("Repeater2") as Repeater;
                    int row = (int)ViewState["row"];
                    List<ZXJH_SYEstimate> list2 = new List<ZXJH_SYEstimate>();
                    ZXJH_SYEstimate sye;
                    string op = (string)ViewState["op"];
                    if (op == "Add")
                    {
                        //sye = new ZXJH_SYEstimate();
                        if (e.Item.ItemIndex <= ar.Count-1)
                        {
                            list2 = (List < ZXJH_SYEstimate >) ar[e.Item.ItemIndex];
                            rp.DataSource = list2;
                            rp.DataBind();
                        }
                        else
                        {
                            sye = new ZXJH_SYEstimate();
                            sye.StartTime = "";
                            sye.EndTime = "";
                            list2.Add(sye);
                            rp.DataSource = list2;
                            rp.DataBind();
                        }
                    }
                    if (op == "Del")
                    {
                        if (e.Item.ItemIndex <= ar.Count - 1)
                        {
                            list2 = (List<ZXJH_SYEstimate>)ar[e.Item.ItemIndex];
                            rp.DataSource = list2;
                            rp.DataBind();
                        }
                    }
                }
                else
                {
                    #region 正常
                    Repeater rp = e.Item.FindControl("Repeater2") as Repeater;
                    int row = e.Item.ItemIndex;
                    List<ZXJH_SYEstimate> list2 = new List<ZXJH_SYEstimate>();
                    ZXJH_SYEstimate sye;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(@"D:\files\aa.xml");
                    XmlNode root = xmlDoc.SelectSingleNode("ZXJH/任务");//查找 txt
                    int i = 0;
                    foreach (XmlNode n in root.ChildNodes)
                    {
                        foreach (XmlNode nd in n.ChildNodes)
                        {
                            if (row == i)
                            {
                                if (nd.Name == "Item")
                                {
                                    sye = new ZXJH_SYEstimate();
                                    sye.StartTime = nd["X"].InnerText;
                                    sye.EndTime = nd["Y"].InnerText;
                                    list2.Add(sye);
                                }
                            }
                        }

                        i = i + 1;
                    }
                    rp.DataSource = list2;
                    rp.DataBind();

                    #endregion
                }
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_DataManage> list2 = new List<ZXJH_DataManage>();
                ZXJH_DataManage dm;
                List<ZXJH_SYEstimate> list1 = new List<ZXJH_SYEstimate>();
                ZXJH_SYEstimate sye;
                
                Repeater rp = (Repeater)source;
                ViewState["row"] = e.Item.ItemIndex;
                ArrayList ar = new ArrayList();
                ViewState["op"] = "Add";

                foreach (RepeaterItem it in rp.Items)
                {
                    Repeater rps = it.FindControl("Repeater2") as Repeater;
                    foreach (RepeaterItem its in rps.Items)
                    {
                            sye = new ZXJH_SYEstimate();
                            TextBox txt1 = (TextBox)its.FindControl("txtX");
                            TextBox txt2 = (TextBox)its.FindControl("txtY");
                            sye.StartTime = txt1.Text;
                            sye.EndTime = txt2.Text;
                            list1.Add(sye);
                    }
                    ar.Add(list1);
                    list1 = new List<ZXJH_SYEstimate>();

                    dm = new ZXJH_DataManage();
                    TextBox txtX = (TextBox)it.FindControl("txtName");
                    TextBox txtY = (TextBox)it.FindControl("txtColor");
                    dm.Work = txtX.Text;
                    dm.Description = txtY.Text;
                    list2.Add(dm);
                }
                ViewState["ar"] = ar;
                dm = new ZXJH_DataManage();
                dm.Work = "";
                dm.Description = "";
                list2.Add(dm);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_DataManage> list2 = new List<ZXJH_DataManage>();
                ZXJH_DataManage dm;
                List<ZXJH_SYEstimate> list1 = new List<ZXJH_SYEstimate>();
                ZXJH_SYEstimate sye;
                ArrayList ar = new ArrayList();
                Repeater rp = (Repeater)source;
                ViewState["row"] = e.Item.ItemIndex;
                ViewState["op"] = "Del";

                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        Repeater rps = it.FindControl("Repeater2") as Repeater;
                        foreach (RepeaterItem its in rps.Items)
                        {
                            sye = new ZXJH_SYEstimate();
                            TextBox txt1 = (TextBox)its.FindControl("txtX");
                            TextBox txt2 = (TextBox)its.FindControl("txtY");
                            sye.StartTime = txt1.Text;
                            sye.EndTime = txt2.Text;
                            list1.Add(sye);
                        }
                        ar.Add(list1);
                        list1 = new List<ZXJH_SYEstimate>();

                        dm = new ZXJH_DataManage();
                        TextBox txtX = (TextBox)it.FindControl("txtName");
                        TextBox txtY = (TextBox)it.FindControl("txtColor");
                        dm.Work = txtX.Text;
                        dm.Description = txtY.Text;
                        list2.Add(dm);
                    }
                }
                ViewState["ar"] = ar;
                rp.DataSource = list2;
                rp.DataBind();
            }
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                List<ZXJH_SYEstimate> list2 = new List<ZXJH_SYEstimate>();
                ZXJH_SYEstimate sye;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        sye = new ZXJH_SYEstimate();
                        TextBox txtX = (TextBox)it.FindControl("txtX");
                        TextBox txtY = (TextBox)it.FindControl("txtY");
                        sye.StartTime = txtX.Text;
                        sye.EndTime = txtY.Text;
                        list2.Add(sye);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }

        protected void rpWork_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_WorkContent> list2 = new List<ZXJH_WorkContent>();
                ZXJH_WorkContent wc;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    wc = new ZXJH_WorkContent();
                    TextBox txtWC_Work = (TextBox)it.FindControl("txtWC_Work");
                    TextBox txtWC_SYID = (TextBox)it.FindControl("txtWC_SYID");
                    TextBox txtWC_StartTime = (TextBox)it.FindControl("txtWC_StartTime");
                    TextBox txtWC_MinTime = (TextBox)it.FindControl("txtWC_MinTime");
                    TextBox txtWC_MaxTime = (TextBox)it.FindControl("txtWC_MaxTime");

                    wc.Work = txtWC_Work.Text;
                    wc.SYID = txtWC_SYID.Text;
                    wc.StartTime = txtWC_StartTime.Text;
                    wc.MinTime = txtWC_MinTime.Text;
                    wc.MaxTime = txtWC_MaxTime.Text;
                    list2.Add(wc);
                }
                wc = new ZXJH_WorkContent();
                wc.Work = "";
                wc.SYID = "";
                wc.StartTime = "";
                wc.MinTime = "";
                wc.MaxTime = "";
                list2.Add(wc);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_WorkContent> list2 = new List<ZXJH_WorkContent>();
                ZXJH_WorkContent wc;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        wc = new ZXJH_WorkContent();
                        TextBox txtWC_Work = (TextBox)it.FindControl("txtWC_Work");
                        TextBox txtWC_SYID = (TextBox)it.FindControl("txtWC_SYID");
                        TextBox txtWC_StartTime = (TextBox)it.FindControl("txtWC_StartTime");
                        TextBox txtWC_MinTime = (TextBox)it.FindControl("txtWC_MinTime");
                        TextBox txtWC_MaxTime = (TextBox)it.FindControl("txtWC_MaxTime");

                        wc.Work = txtWC_Work.Text;
                        wc.SYID = txtWC_SYID.Text;
                        wc.StartTime = txtWC_StartTime.Text;
                        wc.MinTime = txtWC_MinTime.Text;
                        wc.MaxTime = txtWC_MaxTime.Text;
                        list2.Add(wc);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }

        protected void rpSYDataHandle_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_SYDataHandle> list2 = new List<ZXJH_SYDataHandle>();
                ZXJH_SYDataHandle dh;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    dh = new ZXJH_SYDataHandle();
                    TextBox txtSHSYID = (TextBox)it.FindControl("txtSHSYID");
                    TextBox txtSHSatID = (TextBox)it.FindControl("txtSHSatID");
                    TextBox txtSHLaps = (TextBox)it.FindControl("txtSHLaps");
                    TextBox txtSHMaintStation = (TextBox)it.FindControl("txtSHMaintStation");
                    TextBox txtSHBakStation = (TextBox)it.FindControl("txtSHBakStation");
                    TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                    TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                    TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");
                    TextBox txtSHAfterDH = (TextBox)it.FindControl("txtSHAfterDH");

                    dh.SYID = txtSHSYID.Text;
                    dh.SatID = txtSHSatID.Text;
                    dh.Laps = txtSHLaps.Text;
                    dh.MainStationName = txtSHMaintStation.Text;
                    dh.BakStationName = txtSHBakStation.Text;
                    dh.Content = txtSHContent.Text;
                    dh.StartTime = txtSHStartTime.Text;
                    dh.EndTime = txtSHEndTime.Text;
                    dh.AfterWardsDataHandle = txtSHAfterDH.Text;
                    list2.Add(dh);
                }
                dh = new ZXJH_SYDataHandle();
                dh.SYID = "";
                dh.SatID = "";
                dh.Laps = "";
                dh.MainStationName = "";
                dh.BakStationName = "";
                dh.Content = "";
                dh.StartTime = "";
                dh.EndTime = "";
                dh.AfterWardsDataHandle = "";
                list2.Add(dh);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_SYDataHandle> list2 = new List<ZXJH_SYDataHandle>();
                ZXJH_SYDataHandle dh;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        dh = new ZXJH_SYDataHandle();
                        TextBox txtSHSYID = (TextBox)it.FindControl("txtSHSYID");
                        TextBox txtSHSatID = (TextBox)it.FindControl("txtSHSatID");
                        TextBox txtSHLaps = (TextBox)it.FindControl("txtSHLaps");
                        TextBox txtSHMaintStation = (TextBox)it.FindControl("txtSHMaintStation");
                        TextBox txtSHBakStation = (TextBox)it.FindControl("txtSHBakStation");
                        TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                        TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                        TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");
                        TextBox txtSHAfterDH = (TextBox)it.FindControl("txtSHAfterDH");

                        dh.SYID = txtSHSYID.Text;
                        dh.SatID = txtSHSatID.Text;
                        dh.Laps = txtSHLaps.Text;
                        dh.MainStationName = txtSHMaintStation.Text;
                        dh.BakStationName = txtSHBakStation.Text;
                        dh.Content = txtSHContent.Text;
                        dh.StartTime = txtSHStartTime.Text;
                        dh.EndTime = txtSHEndTime.Text;
                        dh.AfterWardsDataHandle = txtSHAfterDH.Text;
                        list2.Add(dh);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }

        protected void rpDirectAndMonitor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_DirectAndMonitor> list2 = new List<ZXJH_DirectAndMonitor>();
                ZXJH_DirectAndMonitor dm;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    dm = new ZXJH_DirectAndMonitor();
                    TextBox txtDMSYID = (TextBox)it.FindControl("txtDMSYID");
                    TextBox txtDMDateSection = (TextBox)it.FindControl("txtDMDateSection");
                    TextBox txtDMTask = (TextBox)it.FindControl("txtDMTask");
                    TextBox txtDMRTTask = (TextBox)it.FindControl("txtDMRTTask");

                    dm.SYID = txtDMSYID.Text;
                    dm.DateSection = txtDMDateSection.Text;
                    dm.Task = txtDMTask.Text;
                    dm.RealTimeShowTask = txtDMRTTask.Text;
                    list2.Add(dm);
                }
                dm = new ZXJH_DirectAndMonitor();
                dm.SYID = "";
                dm.DateSection = "";
                dm.Task = "";
                dm.RealTimeShowTask = "";
                list2.Add(dm);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_DirectAndMonitor> list2 = new List<ZXJH_DirectAndMonitor>();
                ZXJH_DirectAndMonitor dm;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        dm = new ZXJH_DirectAndMonitor();
                        TextBox txtDMSYID = (TextBox)it.FindControl("txtDMSYID");
                        TextBox txtDMDateSection = (TextBox)it.FindControl("txtDMDateSection");
                        TextBox txtDMTask = (TextBox)it.FindControl("txtDMTask");
                        TextBox txtDMRTTask = (TextBox)it.FindControl("txtDMRTTask");

                        dm.SYID = txtDMSYID.Text;
                        dm.DateSection = txtDMDateSection.Text;
                        dm.Task = txtDMTask.Text;
                        dm.RealTimeShowTask = txtDMRTTask.Text;
                        list2.Add(dm);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }

        protected void rpRealTimeControl_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_RealTimeControl> list2 = new List<ZXJH_RealTimeControl>();
                ZXJH_RealTimeControl rc;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    rc = new ZXJH_RealTimeControl();
                    TextBox txtRCWork = (TextBox)it.FindControl("txtRCWork");
                    TextBox txtRCSYID = (TextBox)it.FindControl("txtRCSYID");
                    TextBox txtRCStartTime = (TextBox)it.FindControl("txtRCStartTime");
                    TextBox txtRCEndTime = (TextBox)it.FindControl("txtRCEndTime");

                    rc.Work = txtRCWork.Text;
                    rc.SYID = txtRCSYID.Text;
                    rc.StartTime = txtRCStartTime.Text;
                    rc.EndTime = txtRCEndTime.Text;
                    list2.Add(rc);
                }
                rc = new ZXJH_RealTimeControl();
                rc.Work = "";
                rc.SYID = "";
                rc.StartTime = "";
                rc.EndTime = "";
                list2.Add(rc);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_RealTimeControl> list2 = new List<ZXJH_RealTimeControl>();
                ZXJH_RealTimeControl rc;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        rc = new ZXJH_RealTimeControl();
                        TextBox txtRCWork = (TextBox)it.FindControl("txtRCWork");
                        TextBox txtRCSYID = (TextBox)it.FindControl("txtRCSYID");
                        TextBox txtRCStartTime = (TextBox)it.FindControl("txtRCStartTime");
                        TextBox txtRCEndTime = (TextBox)it.FindControl("txtRCEndTime");

                        rc.Work = txtRCWork.Text;
                        rc.SYID = txtRCSYID.Text;
                        rc.StartTime = txtRCStartTime.Text;
                        rc.EndTime = txtRCEndTime.Text;
                        list2.Add(rc);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }

        protected void SYEstimate_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_SYEstimate> list2 = new List<ZXJH_SYEstimate>();
                ZXJH_SYEstimate sye;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    sye = new ZXJH_SYEstimate();
                    TextBox txtESYID = (TextBox)it.FindControl("txtESYID");
                    TextBox txtEStartTime = (TextBox)it.FindControl("txtEStartTime");
                    TextBox txtEEndTime = (TextBox)it.FindControl("txtEEndTime");

                    sye.SYID = txtESYID.Text;
                    sye.StartTime = txtEStartTime.Text;
                    sye.EndTime = txtEEndTime.Text;
                    list2.Add(sye);
                }
                sye = new ZXJH_SYEstimate();
                sye.SYID = "";
                sye.StartTime = "";
                sye.EndTime = "";
                list2.Add(sye);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_SYEstimate> list2 = new List<ZXJH_SYEstimate>();
                ZXJH_SYEstimate sye;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        sye = new ZXJH_SYEstimate();
                        TextBox txtESYID = (TextBox)it.FindControl("txtESYID");
                        TextBox txtEStartTime = (TextBox)it.FindControl("txtEStartTime");
                        TextBox txtEEndTime = (TextBox)it.FindControl("txtEEndTime");

                        sye.SYID = txtESYID.Text;
                        sye.StartTime = txtEStartTime.Text;
                        sye.EndTime = txtEEndTime.Text;
                        list2.Add(sye);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }

        protected void rpDataManage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_DataManage> list2 = new List<ZXJH_DataManage>();
                ZXJH_DataManage dm;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    dm = new ZXJH_DataManage();
                    TextBox txtMWork = (TextBox)it.FindControl("txtMWork");
                    TextBox txtMDes = (TextBox)it.FindControl("txtMDes");
                    TextBox txtMStartTime = (TextBox)it.FindControl("txtMStartTime");
                    TextBox txtMEndTime = (TextBox)it.FindControl("txtMEndTime");

                    dm.Work = txtMWork.Text;
                    dm.Description = txtMDes.Text;
                    dm.StartTime = txtMStartTime.Text;
                    dm.EndTime = txtMEndTime.Text;
                    list2.Add(dm);
                }
                dm = new ZXJH_DataManage();
                dm.Work = "";
                dm.Description = "";
                dm.StartTime = "";
                dm.EndTime = "";
                list2.Add(dm);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_DataManage> list2 = new List<ZXJH_DataManage>();
                ZXJH_DataManage dm;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    if (e.Item.ItemIndex != it.ItemIndex)
                    {
                        dm = new ZXJH_DataManage();
                        TextBox txtMWork = (TextBox)it.FindControl("txtMWork");
                        TextBox txtMDes = (TextBox)it.FindControl("txtMDes");
                        TextBox txtMStartTime = (TextBox)it.FindControl("txtMStartTime");
                        TextBox txtMEndTime = (TextBox)it.FindControl("txtMEndTime");

                        dm.Work = txtMWork.Text;
                        dm.Description = txtMDes.Text;
                        dm.StartTime = txtMStartTime.Text;
                        dm.EndTime = txtMEndTime.Text;
                        list2.Add(dm);
                    }
                }
                rp.DataSource = list2;
                rp.DataBind();
            }
        }
    }
}