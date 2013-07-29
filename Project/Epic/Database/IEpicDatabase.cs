using System.Data.Entity;
using Epic.Models;

namespace Epic.Database
{
    public interface IEpicDatabase
    {
        DbSet<Post> Posts { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<Setting> Settings { get; set; }

        int SaveChanges ();
    }
}