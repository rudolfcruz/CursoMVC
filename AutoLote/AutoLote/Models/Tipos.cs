using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoLote.Models
{
    public class Tipos
    {
        [Key]
        public int TipoID { get; set; }
        [Display(Name="Tipo")]
        [Required(ErrorMessage="Ingrese la Descripcion")]
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
    }
}