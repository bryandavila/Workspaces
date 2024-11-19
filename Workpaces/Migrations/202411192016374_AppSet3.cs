namespace Workpaces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppSet3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservas", "Sala_IdSala", "dbo.Salas");
            DropIndex("dbo.Reservas", new[] { "Sala_IdSala" });
            DropColumn("dbo.Reservas", "SalaId");
            RenameColumn(table: "dbo.Reservas", name: "Sala_IdSala", newName: "SalaId");
            RenameColumn(table: "dbo.Reservas", name: "Usuario_Id", newName: "UsuarioId");
            RenameIndex(table: "dbo.Reservas", name: "IX_Usuario_Id", newName: "IX_UsuarioId");
            AlterColumn("dbo.Reservas", "SalaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservas", "SalaId");
            AddForeignKey("dbo.Reservas", "SalaId", "dbo.Salas", "IdSala", cascadeDelete: true);
            DropColumn("dbo.Reservas", "idUsuario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservas", "idUsuario", c => c.String());
            DropForeignKey("dbo.Reservas", "SalaId", "dbo.Salas");
            DropIndex("dbo.Reservas", new[] { "SalaId" });
            AlterColumn("dbo.Reservas", "SalaId", c => c.Int());
            RenameIndex(table: "dbo.Reservas", name: "IX_UsuarioId", newName: "IX_Usuario_Id");
            RenameColumn(table: "dbo.Reservas", name: "UsuarioId", newName: "Usuario_Id");
            RenameColumn(table: "dbo.Reservas", name: "SalaId", newName: "Sala_IdSala");
            AddColumn("dbo.Reservas", "SalaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservas", "Sala_IdSala");
            AddForeignKey("dbo.Reservas", "Sala_IdSala", "dbo.Salas", "IdSala");
        }
    }
}
