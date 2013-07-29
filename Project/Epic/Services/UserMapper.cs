using System;
using System.Linq;
using Nancy.Authentication.Forms;
using Nancy.Security;
using Nancy;
using Epic.Database;
using Epic.Models;
using Crypt = BCrypt.Net.BCrypt;

namespace Epic
{
    /// <summary>
    /// Required for forms authentication.
    /// </summary>
    public class UserMapper : IUserMapper
    {
        private IEpicDatabase db;

        public UserMapper (IEpicDatabase db)
        {
            this.db = db;
        }

        public IUserIdentity GetUserFromIdentifier (Guid identifier, NancyContext context)
        {
            User user = db.Users.SingleOrDefault (i => i.Identifier == identifier);

            return user == null ? null : new UserIdentity { UserName = user.Login };
        }
    }
}