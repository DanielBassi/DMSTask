Public Interface IBudgetApplicationService
    Function GetAll(ByVal userId As Guid) As DataTable
    Function GetCategories() As DataTable
    Function SaveBudget(data As DataRow) As Boolean
    Function DeleteBudget(Id As Guid) As Boolean
    Function UpdateBudget(data As DataRow) As Boolean
    Function Import(filePath As String, userId As Guid) As Boolean
    Function Search(searchTerm As String, userId As Guid) As DataTable
End Interface
