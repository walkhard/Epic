using System;
using System.Data.Entity;
using Epic.Models;
using Epic.Services;
using Crypt = BCrypt.Net.BCrypt;

namespace Epic.Database
{
    /// <summary>
    /// Epic database initializer.
    /// It will add `admin` user with `admin` password upon database creation.
    /// It will add default settings.
    /// </summary>
    public class EpicDatabaseInitializer: CreateDatabaseIfNotExists<EpicDatabase>
    {
        protected override void Seed (EpicDatabase context)
        {
            CreateAdminUser (context);
            CreateDefaultSettings (context);
        }

        private void CreateAdminUser(EpicDatabase context)
        {
            IUserService service = new UserService (context);

            User admin = new User {
                Id = 1,
                Identifier = Guid.NewGuid(),
                Login = "admin",
                Password = service.HashPassword("admin")
            };

            context.Users.Add (admin);

            context.SaveChanges ();
        }

        private void CreateDefaultSettings(EpicDatabase context)
        {
            ISettingsService service = new SettingsService (context);
            EpicSettings settings = new EpicSettings 
            {
                MetaTitle = "Epic",
                Title = "Epic"
            };

            service.SaveSettings (settings);
        }
    }
}