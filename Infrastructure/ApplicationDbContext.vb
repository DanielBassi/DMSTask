Imports System.Data.Entity
Imports Domain

Public Class ApplicationDbContext : Inherits DbContext

    Public Sub New()
        MyBase.New("connectionStrings")
    End Sub

    Public Property Users As DbSet(Of Users)
    Public Property Tasks As DbSet(Of Tasks)
    Public Property Categories As DbSet(Of Categories)
    Public Property Budgets As DbSet(Of Budgets)

End Class
