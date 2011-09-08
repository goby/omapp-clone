using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatingManagement.Framework
{
    /// <summary>
    /// Enumerates result of user verify.
    /// </summary>
    public enum UserVerifyResult : int
    {
        /// <summary>
        /// The specific user doesnt exist.
        /// </summary>
        NotExist = 0,
        /// <summary>
        /// The password of specify named user doesnt correctly;
        /// </summary>
        PasswordIncorrect = 1,
        /// <summary>
        /// The specific user was inactive(Locked or Deleted).
        /// </summary>
        Inactive = 2,
        /// <summary>
        /// The LoginName/DisplayName was exist in database.
        /// </summary>
        NameDuplicated = 3,
        /// <summary>
        /// Oracle Exception was handled.
        /// </summary>
        Error = 4,
        /// <summary>
        /// Succecced.
        /// </summary>
        Success = 5
    }
}
