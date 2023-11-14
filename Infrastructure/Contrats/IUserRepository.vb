Imports Domain

Public Interface IUserRepository
    Function Auth(ByVal email As String, ByVal password As String) As Users
End Interface
