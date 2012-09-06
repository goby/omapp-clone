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
                    initial();
                }
            }
        }

        private void initial()
        {
            pnlMain.Visible = true;
            pnlStation.Visible = false;

            List<ZXJH_SYContent> listSY = new List<ZXJH_SYContent>();
            List<ZXJH_WorkContent> listWC = new List<ZXJH_WorkContent>();
            List<ZXJH_CommandMake> listCM = new List<ZXJH_CommandMake>();
            List<ZXJH_SYDataHandle> listDH = new List<ZXJH_SYDataHandle>();
            List<ZXJH_DirectAndMonitor> listDM = new List<ZXJH_DirectAndMonitor>();
            List<ZXJH_RealTimeControl> listRC = new List<ZXJH_RealTimeControl>();
            List<ZXJH_SYEstimate> listE = new List<ZXJH_SYEstimate>();

            listSY.Add(new ZXJH_SYContent());
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
            if (DateTime.Now > jh[0].StartTime)
            {
                btnSubmit.Visible = false;
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
                                sy.SY_SCStationNO = n["站编号"].InnerText;
                                sy.SY_SCEquipmentNO = n["设备编号"].InnerText;
                                sy.SY_SCFrequencyBand = n["频段"].InnerText;
                                sy.SY_SCLaps = n["圈次"].InnerText;
                                sy.SY_SCStartTime = n["开始时间"].InnerText;
                                sy.SY_SCEndTime = n["结束时间"].InnerText;
                                break;
                            case "测控":
                                sy.SY_CKStationNO = n["站编号"].InnerText;
                                sy.SY_CKEquipmentNO = n["设备编号"].InnerText;
                                sy.SY_CKLaps = n["圈次"].InnerText;
                                sy.SY_CKStartTime = n["开始时间"].InnerText;
                                sy.SY_CKEndTime = n["结束时间"].InnerText;
                                break;
                            case "注数":
                                sy.SY_ZSFirst = n["最早时间要求"].InnerText;
                                sy.SY_ZSLast = n["最晚时间要求"].InnerText;
                                sy.SY_ZSContent = n["主要内容"].InnerText;
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
                dh.MainStationEquipment = n["主站设备"].InnerText;
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
            this.PagePermission = "Plan.Edit";
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

                    wc.Work = ddlWC_Work.SelectedItem.Text;
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
                    TextBox txtSHMainStationEquipment = (TextBox)it.FindControl("txtSHMainStationEquipment");
                    TextBox txtSHBakStationEquipment = (TextBox)it.FindControl("txtSHBakStationEquipment");
                    TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                    TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                    TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");

                    dh.SYID = txtSHSYID.Text;
                    dh.SatID = txtSHSatID.Text;
                    dh.Laps = txtSHLaps.Text;
                    dh.Content = txtSHContent.Text;
                    dh.MainStationEquipment = txtSHMainStationEquipment.Text;
                    dh.BakStationEquipment = txtSHBakStationEquipment.Text;
                    dh.StartTime = txtSHStartTime.Text;
                    dh.EndTime = txtSHEndTime.Text;
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
                            TextBox txtSHMainStationEquipment = (TextBox)it.FindControl("txtSHMainStationEquipment");
                            TextBox txtSHBakStationEquipment = (TextBox)it.FindControl("txtSHBakStationEquipment");
                            TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                            TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                            TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");

                            dh.SYID = txtSHSYID.Text;
                            dh.SatID = txtSHSatID.Text;
                            dh.Laps = txtSHLaps.Text;
                            dh.Content = txtSHContent.Text;
                            dh.MainStationEquipment = txtSHMainStationEquipment.Text;
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

                    dm.SYID = txtDMSYID.Text;
                    dm.StartTime = txtDMStartTime.Text;
                    dm.EndTime = txtDMEndTime.Text;
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
                obj.SYCount = txtSYCount.Text;

                obj.SYContents = new List<ZXJH_SYContent>();
                obj.WorkContents = new List<ZXJH_WorkContent>();
                obj.CommandMakes = new List<ZXJH_CommandMake>();
                obj.SYDataHandles = new List<ZXJH_SYDataHandle>();
                obj.DirectAndMonitors = new List<ZXJH_DirectAndMonitor>();
                obj.RealTimeControls = new List<ZXJH_RealTimeControl>();
                obj.SYEstimates = new List<ZXJH_SYEstimate>();
                #endregion

                ZXJH_SYContent sy;
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
                    TextBox txtSYSatID = (TextBox)it.FindControl("txtSYSatID");
                    sy.SatID = txtSYSatID.Text;
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
                    obj.SYContents.Add(sy);
                }
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
                    TextBox txtWork_Command_SatID = (TextBox)it.FindControl("txtWork_Command_SatID");
                    TextBox txtWork_Command_SYID = (TextBox)it.FindControl("txtWork_Command_SYID");
                    TextBox txtWork_Command_Programe = (TextBox)it.FindControl("txtWork_Command_Programe");
                    TextBox txtWork_Command_FinishTime = (TextBox)it.FindControl("txtWork_Command_FinishTime");
                    TextBox txtWork_Command_UpWay = (TextBox)it.FindControl("txtWork_Command_UpWay");
                    TextBox txtWork_Command_UpTime = (TextBox)it.FindControl("txtWork_Command_UpTime");
                    TextBox txtWork_Command_Note = (TextBox)it.FindControl("txtWork_Command_Note");

                    cm.Work_Command_SatID = txtWork_Command_SatID.Text;
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
                    TextBox txtSHMainStationEquipment = (TextBox)it.FindControl("txtSHMainStationEquipment");
                    TextBox txtSHBakStationEquipment = (TextBox)it.FindControl("txtSHBakStationEquipment");
                    TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                    TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                    TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");

                    dh.SYID = txtSHSYID.Text;
                    dh.SatID = txtSHSatID.Text;
                    dh.Laps = txtSHLaps.Text;
                    dh.Content = txtSHContent.Text;
                    dh.MainStationEquipment = txtSHMainStationEquipment.Text;
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
                obj.SYCount = txtSYCount.Text;

                obj.SYContents = new List<ZXJH_SYContent>();
                obj.WorkContents = new List<ZXJH_WorkContent>();
                obj.CommandMakes = new List<ZXJH_CommandMake>();
                obj.SYDataHandles = new List<ZXJH_SYDataHandle>();
                obj.DirectAndMonitors = new List<ZXJH_DirectAndMonitor>();
                obj.RealTimeControls = new List<ZXJH_RealTimeControl>();
                obj.SYEstimates = new List<ZXJH_SYEstimate>();
                #endregion

                ZXJH_SYContent sy;
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
                    TextBox txtSYSatID = (TextBox)it.FindControl("txtSYSatID");
                    sy.SatID = txtSYSatID.Text;
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
                    obj.SYContents.Add(sy);
                }
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
                    TextBox txtWork_Command_SatID = (TextBox)it.FindControl("txtWork_Command_SatID");
                    TextBox txtWork_Command_SYID = (TextBox)it.FindControl("txtWork_Command_SYID");
                    TextBox txtWork_Command_Programe = (TextBox)it.FindControl("txtWork_Command_Programe");
                    TextBox txtWork_Command_FinishTime = (TextBox)it.FindControl("txtWork_Command_FinishTime");
                    TextBox txtWork_Command_UpWay = (TextBox)it.FindControl("txtWork_Command_UpWay");
                    TextBox txtWork_Command_UpTime = (TextBox)it.FindControl("txtWork_Command_UpTime");
                    TextBox txtWork_Command_Note = (TextBox)it.FindControl("txtWork_Command_Note");

                    cm.Work_Command_SatID = txtWork_Command_SatID.Text;
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
                    TextBox txtSHMainStationEquipment = (TextBox)it.FindControl("txtSHMainStationEquipment");
                    TextBox txtSHBakStationEquipment = (TextBox)it.FindControl("txtSHBakStationEquipment");
                    TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                    TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                    TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");

                    dh.SYID = txtSHSYID.Text;
                    dh.SatID = txtSHSatID.Text;
                    dh.Laps = txtSHLaps.Text;
                    dh.Content = txtSHContent.Text;
                    dh.MainStationEquipment = txtSHMainStationEquipment.Text;
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
                    TextBox txtSYSatID = (TextBox)it.FindControl("txtSYSatID");
                    sy.SatID = txtSYSatID.Text;
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
                            TextBox txtSYSatID = (TextBox)it.FindControl("txtSYSatID");
                            sy.SatID = txtSYSatID.Text;
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
                    TextBox txtWork_Command_SatID = (TextBox)it.FindControl("txtWork_Command_SatID");
                    TextBox txtWork_Command_SYID = (TextBox)it.FindControl("txtWork_Command_SYID");
                    TextBox txtWork_Command_Programe = (TextBox)it.FindControl("txtWork_Command_Programe");
                    TextBox txtWork_Command_FinishTime = (TextBox)it.FindControl("txtWork_Command_FinishTime");
                    TextBox txtWork_Command_UpWay = (TextBox)it.FindControl("txtWork_Command_UpWay");
                    TextBox txtWork_Command_UpTime = (TextBox)it.FindControl("txtWork_Command_UpTime");
                    TextBox txtWork_Command_Note = (TextBox)it.FindControl("txtWork_Command_Note");

                    obj.Work_Command_SatID = txtWork_Command_SatID.Text;
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
                            TextBox txtWork_Command_SatID = (TextBox)it.FindControl("txtWork_Command_SatID");
                            TextBox txtWork_Command_SYID = (TextBox)it.FindControl("txtWork_Command_SYID");
                            TextBox txtWork_Command_Programe = (TextBox)it.FindControl("txtWork_Command_Programe");
                            TextBox txtWork_Command_FinishTime = (TextBox)it.FindControl("txtWork_Command_FinishTime");
                            TextBox txtWork_Command_UpWay = (TextBox)it.FindControl("txtWork_Command_UpWay");
                            TextBox txtWork_Command_UpTime = (TextBox)it.FindControl("txtWork_Command_UpTime");
                            TextBox txtWork_Command_Note = (TextBox)it.FindControl("txtWork_Command_Note");

                            obj.Work_Command_SatID = txtWork_Command_SatID.Text;
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
                    //DataRowView rowv = (DataRowView)e.Item.DataItem;
                    ZXJH_SYContent sy = (ZXJH_SYContent)e.Item.DataItem;
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

        protected void btnFormal_Click(object sender, EventArgs e)
        {
            try
            {
                #region basic
                ZXJH obj = new ZXJH();
                obj.Date = txtDate.Text;
                obj.SYCount = txtSYCount.Text;

                obj.SYContents = new List<ZXJH_SYContent>();
                obj.WorkContents = new List<ZXJH_WorkContent>();
                obj.CommandMakes = new List<ZXJH_CommandMake>();
                obj.SYDataHandles = new List<ZXJH_SYDataHandle>();
                obj.DirectAndMonitors = new List<ZXJH_DirectAndMonitor>();
                obj.RealTimeControls = new List<ZXJH_RealTimeControl>();
                obj.SYEstimates = new List<ZXJH_SYEstimate>();
                #endregion

                ZXJH_SYContent sy;
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
                    TextBox txtSYSatID = (TextBox)it.FindControl("txtSYSatID");
                    sy.SatID = txtSYSatID.Text;
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
                    obj.SYContents.Add(sy);
                }
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
                    TextBox txtWork_Command_SatID = (TextBox)it.FindControl("txtWork_Command_SatID");
                    TextBox txtWork_Command_SYID = (TextBox)it.FindControl("txtWork_Command_SYID");
                    TextBox txtWork_Command_Programe = (TextBox)it.FindControl("txtWork_Command_Programe");
                    TextBox txtWork_Command_FinishTime = (TextBox)it.FindControl("txtWork_Command_FinishTime");
                    TextBox txtWork_Command_UpWay = (TextBox)it.FindControl("txtWork_Command_UpWay");
                    TextBox txtWork_Command_UpTime = (TextBox)it.FindControl("txtWork_Command_UpTime");
                    TextBox txtWork_Command_Note = (TextBox)it.FindControl("txtWork_Command_Note");

                    cm.Work_Command_SatID = txtWork_Command_SatID.Text;
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
                    TextBox txtSHMainStationEquipment = (TextBox)it.FindControl("txtSHMainStationEquipment");
                    TextBox txtSHBakStationEquipment = (TextBox)it.FindControl("txtSHBakStationEquipment");
                    TextBox txtSHContent = (TextBox)it.FindControl("txtSHContent");
                    TextBox txtSHStartTime = (TextBox)it.FindControl("txtSHStartTime");
                    TextBox txtSHEndTime = (TextBox)it.FindControl("txtSHEndTime");

                    dh.SYID = txtSHSYID.Text;
                    dh.SatID = txtSHSatID.Text;
                    dh.Laps = txtSHLaps.Text;
                    dh.Content = txtSHContent.Text;
                    dh.MainStationEquipment = txtSHMainStationEquipment.Text;
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
            }
            catch (Exception ex)
            {
                throw (new AspNetException("保存计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void btnStationOutIn_Click(object sender, EventArgs e)
        {
            pnlMain.Visible = false;
            pnlStation.Visible = true;
        }

        /// <summary>
        /// 上传用户文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string filename = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('\\') + 1);
            string filepath = GetFullFilePath(filename);

            FileUpload1.SaveAs(filepath);
            lblUpload.Visible = true;

            #region 读取文件内容
            StationInOutFileReader reader = new StationInOutFileReader();
            List<StationInOut> list;
            list = reader.Read(filename);
            #endregion
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
    }
}