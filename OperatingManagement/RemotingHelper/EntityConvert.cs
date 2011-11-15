using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace OperatingManagement.RemotingHelper
{
    /// <summary>
    /// Implements method to convert xml string to Entity object.
    /// </summary>
    public class EntityConvert
    {
        /// <summary>
        /// Converts xml string to AuthStatus object.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static AuthStatus ToAuthStatus(string xml)
        {
            XElement root = XElement.Parse(xml);
            var user = root.Element("user");
            AuthStatus status = new AuthStatus()
            {
                Code = Convert.ToInt32(root.Element("code").Value),
                Message = root.Element("msg").Value,
                User = new User() {
                    CreatedTime = Convert.ToDateTime(user.Element("createdTime").Value),
                    DisplayName=user.Element("displayName").Value,
                    State = Convert.ToInt32(user.Element("state").Value),
                    UserCatalog = Convert.ToInt32(user.Element("userCatalog").Value),
                    UserType = Convert.ToInt32(user.Element("userType").Value),
                    Id = Convert.ToDouble(user.Element("id").Value),
                    Mobile = user.Element("mobile").Value,
                    LoginName = user.Element("loginName").Value
                }
            };
            var permissions = root.Element("permissions").Elements("permission");
            if (permissions != null && permissions.Count() > 0)
            {
                status.Permissions = (from q in permissions
                                      select new Permission()
                                      {
                                          Id = Convert.ToDouble(q.Element("id").Value),
                                          Module = new Module()
                                          {
                                              Id = Convert.ToDouble(q.Element("module").Element("id").Value),
                                              Name = q.Element("module").Element("name").Value
                                          },
                                          Task = new Task()
                                          {
                                              Id = Convert.ToDouble(q.Element("task").Element("id").Value),
                                              Name = q.Element("task").Element("name").Value
                                          }
                                      }).ToList();
            }

            var roles = root.Element("roles").Elements("role");
            if (roles != null && roles.Count() > 0)
            {
                status.Roles = (from q in roles
                                select new Role()
                                {
                                    Id = Convert.ToDouble(q.Element("id").Value),
                                    Name = q.Element("name").Value
                                }).ToList();
            }
            return status;
        }
    }
}
