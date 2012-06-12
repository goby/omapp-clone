#region
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

        protected void menuCut_MenuItemClick(object sender, MenuEventArgs e)
        {
            mvCut.ActiveViewIndex = Convert.ToInt32(menuCut.SelectedValue);
        }
        /// <summary>
        /// CutMain文件选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblCutMainFileOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            //手工录入
            if (rblCutMainFileOption.SelectedValue == "1")
            {
                tbCutMainUpload.Visible = false;
                tbCutMainFillIn.Visible = true;
            }
            //文件上传
            else if (rblCutMainFileOption.SelectedValue == "0")
            {
                tbCutMainUpload.Visible = true;
                tbCutMainFillIn.Visible = false;
            }
        }
        /// <summary>
        /// CutSub文件选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblCutSubFileOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            //手工录入
            if (rblCutSubFileOption.SelectedValue == "1")
            {
                tbCutSubUpload.Visible = false;
                tbCutSubFillIn.Visible = true;
            }
            //文件上传
            else if (rblCutSubFileOption.SelectedValue == "0")
            {
                tbCutSubUpload.Visible = true;
                tbCutSubFillIn.Visible = false;
            }
        }
        /// <summary>
        /// CutOptional文件选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblCutOptionalFileOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            //手工录入
            if (rblCutOptionalFileOption.SelectedValue == "1")
            {
                tbCutOptionalUpload.Visible = false;
                tbCutOptionalFillIn.Visible = true;
            }
            //文件上传
            else if (rblCutOptionalFileOption.SelectedValue == "0")
            {
                tbCutOptionalUpload.Visible = true;
                tbCutOptionalFillIn.Visible = false;
            }
        }
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
        /// CutSub主星SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplCutSubSatellite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCutSubSatelliteProperty();
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

        /// <summary>
        /// 开始计算交会预报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!ValidateCutMainProperty())
            {
                mvCut.ActiveViewIndex = 0;
            }
        }

        protected void lbtnDeleteCutSub_Click(object sender, EventArgs e)
        {
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
            //this.AddJavaScriptInclude("scripts/pages/businessmanage/OrbitDifferenceAnalysis.aspx.js");
        }

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
        /// 绑定卫星属性及与属性相关的值
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
        /// 绑定卫星属性及与属性相关的值
        /// </summary>
        private void BindCutSubSatelliteProperty()
        {
            Satellite objSatellite = new Satellite();
            objSatellite.Id = dplCutMainSatellite.SelectedValue;
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

        private bool ValidateCutMainProperty()
        {
            bool result = false;
            //手工录入
            if (rblCutMainFileOption.SelectedValue == "1")
            {
                if (string.IsNullOrEmpty(txtCutMainReportBeginDate.Text.Trim()))
                {
                    rfvCutMainReportBeginDate.IsValid = false;
                    txtCutMainReportBeginDate.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMainReportBeginTimeMilliSecond.Text.Trim()))
                {
                    rfvCutMainReportBeginTimeMilliSecond.IsValid = false;
                    txtCutMainReportBeginTimeMilliSecond.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMainReportTime.Text.Trim()))
                {
                    rfvCutMainReportTime.IsValid = false;
                    txtCutMainReportTime.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMainLYDate.Text.Trim()))
                {
                    rfvCutMainLYDate.IsValid = false;
                    txtCutMainLYDate.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMainLYTimeMilliSecond.Text.Trim()))
                {
                    rfvCutMainLYTimeMilliSecond.IsValid = false;
                    txtCutMainLYTimeMilliSecond.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMainD1.Text.Trim()))
                {
                    rfvCutMainD1.IsValid = false;
                    txtCutMainD1.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMainD2.Text.Trim()))
                {
                    rfvCutMainD2.IsValid = false;
                    txtCutMainD2.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMainD3.Text.Trim()))
                {
                    rfvCutMainD3.IsValid = false;
                    txtCutMainD3.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMainD4.Text.Trim()))
                {
                    rfvCutMainD4.IsValid = false;
                    txtCutMainD4.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMainD5.Text.Trim()))
                {
                    rfvCutMainD5.IsValid = false;
                    txtCutMainD5.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMainD6.Text.Trim()))
                {
                    rfvCutMainD6.IsValid = false;
                    txtCutMainD6.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMaindR.Text.Trim()))
                {
                    rfvCutMaindR.IsValid = false;
                    txtCutMaindR.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMaindA.Text.Trim()))
                {
                    rfvCutMaindA.IsValid = false;
                    txtCutMaindA.Focus();
                    return result;
                }
                if (string.IsNullOrEmpty(txtCutMaindE.Text.Trim()))
                {
                    rfvCutMaindE.IsValid = false;
                    txtCutMaindE.Focus();
                    return result;
                }
                DateTime cutMainReportBeginDate = DateTime.MinValue;
                if(!DateTime.TryParse(txtCutMainReportBeginDate.Text.Trim(), out cutMainReportBeginDate))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "预报起始历元日期格式错误。";
                    txtCutMainReportBeginDate.Focus();
                    return result;
                }
                double cutMainReportBeginTimeMilliSecond = 0;
                if(!double.TryParse(txtCutMainReportBeginTimeMilliSecond.Text.Trim(), out cutMainReportBeginTimeMilliSecond))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "预报起始历元时刻毫秒格式错误。";
                    return result;
                }
                cutMainReportBeginDate = cutMainReportBeginDate.AddDays(Convert.ToDouble(dplCutMainReportBeginTimeHour.SelectedValue));
                cutMainReportBeginDate = cutMainReportBeginDate.AddHours(Convert.ToDouble(dplCutMainReportBeginTimeMinute.SelectedValue));
                cutMainReportBeginDate = cutMainReportBeginDate.AddMinutes(Convert.ToDouble(dplCutMainReportBeginTimeSecond.SelectedValue));
                cutMainReportBeginDate = cutMainReportBeginDate.AddMilliseconds(cutMainReportBeginTimeMilliSecond);
                string s = cutMainReportBeginDate.ToString("yyyy MM dd HH:mm:sss.s");

                float du = 0.0F;
                if (!float.TryParse(txtCutMainReportTime.Text.Trim(), out du))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "预报起始历元时刻毫秒格式错误。";
                    return result;
                }

                DateTime cutMainLYDate = DateTime.MinValue;
                if (!DateTime.TryParse(txtCutMainLYDate.Text.Trim(), out cutMainLYDate))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "历元日期格式错误。";
                    return result;
                }
                double cutMainLYTimeMilliSecond = 0;
                if (!double.TryParse(txtCutMainLYTimeMilliSecond.Text.Trim(), out cutMainLYTimeMilliSecond))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "历元时刻毫秒格式错误。";
                    return result;
                }
                cutMainLYDate.AddDays(Convert.ToDouble(dplCutMainLYTimeHour.SelectedValue));
                cutMainLYDate.AddHours(Convert.ToDouble(dplCutMainLYTimeMinute.SelectedValue));
                cutMainLYDate.AddMinutes(Convert.ToDouble(dplCutMainLYTimeSecond.SelectedValue));
                cutMainLYDate.AddMilliseconds(cutMainLYTimeMilliSecond);
                cutMainLYDate.ToString("yyyy MM dd HH:mm:sss.s");

                float d1 = 0.000000F;
                if (!float.TryParse(txtCutMainD1.Text.Trim(), out d1))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "D1格式错误。";
                    return result;
                }
                float d2 = 0.000000F;
                if (!float.TryParse(txtCutMainD2.Text.Trim(), out d2))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "D2格式错误。";
                    return result;
                }
                float d3 = 0.000000F;
                if (!float.TryParse(txtCutMainD3.Text.Trim(), out d3))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "D3格式错误。";
                    return result;
                }
                float d4 = 0.000000F;
                if (!float.TryParse(txtCutMainD4.Text.Trim(), out d4))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "D4格式错误。";
                    return result;
                }
                float d5 = 0.000000F;
                if (!float.TryParse(txtCutMainD5.Text.Trim(), out d5))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "D5格式错误。";
                    return result;
                }
                float d6 = 0.000000F;
                if (!float.TryParse(txtCutMainD6.Text.Trim(), out d6))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "D6格式错误。";
                    return result;
                }
                float dR = 0.000000F;
                if (!float.TryParse(txtCutMaindR.Text.Trim(), out dR))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "dR格式错误。";
                    return result;
                }
                float dA = 0.000000F;
                if (!float.TryParse(txtCutMaindA.Text.Trim(), out dA))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "dA格式错误。";
                    return result;
                }
                float dE = 0.000000F;
                if (!float.TryParse(txtCutMaindE.Text.Trim(), out dE))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "dE格式错误。";
                    return result;
                }
                result = true;
            }
            //文件上传
            else if (rblCutMainFileOption.SelectedValue == "0")
            {
                if (!fuCutMainFile.HasFile)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择CutMain文件。";
                    return result;
                }
                if (fuCutMainFile.PostedFile.ContentLength == 0)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "CutMain文件不能为空。";
                    return result;
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
                    return result;
                }
                if (fileSize > fileMaxSize)
                {
                    string message = string.Format("上传CutMain文件不能超过{0}字节", fileMaxSize.ToString());
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"" + message + "\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return result;
                }
                result = true;
                //string xlDataFileDirectory = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitDifferenceAnalysis, "XLDataFileDirectory");
                //if (!Directory.Exists(xlDataFileDirectory))
                //    Directory.CreateDirectory(xlDataFileDirectory);
                ////星历数据文件服务器路径
                //string xlDataFilePath = Path.Combine(xlDataFileDirectory, Guid.NewGuid().ToString() + Path.GetExtension(fuXLDataFile.PostedFile.FileName));
                //fuXLDataFile.PostedFile.SaveAs(xlDataFilePath);
            }

            return result;
        }
        #endregion
    }
}