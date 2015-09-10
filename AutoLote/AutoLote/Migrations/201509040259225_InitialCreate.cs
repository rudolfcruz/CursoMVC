namespace AutoLote.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tipos",
                c => new
                    {
                        TipoID = c.Int(nullable: false, identity: true),
                        Descripcion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TipoID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tipos");
        }
    }
}
