using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OperatingManagement.DataAccessLayer.BusinessManage;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.BusinessManage
{
    public partial class CenterOutputPolicyManage : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDataSource();
                    BindSatNameDataSource();
                    BindCOPList();
                }
            }
            catch
            {
                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindCOPList();
            }
            catch
            { }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string url = @"~/BusinessManage/CenterOutputPolicyAdd.aspx";
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnEdit = (sender as LinkButton);
                if (lbtnEdit == null)
                {
                    BindCOPList();
                    return;
                }
                string url = @"~/BusinessManage/CenterOutputPolicyEdit.aspx?copid=" + lbtnEdit.CommandArgument;
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }

        protected void lbtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnDownload = (sender as LinkButton);
                if (lbtnDownload == null)
                {
                    BindCOPList();
                    return;
                }
                int copID = 0;
                if (!int.TryParse(lbtnDownload.CommandArgument, out copID))
                {
                    BindCOPList();
                    return;
                }

                CenterOutputPolicy centerOutputPolicy = new CenterOutputPolicy();
                centerOutputPolicy.Id = copID;
                centerOutputPolicy = centerOutputPolicy.SelectByID();
                if (centerOutputPolicy == null)
                {
                    BindCOPList();
                    return;
                }
                StringBuilder strBuilder = new StringBuilder("ConfigurationInfo\r\n");
                strBuilder.AppendFormat("TaskID={0}\r\n", centerOutputPolicy.TaskID);
                strBuilder.AppendFormat("SatName={0}\r\n", centerOutputPolicy.SatName);
                strBuilder.AppendFormat("Source={0}\r\n", centerOutputPolicy.InfoSource);
                strBuilder.AppendFormat("InfoType={0}\r\n", centerOutputPolicy.InfoType);
                strBuilder.AppendFormat("Ddestination={0}\r\n", centerOutputPolicy.Ddestination);
                strBuilder.AppendFormat("EffectTime={0}\r\n", centerOutputPolicy.EffectTime.ToString("yyyy-MM-dd HH:mm:ss"));
                strBuilder.AppendFormat("DefectTime={0}\r\n", centerOutputPolicy.DefectTime.ToString("yyyy-MM-dd HH:mm:ss"));
                Response.Clear();
                Response.Buffer = false;
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "attachment;filename=" + centerOutputPolicy.TaskID + ".dat;");
                Response.Write(strBuilder.ToString());
                Response.Flush();
                Response.End();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch
            { }
        }

        #region Method
        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        private void BindDataSource()
        {
            CenterOutputPolicy centerOutputPolicy = new CenterOutputPolicy();

            dplTask.Items.Clear();
            dplTask.DataSource = centerOutputPolicy.GetSystemParameters(SystemParametersType.TaskList);
            dplTask.DataTextField = "key";
            dplTask.DataValueField = "value";
            dplTask.DataBind();
            dplTask.Items.Insert(0, new ListItem("全部", ""));
        }
        /// <summary>
        /// 绑定卫星数据源
        /// 等确定卫星表结构及来源后替换
        /// </summary>
        private void BindSatNameDataSource()
        {
            dplSatName.Items.Clear();
            for (int i = 1; i <= 5; i++)
            {
                dplSatName.Items.Add(new ListItem("卫星" + i.ToString(), i.ToString()));
            }
            dplSatName.Items.Insert(0, new ListItem("全部", ""));
        }

        private void BindCOPList()
        {
            CenterOutputPolicy centerOutputPolicy = new CenterOutputPolicy();
            centerOutputPolicy.TaskID = dplTask.SelectedValue;
            centerOutputPolicy.SatName = dplSatName.SelectedValue;
            cpPager.DataSource = centerOutputPolicy.SelectByParameters();
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpCOPList;
            rpCOPList.DataSource = cpPager.DataSourcePaged;
            rpCOPList.DataBind();
        }
       
        #endregion
    }
}