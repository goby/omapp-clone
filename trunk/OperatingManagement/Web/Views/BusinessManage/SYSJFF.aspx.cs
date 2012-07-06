using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Threading;

using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer.PlanManage;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Components;
using OperatingManagement.DataAccessLayer.BusinessManage;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class SYSJFF : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideMessage();
            if (!Page.IsPostBack)
            {
                txtFrom.Attributes.Add("readonly", "true");
                txtTo.Attributes.Add("readonly", "true");
                HidControl();
            }
            cpFZData.PostBackPage += new EventHandler(cpFZData_PostBackPage);
            cpUFData.PostBackPage += new EventHandler(cpUFData_PostBackPage);
            cpYCData.PostBackPage += new EventHandler(cpYCData_PostBackPage);
        }

        protected void cpFZData_PostBackPage(object sender, EventArgs e)
        {
            BindData(false);
        }

        protected void cpUFData_PostBackPage(object sender, EventArgs e)
        {
            BindData(false);
        }

        protected void cpYCData_PostBackPage(object sender, EventArgs e)
        {
            BindData(false);
        }
        /// <summary>
        /// 绑定YC控件数据源
        /// </summary>
        /// <param name="listDatas"></param>
        private void BindYCDataSource(List<YCPG> listDatas)
        {
            vYCData.Visible = true;
            cpYCData.DataSource = listDatas;
            cpYCData.PageSize = this.SiteSetting.PageSize;
            if (listDatas.Count > this.SiteSetting.PageSize)
                cpYCData.Visible = true;
            cpYCData.BindToControl = rpYCData;
            rpYCData.DataSource = cpYCData.DataSourcePaged;
            rpYCData.DataBind();
        }

        /// <summary>
        /// 绑定UF控件数据源
        /// </summary>
        /// <param name="listDatas"></param>
        private void BindUFDataSource(List<UserFrame> listDatas)
        {
            vUFData.Visible = true;
            cpUFData.DataSource = listDatas;
            cpUFData.PageSize = this.SiteSetting.PageSize;
            if (listDatas.Count > this.SiteSetting.PageSize)
                cpUFData.Visible = true;
            cpUFData.BindToControl = rpUFData;
            rpUFData.DataSource = cpUFData.DataSourcePaged;
            rpUFData.DataBind();
        }

        /// <summary>
        /// 绑定FZ控件数据源
        /// </summary>
        /// <param name="listDatas"></param>
        private void BindFZDataSource(List<JH> listDatas)
        {
            vFZData.Visible = true;
            cpFZData.DataSource = listDatas;
            cpFZData.PageSize = this.SiteSetting.PageSize;
            if (listDatas.Count > this.SiteSetting.PageSize)
                cpFZData.Visible = true;
            cpFZData.BindToControl = rpFZData;
            rpFZData.DataSource = cpFZData.DataSourcePaged;
            rpFZData.DataBind();
        }

        /// <summary>
        /// 隐藏提示信息
        /// </summary>
        private void HideMessage()
        {
            trMessage.Visible = false;
            lblMessage.Text = "";
        }

        private void HidControl()
        {
            vUFData.Visible = false;
            vYCData.Visible = false;
            vFZData.Visible = false;
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            trMessage.Visible = true;
            lblMessage.Text = message;
        }


        /// <summary>
        /// 初始化页面基本信息
        /// </summary>
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_SYSJFF.View";
            this.ShortTitle = "试验数据分发";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/BusinessManage/SYSJFF.aspx.js");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(true);
        }

        public void BindData(bool fromSearchBtn)
        {
            List<YCPG> listYCData = null;
            List<UserFrame> listUFData = null;
            HidControl();

            #region 取查询条件
            string taskNo = string.Empty;
            string satID = string.Empty;
            string sType = string.Empty;
            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MinValue;
            if (fromSearchBtn)
            {
                sType = ddlDataType.SelectedValue;
                satID = (ucSatellite1.SelectedIndex == 0 ? "" : ucSatellite1.SelectedValue);
                taskNo = (ucTask1.SelectedIndex == 0 ? "" : ucTask1.SelectedValue);
                cpFZData.CurrentPage = 1;
                cpUFData.CurrentPage = 1;
                cpYCData.CurrentPage = 1;
                if (txtFrom.Text != "")
                {
                    if (!DateTime.TryParse(txtFrom.Text.Trim(), out dtFrom))
                    {
                        ShowMessage("开始日期格式非法");
                        return;
                    }
                }
                if (txtTo.Text != "")
                {
                    if (!DateTime.TryParse(txtTo.Text.Trim(), out dtTo))
                    {
                        ShowMessage("结束日期格式非法");
                        return;
                    }
                }

                if (dtFrom != DateTime.MinValue && dtTo != DateTime.MinValue && dtFrom > dtTo)
                {
                    ShowMessage("结束日期必须大于开始日期");
                    return;
                }
                this.ViewState["_filter"] = dtFrom.ToLongDateString() + "," + dtTo.ToLongDateString() + "," 
                    + sType + "," + taskNo + "," + satID;
            }
            else
            {
                string[] strFilters = new string[0];
                if (this.ViewState["_filter"] != null)
                    strFilters = this.ViewState["_filter"].ToString().Split(new char[] { ',' });
                if (strFilters.Length == 5)
                {
                    DateTime.TryParse(strFilters[0], out dtFrom);
                    DateTime.TryParse(strFilters[1], out dtTo);
                    sType = strFilters[2];
                    taskNo = strFilters[3];
                    satID = strFilters[4];
                }
            }
            #endregion

            switch (sType)
            {
                case "0"://TJ
                    try
                    {
                        listYCData = new YCPG().GetSYSJ(dtFrom, dtTo, sType, taskNo, satID);
                        BindYCDataSource(listYCData);
                    }
                    catch (Exception ex)
                    {
                        throw (new AspNetException("天基目标观测试验数据-状态数据，获取出现异常", ex));
                    }
                    finally { }
                    try
                    {
                        listUFData = new UserFrame().GetSYSJ(dtFrom, dtTo, taskNo, satID);
                        BindUFDataSource(listUFData);
                    }
                    catch (Exception ex)
                    {
                        throw (new AspNetException("天基目标观测试验数据-图像数据，获取出现异常", ex));
                    }
                    finally { }
                    break;
                case "1"://KJ
                    try
                    {
                        listYCData = new YCPG().GetSYSJ(dtFrom, dtTo, sType, taskNo, satID);
                        BindYCDataSource(listYCData);
                    }
                    catch (Exception ex)
                    {
                        throw (new AspNetException("空间机动试验数据，获取出现异常", ex));
                    }
                    finally { }
                    break;
                case "2"://FZ
                    try
                    {
                        List<JH> listFZData = new JH().GetJHList("TYSJ", dtFrom, dtTo);
                        BindFZDataSource(listFZData);
                    }
                    catch (Exception ex)
                    {
                        throw (new AspNetException("仿真推演试验数据，获取出现异常", ex));
                    }
                    finally { }
                    break;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            BindData(false);

            string strYCIds = hfycids.Value;
            string strUFIds = hfufids.Value;
            string strFZIds = hffzids.Value;

            if (strYCIds.Equals(string.Empty) && strUFIds.Equals(string.Empty) && strFZIds.Equals(string.Empty))
            {
                ShowMessage("没有选择要发送的数据。");
                return;
            }
            return;

            #region 判断是否多选了，一种类型试验数据的子类只允许选一个
            bool blValid = true;
            string[] ycids = new string[0];
            if (!strYCIds.Equals(string.Empty))
                ycids = strYCIds.Split(new char[] { ',' });
            string[] ufids = new string[0];
            if (!strUFIds.Equals(string.Empty))
                ufids = strUFIds.Split(new char[] { ',' });
            string[] fzids = new string[0];
            if (!strFZIds.Equals(string.Empty))
                fzids = strFZIds.Split(new char[] { ',' });
            string types = string.Empty;
            string stmp = string.Empty;
            switch (ddlDataType.SelectedValue)
            {
                case "0"://TJ
                    if (ycids.Length > 1)
                    {
                        ShowMessage("只允许选择一条状态数据。");
                        return;
                    }
                    for (int i = 0; i < ufids.Length; i++)
                    {
                        stmp = ufids[i].Substring(ufids[i].IndexOf(';') + 1);
                        if (types.IndexOf(stmp) >= 0)
                        {
                            ShowMessage("一类图像数据只允许选择一条。");
                            blValid = false;
                            break;
                        }
                        else
                            types += stmp;
                    }
                    break;
                case "1"://KJ
                    for (int i = 0; i < ycids.Length; i++)
                    {
                        stmp = ycids[i].Substring(ycids[i].IndexOf(';') + 1);
                        if (types.IndexOf(stmp) >= 0)
                        {
                            ShowMessage("一类数据只允许选择一条。");
                            blValid = false;
                            break;
                        }
                        else
                            types += stmp;
                    }
                    break;
                case "2"://FZ
                    if (fzids.Length > 1)
                    {
                        ShowMessage("只允许选择一条仿真推演试验数据。");
                        return;
                    }
                    else if (fzids.Length == 1)
                        fzids[0] = fzids[0].Substring(0, fzids[0].IndexOf(';'));
                    break;
            }
            #endregion
            if (!blValid)
                return;

            Thread oThread = new Thread(new ThreadStart(Data2File));
            oThread.Start();
            ShowMessage("已提交后台进行处理。");
        }

        /// <summary>
        /// 把数据转成文件
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="dataType"></param>
        private void Data2File()
        {
            string strYCIds = hfycids.Value;
            string strUFIds = hfufids.Value;
            string strFZIds = hffzids.Value;
            string sendWay = hfsendway.Value;
            DateTime dt = DateTime.Now;
            string[] ycids = new string[0];
            if (!strYCIds.Equals(string.Empty))
                ycids = strYCIds.Split(new char[] { ',' });
            string[] ufids = new string[0];
            if (!strUFIds.Equals(string.Empty))
                ufids = strUFIds.Split(new char[] { ',' });
            string[] fzids = new string[0];
            if (!strFZIds.Equals(string.Empty))
                fzids = strFZIds.Split(new char[] { ',' });

            #region 隐藏域中存的是ID+type，需重置IDs
            string types = string.Empty;
            string stmp = string.Empty;
            switch (ddlDataType.SelectedValue)
            {
                case "0"://TJ
                    if (ycids.Length == 1)
                        ycids[0] = ycids[0].Substring(0, ycids[0].IndexOf(';'));
                    for (int i = 0; i < ufids.Length; i++)
                    {
                        ufids[i] = ufids[i].Substring(0, ufids[i].IndexOf(';'));
                    }
                    break;
                case "1"://KJ
                    for (int i = 0; i < ycids.Length; i++)
                    {
                        ycids[i] = ycids[i].Substring(0, ycids[i].IndexOf(';'));
                    }
                    break;
                case "2"://FZ
                    if (fzids.Length == 1)
                        fzids[0] = fzids[0].Substring(0, fzids[0].IndexOf(';'));
                    break;
            }
            #endregion

            BizFileCreator oBFCreator;
            string taskNo = ucTask1.SelectedValue;

            switch (ddlDataType.SelectedValue)
            {
                case "0"://TJ
                    oBFCreator = new BizFileCreator();
                    oBFCreator.CreateGCSJDataFile(ycids, ufids, taskNo, (CommunicationWays)Convert.ToInt32(sendWay));
                    break;
                case "1"://KJ
                    oBFCreator = new BizFileCreator();
                    oBFCreator.CreateJDSJDataFile(ycids, taskNo, (CommunicationWays)Convert.ToInt32(sendWay));
                    break;
                case "2"://FZ
                    string dataType = "TYSJ";
                    PlanFileCreator oPFCreator = new PlanFileCreator();
                    XYXSInfo oXyxs =  new XYXSInfo().GetByAddrMark(FileExchangeConfig.GetTgtListForSending(dataType)[0]);
                    string filename = oPFCreator.CreateSendingTYSJFile(strFZIds, oXyxs.ADDRMARK, oXyxs.ADDRName + "(" + oXyxs.EXCODE + ")");
                    int desID = oXyxs.Id;
                    int sourceID = new XYXSInfo().GetByAddrMark(Param.SourceCode).Id;
                    int infoId = new InfoType().GetIDByExMark(dataType);

                    FileSender oSender = new FileSender();
                    oSender.SendFile(filename, Param.OutPutPath, (CommunicationWays)Convert.ToInt32(sendWay), sourceID, desID, infoId, true);
                    break;
            }
        }

    }
}