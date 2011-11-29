using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OperatingManagement.WebKernel.Basic;
using OperatingManagement.WebKernel.Route;
using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer;
using OperatingManagement.Framework;
using System.Web.Security;
using System.Xml;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class YDSJDetail : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.QueryStringObserver("id"))
                {
                    //string sID = this.DecryptString(Request.QueryString["ydsjid"]);
                    string sID = Request.QueryString["id"];
                    int id = 0;
                    Int32.TryParse(sID, out id);
                    BindFileInfo(id);
                }
            }
        }

        void BindFileInfo(int id)
        {
            DataAccessLayer.PlanManage.YDSJ y = new DataAccessLayer.PlanManage.YDSJ { Id = id };
            DataAccessLayer.PlanManage.YDSJ obj = y.SelectById();


            lblVersion.Text = obj.Version;
            lblFlag.Text = obj.Flag;
            lblMainType.Text = obj.MainType;
            lblDataType.Text = obj.DataType;
            lblSource.Text = obj.SourceAddress;
            lblDestination.Text = obj.DestinationAddress;
            lblMissionCode.Text = obj.MissionCode;
            lblSatelliteCode.Text = obj.SatelliteCode;
            lblDataDate.Text = obj.DataDate.ToShortDateString();
            lblDataTime.Text = obj.DataTime;

            lblD.Text = obj.D;
            lblT.Text = obj.T;
            lblA.Text = obj.A;
            lblE.Text = obj.E;
            lblI.Text = obj.I;
            lblOhm.Text = obj.Ohm;
            lblOmega.Text = obj.Omega;
            lblM.Text = obj.M;
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "YDSJ.Detail";
            this.ShortTitle = "引导数据明细";
            base.OnPageLoaded();
            //this.AddJavaScriptInclude("scripts/pages/");
        }
    }
}