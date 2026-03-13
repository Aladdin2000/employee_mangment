namespace employees_mangment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeePropertyValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        PropertyDefinitionId = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.PropertyDefinitions", t => t.PropertyDefinitionId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.PropertyDefinitionId);
            
            CreateTable(
                "dbo.employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PropertyDefinitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        IsRequired = c.Boolean(nullable: false),
                        DropdownOptions = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeePropertyValues", "PropertyDefinitionId", "dbo.PropertyDefinitions");
            DropForeignKey("dbo.EmployeePropertyValues", "EmployeeId", "dbo.employees");
            DropIndex("dbo.EmployeePropertyValues", new[] { "PropertyDefinitionId" });
            DropIndex("dbo.EmployeePropertyValues", new[] { "EmployeeId" });
            DropTable("dbo.PropertyDefinitions");
            DropTable("dbo.employees");
            DropTable("dbo.EmployeePropertyValues");
        }
    }
}
