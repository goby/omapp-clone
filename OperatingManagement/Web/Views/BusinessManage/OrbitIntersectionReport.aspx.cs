﻿#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:OrbitIntersectionReport.cs
//Remark:业务管理-轨道分析-差值分析
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20120602     Create     
//------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.DataAccessLayer.BusinessManage;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class OrbitIntersectionReport : AspNetPage
    {
        #region 属性
        protected readonly string JPLEPHFilePath = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "JPLEPHFilePath");
        protected readonly string TESTRECLFilePath = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "TESTRECLFilePath");
        protected readonly string WGS84FilePath = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "WGS84FilePath");
        protected readonly string eopc04_IAU2000FilePath = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "eopc04_IAU2000FilePath");
        /// <summary>
        /// CutSub各行信息列表
        /// </summary>
        protected List<CutSubItemInfo> CutSubItemInfoList
        {
            get
            {
                if (ViewState["CutSubItemInfoList"] == null)
                {
                    return new List<CutSubItemInfo>();
                }
                else
                {
                    return (ViewState["CutSubItemInfoList"] as List<CutSubItemInfo>);
                }
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //trMessage.Visible = false;
                //lblMessage.Text = string.Empty;
                if (!IsPostBack)
                {
                    BindDataSource();
                    BindCutMainSatelliteProperty();
                    BindCutSubSatelliteProperty();
                }
            }
            catch
            {
                //trMessage.Visible = true;
                //lblMessage.Text = "发生未知错误，操作失败。";
            }
        }
        /// <summary>
        /// 交会预报文件选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblFileOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetResultControls();
            //文件上传
            if (rblFileOption.SelectedValue == "0")
            {
                divFileUpload.Visible = true;
                divFillIn.Visible = false;
            }
            //手工录入
            else if (rblFileOption.SelectedValue == "1")
            {
                divFileUpload.Visible = false;
                divFillIn.Visible = true;
            }
        }
        /// <summary>
        /// 文件菜单选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void menuCut_MenuItemClick(object sender, MenuEventArgs e)
        {
            mvCut.ActiveViewIndex = Convert.ToInt32(menuCut.SelectedValue);
        }
        /// <summary>
        /// 开始计算交会预报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            //if (!ValidateCutMainProperty())
            //{
            //    mvCut.ActiveViewIndex = 0;
            //}
            ResetResultControls();
            if (rblFileOption.SelectedValue == "0")
            {
                UploadFileAndCalculate();
            }
            else if (rblFileOption.SelectedValue == "1")
            {
 
            }
        }
        /// <summary>
        /// 下载计算结果文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnResultFileDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string resultFilePath = lblResultFilePath.Text.Trim();
                if (string.IsNullOrEmpty(resultFilePath))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "计算结果文件不存在。";
                    return;
                }

                if (!File.Exists(resultFilePath))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "计算结果文件不存在。";
                    return;
                }

                Response.Clear();
                Response.Buffer = false;
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "attachment;filename=" + Path.GetFileName(resultFilePath) + ";");
                Response.Write(File.ReadAllText(resultFilePath));
                Response.Flush();
                Response.End();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"下载计算结果文件失败。\")", true);
                trMessage.Visible = true;
                lblMessage.Text = "下载计算结果文件失败。";
            }
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_JHYB.Caculate";
            this.ShortTitle = "交会预报";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/businessmanage/OrbitIntersectionReport.aspx.js");
        }

        #region CutMain相关事件
        /// <summary>
        /// CutMain主星SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplCutMainSatellite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCutMainSatelliteProperty();
        }
        /// <summary>
        /// 是否考虑KAE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblCutMainKAE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //考虑KAE
            if (rblCutMainKAE.SelectedValue == "1")
            {
                txtCutMaindA.Enabled = true;
                txtCutMaindE.Enabled = true;
            }
            //不考虑KAE
            else if (rblCutMainKAE.SelectedValue == "0")
            {
                txtCutMaindA.Enabled = false;
                txtCutMaindE.Enabled = false;
                txtCutMaindA.Text = "0";
                txtCutMaindE.Text = "0";
            }
        }
        #endregion

        #region CutSub相关事件
        /// <summary>
        /// CutSub主星SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplCutSubSatellite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCutSubSatelliteProperty();
        }
        /// <summary>
        /// 添加CutSub记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddCutSubItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCutSubLYDate.Text.Trim()))
                {
                    rfvCutSubLYDate.IsValid = false;
                    txtCutSubLYDate.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCutSubLYTimeMilliSecond.Text.Trim()))
                {
                    rfvCutSubLYTimeMilliSecond.IsValid = false;
                    txtCutSubLYTimeMilliSecond.Focus();
                    return;
                }
                double cutSubLYTimeMilliSecond = 0.0;
                if (!double.TryParse(txtCutSubLYTimeMilliSecond.Text.Trim(), out cutSubLYTimeMilliSecond))
                {
                    rvCutSubLYTimeMilliSecond.IsValid = false;
                    txtCutSubLYTimeMilliSecond.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCutSubD1.Text.Trim()))
                {
                    rfvCutSubD1.IsValid = false;
                    txtCutSubD1.Focus();
                    return;
                }
                double d1 = 0.0;
                if (!double.TryParse(txtCutSubD1.Text.Trim(), out d1))
                {
                    rvCutSubD1.IsValid = false;
                    txtCutSubD1.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCutSubD2.Text.Trim()))
                {
                    rfvCutSubD2.IsValid = false;
                    txtCutSubD2.Focus();
                    return;
                }
                double d2 = 0.0;
                if (!double.TryParse(txtCutSubD2.Text.Trim(), out d2))
                {
                    rvCutSubD2.IsValid = false;
                    txtCutSubD2.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCutSubD3.Text.Trim()))
                {
                    rfvCutSubD3.IsValid = false;
                    txtCutSubD3.Focus();
                    return;
                }
                double d3 = 0.0;
                if (!double.TryParse(txtCutSubD3.Text.Trim(), out d3))
                {
                    rvCutSubD3.IsValid = false;
                    txtCutSubD3.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCutSubD4.Text.Trim()))
                {
                    rfvCutSubD4.IsValid = false;
                    txtCutSubD4.Focus();
                    return;
                }
                double d4 = 0.0;
                if (!double.TryParse(txtCutSubD4.Text.Trim(), out d4))
                {
                    rvCutSubD4.IsValid = false;
                    txtCutSubD4.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCutSubD5.Text.Trim()))
                {
                    rfvCutSubD5.IsValid = false;
                    txtCutSubD5.Focus();
                    return;
                }
                double d5 = 0.0;
                if (!double.TryParse(txtCutSubD5.Text.Trim(), out d5))
                {
                    rvCutSubD5.IsValid = false;
                    txtCutSubD5.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCutSubD6.Text.Trim()))
                {
                    rfvCutSubD6.IsValid = false;
                    txtCutSubD6.Focus();
                    return;
                }
                double d6 = 0.0;
                if (!double.TryParse(txtCutSubD6.Text.Trim(), out d6))
                {
                    rvCutSubD6.IsValid = false;
                    txtCutSubD6.Focus();
                    return;
                }
                DateTime cutSubLYDate = DateTime.Now;
                if (!DateTime.TryParse(txtCutSubLYDate.Text.Trim(), out cutSubLYDate))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "历元日期格式错误";
                    return;
                }
                cutSubLYDate = cutSubLYDate.AddHours(Convert.ToDouble(dplCutSubLYTimeHour.SelectedValue));
                cutSubLYDate = cutSubLYDate.AddMinutes(Convert.ToDouble(dplCutSubLYTimeMinute.SelectedValue));
                cutSubLYDate = cutSubLYDate.AddSeconds(Convert.ToDouble(dplCutSubLYTimeSecond.SelectedValue));

                string lysk = cutSubLYDate.ToString("yyyy MM dd HH:mm:ss");
                lysk = lysk + "." + FillWithSpace(string.Format("{0:F0}", cutSubLYTimeMilliSecond * 1000.0), 6);

                double cutSubSatelliteSm = 0.0;
                double.TryParse(txtCutSubSatelliteSm.Text.Trim(), out cutSubSatelliteSm);

                double cutSubSatelliteRef = 0.0;
                double.TryParse(txtCutSubSatelliteSm.Text.Trim(), out cutSubSatelliteRef);

                int kk = 0;
                int.TryParse(txtCutSubSatelliteKK.Text.Trim(), out kk);

                CutSubItemInfo cutSubItemInfo = new CutSubItemInfo()
                {
                    Id = Guid.NewGuid(),
                    SatelliteName = FillWithSpace(dplCutSubSatellite.SelectedItem.Text, 12),
                    SatelliteNO = FillWithSpace(dplCutSubSatellite.SelectedValue, 6),
                    LYSK = lysk,
                    KK = FillWithSpace(kk, 2),
                    D1 = FillWithSpace(string.Format("{0:F6}", d1), 17),
                    D2 = FillWithSpace(string.Format("{0:F6}", d2), 17),
                    D3 = FillWithSpace(string.Format("{0:F6}", d3), 17),
                    D4 = FillWithSpace(string.Format("{0:F6}", d4), 17),
                    D5 = FillWithSpace(string.Format("{0:F6}", d5), 17),
                    D6 = FillWithSpace(string.Format("{0:F6}", d6), 17),
                    Sm = FillWithSpace(string.Format("{0:F6}", cutSubSatelliteSm), 17),
                    Ref = FillWithSpace(string.Format("{0:F6}", cutSubSatelliteRef), 17)
                };
                List<CutSubItemInfo> cutSubItemInfoList = CutSubItemInfoList;
                cutSubItemInfoList.Add(cutSubItemInfo);
                ViewState["CutSubItemInfoList"] = cutSubItemInfoList;
                BindCutSubList();
                ResetCutSubControls();

            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        /// <summary>
        /// 重置CutSub控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnResetCutSubItem_Click(object sender, EventArgs e)
        {
            //ViewState["CutSubItemInfoList"] = null;
            //BindCutSubList();
            ResetCutSubControls();
        }
        /// <summary>
        /// 删除CutSub记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDeleteCutSub_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnDeleteCutSub = (sender as LinkButton);
                if (!string.IsNullOrEmpty(lbtnDeleteCutSub.CommandArgument))
                {
                    List<CutSubItemInfo> cutSubItemInfoList = CutSubItemInfoList;
                    int index = cutSubItemInfoList.FindIndex(a => a.Id.ToString().ToLower() == lbtnDeleteCutSub.CommandArgument.ToLower());
                    if (index >= 0)
                        cutSubItemInfoList.RemoveAt(index);

                    ViewState["CutSubItemInfoList"] = cutSubItemInfoList;
                }

                BindCutSubList();
                ResetCutSubControls();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }
        #endregion

        #region CutOptional相关事件
       

        #endregion

        #region Method
        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        private void BindDataSource()
        {
            dplCutMainReportBeginTimeHour.Items.Clear();
            dplCutMainLYTimeHour.Items.Clear();
            dplCutSubLYTimeHour.Items.Clear();
            for (int i = 0; i < 24; i++)
            {
                dplCutMainReportBeginTimeHour.Items.Add(new ListItem(i.ToString() + "时", i.ToString()));
                dplCutMainLYTimeHour.Items.Add(new ListItem(i.ToString() + "时", i.ToString()));
                dplCutSubLYTimeHour.Items.Add(new ListItem(i.ToString() + "时", i.ToString()));
            }
            dplCutMainReportBeginTimeMinute.Items.Clear();
            dplCutMainLYTimeMinute.Items.Clear();
            dplCutMainReportBeginTimeSecond.Items.Clear();
            dplCutMainLYTimeSecond.Items.Clear();
            dplCutSubLYTimeMinute.Items.Clear();
            dplCutSubLYTimeSecond.Items.Clear();
            for (int i = 0; i < 60; i++)
            {
                dplCutMainReportBeginTimeMinute.Items.Add(new ListItem(i.ToString() + "分", i.ToString()));
                dplCutMainLYTimeMinute.Items.Add(new ListItem(i.ToString() + "分", i.ToString()));
                dplCutSubLYTimeMinute.Items.Add(new ListItem(i.ToString() + "分", i.ToString()));

                dplCutMainReportBeginTimeSecond.Items.Add(new ListItem(i.ToString() + "秒", i.ToString()));
                dplCutMainLYTimeSecond.Items.Add(new ListItem(i.ToString() + "秒", i.ToString()));
                dplCutSubLYTimeSecond.Items.Add(new ListItem(i.ToString() + "秒", i.ToString()));
            }

            Satellite objSatellite = new Satellite();
            dplCutMainSatellite.Items.Clear();
            dplCutMainSatellite.DataSource = objSatellite.Cache;
            dplCutMainSatellite.DataTextField = "WXMC";
            dplCutMainSatellite.DataValueField = "Id";//WXBM，该字段统一为基类中的ID
            dplCutMainSatellite.DataBind();

            dplCutSubSatellite.Items.Clear();
            dplCutSubSatellite.DataSource = objSatellite.Cache;
            dplCutSubSatellite.DataTextField = "WXMC";
            dplCutSubSatellite.DataValueField = "Id";//WXBM，该字段统一为基类中的ID
            dplCutSubSatellite.DataBind();
        }

        /// <summary>
        /// 绑定CutMain卫星属性及与属性相关的值
        /// </summary>
        private void BindCutMainSatelliteProperty()
        {
            Satellite objSatellite = new Satellite();
            objSatellite.Id = dplCutMainSatellite.SelectedValue;
            objSatellite.SelectByID();
            txtCutMainSatelliteNO.Text = objSatellite.Id;
            txtCutMainSatelliteKK.Text = objSatellite.State;
            txtCutMainSatelliteSm.Text = objSatellite.MZB.ToString();
            txtCutMainSatelliteRef.Text = objSatellite.BMFSXS.ToString();
            switch (txtCutMainSatelliteKK.Text)
            {
                case "1":
                case "2":
                    lblCutMainD1Unit.Text = "米";
                    lblCutMainD2Unit.Text = string.Empty;
                    lblCutMainD3Unit.Text = "度";
                    lblCutMainD4Unit.Text = "度";
                    lblCutMainD5Unit.Text = "度";
                    lblCutMainD6Unit.Text = "度";
                    break;
                case "3":
                    lblCutMainD1Unit.Text = "米";
                    lblCutMainD2Unit.Text = "米";
                    lblCutMainD3Unit.Text = "米";
                    lblCutMainD4Unit.Text = "米/秒";
                    lblCutMainD5Unit.Text = "米/秒";
                    lblCutMainD6Unit.Text = "米/秒";
                    break;
                default:
                    lblCutMainD1Unit.Text = "米";
                    lblCutMainD2Unit.Text = string.Empty;
                    lblCutMainD3Unit.Text = "度";
                    lblCutMainD4Unit.Text = "度";
                    lblCutMainD5Unit.Text = "度";
                    lblCutMainD6Unit.Text = "度";
                    break;
            }
        }

        /// <summary>
        /// 绑定CutSub卫星属性及与属性相关的值
        /// </summary>
        private void BindCutSubSatelliteProperty()
        {
            Satellite objSatellite = new Satellite();
            objSatellite.Id = dplCutSubSatellite.SelectedValue;
            objSatellite.SelectByID();
            txtCutSubSatelliteNO.Text = objSatellite.Id;
            txtCutSubSatelliteKK.Text = objSatellite.State;
            txtCutSubSatelliteSm.Text = objSatellite.MZB.ToString();
            txtCutSubSatelliteRef.Text = objSatellite.BMFSXS.ToString();
            switch (txtCutSubSatelliteKK.Text)
            {
                case "1":
                case "2":
                    lblCutSubD1Unit.Text = "米";
                    lblCutSubD2Unit.Text = string.Empty;
                    lblCutSubD3Unit.Text = "度";
                    lblCutSubD4Unit.Text = "度";
                    lblCutSubD5Unit.Text = "度";
                    lblCutSubD6Unit.Text = "度";
                    break;
                case "3":
                    lblCutSubD1Unit.Text = "米";
                    lblCutSubD2Unit.Text = "米";
                    lblCutSubD3Unit.Text = "米";
                    lblCutSubD4Unit.Text = "米/秒";
                    lblCutSubD5Unit.Text = "米/秒";
                    lblCutSubD6Unit.Text = "米/秒";
                    break;
                default:
                    lblCutSubD1Unit.Text = "米";
                    lblCutSubD2Unit.Text = string.Empty;
                    lblCutSubD3Unit.Text = "度";
                    lblCutSubD4Unit.Text = "度";
                    lblCutSubD5Unit.Text = "度";
                    lblCutSubD6Unit.Text = "度";
                    break;
            }
        }
        /// <summary>
        /// 上传文件并调用异步计算
        /// </summary>
        private void UploadFileAndCalculate()
        {
            try
            {
                if (!fuCutMainFile.HasFile)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择CutMain文件。";
                    return;
                }
                if (fuCutMainFile.PostedFile.ContentLength == 0)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "CutMain文件不能为空。";
                    return;
                }
                if (!fuCutSubFile.HasFile)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择CutSub文件。";
                    return;
                }
                if (fuCutSubFile.PostedFile.ContentLength == 0)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "CutSub文件不能为空。";
                    return;
                }
                if (!fuCutOptionalFile.HasFile)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择CutOptional文件。";
                    return;
                }
                if (fuCutOptionalFile.PostedFile.ContentLength == 0)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "CutOptional文件不能为空。";
                    return;
                }

                //校验CutMain文件扩展名、文件大小等合法性
                int fileSize = fuCutMainFile.PostedFile.ContentLength;
                string fileExtension = Path.GetExtension(fuCutMainFile.PostedFile.FileName);
                int fileMaxSize = int.Parse(SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "CuMainFileMaxSize"));
                string allowFileExtension = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "CuMainFileExtension").Trim(new char[] { ',', '，', ';', '；' });
                string[] extensionArray = allowFileExtension.Split(new char[] { ',', '，', ';', '；' }, StringSplitOptions.RemoveEmptyEntries);
                bool allowExtension = false;
                if (extensionArray != null && extensionArray.Length > 0)
                {
                    foreach (string extension in extensionArray)
                    {
                        if (extension.Trim(new char[] { ' ', '.' }).ToLower() == fileExtension.Trim(new char[] { ' ', '.' }).ToLower())
                        {
                            allowExtension = true;
                            break;
                        }
                    }
                }
                if (!allowExtension)
                {
                    string message = string.Format("上传CutMain文件格式不正确，应为：{0}。", allowFileExtension);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"" + message + "\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return;
                }
                if (fileSize > fileMaxSize)
                {
                    string message = string.Format("上传CutMain文件不能超过{0}字节", fileMaxSize.ToString());
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"" + message + "\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return;
                }

                //校验CutSub文件扩展名、文件大小等合法性
                fileSize = fuCutSubFile.PostedFile.ContentLength;
                fileExtension = Path.GetExtension(fuCutSubFile.PostedFile.FileName);
                fileMaxSize = int.Parse(SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "CuSubFileMaxSize"));
                allowFileExtension = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "CuSubFileExtension").Trim(new char[] { ',', '，', ';', '；' });
                extensionArray = allowFileExtension.Split(new char[] { ',', '，', ';', '；' }, StringSplitOptions.RemoveEmptyEntries);
                allowExtension = false;
                if (extensionArray != null && extensionArray.Length > 0)
                {
                    foreach (string extension in extensionArray)
                    {
                        if (extension.Trim(new char[] { ' ', '.' }).ToLower() == fileExtension.Trim(new char[] { ' ', '.' }).ToLower())
                        {
                            allowExtension = true;
                            break;
                        }
                    }
                }
                if (!allowExtension)
                {
                    string message = string.Format("上传CutSub文件格式不正确，应为：{0}。", allowFileExtension);
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return;
                }
                if (fileSize > fileMaxSize)
                {
                    string message = string.Format("上传CutSub文件不能超过{0}字节", fileMaxSize.ToString());
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return;
                }

                //校验CutOptional文件扩展名、文件大小等合法性
                fileSize = fuCutSubFile.PostedFile.ContentLength;
                fileExtension = Path.GetExtension(fuCutSubFile.PostedFile.FileName);
                fileMaxSize = int.Parse(SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "CuOptionalFileMaxSize"));
                allowFileExtension = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "CuOptionalFileExtension").Trim(new char[] { ',', '，', ';', '；' });
                extensionArray = allowFileExtension.Split(new char[] { ',', '，', ';', '；' }, StringSplitOptions.RemoveEmptyEntries);
                allowExtension = false;
                if (extensionArray != null && extensionArray.Length > 0)
                {
                    foreach (string extension in extensionArray)
                    {
                        if (extension.Trim(new char[] { ' ', '.' }).ToLower() == fileExtension.Trim(new char[] { ' ', '.' }).ToLower())
                        {
                            allowExtension = true;
                            break;
                        }
                    }
                }
                if (!allowExtension)
                {
                    string message = string.Format("上传CutOptional文件格式不正确，应为：{0}。", allowFileExtension);
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return;
                }
                if (fileSize > fileMaxSize)
                {
                    string message = string.Format("上传CutOptional文件不能超过{0}字节", fileMaxSize.ToString());
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return;
                }

                string cuMainFileDirectory = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "CuMainFileDirectory");
                if (!Directory.Exists(cuMainFileDirectory))
                    Directory.CreateDirectory(cuMainFileDirectory);
                //CutMain文件服务器路径
                string cuMainFilePath = Path.Combine(cuMainFileDirectory, Guid.NewGuid().ToString() + Path.GetExtension(fuCutMainFile.PostedFile.FileName));
                fuCutMainFile.PostedFile.SaveAs(cuMainFilePath);

                string cutSubFileDirectory = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "CuSubFileDirectory");
                if (!Directory.Exists(cutSubFileDirectory))
                    Directory.CreateDirectory(cutSubFileDirectory);
                //CutSub文件服务器路径
                string cutSubFilePath = Path.Combine(cutSubFileDirectory, Guid.NewGuid().ToString() + Path.GetExtension(fuCutSubFile.PostedFile.FileName));
                fuCutSubFile.PostedFile.SaveAs(cutSubFilePath);

                string cutOptionalDirectory = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitIntersectionReport, "CuOptionalFileDirectory");
                if (!Directory.Exists(cutOptionalDirectory))
                    Directory.CreateDirectory(cutOptionalDirectory);
                //CutOptional文件服务器路径
                string cutOptionalFilePath = Path.Combine(cutOptionalDirectory, Guid.NewGuid().ToString() + Path.GetExtension(fuCutOptionalFile.PostedFile.FileName));
                fuCutOptionalFile.PostedFile.SaveAs(cutOptionalFilePath);

                ltCutMainFilePath.Text = fuCutMainFile.PostedFile.FileName;
                ltCutSubFilePath.Text = fuCutSubFile.PostedFile.FileName;
                ltCutOptinalFilePath.Text = fuCutOptionalFile.PostedFile.FileName;

                ltCutMainFile.Text = File.ReadAllText(cuMainFilePath, System.Text.Encoding.Default);
                ltCutSubFile.Text = File.ReadAllText(cutSubFilePath, System.Text.Encoding.Default);
                ltCutOptinalFile.Text = File.ReadAllText(cutOptionalFilePath, System.Text.Encoding.Default);

                //定义计算结果
                bool calResult = false;
                //定义结果文件路径
                string resultFilePath = @"D:\ResourceCalculate\ResultFileDirectory\2f318cd1-82ba-4593-9884-263cfb2887bd.txt";

                /**
                * TODO: 在这里开始计算，将结果calResult和结果文件路径resultFilePath赋值
                * */
                System.Threading.Thread.Sleep(10000);

                lblResultFilePath.Text = resultFilePath;
                lblCalResult.Text = calResult ? "计算成功" : "计算失败";
                if (!string.IsNullOrEmpty(resultFilePath) && File.Exists(resultFilePath))
                {
                    ltResultFile.Text = File.ReadAllText(resultFilePath, System.Text.Encoding.Default);
                }
                divCalResult.Visible = true;

                DeleteFile(cuMainFilePath);
                DeleteFile(cutSubFilePath);
                DeleteFile(cutOptionalFilePath);

                ClientScript.RegisterClientScriptBlock(this.GetType(),
                   "open-dialog",
                   "var _autoOpen=true;",
                   true);
            }
            catch
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
            }
        }
        /// <summary>
        /// 生成文件并调用异步计算
        /// </summary>
        private void CreateFileAndCalculate()
        {
            int activeViewIndex = 0;
            if (string.IsNullOrEmpty(txtCutMainReportBeginDate.Text.Trim()))
            {
                rfvCutMainReportBeginDate.IsValid = false;
                txtCutMainReportBeginDate.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMainReportBeginTimeMilliSecond.Text.Trim()))
            {
                rfvCutMainReportBeginTimeMilliSecond.IsValid = false;
                txtCutMainReportBeginTimeMilliSecond.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double cutMainReportBeginTimeMilliSecond = 0.0;
            if (!double.TryParse(txtCutMainReportBeginTimeMilliSecond.Text.Trim(), out cutMainReportBeginTimeMilliSecond))
            {
                rvCutMainReportBeginTimeMilliSecond.IsValid = false;
                txtCutMainReportBeginTimeMilliSecond.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMainReportTime.Text.Trim()))
            {
                rfvCutMainReportTime.IsValid = false;
                txtCutMainReportTime.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double cutMainReportTime = 0.0;
            if (!double.TryParse(txtCutMainReportTime.Text.Trim(), out cutMainReportTime))
            {
                trMessage.Visible = true;
                lblMessage.Text = "预报时长格式错误";
                txtCutMainReportTime.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMainLYDate.Text.Trim()))
            {
                rfvCutMainLYDate.IsValid = false;
                txtCutMainLYDate.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMainLYTimeMilliSecond.Text.Trim()))
            {
                rfvCutMainLYTimeMilliSecond.IsValid = false;
                txtCutMainLYTimeMilliSecond.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double cutMainLYTimeMilliSecond = 0.0;
            if (!double.TryParse(txtCutMainLYTimeMilliSecond.Text.Trim(), out cutMainLYTimeMilliSecond))
            {
                rvCutMainLYTimeMilliSecond.IsValid = false;
                txtCutMainLYTimeMilliSecond.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMainD1.Text.Trim()))
            {
                rfvCutMainD1.IsValid = false;
                txtCutMainD1.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double d1 = 0.0;
            if (!double.TryParse(txtCutMainD1.Text.Trim(), out d1))
            {
                rvCutMainD1.IsValid = false;
                txtCutMainD1.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMainD2.Text.Trim()))
            {
                rfvCutMainD2.IsValid = false;
                txtCutMainD2.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double d2 = 0.0;
            if (!double.TryParse(txtCutMainD2.Text.Trim(), out d2))
            {
                rvCutMainD2.IsValid = false;
                txtCutMainD2.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMainD3.Text.Trim()))
            {
                rfvCutMainD3.IsValid = false;
                txtCutMainD3.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double d3 = 0.0;
            if (!double.TryParse(txtCutMainD3.Text.Trim(), out d3))
            {
                rvCutMainD3.IsValid = false;
                txtCutMainD3.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMainD4.Text.Trim()))
            {
                rfvCutMainD4.IsValid = false;
                txtCutMainD4.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double d4 = 0.0;
            if (!double.TryParse(txtCutMainD4.Text.Trim(), out d4))
            {
                rvCutMainD4.IsValid = false;
                txtCutMainD4.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMainD5.Text.Trim()))
            {
                rfvCutMainD5.IsValid = false;
                txtCutMainD5.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double d5 = 0.0;
            if (!double.TryParse(txtCutMainD5.Text.Trim(), out d5))
            {
                rvCutMainD5.IsValid = false;
                txtCutMainD5.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMainD6.Text.Trim()))
            {
                rfvCutMainD6.IsValid = false;
                txtCutMainD6.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double d6 = 0.0;
            if (!double.TryParse(txtCutMainD6.Text.Trim(), out d6))
            {
                rvCutMainD6.IsValid = false;
                txtCutMainD6.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMaindR.Text.Trim()))
            {
                rfvCutMaindR.IsValid = false;
                txtCutMaindR.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double dR = 0.0;
            if (!double.TryParse(txtCutMaindR.Text.Trim(), out dR))
            {
                rvCutMaindR.IsValid = false;
                txtCutMaindR.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMaindA.Text.Trim()))
            {
                rfvCutMaindA.IsValid = false;
                txtCutMaindA.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double dA = 0.0;
            if (!double.TryParse(txtCutMaindA.Text.Trim(), out dA))
            {
                rvCutMaindA.IsValid = false;
                txtCutMaindA.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            if (string.IsNullOrEmpty(txtCutMaindE.Text.Trim()))
            {
                rfvCutMaindE.IsValid = false;
                txtCutMaindE.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double dE = 0.0;
            if (!double.TryParse(txtCutMaindE.Text.Trim(), out dE))
            {
                rvCutMaindE.IsValid = false;
                txtCutMaindE.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }

            DateTime cutMainReportBeginDate = DateTime.Now;
            if (!DateTime.TryParse(txtCutMainReportBeginDate.Text.Trim(), out cutMainReportBeginDate))
            {
                trMessage.Visible = true;
                lblMessage.Text = "预报起始历元日期格式错误。";
                txtCutMainReportBeginDate.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            cutMainReportBeginDate = cutMainReportBeginDate.AddDays(Convert.ToDouble(dplCutMainReportBeginTimeHour.SelectedValue));
            cutMainReportBeginDate = cutMainReportBeginDate.AddHours(Convert.ToDouble(dplCutMainReportBeginTimeMinute.SelectedValue));
            cutMainReportBeginDate = cutMainReportBeginDate.AddMinutes(Convert.ToDouble(dplCutMainReportBeginTimeSecond.SelectedValue));

            string reportBeginDate = cutMainReportBeginDate.ToString("yyyy MM dd HH:mm:ss");
            reportBeginDate = reportBeginDate + "." + FillWithSpace(string.Format("{0:F0}", cutMainReportBeginTimeMilliSecond * 1000.0), 6);

            DateTime cutMainLYDate = DateTime.Now;
            if (!DateTime.TryParse(txtCutMainLYDate.Text.Trim(), out cutMainLYDate))
            {
                trMessage.Visible = true;
                lblMessage.Text = "历元日期格式错误。";
                txtCutMainLYDate.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            cutMainLYDate = cutMainLYDate.AddDays(Convert.ToDouble(dplCutMainLYTimeHour.SelectedValue));
            cutMainLYDate = cutMainLYDate.AddHours(Convert.ToDouble(dplCutMainLYTimeMinute.SelectedValue));
            cutMainLYDate = cutMainLYDate.AddMinutes(Convert.ToDouble(dplCutMainLYTimeSecond.SelectedValue));

            string lydate = cutMainLYDate.ToString("yyyy MM dd HH:mm:ss");
            lydate = lydate + "." + FillWithSpace(string.Format("{0:F0}", cutMainLYTimeMilliSecond * 1000.0), 6);

            int kae = 0;
            int.TryParse(rblCutMainKAE.SelectedValue.Trim(), out kae);

            activeViewIndex = 1;
            if (CutSubItemInfoList == null || CutSubItemInfoList.Count < 1)
            {
                trMessage.Visible = true;
                lblMessage.Text = "请录入CutSub主星列表信息。";
                txtCutMainLYDate.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }

            activeViewIndex = 2;
            if (string.IsNullOrEmpty(txtCutOptionalTimeInterval.Text.Trim()))
            {
                rfvCutOptionalTimeInterval.IsValid = false;
                txtCutOptionalTimeInterval.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            double cutOptionalTimeInterval = 0.0;
            if (!double.TryParse(txtCutOptionalTimeInterval.Text.Trim(), out cutOptionalTimeInterval))
            {
                rvCutOptionalTimeInterval.IsValid = false;
                txtCutOptionalTimeInterval.Focus();
                mvCut.ActiveViewIndex = activeViewIndex;
                return;
            }
            //

        }
        /// <summary>
        /// 绑定CutSub的主星列表信息
        /// </summary>
        private void BindCutSubList()
        {
            cpCuSubPager.DataSource = CutSubItemInfoList;
            cpCuSubPager.PageSize = this.SiteSetting.PageSize;
            cpCuSubPager.BindToControl = rpCutSubList;
            rpCutSubList.DataSource = cpCuSubPager.DataSourcePaged;
            rpCutSubList.DataBind();
        }
        /// <summary>
        /// 重置CutSub的主星相关控件
        /// </summary>
        private void ResetCutSubControls()
        {
            txtCutSubLYDate.Text = string.Empty;
            dplCutSubLYTimeHour.SelectedIndex = 0;
            dplCutSubLYTimeMinute.SelectedIndex = 0;
            dplCutSubLYTimeSecond.SelectedIndex = 0;
            txtCutSubLYTimeMilliSecond.Text = string.Empty;
            dplCutSubSatellite.SelectedIndex = 0;
            txtCutSubD1.Text = string.Empty;
            txtCutSubD2.Text = string.Empty;
            txtCutSubD3.Text = string.Empty;
            txtCutSubD4.Text = string.Empty;
            txtCutSubD5.Text = string.Empty;
            txtCutSubD6.Text = string.Empty;

            BindCutSubSatelliteProperty();
        }
        /// <summary>
        /// 重置计算结果相关控件
        /// </summary>
        private void ResetResultControls()
        {
            divCalResult.Visible = false;
            lblResultFilePath.Text = string.Empty;
            lblCalResult.Text = "等待计算";
            ltCutMainFilePath.Text = string.Empty;
            ltCutSubFilePath.Text = string.Empty;
            ltCutOptinalFilePath.Text = string.Empty;
            ltCutMainFile.Text = string.Empty;
            ltCutSubFile.Text = string.Empty;
            ltCutOptinalFile.Text = string.Empty;
            ltResultFile.Text = string.Empty;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        private void DeleteFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                try { File.Delete(filePath); }
                catch { }
            }
        }
        /// <summary>
        /// 用空格补足指定的位数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected string FillWithSpace(object obj, int length)
        {
            if (obj == null || length < 1)
            {
                return string.Empty;
            }
            if (obj.ToString().Length > length)
            {
                return obj.ToString().Substring(0,length);
            }
            string objStr = obj.ToString();
            int objStrLength = objStr.Length;
            for (int i = objStrLength; i < length; i++)
            {
                objStr = " " + objStr;
            }
            return objStr;
        }
        #endregion
    }
}