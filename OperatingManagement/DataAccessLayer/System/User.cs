using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using System.Data.OracleClient;
using OperatingManagement.Framework.Core;
using System.Data;

namespace OperatingManagement.DataAccessLayer
{
    public class User : BaseEntity<double,User>
    {
        /// <summary>
        /// Create a new instance of <see cref="User"/> class.
        /// </summary>
        public User() {
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;

        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public FieldStatus Status { get; set; }
        public string Mobile { get; set; }
        public string Note { get; set; }
        #endregion

        #region -Methods-
        public User SelectByLoginName()
        {
            using (IDataReader reader = _database.SpExecuteReader("up_user_selectbyloginname", new OracleParameter("LoginName", this.LoginName)))
            {
                while (reader.Read())
                {
                    return new User()
                    {
                        Id = Convert.ToDouble(reader["UserID"].ToString()),
                        CreatedTime = Convert.ToDateTime(reader["CTIME"].ToString()),
                        DisplayName = reader["DisplayName"].ToString(),
                        LoginName = reader["LoginName"].ToString(),
                        Mobile = reader["Mobile"].ToString(),
                        Note = reader["Note"].ToString(),
                        Password = reader["Password1"].ToString(),
                        UpdatedTime = Convert.ToDateTime(reader["LastUpdatedTime"].ToString()),
                        Status = (FieldStatus)(Convert.ToInt32(reader["Status"].ToString())),
                        UserType = (UserType)(Convert.ToInt32(reader["UserType"].ToString()))
                    };
                }
                return null;
            }
        }
        public UserVerifyResult Verify()
        {
            User u = this.SelectByLoginName();
            if (u == null)
                return UserVerifyResult.NotExist;

            if (u.Password != GlobalSettings.EncryptPassword(this.Password))
                return UserVerifyResult.PasswordIncorrect;

            if (u.Status != FieldStatus.Active)
                return UserVerifyResult.Inactive;

            return UserVerifyResult.Error;
        }
        #endregion

        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
            this.AddValidRules("LoginName", "登录名不能为空。", string.IsNullOrEmpty(LoginName));
            this.AddValidRules("DisplayName", "显示名不能为空。", string.IsNullOrEmpty(LoginName));
            this.AddValidRules("Password", "密码不能为空。", string.IsNullOrEmpty(LoginName));
        }
        #endregion
    }
}
