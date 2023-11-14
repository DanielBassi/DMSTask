Imports System.Threading.Tasks
Imports Application

Public Class Tasks : Inherits BasePageInside

    Private ReadOnly _taskApplicationService As ITaskApplicationService

    Sub New()
        _taskApplicationService = New TaskApplicationService()
        dataTable = DataTables.GetDataTable(DataTableEnums.TASK)
        dataRow = dataTable.NewRow()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If

    End Sub

    Protected Sub SaveTask()

        Dim title = txtTitle.Text
        Dim description = TextDescription.Text
        Dim fechaVencimiento = TextFechaVencimiento.Text

        If ValidateInputsParamArray(title, description, fechaVencimiento) Then
            MsgBox("Se deben de llenar todos los campos del formulario", vbCritical, Titles.TITLE_TASK)
            Return
        End If

        dataRow("Title") = title
        dataRow("Description") = description
        dataRow("ExpiredDate") = fechaVencimiento
        dataRow("UserId") = Session("Id")

        Dim response As Boolean = _taskApplicationService.SaveTask(dataRow)

        If response Then
            BindData()
            MsgBox("Tarea guardada satisfatoriamente", vbOKOnly, Titles.TITLE_TASK)
            ResetForm()
        Else
            MsgBox("No se pudo guardar la tarea", vbAbort, Titles.TITLE_TASK)
        End If
    End Sub

    Protected Sub rowCancelEvent(sender As Object, e As GridViewCancelEditEventArgs)
        gridView.EditIndex = -1
        BindData()
    End Sub

    Protected Sub rowEditEvent(sender As Object, e As GridViewEditEventArgs)
        gridView.EditIndex = e.NewEditIndex
    End Sub

    Protected Sub rowUpdteEvent(sender As Object, e As GridViewUpdateEventArgs)
        dataRow("Id") = gridView.DataKeys(e.RowIndex).Value
        dataRow("Title") = e.NewValues.Values(0)
        dataRow("Description") = e.NewValues.Values(1)
        dataRow("ExpiredDate") = e.NewValues.Values(2)
        dataRow("UserId") = Session("Id")
        gridView.EditIndex = -1
        Dim Response As Boolean = _taskApplicationService.UpdateTask(dataRow)

        If response Then
            BindData()
            ResetForm()
            MsgBox("Tarea actualizada satisfatoriamente", vbOKOnly, Titles.TITLE_TASK)
        Else
            MsgBox("No se pudo actualizar la tarea", vbCritical, Titles.TITLE_TASK)
        End If
    End Sub

    Protected Sub rowDeleteEvent(sender As Object, e As GridViewDeleteEventArgs)
        Dim response As Boolean = _taskApplicationService.DeleteTask(gridView.DataKeys(e.RowIndex).Value)

        If response Then
            BindData()
            MsgBox("Tarea eliminada satisfatoriamente", vbOKOnly, Titles.TITLE_TASK)
        Else
            MsgBox("No se pudo eliminar la tarea", vbAbort, Titles.TITLE_TASK)
        End If
    End Sub

    Protected Sub GridView_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gridView.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        BindData()
    End Sub


    Private Sub BindData()
        Dim tasks As DataTable
        Dim searchTerm As String = txtSearch.Text.Trim()
        If String.IsNullOrEmpty(searchTerm) Then
            tasks = _taskApplicationService.GetAll(Session("Id"))
        Else
            tasks = _taskApplicationService.Search(searchTerm, Session("Id"))
        End If

        If tasks.Rows.Count > 0 Then
            gridView.DataSource = tasks
            lblEmptyData.Visible = False
        Else
            lblEmptyData.Visible = True
            gridView.DataSource = Nothing
        End If
        gridView.DataBind()
    End Sub

    Private Sub ResetForm()
        txtTitle.Text = ""
        TextDescription.Text = ""
        TextFechaVencimiento.Text = ""
    End Sub

End Class