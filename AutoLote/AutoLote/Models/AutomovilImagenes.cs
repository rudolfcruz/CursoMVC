using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoLote.Models
{
    public class AutomovilImagenes
    {
        [Key]
        public int AutoimagenesID { get; set; }
        public int AutomovilID { get; set; }
        public Automovil Automovil { get; set; }
        public string UrlImagenMiniatura{ get; set; }
        public string UrlImagenMediana { get; set; }
        [NotMapped]
        public HttpPostedFileWrapper ImagenSubida { get; set; }
        [NotMapped]
        public bool ImagenEliminada { get; set; }
    }
}