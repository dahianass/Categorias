namespace OnData.PruebaTecnica.DASS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Etiqueta
    {
        [Key]
        public int IdEtiquetas { get; set; }

        public int IdContenido { get; set; }

        [Required]
        [StringLength(10)]
        public string Nombre { get; set; }

        public virtual Contenido Contenido { get; set; }
    }
}
