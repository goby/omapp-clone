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
            if (ucOutTask1.SelectedValue == "-1")
            { ViewState["_Task"] = null; }
            else
            { ViewState["_Task"] = ucOutTask1.SelectedValue; }
        }

        //绑定列表
        void BindGridView(bool fromSearch)
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            string TaskID = "-1";
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
                TaskID = ucOutTask1.SelectedValue;
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
                if (ViewState["_Task"] != null)
                { TaskID = ViewState["_Task"].ToString(); }
            }
            //外部任务代号
            List<YDSJ> listDatas = (new YDSJ()).GetListByDate(startDate, endDate, TaskID);
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
            string taskID = string.Empty;
            string satID = string.Empty;
            string outTaskID = string.Empty;
            int iIdx = 0;
            string twoSpace = "  ";
            string[] datas;
            bool blResult;
            lblMessage.Text = "";

            #region 参数转换计算
            try
            {
                int iTimeZone = 8;
                int cvtType = 16;//瞬时Kepler根数-J2000坐标系
                int[] iEmitPath = DataValidator.GetIntPath(@"D:\Deploy\GDFiles\Convert\");//这个参数在计算的时候必须提供，但此时无意义
                int[] iEmitFile = DataValidator.GetIntPath("Launch.dat");//这个参数在计算的时候必须提供，但此时无意义
                List<YDSJ> lstYDSJ = (new YDSJ()).SelectByIDS(txtId.Text);
                double[] dblResult;
                datas = new string[lstYDSJ.Count()];
                for (int i =0;i<lstYDSJ.Count();i++)
                {
                    iIdx = lstYDSJ[i].Id;
                    blResult = ParamConvertor.Instance.ParamConvert(true, true, iTimeZone, cvtType
                        , GetYM(lstYDSJ[i].Times), (lstYDSJ[i].Times.Second + lstYDSJ[i].Times.Millisecond)
                        , GetData(lstYDSJ[i]), iEmitFile, iEmitPath, out dblResult);
                    if (!blResult)
                    {
                        lblMessage.Text = string.Format("数据{0}参数转换出现错误", iIdx);
                        return;
                    }
                    datas[i] = lstYDSJ[i].Times.ToString("yyyyMMdd") + twoSpace
                        + lstYDSJ[i].Times.ToString("HHmmssffff") + twoSpace
                        + dblResult[0].ToString("f4") + twoSpace + dblResult[1].ToString("f6") + twoSpace
                        + dblResult[2].ToString("f4") + twoSpace + dblResult[3].ToString("f6") + twoSpace
                        + dblResult[4].ToString("f6") + twoSpace + dblResult[5].ToString("f6") + twoSpace;
                }
                outTaskID = lstYDSJ[0].TaskID;
            }
            catch (Exception ex)
            {
                throw (new AspNetException("发送引导数据-进行参数转换计算出现异常，异常原因", ex));
            }
            #endregion

            try
            {
                FileSender objFileSender = new FileSender();
                XYXSInfo objXYXSInfo = new XYXSInfo();
                string strResultFile = string.Empty;
                //发送协议
                CommunicationWays protocl = (CommunicationWays)Convert.ToInt32(rbtProtocl.SelectedValue);
                string strYDSJCode = PlanParameters.ReadParamValue("YDSJDataCode");
                //信息类型id
                int infotypeid = (new InfoType()).GetIDByExMark(strYDSJCode);
                //发送方ID （运控中心 YKZX）
                int senderid = objXYXSInfo.GetIdByAddrMark(System.Configuration.ConfigurationManager.AppSettings["ZXBM"]);
                //接收方ID 
                int reveiverid;
                string strResult = string.Empty;
                new Task().GetTaskNoSatID(outTaskID, out taskID, out satID);
                foreach (ListItem li in ckbDestination.Items)
                {
                    if (li.Selected)
                    {
                        reveiverid = objXYXSInfo.GetIdByAddrMark(li.Value);
                        strResultFile = new PlanFileCreator().CreateSendingYDSJFile(taskID, satID, li.Value, datas);

                        if (protocl == CommunicationWays.FTP)//将文件移至FTP路径中
                        {
                            strResult = DataFileHandle.MoveFile(strResultFile, GetFilePathByFilePath(strResultFile) + @"FTP\");
                            if (!strResult.Equals(string.Empty))
                                lblMessage.Text += GetFileNameByFilePath(strResultFile) + " 路径中已有同名文件。" + "<br />";
                            else
                                strResultFile = GetFilePathByFilePath(strResultFile) + @"FTP\" + GetFileNameByFilePath(strResultFile);
                        }
                        if (strResult.Equals(string.Empty))
                        {
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
            }
            catch (Exception ex)
            {
                throw (new AspNetException("发送引导数据出现异常，异常原因", ex));
            }
            finally
            {
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

        private int[] GetYM(DateTime date)
        {
            int[] ym = new int[5];
            ym[0] = date.Year;
            ym[1] = date.Month;
            ym[2] = date.Day;
            ym[3] = date.Hour;
            ym[4] = date.Minute;
            return ym;
        }

        private double[] GetData(YDSJ oYdsj)
        {
            double[] data = new double[6];
            data[0] = oYdsj.A;
            data[1] = oYdsj.E;
            data[2] = oYdsj.I;
            data[3] = oYdsj.O;
            data[4] = oYdsj.W;
            data[5] = oYdsj.M;
            return data;
        }
    }
}