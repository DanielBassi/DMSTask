Public Interface IRepository(Of TEntity As Class)
    Function GetAll() As ICollection(Of TEntity)
    Function GetById(ByVal Id As Guid) As TEntity
    Sub Add(entity As TEntity)
    Sub AddRange(entities As List(Of TEntity))
    Sub Update(entity As TEntity)
    Sub Delete(entity As TEntity)
    Sub Save()
    Function Import(filePath As String, userId As Guid, dataToImport As Dictionary(Of String, Integer)) As DataTable
End Interface
