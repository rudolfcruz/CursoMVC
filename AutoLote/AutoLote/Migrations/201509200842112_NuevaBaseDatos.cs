namespace AutoLote.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NuevaBaseDatos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Automovils",
                c => new
                    {
                        AutomovilID = c.Int(nullable: false, identity: true),
                        ModeloID = c.Int(nullable: false),
                        TipoID = c.Int(nullable: false),
                        TieneAireAcondicionado = c.Boolean(nullable: false),
                        Comentarios = c.String(),
                        Anio = c.Int(nullable: false),
                        Color = c.String(),
                        FechaPublicacion = c.DateTime(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.AutomovilID)
                .ForeignKey("dbo.Modelos", t => t.ModeloID, cascadeDelete: true)
                .ForeignKey("dbo.Tipos", t => t.TipoID, cascadeDelete: true)
                .Index(t => t.ModeloID)
                .Index(t => t.TipoID);
            
            CreateTable(
                "dbo.AutomovilImagenes",
                c => new
                    {
                        AutoimagenesID = c.Int(nullable: false, identity: true),
                        AutomovilID = c.Int(nullable: false),
                        UrlImagenMiniatura = c.String(),
                        UrlImagenMediana = c.String(),
                    })
                .PrimaryKey(t => t.AutoimagenesID)
                .ForeignKey("dbo.Automovils", t => t.AutomovilID, cascadeDelete: true)
                .Index(t => t.AutomovilID);
            
            CreateTable(
                "dbo.Modelos",
                c => new
                    {
                        ModeloID = c.Int(nullable: false, identity: true),
                        MarcaId = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ModeloID)
                .ForeignKey("dbo.Marcas", t => t.MarcaId, cascadeDelete: true)
                .Index(t => t.MarcaId);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        MarcaID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                        UrlImagen = c.String(),
                    })
                .PrimaryKey(t => t.MarcaID);
            
            CreateTable(
                "dbo.Tipos",
                c => new
                    {
                        TipoID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                        Comentario = c.String(),
                    })
                .PrimaryKey(t => t.TipoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Automovils", "TipoID", "dbo.Tipos");
            DropForeignKey("dbo.Automovils", "ModeloID", "dbo.Modelos");
            DropForeignKey("dbo.Modelos", "MarcaId", "dbo.Marcas");
            DropForeignKey("dbo.AutomovilImagenes", "AutomovilID", "dbo.Automovils");
            DropIndex("dbo.Modelos", new[] { "MarcaId" });
            DropIndex("dbo.AutomovilImagenes", new[] { "AutomovilID" });
            DropIndex("dbo.Automovils", new[] { "TipoID" });
            DropIndex("dbo.Automovils", new[] { "ModeloID" });
            DropTable("dbo.Tipos");
            DropTable("dbo.Marcas");
            DropTable("dbo.Modelos");
            DropTable("dbo.AutomovilImagenes");
            DropTable("dbo.Automovils");
        }
    }
}
