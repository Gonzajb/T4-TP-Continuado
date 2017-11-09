namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificacionEncuentro : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Encuentros", "UsuarioReclutador_Id", "dbo.IdentityUser");
            DropIndex("dbo.Encuentros", new[] { "UsuarioEmpleado_Id1" });
            DropColumn("dbo.Encuentros", "UsuarioEmpleado_Id");
            RenameColumn(table: "dbo.Encuentros", name: "UsuarioEmpleado_Id1", newName: "UsuarioEmpleado_Id");
            AddColumn("dbo.Encuentros", "Aviso_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Encuentros", "UsuarioEmpleado_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Encuentros", "UsuarioEmpleado_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Encuentros", "UsuarioEmpleado_Id");
            CreateIndex("dbo.Encuentros", "Aviso_Id");
            AddForeignKey("dbo.Encuentros", "Aviso_Id", "dbo.Avisos", "Id");
            AddForeignKey("dbo.Encuentros", "UsuarioReclutador_Id", "dbo.IdentityUser", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Encuentros", "UsuarioReclutador_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.Encuentros", "Aviso_Id", "dbo.Avisos");
            DropIndex("dbo.Encuentros", new[] { "Aviso_Id" });
            DropIndex("dbo.Encuentros", new[] { "UsuarioEmpleado_Id" });
            AlterColumn("dbo.Encuentros", "UsuarioEmpleado_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Encuentros", "UsuarioEmpleado_Id", c => c.String());
            DropColumn("dbo.Encuentros", "Aviso_Id");
            RenameColumn(table: "dbo.Encuentros", name: "UsuarioEmpleado_Id", newName: "UsuarioEmpleado_Id1");
            AddColumn("dbo.Encuentros", "UsuarioEmpleado_Id", c => c.String());
            CreateIndex("dbo.Encuentros", "UsuarioEmpleado_Id1");
            AddForeignKey("dbo.Encuentros", "UsuarioReclutador_Id", "dbo.IdentityUser", "Id", cascadeDelete: true);
        }
    }
}
