using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer;
using OperatingManagement.DataAccessLayer.PlanManage;
using OperatingManagement.Framework;
using System.Web.Security;
using System.Xml;
using System.Collections;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class ZXJHEdit : AspNetPage, IRouteContext
    {
        bool isTempJH = false;  //是否为临时计划,默认为正式计划

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnFormal.Visible = false; 
                txtPlanStartTime.Attributes.Add("readonly", "true");
                txtPlanEndTime.Attributes.Add("readonly", "true");
                initial();
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    string sID = Request.QueryString["id"];
                    if (!string.IsNullOrEmpty(Request.QueryString["istemp"]) && Request.QueryString["istemp"] == "true")
                    {
                        isTempJH = true;
                        ViewState["isTempJH"] = true;
                        btnFormal.Visible = true;   //只有临时计划才能转为正式计划
                    }

                    HfID.Value = sID;
                    hfStatus.Value = "edit";    //编辑
                    BindJhTable(sID);
                    BindXML();
                    hfURL.Value = "?type=ZXJH&startDate=" + Request.QueryString["startDate"] + "&endDate=" + Request.QueryString["endDate"];
                    if ("detail" == Request.QueryString["op"])
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script type='text/javascript'>hideAllButton();</script>");
                    }
                }
                else
                {
                    btnReturn.Visible = false;
                    hfStatus.Value = "new"; //新建
                    btnSaveTo.Visible = false;
                    
                }
            }
        }

        private void initial()
        {
            pnlMain.Visible = true;
            pnlStation.Visible = false;

            List<ZXJH_SYContent> listSY = new List<ZXJH_SYContent>();
            List<ZXJH_SYContent_SC> listSYSC = new List<ZXJH_SYContent_SC>();
            List<ZXJH_SYContent_CK> listSYCK = new List<ZXJH_SYContent_CK>();
            List<ZXJH_SYContent_ZS> listSYZS = new List<ZXJH_SYContent_ZS>();
            List<ZXJH_WorkContent> listWC = new List<ZXJH_WorkContent>();
            List<ZXJH_CommandMake> listCM = new List<ZXJH_CommandMake>();
            List<ZXJH_SYDataHandle> listDH = new List<ZXJH_SYDataHandle>();
            List<ZXJH_DirectAndMonitor> listDM = new List<ZXJH_DirectAndMonitor>();
            List<ZXJH_RealTimeControl> listRC = new List<ZXJH_RealTimeControl>();
            List<ZXJH_SYEstimate> listE = new List<ZXJH_SYEstimate>();

            listSYSC.Add(new ZXJH_SYContent_SC());
            listSYCK.Add(new ZXJH_SYContent_CK());
            listSYZS.Add(new ZXJH_SYContent_ZS());
            ZXJH_SYContent sy = new ZXJH_SYContent() { 
                SCList = listSYSC,
                CKList = listSYCK,
                ZSList = listSYZS
            };
            listSY.Add(sy);
            listWC.Add(new ZXJH_WorkContent());
            listCM.Add(new ZXJH_CommandMake());
            listDH.Add(new ZXJH_SYDataHandle());
            listDM.Add(new ZXJH_DirectAndMonitor());
            listRC.Add(new ZXJH_RealTimeControl());
            listE.Add(new ZXJH_SYEstimate());

            rpSYContent.DataSource = listSY;
            rpSYContent.DataBind();
            rpWork.DataSource = listWC;
            rpWork.DataBind();
            rpCommandMake.DataSource = listCM;
            rpCommandMake.DataBind();
            rpSYDataHandle.DataSource = listDH;
            rpSYDataHandle.DataBind();
            rpDirectAndMonitor.DataSource = listDM;
            rpDirectAndMonitor.DataBind();
            rpRealTimeControl.DataSource = listRC;
            rpRealTimeControl.DataBind();
            rpSYEstimate.DataSource = listE;
            rpSYEstimate.DataBind();

        }
        private void BindJhTable(string sID)
        {
            isTempJH = GetIsTempJHValue();
            List<JH> jh = (new JH(isTempJH)).SelectByIDS(sID);
            txtPlanStartTime.Text = jh[0].StartTime.ToString("yyyy-MM-dd HH:mm:ss");
            txtPlanEndTime.Text = jh[0].EndTime.ToString("yyyy-MM-dd HH:mm:ss");
            HfFileIndex.Value = jh[0].FileIndex;
            hfTaskID.Value = jh[0].TaskID.ToString();
            ucTask1.SelectedValue = jh[0].TaskID.ToString();
            string[] strTemp = jh[0].FileIndex.Split('_');
            if (strTemp.Length >= 2)
            {
                hfSatID.Value = strTemp[strTemp.Length - 2];
                ucSatellite1.SelectedValue = strTemp[strTemp.Length - 2];
            }
            txtNote.Text = jh[0].Reserve.ToString();
            //计划启动后不能修改计划
            if (DateTime.Now > jh[0].StartTime)
            {
                //btnSubmit.Visible = false;
                //hfOverDate.Value = "true";
            }
        }
        private void BindXML()
        {
            List<ZXJH_SYContent> listSY = new List<ZXJH_SYContent>();
            List<ZXJH_WorkContent> listWC = new List<ZXJH_WorkContent>();
            List<ZXJH_CommandMake> listCM = new List<ZXJH_CommandMake>();
            List<ZXJH_SYDataHandle> listDH = new List<ZXJH_SYDataHandle>();
            List<ZXJH_DirectAndMonitor> listDM = new List<ZXJH_DirectAndMonitor>();
            List<ZXJH_RealTimeControl> listRC = new List<ZXJH_RealTimeControl>();
            List<ZXJH_SYEstimate> listE = new List<ZXJH_SYEstimate>();

            ZXJH_SYContent sy;
            ZXJH_SYContent_SC sy_sc;
            ZXJH_SYContent_CK sy_ck;
            ZXJH_SYContent_ZS sy_zs;
            ZXJH_WorkContent wc;
            ZXJH_CommandMake cm;
            ZXJH_SYDataHandle dh;
            ZXJH_DirectAndMonitor dam;
            ZXJH_RealTimeControl rc;
            ZXJH_SYEstimate sye;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HfFileIndex.Value);
            #region 试验计划
            XmlNode root = xmlDoc.SelectSingleNode("中心运行计划/日期");
            txtDate.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("中心运行计划/试验计划/对应日期的试验个数");
            txtSYCount.Text = root.InnerText;

            #endregion
            #region Repeater
            #region 试验计划
            root = xmlDoc.SelectSingleNode("中心运行计划/试验计划");
            foreach (XmlNode no in root.ChildNodes)
            {
                if (no.Name == "试验内容")
                {
                    sy = new ZXJH_SYContent();
                    foreach (XmlNode n in no.ChildNodes)
                    {
                        switch (n.Name)
                        {
                            case "卫星代号":
                                sy.SatID = n.InnerText;
                                break;
                            case "试验":
                                sy.SYID = n["在当日计划中的ID"].InnerText;
                                sy.SYName = n["试验项目名称"].InnerText;
                                sy.SYStartTime = n["试验开始时间"].InnerText;
                                sy.SYEndTime = n["试验结束时间"].InnerText;
                                sy.SYDays = n["试验运行的天数"].InnerText;
                                sy.SYNote = n["说明"].InnerText;
                                break;
                            case "数传":
                                sy_sc = new ZXJH_SYContent_SC();
                                sy_sc.SY_SCStationNO = n["站编号"].InnerText;
                                sy_sc.SY_SCEquipmentNO = n["设备编号"].InnerText;
                                sy_sc.SY_SCFrequencyBand = n["频段"].InnerText;
                                sy_sc.SY_SCLaps = n["圈次"].InnerText;
                                sy_sc.SY_SCStartTime = n["开始时间"].InnerText;
                                sy_sc.SY_SCEndTime = n["结束时间"].InnerText;
                                if (sy.SCList == null)
                                    sy.SCList = new List<ZXJH_SYContent_SC>();
                                sy.SCList.Add(sy_sc);
                                break;
                            case "测控":
                                sy_ck = new ZXJH_SYContent_CK();
                                sy_ck.SY_CKStationNO = n["站编号"].InnerText;
                                sy_ck.SY_CKEquipmentNO = n["设备编号"].InnerText;
                                sy_ck.SY_CKLaps = n["圈次"].InnerText;
                                sy_ck.SY_CKStartTime = n["开始时间"].InnerText;
                                sy_ck.SY_CKEndTime = n["结束时间"].InnerText;
                                if (sy.CKList == null)
                                    sy.CKList = new List<ZXJH_SYContent_CK>();
                                sy.CKList.Add(sy_ck);
                                break;
                            case "注数":
                                sy_zs = new ZXJH_SYContent_ZS();
                                sy_zs.SY_ZSFirst = n["最早时间要求"].InnerText;
                                sy_zs.SY_ZSLast = n["最晚时间要求"].InnerText;
                                sy_zs.SY_ZSContent = n["主要内容"].InnerText;
                                if (sy.ZSList == null)
                                    sy.ZSList = new List<ZXJH_SYContent_ZS>();
                                sy.ZSList.Add(sy_zs);
                                break;
                        }
                    }
                    listSY.Add(sy);
                }

                //listSY.Add(sy);
            }
            rpSYContent.DataSource = listSY;
            rpSYContent.DataBind();
            #endregion 试验计划

            #region  任务管理
            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/任务管理");
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
            #endregion

            #region  指令制作
            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/指令制作");
            foreach (XmlNode n in root.ChildNodes)
            {
                cm = new ZXJH_CommandMake();
                cm.Work_Command_SatID = n["卫星代号"].InnerText;
                cm.Work_Command_SYID = n["对应试验ID"].InnerText;
                cm.Work_Command_Programe = n["对应控制程序"].InnerText;
                cm.Work_Command_FinishTime = n["完成时间"].InnerText;
                cm.Work_Command_UpWay = n["上注方式"].InnerText;
                cm.Work_Command_UpTime = n["上注时间"].InnerText;
                cm.Work_Command_Note = n["说明"].InnerText;
                listCM.Add(cm);
            }
            rpCommandMake.DataSource = listCM;
            rpCommandMake.DataBind();
            #endregion

            #region 实时试验数据处理
            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/实时试验数据处理");
            foreach (XmlNode n in root.ChildNodes)
            {
                dh = new ZXJH_SYDataHandle();
                dh.SYID = n["对应试验ID"].InnerText;
                dh.SatID = n["卫星代号"].InnerText;
                dh.Laps = n["圈次"].InnerText;
                dh.MainStation = n["主站"].InnerText;
                dh.MainStationEquipment = n["主站设备"].InnerText;
                dh.BakStation = n["备站"].InnerText;
                dh.BakStationEquipment = n["备站设备"].InnerText;
                dh.Content = n["工作内容"].InnerText;
                dh.StartTime = n["实时开始处理时间"].InnerText;
                dh.EndTime = n["实时结束处理时间"].InnerText;
                listDH.Add(dh);
            }
            rpSYDataHandle.DataSource = listDH;
            rpSYDataHandle.DataBind();
            #endregion

            #region 指挥与监视
            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/指挥与监视");
            foreach (XmlNode n in root.ChildNodes)
            {
                dam = new ZXJH_DirectAndMonitor();
                dam.SYID = n["对应试验ID"].InnerText;
                dam.StartTime = n["开始时间"].InnerText;
                dam.EndTime = n["结束时间"].InnerText;
                dam.RealTimeDemoTask = n["实时演示任务"].InnerText;
                listDM.Add(dam);
            }
            rpDirectAndMonitor.DataSource = listDM;
            rpDirectAndMonitor.DataBind();
            #endregion

            #region 实时控制
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
            #endregion

            #region 处理评估
            root = xmlDoc.SelectSingleNode("中心运行计划/工作计划/处理评估");
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
            #endregion

            #endregion
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMPLAN_Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/ZXJHEdit.aspx.js");
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
                    DropDownList ddlWC_Work = (DropDownList)it.FindControl("ddlWC_Work");
                    TextBox txtWC_SYID = (TextBox)it.FindControl("txtWC_SYID");
                    TextBox txtWC_StartTime = (TextBox)it.FindControl("txtWC_StartTime");
                    TextBox txtWC_MinTime = (TextBox)it.FindControl("txtWC_MinTime");
                    TextBox txtWC_MaxTime = (TextBox)it.FindControl("txtWC_MaxTime");

                    if (CheckCtrlExits(txtWC_SYID, "试验ID"))
                        wc.SYID = txtWC_SYID.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtWC_StartTime, "开始时间"))
                        wc.StartTime = txtWC_StartTime.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtWC_MinTime, "最短持续时间"))
                        wc.MinTime = txtWC_MinTime.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtWC_MaxTime, "最长持续时间"))
                        wc.MaxTime = txtWC_MaxTime.Text;
                    else
                        return;
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
                Repeater rp = (Repeater)source;
                if (rp.Items.Count <= 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "del", "<script type='text/javascript'>showMsg('最后一条，无法删除!');</script>");
                }
                else
                {
                    List<ZXJH_WorkContent> list2 = new List<ZXJH_WorkContent>();
                    ZXJH_WorkContent wc;

                    foreach (RepeaterItem it in rp.Items)
                    {
                        if (e.Item.ItemIndex != it.ItemIndex)
                        {
                            wc = new ZXJH_WorkContent();
                            DropDownList ddlWC_Work = (DropDownList)it.FindControl("ddlWC_Work");
                            TextBox txtWC_SYID = (TextBox)it.FindControl("txtWC_SYID");
                            TextBox txtWC_StartTime = (TextBox)it.FindControl("txtWC_StartTime");
                            TextBox txtWC_MinTime = (TextBox)it.FindControl("txtWC_MinTime");
                            TextBox txtWC_MaxTime = (TextBox)it.FindControl("txtWC_MaxTime");

                            wc.Work = ddlWC_Work.SelectedItem.Text;
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
                    TextBox txtMainStation = (TextBox)it.FindControl("txtMainStation");
                    TextBox txtSHMainStationEquipment = (TextBox)it.FindControl("txtSHMainStationEquipment");
                    TextBox txtBackStation = (TextBox)it.FindControl("txtBackStation");
                    TextBox txtSHBakStationEquipment = (TextBox)it.FindControl("txtSHBakStationEquipment");
                    TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                    TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                    TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");

                    if (CheckCtrlExits(txtSHSYID, "试验ID"))
                        dh.SYID = txtSHSYID.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtSHSatID, "卫星代号"))
                        dh.SatID = txtSHSatID.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtSHLaps, "圈次"))
                        dh.Laps = txtSHLaps.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtSHContent, "工作内容"))
                        dh.Content = txtSHContent.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtMainStation, "主站"))
                        dh.MainStation = txtMainStation.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtSHMainStationEquipment, "主站设备"))
                        dh.MainStationEquipment = txtSHMainStationEquipment.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtBackStation, "备站"))
                    dh.BakStation = txtBackStation.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtSHBakStationEquipment, "备站设备"))
                    dh.BakStationEquipment = txtSHBakStationEquipment.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtSHStartTime, "实时开始处理时间"))
                    dh.StartTime = txtSHStartTime.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtSHEndTime, "实时结束处理时间"))
                    dh.EndTime = txtSHEndTime.Text;
                    else
                        return;
                    list2.Add(dh);
                }
                dh = new ZXJH_SYDataHandle();
                list2.Add(dh);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_SYDataHandle> list2 = new List<ZXJH_SYDataHandle>();
                ZXJH_SYDataHandle dh;
                Repeater rp = (Repeater)source;
                if (rp.Items.Count <= 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "del", "<script type='text/javascript'>showMsg('最后一条，无法删除!');</script>");
                }
                else
                {
                    foreach (RepeaterItem it in rp.Items)
                    {
                        if (e.Item.ItemIndex != it.ItemIndex)
                        {
                            dh = new ZXJH_SYDataHandle();
                            TextBox txtSHSYID = (TextBox)it.FindControl("txtSHSYID");
                            TextBox txtSHSatID = (TextBox)it.FindControl("txtSHSatID");
                            TextBox txtSHLaps = (TextBox)it.FindControl("txtSHLaps");
                            TextBox txtMainStation = (TextBox)it.FindControl("txtMainStation");
                            TextBox txtSHMainStationEquipment = (TextBox)it.FindControl("txtSHMainStationEquipment");
                            TextBox txtBackStation = (TextBox)it.FindControl("txtBackStation");
                            TextBox txtSHBakStationEquipment = (TextBox)it.FindControl("txtSHBakStationEquipment");
                            TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                            TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                            TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");

                            dh.SYID = txtSHSYID.Text;
                            dh.SatID = txtSHSatID.Text;
                            dh.Laps = txtSHLaps.Text;
                            dh.Content = txtSHContent.Text;
                            dh.MainStation = txtMainStation.Text;
                            dh.MainStationEquipment = txtSHMainStationEquipment.Text;
                            dh.BakStation = txtBackStation.Text;
                            dh.BakStationEquipment = txtSHBakStationEquipment.Text;
                            dh.StartTime = txtSHStartTime.Text;
                            dh.EndTime = txtSHEndTime.Text;
                            list2.Add(dh);
                        }
                    }
                    rp.DataSource = list2;
                    rp.DataBind();
                }
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
                    TextBox txtDMStartTime = (TextBox)it.FindControl("txtDMStartTime");
                    TextBox txtDMEndTime = (TextBox)it.FindControl("txtDMEndTime");
                    DropDownList ddlDMRTTask = (DropDownList)it.FindControl("ddlDMRTTask");

                    if (CheckCtrlExits(txtDMSYID, "试验ID"))
                        dm.SYID = txtDMSYID.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtDMStartTime, "开始时间"))
                        dm.StartTime = txtDMStartTime.Text;
                    else
                        return;

                    if (CheckCtrlExits(txtDMEndTime, "结束时间"))
                        dm.EndTime = txtDMEndTime.Text;
                    else
                        return;

                    dm.RealTimeDemoTask = ddlDMRTTask.SelectedItem.Text;
                    list2.Add(dm);
                }
                dm = new ZXJH_DirectAndMonitor();
                list2.Add(dm);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_DirectAndMonitor> list2 = new List<ZXJH_DirectAndMonitor>();
                ZXJH_DirectAndMonitor dm;
                Repeater rp = (Repeater)source;
                if (rp.Items.Count <= 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "del", "<script type='text/javascript'>showMsg('最后一条，无法删除!');</script>");
                }
                else
                {
                    foreach (RepeaterItem it in rp.Items)
                    {
                        if (e.Item.ItemIndex != it.ItemIndex)
                        {
                            dm = new ZXJH_DirectAndMonitor();
                            TextBox txtDMSYID = (TextBox)it.FindControl("txtDMSYID");
                            TextBox txtDMStartTime = (TextBox)it.FindControl("txtDMStartTime");
                            TextBox txtDMEndTime = (TextBox)it.FindControl("txtDMEndTime");
                            DropDownList ddlDMRTTask = (DropDownList)it.FindControl("ddlDMRTTask");

                            dm.SYID = txtDMSYID.Text;
                            dm.StartTime = txtDMStartTime.Text;
                            dm.EndTime = txtDMEndTime.Text;
                            dm.RealTimeDemoTask = ddlDMRTTask.SelectedItem.Text;
                            list2.Add(dm);
                        }
                    }
                    rp.DataSource = list2;
                    rp.DataBind();
                }
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

                    if (CheckCtrlExits(txtRCWork, "工作"))
                        rc.Work = txtRCWork.Text;
                    else
                        return;
                    if (CheckCtrlExits(txtRCSYID, "试验ID"))
                        rc.SYID = txtRCSYID.Text;
                    else
                        return;
                    if (CheckCtrlExits(txtRCStartTime, "开始时间"))
                        rc.StartTime = txtRCStartTime.Text;
                    else
                        return;
                    if (CheckCtrlExits(txtRCEndTime, "结束时间"))
                        rc.EndTime = txtRCEndTime.Text;
                    else
                        return;
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
                if (rp.Items.Count <= 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "del", "<script type='text/javascript'>showMsg('最后一条，无法删除!');</script>");
                }
                else
                {
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

                    if (CheckCtrlExits(txtESYID, "试验ID"))
                        sye.SYID = txtESYID.Text;
                    else
                        return;
                    if (CheckCtrlExits(txtEStartTime, "开始时间"))
                        sye.StartTime = txtEStartTime.Text;
                    else
                        return;
                    if (CheckCtrlExits(txtEEndTime, "完成时间"))
                        sye.EndTime = txtEEndTime.Text;
                    else
                        return;
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
                if (rp.Items.Count <= 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "del", "<script type='text/javascript'>showMsg('最后一条，无法删除!');</script>");
                }
                else
                {
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
        }

        /// <summary>
        /// 保存计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                isTempJH = GetIsTempJHValue();

                #region basic
                ZXJH obj = new ZXJH();
                obj.Date = txtDate.Text;
                //obj.SYCount = txtSYCount.Text;

                obj.SYContents = new List<ZXJH_SYContent>();
                obj.WorkContents = new List<ZXJH_WorkContent>();
                obj.CommandMakes = new List<ZXJH_CommandMake>();
                obj.SYDataHandles = new List<ZXJH_SYDataHandle>();
                obj.DirectAndMonitors = new List<ZXJH_DirectAndMonitor>();
                obj.RealTimeControls = new List<ZXJH_RealTimeControl>();
                obj.SYEstimates = new List<ZXJH_SYEstimate>();
                #endregion

                ZXJH_SYContent sy;
                ZXJH_SYContent_SC sy_sc;
                ZXJH_SYContent_CK sy_ck;
                ZXJH_SYContent_ZS sy_zs;
                ZXJH_WorkContent wc;
                ZXJH_CommandMake cm;
                ZXJH_SYDataHandle dh;
                ZXJH_DirectAndMonitor dam;
                ZXJH_RealTimeControl rc;
                ZXJH_SYEstimate sye;

                #region SYContent
                foreach (RepeaterItem it in rpSYContent.Items)
                {
                    sy = new ZXJH_SYContent();
                    sy.SCList = new List<ZXJH_SYContent_SC>();
                    sy.CKList = new List<ZXJH_SYContent_CK>();
                    sy.ZSList = new List<ZXJH_SYContent_ZS>();
                    //TextBox txtSYSatID = (TextBox)it.FindControl("txtSYSatID");
                    //sy.SatID = txtSYSatID.Text;
                    ucs.ucSatellite ddlSYSatID = (ucs.ucSatellite)it.FindControl("ddlSYSatID");
                    sy.SatID = ddlSYSatID.SelectedValue;
                    #region 试验
                    TextBox txtSYID = (TextBox)it.FindControl("txtSYID");
                    TextBox txtSYName = (TextBox)it.FindControl("txtSYName");
                    TextBox txtSYStartDateTime = (TextBox)it.FindControl("txtSYStartDateTime");
                    TextBox txtSYEndDateTime = (TextBox)it.FindControl("txtSYEndDateTime");
                    TextBox txtSYDays = (TextBox)it.FindControl("txtSYDays");
                    TextBox txtSYNote = (TextBox)it.FindControl("txtSYNote");

                    sy.SYID = txtSYID.Text;
                    sy.SYName = txtSYName.Text;
                    sy.SYStartTime = txtSYStartDateTime.Text;
                    sy.SYEndTime = txtSYEndDateTime.Text;
                    sy.SYDays = txtSYDays.Text;
                    sy.SYNote = txtSYNote.Text;
                    #endregion
                    #region 数传
                    Repeater rpsc = it.FindControl("rpSYContentSC") as Repeater;
                    foreach (RepeaterItem its in rpsc.Items)
                    {
                        sy_sc = new ZXJH_SYContent_SC();
                        TextBox txtSCStationNO = (TextBox)its.FindControl("txtSCStationNO");
                        TextBox txtSCEquipmentNO = (TextBox)its.FindControl("txtSCEquipmentNO");
                        DropDownList ddlSCFrequencyBand = (DropDownList)its.FindControl("ddlSCFrequencyBand");
                        //TextBox txtSCFrequencyBand = (TextBox)it.FindControl("txtSCFrequencyBand");
                        TextBox txtSCLaps = (TextBox)its.FindControl("txtSCLaps");
                        TextBox txtSCStartTime = (TextBox)its.FindControl("txtSCStartTime");
                        TextBox txtSCEndTime = (TextBox)its.FindControl("txtSCEndTime");

                        sy_sc.SY_SCStationNO = txtSCStationNO.Text;
                        sy_sc.SY_SCEquipmentNO = txtSCEquipmentNO.Text;
                        sy_sc.SY_SCFrequencyBand = ddlSCFrequencyBand.SelectedValue;
                        sy_sc.SY_SCLaps = txtSCLaps.Text;
                        sy_sc.SY_SCStartTime = txtSCStartTime.Text;
                        sy_sc.SY_SCEndTime = txtSCEndTime.Text;
                        sy.SCList.Add(sy_sc);
                    }
                    #endregion
                    #region 测控
                    Repeater rpck = it.FindControl("rpSYContentCK") as Repeater;
                    foreach (RepeaterItem its in rpck.Items)
                    {
                        sy_ck = new ZXJH_SYContent_CK();
                        TextBox txtCKStationNO = (TextBox)its.FindControl("txtCKStationNO");
                        TextBox txtCKEquipmentNO = (TextBox)its.FindControl("txtCKEquipmentNO");
                        TextBox txtCKStartTime = (TextBox)its.FindControl("txtCKStartTime");
                        TextBox txtCKEndTime = (TextBox)its.FindControl("txtCKEndTime");
                        TextBox txtCKLaps = (TextBox)its.FindControl("txtCKLaps");

                        sy_ck.SY_CKStationNO = txtCKStationNO.Text;
                        sy_ck.SY_CKEquipmentNO = txtCKEquipmentNO.Text;
                        sy_ck.SY_CKStartTime = txtCKStartTime.Text;
                        sy_ck.SY_CKEndTime = txtCKEndTime.Text;
                        sy_ck.SY_CKLaps = txtCKLaps.Text;
                        sy.CKList.Add(sy_ck);
                    }
                    #endregion
                    #region 注数
                    Repeater rpzs = it.FindControl("rpSYContentZS") as Repeater;
                    foreach (RepeaterItem its in rpzs.Items)
                    {
                        sy_zs = new ZXJH_SYContent_ZS();
                        TextBox txtZSFirst = (TextBox)its.FindControl("txtZSFirst");
                        TextBox txtZSLast = (TextBox)its.FindControl("txtZSLast");
                        TextBox txtZSContent = (TextBox)its.FindControl("txtZSContent");

                        sy_zs.SY_ZSFirst = txtZSFirst.Text;
                        sy_zs.SY_ZSLast = txtZSLast.Text;
                        sy_zs.SY_ZSContent = txtZSContent.Text;
                        sy.ZSList.Add(sy_zs);
                    }
                    #endregion
                    obj.SYContents.Add(sy);
                }

                obj.SYCount = obj.SYContents.Count.ToString();  //对应日期的试验个数=试验内容的个数
                #endregion
                #region workContent
                foreach (RepeaterItem it in rpWork.Items)
                {
                    wc = new ZXJH_WorkContent();
                    DropDownList ddlWC_Work = (DropDownList)it.FindControl("ddlWC_Work");
                    TextBox txtWC_SYID = (TextBox)it.FindControl("txtWC_SYID");
                    TextBox txtWC_StartTime = (TextBox)it.FindControl("txtWC_StartTime");
                    TextBox txtWC_MinTime = (TextBox)it.FindControl("txtWC_MinTime");
                    TextBox txtWC_MaxTime = (TextBox)it.FindControl("txtWC_MaxTime");

                    wc.Work = ddlWC_Work.SelectedItem.Text;
                    wc.SYID = txtWC_SYID.Text;
                    wc.StartTime = txtWC_StartTime.Text;
                    wc.MinTime = txtWC_MinTime.Text;
                    wc.MaxTime = txtWC_MaxTime.Text;
                    obj.WorkContents.Add(wc);
                }
                #endregion
                #region CommandMake
                foreach (RepeaterItem it in rpCommandMake.Items)
                {
                    cm = new ZXJH_CommandMake();
                    ucs.ucSatellite txtWork_Command_SatID = (ucs.ucSatellite)it.FindControl("txtWork_Command_SatID");
                    TextBox txtWork_Command_SYID = (TextBox)it.FindControl("txtWork_Command_SYID");
                    TextBox txtWork_Command_Programe = (TextBox)it.FindControl("txtWork_Command_Programe");
                    TextBox txtWork_Command_FinishTime = (TextBox)it.FindControl("txtWork_Command_FinishTime");
                    TextBox txtWork_Command_UpWay = (TextBox)it.FindControl("txtWork_Command_UpWay");
                    TextBox txtWork_Command_UpTime = (TextBox)it.FindControl("txtWork_Command_UpTime");
                    TextBox txtWork_Command_Note = (TextBox)it.FindControl("txtWork_Command_Note");

                    cm.Work_Command_SatID = txtWork_Command_SatID.SelectedValue;
                    cm.Work_Command_SYID = txtWork_Command_SYID.Text;
                    cm.Work_Command_Programe = txtWork_Command_Programe.Text;
                    cm.Work_Command_FinishTime = txtWork_Command_FinishTime.Text;
                    cm.Work_Command_UpWay = txtWork_Command_UpWay.Text;
                    cm.Work_Command_UpTime = txtWork_Command_UpTime.Text;
                    cm.Work_Command_Note = txtWork_Command_Note.Text;
                    obj.CommandMakes.Add(cm);
                }
                #endregion
                #region SYDataHandle
                foreach (RepeaterItem it in rpSYDataHandle.Items)
                {
                    dh = new ZXJH_SYDataHandle();
                    TextBox txtSHSYID = (TextBox)it.FindControl("txtSHSYID");
                    TextBox txtSHSatID = (TextBox)it.FindControl("txtSHSatID");
                    TextBox txtSHLaps = (TextBox)it.FindControl("txtSHLaps");
                    TextBox txtMainStation = (TextBox)it.FindControl("txtMainStation");
                    TextBox txtSHMainStationEquipment = (TextBox)it.FindControl("txtSHMainStationEquipment");
                    TextBox txtBackStation = (TextBox)it.FindControl("txtBackStation");
                    TextBox txtSHBakStationEquipment = (TextBox)it.FindControl("txtSHBakStationEquipment");
                    TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                    TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                    TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");

                    dh.SYID = txtSHSYID.Text;
                    dh.SatID = txtSHSatID.Text;
                    dh.Laps = txtSHLaps.Text;
                    dh.Content = txtSHContent.Text;
                    dh.MainStation = txtMainStation.Text;
                    dh.MainStationEquipment = txtSHMainStationEquipment.Text;
                    dh.BakStation = txtBackStation.Text;
                    dh.BakStationEquipment = txtSHBakStationEquipment.Text;
                    dh.StartTime = txtSHStartTime.Text;
                    dh.EndTime = txtSHEndTime.Text;
                    obj.SYDataHandles.Add(dh);
                }
                #endregion
                #region DirectAndMonitor
                foreach (RepeaterItem it in rpDirectAndMonitor.Items)
                {
                    dam = new ZXJH_DirectAndMonitor();
                    TextBox txtDMSYID = (TextBox)it.FindControl("txtDMSYID");
                    TextBox txtDMStartTime = (TextBox)it.FindControl("txtDMStartTime");
                    TextBox txtDMEndTime = (TextBox)it.FindControl("txtDMEndTime");
                    DropDownList ddlDMRTTask = (DropDownList)it.FindControl("ddlDMRTTask");

                    dam.SYID = txtDMSYID.Text;
                    dam.StartTime = txtDMStartTime.Text;
                    dam.EndTime = txtDMEndTime.Text;
                    dam.RealTimeDemoTask = ddlDMRTTask.SelectedItem.Text;
                    obj.DirectAndMonitors.Add(dam);
                }
                #endregion
                #region RealTimeControl
                foreach (RepeaterItem it in rpRealTimeControl.Items)
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
                    obj.RealTimeControls.Add(rc);
                }
                #endregion
                #region SYEstimate
                foreach (RepeaterItem it in rpSYEstimate.Items)
                {
                    sye = new ZXJH_SYEstimate();
                    TextBox txtESYID = (TextBox)it.FindControl("txtESYID");
                    TextBox txtEStartTime = (TextBox)it.FindControl("txtEStartTime");
                    TextBox txtEEndTime = (TextBox)it.FindControl("txtEEndTime");

                    sye.SYID = txtESYID.Text;
                    sye.StartTime = txtEStartTime.Text;
                    sye.EndTime = txtEEndTime.Text;
                    obj.SYEstimates.Add(sye);
                }
                #endregion

                obj.TaskID = ucTask1.SelectedItem.Value;    //任务ID
                obj.SatID = ucSatellite1.SelectedItem.Value;    //卫星ID

                PlanFileCreator creater = new PlanFileCreator(isTempJH);
                if (hfStatus.Value == "new")
                {
                    string filepath = creater.CreateZXJHFile(obj, 0);

                    DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                    {
                        TaskID = obj.TaskID,
                        PlanType = "ZXJH",
                        PlanID = (new Sequence()).GetZXJHSequnce(),
                        StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                        EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                        SRCType = 0,
                        FileIndex = filepath,
                        SatID = obj.SatID,
                        Reserve = txtNote.Text
                    };
                    var result = jh.Add();
                }
                else
                {
                    //当任务和卫星更新时，需要更新文件名称
                    if (hfSatID.Value != ucSatellite1.SelectedValue || hfTaskID.Value != ucTask1.SelectedValue)
                    {
                        string filepath = creater.CreateZXJHFile(obj, 0);

                        DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                        {
                            Id = Convert.ToInt32(HfID.Value),
                            TaskID = obj.TaskID,
                            StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                            EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                            FileIndex = filepath,
                            SatID = obj.SatID,
                            Reserve = txtNote.Text
                        };
                        var result = jh.Update();
                        //更新隐藏域的任务ID和卫星ID
                        hfTaskID.Value = jh.TaskID;
                        hfSatID.Value = jh.SatID;
                    }
                    else
                    {
                        creater.FilePath = HfFileIndex.Value;
                        creater.CreateZXJHFile(obj, 1);
                    }
                }
                ltMessage.Text = "计划保存成功";
                txtSYCount.Text = obj.SYCount;
                //ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
            }
            catch (Exception ex)
            {
                throw (new AspNetException("保存计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void btnSaveTo_Click(object sender, EventArgs e)
        {
            try
            {
                isTempJH = GetIsTempJHValue();

                #region basic
                ZXJH obj = new ZXJH();
                obj.Date = txtDate.Text;
               // obj.SYCount = txtSYCount.Text;

                obj.SYContents = new List<ZXJH_SYContent>();
                obj.WorkContents = new List<ZXJH_WorkContent>();
                obj.CommandMakes = new List<ZXJH_CommandMake>();
                obj.SYDataHandles = new List<ZXJH_SYDataHandle>();
                obj.DirectAndMonitors = new List<ZXJH_DirectAndMonitor>();
                obj.RealTimeControls = new List<ZXJH_RealTimeControl>();
                obj.SYEstimates = new List<ZXJH_SYEstimate>();
                #endregion

                ZXJH_SYContent sy;
                ZXJH_SYContent_SC sy_sc;
                ZXJH_SYContent_CK sy_ck;
                ZXJH_SYContent_ZS sy_zs;
                ZXJH_WorkContent wc;
                ZXJH_CommandMake cm;
                ZXJH_SYDataHandle dh;
                ZXJH_DirectAndMonitor dam;
                ZXJH_RealTimeControl rc;
                ZXJH_SYEstimate sye;

                #region SYContent
                foreach (RepeaterItem it in rpSYContent.Items)
                {
                    sy = new ZXJH_SYContent();
                    sy.SCList = new List<ZXJH_SYContent_SC>();
                    sy.CKList = new List<ZXJH_SYContent_CK>();
                    sy.ZSList = new List<ZXJH_SYContent_ZS>();
                    //TextBox txtSYSatID = (TextBox)it.FindControl("txtSYSatID");
                    //sy.SatID = txtSYSatID.Text;
                    ucs.ucSatellite ddlSYSatID = (ucs.ucSatellite)it.FindControl("ddlSYSatID");
                    sy.SatID = ddlSYSatID.SelectedValue;
                    #region 试验
                    TextBox txtSYID = (TextBox)it.FindControl("txtSYID");
                    TextBox txtSYName = (TextBox)it.FindControl("txtSYName");
                    TextBox txtSYStartDateTime = (TextBox)it.FindControl("txtSYStartDateTime");
                    TextBox txtSYEndDateTime = (TextBox)it.FindControl("txtSYEndDateTime");
                    TextBox txtSYDays = (TextBox)it.FindControl("txtSYDays");
                    TextBox txtSYNote = (TextBox)it.FindControl("txtSYNote");

                    sy.SYID = txtSYID.Text;
                    sy.SYName = txtSYName.Text;
                    sy.SYStartTime = txtSYStartDateTime.Text;
                    sy.SYEndTime = txtSYEndDateTime.Text;
                    sy.SYDays = txtSYDays.Text;
                    sy.SYNote = txtSYNote.Text;
                    #endregion
                    #region 数传
                    Repeater rpsc = it.FindControl("rpSYContentSC") as Repeater;
                    foreach (RepeaterItem its in rpsc.Items)
                    {
                        sy_sc = new ZXJH_SYContent_SC();
                        TextBox txtSCStationNO = (TextBox)its.FindControl("txtSCStationNO");
                        TextBox txtSCEquipmentNO = (TextBox)its.FindControl("txtSCEquipmentNO");
                        DropDownList ddlSCFrequencyBand = (DropDownList)its.FindControl("ddlSCFrequencyBand");
                        //TextBox txtSCFrequencyBand = (TextBox)its.FindControl("txtSCFrequencyBand");
                        TextBox txtSCLaps = (TextBox)its.FindControl("txtSCLaps");
                        TextBox txtSCStartTime = (TextBox)its.FindControl("txtSCStartTime");
                        TextBox txtSCEndTime = (TextBox)its.FindControl("txtSCEndTime");

                        sy_sc.SY_SCStationNO = txtSCStationNO.Text;
                        sy_sc.SY_SCEquipmentNO = txtSCEquipmentNO.Text;
                        sy_sc.SY_SCFrequencyBand = ddlSCFrequencyBand.SelectedValue;
                        sy_sc.SY_SCLaps = txtSCLaps.Text;
                        sy_sc.SY_SCStartTime = txtSCStartTime.Text;
                        sy_sc.SY_SCEndTime = txtSCEndTime.Text;
                        sy.SCList.Add(sy_sc);
                    }
                    #endregion
                    #region 测控
                    Repeater rpck = it.FindControl("rpSYContentCK") as Repeater;
                    foreach (RepeaterItem its in rpck.Items)
                    {
                        sy_ck = new ZXJH_SYContent_CK();
                        TextBox txtCKStationNO = (TextBox)its.FindControl("txtCKStationNO");
                        TextBox txtCKEquipmentNO = (TextBox)its.FindControl("txtCKEquipmentNO");
                        TextBox txtCKStartTime = (TextBox)its.FindControl("txtCKStartTime");
                        TextBox txtCKEndTime = (TextBox)its.FindControl("txtCKEndTime");
                        TextBox txtCKLaps = (TextBox)its.FindControl("txtCKLaps");

                        sy_ck.SY_CKStationNO = txtCKStationNO.Text;
                        sy_ck.SY_CKEquipmentNO = txtCKEquipmentNO.Text;
                        sy_ck.SY_CKStartTime = txtCKStartTime.Text;
                        sy_ck.SY_CKEndTime = txtCKEndTime.Text;
                        sy_ck.SY_CKLaps = txtCKLaps.Text;
                        sy.CKList.Add(sy_ck);
                    }
                    #endregion
                    #region 注数
                    Repeater rpzs = it.FindControl("rpSYContentZS") as Repeater;
                    foreach (RepeaterItem its in rpzs.Items)
                    {
                        sy_zs = new ZXJH_SYContent_ZS();
                        TextBox txtZSFirst = (TextBox)its.FindControl("txtZSFirst");
                        TextBox txtZSLast = (TextBox)its.FindControl("txtZSLast");
                        TextBox txtZSContent = (TextBox)its.FindControl("txtZSContent");

                        sy_zs.SY_ZSFirst = txtZSFirst.Text;
                        sy_zs.SY_ZSLast = txtZSLast.Text;
                        sy_zs.SY_ZSContent = txtZSContent.Text;
                        sy.ZSList.Add(sy_zs);
                    }
                    #endregion
                    obj.SYContents.Add(sy);
                }

                obj.SYCount = obj.SYContents.Count.ToString();  //对应日期的试验个数=试验内容的个数
                #endregion
                #region workContent
                foreach (RepeaterItem it in rpWork.Items)
                {
                    wc = new ZXJH_WorkContent();
                    DropDownList ddlWC_Work = (DropDownList)it.FindControl("ddlWC_Work");
                    TextBox txtWC_SYID = (TextBox)it.FindControl("txtWC_SYID");
                    TextBox txtWC_StartTime = (TextBox)it.FindControl("txtWC_StartTime");
                    TextBox txtWC_MinTime = (TextBox)it.FindControl("txtWC_MinTime");
                    TextBox txtWC_MaxTime = (TextBox)it.FindControl("txtWC_MaxTime");

                    wc.Work = ddlWC_Work.SelectedItem.Text;
                    wc.SYID = txtWC_SYID.Text;
                    wc.StartTime = txtWC_StartTime.Text;
                    wc.MinTime = txtWC_MinTime.Text;
                    wc.MaxTime = txtWC_MaxTime.Text;
                    obj.WorkContents.Add(wc);
                }
                #endregion
                #region CommandMake
                foreach (RepeaterItem it in rpCommandMake.Items)
                {
                    cm = new ZXJH_CommandMake();
                    ucs.ucSatellite txtWork_Command_SatID = (ucs.ucSatellite)it.FindControl("txtWork_Command_SatID");
                    TextBox txtWork_Command_SYID = (TextBox)it.FindControl("txtWork_Command_SYID");
                    TextBox txtWork_Command_Programe = (TextBox)it.FindControl("txtWork_Command_Programe");
                    TextBox txtWork_Command_FinishTime = (TextBox)it.FindControl("txtWork_Command_FinishTime");
                    TextBox txtWork_Command_UpWay = (TextBox)it.FindControl("txtWork_Command_UpWay");
                    TextBox txtWork_Command_UpTime = (TextBox)it.FindControl("txtWork_Command_UpTime");
                    TextBox txtWork_Command_Note = (TextBox)it.FindControl("txtWork_Command_Note");

                    cm.Work_Command_SatID = txtWork_Command_SatID.SelectedValue;
                    cm.Work_Command_SYID = txtWork_Command_SYID.Text;
                    cm.Work_Command_Programe = txtWork_Command_Programe.Text;
                    cm.Work_Command_FinishTime = txtWork_Command_FinishTime.Text;
                    cm.Work_Command_UpWay = txtWork_Command_UpWay.Text;
                    cm.Work_Command_UpTime = txtWork_Command_UpTime.Text;
                    cm.Work_Command_Note = txtWork_Command_Note.Text;
                    obj.CommandMakes.Add(cm);
                }
                #endregion
                #region SYDataHandle
                foreach (RepeaterItem it in rpSYDataHandle.Items)
                {
                    dh = new ZXJH_SYDataHandle();
                    TextBox txtSHSYID = (TextBox)it.FindControl("txtSHSYID");
                    TextBox txtSHSatID = (TextBox)it.FindControl("txtSHSatID");
                    TextBox txtSHLaps = (TextBox)it.FindControl("txtSHLaps");
                    TextBox txtMainStation = (TextBox)it.FindControl("txtMainStation");
                    TextBox txtSHMainStationEquipment = (TextBox)it.FindControl("txtSHMainStationEquipment");
                    TextBox txtBackStation = (TextBox)it.FindControl("txtBackStation");
                    TextBox txtSHBakStationEquipment = (TextBox)it.FindControl("txtSHBakStationEquipment");
                    TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                    TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                    TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");

                    dh.SYID = txtSHSYID.Text;
                    dh.SatID = txtSHSatID.Text;
                    dh.Laps = txtSHLaps.Text;
                    dh.Content = txtSHContent.Text;
                    dh.MainStation = txtMainStation.Text;
                    dh.MainStationEquipment = txtSHMainStationEquipment.Text;
                    dh.BakStation = txtBackStation.Text;
                    dh.BakStationEquipment = txtSHBakStationEquipment.Text;
                    dh.StartTime = txtSHStartTime.Text;
                    dh.EndTime = txtSHEndTime.Text;
                    obj.SYDataHandles.Add(dh);
                }
                #endregion
                #region DirectAndMonitor
                foreach (RepeaterItem it in rpDirectAndMonitor.Items)
                {
                    dam = new ZXJH_DirectAndMonitor();
                    TextBox txtDMSYID = (TextBox)it.FindControl("txtDMSYID");
                    TextBox txtDMStartTime = (TextBox)it.FindControl("txtDMStartTime");
                    TextBox txtDMEndTime = (TextBox)it.FindControl("txtDMEndTime");
                    DropDownList ddlDMRTTask = (DropDownList)it.FindControl("ddlDMRTTask");

                    dam.SYID = txtDMSYID.Text;
                    dam.StartTime = txtDMStartTime.Text;
                    dam.EndTime = txtDMEndTime.Text;
                    dam.RealTimeDemoTask = ddlDMRTTask.SelectedItem.Text;
                    obj.DirectAndMonitors.Add(dam);
                }
                #endregion
                #region RealTimeControl
                foreach (RepeaterItem it in rpRealTimeControl.Items)
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
                    obj.RealTimeControls.Add(rc);
                }
                #endregion
                #region SYEstimate
                foreach (RepeaterItem it in rpSYEstimate.Items)
                {
                    sye = new ZXJH_SYEstimate();
                    TextBox txtESYID = (TextBox)it.FindControl("txtESYID");
                    TextBox txtEStartTime = (TextBox)it.FindControl("txtEStartTime");
                    TextBox txtEEndTime = (TextBox)it.FindControl("txtEEndTime");

                    sye.SYID = txtESYID.Text;
                    sye.StartTime = txtEStartTime.Text;
                    sye.EndTime = txtEEndTime.Text;
                    obj.SYEstimates.Add(sye);
                }
                #endregion

                PlanFileCreator creater = new PlanFileCreator(isTempJH);

                obj.TaskID = ucTask1.SelectedItem.Value;
                obj.SatID = ucSatellite1.SelectedItem.Value;
                //检查文件是否已经存在
                if (creater.TestZXJHFileName(obj))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "File", "<script type='text/javascript'>showMsg('存在同名文件，请一分钟后重试');</script>");
                    return;
                }
                string filepath = creater.CreateZXJHFile(obj, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                {
                    TaskID = obj.TaskID,
                    PlanType = "ZXJH",
                    PlanID = (new Sequence()).GetZXJHSequnce(),
                    StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                    EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = obj.SatID,
                    Reserve = txtNote.Text
                };
                var result = jh.Add();

                ltMessage.Text = "计划保存成功";
                txtSYCount.Text = obj.SYCount;
                //ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
            }
            catch (Exception ex)
            {
                throw (new AspNetException("保存计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(HfID.Value))
                {
                    Page.Response.Redirect(Request.CurrentExecutionFilePath, false);
                }
                else
                {
                    string sID = HfID.Value;
                    HfID.Value = sID;
                    hfStatus.Value = "edit";    //编辑
                    BindJhTable(sID);
                    BindXML();
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("重置页面出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            isTempJH = GetIsTempJHValue();
            if (isTempJH)
            {
                Response.Redirect("PlanTempList.aspx" + hfURL.Value);
            }
            else
            {
                Response.Redirect("PlanList.aspx" + hfURL.Value);
            }
        }

        protected bool GetIsTempJHValue()
        {
            bool returnvalue = false;
            if (null != ViewState["isTempJH"])
            {
                returnvalue = Convert.ToBoolean(ViewState["isTempJH"]);
            }
            return returnvalue;
        }

        /// <summary>
        /// 试验内容
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rpSYContent_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_SYContent> list2 = new List<ZXJH_SYContent>();
                ZXJH_SYContent sy;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    sy = new ZXJH_SYContent();
                    //TextBox txtSYSatID = (TextBox)it.FindControl("txtSYSatID");
                    //sy.SatID = txtSYSatID.Text;
                    ucs.ucSatellite ddlSYSatID = (ucs.ucSatellite)it.FindControl("ddlSYSatID");
                    sy.SatID = ddlSYSatID.SelectedValue;
                    #region 试验
                    TextBox txtSYID = (TextBox)it.FindControl("txtSYID");
                    TextBox txtSYName = (TextBox)it.FindControl("txtSYName");
                    TextBox txtSYStartDateTime = (TextBox)it.FindControl("txtSYStartDateTime");
                    TextBox txtSYEndDateTime = (TextBox)it.FindControl("txtSYEndDateTime");
                    TextBox txtSYDays = (TextBox)it.FindControl("txtSYDays");
                    TextBox txtSYNote = (TextBox)it.FindControl("txtSYNote");

                    sy.SYID = txtSYID.Text;
                    sy.SYName = txtSYName.Text;
                    sy.SYStartTime = txtSYStartDateTime.Text;
                    sy.SYEndTime = txtSYEndDateTime.Text;
                    sy.SYDays = txtSYDays.Text;
                    sy.SYNote = txtSYNote.Text;
                    #endregion
                    #region 数传
                    TextBox txtSCStationNO = (TextBox)it.FindControl("txtSCStationNO");
                    TextBox txtSCEquipmentNO = (TextBox)it.FindControl("txtSCEquipmentNO");
                    DropDownList ddlSCFrequencyBand = (DropDownList)it.FindControl("ddlSCFrequencyBand");
                    //TextBox txtSCFrequencyBand = (TextBox)it.FindControl("txtSCFrequencyBand");
                    TextBox txtSCLaps = (TextBox)it.FindControl("txtSCLaps");
                    TextBox txtSCStartTime = (TextBox)it.FindControl("txtSCStartTime");
                    TextBox txtSCEndTime = (TextBox)it.FindControl("txtSCEndTime");
                    if (txtSCStationNO == null)
                        return;

                    sy.SY_SCStationNO = txtSCStationNO.Text;
                    sy.SY_SCEquipmentNO = txtSCEquipmentNO.Text;
                    sy.SY_SCFrequencyBand = ddlSCFrequencyBand.SelectedValue;
                    sy.SY_SCLaps = txtSCLaps.Text;
                    sy.SY_SCStartTime = txtSCStartTime.Text;
                    sy.SY_SCEndTime = txtSCEndTime.Text;
                    #endregion
                    #region 测控
                    TextBox txtCKStationNO = (TextBox)it.FindControl("txtCKStationNO");
                    TextBox txtCKEquipmentNO = (TextBox)it.FindControl("txtCKEquipmentNO");
                    TextBox txtCKStartTime = (TextBox)it.FindControl("txtCKStartTime");
                    TextBox txtCKEndTime = (TextBox)it.FindControl("txtCKEndTime");
                    TextBox txtCKLaps = (TextBox)it.FindControl("txtCKLaps");

                    sy.SY_CKStationNO = txtCKStationNO.Text;
                    sy.SY_CKEquipmentNO = txtCKEquipmentNO.Text;
                    sy.SY_CKStartTime = txtCKStartTime.Text;
                    sy.SY_CKEndTime = txtCKEndTime.Text;
                    sy.SY_CKLaps = txtCKLaps.Text;
                    #endregion
                    #region 注数
                    TextBox txtZSFirst = (TextBox)it.FindControl("txtZSFirst");
                    TextBox txtZSLast = (TextBox)it.FindControl("txtZSLast");
                    TextBox txtZSContent = (TextBox)it.FindControl("txtZSContent");

                    sy.SY_ZSFirst = txtZSFirst.Text;
                    sy.SY_ZSLast = txtZSLast.Text;
                    sy.SY_ZSContent = txtZSContent.Text;

                    #endregion
                    list2.Add(sy);
                }
                sy = new ZXJH_SYContent();
                list2.Add(sy);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_SYContent> list2 = new List<ZXJH_SYContent>();
                ZXJH_SYContent sy;
                Repeater rp = (Repeater)source;
                if (rp.Items.Count <= 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "del", "<script type='text/javascript'>showMsg('最后一条，无法删除!');</script>");
                }
                else
                {
                    foreach (RepeaterItem it in rp.Items)
                    {
                        if (e.Item.ItemIndex != it.ItemIndex)
                        {
                            sy = new ZXJH_SYContent();
                            //TextBox txtSYSatID = (TextBox)it.FindControl("txtSYSatID");
                            //sy.SatID = txtSYSatID.Text;
                            ucs.ucSatellite ddlSYSatID = (ucs.ucSatellite)it.FindControl("ddlSYSatID");
                            sy.SatID = ddlSYSatID.SelectedValue;
                            #region 试验
                            TextBox txtSYID = (TextBox)it.FindControl("txtSYID");
                            TextBox txtSYName = (TextBox)it.FindControl("txtSYName");
                            TextBox txtSYStartDateTime = (TextBox)it.FindControl("txtSYStartDateTime");
                            TextBox txtSYEndDateTime = (TextBox)it.FindControl("txtSYEndDateTime");
                            TextBox txtSYDays = (TextBox)it.FindControl("txtSYDays");
                            TextBox txtSYNote = (TextBox)it.FindControl("txtSYNote");

                            sy.SYID = txtSYID.Text;
                            sy.SYName = txtSYName.Text;
                            sy.SYStartTime = txtSYStartDateTime.Text;
                            sy.SYEndTime = txtSYEndDateTime.Text;
                            sy.SYDays = txtSYDays.Text;
                            sy.SYNote = txtSYNote.Text;
                            #endregion
                            #region 数传
                            TextBox txtSCStationNO = (TextBox)it.FindControl("txtSCStationNO");
                            TextBox txtSCEquipmentNO = (TextBox)it.FindControl("txtSCEquipmentNO");
                            DropDownList ddlSCFrequencyBand = (DropDownList)it.FindControl("ddlSCFrequencyBand");
                            //TextBox txtSCFrequencyBand = (TextBox)it.FindControl("txtSCFrequencyBand");
                            TextBox txtSCLaps = (TextBox)it.FindControl("txtSCLaps");
                            TextBox txtSCStartTime = (TextBox)it.FindControl("txtSCStartTime");
                            TextBox txtSCEndTime = (TextBox)it.FindControl("txtSCEndTime");

                            sy.SY_SCStationNO = txtSCStationNO.Text;
                            sy.SY_SCEquipmentNO = txtSCEquipmentNO.Text;
                            sy.SY_SCFrequencyBand = ddlSCFrequencyBand.SelectedValue;
                            sy.SY_SCLaps = txtSCLaps.Text;
                            sy.SY_SCStartTime = txtSCStartTime.Text;
                            sy.SY_SCEndTime = txtSCEndTime.Text;
                            #endregion
                            #region 测控
                            TextBox txtCKStationNO = (TextBox)it.FindControl("txtCKStationNO");
                            TextBox txtCKEquipmentNO = (TextBox)it.FindControl("txtCKEquipmentNO");
                            TextBox txtCKStartTime = (TextBox)it.FindControl("txtCKStartTime");
                            TextBox txtCKEndTime = (TextBox)it.FindControl("txtCKEndTime");
                            TextBox txtCKLaps = (TextBox)it.FindControl("txtCKLaps");

                            sy.SY_CKStationNO = txtCKStationNO.Text;
                            sy.SY_CKEquipmentNO = txtCKEquipmentNO.Text;
                            sy.SY_CKStartTime = txtCKStartTime.Text;
                            sy.SY_CKEndTime = txtCKEndTime.Text;
                            sy.SY_CKLaps = txtCKLaps.Text;
                            #endregion
                            #region 注数
                            TextBox txtZSFirst = (TextBox)it.FindControl("txtZSFirst");
                            TextBox txtZSLast = (TextBox)it.FindControl("txtZSLast");
                            TextBox txtZSContent = (TextBox)it.FindControl("txtZSContent");

                            sy.SY_ZSFirst = txtZSFirst.Text;
                            sy.SY_ZSLast = txtZSLast.Text;
                            sy.SY_ZSContent = txtZSContent.Text;

                            #endregion
                            list2.Add(sy);
                        }
                    }
                    rp.DataSource = list2;
                    rp.DataBind();
                }
            }
        }

        protected void rpCommandMake_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_CommandMake> list2 = new List<ZXJH_CommandMake>();
                ZXJH_CommandMake obj;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    obj = new ZXJH_CommandMake();
                    ucs.ucSatellite txtWork_Command_SatID = (ucs.ucSatellite)it.FindControl("txtWork_Command_SatID");
                    TextBox txtWork_Command_SYID = (TextBox)it.FindControl("txtWork_Command_SYID");
                    TextBox txtWork_Command_Programe = (TextBox)it.FindControl("txtWork_Command_Programe");
                    TextBox txtWork_Command_FinishTime = (TextBox)it.FindControl("txtWork_Command_FinishTime");
                    TextBox txtWork_Command_UpWay = (TextBox)it.FindControl("txtWork_Command_UpWay");
                    TextBox txtWork_Command_UpTime = (TextBox)it.FindControl("txtWork_Command_UpTime");
                    TextBox txtWork_Command_Note = (TextBox)it.FindControl("txtWork_Command_Note");

                    obj.Work_Command_SatID = txtWork_Command_SatID.SelectedValue;
                    obj.Work_Command_SYID = txtWork_Command_SYID.Text;
                    obj.Work_Command_Programe = txtWork_Command_Programe.Text;
                    obj.Work_Command_FinishTime = txtWork_Command_FinishTime.Text;
                    obj.Work_Command_UpWay = txtWork_Command_UpWay.Text;
                    obj.Work_Command_UpTime = txtWork_Command_UpTime.Text;
                    obj.Work_Command_Note = txtWork_Command_Note.Text;

                    list2.Add(obj);
                }
                obj = new ZXJH_CommandMake();
                list2.Add(obj);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                Repeater rp = (Repeater)source;
                if (rp.Items.Count <= 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "del", "<script type='text/javascript'>showMsg('最后一条，无法删除!');</script>");
                }
                else
                {
                    List<ZXJH_CommandMake> list2 = new List<ZXJH_CommandMake>();
                    ZXJH_CommandMake obj;

                    foreach (RepeaterItem it in rp.Items)
                    {
                        if (e.Item.ItemIndex != it.ItemIndex)
                        {
                            obj = new ZXJH_CommandMake();
                            ucs.ucSatellite txtWork_Command_SatID = (ucs.ucSatellite)it.FindControl("txtWork_Command_SatID");
                            TextBox txtWork_Command_SYID = (TextBox)it.FindControl("txtWork_Command_SYID");
                            TextBox txtWork_Command_Programe = (TextBox)it.FindControl("txtWork_Command_Programe");
                            TextBox txtWork_Command_FinishTime = (TextBox)it.FindControl("txtWork_Command_FinishTime");
                            TextBox txtWork_Command_UpWay = (TextBox)it.FindControl("txtWork_Command_UpWay");
                            TextBox txtWork_Command_UpTime = (TextBox)it.FindControl("txtWork_Command_UpTime");
                            TextBox txtWork_Command_Note = (TextBox)it.FindControl("txtWork_Command_Note");

                            obj.Work_Command_SatID = txtWork_Command_SatID.SelectedValue;
                            obj.Work_Command_SYID = txtWork_Command_SYID.Text;
                            obj.Work_Command_Programe = txtWork_Command_Programe.Text;
                            obj.Work_Command_FinishTime = txtWork_Command_FinishTime.Text;
                            obj.Work_Command_UpWay = txtWork_Command_UpWay.Text;
                            obj.Work_Command_UpTime = txtWork_Command_UpTime.Text;
                            obj.Work_Command_Note = txtWork_Command_Note.Text;

                            list2.Add(obj);
                        }
                    }
                    rp.DataSource = list2;
                    rp.DataBind();
                }
            }
        }

        protected void rpDirectAndMonitor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //DataRowView rowv = (DataRowView)e.Item.DataItem;
                    ZXJH_DirectAndMonitor dm = (ZXJH_DirectAndMonitor)e.Item.DataItem;
                    DropDownList ddlDMRTTask = (DropDownList)e.Item.FindControl("ddlDMRTTask") as DropDownList;

                    ddlDMRTTask.SelectedValue = dm.RealTimeDemoTask;
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定地面计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void rpWork_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //DataRowView rowv = (DataRowView)e.Item.DataItem;
                    ZXJH_WorkContent wc = (ZXJH_WorkContent)e.Item.DataItem;
                    DropDownList ddlWC_Work = (DropDownList)e.Item.FindControl("ddlWC_Work") as DropDownList;

                    ddlWC_Work.SelectedValue = wc.Work;
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定地面计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void rpSYContent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ZXJH_SYContent sy = (ZXJH_SYContent)e.Item.DataItem;
                    #region 正常
                    Repeater rpSC = e.Item.FindControl("rpSYContentSC") as Repeater;
                    Repeater rpCK = e.Item.FindControl("rpSYContentCK") as Repeater;
                    Repeater rpZS = e.Item.FindControl("rpSYContentZS") as Repeater;
                    ucs.ucSatellite ddlSYSatID = e.Item.FindControl("ddlSYSatID") as ucs.ucSatellite;

                    ddlSYSatID.SelectedValue = sy.SatID;
                    rpSC.DataSource = sy.SCList;
                    rpSC.DataBind();
                    rpCK.DataSource = sy.CKList;
                    rpCK.DataBind();
                    rpZS.DataSource = sy.ZSList;
                    rpZS.DataBind();

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定地面计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void btnFormal_Click(object sender, EventArgs e)
        {
            try
            {
                #region basic
                ZXJH obj = new ZXJH();
                obj.Date = txtDate.Text;
                //obj.SYCount = txtSYCount.Text;

                obj.SYContents = new List<ZXJH_SYContent>();
                obj.WorkContents = new List<ZXJH_WorkContent>();
                obj.CommandMakes = new List<ZXJH_CommandMake>();
                obj.SYDataHandles = new List<ZXJH_SYDataHandle>();
                obj.DirectAndMonitors = new List<ZXJH_DirectAndMonitor>();
                obj.RealTimeControls = new List<ZXJH_RealTimeControl>();
                obj.SYEstimates = new List<ZXJH_SYEstimate>();
                #endregion

                ZXJH_SYContent sy;
                ZXJH_SYContent_SC sy_sc;
                ZXJH_SYContent_CK sy_ck;
                ZXJH_SYContent_ZS sy_zs;
                ZXJH_WorkContent wc;
                ZXJH_CommandMake cm;
                ZXJH_SYDataHandle dh;
                ZXJH_DirectAndMonitor dam;
                ZXJH_RealTimeControl rc;
                ZXJH_SYEstimate sye;

                #region SYContent
                foreach (RepeaterItem it in rpSYContent.Items)
                {
                    sy = new ZXJH_SYContent();
                    sy.SCList = new List<ZXJH_SYContent_SC>();
                    sy.CKList = new List<ZXJH_SYContent_CK>();
                    sy.ZSList = new List<ZXJH_SYContent_ZS>();
                    //TextBox txtSYSatID = (TextBox)it.FindControl("txtSYSatID");
                    //sy.SatID = txtSYSatID.Text;
                    ucs.ucSatellite ddlSYSatID = (ucs.ucSatellite)it.FindControl("ddlSYSatID");
                    sy.SatID = ddlSYSatID.SelectedValue;
                    #region 试验
                    TextBox txtSYID = (TextBox)it.FindControl("txtSYID");
                    TextBox txtSYName = (TextBox)it.FindControl("txtSYName");
                    TextBox txtSYStartDateTime = (TextBox)it.FindControl("txtSYStartDateTime");
                    TextBox txtSYEndDateTime = (TextBox)it.FindControl("txtSYEndDateTime");
                    TextBox txtSYDays = (TextBox)it.FindControl("txtSYDays");
                    TextBox txtSYNote = (TextBox)it.FindControl("txtSYNote");

                    sy.SYID = txtSYID.Text;
                    sy.SYName = txtSYName.Text;
                    sy.SYStartTime = txtSYStartDateTime.Text;
                    sy.SYEndTime = txtSYEndDateTime.Text;
                    sy.SYDays = txtSYDays.Text;
                    sy.SYNote = txtSYNote.Text;
                    #endregion
                    #region 数传
                    Repeater rpsc = it.FindControl("rpSYContentSC") as Repeater;
                    foreach (RepeaterItem its in rpsc.Items)
                    {
                        sy_sc = new ZXJH_SYContent_SC();
                        TextBox txtSCStationNO = (TextBox)it.FindControl("txtSCStationNO");
                        TextBox txtSCEquipmentNO = (TextBox)it.FindControl("txtSCEquipmentNO");
                        DropDownList ddlSCFrequencyBand = (DropDownList)it.FindControl("ddlSCFrequencyBand");
                        //TextBox txtSCFrequencyBand = (TextBox)it.FindControl("txtSCFrequencyBand");
                        TextBox txtSCLaps = (TextBox)it.FindControl("txtSCLaps");
                        TextBox txtSCStartTime = (TextBox)it.FindControl("txtSCStartTime");
                        TextBox txtSCEndTime = (TextBox)it.FindControl("txtSCEndTime");

                        sy_sc.SY_SCStationNO = txtSCStationNO.Text;
                        sy_sc.SY_SCEquipmentNO = txtSCEquipmentNO.Text;
                        sy_sc.SY_SCFrequencyBand = ddlSCFrequencyBand.SelectedValue;
                        sy_sc.SY_SCLaps = txtSCLaps.Text;
                        sy_sc.SY_SCStartTime = txtSCStartTime.Text;
                        sy_sc.SY_SCEndTime = txtSCEndTime.Text;
                        sy.SCList.Add(sy_sc);
                    }
                    #endregion
                    #region 测控
                    Repeater rpck = it.FindControl("rpSYContentCK") as Repeater;
                    foreach (RepeaterItem its in rpck.Items)
                    {
                        sy_ck = new ZXJH_SYContent_CK();
                        TextBox txtCKStationNO = (TextBox)it.FindControl("txtCKStationNO");
                        TextBox txtCKEquipmentNO = (TextBox)it.FindControl("txtCKEquipmentNO");
                        TextBox txtCKStartTime = (TextBox)it.FindControl("txtCKStartTime");
                        TextBox txtCKEndTime = (TextBox)it.FindControl("txtCKEndTime");
                        TextBox txtCKLaps = (TextBox)it.FindControl("txtCKLaps");

                        sy_ck.SY_CKStationNO = txtCKStationNO.Text;
                        sy_ck.SY_CKEquipmentNO = txtCKEquipmentNO.Text;
                        sy_ck.SY_CKStartTime = txtCKStartTime.Text;
                        sy_ck.SY_CKEndTime = txtCKEndTime.Text;
                        sy_ck.SY_CKLaps = txtCKLaps.Text;
                        sy.CKList.Add(sy_ck);
                    }
                    #endregion
                    #region 注数
                    Repeater rpzs = it.FindControl("rpReakTimeTransfor") as Repeater;
                    foreach (RepeaterItem its in rpzs.Items)
                    {
                        sy_zs = new ZXJH_SYContent_ZS();
                        TextBox txtZSFirst = (TextBox)it.FindControl("txtZSFirst");
                        TextBox txtZSLast = (TextBox)it.FindControl("txtZSLast");
                        TextBox txtZSContent = (TextBox)it.FindControl("txtZSContent");

                        sy_zs.SY_ZSFirst = txtZSFirst.Text;
                        sy_zs.SY_ZSLast = txtZSLast.Text;
                        sy_zs.SY_ZSContent = txtZSContent.Text;
                        sy.ZSList.Add(sy_zs);
                    }
                    #endregion
                    obj.SYContents.Add(sy);
                }

                obj.SYCount = obj.SYContents.Count.ToString();  //对应日期的试验个数=试验内容的个数
                #endregion
                #region workContent
                foreach (RepeaterItem it in rpWork.Items)
                {
                    wc = new ZXJH_WorkContent();
                    DropDownList ddlWC_Work = (DropDownList)it.FindControl("ddlWC_Work");
                    TextBox txtWC_SYID = (TextBox)it.FindControl("txtWC_SYID");
                    TextBox txtWC_StartTime = (TextBox)it.FindControl("txtWC_StartTime");
                    TextBox txtWC_MinTime = (TextBox)it.FindControl("txtWC_MinTime");
                    TextBox txtWC_MaxTime = (TextBox)it.FindControl("txtWC_MaxTime");

                    wc.Work = ddlWC_Work.SelectedItem.Text;
                    wc.SYID = txtWC_SYID.Text;
                    wc.StartTime = txtWC_StartTime.Text;
                    wc.MinTime = txtWC_MinTime.Text;
                    wc.MaxTime = txtWC_MaxTime.Text;
                    obj.WorkContents.Add(wc);
                }
                #endregion
                #region CommandMake
                foreach (RepeaterItem it in rpCommandMake.Items)
                {
                    cm = new ZXJH_CommandMake();
                    //TextBox txtWork_Command_SatID = (TextBox)it.FindControl("txtWork_Command_SatID");
                    ucs.ucSatellite txtWork_Command_SatID = (ucs.ucSatellite)it.FindControl("txtWork_Command_SatID");
                    TextBox txtWork_Command_SYID = (TextBox)it.FindControl("txtWork_Command_SYID");
                    TextBox txtWork_Command_Programe = (TextBox)it.FindControl("txtWork_Command_Programe");
                    TextBox txtWork_Command_FinishTime = (TextBox)it.FindControl("txtWork_Command_FinishTime");
                    TextBox txtWork_Command_UpWay = (TextBox)it.FindControl("txtWork_Command_UpWay");
                    TextBox txtWork_Command_UpTime = (TextBox)it.FindControl("txtWork_Command_UpTime");
                    TextBox txtWork_Command_Note = (TextBox)it.FindControl("txtWork_Command_Note");

                    cm.Work_Command_SatID = txtWork_Command_SatID.SelectedValue;
                    cm.Work_Command_SYID = txtWork_Command_SYID.Text;
                    cm.Work_Command_Programe = txtWork_Command_Programe.Text;
                    cm.Work_Command_FinishTime = txtWork_Command_FinishTime.Text;
                    cm.Work_Command_UpWay = txtWork_Command_UpWay.Text;
                    cm.Work_Command_UpTime = txtWork_Command_UpTime.Text;
                    cm.Work_Command_Note = txtWork_Command_Note.Text;
                    obj.CommandMakes.Add(cm);
                }
                #endregion
                #region SYDataHandle
                foreach (RepeaterItem it in rpSYDataHandle.Items)
                {
                    dh = new ZXJH_SYDataHandle();
                    TextBox txtSHSYID = (TextBox)it.FindControl("txtSHSYID");
                    TextBox txtSHSatID = (TextBox)it.FindControl("txtSHSatID");
                    TextBox txtSHLaps = (TextBox)it.FindControl("txtSHLaps");
                    TextBox txtMainStation = (TextBox)it.FindControl("txtMainStation");
                    TextBox txtSHMainStationEquipment = (TextBox)it.FindControl("txtSHMainStationEquipment");
                    TextBox txtBackStation = (TextBox)it.FindControl("txtBackStation");
                    TextBox txtSHBakStationEquipment = (TextBox)it.FindControl("txtSHBakStationEquipment");
                    TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                    TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                    TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");

                    dh.SYID = txtSHSYID.Text;
                    dh.SatID = txtSHSatID.Text;
                    dh.Laps = txtSHLaps.Text;
                    dh.Content = txtSHContent.Text;
                    dh.MainStation = txtMainStation.Text;
                    dh.MainStationEquipment = txtSHMainStationEquipment.Text;
                    dh.BakStation = txtBackStation.Text;
                    dh.BakStationEquipment = txtSHBakStationEquipment.Text;
                    dh.StartTime = txtSHStartTime.Text;
                    dh.EndTime = txtSHEndTime.Text;
                    obj.SYDataHandles.Add(dh);
                }
                #endregion
                #region DirectAndMonitor
                foreach (RepeaterItem it in rpDirectAndMonitor.Items)
                {
                    dam = new ZXJH_DirectAndMonitor();
                    TextBox txtDMSYID = (TextBox)it.FindControl("txtDMSYID");
                    TextBox txtDMStartTime = (TextBox)it.FindControl("txtDMStartTime");
                    TextBox txtDMEndTime = (TextBox)it.FindControl("txtDMEndTime");
                    DropDownList ddlDMRTTask = (DropDownList)it.FindControl("ddlDMRTTask");

                    dam.SYID = txtDMSYID.Text;
                    dam.StartTime = txtDMStartTime.Text;
                    dam.EndTime = txtDMEndTime.Text;
                    dam.RealTimeDemoTask = ddlDMRTTask.SelectedItem.Text;
                    obj.DirectAndMonitors.Add(dam);
                }
                #endregion
                #region RealTimeControl
                foreach (RepeaterItem it in rpRealTimeControl.Items)
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
                    obj.RealTimeControls.Add(rc);
                }
                #endregion
                #region SYEstimate
                foreach (RepeaterItem it in rpSYEstimate.Items)
                {
                    sye = new ZXJH_SYEstimate();
                    TextBox txtESYID = (TextBox)it.FindControl("txtESYID");
                    TextBox txtEStartTime = (TextBox)it.FindControl("txtEStartTime");
                    TextBox txtEEndTime = (TextBox)it.FindControl("txtEEndTime");

                    sye.SYID = txtESYID.Text;
                    sye.StartTime = txtEStartTime.Text;
                    sye.EndTime = txtEEndTime.Text;
                    obj.SYEstimates.Add(sye);
                }
                #endregion

                PlanFileCreator creater = new PlanFileCreator();

                obj.TaskID = ucTask1.SelectedItem.Value;
                obj.SatID = ucSatellite1.SelectedItem.Value;
                //检查文件是否已经存在
                if (creater.TestZXJHFileName(obj))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "File", "<script type='text/javascript'>showMsg('存在同名文件，请一分钟后重试');</script>");
                    return;
                }
                string filepath = creater.CreateZXJHFile(obj, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                {
                    TaskID = obj.TaskID,
                    PlanType = "ZXJH",
                    PlanID = (new Sequence()).GetZXJHSequnce(),
                    StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                    EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = obj.SatID,
                    Reserve = txtNote.Text
                };
                var result = jh.Add();

                //删除当前临时计划
                DataAccessLayer.PlanManage.JH jhtemp = new DataAccessLayer.PlanManage.JH(true)
                {
                    Id = Convert.ToInt32(HfID.Value),
                };
                var resulttemp = jhtemp.DeleteTempJH();

                #region 转成正式计划之后，禁用除“返回”之外的所有按钮
                btnSubmit.Visible = false;
                btnSaveTo.Visible = false;
                btnReset.Visible = false;
                btnFormal.Visible = false;

                #endregion

                ltMessage.Text = "计划保存成功";
                txtSYCount.Text = obj.SYCount;
            }
            catch (Exception ex)
            {
                throw (new AspNetException("保存计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        /// <summary>
        /// 上传用户文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileUpload1.FileName))
            {
                return;
            }

            string filename = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('\\') + 1);
            string filepath = GetFullFilePath(filename);
            hfStationFile.Value = filepath;

            FileUpload1.SaveAs(filepath);
            lblUpload.Visible = true;

            #region 读取文件内容
            StationInOutFileReader reader = new StationInOutFileReader();
            List<StationInOut> list;
            list = reader.Read(filename);

            rpDatas.DataSource = list;
            rpDatas.DataBind();
            #endregion

            ClientScript.RegisterStartupScript(this.GetType(), "File", "<script type='text/javascript'>showFileContentForm();</script>");
        }

        /// <summary>
        /// 获得完整路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string GetFullFilePath(string filename)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["StationInOutFilePath"];
            if (path != string.Empty)
            {
                if (path[path.Length - 1] != '\\')
                    path = path + @"\";
            }
            else
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"TempJHSavePath\";
            }
            path += filename;
            return path;
        }

        protected void btnGetStationData_Click(object sender, EventArgs e)
        {
            string filepath = hfStationFile.Value;  //文件路径
            string ids = txtIds.Text;   //行号

            System.IO.File.Delete(filepath);    //删除临时文件
        }

        protected void rpSYContentSC_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_SYContent_SC> list2 = new List<ZXJH_SYContent_SC>();
                ZXJH_SYContent_SC sy;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    sy = new ZXJH_SYContent_SC();
                    #region 数传
                    TextBox txtSCStationNO = (TextBox)it.FindControl("txtSCStationNO");
                    TextBox txtSCEquipmentNO = (TextBox)it.FindControl("txtSCEquipmentNO");
                    DropDownList ddlSCFrequencyBand = (DropDownList)it.FindControl("ddlSCFrequencyBand");
                    TextBox txtSCLaps = (TextBox)it.FindControl("txtSCLaps");
                    TextBox txtSCStartTime = (TextBox)it.FindControl("txtSCStartTime");
                    TextBox txtSCEndTime = (TextBox)it.FindControl("txtSCEndTime");

                    sy.SY_SCStationNO = txtSCStationNO.Text;
                    sy.SY_SCEquipmentNO = txtSCEquipmentNO.Text;
                    sy.SY_SCFrequencyBand = ddlSCFrequencyBand.SelectedValue;
                    sy.SY_SCLaps = txtSCLaps.Text;
                    sy.SY_SCStartTime = txtSCStartTime.Text;
                    sy.SY_SCEndTime = txtSCEndTime.Text;
                    #endregion
                    list2.Add(sy);
                }
                sy = new ZXJH_SYContent_SC();
                list2.Add(sy);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_SYContent_SC> list2 = new List<ZXJH_SYContent_SC>();
                ZXJH_SYContent_SC sy;
                Repeater rp = (Repeater)source;
                if (rp.Items.Count <= 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "del", "<script type='text/javascript'>showMsg('最后一条，无法删除!');</script>");
                }
                else
                {
                    foreach (RepeaterItem it in rp.Items)
                    {
                        if (e.Item.ItemIndex != it.ItemIndex)
                        {
                            sy = new ZXJH_SYContent_SC();
                            #region 数传
                            TextBox txtSCStationNO = (TextBox)it.FindControl("txtSCStationNO");
                            TextBox txtSCEquipmentNO = (TextBox)it.FindControl("txtSCEquipmentNO");
                            DropDownList ddlSCFrequencyBand = (DropDownList)it.FindControl("ddlSCFrequencyBand");
                            //TextBox txtSCFrequencyBand = (TextBox)it.FindControl("txtSCFrequencyBand");
                            TextBox txtSCLaps = (TextBox)it.FindControl("txtSCLaps");
                            TextBox txtSCStartTime = (TextBox)it.FindControl("txtSCStartTime");
                            TextBox txtSCEndTime = (TextBox)it.FindControl("txtSCEndTime");

                            sy.SY_SCStationNO = txtSCStationNO.Text;
                            sy.SY_SCEquipmentNO = txtSCEquipmentNO.Text;
                            sy.SY_SCFrequencyBand = ddlSCFrequencyBand.SelectedValue;
                            sy.SY_SCLaps = txtSCLaps.Text;
                            sy.SY_SCStartTime = txtSCStartTime.Text;
                            sy.SY_SCEndTime = txtSCEndTime.Text;
                            #endregion
                            list2.Add(sy);
                        }
                    }
                    rp.DataSource = list2;
                    rp.DataBind();
                }
            }
        }
        protected void rpSYContentSC_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //DataRowView rowv = (DataRowView)e.Item.DataItem;
                    ZXJH_SYContent_SC sy = (ZXJH_SYContent_SC)e.Item.DataItem;
                    DropDownList ddlSCFrequencyBand = (DropDownList)e.Item.FindControl("ddlSCFrequencyBand") as DropDownList;

                    ddlSCFrequencyBand.SelectedValue = sy.SY_SCFrequencyBand;
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定地面计划信息出现异常，异常原因", ex));
            }
            finally { }
        }
        protected void rpSYContentCK_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_SYContent_CK> list2 = new List<ZXJH_SYContent_CK>();
                ZXJH_SYContent_CK sy;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    sy = new ZXJH_SYContent_CK();
                    #region 测控
                    TextBox txtCKStationNO = (TextBox)it.FindControl("txtCKStationNO");
                    TextBox txtCKEquipmentNO = (TextBox)it.FindControl("txtCKEquipmentNO");
                    TextBox txtCKStartTime = (TextBox)it.FindControl("txtCKStartTime");
                    TextBox txtCKEndTime = (TextBox)it.FindControl("txtCKEndTime");
                    TextBox txtCKLaps = (TextBox)it.FindControl("txtCKLaps");

                    sy.SY_CKStationNO = txtCKStationNO.Text;
                    sy.SY_CKEquipmentNO = txtCKEquipmentNO.Text;
                    sy.SY_CKStartTime = txtCKStartTime.Text;
                    sy.SY_CKEndTime = txtCKEndTime.Text;
                    sy.SY_CKLaps = txtCKLaps.Text;
                    #endregion
                    list2.Add(sy);
                }
                sy = new ZXJH_SYContent_CK();
                list2.Add(sy);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_SYContent_CK> list2 = new List<ZXJH_SYContent_CK>();
                ZXJH_SYContent_CK sy;
                Repeater rp = (Repeater)source;
                if (rp.Items.Count <= 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "del", "<script type='text/javascript'>showMsg('最后一条，无法删除!');</script>");
                }
                else
                {
                    foreach (RepeaterItem it in rp.Items)
                    {
                        if (e.Item.ItemIndex != it.ItemIndex)
                        {
                            sy = new ZXJH_SYContent_CK();
                            #region 测控
                            TextBox txtCKStationNO = (TextBox)it.FindControl("txtCKStationNO");
                            TextBox txtCKEquipmentNO = (TextBox)it.FindControl("txtCKEquipmentNO");
                            TextBox txtCKStartTime = (TextBox)it.FindControl("txtCKStartTime");
                            TextBox txtCKEndTime = (TextBox)it.FindControl("txtCKEndTime");
                            TextBox txtCKLaps = (TextBox)it.FindControl("txtCKLaps");

                            sy.SY_CKStationNO = txtCKStationNO.Text;
                            sy.SY_CKEquipmentNO = txtCKEquipmentNO.Text;
                            sy.SY_CKStartTime = txtCKStartTime.Text;
                            sy.SY_CKEndTime = txtCKEndTime.Text;
                            sy.SY_CKLaps = txtCKLaps.Text;
                            #endregion
                            list2.Add(sy);
                        }
                    }
                    rp.DataSource = list2;
                    rp.DataBind();
                }
            }
        }
        protected void rpSYContentZS_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ZXJH_SYContent_ZS> list2 = new List<ZXJH_SYContent_ZS>();
                ZXJH_SYContent_ZS sy;
                Repeater rp = (Repeater)source;
                foreach (RepeaterItem it in rp.Items)
                {
                    sy = new ZXJH_SYContent_ZS();
                    #region 注数
                    TextBox txtZSFirst = (TextBox)it.FindControl("txtZSFirst");
                    TextBox txtZSLast = (TextBox)it.FindControl("txtZSLast");
                    TextBox txtZSContent = (TextBox)it.FindControl("txtZSContent");

                    sy.SY_ZSFirst = txtZSFirst.Text;
                    sy.SY_ZSLast = txtZSLast.Text;
                    sy.SY_ZSContent = txtZSContent.Text;

                    #endregion
                    list2.Add(sy);
                }
                sy = new ZXJH_SYContent_ZS();
                list2.Add(sy);
                rp.DataSource = list2;
                rp.DataBind();

            }
            if (e.CommandName == "Del")
            {
                List<ZXJH_SYContent_ZS> list2 = new List<ZXJH_SYContent_ZS>();
                ZXJH_SYContent_ZS sy;
                Repeater rp = (Repeater)source;
                if (rp.Items.Count <= 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "del", "<script type='text/javascript'>showMsg('最后一条，无法删除!');</script>");
                }
                else
                {
                    foreach (RepeaterItem it in rp.Items)
                    {
                        if (e.Item.ItemIndex != it.ItemIndex)
                        {
                            sy = new ZXJH_SYContent_ZS();
                            #region 注数
                            TextBox txtZSFirst = (TextBox)it.FindControl("txtZSFirst");
                            TextBox txtZSLast = (TextBox)it.FindControl("txtZSLast");
                            TextBox txtZSContent = (TextBox)it.FindControl("txtZSContent");

                            sy.SY_ZSFirst = txtZSFirst.Text;
                            sy.SY_ZSLast = txtZSLast.Text;
                            sy.SY_ZSContent = txtZSContent.Text;

                            #endregion
                            list2.Add(sy);
                        }
                    }
                    rp.DataSource = list2;
                    rp.DataBind();
                }
            }
        }

        private bool CheckCtrlExits(Control ctrl, string msg)
        {
            if (ctrl != null)
                return true;
            else
            {
                ltMessage.Text = string.Format("{0}的内容不能为空", msg);
                return false;
            }
        }

        protected void rpCommandMake_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ZXJH_CommandMake wc = (ZXJH_CommandMake)e.Item.DataItem;
                    ucs.ucSatellite txtWork_Command_SatID = (ucs.ucSatellite)e.Item.FindControl("txtWork_Command_SatID") as ucs.ucSatellite;

                    txtWork_Command_SatID.SelectedValue = wc.Work_Command_SatID;
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定地面计划信息出现异常，异常原因", ex));
            }
            finally { }
        }
    }
}