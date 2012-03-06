using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.DataAccessLayer.BusinessManage;

namespace OperatingManagement.Web.ucs
{
    public partial class ucXYXSInfo : System.Web.UI.UserControl
    {
        private bool isAllowBlankItem = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindDataSource();
        }

        private void BindDataSource()
        {
            XYXSInfo objInfos = new XYXSInfo();
            XYXSList.Items.Clear();
            XYXSList.DataSource = objInfos.Cache;
            XYXSList.DataTextField = "ADDRNAME";
            XYXSList.DataValueField = "id";
            XYXSList.DataBind();
            if (isAllowBlankItem)
                XYXSList.Items.Insert(0, new ListItem("请选择", "0"));
        }

        public ListItem SelectedItem
        {
            get
            {
                return XYXSList.SelectedItem;
            }
        }

        public int SelectedIndex
        {
            get { return XYXSList.SelectedIndex; }
            set { XYXSList.SelectedIndex = value; }
        }

        public bool AllowBlankItem
        {
            set
            {
                isAllowBlankItem = value;
            }
        }
    }
}