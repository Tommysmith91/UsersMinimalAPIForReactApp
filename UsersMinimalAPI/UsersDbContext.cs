using Microsoft.EntityFrameworkCore;
using UsersMinimalAPI.Entities;

namespace UsersMinimalAPI
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        public DbSet<User> Users { get;set; }

    }
}
