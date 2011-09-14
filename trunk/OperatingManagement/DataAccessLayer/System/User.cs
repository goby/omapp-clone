using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;
using System.Data;
using Oracle.DataAccess.Client;

namespace OperatingManagement.DataAccessLayer
{
    [Serializable]
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
            OracleParameter p = new OracleParameter(){
                ParameterName = "o_cursor",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.RefCursor
            };

            DataSet ds = _database.SpExecuteDataSet("up_user_selectbyloginname", new OracleParameter[]{
                new OracleParameter("p_LoginName", this.LoginName), 
                p
            });

            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    return new User()
                    {
                        Id = Convert.ToDouble(dr["UserID"].ToString()),
                        CreatedTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                        DisplayName = dr["DisplayName"].ToString(),
                        LoginName = dr["LoginName"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        Note = dr["Note"].ToString(),
                        Password = dr["Password1"].ToString(),
                        UpdatedTime = Convert.ToDateTime(dr["LastUpdatedTime"].ToString()),
                        Status = (FieldStatus)(Convert.ToInt32(dr["Status"].ToString())),
                        UserType = (UserType)(Convert.ToInt32(dr["UserType"].ToString()))
                    };
                }
            }
            return null;
        }
        public UserVerifyResult Verify()
        {
            try
            {
                User u = this.SelectByLoginName();
                if (u == null)
                    return UserVerifyResult.NotExist;

                if (u.Password != GlobalSettings.EncryptPassword(this.Password))
                    return UserVerifyResult.PasswordIncorrect;

                if (u.Status != FieldStatus.Active)
                    return UserVerifyResult.Inactive;
                return UserVerifyResult.Success;
            }
            catch
            {
                return UserVerifyResult.Error;
            }
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
