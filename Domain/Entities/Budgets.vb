Imports System.ComponentModel.DataAnnotations.Schema

Public Class Budgets : Inherits AuditEntities
    Public Property Name As String
    Public Property Amount As Double
    Public Property CategoryId As Guid
    <ForeignKey("CategoryId")>
    Public Overridable Property CategoryNavigation As Categories
    Public Property UserId As Guid
    <ForeignKey("UserId")>
    Public Overridable Property UserNavigation As Users
End Class
