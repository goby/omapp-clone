using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using OperatingManagement.Framework;

namespace OperatingManagement.WebKernel.HttpHandlers
{
    public class LoginHandler:AbHttpHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            var req = context.Request; 
            string msg = string.Empty;
            bool suc = false;
            //dynamic loginData = new ExpandoObject();
            //loginData.userName = req["uid"];
            //loginData.password = req["pwd"];
            //UserVerifyStatus vs = Business.Provider.UserValidate(loginData.userName, loginData.password);

            //switch (vs)
            //{
            //    case UserVerifyStatus.NotFound:
            //    case UserVerifyStatus.PasswordIncredible:
            //    case UserVerifyStatus.Error:
            //        msg = vs.ToString().ToLower();
            //        break;
            //    case UserVerifyStatus.Success:
            //        HttpCookie cookie = FormsAuthentication.GetAuthCookie(loginData.userName, true);
            //        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            //        FormsAuthenticationTicket newticket = new FormsAuthenticationTicket(
            //                                                                        ticket.Version,
            //                                                                        ticket.Name,
            //                                                                        ticket.IssueDate,
            //                                                                        ticket.Expiration,
            //                                                                        ticket.IsPersistent,
            //                                                                        DateTime.Now.ToShortDateString());
            //        cookie.Value = FormsAuthentication.Encrypt(newticket);
            //        TMGCookie.AddCookie(cookie);
            //        string url = FormsAuthentication.GetRedirectUrl(loginData.userName, true);
            //        suc = true;
            //        msg = url;
            //        break;
            //}
            WriteResponse(msg, suc);
        }
    }
}
