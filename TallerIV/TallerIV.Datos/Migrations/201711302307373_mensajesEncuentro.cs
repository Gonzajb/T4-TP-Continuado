namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mensajesEncuentro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mensaje",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Texto = c.String(),
                        Usuario_Id = c.String(nullable: false, maxLength: 128),
                        Encuentro_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUser", t => t.Usuario_Id)
                .ForeignKey("dbo.Encuentros", t => t.Encuentro_Id, cascadeDelete: true)
                .Index(t => t.Usuario_Id)
                .Index(t => t.Encuentro_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mensaje", "Encuentro_Id", "dbo.Encuentros");
            DropForeignKey("dbo.Mensaje", "Usuario_Id", "dbo.IdentityUser");
            DropIndex("dbo.Mensaje", new[] { "Encuentro_Id" });
            DropIndex("dbo.Mensaje", new[] { "Usuario_Id" });
            DropTable("dbo.Mensaje");
        }
    }
}
