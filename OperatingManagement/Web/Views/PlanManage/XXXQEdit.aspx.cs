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
using OperatingManagement.Framework.Storage;
using System.Web.Security;
using System.Xml;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class XXXQEdit : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initial();

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    string sID = Request.QueryString["id"];
                    HfID.Value = sID;
                    hfStatus.Value = "edit";    //编辑
                    BindJhTable(sID);
                    BindXML();
                    hfURL.Value = "?type=XXXQ&startDate=" + Request.QueryString["startDate"] + "&endDate=" + Request.QueryString["endDate"];
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
            try
            {
                txtPlanStartTime.Attributes.Add("readonly", "true");
                txtPlanEndTime.Attributes.Add("readonly", "true");
                txtMBTimeSection1.Attributes.Add("readonly", "true");
                txtMBTimeSection2.Attributes.Add("readonly", "true");
                txtHJTimeSection1.Attributes.Add("readonly", "true");
                txtHJTimeSection2.Attributes.Add("readonly", "true");

                txtMBUser.Text = PlanParameters.ReadMBXQDefaultUser();
                txtHJUser.Text = PlanParameters.ReadHJXQDefaultUser();
                txtMBTargetInfo.Text = PlanParameters.ReadMBXQDefaultTargetInfo();
                txtHJEnvironInfo.Text = PlanParameters.ReadHJXQHJXQDefaultEnvironInfo();
                List<MBXQSatInfo> list = new List<MBXQSatInfo>();
                list.Add(new MBXQSatInfo());
                rpMB.DataSource = list;
                rpMB.DataBind();
                List<HJXQSatInfo> listhj = new List<HJXQSatInfo>();
                listhj.Add(new HJXQSatInfo());
                rpHJ.DataSource = listhj;
                rpHJ.DataBind();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("初始化页面出现异常，异常原因", ex));
            }
            finally { }
        }

        /// <summary>
        /// 绑定计划表信息
        /// </summary>
        /// <param name="sID"></param>
        private void BindJhTable(string sID)
        {
            try
            {
                List<JH> jh = (new JH()).SelectByIDS(sID);
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
            catch (Exception ex)
            {
                throw (new AspNetException("绑定计划基本信息出现异常，异常原因", ex));
            }
            finally { }
        }

        /// <summary>
        /// 绑定关联XML文件信息
        /// </summary>
        private void BindXML()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(HfFileIndex.Value);

                #region 空间目标信息需求
                XmlNode root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/User");
                txtMBUser.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/Time");
                txtMBTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/TargetInfo");
                txtMBTargetInfo.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/TimeSection1");
                txtMBTimeSection1.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/TimeSection2");
                txtMBTimeSection2.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/Sum");
                txtMBSum.Text = root.InnerText;

                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求");
                List<MBXQSatInfo> list = new List<MBXQSatInfo>();
                MBXQSatInfo sat;
                foreach (XmlNode n in root.ChildNodes)
                {
                    if (n.Name == "卫星")
                    {
                        sat = new MBXQSatInfo();
                        sat.SatName = n["SatName"].InnerText;
                        sat.InfoName = n["InfoName"].InnerText;
                        sat.InfoTime = n["InfoTime"].InnerText;
                        list.Add(sat);
                    }
                }
                rpMB.DataSource = list;
                rpMB.DataBind();

                #endregion

                #region 空间环境信息需求
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/User");
                txtHJUser.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/Time");
                txtHJTime.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/EnvironInfo");
                txtHJEnvironInfo.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/TimeSection1");
                txtHJTimeSection1.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/TimeSection2");
                txtHJTimeSection2.Text = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/Sum");
                txtHJSum.Text = root.InnerText;

                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求");
                List<HJXQSatInfo> listhj = new List<HJXQSatInfo>();
                HJXQSatInfo sathj;
                foreach (XmlNode n in root.ChildNodes)
                {
                    if (n.Name == "卫星")
                    {
                        sathj = new HJXQSatInfo();
                        sathj.SatName = n["SatName"].InnerText;
                        sathj.InfoName = n["InfoName"].InnerText;
                        sathj.InfoArea = n["InfoArea"].InnerText;
                        sathj.InfoTime = n["InfoTime"].InnerText;
                        listhj.Add(sathj);
                    }
                }
                rpHJ.DataSource = listhj;
                rpHJ.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定信息需求计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void rpMB_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Add")
                {
                    List<MBXQSatInfo> list2 = new List<MBXQSatInfo>();
                    MBXQSatInfo dm;
                    Repeater rp = (Repeater)source;
                    ViewState["opMB"] = "Add";
                    foreach (RepeaterItem it in rp.Items)
                    {
                        dm = new MBXQSatInfo();
                        ucs.ucSatellite txtMBSatName = (ucs.ucSatellite)it.FindControl("ucSatelliteMB");
                        DropDownList txtMBInfoName = (DropDownList)it.FindControl("ddlMBInfoName");
                        TextBox txtMBInfoTime = (TextBox)it.FindControl("txtMBInfoTime");

                        dm.SatName = txtMBSatName.SelectedItem.Text;
                        dm.InfoName = txtMBInfoName.SelectedItem.Text;
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
                    ViewState["opMB"] = "Del";
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
                                dm = new MBXQSatInfo();
                                ucs.ucSatellite txtMBSatName = (ucs.ucSatellite)it.FindControl("ucSatelliteMB");
                                DropDownList txtMBInfoName = (DropDownList)it.FindControl("ddlMBInfoName");
                                TextBox txtMBInfoTime = (TextBox)it.FindControl("txtMBInfoTime");

                                dm.SatName = txtMBSatName.SelectedItem.Text;
                                dm.InfoName = txtMBInfoName.SelectedItem.Text;
                                dm.InfoTime = txtMBInfoTime.Text;
                                list2.Add(dm);
                            }
                        }
                        rp.DataSource = list2;
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

        protected void rpHJ_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Add")
                {
                    List<HJXQSatInfo> list2 = new List<HJXQSatInfo>();
                    HJXQSatInfo dm;
                    Repeater rp = (Repeater)source;
                    ViewState["opHJ"] = "Add";
                    foreach (RepeaterItem it in rp.Items)
                    {
                        dm = new HJXQSatInfo();
                        ucs.ucSatellite txtHJSatName = (ucs.ucSatellite)it.FindControl("ucSatelliteHJ");
                        DropDownList txtHJInfoName = (DropDownList)it.FindControl("ddlHJInfoName");
                        TextBox txtHJInfoArea = (TextBox)it.FindControl("txtHJInfoArea");
                        TextBox txtHJInfoTime = (TextBox)it.FindControl("txtHJInfoTime");

                        dm.SatName = txtHJSatName.SelectedItem.Text;
                        dm.InfoName = txtHJInfoName.SelectedItem.Text;
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
                    ViewState["opHJ"] = "Del";
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
                                dm = new HJXQSatInfo();
                                ucs.ucSatellite txtHJSatName = (ucs.ucSatellite)it.FindControl("ucSatelliteHJ");
                                DropDownList txtHJInfoName = (DropDownList)it.FindControl("ddlHJInfoName");
                                TextBox txtHJInfoArea = (TextBox)it.FindControl("txtHJInfoArea");
                                TextBox txtHJInfoTime = (TextBox)it.FindControl("txtHJInfoTime");

                                dm.SatName = txtHJSatName.SelectedItem.Text;
                                dm.InfoName = txtHJInfoName.SelectedItem.Text;
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
            catch (Exception ex)
            {
                throw (new AspNetException("绑定环境信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                #region MBXQ
                MBXQ objMB = new MBXQ();
                objMB.User = txtMBUser.Text;
                objMB.Time = txtMBTime.Text;
                objMB.TargetInfo = txtMBTargetInfo.Text;
                objMB.TimeSection1 = txtMBTimeSection1.Text;
                objMB.TimeSection2 = txtMBTimeSection2.Text;
                //obj.Sum = txtMBSum.Text;
                objMB.SatInfos = new List<MBXQSatInfo>();

                MBXQSatInfo dm;
                foreach (RepeaterItem it in rpMB.Items)
                {
                    dm = new MBXQSatInfo();
                    ucs.ucSatellite txtMBSatName = (ucs.ucSatellite)it.FindControl("ucSatelliteMB");
                    DropDownList txtMBInfoName = (DropDownList)it.FindControl("ddlMBInfoName");
                    TextBox txtMBInfoTime = (TextBox)it.FindControl("txtMBInfoTime");

                    dm.SatName = txtMBSatName.SelectedItem.Text;
                    dm.InfoName = txtMBInfoName.SelectedItem.Text;
                    dm.InfoTime = txtMBInfoTime.Text;

                    objMB.SatInfos.Add(dm);
                }
                objMB.Sum = objMB.SatInfos.Count.ToString(); //信息条数，自动计算得到

                #endregion

                #region HJXQ
                HJXQ objHJ = new HJXQ();
                objHJ.User = txtHJUser.Text;
                objHJ.Time = txtHJTime.Text;
                objHJ.EnvironInfo = txtHJEnvironInfo.Text;
                objHJ.TimeSection1 = txtHJTimeSection1.Text;
                objHJ.TimeSection2 = txtHJTimeSection2.Text;
                //obj.Sum = txtHJSum.Text;

                objHJ.SatInfos = new List<HJXQSatInfo>();

                HJXQSatInfo dmhj;
                foreach (RepeaterItem it in rpHJ.Items)
                {
                    dmhj = new HJXQSatInfo();
                    ucs.ucSatellite txtHJSatName = (ucs.ucSatellite)it.FindControl("ucSatelliteHJ");
                    DropDownList txtHJInfoName = (DropDownList)it.FindControl("ddlHJInfoName");
                    TextBox txtHJInfoArea = (TextBox)it.FindControl("txtHJInfoArea");
                    TextBox txtHJInfoTime = (TextBox)it.FindControl("txtHJInfoTime");

                    dmhj.SatName = txtHJSatName.SelectedItem.Text;
                    dmhj.InfoName = txtHJInfoName.SelectedItem.Text;
                    dmhj.InfoArea = txtHJInfoArea.Text;
                    dmhj.InfoTime = txtHJInfoTime.Text;

                    objHJ.SatInfos.Add(dmhj);
                }
                objHJ.Sum = objHJ.SatInfos.Count.ToString(); //信息条数，自动计算得到

                #endregion

                XXXQ objXXXQ = new XXXQ();
                objXXXQ.objMBXQ = objMB;
                objXXXQ.objHJXQ = objHJ;
                objXXXQ.TaskID = ucTask1.SelectedItem.Value;
                objXXXQ.SatID = ucSatellite1.SelectedItem.Value;

                PlanFileCreator creater = new PlanFileCreator();
                if (hfStatus.Value == "new")
                {
                    string filepath = creater.CreateXXXQFile(objXXXQ, 0);

                    DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                    {
                        TaskID = objXXXQ.TaskID,
                        PlanType = "XXXQ",
                        PlanID = (new Sequence()).GetXXXQSequnce(),
                        StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                        EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                        SRCType = 0,
                        FileIndex = filepath,
                        SatID = objXXXQ.SatID,
                        Reserve = txtNote.Text
                    };
                    var result = jh.Add();
                }
                else
                {
                    //当任务和卫星更新时，需要更新文件名称
                    if (hfSatID.Value != ucSatellite1.SelectedValue || hfTaskID.Value != ucTask1.SelectedValue)
                    {
                        string filepath = creater.CreateXXXQFile(objXXXQ, 0);

                        DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                        {
                            Id = Convert.ToInt32(HfID.Value),
                            TaskID = objXXXQ.TaskID,
                            StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                            EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                            FileIndex = filepath,
                            SatID = objXXXQ.SatID,
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
                        creater.CreateXXXQFile(objXXXQ, 1);
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


        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.Edit";
            this.ShortTitle = "空间信息需求编辑";
            base.OnPageLoaded();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/XXXQEdit.aspx.js");
        }

        protected void btnSaveTo_Click(object sender, EventArgs e)
        {
            try
            {
                #region MBXQ
                MBXQ objMB = new MBXQ();
                objMB.User = txtMBUser.Text;
                objMB.Time = txtMBTime.Text;
                objMB.TargetInfo = txtMBTargetInfo.Text;
                objMB.TimeSection1 = txtMBTimeSection1.Text;
                objMB.TimeSection2 = txtMBTimeSection2.Text;
                //obj.Sum = txtMBSum.Text;
                objMB.SatInfos = new List<MBXQSatInfo>();

                MBXQSatInfo dm;
                foreach (RepeaterItem it in rpMB.Items)
                {
                    dm = new MBXQSatInfo();
                    ucs.ucSatellite txtMBSatName = (ucs.ucSatellite)it.FindControl("ucSatelliteMB");
                    DropDownList txtMBInfoName = (DropDownList)it.FindControl("ddlMBInfoName");
                    TextBox txtMBInfoTime = (TextBox)it.FindControl("txtMBInfoTime");

                    dm.SatName = txtMBSatName.SelectedItem.Text;
                    dm.InfoName = txtMBInfoName.SelectedItem.Text;
                    dm.InfoTime = txtMBInfoTime.Text;

                    objMB.SatInfos.Add(dm);
                }
                objMB.Sum = objMB.SatInfos.Count.ToString(); //信息条数，自动计算得到

                #endregion

                #region HJXQ
                HJXQ objHJ = new HJXQ();
                objHJ.User = txtHJUser.Text;
                objHJ.Time = txtHJTime.Text;
                objHJ.EnvironInfo = txtHJEnvironInfo.Text;
                objHJ.TimeSection1 = txtHJTimeSection1.Text;
                objHJ.TimeSection2 = txtHJTimeSection2.Text;
                //obj.Sum = txtHJSum.Text;

                objHJ.SatInfos = new List<HJXQSatInfo>();

                HJXQSatInfo dmhj;
                foreach (RepeaterItem it in rpHJ.Items)
                {
                    dmhj = new HJXQSatInfo();
                    ucs.ucSatellite txtHJSatName = (ucs.ucSatellite)it.FindControl("ucSatelliteHJ");
                    DropDownList txtHJInfoName = (DropDownList)it.FindControl("ddlHJInfoName");
                    TextBox txtHJInfoArea = (TextBox)it.FindControl("txtHJInfoArea");
                    TextBox txtHJInfoTime = (TextBox)it.FindControl("txtHJInfoTime");

                    dmhj.SatName = txtHJSatName.SelectedItem.Text;
                    dmhj.InfoName = txtHJInfoName.SelectedItem.Text;
                    dmhj.InfoArea = txtHJInfoArea.Text;
                    dmhj.InfoTime = txtHJInfoTime.Text;

                    objHJ.SatInfos.Add(dmhj);
                }
                objHJ.Sum = objHJ.SatInfos.Count.ToString(); //信息条数，自动计算得到

                #endregion

                XXXQ objXXXQ = new XXXQ();
                objXXXQ.objMBXQ = objMB;
                objXXXQ.objHJXQ = objHJ;

                PlanFileCreator creater = new PlanFileCreator();

                objXXXQ.TaskID = ucTask1.SelectedItem.Value;
                objXXXQ.SatID = ucSatellite1.SelectedItem.Value;

                //检查文件是否已经存在
                if (creater.TestXXXQFileName(objXXXQ))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "File", "<script type='text/javascript'>showMsg('操作过快，请稍后重试');</script>");
                    return;
                }
                string filepath = creater.CreateXXXQFile(objXXXQ, 0);

                DataAccessLayer.PlanManage.JH jh = new DataAccessLayer.PlanManage.JH()
                {
                    TaskID = objXXXQ.TaskID,
                    PlanType = "XXXQ",
                    PlanID = (new Sequence()).GetXXXQSequnce(),
                    StartTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()),
                    EndTime = Convert.ToDateTime(txtPlanEndTime.Text.Trim()),
                    SRCType = 0,
                    FileIndex = filepath,
                    SatID = objXXXQ.SatID,
                    Reserve = txtNote.Text
                };
                var result = jh.Add();
                ltMessage.Text = "计划保存成功";
               // ClientScript.RegisterStartupScript(this.GetType(), "OK", "<script type='text/javascript'>showMsg('计划保存成功');</script>");
            }
            catch (Exception ex)
            {
                throw (new AspNetException("另存计划信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void rpMB_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ucs.ucSatellite txtMBSatName = (ucs.ucSatellite)e.Item.FindControl("ucSatelliteMB");
                    txtMBSatName.ReBindData();

                    DropDownList ddlRFS = (DropDownList)e.Item.FindControl("ddlMBInfoName") as DropDownList;
                    ddlRFS.DataSource = PlanParameters.ReadParameters("MBXQInfoName");
                    ddlRFS.DataTextField = "Text";
                    ddlRFS.DataValueField = "Text";
                    ddlRFS.DataBind();

                    MBXQSatInfo mbs = (MBXQSatInfo)e.Item.DataItem;
                    if (!string.IsNullOrEmpty(mbs.SatName))
                    {
                        txtMBSatName.Items.FindByText(DataBinder.Eval(e.Item.DataItem, "SatName").ToString()).Selected = true;
                    }
                    if (mbs.InfoName != null)
                    {
                        ddlRFS.SelectedValue = DataBinder.Eval(e.Item.DataItem, "InfoName").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定目标信息出现异常，异常原因", ex));
            }
            finally { }
        }

        protected void rpHJ_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ucs.ucSatellite txtHJSatName = (ucs.ucSatellite)e.Item.FindControl("ucSatelliteHJ");
                    txtHJSatName.ReBindData();

                    DropDownList ddlRFS = (DropDownList)e.Item.FindControl("ddlHJInfoName") as DropDownList;
                    ddlRFS.DataSource = PlanParameters.ReadParameters("HJXQInfoName");
                    ddlRFS.DataTextField = "Text";
                    ddlRFS.DataValueField = "Text";
                    ddlRFS.DataBind();

                    HJXQSatInfo hjs = (HJXQSatInfo)e.Item.DataItem;
                    if (!string.IsNullOrEmpty(hjs.SatName))
                    {
                        txtHJSatName.Items.FindByText(DataBinder.Eval(e.Item.DataItem, "SatName").ToString()).Selected = true;
                    }
                    if (hjs.InfoName != null)
                    {
                        ddlRFS.SelectedValue = DataBinder.Eval(e.Item.DataItem, "InfoName").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("绑定环境信息出现异常，异常原因", ex));
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
            Response.Redirect("PlanList.aspx" + hfURL.Value);
        }
    }
}