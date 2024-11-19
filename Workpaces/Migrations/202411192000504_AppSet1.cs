namespace Workpaces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppSet1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reservas", name: "idUsuario_Id", newName: "Usuario_Id");
            RenameIndex(table: "dbo.Reservas", name: "IX_idUsuario_Id", newName: "IX_Usuario_Id");
            AddColumn("dbo.Reservas", "idUsuario", c => c.String());
            DropColumn("dbo.Reservas", "ApplicationUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservas", "ApplicationUser", c => c.Int(nullable: false));
            DropColumn("dbo.Reservas", "idUsuario");
            RenameIndex(table: "dbo.Reservas", name: "IX_Usuario_Id", newName: "IX_idUsuario_Id");
            RenameColumn(table: "dbo.Reservas", name: "Usuario_Id", newName: "idUsuario_Id");
        }
    }
}
