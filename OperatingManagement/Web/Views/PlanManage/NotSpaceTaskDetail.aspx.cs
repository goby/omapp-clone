﻿using System;
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
    public partial class NotSpaceTaskDetail : AspNetPage, IRouteContext
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

            /*lblVersion.Text = obj.Version;
            lblFlag.Text = obj.Flag;
            lblMainType.Text = obj.MainType;
            lblDataType.Text = obj.DataType;
            lblSource.Text = obj.SourceAddress;
            lblDestination.Text = obj.DestinationAddress;
            lblMissionCode.Text = obj.MissionCode;
            lblSatelliteCode.Text = obj.SatelliteCode;
            lblDataDate.Text = obj.DataDate.ToShortDateString();
            lblDataTime.Text = obj.DataTime;
            */
            lblD.Text = obj.Times.ToString("yyyyMMdd HH:mm:ss.fff");
            lblT.Text = obj.T.ToString();
            lblA.Text = obj.A.ToString();
            lblRa.Text = obj.Ra.ToString();
            lblRp.Text = obj.Rp.ToString();
            lblE.Text = obj.E.ToString();
            lblI.Text = obj.I.ToString();
            lblOhm.Text = obj.Q.ToString();
            lblOmega.Text = obj.W.ToString();
            lblM.Text = obj.M.ToString();
            lblP.Text = obj.P.ToString();
            lblPi.Text = obj.PP.ToString();
        }

        public override void OnPageLoaded()
        {
            this.PagePermission = "NotSpaceTask.Detail";
            this.ShortTitle = "非空间引导数据明细";
            base.OnPageLoaded();
            //this.AddJavaScriptInclude("scripts/pages/");
        }

    }
}