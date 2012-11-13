using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Configuration;

using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer;
using OperatingManagement.Framework;
using OperatingManagement.DataAccessLayer.PlanManage;
using OperatingManagement.DataAccessLayer.BusinessManage;
using ServicesKernel.File;
using ServicesKernel.DataFrame;
using ServicesKernel.GDFX;
using OperatingManagement.Framework.Storage;
using OperatingManagement.Framework.Components;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class SpaceTaskList : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Attributes.Add("readonly", "true");
                txtEndDate.Attributes.Add("readonly", "true");
                //btnSend.Attributes.Add("onclick", "javascript:return confirm('确定要发送所选数据吗?');");
                pnlAll1.Visible = false;
                pnlAll2.Visible = false;

                pnlDestination.Visible = false;
                pnlData.Visible = true;

                BindCheckBoxDestination();
                DefaultSearch();
            }
            cpPager.PostBackPage += new EventHandler(cpPager_PostBackPage);
        }

        /// <summary>
        /// 默认查询两周内的数据
        /// </summary>
        private void DefaultSearch()
        {
            txtStartDate.Text = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            btnSearch_Click(new Object(), new EventArgs());
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCondition();
                cpPager.CurrentPage = 1;
                BindGridView(true);
            }
            catch (Exception ex)
            {
                throw (new AspNetException("列表页面搜索出现异常，异常原因", ex));
            }
            finally { }
        }

        private void SaveCondition()
        {
            if (string.IsNullOrEmpty(txtStartDate.Text))
            { ViewState["_StartDate"] = null; }
            else
            { ViewState["_StartDate"] = txtStartDate.Text.Trim(); }
            if (string.IsNullOrEmpty(txtEndDate.Text))
            { ViewState["_EndDate"] = null; }
            else
            { ViewState["_EndDate"] = Convert.ToDateTime(txtEndDate.Text.Trim()).AddDays(1).AddMilliseconds(-1); }
        }

        //绑定列表
        void BindGridView(bool fromSearch)
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            if (fromSearch)
            {
                if (!string.IsNullOrEmpty(txtStartDate.Text))
                {
                    startDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                }
                //else
                //{
                //    startDate = DateTime.Now.AddDays(-14);
                //}
                if (!string.IsNullOrEmpty(txtEndDate.Text))
                {
                    endDate = Convert.ToDateTime(txtEndDate.Text).AddDays(1).AddMilliseconds(-1);   //查询时可查当天
                }
                //else
                //{
                //    endDate = DateTime.Now.AddDays(1).AddMilliseconds(-1);   //查询时可查当天
                //}
            }
            else
            {
                if (ViewState["_StartDate"] == null)
                {
                    //startDate = DateTime.Now.AddDays(-14);
                }
                else
                {
                    startDate = Convert.ToDateTime(ViewState["_StartDate"].ToString());
                }
                if (ViewState["_EndDate"] == null)
                {
                    //endDate = DateTime.Now;
                }
                else
                {
                    endDate = Convert.ToDateTime(ViewState["_EndDate"].ToString());
                }
            }

            List<YDSJ> listDatas = (new YDSJ()).GetListByDate(startDate, endDate);
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();

            if (listDatas.Count > 0)
            {
                pnlAll1.Visible = true;
                pnlAll2.Visible = true;
            }
            else
            {
                pnlAll1.Visible = false;
                pnlAll2.Visible = false;
            }
        }

        protected void cpPager_PostBackPage(object sender, EventArgs e)
        {
            BindGridView(false);
        }

        /// <summary>
        /// 绑定发送目标
        /// </summary>
        void BindCheckBoxDestination()
        {
            List<PlanParameter> targetList;
            ckbDestination.Items.Clear();
            targetList = PlanParameters.ReadParameters("YDSJSTTargetList");
            ckbDestination.DataSource = targetList;
            ckbDestination.DataTextField = "Text";
            ckbDestination.DataValueField = "Value";
            ckbDestination.DataBind();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            BindCheckBoxDestination();
        }
        //最终发送
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string cvtFilePath = string.Empty;
            lblMessage.Text = "";
            try
            {
                List<YDSJ> lstYDSJ = (new YDSJ()).SelectByIDS(txtId.Text);
                string filePath = SystemParameters.GetSystemParameterValue(SystemParametersType.GDJSResult, "result_path")
                    + SystemParameters.GetSystemParameterValue(SystemParametersType.GDJSResult, "cvt_path");
                if (!System.IO.Directory.Exists(filePath))
                    System.IO.Directory.CreateDirectory(filePath);
                cvtFilePath = System.IO.Path.Combine(filePath, Guid.NewGuid() + ".dat");
                StreamWriter oWriter = new StreamWriter(cvtFilePath);
                string strLine = string.Empty;
                int iIdx = 0;
                Dictionary<string, List<int>> dicTaskRow = new Dictionary<string, List<int>>();
                List<int> lstIdx;
                foreach (YDSJ oYdsj in lstYDSJ)
                {
                    strLine = string.Format("  {0}  {1}  {2}  {3}  {4}  {5}.{6}  {7}  {8}  {9}  {10}  {11}  {12}",
                        oYdsj.Times.Year, oYdsj.Times.Month, oYdsj.Times.Day, oYdsj.Times.Hour, oYdsj.Times.Minute
                        , oYdsj.Times.Second, oYdsj.Times.Millisecond.ToString("000000"), oYdsj.A.ToString("0000000000.000000")
                        , oYdsj.E.ToString("0000000000.000000"), oYdsj.I.ToString("0000000000.000000"), oYdsj.O.ToString("0000000000.000000")
                        , oYdsj.W.ToString("0000000000.000000"), oYdsj.M.ToString("0000000000.000000"));
                    oWriter.WriteLine(strLine);
                    //if (dicTaskRow.ContainsKey(oYdsj.SatName))
                    //    dicTaskRow[oYdsj.SatName].Add(iIdx);
                    //else
                    //{
                    //    lstIdx = new List<int>();
                    //    dicTaskRow.Add(oYdsj.TaskID, lstIdx);
                    //}
                    iIdx++;
                }
                oWriter.Close();
                int iTimeZone = 8;
                string strResultFile;
                //瞬时Kepler根数-J2000坐标系
                string strResult = new GDFXProcessor().ParamConvert(true, true, iTimeZone, "16"
                , cvtFilePath, cvtFilePath, out strResultFile);
                if (!strResult.Equals(string.Empty))
                {
                    lblMessage.Text = strResult;
                    return;
                }
                string[] datas = new string[iIdx];
                StreamReader oReader = new StreamReader(strResultFile);
                strLine = oReader.ReadLine();
                iIdx = 0;
                DateTime dt;
                while (!string.IsNullOrEmpty(strLine))
                {
                    strResult = strLine.Substring(0, 23).Trim();
                    dt = DateTime.ParseExact(strResult, "yyyy MM dd HH:mm:ss.f", System.Globalization.CultureInfo.InvariantCulture);
                    while (strLine.IndexOf("   ") >= 0)
                    {
                        strLine = strLine.Replace("   ", "  ");
                    }
                    datas[iIdx] = dt.ToString("yyyyMMdd") + "  " + dt.ToString("HHmmssffff") + strLine.Substring(23);
                    strLine = oReader.ReadLine();
                    iIdx++;
                }
                    //foreach (KeyValuePair<string, List<int>> kValue in dicTaskRow)
                    //{

                    //}

                FileSender objFileSender = new FileSender();
                bool blResult = true; //发送结果
                XYXSInfo objXYXSInfo = new XYXSInfo();
                //发送协议
                CommunicationWays protocl = (CommunicationWays)Convert.ToInt32(rbtProtocl.SelectedValue);
                string strYDSJCode = PlanParameters.ReadParamValue("YDSJDataCode");
                //信息类型id
                int infotypeid = (new InfoType()).GetIDByExMark(strYDSJCode);
                //发送方ID （运控中心 YKZX）
                int senderid = objXYXSInfo.GetIdByAddrMark(System.Configuration.ConfigurationManager.AppSettings["ZXBM"]);
                //接收方ID 
                int reveiverid;
                foreach (ListItem li in ckbDestination.Items)
                {
                    if (li.Selected)
                    {
                        reveiverid = objXYXSInfo.GetIdByAddrMark(li.Value);
                        strResultFile = new PlanFileCreator().CreateSendingYDSJFile(ConfigurationManager.AppSettings["CurTaskNo"], li.Value, datas);
                        blResult = objFileSender.SendFile(GetFileNameByFilePath(strResultFile), GetFilePathByFilePath(strResultFile), protocl, senderid, reveiverid, infotypeid, true);
                        if (blResult)
                        {
                            lblMessage.Text += GetFileNameByFilePath(strResultFile) + " 文件发送请求提交成功。" + "<br />";
                        }
                        else
                        {
                            lblMessage.Text += GetFileNameByFilePath(strResultFile) + " 文件发送请求提交失败。" + "<br />";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (new AspNetException("发送引导数据出现异常，异常原因", ex));
            }
            finally
            {
                if (File.Exists(cvtFilePath))
                    File.Delete(cvtFilePath); 
            }

        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDestination.Visible = false;
            pnlData.Visible = true;
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMPLAN_SpaceTask.View";
            this.ShortTitle = "查看空间机动任务";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanManage/SpaceTaskList.aspx.js");
        }

        protected void btnHidden_Click(object sender, EventArgs e)
        {
            pnlDestination.Visible = true;
            pnlData.Visible = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Request.CurrentExecutionFilePath);
        }

        /// <summary>
        /// 获取文件完整路径下的文件名
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private string GetFileNameByFilePath(string filepath)
        {
            return filepath.Substring(filepath.LastIndexOf("\\") + 1);
        }

        /// <summary>
        /// 获取文件完整路径下的文件路径
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private string GetFilePathByFilePath(string filepath)
        {
            return filepath.Substring(0, filepath.LastIndexOf("\\") + 1);
        }
    }
}