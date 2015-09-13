using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoLote.Models
{
    public class Automovil
    {
        [Key]
        public int AutomovilID { get; set; }
        public int ModelosID { get; set; }
        public Modelos Modelo { get; set; }
        [Required]
        public int TiposID { get; set; }
        public Tipos Tipo { get; set; }
        [Display(Name="Tiene A/C")]
        public bool TieneAireAcondicionado { get; set; }
        public string Comentarios { get; set; }
        [Display(Name="Año")]
        public int Anio { get; set; }
        public string Color { get; set; }
        [Display(Name="Año Publicación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}",ApplyFormatInEditMode = true)]
        public DateTime FechaPublicacion { get; set; }
        public string Email { get; set; }
        public List<AutomovilImagenes> AutomovilImagenes { get; set; }
    }
}