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
using OperatingManagement.ServicesKernel.File;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class GZJHEdit : AspNetPage, IRouteContext
    {
        bool isTempJH = false;  //是否为临时计划,默认为正式计划

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnFormal.Visible = false; 
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
                    inital(false);
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
                    inital(true);
                }
            }
            
        }

        private void inital(bool isNew)
        {
            txtJXH.Attributes.Add("readonly", "true");
            txtQS.Attributes.Add("readonly", "true");

            //信息分类
            ddlXXFL.DataSource = PlanParameters.ReadParameters("GZJHXXFL");
            ddlXXFL.DataTextField = "Text";
            ddlXXFL.DataValueField = "Value";
            ddlXXFL.DataBind();

            if (isNew)
            {
                List<GZJH_Content> list = new List<GZJH_Content>();
                list.Add(new GZJH_Content());
                rpDatas.DataSource = list;
                rpDatas.DataBind();
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
                txtJXH.Text = jh[0].PlanID.ToString("0000");
                txtPlanStartTime.Text = jh[0].StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                txtPlanEndTime.Text = jh[0].EndTime.ToString("yyyy-MM-dd HH:mm:ss");
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
                root = xmlDoc.SelectSingleNode("地面站工作计划/QS");
                txtQS.Text = root.InnerText;

                #region Content
                root = xmlDoc.SelectSingleNode("地面站工作计划");
                List<GZJH_Content> list = new List<GZJH_Content>();
                GZJH_Content c;
                foreach (XmlNode n in root.ChildNodes)
                {
                    if (n.Name == "Content")
                    {
                        c = new GZJH_Content();
                        c.DW = n["DW"].InnerText;
                        c.SB = n["SB"].InnerText;
                        c.QH = n["QH"].InnerText;
                        c.DH = n["DH"].InnerText;
                        c.FS = n["FS"].InnerText;
                        c.JXZ = n["JXZ"].InnerText;
                        c.MS = n["MS"].InnerText;
                        c.QB = n["QB"].InnerText;
                        c.GXZ = n["GXZ"].InnerText;
                        c.ZHB = n["ZHB"].InnerText;
                        c.GZK = n["GZK"].InnerText;
                        c.GZJ = n["GZJ"].InnerText;
                        c.KSHX = n["KSHX"].InnerText;
                        c.GSHX = n["GSHX"].InnerText;
                        c.RK = n["RK"].InnerText;
                        c.JS = n["JS"].InnerText;
                        c.BID = n["BID"].InnerText;
                        c.SBZ = n["SBZ"].InnerText;
                        c.RTs = n["RTs"].InnerText;
                        c.RTe = n["RTe"].InnerText;
                        c.SL = n["SL"].InnerText;
                        c.HBID = n["HBID"].InnerText;
                        c.HBZ = n["HBZ"].InnerText;
                        c.Ts = n["Ts"].InnerText;
                        c.Te = n["Te"].InnerText;
                        c.HRTs = n["HRTs"].InnerText;
                        c.HSL = n["HSL"].InnerText;
                        list.Add(c);
                    }
                }
                rpDatas.DataSource = list;
                rpDatas.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定地面站工作计划出现异常，异常原因", ex));
            }
            finally { }
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMPLAN_Plan.Edit";
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
                //obj.QS = txtQS.Text;
                obj.GZJHContents = new List<GZJH_Content>();

                #region GZJH_Content
                GZJH_Content co;
                foreach (RepeaterItem it in rpDatas.Items)
                {
                    co = new GZJH_Content();
                    #region 赋值
                    TextBox txtDW = (TextBox)it.FindControl("txtDW");
                    TextBox txtSB = (TextBox)it.FindControl("txtSB");
                    DropDownList ddlDH = (DropDownList)it.FindControl("ddlDH");
                    DropDownList ddlFS = (DropDownList)it.FindControl("ddlFS");
                    DropDownList ddlJXZ = (DropDownList)it.FindControl("ddlJXZ");
                    DropDownList ddlMS = (DropDownList)it.FindControl("ddlMS");
                    DropDownList ddlQB = (DropDownList)it.FindControl("ddlQB");
                    DropDownList ddlGXZ = (DropDownList)it.FindControl("ddlGXZ");
                    TextBox txtPreStartTime = (TextBox)it.FindControl("txtPreStartTime");
                    TextBox txtQH = (TextBox)it.FindControl("txtQH");
                    TextBox txtTrackStartTime = (TextBox)it.FindControl("txtTrackStartTime");
                    TextBox txtTrackEndTime = (TextBox)it.FindControl("txtTrackEndTime");
                    TextBox txtWaveOnStartTime = (TextBox)it.FindControl("txtWaveOnStartTime");
                    TextBox txtWaveOffStartTime = (TextBox)it.FindControl("txtWaveOffStartTime");
                    TextBox txtStartTime = (TextBox)it.FindControl("txtStartTime");
                    TextBox txtEndTime = (TextBox)it.FindControl("txtEndTime");
                    DropDownList ddlBID = (DropDownList)it.FindControl("ddlBID");
                    TextBox txtJSBZ = (TextBox)it.FindControl("txtJSBZ");
                    TextBox txtTransStartTime = (TextBox)it.FindControl("txtTransStartTime");
                    TextBox txtTransEndTime = (TextBox)it.FindControl("txtTransEndTime");
                    TextBox txtTransSpeedRate = (TextBox)it.FindControl("txtTransSpeedRate");
                    TextBox txtHBZ = (TextBox)it.FindControl("txtHBZ");
                    TextBox DataStartTime = (TextBox)it.FindControl("DataStartTime");
                    TextBox DataEndTime = (TextBox)it.FindControl("DataEndTime");
                    DropDownList ddlSHBID = (DropDownList)it.FindControl("ddlSHBID");
                    TextBox txtSHTransStartTime = (TextBox)it.FindControl("txtSHTransStartTime");
                    TextBox txtHSL = (TextBox)it.FindControl("txtHSL");

                    co.DW = txtDW.Text;
                    co.SB = txtSB.Text;
                    co.DH = ddlDH.SelectedItem.Text;
                    co.FS = ddlFS.SelectedItem.Text;
                    co.JXZ = ddlJXZ.SelectedItem.Text;
                    co.MS = ddlMS.SelectedItem.Text;
                    co.QB = ddlQB.SelectedItem.Text;
                    co.GXZ = ddlGXZ.SelectedItem.Text;
                    co.ZHB = txtPreStartTime.Text;
                    co.QH = txtQH.Text;
                    co.GZK = txtTrackStartTime.Text;
                    co.GZJ = txtTrackEndTime.Text;
                    co.KSHX = txtWaveOnStartTime.Text;
                    co.GSHX = txtWaveOffStartTime.Text;
                    co.RK = txtStartTime.Text;
                    co.JS = txtEndTime.Text;
                    co.BID = ddlBID.SelectedItem.Text;
                    co.SBZ = txtJSBZ.Text;
                    co.RTs = txtTransStartTime.Text;
                    co.RTe = txtTransEndTime.Text;
                    co.SL = txtTransSpeedRate.Text;
                    co.HBID = ddlSHBID.SelectedItem.Text;
                    co.HBZ = txtHBZ.Text;
                    co.Ts = DataStartTime.Text;
                    co.Te = DataEndTime.Text;
                    co.HRTs = txtSHTransStartTime.Text;
                    co.HSL = txtHSL.Text;
                    #endregion
                    obj.GZJHContents.Add(co);
                }
                obj.QS = obj.GZJHContents.Count.ToString(); //圈数，自动计算得到
                #endregion
                
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
                //obj.QS = txtQS.Text;
                obj.GZJHContents = new List<GZJH_Content>();

                #region GZJH_Content
                GZJH_Content co;
                foreach (RepeaterItem it in rpDatas.Items)
                {
                    co = new GZJH_Content();
                    #region 赋值
                    TextBox txtDW = (TextBox)it.FindControl("txtDW");
                    TextBox txtSB = (TextBox)it.FindControl("txtSB");
                    DropDownList ddlDH = (DropDownList)it.FindControl("ddlDH");
                    DropDownList ddlFS = (DropDownList)it.FindControl("ddlFS");
                    DropDownList ddlJXZ = (DropDownList)it.FindControl("ddlJXZ");
                    DropDownList ddlMS = (DropDownList)it.FindControl("ddlMS");
                    DropDownList ddlQB = (DropDownList)it.FindControl("ddlQB");
                    DropDownList ddlGXZ = (DropDownList)it.FindControl("ddlGXZ");
                    TextBox txtPreStartTime = (TextBox)it.FindControl("txtPreStartTime");
                    TextBox txtQH = (TextBox)it.FindControl("txtQH");
                    TextBox txtTrackStartTime = (TextBox)it.FindControl("txtTrackStartTime");
                    TextBox txtTrackEndTime = (TextBox)it.FindControl("txtTrackEndTime");
                    TextBox txtWaveOnStartTime = (TextBox)it.FindControl("txtWaveOnStartTime");
                    TextBox txtWaveOffStartTime = (TextBox)it.FindControl("txtWaveOffStartTime");
                    TextBox txtStartTime = (TextBox)it.FindControl("txtStartTime");
                    TextBox txtEndTime = (TextBox)it.FindControl("txtEndTime");
                    DropDownList ddlBID = (DropDownList)it.FindControl("ddlBID");
                    TextBox txtJSBZ = (TextBox)it.FindControl("txtJSBZ");
                    TextBox txtTransStartTime = (TextBox)it.FindControl("txtTransStartTime");
                    TextBox txtTransEndTime = (TextBox)it.FindControl("txtTransEndTime");
                    TextBox txtTransSpeedRate = (TextBox)it.FindControl("txtTransSpeedRate");
                    TextBox txtHBZ = (TextBox)it.FindControl("txtHBZ");
                    TextBox DataStartTime = (TextBox)it.FindControl("DataStartTime");
                    TextBox DataEndTime = (TextBox)it.FindControl("DataEndTime");
                    DropDownList ddlSHBID = (DropDownList)it.FindControl("ddlSHBID");
                    TextBox txtSHTransStartTime = (TextBox)it.FindControl("txtSHTransStartTime");
                    TextBox txtHSL = (TextBox)it.FindControl("txtHSL");

                    co.DW = txtDW.Text;
                    co.SB = txtSB.Text;
                    co.DH = ddlDH.SelectedItem.Text;
                    co.FS = ddlFS.SelectedItem.Text;
                    co.JXZ = ddlJXZ.SelectedItem.Text;
                    co.MS = ddlMS.SelectedItem.Text;
                    co.QB = ddlQB.SelectedItem.Text;
                    co.GXZ = ddlGXZ.SelectedItem.Text;
                    co.ZHB = txtPreStartTime.Text;
                    co.QH = txtQH.Text;
                    co.GZK = txtTrackStartTime.Text;
                    co.GZJ = txtTrackEndTime.Text;
                    co.KSHX = txtWaveOnStartTime.Text;
                    co.GSHX = txtWaveOffStartTime.Text;
                    co.RK = txtStartTime.Text;
                    co.JS = txtEndTime.Text;
                    co.BID = ddlBID.SelectedItem.Text;
                    co.SBZ = txtJSBZ.Text;
                    co.RTs = txtTransStartTime.Text;
                    co.RTe = txtTransEndTime.Text;
                    co.SL = txtTransSpeedRate.Text;
                    co.HBID = ddlSHBID.SelectedItem.Text;
                    co.HBZ = txtHBZ.Text;
                    co.Ts = DataStartTime.Text;
                    co.Te = DataEndTime.Text;
                    co.HRTs = txtSHTransStartTime.Text;
                    co.HSL = txtHSL.Text;
                    #endregion
                    obj.GZJHContents.Add(co);
                }
                obj.QS = obj.GZJHContents.Count.ToString(); //圈数，自动计算得到
                #endregion

                //CultureInfo provider = CultureInfo.InvariantCulture;

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

        protected void btnFormal_Click(object sender, EventArgs e)
        {
            try
            {
                GZJH obj = new GZJH();
                obj.SatID = ucSatellite1.SelectedValue;
                obj.TaskID = ucTask1.SelectedValue;
                //obj.JXH = txtJXH.Text;
                obj.XXFL = ddlXXFL.SelectedValue;
                //obj.QS = txtQS.Text;
                obj.GZJHContents = new List<GZJH_Content>();

                #region GZJH_Content
                GZJH_Content co;
                foreach (RepeaterItem it in rpDatas.Items)
                {
                    co = new GZJH_Content();
                    #region 赋值
                    TextBox txtDW = (TextBox)it.FindControl("txtDW");
                    TextBox txtSB = (TextBox)it.FindControl("txtSB");
                    DropDownList ddlDH = (DropDownList)it.FindControl("ddlDH");
                    DropDownList ddlFS = (DropDownList)it.FindControl("ddlFS");
                    DropDownList ddlJXZ = (DropDownList)it.FindControl("ddlJXZ");
                    DropDownList ddlMS = (DropDownList)it.FindControl("ddlMS");
                    DropDownList ddlQB = (DropDownList)it.FindControl("ddlQB");
                    DropDownList ddlGXZ = (DropDownList)it.FindControl("ddlGXZ");
                    TextBox txtPreStartTime = (TextBox)it.FindControl("txtPreStartTime");
                    TextBox txtQH = (TextBox)it.FindControl("txtQH");
                    TextBox txtTrackStartTime = (TextBox)it.FindControl("txtTrackStartTime");
                    TextBox txtTrackEndTime = (TextBox)it.FindControl("txtTrackEndTime");
                    TextBox txtWaveOnStartTime = (TextBox)it.FindControl("txtWaveOnStartTime");
                    TextBox txtWaveOffStartTime = (TextBox)it.FindControl("txtWaveOffStartTime");
                    TextBox txtStartTime = (TextBox)it.FindControl("txtStartTime");
                    TextBox txtEndTime = (TextBox)it.FindControl("txtEndTime");
                    DropDownList ddlBID = (DropDownList)it.FindControl("ddlBID");
                    TextBox txtJSBZ = (TextBox)it.FindControl("txtJSBZ");
                    TextBox txtTransStartTime = (TextBox)it.FindControl("txtTransStartTime");
                    TextBox txtTransEndTime = (TextBox)it.FindControl("txtTransEndTime");
                    TextBox txtTransSpeedRate = (TextBox)it.FindControl("txtTransSpeedRate");
                    TextBox txtHBZ = (TextBox)it.FindControl("txtHBZ");
                    TextBox DataStartTime = (TextBox)it.FindControl("DataStartTime");
                    TextBox DataEndTime = (TextBox)it.FindControl("DataEndTime");
                    DropDownList ddlSHBID = (DropDownList)it.FindControl("ddlSHBID");
                    TextBox txtSHTransStartTime = (TextBox)it.FindControl("txtSHTransStartTime");
                    TextBox txtHSL = (TextBox)it.FindControl("txtHSL");

                    co.DW = txtDW.Text;
                    co.SB = txtSB.Text;
                    co.DH = ddlDH.SelectedItem.Text;
                    co.FS = ddlFS.SelectedItem.Text;
                    co.JXZ = ddlJXZ.SelectedItem.Text;
                    co.MS = ddlMS.SelectedItem.Text;
                    co.QB = ddlQB.SelectedItem.Text;
                    co.GXZ = ddlGXZ.SelectedItem.Text;
                    co.ZHB = txtPreStartTime.Text;
                    co.QH = txtQH.Text;
                    co.GZK = txtTrackStartTime.Text;
                    co.GZJ = txtTrackEndTime.Text;
                    co.KSHX = txtWaveOnStartTime.Text;
                    co.GSHX = txtWaveOffStartTime.Text;
                    co.RK = txtStartTime.Text;
                    co.JS = txtEndTime.Text;
                    co.BID = ddlBID.SelectedItem.Text;
                    co.SBZ = txtJSBZ.Text;
                    co.RTs = txtTransStartTime.Text;
                    co.RTe = txtTransEndTime.Text;
                    co.SL = txtTransSpeedRate.Text;
                    co.HBID = ddlSHBID.SelectedItem.Text;
                    co.HBZ = txtHBZ.Text;
                    co.Ts = DataStartTime.Text;
                    co.Te = DataEndTime.Text;
                    co.HRTs = txtSHTransStartTime.Text;
                    co.HSL = txtHSL.Text;
                    #endregion
                    obj.GZJHContents.Add(co);
                }
                obj.QS = obj.GZJHContents.Count.ToString(); //圈数，自动计算得到
                #endregion


                PlanFileCreator creater = new PlanFileCreator();

                obj.JXH = (new Sequence()).GetGZJHSequnce().ToString("0000");

                if (creater.TestGZJHFileName(obj))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "File", "<script type='text/javascript'>showMsg('存在同名文件，请一分钟后重试');</script>");
                    return;
                }

                string filepath = creater.CreateGZJHFile(obj, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
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

                txtJXH.Text = obj.JXH;  //另存后显示新的序号
                trMessage.Visible = true;
                ltMessage.Text = "计划保存成功";
            }
            catch (Exception ex)
            {
                throw (new AspNetException("另存计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void rpDatas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Add")
                {
                    List<GZJH_Content> list = new List<GZJH_Content>();
                    GZJH_Content co;
                    Repeater rp = (Repeater)source;
                    ViewState["op"] = "Add";
                    foreach (RepeaterItem it in rp.Items)
                    {
                        #region 赋值
                        co = new GZJH_Content();
                        TextBox txtDW = (TextBox)it.FindControl("txtDW");
                        TextBox txtSB = (TextBox)it.FindControl("txtSB");
                        DropDownList ddlDH = (DropDownList)it.FindControl("ddlDH");
                        DropDownList ddlFS = (DropDownList)it.FindControl("ddlFS");
                        DropDownList ddlJXZ = (DropDownList)it.FindControl("ddlJXZ");
                        DropDownList ddlMS = (DropDownList)it.FindControl("ddlMS");
                        DropDownList ddlQB = (DropDownList)it.FindControl("ddlQB");
                        DropDownList ddlGXZ = (DropDownList)it.FindControl("ddlGXZ");
                        TextBox txtPreStartTime = (TextBox)it.FindControl("txtPreStartTime");
                        TextBox txtQH = (TextBox)it.FindControl("txtQH");
                        TextBox txtTrackStartTime = (TextBox)it.FindControl("txtTrackStartTime");
                        TextBox txtTrackEndTime = (TextBox)it.FindControl("txtTrackEndTime");
                        TextBox txtWaveOnStartTime = (TextBox)it.FindControl("txtWaveOnStartTime");
                        TextBox txtWaveOffStartTime = (TextBox)it.FindControl("txtWaveOffStartTime");
                        TextBox txtStartTime = (TextBox)it.FindControl("txtStartTime");
                        TextBox txtEndTime = (TextBox)it.FindControl("txtEndTime");
                        DropDownList ddlBID = (DropDownList)it.FindControl("ddlBID");
                        TextBox txtJSBZ = (TextBox)it.FindControl("txtJSBZ");
                        TextBox txtTransStartTime = (TextBox)it.FindControl("txtTransStartTime");
                        TextBox txtTransEndTime = (TextBox)it.FindControl("txtTransEndTime");
                        TextBox txtTransSpeedRate = (TextBox)it.FindControl("txtTransSpeedRate");
                        TextBox txtHBZ = (TextBox)it.FindControl("txtHBZ");
                        TextBox DataStartTime = (TextBox)it.FindControl("DataStartTime");
                        TextBox DataEndTime = (TextBox)it.FindControl("DataEndTime");
                        DropDownList ddlSHBID = (DropDownList)it.FindControl("ddlSHBID");
                        TextBox txtSHTransStartTime = (TextBox)it.FindControl("txtSHTransStartTime");
                        TextBox txtHSL = (TextBox)it.FindControl("txtHSL");

                        co.DW = txtDW.Text;
                        co.SB = txtSB.Text;
                        co.DH = ddlDH.SelectedItem.Text;
                        co.FS = ddlFS.SelectedItem.Text;
                        co.JXZ = ddlJXZ.SelectedItem.Text;
                        co.MS = ddlMS.SelectedItem.Text;
                        co.QB = ddlQB.SelectedItem.Text;
                        co.GXZ = ddlGXZ.SelectedItem.Text;
                        co.ZHB = txtPreStartTime.Text;
                        co.QH = txtQH.Text;
                        co.GZK = txtTrackStartTime.Text;
                        co.GZJ = txtTrackEndTime.Text;
                        co.KSHX = txtWaveOnStartTime.Text;
                        co.GSHX = txtWaveOffStartTime.Text;
                        co.RK = txtStartTime.Text;
                        co.JS = txtEndTime.Text;
                        co.BID = ddlBID.SelectedItem.Text;
                        co.SBZ = txtJSBZ.Text;
                        co.RTs = txtTransStartTime.Text;
                        co.RTe = txtTransEndTime.Text;
                        co.SL = txtTransSpeedRate.Text;
                        co.HBID = ddlSHBID.SelectedItem.Text;
                        co.HBZ = txtHBZ.Text;
                        co.Ts = DataStartTime.Text;
                        co.Te = DataEndTime.Text;
                        co.HRTs = txtSHTransStartTime.Text;
                        co.HSL = txtHSL.Text;
                        #endregion
                        list.Add(co);
                    }
                    #region new a GZJH_Content
                    co = new GZJH_Content() { 
                        DW="",
                        SB="",
                        DH="",
                        FS="",
                        JXZ = "",
                        MS = "",
                        QB = "",
                        GXZ = "",
                        ZHB = "",
                        QH = "",
                        GZK = "",
                        GZJ = "",
                        KSHX = "",
                        GSHX = "",
                        RK = "",
                        JS = "",
                        BID = "",
                        SBZ = "",
                        RTs = "",
                        RTe = "",
                        SL = "",
                        HBID="",
                        HBZ = "",
                        Ts = "",
                        Te = "",
                        HRTs="",
                        HSL=""
                    };
                    #endregion
                    list.Add(co);
                    rp.DataSource = list;
                    rp.DataBind();

                }
                if (e.CommandName == "Del")
                {
                    List<GZJH_Content> list = new List<GZJH_Content>();
                    GZJH_Content co;
                    Repeater rp = (Repeater)source;
                    ViewState["op"] = "Del";
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
                                #region 赋值
                                co = new GZJH_Content();
                                TextBox txtDW = (TextBox)it.FindControl("txtDW");
                                TextBox txtSB = (TextBox)it.FindControl("txtSB");
                                DropDownList ddlDH = (DropDownList)it.FindControl("ddlDH");
                                DropDownList ddlFS = (DropDownList)it.FindControl("ddlFS");
                                DropDownList ddlJXZ = (DropDownList)it.FindControl("ddlJXZ");
                                DropDownList ddlMS = (DropDownList)it.FindControl("ddlMS");
                                DropDownList ddlQB = (DropDownList)it.FindControl("ddlQB");
                                DropDownList ddlGXZ = (DropDownList)it.FindControl("ddlGXZ");
                                TextBox txtPreStartTime = (TextBox)it.FindControl("txtPreStartTime");
                                TextBox txtQH = (TextBox)it.FindControl("txtQH");
                                TextBox txtTrackStartTime = (TextBox)it.FindControl("txtTrackStartTime");
                                TextBox txtTrackEndTime = (TextBox)it.FindControl("txtTrackEndTime");
                                TextBox txtWaveOnStartTime = (TextBox)it.FindControl("txtWaveOnStartTime");
                                TextBox txtWaveOffStartTime = (TextBox)it.FindControl("txtWaveOffStartTime");
                                TextBox txtStartTime = (TextBox)it.FindControl("txtStartTime");
                                TextBox txtEndTime = (TextBox)it.FindControl("txtEndTime");
                                DropDownList ddlBID = (DropDownList)it.FindControl("ddlBID");
                                TextBox txtJSBZ = (TextBox)it.FindControl("txtJSBZ");
                                TextBox txtTransStartTime = (TextBox)it.FindControl("txtTransStartTime");
                                TextBox txtTransEndTime = (TextBox)it.FindControl("txtTransEndTime");
                                TextBox txtTransSpeedRate = (TextBox)it.FindControl("txtTransSpeedRate");
                                TextBox txtHBZ = (TextBox)it.FindControl("txtHBZ");
                                TextBox DataStartTime = (TextBox)it.FindControl("DataStartTime");
                                TextBox DataEndTime = (TextBox)it.FindControl("DataEndTime");
                                DropDownList ddlSHBID = (DropDownList)it.FindControl("ddlSHBID");
                                TextBox txtSHTransStartTime = (TextBox)it.FindControl("txtSHTransStartTime");
                                TextBox txtHSL = (TextBox)it.FindControl("txtHSL");

                                co.DW = txtDW.Text;
                                co.SB = txtSB.Text;
                                co.DH = ddlDH.SelectedItem.Text;
                                co.FS = ddlFS.SelectedItem.Text;
                                co.JXZ = ddlJXZ.SelectedItem.Text;
                                co.MS = ddlMS.SelectedItem.Text;
                                co.QB = ddlQB.SelectedItem.Text;
                                co.GXZ = ddlGXZ.SelectedItem.Text;
                                co.ZHB = txtPreStartTime.Text;
                                co.QH = txtQH.Text;
                                co.GZK = txtTrackStartTime.Text;
                                co.GZJ = txtTrackEndTime.Text;
                                co.KSHX = txtWaveOnStartTime.Text;
                                co.GSHX = txtWaveOffStartTime.Text;
                                co.RK = txtStartTime.Text;
                                co.JS = txtEndTime.Text;
                                co.BID = ddlBID.SelectedItem.Text;
                                co.SBZ = txtJSBZ.Text;
                                co.RTs = txtTransStartTime.Text;
                                co.RTe = txtTransEndTime.Text;
                                co.SL = txtTransSpeedRate.Text;
                                co.HBID = ddlSHBID.SelectedItem.Text;
                                co.HBZ = txtHBZ.Text;
                                co.Ts = DataStartTime.Text;
                                co.Te = DataEndTime.Text;
                                co.HRTs = txtSHTransStartTime.Text;
                                co.HSL = txtHSL.Text;
                                #endregion
                                list.Add(co);
                            }
                        }
                        rp.DataSource = list;
                        rp.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定目标信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void rpDatas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    #region 初始化各个下拉列表的值

                    //任务代号
                    DropDownList ddlDH = (DropDownList)e.Item.FindControl("ddlDH") as DropDownList;
                    ddlDH.DataSource = PlanParameters.ReadParameters("GZJHDH");
                    ddlDH.DataTextField = "Text";
                    ddlDH.DataValueField = "Value";
                    ddlDH.DataBind();
                    //工作方式
                    DropDownList ddlFS = (DropDownList)e.Item.FindControl("ddlFS") as DropDownList;
                    ddlFS.DataSource = PlanParameters.ReadParameters("DJZYSQFS");
                    ddlFS.DataTextField = "Text";
                    ddlFS.DataValueField = "Value";
                    ddlFS.DataBind();
                    //计划性质
                    DropDownList ddlJXZ = (DropDownList)e.Item.FindControl("ddlJXZ") as DropDownList;
                    ddlJXZ.DataSource = PlanParameters.ReadParameters("DJZYSQSXZ");
                    ddlJXZ.DataTextField = "Text";
                    ddlJXZ.DataValueField = "Value";
                    ddlJXZ.DataBind();
                    //设备工作模式
                    DropDownList ddlMS = (DropDownList)e.Item.FindControl("ddlMS") as DropDownList;
                    ddlMS.DataSource = PlanParameters.ReadParameters("GZJHMS");
                    ddlMS.DataTextField = "Text";
                    ddlMS.DataValueField = "Value";
                    ddlMS.DataBind();
                    //圈标
                    DropDownList ddlQB = (DropDownList)e.Item.FindControl("ddlQB") as DropDownList;
                    ddlQB.DataSource = PlanParameters.ReadParameters("GZJHQB");
                    ddlQB.DataTextField = "Text";
                    ddlQB.DataValueField = "Value";
                    ddlQB.DataBind();
                    //工作性质
                    DropDownList ddlGXZ = (DropDownList)e.Item.FindControl("ddlGXZ") as DropDownList;
                    ddlGXZ.DataSource = PlanParameters.ReadParameters("GZJHGXZ");
                    ddlGXZ.DataTextField = "Text";
                    ddlGXZ.DataValueField = "Value";
                    ddlGXZ.DataBind();
                    //信息类别标志
                    DropDownList ddlBID = (DropDownList)e.Item.FindControl("ddlBID") as DropDownList;
                    ddlBID.DataSource = PlanParameters.ReadParameters("GZJHBID");
                    ddlBID.DataTextField = "Text";
                    ddlBID.DataValueField = "Value";
                    ddlBID.DataBind();
                    //信息类别标志
                    DropDownList ddlSHBID = (DropDownList)e.Item.FindControl("ddlSHBID") as DropDownList;
                    ddlSHBID.DataSource = PlanParameters.ReadParameters("GZJHBID");
                    ddlSHBID.DataTextField = "Text";
                    ddlSHBID.DataValueField = "Value";
                    ddlSHBID.DataBind();
                    #endregion

                    GZJH_Content g = (GZJH_Content)e.Item.DataItem;
                    if ( !string.IsNullOrEmpty(g.DH))
                    {
                        ddlDH.SelectedValue = g.DH;
                    }
                    if (!string.IsNullOrEmpty(g.FS))
                    {
                        ddlFS.SelectedValue = g.FS;
                    }
                    if (!string.IsNullOrEmpty(g.JXZ))
                    {
                        ddlJXZ.SelectedValue = g.JXZ;
                    }
                    
                    if (!string.IsNullOrEmpty(g.MS))
                    {
                        ddlMS.SelectedValue = g.MS;
                    }
                    if (!string.IsNullOrEmpty(g.QB))
                    {
                        ddlQB.SelectedValue = g.QB;
                    }
                    if (!string.IsNullOrEmpty(g.GXZ))
                    {
                        ddlGXZ.SelectedValue = g.GXZ;
                    }
                    if (!string.IsNullOrEmpty(g.BID))
                    {
                        ddlBID.SelectedValue = g.BID;
                    }
                    if (!string.IsNullOrEmpty(g.HBID))
                    {
                        ddlSHBID.SelectedValue = g.HBID;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定地面计划信息出现异常，异常原因", ex));
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
            //lblUpload.Visible = true;

            #region 读取文件内容
            StationInOutFileReader reader = new StationInOutFileReader();
            List<StationInOut> list;
            try
            {
                list = reader.Read(filename);
                rpStation.DataSource = list;
                rpStation.DataBind();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("新建总参GZJH，载入StationInOut文件出现异常，异常原因", ex));
            }
            finally { }
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
            GZJH ojh =null;
            string filepath = hfStationFile.Value;  //文件路径
            string ids = txtIds.Text;   //行号

            PlanProcessor pp = new PlanProcessor();
            try
            {
                pp.AddSIOtoZCDMZJH(ref ojh, filepath, ids);
                rpDatas.DataSource = ojh.GZJHContents;
                rpDatas.DataBind();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("新建总参GZJH，加入StationInOut文件出现异常，异常原因", ex));
            }
            finally { }
            System.IO.File.Delete(filepath);    //删除临时文件
        }

    }
}