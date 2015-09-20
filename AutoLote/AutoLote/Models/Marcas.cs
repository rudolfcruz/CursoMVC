using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoLote.Models
{
    public class Marcas
    {
        [Key]
        public int MarcaID { get; set; }
        [Display(Name = "Marca")]
        [Required(ErrorMessage="Ingrese la descripcion de la marca")]
        public string  Descripcion{ get; set; }
        public string UrlImagen{ get; set; }
        public virtual ICollection<Modelos> ListaModelos { get; set; }
        [NotMapped]
        public HttpPostedFileWrapper ImagenSubida { get; set; }
    }
}