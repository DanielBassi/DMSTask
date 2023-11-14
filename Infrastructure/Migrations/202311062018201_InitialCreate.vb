Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class InitialCreate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Budgets",
                Function(c) New With
                    {
                        .Id = c.Guid(nullable := False),
                        .Name = c.String(),
                        .Amount = c.Double(nullable := False),
                        .CategoryId = c.Guid(nullable := False),
                        .UserId = c.Guid(nullable := False),
                        .Status = c.Boolean(),
                        .Created_at = c.DateTime(),
                        .Updated_at = c.DateTime(),
                        .Deleted_at = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Categories", Function(t) t.CategoryId, cascadeDelete := True) _
                .ForeignKey("dbo.Users", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.CategoryId) _
                .Index(Function(t) t.UserId)
            
            CreateTable(
                "dbo.Categories",
                Function(c) New With
                    {
                        .Id = c.Guid(nullable := False),
                        .Name = c.String(),
                        .Status = c.Boolean(),
                        .Created_at = c.DateTime(),
                        .Updated_at = c.DateTime(),
                        .Deleted_at = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Users",
                Function(c) New With
                    {
                        .Id = c.Guid(nullable := False),
                        .FirstName = c.String(),
                        .LastName = c.String(),
                        .Email = c.String(),
                        .Password = c.String(),
                        .Salt = c.String(),
                        .Status = c.Boolean(),
                        .Created_at = c.DateTime(),
                        .Updated_at = c.DateTime(),
                        .Deleted_at = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Tasks",
                Function(c) New With
                    {
                        .Id = c.Guid(nullable := False),
                        .Title = c.String(),
                        .Description = c.String(),
                        .ExpiredDate = c.DateTime(nullable := False),
                        .UserId = c.Guid(nullable := False),
                        .Status = c.Boolean(),
                        .Created_at = c.DateTime(),
                        .Updated_at = c.DateTime(),
                        .Deleted_at = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Users", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.UserId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Budgets", "UserId", "dbo.Users")
            DropForeignKey("dbo.Tasks", "UserId", "dbo.Users")
            DropForeignKey("dbo.Budgets", "CategoryId", "dbo.Categories")
            DropIndex("dbo.Tasks", New String() { "UserId" })
            DropIndex("dbo.Budgets", New String() { "UserId" })
            DropIndex("dbo.Budgets", New String() { "CategoryId" })
            DropTable("dbo.Tasks")
            DropTable("dbo.Users")
            DropTable("dbo.Categories")
            DropTable("dbo.Budgets")
        End Sub
    End Class
End Namespace
