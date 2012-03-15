using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.DataAccessLayer.BusinessManage;

namespace OperatingManagement.Web.ucs
{
    public partial class ucTask : System.Web.UI.UserControl
    {
        private bool isAllowBlankItem = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TaskList.Items.Clear();
                TaskList.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.CenterOutputPolicyTaskList);
                TaskList.DataTextField = "key";
                TaskList.DataValueField = "value";
                TaskList.DataBind();
                if (isAllowBlankItem)
                    TaskList.Items.Insert(0, new ListItem("请选择", ""));
            }
        }

        public ListItem SelectedItem
        {
            get
            {
                return TaskList.SelectedItem;
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
                TaskList.SelectedIndex = value;
            }
            get
            {
                return TaskList.SelectedIndex;
            }
        }
    }
}