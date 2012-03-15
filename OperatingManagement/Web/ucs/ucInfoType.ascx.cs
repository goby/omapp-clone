using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.DataAccessLayer.BusinessManage;

namespace OperatingManagement.Web.ucs
{
    public partial class ucInfoType : System.Web.UI.UserControl
    {
        private bool isAllowBlankItem = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindDataSource();
        }

        private void BindDataSource()
        {
            InfoType objInfos = new InfoType();
            InfoTypeList.Items.Clear();
            InfoTypeList.DataSource = objInfos.Cache;
            InfoTypeList.DataTextField = "DATANAME";
            InfoTypeList.DataValueField = "id";
            InfoTypeList.DataBind();
            if (isAllowBlankItem)
                InfoTypeList.Items.Insert(0, new ListItem("请选择", "0"));
        }

        public ListItem SelectedItem
        {
            get {
                return InfoTypeList.SelectedItem;
            }
        }

        public bool AllowBlankItem
        {
            set
            {
                isAllowBlankItem = value;
            }
        }

        public int SelectedIndex
        {
            set
            {
                InfoTypeList.SelectedIndex = value;
            }
            get
            {
                return InfoTypeList.SelectedIndex;
            }
        }
    }
}