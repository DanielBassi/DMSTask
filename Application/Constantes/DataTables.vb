Public Class DataTables

    Private Shared dataTable As New DataTable
    Private Shared dataRow As DataRow = dataTable.NewRow()

    Public Shared Function GetDataTable(ByVal type As DataTableEnums)
        SetDataTable(type)
        Return dataTable
    End Function

    Private Shared Sub SetDataTable(ByVal type As DataTableEnums)
        dataTable.Reset()

        Select Case type
            Case DataTableEnums.USER
                dataTable.Columns.Add("Id", GetType(Guid))
                dataTable.Columns.Add("Email", GetType(String))
                dataTable.Columns.Add("FirstName", GetType(String))
                dataTable.Columns.Add("LastName", GetType(String))

            Case DataTableEnums.BUDGET
                dataTable.Columns.Add("Id", GetType(Guid))
                dataTable.Columns.Add("UserId", GetType(Guid))
                dataTable.Columns.Add("Name", GetType(String))
                dataTable.Columns.Add("Amount", GetType(String))
                dataTable.Columns.Add("Category", GetType(Guid))
                dataTable.Columns.Add("CategoryDescription", GetType(String))

            Case DataTableEnums.TASK
                dataTable.Columns.Add("Id", GetType(Guid))
                dataTable.Columns.Add("UserId", GetType(Guid))
                dataTable.Columns.Add("Title", GetType(String))
                dataTable.Columns.Add("Description", GetType(String))
                dataTable.Columns.Add("ExpiredDate", GetType(DateTime))

        End Select

    End Sub


End Class