using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoLote.Models
{
    public class DBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public DBContext() : base("name=DBContext")
        {
        }

        public System.Data.Entity.DbSet<AutoLote.Models.Tipos> Tipos { get; set; }

        public System.Data.Entity.DbSet<AutoLote.Models.Marcas> Marcas { get; set; }

        public System.Data.Entity.DbSet<AutoLote.Models.Modelos> Modelos { get; set; }

        public System.Data.Entity.DbSet<AutoLote.Models.Automovil> Automovils { get; set; }
    
    }
}
