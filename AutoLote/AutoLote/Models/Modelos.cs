using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoLote.Models
{
    public class Modelos
    {
        [Key]
        public int ModeloID { get; set; }
        [Required(ErrorMessage = "Seleccione la marca del modelo")]
        public int MarcaId { get; set;}
        [Display(Name = "Modelo")]
        [Required(ErrorMessage="Ingre la descripcion del Modelo")]
        public string Descripcion { get; set; }
        public Marcas Marcas { get; set; }
    }
}