using System;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using Epic.Database;
using Epic.Models;
using Epic.Services;

namespace Epic.Modules
{
    /// <summary>
    /// Login module handles authentication.
    /// </summary>
    public class LoginModule : NancyModule
    {
        private int rememberMeDays = 365;

        public LoginModule (IEpicDatabase db, IUserService userService)
        {
            // Show log in form
            Get ["/login"] = parameters =>
            {
                return View ["login"];
            };

            // Handle log in form; Try to authenticate and log in user
            Post ["/login"] = parameters => 
            {
                string user = Request.Form ["Username"];
                string pass = Request.Form ["Password"];

                Guid? userGuid = userService.ValidateUser (user, pass);
                if (!userGuid.HasValue)
                {
                    this.Alert("Invalid username or password");
                    return this.Context.GetRedirect ("~/login");
                }

                DateTime? expiry = null;
                if (Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays (rememberMeDays);
                }

                return this.LoginAndRedirect (userGuid.Value, expiry);
            };

            // Log out user
            Get ["/logout"] = parameters => 
            {
                return this.LogoutAndRedirect ("~/");
            };
        }
    }
}