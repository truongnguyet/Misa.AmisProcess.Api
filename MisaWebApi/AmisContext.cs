using Microsoft.EntityFrameworkCore;
using MisaWebApi.Models;

namespace MisaWebApi
{
    public class AmisContext : DbContext
    {
        public AmisContext()
        {
        }

        public AmisContext(DbContextOptions<AmisContext> options)
    : base(options)
        { }

        public virtual DbSet<Users> Users { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.userId).HasColumnType("int(11)");
            });

           
        }
    }
}
