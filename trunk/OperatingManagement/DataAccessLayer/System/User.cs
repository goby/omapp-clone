using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using OperatingManagement.Framework.Core;
using System.Data;
using Oracle.DataAccess.Client;

namespace OperatingManagement.DataAccessLayer.System
{
    /// <summary>
    /// Represents the User object from database.
    /// </summary>
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
        /// <summary>
        /// Gets/Sets the unique login name.
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// Gets/Sets the display name.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets/Sets the password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets/Sets the user type.
        /// </summary>
        public UserType UserType { get; set; }
        /// <summary>
        /// Gets/Sets the status.
        /// </summary>
        public FieldStatus Status { get; set; }
        /// <summary>
        /// Gets/Sets the cellphone number.
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Gets/Sets the description/note.
        /// </summary>
        public string Note { get; set; }
        #endregion

        #region -Private methods-
        private OracleParameter PrepareRefCursor() {
            return new OracleParameter()
            {
                ParameterName = "o_cursor",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.RefCursor
            };
        }
        #endregion

        #region -Public ethods-
        /// <summary>
        /// Selects all the users from database.
        /// </summary>
        /// <returns>List of <see cref="User"/> collection.</returns>
        public List<User> SelectAll() {
            OracleParameter p = PrepareRefCursor();
            DataSet ds = _database.SpExecuteDataSet("up_user_selectall", new OracleParameter[]{
                p
            });
            List<User> users = new List<User>();
            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    users.Add(new User()
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
                    });
                }
            }
            return users;
        }
        /// <summary>
        /// Selects the specific user by login name.
        /// </summary>
        /// <returns><see cref="User"/> object.</returns>
        public User SelectByLoginName()
        {
            OracleParameter p = PrepareRefCursor();

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
        /// <summary>
        /// Verifies the specific user by LoginName and Password.
        /// </summary>
        /// <returns><see cref="FieldVerifyResult"/> object.</returns>
        public FieldVerifyResult Verify()
        {
            try
            {
                User u = this.SelectByLoginName();
                if (u == null)
                    return FieldVerifyResult.NotExist;

                if (u.Password != GlobalSettings.EncryptPassword(this.Password))
                    return FieldVerifyResult.PasswordIncorrect;

                if (u.Status != FieldStatus.Active)
                    return FieldVerifyResult.Inactive;
                return FieldVerifyResult.Success;
            }
            catch
            {
                return FieldVerifyResult.Error;
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
