namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creacion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agenda",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Owner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Agenda_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agenda", t => t.Agenda_Id)
                .Index(t => t.Agenda_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agenda", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Agenda_Id", "dbo.Agenda");
            DropIndex("dbo.Users", new[] { "Agenda_Id" });
            DropIndex("dbo.Agenda", new[] { "Owner_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Agenda");
        }
    }
}
