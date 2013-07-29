using Nancy.Security;
using System.Collections.Generic;

namespace Epic
{
    /// <summary>
    /// Required for forms authentication
    /// </summary>
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}