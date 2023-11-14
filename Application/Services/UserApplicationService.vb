Imports System.Text.RegularExpressions
Imports Domain
Imports Infrastructure

Public Class UserApplicationService : Implements IUserApplicationService

    Private _userRepository As IUserRepository
    Private _repository As IRepository(Of Users)
    Dim dataTable As New DataTable()
    Private dataRow As DataRow = dataTable.NewRow()

    Sub New()
        _userRepository = New UserRepository()
        _repository = New Repository(Of Users)
        dataTable.Columns.Add("Id", GetType(Guid))
        dataTable.Columns.Add("Email", GetType(String))
        dataTable.Columns.Add("FirstName", GetType(String))
        dataTable.Columns.Add("LastName", GetType(String))
    End Sub

    Public Function Auth(email As String, password As String) As DataRow Implements IUserApplicationService.Auth
        Try
            dataTable.Rows.Clear()
            Dim user = _userRepository.Auth(email, password)

            If Not IsNothing(user) Then
                dataRow("Id") = user.Id
                dataRow("Email") = user.Email
                dataRow("FirstName") = user.FirstName
                dataRow("LastName") = user.LastName
                Return dataRow
            End If

            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Register(email As String, password As String, firstName As String, lastName As String) As Boolean Implements IUserApplicationService.Register

        Try
            dataTable.Rows.Clear()
            Dim user As Users = New Users()
            user.Id = Guid.NewGuid()
            user.FirstName = firstName
            user.LastName = lastName
            user.Email = email
            user.Salt = UserRepository.Salt()
            user.Password = UserRepository.HashPassword(password, user.Salt)
            user.Created_at = DateTime.Now
            _repository.Add(user)
            _repository.Save()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Function Import(filePath As String, userId As Guid) As Boolean Implements IUserApplicationService.Import
        Try
            Dim columnsAndIndex As New Dictionary(Of String, Integer)
            columnsAndIndex.Add("FirstName", 1)
            columnsAndIndex.Add("LastName", 2)
            columnsAndIndex.Add("Email", 7)
            columnsAndIndex.Add("Password", 1)

            Dim records = _repository.Import(filePath, userId, columnsAndIndex)
            Dim users As New List(Of Users)

            For Each record As DataRow In records.Rows
                Dim user As New Users
                user.Id = Guid.Parse(record("Id").ToString())
                user.FirstName = record("FirstName")
                user.LastName = record("LastName")
                user.Email = record("Email")
                user.Salt = UserRepository.Salt()
                user.Password = UserRepository.HashPassword(record("Password"), user.Salt)
                user.Created_at = record("Created_at")
                users.Add(user)
            Next

            _repository.AddRange(users)
            _repository.Save()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
