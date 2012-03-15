using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.DataAccessLayer;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.Views.UserAndRole
{
    public partial class PermissionAdd : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitialPageData();
        }

        private void InitialPageData()
        {
            HideMessage();
            try
            {
                BindDataSource();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        private void BindDataSource()
        {
            cblActions.Items.Clear();
            cblActions.DataSource = new DataAccessLayer.System.Task().SelectAll();
            cblActions.DataTextField = "TaskNote";
            cblActions.DataValueField = "Id";
            cblActions.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            DataAccessLayer.System.Module oModule = new DataAccessLayer.System.Module();
            oModule.ModuleName = txtName.Text;
            oModule.ModuleNote = txtNote.Text;
            string strActionIds = "";
            foreach (ListItem item in cblActions.Items)
            {
                if (item.Selected)
                    strActionIds += "," + item.Value;
            }

            oModule.ActionIds = strActionIds.Substring(1);
            Framework.FieldVerifyResult result = oModule.Add();
            string msg = "";
            switch (result)
            {
                case Framework.FieldVerifyResult.Error:
                    msg = "发生了数据错误，无法完成请求的操作。";
                    break;
                case Framework.FieldVerifyResult.Success:
                    msg = "新增权限已成功。";
                    break;
                case Framework.FieldVerifyResult.NameDuplicated2:
                    msg = "已存在相同名称，请输入其他“名称”。";
                    break;
            }
            ShowMessage(msg);
        }

        /// <summary>
        /// 隐藏提示信息
        /// </summary>
        private void HideMessage()
        {
            trMessage.Visible = false;
            ltMessage.Text = "";
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            trMessage.Visible = true;
            ltMessage.Text = message;
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OMRermissionManage.Add";
            this.ShortTitle = "新增权限";
            this.SetTitle();
        }
    }
}