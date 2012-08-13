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
using OperatingManagement.Framework.Storage;
using System.Web.Security;
using System.Xml;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class GZJHEdit : AspNetPage, IRouteContext
    {
        bool isTempJH = false;  //是否为临时计划,默认为正式计划

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartTime.Attributes.Add("readonly", "true");
                txtEndTime.Attributes.Add("readonly", "true");
                inital();
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    string sID = Request.QueryString["id"];
                    if (!string.IsNullOrEmpty(Request.QueryString["istemp"]) && Request.QueryString["istemp"] == "true")
                    {
                        isTempJH = true;
                        ViewState["isTempJH"] = true;
                    }

                    HfID.Value = sID;
                    hfStatus.Value = "edit";    //编辑
                    BindJhTable(sID);
                    BindXML();
                    hfURL.Value = "?type=GZJH&startDate=" + Request.QueryString["startDate"] + "&endDate=" + Request.QueryString["endDate"];
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

        private void inital()
        {
            //信息分类
            ddlXXFL.DataSource = PlanParameters.ReadParameters("GZJHXXFL");
            ddlXXFL.DataTextField = "Text";
            ddlXXFL.DataValueField = "Value";
            ddlXXFL.DataBind();
            //任务代号
            ddlDH.DataSource = PlanParameters.ReadParameters("GZJHDH");
            ddlDH.DataTextField = "Text";
            ddlDH.DataValueField = "Value";
            ddlDH.DataBind();
            //工作方式
            ddlFS.DataSource = PlanParameters.ReadParameters("DJZYSQFS");
            ddlFS.DataTextField = "Text";
            ddlFS.DataValueField = "Value";
            ddlFS.DataBind();
            //计划性质
            ddlJXZ.DataSource = PlanParameters.ReadParameters("DJZYSQSXZ");
            ddlJXZ.DataTextField = "Text";
            ddlJXZ.DataValueField = "Value";
            ddlJXZ.DataBind();
            //设备工作模式
            ddlMS.DataSource = PlanParameters.ReadParameters("GZJHMS");
            ddlMS.DataTextField = "Text";
            ddlMS.DataValueField = "Value";
            ddlMS.DataBind();
            //圈标
            ddlQB.DataSource = PlanParameters.ReadParameters("GZJHQB");
            ddlQB.DataTextField = "Text";
            ddlQB.DataValueField = "Value";
            ddlQB.DataBind();
            //工作性质
            ddlGXZ.DataSource = PlanParameters.ReadParameters("GZJHGXZ");
            ddlGXZ.DataTextField = "Text";
            ddlGXZ.DataValueField = "Value";
            ddlGXZ.DataBind();
            //信息类别标志
            ddlBID.DataSource = PlanParameters.ReadParameters("GZJHBID");
            ddlBID.DataTextField = "Text";
            ddlBID.DataValueField = "Value";
            ddlBID.DataBind();
                
        }
        private void BindJhTable(string sID)
        {
            try
            {
                isTempJH = GetIsTempJHValue();
                List<JH> jh = (new JH(isTempJH)).SelectByIDS(sID);
                HfFileIndex.Value = jh[0].FileIndex;
                hfTaskID.Value = jh[0].TaskID.ToString();
                txtJXH.Text = jh[0].PlanID.ToString("0000");
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
                XmlNode root = xmlDoc.SelectSingleNode("地面站工作计划/JXH");
                txtJXH.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/XXFL");
                ddlXXFL.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/DW");
                txtDW.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/SB");
                txtSB.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/QS");
                txtQS.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/QH");
                txtQH.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/DH");
                ddlDH.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/FS");
                ddlFS.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/JXZ");
                ddlJXZ.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/MS");
                ddlMS.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/QB");
                ddlQB.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/GXZ");
                ddlGXZ.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/ZHB");
                txtPreStartTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/GZK");
                txtTrackStartTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/GZJ");
                txtTrackEndTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/KSHX");
                txtWaveOnStartTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/GSHX");
                txtWaveOffStartTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/RK");
                txtStartTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/JS");
                txtEndTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/BID");
                ddlBID.SelectedValue = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/SBZ");
                txtJSBZ.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/RTs");
                txtTransStartTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/RTe");
                txtTransEndTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/SL");
                txtTransSpeedRate.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/HBZ");
                txtHBZ.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/Ts");
                DataStartTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("地面站工作计划/Te");
                DataEndTime.Text = root.InnerText;

            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定地面站工作计划出现异常，异常原因", ex));
            }
            finally { }
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "编辑计划";
            base.OnPageLoaded();
           //this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/GZJHEdit.aspx.js");

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                isTempJH = GetIsTempJHValue();

                GZJH obj = new GZJH();
                obj.SatID = ucSatellite1.SelectedValue;
                obj.TaskID = ucTask1.SelectedValue;
                //obj.JXH = txtJXH.Text;
                obj.XXFL = ddlXXFL.SelectedValue;
                obj.DW = txtDW.Text;
                obj.SB = txtSB.Text;
                obj.QS = txtQS.Text;
                obj.QH = txtQH.Text;
                obj.DH = ddlDH.SelectedValue;
                obj.FS = ddlFS.SelectedValue;
                obj.JXZ = ddlJXZ.SelectedValue;
                obj.MS = ddlMS.SelectedValue;
                obj.QB = ddlQB.SelectedValue;
                obj.GXZ = ddlGXZ.SelectedValue;
                obj.ZHB = txtPreStartTime.Text;
                obj.RK = txtStartTime.Text;
                obj.GZK = txtTrackStartTime.Text;
                obj.KSHX = txtWaveOnStartTime.Text;
                obj.GSHX = txtWaveOffStartTime.Text;
                obj.GZJ = txtTrackEndTime.Text;
                obj.JS = txtEndTime.Text;
                obj.BID = ddlBID.SelectedValue;
                obj.SBZ = txtJSBZ.Text;
                obj.RTs = txtTransStartTime.Text;
                obj.RTe = txtTransEndTime.Text;
                obj.SL = txtTransSpeedRate.Text;
                obj.HBZ = txtHBZ.Text;
                obj.Ts = DataStartTime.Text;
                obj.Te = DataEndTime.Text;
                //CultureInfo provider = CultureInfo.InvariantCulture;

                PlanFileCreator creater = new PlanFileCreator(isTempJH);

                if (hfStatus.Value == "new")
                {
                    //保存时才生成计划序号
                    obj.JXH = (new Sequence()).GetGZJHSequnce().ToString("0000");
                    txtJXH.Text = obj.JXH;
                    string filepath = creater.CreateGZJHFile(obj, 0);
                    DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                    {
                        TaskID = obj.TaskID,
                        PlanType = "GZJH",
                        PlanID = Convert.ToInt32(obj.JXH),
                        StartTime = Convert.ToDateTime(txtPlanStartTime.Text),
                        EndTime = Convert.ToDateTime(txtPlanEndTime.Text),
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
                        string filepath = creater.CreateGZJHFile(obj, 0);
                        DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                        {
                            Id = Convert.ToInt32(HfID.Value),
                            TaskID = obj.TaskID,
                            StartTime = Convert.ToDateTime(txtPlanStartTime.Text),
                            EndTime = Convert.ToDateTime(txtPlanEndTime.Text),
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
                        creater.CreateGZJHFile(obj, 1);
                    }
                }

                trMessage.Visible = true;
                ltMessage.Text = "计划保存成功";
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

                GZJH obj = new GZJH();
                obj.SatID = ucSatellite1.SelectedValue;
                obj.TaskID = ucTask1.SelectedValue;
                //obj.JXH = txtJXH.Text;
                obj.XXFL = ddlXXFL.SelectedValue;
                obj.DW = txtDW.Text;
                obj.SB = txtSB.Text;
                obj.QS = txtQS.Text;
                obj.QH = txtQH.Text;
                obj.DH = ddlDH.SelectedValue;
                obj.FS = ddlFS.SelectedValue;
                obj.JXZ = ddlJXZ.SelectedValue;
                obj.MS = ddlMS.SelectedValue;
                obj.QB = ddlQB.SelectedValue;
                obj.GXZ = ddlGXZ.SelectedValue;
                obj.ZHB = txtPreStartTime.Text;
                obj.RK = txtStartTime.Text;
                obj.GZK = txtTrackStartTime.Text;
                obj.KSHX = txtWaveOnStartTime.Text;
                obj.GSHX = txtWaveOffStartTime.Text;
                obj.GZJ = txtTrackEndTime.Text;
                obj.JS = txtEndTime.Text;
                obj.BID = ddlBID.SelectedValue;
                obj.SBZ = txtJSBZ.Text;
                obj.RTs = txtTransStartTime.Text;
                obj.RTe = txtTransEndTime.Text;
                obj.SL = txtTransSpeedRate.Text;
                obj.HBZ = txtHBZ.Text;
                obj.Ts = DataStartTime.Text;
                obj.Te = DataEndTime.Text;

                CultureInfo provider = CultureInfo.InvariantCulture;

                PlanFileCreator creater = new PlanFileCreator(isTempJH);

                obj.JXH = (new Sequence()).GetGZJHSequnce().ToString("0000");

                if (creater.TestGZJHFileName(obj))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "File", "<script type='text/javascript'>showMsg('存在同名文件，请一分钟后重试');</script>");
                    return;
                }

                string filepath = creater.CreateGZJHFile(obj, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH(isTempJH)
                {
                    TaskID = obj.TaskID,
                    PlanType = "GZJH",
                    PlanID = Convert.ToInt32(obj.JXH),
                    StartTime = Convert.ToDateTime(txtPlanStartTime.Text),
                    EndTime = Convert.ToDateTime(txtPlanEndTime.Text),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = obj.SatID,
                    Reserve = txtNote.Text
                };
                var result = jh.Add();

                txtJXH.Text = obj.JXH;  //另存后显示新的序号
                trMessage.Visible = true;
                ltMessage.Text = "计划保存成功";
               // ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
            }
            catch (Exception ex)
            {
                throw (new AspNetException("另存计划信息出现异常，异常原因", ex));
            }
            finally { }
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

    }
}