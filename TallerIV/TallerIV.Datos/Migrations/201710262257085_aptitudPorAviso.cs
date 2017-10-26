namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aptitudPorAviso : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Tag", newName: "Aptitudes");
            RenameTable(name: "dbo.UsuarioEmpleadoTag", newName: "UsuarioEmpleadoAptitud");
            DropForeignKey("dbo.AvisoTag", "Aviso_Id", "dbo.Avisos");
            DropForeignKey("dbo.AvisoTag", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.UsuarioEmpleadoTag", "Tag_Id", "dbo.Tag");
            DropIndex("dbo.AvisoTag", new[] { "Aviso_Id" });
            DropIndex("dbo.AvisoTag", new[] { "Tag_Id" });
            DropIndex("dbo.UsuarioEmpleadoAptitud", new[] { "Tag_Id" });
            RenameColumn(table: "dbo.UsuarioEmpleadoAptitud", name: "Tag_Id", newName: "Aptitud_Id");
            DropPrimaryKey("dbo.Avisos");
            DropPrimaryKey("dbo.Aptitudes");
            DropPrimaryKey("dbo.UsuarioEmpleadoAptitud");
            CreateTable(
                "dbo.AptitudesPorAviso",
                c => new
                    {
                        Aptitud_Id = c.Long(nullable: false),
                        Aviso_Id = c.Long(nullable: false),
                        Prioridad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Aptitud_Id, t.Aviso_Id })
                .ForeignKey("dbo.Aptitudes", t => t.Aptitud_Id, cascadeDelete: true)
                .ForeignKey("dbo.Avisos", t => t.Aviso_Id, cascadeDelete: true)
                .Index(t => t.Aptitud_Id)
                .Index(t => t.Aviso_Id);
            
            AlterColumn("dbo.Avisos", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Aptitudes", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.UsuarioEmpleadoAptitud", "Aptitud_Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Avisos", "Id");
            AddPrimaryKey("dbo.Aptitudes", "Id");
            AddPrimaryKey("dbo.UsuarioEmpleadoAptitud", new[] { "UsuarioEmpleado_Id", "Aptitud_Id" });
            CreateIndex("dbo.UsuarioEmpleadoAptitud", "Aptitud_Id");
            AddForeignKey("dbo.UsuarioEmpleadoAptitud", "Aptitud_Id", "dbo.Aptitudes", "Id", cascadeDelete: true);
            DropTable("dbo.AvisoTag");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AvisoTag",
                c => new
                    {
                        Aviso_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Aviso_Id, t.Tag_Id });
            
            DropForeignKey("dbo.UsuarioEmpleadoAptitud", "Aptitud_Id", "dbo.Aptitudes");
            DropForeignKey("dbo.AptitudesPorAviso", "Aviso_Id", "dbo.Avisos");
            DropForeignKey("dbo.AptitudesPorAviso", "Aptitud_Id", "dbo.Aptitudes");
            DropIndex("dbo.UsuarioEmpleadoAptitud", new[] { "Aptitud_Id" });
            DropIndex("dbo.AptitudesPorAviso", new[] { "Aviso_Id" });
            DropIndex("dbo.AptitudesPorAviso", new[] { "Aptitud_Id" });
            DropPrimaryKey("dbo.UsuarioEmpleadoAptitud");
            DropPrimaryKey("dbo.Aptitudes");
            DropPrimaryKey("dbo.Avisos");
            AlterColumn("dbo.UsuarioEmpleadoAptitud", "Aptitud_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Aptitudes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Avisos", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.AptitudesPorAviso");
            AddPrimaryKey("dbo.UsuarioEmpleadoAptitud", new[] { "UsuarioEmpleado_Id", "Tag_Id" });
            AddPrimaryKey("dbo.Aptitudes", "Id");
            AddPrimaryKey("dbo.Avisos", "Id");
            RenameColumn(table: "dbo.UsuarioEmpleadoAptitud", name: "Aptitud_Id", newName: "Tag_Id");
            CreateIndex("dbo.UsuarioEmpleadoAptitud", "Tag_Id");
            CreateIndex("dbo.AvisoTag", "Tag_Id");
            CreateIndex("dbo.AvisoTag", "Aviso_Id");
            AddForeignKey("dbo.UsuarioEmpleadoTag", "Tag_Id", "dbo.Tag", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AvisoTag", "Tag_Id", "dbo.Tag", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AvisoTag", "Aviso_Id", "dbo.Avisos", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.UsuarioEmpleadoAptitud", newName: "UsuarioEmpleadoTag");
            RenameTable(name: "dbo.Aptitudes", newName: "Tag");
        }
    }
}
