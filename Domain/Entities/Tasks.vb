Imports System.ComponentModel.DataAnnotations.Schema

Public Class Tasks : Inherits AuditEntities
    Public Property Title As String
    Public Property Description As String
    Public Property ExpiredDate As DateTime
    Public Property UserId As Guid
    <ForeignKey("UserId")>
    Public Overridable Property UserNavigation As Users
End Class
