using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Validation;
using System.Collections.Generic;
using Epic.Database;
using Epic.Extensions;
using Epic.Models;
using Epic.Services;
using Epic.ViewModels;

namespace Epic.Modules
{
    /// <summary>
    /// Admin module, requires `admin` authentication.
    /// </summary>
    public class AdminModule : NancyModule
    {
        public AdminModule (IEpicDatabase db, IUserService userService, ISettingsService settingsService) : base ("/admin")
        {
            this.RequiresAuthentication ();

            // Show add post form
            Get ["/posts/add"] = parameters =>
            {
                return View ["Post/Add"];
            };

            // Show edit post form
            Get ["/posts/{id}/edit"] = parameters =>
            {
                int id = parameters.id;
                Post post = db.Posts.FirstOrDefault (i => i.Id == id);

                return View ["Post/Edit", post];
            };

            // Handle add/edit post forms
            // Returns JSON formatted data: validation errors or post id
            Post ["/posts/edit"] = parameters =>
            {
                Post post = this.Bind<Post> ();
                ModelValidationResult result = this.Validate (post);
                if (!result.IsValid)
                {
                    return this.Response.AsJson (new { Errors = result.ToErrorViewModel () });
                }

                Post existingPost = db.Posts.FirstOrDefault (i => i.Id == post.Id);
                if (existingPost == null)
                {
                    post.Created = DateTime.Now;
                    db.Posts.Add (post);
                }
                else
                {
                    existingPost.Title = post.Title;
                    existingPost.Slug = post.Slug;
                    existingPost.Content = post.Content;
                }

                db.SaveChanges ();

                return this.Response.AsJson (new { Id = post.Id });
            };

            // Delete post
            Post ["/posts/delete"] = parameters =>
            {
                int id = Request.Form.id;
                Post post = db.Posts.FirstOrDefault (i => i.Id == id);
                if(post == null)
                {
                    this.Alert("Post doesn't exist.");
                }
                else
                {
                    db.Posts.Remove (post);
                    db.SaveChanges ();
                    this.Alert("Post was deleted.");
                }

                return this.Response.AsRedirect ("/");
            };

            // Change password form
            Get ["/password"] = parameters =>
            {
                return View ["Password"];
            };

            // Handle change password
            Post ["/password"] = parameters =>
            {
                var viewModel = this.Bind<ChangePasswordViewModel> ();
                viewModel.Username = Context.CurrentUser.UserName;

                ModelValidationResult result = this.Validate (viewModel);
                if (!result.IsValid)
                {
                    Context.Items ["errors"] = result.ToErrorViewModel ();
                    return View ["Password", viewModel];
                }

                userService.SetPassword (Context.CurrentUser.UserName, viewModel.NewPassword);
                this.Alert("Password was changed.");

                return this.Response.AsRedirect ("/admin/password");
            };

            // Settings form
            Get ["/settings"] = parameters =>
            {
                EpicSettings settings = settingsService.Get();
                var viewModel = new SettingsViewModel(settings);

                return View ["Settings", viewModel];
            };

            // Handle change settings
            Post ["/settings"] = parameters =>
            {
                var viewModel = this.Bind<SettingsViewModel> ();
                EpicSettings settings = new EpicSettings
                {
                    Title = viewModel.Title,
                    MetaTitle = viewModel.MetaTitle
                };

                settingsService.SaveSettings (settings);
                this.Alert("Settings were saved.");

                return this.Response.AsRedirect ("/admin/settings");
            };
        }
    }
}