namespace Workpaces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estadisticas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "Estado", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservas", "Estado");
        }
    }
}
