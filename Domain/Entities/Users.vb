Public Class Users : Inherits AuditEntities
    Public Property FirstName As String
    Public Property LastName As String
    Public Property Email As String
    Public Property Password As String
    Public Property Salt As String
    Public Overridable Property Tasks As ICollection(Of Tasks)
End Class
