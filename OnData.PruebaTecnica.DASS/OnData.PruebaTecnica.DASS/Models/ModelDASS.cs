namespace OnData.PruebaTecnica.DASS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDASS : DbContext
    {
        public ModelDASS()
            : base("name=ModelDASS")
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Contenido> Contenidos { get; set; }
        public virtual DbSet<Etiqueta> Etiquetas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contenido>()
                .HasMany(e => e.Categorias)
                .WithRequired(e => e.Contenido)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contenido>()
                .HasMany(e => e.Etiquetas)
                .WithRequired(e => e.Contenido)
                .WillCascadeOnDelete(false);
        }
    }
}
