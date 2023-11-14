Imports System.Data.Entity
Imports OfficeOpenXml
Imports System.IO

Public Class Repository(Of TEntity As Class) : Implements IRepository(Of TEntity)

    Private ReadOnly _context As ApplicationDbContext
    Private ReadOnly _dbSet As DbSet(Of TEntity)

    Public Sub New()
        _context = New ApplicationDbContext()
        _dbSet = _context.Set(Of TEntity)()
    End Sub

    Public Function GetAll() As ICollection(Of TEntity) Implements IRepository(Of TEntity).GetAll
        Return _dbSet.AsNoTracking().ToList()
    End Function

    Public Function GetById(Id As Guid) As TEntity Implements IRepository(Of TEntity).GetById
        Return _dbSet.Find(Id)
    End Function

    Public Sub Add(entity As TEntity) Implements IRepository(Of TEntity).Add
        _dbSet.Add(entity)
    End Sub

    Public Sub AddRange(entities As List(Of TEntity)) Implements IRepository(Of TEntity).AddRange
        _dbSet.AddRange(entities)
    End Sub

    Public Sub Update(entity As TEntity) Implements IRepository(Of TEntity).Update
        _dbSet.Attach(entity)
        _context.Entry(entity).State = EntityState.Modified
    End Sub

    Public Sub Delete(entity As TEntity) Implements IRepository(Of TEntity).Delete
        _dbSet.Remove(entity)
    End Sub

    Public Sub Save() Implements IRepository(Of TEntity).Save
        _context.SaveChanges()
    End Sub

    Public Function Import(filePath As String, userId As Guid, columnsAndIndex As Dictionary(Of String, Integer)) As DataTable Implements IRepository(Of TEntity).Import
        ' Utiliza la biblioteca EPPlus para leer el archivo Excel
        ExcelPackage.LicenseContext = LicenseContext.Commercial

        Dim dataTable As DataTable = GenerateDataTableStructure(columnsAndIndex)

        Using package As New ExcelPackage(New FileInfo(filePath))
            Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets(0)
            FillDataTableWithDynamicData(dataTable, columnsAndIndex, userId, worksheet)
        End Using

        Return dataTable
    End Function

    Private Function GenerateDataTableStructure(columnsAndIndex As Dictionary(Of String, Integer)) As DataTable
        Dim dataTable As New DataTable()

        If columnsAndIndex.Any Then
            For Each key In columnsAndIndex.Keys
                dataTable.Columns.Add(key, GetType(String))
            Next
        End If

        dataTable.Columns.Add("Id", GetType(Guid))
        dataTable.Columns.Add("UserId", GetType(Guid))
        dataTable.Columns.Add("Created_at", GetType(DateTime))

        Return dataTable
    End Function

    Private Sub FillDataTableWithDynamicData(ByRef dataTable As DataTable, columnsAndIndex As Dictionary(Of String, Integer), userId As Guid, worksheet As ExcelWorksheet)

        For row As Integer = 2 To worksheet.Dimension.End.Row

            Dim newRow As DataRow = dataTable.NewRow()

            For Each column In columnsAndIndex
                newRow(column.Key) = worksheet.Cells(row, column.Value).Text
            Next

            newRow("Id") = Guid.NewGuid()
            newRow("UserId") = userId
            newRow("Created_at") = DateTime.Now
            dataTable.Rows.Add(newRow)
        Next

    End Sub

End Class
