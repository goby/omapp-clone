using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace OperatingManagement.Security
{
    /// <summary>
    /// Provides principal management.
    /// </summary>
    public class AspNetPrincipal:IPrincipal
    {
        /// <summary>
        /// Create a new instance of <see cref="OperatingManagement.Security.AspNetPrincipal"/> class.
        /// </summary>
        /// <param name="identity">AspNetIdentity object</param>
        public AspNetPrincipal(AspNetIdentity identity)
        {
            this.Identity = identity;
        }

        #region -IPrincipal Members-
        /// <summary>
        /// Gets/sets the basic info of specified Identity.
        /// </summary>
        public IIdentity Identity { get; set; }

        /// <summary>
        /// Gets a value indicating whether the user is in a specified role.
        /// </summary>
        /// <param name="role">The name of specified role.</param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
