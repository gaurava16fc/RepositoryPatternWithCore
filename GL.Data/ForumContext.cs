using GL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GL.Data
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersStatus> UsersStatus { get; set; }
    }
}
