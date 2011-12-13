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
using OperatingManagement.DataAccessLayer.PlanManage;
using System.Web.Security;
using System.Data;
using ServicesKernel.File;

namespace OperatingManagement.Web.Views.PlanManage
{
    public partial class PlanList : AspNetPage, IRouteContext
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "Plan.List";
            this.ShortTitle = "查询计划";
            this.SetTitle();
            this.AddJavaScriptInclude("scripts/pages/PlanList.aspx.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlDestination.Visible = false;
                pnlData.Visible = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            BindGridView();
        }

        //绑定列表
        void BindGridView()
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                startDate = Convert.ToDateTime(txtStartDate.Text);
            }
            else
            {
                startDate = DateTime.Now.AddDays(-14);
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                endDate = Convert.ToDateTime(txtEndDate.Text);
            }
            else
            {
                endDate = DateTime.Now;
            }
            string planType = rbtType.Text;
            string planAging = ddlAging.SelectedValue;

            List<JH> listDatas = (new JH()).GetJHList(planType, planAging, startDate, endDate);
            cpPager.DataSource = listDatas;
            cpPager.PageSize = this.SiteSetting.PageSize;
            cpPager.BindToControl = rpDatas;
            rpDatas.DataSource = cpPager.DataSourcePaged;
            rpDatas.DataBind();
        }


        //最终发送
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string plantype = txtPlanType.Text;
            string planid = txtPlanID.Text;
            string id = txtId.Text;
            JH objJH = new JH();
            objJH.Id = Convert.ToInt32(id);
            FileNameMaker fm = new FileNameMaker();
            CreatePlanFile cpf = new CreatePlanFile();
            string filename="";
            switch (plantype)
            {
                case "YJJH":
                    filename =fm.GenarateFileNameTypeThree("YJJH", "YKZX", rbtDestination.SelectedValue, ".PLA");
                    YJJH obj = new YJJH();
                    obj = obj.SelectById(Convert.ToInt32(planid));
                    cpf.ID = obj.ID;
                    cpf.CTime = obj.CTime;
                    cpf.Source = obj.Source;
                    cpf.Destination = obj.Destination;
                    cpf.Format1 = obj.Format1;
                    cpf.DataSection = obj.DataSection;
                    cpf.SavePath = System.Configuration.ConfigurationManager.AppSettings["savepath"];
                    cpf.FilePath = cpf.SavePath + filename;
                    cpf.NewFile();
                    obj.FileIndex = cpf.FilePath;
                    obj.UpdateFileIndex();

                    objJH.FileIndex = cpf.FilePath;
                    break;
                case "XXXQ":
                    filename = fm.GenarateFileNameTypeThree("MBXQ", "YKZX", rbtDestination.SelectedValue, ".REG");
                    XXXQ objx = new XXXQ();
                    objx = objx.SelectById(Convert.ToInt32(planid));
                    cpf.ID = objx.ID;
                    cpf.CTime = objx.CTime;
                    cpf.Source = objx.Source;
                    cpf.Destination = objx.Destination;
                    cpf.Format1 = objx.Format1;
                    cpf.DataSection = objx.DataSection;
                    cpf.SavePath = System.Configuration.ConfigurationManager.AppSettings["savepath"];
                    cpf.FilePath = cpf.SavePath + filename;
                    cpf.NewFile();
                    objx.FileIndex = cpf.FilePath;
                    objx.UpdateFileIndex();

                    objJH.FileIndex = cpf.FilePath;
                    break;
                case "GZJH":
                    //filename = fm.GenarateFileNameTypeThree("GZJH", "YKZX", rbtDestination.SelectedValue, ".PLA");
                    break;
                case "ZXJH":
                    //filename = fm.GenarateFileNameTypeThree("YJJH", "YKZX", rbtDestination.SelectedValue, ".PLA");
                    break;
                case "TYSJ":
                    filename = fm.GenarateFileNameTypeThree("TYSJ", "YKZX", rbtDestination.SelectedValue, ".DAT");
                    TYSJ objt = new TYSJ();
                    objt = objt.SelectById(Convert.ToInt32(planid));
                    cpf.ID = objt.ID;
                    cpf.CTime = objt.CTime;
                    cpf.Source = objt.Source;
                    cpf.Destination = objt.Destination;
                    cpf.Format1 = objt.Format1;
                    cpf.DataSection = objt.DataSection;
                    cpf.SavePath = System.Configuration.ConfigurationManager.AppSettings["savepath"];
                    cpf.FilePath = cpf.SavePath + filename;
                    cpf.NewFile();
                    objt.FileIndex = cpf.FilePath;
                    objt.UpdateFileIndex();

                    objJH.FileIndex = cpf.FilePath;
                    break;
                case "SBJH":
                    filename = fm.GenarateFileNameTypeOne("SBJH","B",1);
                    break;

            }
            objJH.UpdateFileIndex();//更新计划表
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDestination.Visible = false;
            pnlData.Visible = true;
        }

        protected void btnHidden_Click(object sender, EventArgs e)
        {
            pnlDestination.Visible = true;
            pnlData.Visible = false;
            string plantype = txtPlanType.Text;
            switch (plantype)
            {
                case "YJJH":
                    rbtDestination.Items.Clear();
                    rbtDestination.Items.Add(new ListItem("天基目标观测应用研究分系统（GCYJ）", "GCYJ"));
                    rbtDestination.Items.Add(new ListItem("遥操作应用研究分系统（CZYJ）", "CZYJ"));
                    rbtDestination.Items.Add(new ListItem("空间机动应用研究分系统（JDYJ）", "JDYJ"));
                    rbtDestination.Items.Add(new ListItem("仿真推演分系统（FZTY）", "FZTY"));
                    break;
                case "XXXQ":
                    rbtDestination.Items.Clear();
                    rbtDestination.Items.Add(new ListItem("空间信息综合应用中心(XXZX)", "XXZX"));
                    break;
                case "GZJH":
                    rbtDestination.Items.Clear();
                    rbtDestination.Items.Add(new ListItem("西安中心（XSCC）", "XSCC"));
                    rbtDestination.Items.Add(new ListItem("总参二部信息处理中心（XXZX）", "XXZX"));
                    rbtDestination.Items.Add(new ListItem("总参三部技侦中心（JZZX）", "JZZX"));
                    rbtDestination.Items.Add(new ListItem("总参气象水文空间天气总站资料处理中心（ZLZX）", "ZLZX"));
                    rbtDestination.Items.Add(new ListItem("863-YZ4701遥科学综合站（JYZ1）", "JYZ1"));
                    rbtDestination.Items.Add(new ListItem("863-YZ4702遥科学综合站（JYZ2）", "JYZ2"));
                    break;
                case "ZXJH":
                    rbtDestination.Items.Clear();
                    break;
                case "TYSJ":
                    rbtDestination.Items.Clear();
                    rbtDestination.Items.Add(new ListItem("仿真推演分系统(FZTY)", "FZTY"));
                    break;
                case "SBJH":
                    rbtDestination.Items.Clear();
                    rbtDestination.Items.Add(new ListItem("运控评估中心YKZX(02 04 00 00)", "YKZX"));
                    break;

            }
            
        }
    }
}