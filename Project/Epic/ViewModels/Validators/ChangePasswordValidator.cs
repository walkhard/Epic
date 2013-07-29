using System;
using FluentValidation;
using Nancy.TinyIoc;
using FluentValidation.Results;
using Nancy.Security;
using Nancy;
using Epic.Models;
using Epic.Services;

namespace Epic.ViewModels.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordViewModel>
    {
        // We need access to database, to check current password
        public IUserService Service { get; set; }

        public ChangePasswordValidator ()
        {
            // I don't like it, but constructor injection is not working
            // must investigate it further, though this forks for now
            TinyIoCContainer.Current.AutoRegister ();
            TinyIoCContainer.Current.BuildUp (this);

            RuleFor (viewModel => viewModel.NewPassword).
                NotEmpty ();

            // check if current password matches entered password
            Custom (viewModel =>
                    {
                User user = Service.Get (viewModel.Username);
                if (Service.CheckPassword (user, viewModel.CurrentPassword))
                {
                    return null;
                }

                return new ValidationFailure ("CurrentPassword", "'CurrentPassword' is invalid.");
            });

            // check if repeated password is same as new password
            Custom (viewModel =>
                    {
                if (!string.IsNullOrEmpty (viewModel.NewPassword) && viewModel.NewPassword.Equals (viewModel.RepeatPassword))
                {
                    return null;
                }

                return new ValidationFailure ("RepeatPassword", "Entered passwords do not match.");
            });
        }
    }
}

