﻿using System;
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
                ddlSatName_SelectedIndexChanged(null, null);
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    string sID = Request.QueryString["id"];
                    HfID.Value = sID;
                    hfStatus.Value = "edit";    //编辑
                    BindJhTable(sID);
                    BindXML();

                    if ("detail" == Request.QueryString["op"])
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script type='text/javascript'>hideAllButton();</script>");
                    }
                }
                else
                {
                    hfStatus.Value = "new"; //新建
                    btnSaveTo.Visible = false;
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
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HfFileIndex.Value);
            XmlNode root = xmlDoc.SelectSingleNode("仿真推演试验数据/SatName");
            ddlSatName.SelectedValue = root.InnerText;
            root = xmlDoc.SelectSingleNode("仿真推演试验数据/Type");
            ddlType.SelectedValue= root.InnerText;
            root = xmlDoc.SelectSingleNode("仿真推演试验数据/TestItem");
            ddlTestItem.SelectedValue = root.InnerText;
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
            this.AddJavaScriptInclude("scripts/pages/PlanManage/TYSJEdit.aspx.js");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TYSJ objTYSJ = new TYSJ();
            objTYSJ.SatName = ddlSatName.SelectedItem.Text;
            objTYSJ.Type = ddlType.SelectedItem.Text;
            objTYSJ.TestItem = ddlTestItem.SelectedItem.Text;
            objTYSJ.StartTime = txtStartTime.Text;
            objTYSJ.EndTime = txtEndTime.Text;
            objTYSJ.Condition = txtCondition.Text;
            objTYSJ.TaskID = ucTask1.SelectedItem.Value;
            objTYSJ.SatID = ucSatellite1.SelectedItem.Value;

            PlanFileCreator creater = new PlanFileCreator();
            if (hfStatus.Value == "new")
            {
                string filepath = creater.CreateTYSJFile(objTYSJ, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                {
                    TaskID = objTYSJ.TaskID,
                    PlanType = "TYSJ",
                    PlanID = (new Sequence()).GetTYSJSequnce(),
                    StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                    EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = objTYSJ.SatID,
                    Reserve = txtNote.Text
                };
                var result = jh.Add();
            }
            else
            {
                //当任务和卫星更新时，需要更新文件名称
                if (hfSatID.Value != ucSatellite1.SelectedValue || hfTaskID.Value != ucTask1.SelectedValue)
                {
                    string filepath = creater.CreateTYSJFile(objTYSJ, 0);

                    DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                    {
                        Id = Convert.ToInt32(HfID.Value),
                        TaskID = objTYSJ.TaskID,
                        StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                        EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                        FileIndex = filepath,
                        SatID = objTYSJ.SatID,
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
                    creater.CreateTYSJFile(objTYSJ, 1);
                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
        }

        protected void btnSaveTo_Click(object sender, EventArgs e)
        {
            TYSJ objTYSJ = new TYSJ();
            objTYSJ.SatName = ddlSatName.SelectedItem.Text;
            objTYSJ.Type = ddlType.SelectedItem.Text;
            objTYSJ.TestItem = ddlTestItem.SelectedItem.Text;
            objTYSJ.StartTime = txtStartTime.Text;
            objTYSJ.EndTime = txtEndTime.Text;
            objTYSJ.Condition = txtCondition.Text;

            PlanFileCreator creater = new PlanFileCreator();

                objTYSJ.TaskID = ucTask1.SelectedItem.Value;
                objTYSJ.SatID = ucSatellite1.SelectedItem.Value;
                string filepath = creater.CreateTYSJFile(objTYSJ, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                {
                    TaskID = objTYSJ.TaskID,
                    PlanType = "TYSJ",
                    PlanID = (new Sequence()).GetTYSJSequnce(),
                    StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                    EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = objTYSJ.SatID,
                    Reserve = txtNote.Text
                };
                var result = jh.Add();

                ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
        
        }

        /// <summary>
        /// 卫星名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSatName_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlSatName.SelectedItem.Text)
            {
                case "探索三号卫星":
                    ddlType.Items.Clear();
                    ddlType.Items.Add(new ListItem("GEO目标观测试验"));
                    ddlType.Items.Add(new ListItem("LEO目标成像试验"));
                    ddlType_SelectedIndexChanged(null, null);
                    break;
                case "探索四号卫星":
                    ddlType.Items.Clear();
                    ddlType.Items.Add(new ListItem("释放抓捕目标试验"));
                    ddlType.Items.Add(new ListItem("逼近停靠试验"));
                    ddlType.Items.Add(new ListItem("遥操作试验"));
                    ddlType_SelectedIndexChanged(null, null);
                    break;
                case "探索五号卫星":
                    ddlType.Items.Clear();
                    ddlType.Items.Add(new ListItem("远程自主快速机动试验"));
                    ddlType.Items.Add(new ListItem("TS-5-B卫星在轨施放试验"));
                    ddlType_SelectedIndexChanged(null, null);
                    break;
            }
        }
        /// <summary>
        /// 试验类别改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlType.SelectedItem.Text)
            {
                case "GEO目标观测试验":
                    ddlTestItem.Items.Clear();
                    ddlTestItem.Items.Add(new ListItem("自然交会观测试验"));
                    ddlTestItem.Items.Add(new ListItem("同步带凝视观测试验"));
                    ddlTestItem.Items.Add(new ListItem("天地基联合观测试验"));
                    ddlTestItem.Items.Add(new ListItem("其它扩展试验项目"));
                    break;
                case "LEO目标成像试验":
                    ddlTestItem.Items.Clear();
                    ddlTestItem.Items.Add(new ListItem("等待成像试验"));
                    ddlTestItem.Items.Add(new ListItem("自动跟踪成像试验"));
                    ddlTestItem.Items.Add(new ListItem("引导跟踪成像试验"));
                    ddlTestItem.Items.Add(new ListItem("其它扩展试验项目"));
                    break;
                case "释放抓捕目标试验":
                    ddlTestItem.Items.Clear();
                    ddlTestItem.Items.Add(new ListItem("空间机械手性能试验"));
                    ddlTestItem.Items.Add(new ListItem("空间机械手协调控制试验"));
                    ddlTestItem.Items.Add(new ListItem("空间机械手辅助分离试验"));
                    ddlTestItem.Items.Add(new ListItem("释放抓捕试验"));
                    ddlTestItem.Items.Add(new ListItem("其它扩展试验项目"));
                    break;
                case "逼近停靠试验":
                    ddlTestItem.Items.Clear();
                    ddlTestItem.Items.Add(new ListItem("合作目标逼近停靠试验"));
                    ddlTestItem.Items.Add(new ListItem("非合作目标跟踪接近试验"));
                    ddlTestItem.Items.Add(new ListItem("其它扩展试验项目"));
                    break;
                case "遥操作试验":
                    ddlTestItem.Items.Clear();
                    ddlTestItem.Items.Add(new ListItem("遥编程遥操作试验"));
                    ddlTestItem.Items.Add(new ListItem("主从遥操作试验"));
                    ddlTestItem.Items.Add(new ListItem("ORU更换模拟试验"));
                    ddlTestItem.Items.Add(new ListItem("其它扩展试验项目"));
                    break;
                case "远程自主快速机动试验":
                    ddlTestItem.Items.Clear();
                    ddlTestItem.Items.Add(new ListItem("自主调相轨控试验"));
                    ddlTestItem.Items.Add(new ListItem("闭环反馈轨控试验"));
                    ddlTestItem.Items.Add(new ListItem("远近程交班试验"));
                    ddlTestItem.Items.Add(new ListItem("近程接近伴飞试验"));
                    ddlTestItem.Items.Add(new ListItem("其它扩展试验项目"));
                    break;
                case "TS-5-B卫星在轨施放试验":
                    ddlTestItem.Items.Clear();
                    ddlTestItem.Items.Add(new ListItem("动基座惯性基准传递试验"));
                    ddlTestItem.Items.Add(new ListItem("在轨施放TS-5-B卫星试验"));
                    ddlTestItem.Items.Add(new ListItem("TS-5-B卫星捕获飞行试验"));
                    ddlTestItem.Items.Add(new ListItem("TS-5-B卫星离轨控制试验"));
                    ddlTestItem.Items.Add(new ListItem("其它扩展试验项目"));
                    break;
            }
        }
    }
}