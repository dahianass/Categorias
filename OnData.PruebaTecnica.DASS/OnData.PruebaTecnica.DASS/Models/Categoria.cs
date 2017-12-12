namespace OnData.PruebaTecnica.DASS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Categoria")]
    public partial class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        public int IdContenido { get; set; }

        [Required]
        [StringLength(10)]
        public string Nombre { get; set; }

        public virtual Contenido Contenido { get; set; }
    }
}
