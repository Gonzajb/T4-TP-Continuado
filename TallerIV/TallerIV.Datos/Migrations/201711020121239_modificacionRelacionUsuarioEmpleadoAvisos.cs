namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificacionRelacionUsuarioEmpleadoAvisos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Avisos", "UsuarioEmpleado_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Avisos", "UsuarioEmpleado_Id1", "dbo.Usuarios");
            DropIndex("dbo.Avisos", new[] { "UsuarioEmpleado_Id" });
            DropIndex("dbo.Avisos", new[] { "UsuarioEmpleado_Id1" });
            CreateTable(
                "dbo.UsuarioEmpleadoAviso",
                c => new
                    {
                        UsuarioEmpleado_Id = c.String(nullable: false, maxLength: 128),
                        Aviso_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioEmpleado_Id, t.Aviso_Id })
                .ForeignKey("dbo.Usuarios", t => t.UsuarioEmpleado_Id, cascadeDelete: true)
                .ForeignKey("dbo.Avisos", t => t.Aviso_Id, cascadeDelete: true)
                .Index(t => t.UsuarioEmpleado_Id)
                .Index(t => t.Aviso_Id);
            
            CreateTable(
                "dbo.UsuarioEmpleadoAviso1",
                c => new
                    {
                        UsuarioEmpleado_Id = c.String(nullable: false, maxLength: 128),
                        Aviso_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioEmpleado_Id, t.Aviso_Id })
                .ForeignKey("dbo.Usuarios", t => t.UsuarioEmpleado_Id, cascadeDelete: true)
                .ForeignKey("dbo.Avisos", t => t.Aviso_Id, cascadeDelete: true)
                .Index(t => t.UsuarioEmpleado_Id)
                .Index(t => t.Aviso_Id);
            
            DropColumn("dbo.Avisos", "UsuarioEmpleado_Id");
            DropColumn("dbo.Avisos", "UsuarioEmpleado_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Avisos", "UsuarioEmpleado_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Avisos", "UsuarioEmpleado_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.UsuarioEmpleadoAviso1", "Aviso_Id", "dbo.Avisos");
            DropForeignKey("dbo.UsuarioEmpleadoAviso1", "UsuarioEmpleado_Id", "dbo.Usuarios");
            DropForeignKey("dbo.UsuarioEmpleadoAviso", "Aviso_Id", "dbo.Avisos");
            DropForeignKey("dbo.UsuarioEmpleadoAviso", "UsuarioEmpleado_Id", "dbo.Usuarios");
            DropIndex("dbo.UsuarioEmpleadoAviso1", new[] { "Aviso_Id" });
            DropIndex("dbo.UsuarioEmpleadoAviso1", new[] { "UsuarioEmpleado_Id" });
            DropIndex("dbo.UsuarioEmpleadoAviso", new[] { "Aviso_Id" });
            DropIndex("dbo.UsuarioEmpleadoAviso", new[] { "UsuarioEmpleado_Id" });
            DropTable("dbo.UsuarioEmpleadoAviso1");
            DropTable("dbo.UsuarioEmpleadoAviso");
            CreateIndex("dbo.Avisos", "UsuarioEmpleado_Id1");
            CreateIndex("dbo.Avisos", "UsuarioEmpleado_Id");
            AddForeignKey("dbo.Avisos", "UsuarioEmpleado_Id1", "dbo.Usuarios", "Id");
            AddForeignKey("dbo.Avisos", "UsuarioEmpleado_Id", "dbo.Usuarios", "Id");
        }
    }
}
