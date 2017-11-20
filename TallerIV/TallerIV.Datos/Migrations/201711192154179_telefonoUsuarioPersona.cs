namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class telefonoUsuarioPersona : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IdentityUser", "Telefono", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IdentityUser", "Telefono");
        }
    }
}
