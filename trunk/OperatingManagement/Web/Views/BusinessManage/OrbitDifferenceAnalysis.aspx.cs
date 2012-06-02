using System;
using System.Collections.Generic;
using System.Linq;
#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:OrbitDifferenceAnalysis.cs
//Remark:业务管理-轨道分析-差值分析
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      2012519    Create     
//------------------------------------------------------
#endregion
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.DataAccessLayer.BusinessManage;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class OrbitDifferenceAnalysis : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            trMessage.Visible = false;
            lblMessage.Text = string.Empty;
        }

        /// <summary>
        /// 开始计算差值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fuXLDataFile.HasFile)
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"请选择星历数据文件。\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择星历数据文件。";
                    return;
                }
                if (fuXLDataFile.PostedFile.ContentLength == 0)
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"星历数据文件不能为空。\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = "星历数据文件不能为空。";
                    return;
                }
                if (!fuDifCalTimeFile.HasFile)
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"请选择差值计算时间文件。\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择差值计算时间文件。";
                    return;
                }
                if (fuDifCalTimeFile.PostedFile.ContentLength == 0)
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"差值计算时间文件不能为空。\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = "差值计算时间文件不能为空。";
                    return;
                }

                //校验星历数据文件扩展名、文件大小等合法性
                int fileSize = fuXLDataFile.PostedFile.ContentLength;
                string fileExtension = Path.GetExtension(fuXLDataFile.PostedFile.FileName);
                int fileMaxSize = int.Parse(SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitDifferenceAnalysis, "XLDataFileMaxSize"));
                string allowFileExtension = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitDifferenceAnalysis, "XLDataFileExtension").Trim(new char[] { ',', '，', ';', '；' });
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
                    string message = string.Format("上传星历数据文件格式不正确，应为：{0}。", allowFileExtension);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"" + message + "\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return;
                }
                if (fileSize > fileMaxSize)
                {
                    string message = string.Format("上传星历数据文件不能超过{0}字节", fileMaxSize.ToString());
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"" + message + "\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return;
                }

                //校验差值计算时间文件扩展名、文件大小等合法性
                fileSize = fuDifCalTimeFile.PostedFile.ContentLength;
                fileExtension = Path.GetExtension(fuDifCalTimeFile.PostedFile.FileName);
                fileMaxSize = int.Parse(SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitDifferenceAnalysis, "DifCalTimeFileMaxSize"));
                allowFileExtension = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitDifferenceAnalysis, "DifCalTimeFileExtension").Trim(new char[] { ',', '，', ';', '；' });
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
                    string message = string.Format("上传差值计算时间文件格式不正确，应为：{0}。", allowFileExtension);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"" + message + "\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return;
                }
                if (fileSize > fileMaxSize)
                {
                    string message = string.Format("上传差值计算时间文件不能超过{0}字节", fileMaxSize.ToString());
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(\"" + message + "\")", true);
                    trMessage.Visible = true;
                    lblMessage.Text = message;
                    return;
                }

                string xlDataFileDirectory = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitDifferenceAnalysis, "XLDataFileDirectory").TrimEnd(new char[] { '\\' }) + "\\";
                if (!Directory.Exists(xlDataFileDirectory))
                    Directory.CreateDirectory(xlDataFileDirectory);
                string xlDataFilePath = Path.Combine(xlDataFileDirectory, Guid.NewGuid().ToString() + Path.GetExtension(fuXLDataFile.PostedFile.FileName));
                fuXLDataFile.PostedFile.SaveAs(xlDataFilePath);

                string difCalTimeFileDirectory = SystemParameters.GetSystemParameterValue(SystemParametersType.OrbitDifferenceAnalysis, "DifCalTimeFileDirectory").TrimEnd(new char[] { '\\' }) + "\\";
                if (!Directory.Exists(difCalTimeFileDirectory))
                    Directory.CreateDirectory(difCalTimeFileDirectory);
                string difCalTimeFilePath = Path.Combine(difCalTimeFileDirectory, Guid.NewGuid().ToString() + Path.GetExtension(fuDifCalTimeFile.PostedFile.FileName));
                fuDifCalTimeFile.PostedFile.SaveAs(difCalTimeFilePath);

                /**
                * TODO: 在这里开始计算。
                * */
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

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_CZFX.Caculate";
            this.ShortTitle = "差值分析";
            this.SetTitle();
            //this.AddJavaScriptInclude("scripts/pages/businessmanage/orbitparacalculate.aspx.js");
        }

        #region Method
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
        #endregion
    }
}