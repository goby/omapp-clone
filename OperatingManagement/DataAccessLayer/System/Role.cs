using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatingManagement.Framework.Basic;
using OperatingManagement.Framework;
using Oracle.DataAccess.Client;
using System.Data;

namespace OperatingManagement.DataAccessLayer.System
{
    /// <summary>
    /// Represents the Role object from database.
    /// </summary>
    [Serializable]
    public class Role : BaseEntity<double, Role>
    {
        /// <summary>
        /// Create a new instance of <see cref="Role"/> class.
        /// </summary>
        public Role(){
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;
        /// <summary>
        /// Gets/Sets the role name.
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// Gets/Sets the description/note.
        /// </summary>
        public string Note { get; set; }
        #endregion

        #region -Private methods-
        private OracleParameter PrepareRefCursor()
        {
            return new OracleParameter()
            {
                ParameterName = "o_cursor",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.RefCursor
            };
        }
        #endregion

        #region -Public methods-
        public List<Role> SelectAll()
        {
            OracleParameter p = PrepareRefCursor();
            DataSet ds = _database.SpExecuteDataSet("up_role_selectall", new OracleParameter[]{
                p
            });
            List<Role> roles = new List<Role>();
            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    roles.Add(new Role()
                    {
                        Id = Convert.ToDouble(dr["RoleId"].ToString()),
                        CreatedTime = Convert.ToDateTime(dr["CTIME"].ToString()),
                        RoleName = dr["RoleName"].ToString(),
                        Note = dr["Note"].ToString()
                    });
                }
            }
            return roles;
        }
        #endregion
        
        #region -Override BaseEntity-
        protected override void ValidationRules()
        {
            this.AddValidRules("RoleName", "角色名不能为空。", string.IsNullOrEmpty(RoleName));
        }
        #endregion
    }
}
