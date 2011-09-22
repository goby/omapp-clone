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
    /// Represents the Module object from database.
    /// </summary>
    [Serializable]
    public class Module : BaseEntity<double, Module>
    {
        /// <summary>
        /// Create a new instance of <see cref="Module"/> class.
        /// </summary>
        public Module(){
            _database = OracleDatabase.FromConfiguringNode("ApplicationServices");
        }

        #region -Properties-
        private OracleDatabase _database = null;
        /// <summary>
        /// Gets/Sets the module name.
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// Gets/Sets the description/note.
        /// </summary>
        public string ModuleNote { get; set; }
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
        /// <summary>
        /// Selects all Modules from database.
        /// </summary>
        /// <returns></returns>
        public List<Module> SelectAll()
        {
            OracleParameter p = PrepareRefCursor();
            DataSet ds = _database.SpExecuteDataSet("up_module_selectall", new OracleParameter[]{
                p
            });
            List<Module> modules = new List<Module>();
            if (ds != null && ds.Tables.Count == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    modules.Add(new Module()
                    {
                        Id = Convert.ToDouble(dr["ModuleId"].ToString()),
                        ModuleName= dr["ModuleName"].ToString(),
                        ModuleNote = dr["Note"].ToString()
                    });
                }
            }
            return modules;
        }
        #endregion
        
        #region -Override BaseEntity-
        protected override void ValidationRules()
        { }
        #endregion
    }
}
