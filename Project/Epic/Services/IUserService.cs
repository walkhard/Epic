using System;
using Epic.Models;

namespace Epic.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Checks user password.
        /// </summary>
        /// <returns><c>true</c>, if password was checked, <c>false</c> otherwise.</returns>
        /// <param name="user">User.</param>
        /// <param name="password">Password.</param>
        bool CheckPassword (User user, string password);

        /// <summary>
        /// Get user by username;
        /// </summary>
        /// <param name="username">Username.</param>
        User Get (string username);

        /// <summary>
        /// Hash password.
        /// </summary>
        /// <returns>Hashed password.</returns>
        /// <param name="password">Password.</param>
        string HashPassword (string password);

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <returns><c>Guid</c> if validated, otherwise <c>null</c>.</returns>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        Guid? ValidateUser (string username, string password);

        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        void SetPassword (string username, string password);
    }
}