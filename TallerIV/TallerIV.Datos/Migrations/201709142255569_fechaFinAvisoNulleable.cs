namespace TallerIV.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fechaFinAvisoNulleable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Avisos", "FechaFin", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Avisos", "FechaFin", c => c.DateTime(nullable: false));
        }
    }
}
