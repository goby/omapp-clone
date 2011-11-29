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
    public partial class OribitalQuantityDetail : AspNetPage, IRouteContext
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.QueryStringObserver("id"))
                {
                    //string sID = this.DecryptString(Request.QueryString["gdid"]);
                    string sID = Request.QueryString["id"];
                    int id = 0;
                    Int32.TryParse(sID, out id);
                    BindFileInfo(id);
                }
            }
        }

        void BindFileInfo(int id)
        {
            DataAccessLayer.BusinessManage.GD g = new DataAccessLayer.BusinessManage.GD { Id = id };
            DataAccessLayer.BusinessManage.GD obj = g.SelectById();

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
            lblRa.Text = obj.Ra;
            lblRp.Text = obj.Rp;
            lblE.Text = obj.E;
            lblI.Text = obj.I;
            lblOhm.Text = obj.Ohm;
            lblOmega.Text = obj.Omega;
            lblM.Text = obj.M;
            lblP.Text = obj.P;
            lblPi.Text = obj.Pi;
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "OribitalQuantity.Detail";
            this.ShortTitle = "轨道根数明细";
            base.OnPageLoaded();
            //this.AddJavaScriptInclude("scripts/pages/");
        }

    }
}