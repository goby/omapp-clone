using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.DataAccessLayer.BusinessManage;

namespace OperatingManagement.Web.ucs
{
    public partial class ucSatellite : System.Web.UI.UserControl
    {
        private bool isAllowBlankItem = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            Satellite objSatellite = new Satellite();
            SatelliteList.Items.Clear();
            SatelliteList.DataSource = objSatellite.Cache;
            SatelliteList.DataTextField = "WXMC";
            SatelliteList.DataValueField = "Id";
            SatelliteList.DataBind();
            if (isAllowBlankItem)
                SatelliteList.Items.Insert(0, new ListItem("请选择", "0"));
        }

        public ListItem SelectedItem
        {
            get {
                return SatelliteList.SelectedItem;
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
                SatelliteList.SelectedIndex = value;
            }
            get
            {
                return SatelliteList.SelectedIndex;
            }
        }

        public string SelectedValue
        {
            set
            {
                SatelliteList.SelectedValue = value;
    }
            get
            {
                return SatelliteList.SelectedItem.Value;
            }
        }
    }
}