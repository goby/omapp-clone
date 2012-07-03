using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OperatingManagement.WebKernel.Basic;
using OperatingManagement.Framework.Storage;
using OperatingManagement.Framework.Core;
using System.IO;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class GDJHFX : AspNetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            //计算前检查文件是否都存在，CutAnalyzeData.dat、JPLEPH、TESTRECL、WGS84.GEO、eopc04_IAU2000.dat

        }
    }
}