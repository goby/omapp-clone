using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using OperatingManagement.Framework;

namespace OperatingManagement.WebKernel.HttpHandlers
{
    public class DataHandler:AbHttpHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            var req = context.Request; 
            string msg = string.Empty;
            bool suc = false;
            
            WriteResponse(msg, suc);
        }
    }
}
