using Microsoft.EntityFrameworkCore;
using MisaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisaWebApi
{
    public class AmisContext : DbContext
    {
        public AmisContext(DbContextOptions<AmisContext> options)
    : base(options)
        { }

        public virtual DbSet<Users> Blogs { get; set; }
      

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
