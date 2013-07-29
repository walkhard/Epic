using System.Data.Entity;
using Epic.Models;

namespace Epic.Database
{
    public class EpicDatabase: DbContext, IEpicDatabase
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Setting> Settings { get; set; }
    }
}