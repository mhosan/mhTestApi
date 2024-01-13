using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Archivo de contexto (representa una sesion en la bd)
// permite manejar cadenas de conexión
// permite crear modelos y relaciones entre ellos
// permite hacer seguim. de los cambios de los datos
// permite hacer operaciones en la bd, incluso cachear
// 

namespace mhTestApi.Models
{
    /// <summary>
    /// Clase de contexto, hereda de DBContext
    /// </summary>
    public partial class DBmhtestContext : DbContext
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public DBmhtestContext()
        {
        }

        public DBmhtestContext(DbContextOptions<DBmhtestContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Referencia al modelo de la tabla Tarea
        /// </summary>
        public DbSet<Tarea> Tarea { get; set; }
        /// <summary>
        /// Referencia a la tabla ClientMachine con los datos del equipo del cliente
        /// </summary>
        public DbSet<ClientMachine> ClientMachine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {     
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => e.IdTarea)
                    .HasName("PK__Tarea__756A54024F95DB6D");

                entity.Property(e => e.nombre).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
