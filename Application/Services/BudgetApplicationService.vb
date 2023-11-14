Imports Domain
Imports Infrastructure

Public Class BudgetApplicationService : Implements IBudgetApplicationService
    Dim dataTable As New DataTable()
    Private ReadOnly _repositoryBudget As IRepository(Of Budgets)
    Private ReadOnly _repositoryCategory As IRepository(Of Categories)

    Public Sub New()
        _repositoryBudget = New Repository(Of Budgets)
        _repositoryCategory = New Repository(Of Categories)

        dataTable.Columns.Add("Id", GetType(Guid))
        dataTable.Columns.Add("Name", GetType(String))
        dataTable.Columns.Add("Amount", GetType(String))
        dataTable.Columns.Add("Category", GetType(Guid))
        dataTable.Columns.Add("CategoryDescription", GetType(String))
    End Sub

    Private Sub Mapper(ByVal budgets As List(Of Budgets))
        For Each budget As Budgets In budgets
            Dim newRow As DataRow = dataTable.NewRow()
            newRow("Id") = budget.Id
            newRow("Name") = budget.Name
            newRow("Amount") = budget.Amount
            newRow("Category") = budget.CategoryId
            newRow("CategoryDescription") = budget.CategoryNavigation.Name
            dataTable.Rows.Add(newRow)
        Next
    End Sub

    Public Function GetAll(userId As Guid) As DataTable Implements IBudgetApplicationService.GetAll
        Try
            dataTable.Rows.Clear()
            Dim budgets As ICollection(Of Budgets) = _repositoryBudget.GetAll() _
                .Where(Function(w) w.UserId.Equals(userId)) _
                .OrderByDescending(Function(ob) ob.Created_at) _
                .ToList()

            Mapper(budgets)
            Return dataTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetCategories() As DataTable Implements IBudgetApplicationService.GetCategories
        Try
            dataTable.Rows.Clear()
            Dim categories As ICollection(Of Categories) = _repositoryCategory.GetAll()

            For Each budget As Categories In categories
                Dim newRow As DataRow = dataTable.NewRow()
                newRow("Id") = budget.Id
                newRow("Name") = budget.Name
                dataTable.Rows.Add(newRow)
            Next

            Return dataTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function SaveBudget(data As DataRow) As Boolean Implements IBudgetApplicationService.SaveBudget
        Try
            Dim budget As Budgets = New Budgets
            budget.Id = Guid.NewGuid()
            budget.UserId = data("UserId")
            budget.Name = data("Name")
            budget.Amount = data("Amount")
            budget.CategoryId = data("Category")
            budget.Created_at = DateTime.Now
            _repositoryBudget.Add(budget)
            _repositoryBudget.Save()
            Return True
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function DeleteBudget(Id As Guid) As Boolean Implements IBudgetApplicationService.DeleteBudget
        Try
            Dim budget As Budgets = _repositoryBudget.GetById(Id)
            _repositoryBudget.Delete(budget)
            _repositoryBudget.Save()
            Return True
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function UpdateBudget(data As DataRow) As Boolean Implements IBudgetApplicationService.UpdateBudget
        Try
            Dim budget As Budgets = New Budgets
            budget.Id = data("Id")
            budget.Name = data("Name")
            budget.Amount = data("Amount")
            budget.CategoryId = data("Category")
            _repositoryBudget.Update(budget)
            _repositoryBudget.Save()
            Return True
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Function Import(filePath As String, userId As Guid) As Boolean Implements IBudgetApplicationService.Import
        Try
            Dim columnsAndIndex As New Dictionary(Of String, Integer)
            columnsAndIndex.Add("Name", 1)
            columnsAndIndex.Add("Amount", 10)
            columnsAndIndex.Add("Category", 9)

            Dim records = _repositoryBudget.Import(filePath, userId, columnsAndIndex)

            Dim categories = records.AsEnumerable() _
                .GroupBy(Function(row) row.Field(Of String)("Category")) _
                .Where(Function(group) group.Count() > 0) _
                .Select(Function(group)
                            Return New Categories With {
                                .Id = Guid.NewGuid(),
                                .Name = group.Key,
                                .Created_at = DateTime.Now
                            }
                        End Function) _
                .ToList()

            _repositoryCategory.AddRange(categories)
            _repositoryCategory.Save()

            Dim budgets As New List(Of Budgets)

            For Each record As DataRow In records.Rows
                Dim budget As New Budgets
                budget.Id = Guid.Parse(record("Id").ToString())
                budget.UserId = Guid.Parse(record("UserId").ToString())
                budget.Name = record("Name")
                budget.Amount = record("Amount")
                budget.CategoryId = categories.Where(Function(w) w.Name.Equals(record("Category"))).FirstOrDefault().Id
                budget.Created_at = record("Created_at")
                budgets.Add(budget)
            Next

            _repositoryBudget.AddRange(budgets)
            _repositoryBudget.Save()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Search(searchTerm As String, userId As Guid) As DataTable Implements IBudgetApplicationService.Search
        Try
            Dim budgets = _repositoryBudget.GetAll() _
            .Where(Function(w) w.UserId.Equals(userId) AndAlso
                (w.Name.Contains(searchTerm) OrElse w.CategoryNavigation.Name.Contains(searchTerm))) _
            .OrderByDescending(Function(ob) ob.Created_at) _
            .ToList()

            Mapper(budgets)
            Return dataTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
