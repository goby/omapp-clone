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
    public partial class YJJHEdit : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                    txtJXH.Text = (new Sequence()).GetYJJHSequnce().ToString("0000");   //新建时先给出计划序号
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
            string[] strTemp= jh[0].FileIndex.Split('_');
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
            XmlNode root = xmlDoc.SelectSingleNode("应用研究工作计划/XXFL");
            //txtXXFL.Text = root.InnerText;
            radBtnXXFL.SelectedValue = root.InnerText;
            root = xmlDoc.SelectSingleNode("应用研究工作计划/JXH");
            txtJXH.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("应用研究工作计划/SysName");
            //txtSysName.Text = root.InnerText;
            ddlSysName.SelectedValue = root.InnerText;
            root = xmlDoc.SelectSingleNode("应用研究工作计划/StartTime");
            txtStartTime.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("应用研究工作计划/EndTime");
            txtEndTime.Text = root.InnerText;
            root = xmlDoc.SelectSingleNode("应用研究工作计划/Task");
            txtTask.Text = root.InnerText;

        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
           //this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/YJJHEdit.aspx.js");

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            YJJH obj = new YJJH();
            obj.XXFL = radBtnXXFL.SelectedValue;
            obj.JXH = txtJXH.Text;
            obj.SysName = ddlSysName.SelectedValue;
            obj.StartTime = txtStartTime.Text;
            obj.EndTime = txtEndTime.Text;
            obj.Task = txtTask.Text;
            obj.TaskID = ucTask1.SelectedItem.Value;
            obj.SatID = ucSatellite1.SelectedItem.Value;

            PlanFileCreator creater = new PlanFileCreator();

            if (hfStatus.Value == "new")
            {
                //obj.JXH = (new Sequence()).GetYJJHSequnce().ToString("0000");
                string filepath = creater.CreateYJJHFile(obj, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                {
                    TaskID = obj.TaskID,
                    PlanType = "YJJH",
                    PlanID = Convert.ToInt32(obj.JXH),
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
                    string filepath = creater.CreateYJJHFile(obj, 0);
                    DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                    {
                        Id = Convert.ToInt32( HfID.Value),
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
                    creater.CreateYJJHFile(obj, 1);
                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
        }

        protected void btnSaveTo_Click(object sender, EventArgs e)
        {
            YJJH obj = new YJJH();
            obj.XXFL = radBtnXXFL.SelectedValue;
            //obj.JXH = txtJXH.Text;
            obj.SysName = ddlSysName.SelectedValue;
            obj.StartTime = txtStartTime.Text;
            obj.EndTime = txtEndTime.Text;
            obj.Task = txtTask.Text;

            PlanFileCreator creater = new PlanFileCreator();

                obj.TaskID = ucTask1.SelectedItem.Value;
                obj.SatID = ucSatellite1.SelectedItem.Value;
                obj.JXH = (new Sequence()).GetYJJHSequnce().ToString("0000");

                string filepath = creater.CreateYJJHFile(obj, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                {
                    TaskID = obj.TaskID,
                    PlanType = "YJJH",
                    PlanID = Convert.ToInt32(obj.JXH),
                    StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                    EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = obj.SatID,
                    Reserve = txtNote.Text
                };
                var result = jh.Add();

                txtJXH.Text = obj.JXH;  //另存后显示新的序号
                ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
       
        }

    }
}