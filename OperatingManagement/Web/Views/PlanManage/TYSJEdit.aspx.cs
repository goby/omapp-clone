using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

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
        bool isTempJH = false;  //是否为临时计划,默认为正式计划

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnFormal.Visible = false; 
                txtStartTime.Attributes.Add("readonly", "true");
                txtEndTime.Attributes.Add("readonly", "true");
                ddlSatName_SelectedIndexChanged(null, null);
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
                    hfURL.Value = "?type=TYSJ&startDate=" + Request.QueryString["startDate"] + "&endDate=" + Request.QueryString["endDate"];
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
        private void BindJhTable(string sID)
        {
            try
            {
                isTempJH = GetIsTempJHValue();

                List<JH> jh = (new JH(isTempJH)).SelectByIDS(sID);
                HfFileIndex.Value = jh[0].FileIndex;
                hfTaskID.Value = jh[0].TaskID.ToString();
                ucTask1.SelectedValue = jh[0].TaskID.ToString();
                txtJXH.Text = jh[0].PlanID.ToString("0000");
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
            catch (Exception ex)
            {
                throw (new AspNetException("绑定计划基本信息出现异常，异常原因", ex));
            }
            finally { }
        }
        private void BindXML()
        {
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(HfFileIndex.Value);
                XmlNode root = xmlDoc.SelectSingleNode("仿真推演试验数据/SatName");
                ddlSatName.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("仿真推演试验数据/Type");
                ddlType.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("仿真推演试验数据/TestItem");
                ddlTestItem.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("仿真推演试验数据/StartTime");
                //txtStartTime.Text = root.InnerText;
                txtStartTime.Text = root.InnerText;
                //ucStartTimer.Timer = root.InnerText.Substring(8);
                root = xmlDoc.SelectSingleNode("仿真推演试验数据/EndTime");
                txtEndTime.Text = root.InnerText;
                //ucEndTimer.Timer = root.InnerText.Substring(8);
                root = xmlDoc.SelectSingleNode("仿真推演试验数据/Condition");
                txtCondition.Text = root.InnerText;
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定仿真推演试验计划信息出现异常，异常原因", ex));
            }
            finally { }
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMPLAN_Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/TYSJEdit.aspx.js");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                isTempJH = GetIsTempJHValue();

                TYSJ objTYSJ = new TYSJ();
                objTYSJ.SatName = ddlSatName.SelectedItem.Text;
                objTYSJ.Type = ddlType.SelectedItem.Text;
                objTYSJ.TestItem = ddlTestItem.SelectedItem.Text;
                objTYSJ.StartTime = txtStartTime.Text;
                objTYSJ.EndTime = txtEndTime.Text;
                objTYSJ.Condition = txtCondition.Text;
                objTYSJ.TaskID = ucTask1.SelectedItem.Value;
                objTYSJ.SatID = ucSatellite1.SelectedItem.Value;
                CultureInfo provider = CultureInfo.InvariantCulture;

                PlanFileCreator creater = new PlanFileCreator(isTempJH);
                if (hfStatus.Value == "new")
                {
                    string filepath = creater.CreateTYSJFile(objTYSJ, 0);
                    DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                    {
                        TaskID = objTYSJ.TaskID,
                        PlanType = "TYSJ",
                        PlanID = (new Sequence()).GetTYSJSequnce(),
                        //StartTime = DateTime.ParseExact(txtStartTime.Text.Trim(), "yyyyMMddHHmmss", provider),
                        //EndTime = DateTime.ParseExact(txtEndTime.Text.Trim(), "yyyyMMddHHmmss", provider),
                        StartTime = DateTime.ParseExact(objTYSJ.StartTime, "yyyyMMddHHmmss", provider),
                        EndTime = DateTime.ParseExact(objTYSJ.EndTime, "yyyyMMddHHmmss", provider),
                        SRCType = 0,
                        FileIndex = filepath,
                        SatID = objTYSJ.SatID,
                        Reserve = txtNote.Text
                    };
                    var result = jh.Add();
                    txtJXH.Text = jh.PlanID.ToString("0000");
                }
                else
                {
                    //当任务和卫星更新时，需要更新文件名称
                    if (hfSatID.Value != ucSatellite1.SelectedValue || hfTaskID.Value != ucTask1.SelectedValue)
                    {
                        string filepath = creater.CreateTYSJFile(objTYSJ, 0);

                        DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                        {
                            Id = Convert.ToInt32(HfID.Value),
                            TaskID = objTYSJ.TaskID,
                            StartTime = DateTime.ParseExact(objTYSJ.StartTime, "yyyyMMddHHmmss", provider),
                            EndTime = DateTime.ParseExact(objTYSJ.EndTime, "yyyyMMddHHmmss", provider),
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

                trMessage.Visible = true;
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

                TYSJ objTYSJ = new TYSJ();
                objTYSJ.SatName = ddlSatName.SelectedItem.Text;
                objTYSJ.Type = ddlType.SelectedItem.Text;
                objTYSJ.TestItem = ddlTestItem.SelectedItem.Text;
                objTYSJ.StartTime = txtStartTime.Text;
                objTYSJ.EndTime = txtEndTime.Text;
                objTYSJ.Condition = txtCondition.Text;
                CultureInfo provider = CultureInfo.InvariantCulture;

                PlanFileCreator creater = new PlanFileCreator(isTempJH);

                objTYSJ.TaskID = ucTask1.SelectedItem.Value;
                objTYSJ.SatID = ucSatellite1.SelectedItem.Value;
                int planid = (new Sequence()).GetTYSJSequnce();

                //检查文件是否已经存在
                if (creater.TestTYSJFileName(objTYSJ))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "File", "<script type='text/javascript'>showMsg('存在同名文件，请一分钟后重试');</script>");
                    return;
                }
                string filepath = creater.CreateTYSJFile(objTYSJ, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                {
                    TaskID = objTYSJ.TaskID,
                    PlanType = "TYSJ",
                    PlanID = planid,
                    StartTime = DateTime.ParseExact(objTYSJ.StartTime, "yyyyMMddHHmmss", provider),
                    EndTime = DateTime.ParseExact(objTYSJ.EndTime, "yyyyMMddHHmmss", provider),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = objTYSJ.SatID,
                    Reserve = txtNote.Text
                };
                var result = jh.Add();

                txtJXH.Text = planid.ToString();
                trMessage.Visible = true;
                ltMessage.Text = "计划保存成功";
                //ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
            }
            catch (Exception ex)
            {
                throw (new AspNetException("另存计划信息出现异常，异常原因", ex));
            }
            finally { }
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(HfID.Value))
                {
                    Page.Response.Redirect(Request.CurrentExecutionFilePath,false);
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

        protected void btnFormal_Click(object sender, EventArgs e)
        {
            try
            {
                TYSJ objTYSJ = new TYSJ();
                objTYSJ.SatName = ddlSatName.SelectedItem.Text;
                objTYSJ.Type = ddlType.SelectedItem.Text;
                objTYSJ.TestItem = ddlTestItem.SelectedItem.Text;
                objTYSJ.StartTime = txtStartTime.Text;
                objTYSJ.EndTime = txtEndTime.Text;
                objTYSJ.Condition = txtCondition.Text;
                CultureInfo provider = CultureInfo.InvariantCulture;

                PlanFileCreator creater = new PlanFileCreator();

                objTYSJ.TaskID = ucTask1.SelectedItem.Value;
                objTYSJ.SatID = ucSatellite1.SelectedItem.Value;
                int planid = (new Sequence()).GetTYSJSequnce();

                //检查文件是否已经存在
                if (creater.TestTYSJFileName(objTYSJ))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "File", "<script type='text/javascript'>showMsg('存在同名文件，请一分钟后重试');</script>");
                    return;
                }
                string filepath = creater.CreateTYSJFile(objTYSJ, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                {
                    TaskID = objTYSJ.TaskID,
                    PlanType = "TYSJ",
                    PlanID = planid,
                    StartTime = DateTime.ParseExact(objTYSJ.StartTime, "yyyyMMddHHmmss", provider),
                    EndTime = DateTime.ParseExact(objTYSJ.EndTime, "yyyyMMddHHmmss", provider),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = objTYSJ.SatID,
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

                txtJXH.Text = planid.ToString();
                trMessage.Visible = true;
                ltMessage.Text = "计划保存成功";
               
            }
            catch (Exception ex)
            {
                throw (new AspNetException("另存计划信息出现异常，异常原因", ex));
            }
            finally { }
        }
    }
}