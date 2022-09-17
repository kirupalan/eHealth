using eHealthAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace eHealthAPI.Data
{
    public class eHealthDBContext : DbContext
    {

        public eHealthDBContext(DbContextOptions<eHealthDBContext> options) : base(options)
        {

        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
