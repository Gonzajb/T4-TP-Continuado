namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificacionNombreTablasAvisosUsuarios : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AvisoUsuarioEmpleado", newName: "AvisoUsuariosEmpleadosAprobados");
            RenameTable(name: "dbo.AvisoUsuarioEmpleado1", newName: "AvisoUsuariosEmpleadosDesaprobados");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AvisoUsuariosEmpleadosDesaprobados", newName: "AvisoUsuarioEmpleado1");
            RenameTable(name: "dbo.AvisoUsuariosEmpleadosAprobados", newName: "AvisoUsuarioEmpleado");
        }
    }
}
