using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.Framework.Storage;
using OperatingManagement.Framework.Core;
using ServicesKernel.GDFX;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class GDJHFX : AspNetPage
    {
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_JHFX.Caculate";
            this.ShortTitle = "交会分析";
            this.SetTitle();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            string filePath = GlobalSettings.MapPath(@"~\GDDLL\CutAna\File\");
            if (!fuSubFile.HasFile)
            {
                ShowMessage("主星文件不能为空");
                return;
            }

            if (!Path.GetExtension(fuSubFile.FileName).Trim('.').Equals("txt",
                StringComparison.InvariantCultureIgnoreCase))
            {
                ShowMessage("“主星文件”格式只能为TXT。");
                return;
            }

            if (!fuTgtFile.HasFile)
            {
                ShowMessage("目标星文件不能为空");
                return;
            }

            if (!Path.GetExtension(fuTgtFile.FileName).Trim('.').Equals("txt",
                StringComparison.InvariantCultureIgnoreCase))
            {
                ShowMessage("“目标星文件”格式只能为TXT。");
                return;
            }

            string subNewPath = Path.Combine(filePath, Guid.NewGuid() + ".txt");
            fuSubFile.SaveAs(subNewPath);

            string tgtNewPath = Path.Combine(filePath, Guid.NewGuid() + ".txt");
            fuTgtFile.SaveAs(tgtNewPath);

            GDFXProcessor oProc = new GDFXProcessor();
            string strResult = string.Empty;
            string resultFilePath = string.Empty;

            strResult = oProc.CutAnalyze(subNewPath, tgtNewPath, out resultFilePath);
            if (strResult.Equals(string.Empty))
                ShowMessage("计算成功。");
            else
                ShowMessage(strResult);
        }

        private void ShowMessage(string msg)
        {
            ltMessage.Text = msg;
        }
    }
}