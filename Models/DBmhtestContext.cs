using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace mhTestApi.Models
{
    public partial class DBmhtestContext : DbContext
    {
        public DBmhtestContext()
        {
        }

        public DBmhtestContext(DbContextOptions<DBmhtestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tarea> Tarea { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => e.idTarea)
                    .HasName("PK__Tarea__756A54024F95DB6D");

                entity.Property(e => e.nombre).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
