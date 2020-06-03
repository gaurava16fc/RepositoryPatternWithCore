using GL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GL.Data
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options) : base(options)
        {
        }

        //public MasterContext()
        //{
        //}

        public DbSet<Account> Accounts { get; set; }
    }
}
