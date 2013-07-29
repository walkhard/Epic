using System;
using System.Linq;
using Epic.Database;
using Epic.Models;
using Crypt = BCrypt.Net.BCrypt;

namespace Epic.Services
{
    public class UserService : IUserService
    {
        private IEpicDatabase db;

        public UserService (IEpicDatabase db)
        {
            this.db = db;
        }

        public Guid? ValidateUser (string username, string password)
        {
            User user = db.Users.SingleOrDefault (u => u.Login == username);
            bool userExists = user != null;
            bool userIsValidated = userExists && CheckPassword (user, password);
            if (!userIsValidated)
            {
                return null;
            }

            return user.Identifier;
        }

        public bool CheckPassword (User user, string password)
        {
            return user == null ? false : Crypt.Verify (password, user.Password);
        }

        public string HashPassword (string password)
        {
            return Crypt.HashPassword (password, Crypt.GenerateSalt ());
        }

        public User Get (string username)
        {
            return db.Users.SingleOrDefault (u => u.Login.Equals (username, StringComparison.OrdinalIgnoreCase));
        }

        public void SetPassword (string username, string password)
        {
            User user = Get (username);
            user.Password = HashPassword (password);

            db.SaveChanges ();
        }
    }
}

