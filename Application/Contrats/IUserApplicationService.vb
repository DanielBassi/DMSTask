Public Interface IUserApplicationService
    Function Auth(ByVal email As String, ByVal password As String) As DataRow
    Function Register(ByVal email As String, ByVal password As String, ByVal firstName As String, ByVal lastName As String) As Boolean
    Function Import(filePath As String, userId As Guid) As Boolean
End Interface
