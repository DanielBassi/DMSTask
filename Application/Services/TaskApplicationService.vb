Imports Domain
Imports Infrastructure

Public Class TaskApplicationService : Implements ITaskApplicationService

    Dim dataTable As New DataTable()
    Private ReadOnly _repository As IRepository(Of Tasks)

    Public Sub New()
        _repository = New Repository(Of Tasks)
        dataTable.Columns.Add("Id", GetType(Guid))
        dataTable.Columns.Add("Title", GetType(String))
        dataTable.Columns.Add("Description", GetType(String))
        dataTable.Columns.Add("ExpiredDate", GetType(Date))
    End Sub

    Private Sub Mapper(ByVal tasks As List(Of Tasks))
        For Each task As Tasks In tasks
            Dim newRow As DataRow = dataTable.NewRow()
            newRow("Id") = task.Id
            newRow("Title") = task.Title
            newRow("Description") = task.Description
            newRow("ExpiredDate") = task.ExpiredDate
            dataTable.Rows.Add(newRow)
        Next
    End Sub

    Public Function GetAll(userId As Guid) As DataTable Implements ITaskApplicationService.GetAll
        Try
            dataTable.Rows.Clear()
            Dim tasks As ICollection(Of Tasks) = _repository.GetAll().Where(Function(w) w.UserId.Equals(userId)).ToList()
            Mapper(tasks)
            Return dataTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function SaveTask(data As DataRow) As Boolean Implements ITaskApplicationService.SaveTask
        Try
            Dim task As Tasks = New Tasks
            task.Id = Guid.NewGuid()
            task.Title = data("Title")
            task.Description = data("Description")
            task.ExpiredDate = data("ExpiredDate")
            task.UserId = data("UserId")
            task.Created_at = DateTime.Now
            _repository.Add(task)
            _repository.Save()
            Return True
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function DeleteTask(Id As Guid) As Boolean Implements ITaskApplicationService.DeleteTask
        Try
            Dim task As Tasks = _repository.GetById(Id)
            _repository.Delete(task)
            _repository.Save()
            Return True
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function UpdateTask(data As DataRow) As Boolean Implements ITaskApplicationService.UpdateTask
        Try
            Dim task As Tasks = New Tasks
            task.Id = data("Id")
            task.Title = data("Title")
            task.Description = data("Description")
            task.ExpiredDate = data("ExpiredDate")
            task.UserId = data("UserId")
            _repository.Update(task)
            _repository.Save()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function Import(filePath As String, userId As Guid) As Boolean Implements ITaskApplicationService.Import
        Try
            Dim columnsAndIndex As New Dictionary(Of String, Integer)
            columnsAndIndex.Add("Title", 1)
            columnsAndIndex.Add("Description", 2)
            columnsAndIndex.Add("ExpiredDate", 3)

            Dim records = _repository.Import(filePath, userId, columnsAndIndex)
            Dim tasks As New List(Of Tasks)

            For Each record As DataRow In records.Rows
                Dim task As New Tasks
                task.Id = Guid.Parse(record("Id").ToString())
                task.UserId = Guid.Parse(record("UserId").ToString())
                task.Title = record("Title")
                task.Description = record("Description")
                task.ExpiredDate = record("ExpiredDate")
                task.Created_at = record("Created_at")
                tasks.Add(task)
            Next

            _repository.AddRange(tasks)
            _repository.Save()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Search(searchTerm As String, userId As Guid) As DataTable Implements ITaskApplicationService.Search
        Try
            Dim tasks = _repository.GetAll() _
            .Where(Function(w) w.UserId.Equals(userId) AndAlso
                (w.Title.Contains(searchTerm) OrElse w.Description.Contains(searchTerm))) _
            .OrderByDescending(Function(ob) ob.Created_at) _
            .ToList()

            Mapper(tasks)
            Return dataTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
