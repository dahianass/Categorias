namespace OnData.PruebaTecnica.DASS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contenidos")]
    public partial class Contenido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contenido()
        {
            Categorias = new HashSet<Categoria>();
            Etiquetas = new HashSet<Etiqueta>();
        }

        [Key]
        public int IdContenido { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(300)]
        public string Descripcion { get; set; }

        [StringLength(20)]
        public string TipoContenido { get; set; }

        [Column("Contenido")]
        [StringLength(1000)]
        public string Contenido1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categoria> Categorias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etiqueta> Etiquetas { get; set; }
    }
}
