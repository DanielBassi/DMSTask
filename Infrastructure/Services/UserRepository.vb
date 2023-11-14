Imports Domain
Imports BCrypt

Public Class UserRepository : Implements IUserRepository

    Private ReadOnly _context As ApplicationDbContext

    Sub New()
        _context = New ApplicationDbContext()
    End Sub

    Public Function Auth(email As String, password As String) As Users Implements IUserRepository.Auth
        Dim user As Users = _context.Users.AsNoTracking().Where(Function(w) _
                                                       w.Email.Equals(email)) _
                                            .FirstOrDefault()

        If user IsNot Nothing AndAlso VerifyPassword(password, user.Password) Then
            Return user
        Else
            Return Nothing
        End If

    End Function

    Public Shared Function Salt()
        Return Net.BCrypt.GenerateSalt()
    End Function

    Public Shared Function HashPassword(password As String, salt As String) As String
        Dim hashedPassword As String = Net.BCrypt.HashPassword(password, salt)
        Return hashedPassword
    End Function

    Private Function VerifyPassword(password As String, hashedPassword As String) As Boolean
        Return Net.BCrypt.Verify(password, hashedPassword)
    End Function

End Class
