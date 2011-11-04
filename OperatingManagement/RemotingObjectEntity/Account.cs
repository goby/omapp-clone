using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatingManagement.RemotingObjectInterface;
using OperatingManagement.DataAccessLayer.System;
using OperatingManagement.Framework;
using System.Xml.Linq;

namespace OperatingManagement.RemotingObjectEntity
{
    public class Account:MarshalByRefObject, IAccount
    {
        public string ValidateUser(string userName, string password)
        {
            XElement root = new XElement("status");
            XElement code = new XElement("code");
            XElement msg = new XElement("msg");
            XElement user = new XElement("user");
            XElement roles = new XElement("roles");
            XElement permissions = new XElement("permissions");

            try
            {
                User u = new User()
                {
                    LoginName = userName,
                    Password = password
                };
                FieldVerifyResult retVal = u.Verify();
                switch (retVal)
                {
                    case FieldVerifyResult.NotExist:
                        code.Value = "1";
                        break;
                    case FieldVerifyResult.PasswordIncorrect:
                        code.Value = "2";
                        break;
                    case FieldVerifyResult.Inactive:
                        code.Value = "3";
                        break;
                    case FieldVerifyResult.Error:
                        code.Value = "4";
                        break;
                    case FieldVerifyResult.Success:
                        code.Value = "5";
                        user.Add(new XElement("id", u.Id),
                            new XElement("displayName", u.DisplayName),
                            new XElement("LoginName", u.LoginName),
                            new XElement("mobile", u.Mobile),
                            new XElement("state", (int)u.Status),
                            new XElement("userType", (int)u.UserType),
                            new XElement("userCatalog", (int)u.UserCatalog),
                            new XElement("createdTime", u.CreatedTime.ToString("yyyyMMdd HH:mm:ss",
                                System.Globalization.DateTimeFormatInfo.InvariantInfo)));
                        var rs = u.SelectRolesById();
                        if (rs != null && rs.Count > 0)
                        {
                            foreach (var r in rs) {
                                roles.Add(new XElement("role", 
                                    new XElement("id", r.Id),
                                    new XElement("name", r.RoleName)));
                            }
                        }
                        var p = new Permission();
                        var ps = p.SelectByLoginName(userName);
                        if (ps != null && ps.Count > 0)
                        {
                            foreach (var per in ps)
                            {
                                permissions.Add(new XElement("permission",
                                    new XElement("id", per.Id),
                                    new XElement("module",
                                        new XElement("id", per.Id),
                                        new XElement("name", per.Module.ModuleName)),
                                    new XElement("task",
                                        new XElement("id", per.Task.Id),
                                        new XElement("name", per.Task.TaskName)))
                                        );
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                code.Value = "0";
                msg.Add(new XCData(ex.Message));
            }
            root.Add(code, msg, user, roles, permissions);
            return root.ToString();
        }
    }
}
