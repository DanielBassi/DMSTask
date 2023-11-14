Imports Infrastructure

Public Interface ITaskApplicationService
    Function GetAll(ByVal userId As Guid) As DataTable
    Function SaveTask(data As DataRow) As Boolean
    Function DeleteTask(Id As Guid) As Boolean
    Function UpdateTask(data As DataRow) As Boolean
    Function Import(ByVal filePath As String, ByVal userId As Guid) As Boolean
    Function Search(searchTerm As String, userId As Guid) As DataTable
End Interface
