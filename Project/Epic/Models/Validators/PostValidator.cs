using System;
using System.Linq;
using FluentValidation;
using Nancy.TinyIoc;
using FluentValidation.Results;
using Epic.Database;
using Epic.Models;

namespace Epic.Models.Validators
{
    public class PostValidator : AbstractValidator<Post>
    {
        // We need access to database, to check uniqueness of `slug` property.
        public IEpicDatabase Db { get; set; }

        public PostValidator ()
        {
            // I don't like it, but constructor injection is not working
            // must investigate it further, though this forks for now
            TinyIoCContainer.Current.AutoRegister ();
            TinyIoCContainer.Current.BuildUp (this);

            RuleFor (post => post.Title).
                NotEmpty ();

            RuleFor (post => post.Slug).
                NotEmpty ();

            RuleFor (post => post.Slug).
                Matches ("^[a-zA-Z0-9_-]+$").
                    WithMessage ("Is not a valid slug.");

            // check if slug is unique
            Custom (post =>
                    {
                if (string.IsNullOrWhiteSpace (post.Slug))
                {
                    // we don't want it return error if it's empty or null
                    return null;
                }

                var posts = Db.Posts.Where (i => i.Id != post.Id && i.Slug.Equals (post.Slug, StringComparison.InvariantCultureIgnoreCase));

                if (!posts.Any ())
                {
                    return null;
                }

                return new ValidationFailure ("Slug", "'Slug' must be unique.");
            });
        }
    }
}