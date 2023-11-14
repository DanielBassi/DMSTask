Imports System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder
Imports Application

Public Class Budget : Inherits BasePageInside

    Private ReadOnly _budgetApplicationService As IBudgetApplicationService

    Sub New()
        _budgetApplicationService = New BudgetApplicationService()
        dataTable = DataTables.GetDataTable(DataTableEnums.BUDGET)
        dataRow = dataTable.NewRow()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
            FillDropDownList(txtCategory)
        End If
    End Sub

    Protected Sub SaveBudget()

        Dim name = txtName.Text
        Dim amount = txtAmount.Text
        Dim category = txtCategory.Text

        If ValidateInputsParamArray(name, amount, category) Then
            MsgBox("Se deben de llenar todos los campos del formulario", vbCritical, Titles.TITLE_BUDGET)
            Return
        End If

        dataRow("Name") = name
        dataRow("Amount") = amount
        dataRow("Category") = category
        dataRow("UserId") = Session("Id")

        Dim response As Boolean = _budgetApplicationService.SaveBudget(dataRow)

        If response Then
            BindData()
            ResetForm()
            MsgBox("Tarea guardada satisfatoriamente", vbOKOnly, Titles.TITLE_BUDGET)
        Else
            MsgBox("No se pudo guardar la tarea", vbAbort, Titles.TITLE_BUDGET)
        End If
    End Sub

    Protected Sub rowCancelEvent(sender As Object, e As GridViewCancelEditEventArgs)
        gridView.EditIndex = -1
        BindData()
    End Sub

    Protected Sub rowEditEvent(sender As Object, e As GridViewEditEventArgs)
        Dim row As GridViewRow = gridView.Rows(e.NewEditIndex)
        gridView.EditIndex = e.NewEditIndex

        If row.RowType = DataControlRowType.DataRow Then
            Dim ddlEdit As DropDownList = CType(row.FindControl("dropDownCategory"), DropDownList)
            If Not IsNothing(ddlEdit) Then
                FillDropDownList(ddlEdit)
            End If
        End If

    End Sub

    Protected Sub rowUpdteEvent(sender As Object, e As GridViewUpdateEventArgs)
        dataRow("Id") = gridView.DataKeys(e.RowIndex).Value
        dataRow("Name") = e.NewValues.Values(0)
        dataRow("Amount") = e.NewValues.Values(1)
        dataRow("Category") = e.NewValues.Values(2)
        dataRow("UserId") = Session("Id")

        Dim response As Boolean = _budgetApplicationService.UpdateBudget(dataRow)
        gridView.EditIndex = -1

        If response Then
            BindData()
            ResetForm()
            MsgBox("Tarea actualizada satisfatoriamente", vbOKOnly, Titles.TITLE_BUDGET)
        Else
            MsgBox("No se pudo actualizar la tarea", vbAbort, Titles.TITLE_BUDGET)
        End If
    End Sub

    Protected Sub rowDeleteEvent(sender As Object, e As GridViewDeleteEventArgs)
        Dim response As Boolean = _budgetApplicationService.DeleteBudget(gridView.DataKeys(e.RowIndex).Value)
        If response Then
            BindData()
            MsgBox("Tarea eliminada satisfatoriamente", vbOKOnly, Titles.TITLE_BUDGET)
        Else
            MsgBox("No se pudo eliminar la tarea", vbAbort, Titles.TITLE_BUDGET)
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        BindData()
    End Sub


    Private Sub BindData()
        Dim budgets As DataTable
        Dim searchTerm As String = txtSearch.Text.Trim()
        If String.IsNullOrEmpty(searchTerm) Then
            budgets = _budgetApplicationService.GetAll(Session("Id"))
        Else
            budgets = _budgetApplicationService.Search(searchTerm, Session("Id"))
        End If

        If budgets.Rows.Count > 0 Then
            gridView.DataSource = budgets
            lblEmptyData.Visible = False
        Else
            lblEmptyData.Visible = True
            gridView.DataSource = Nothing
        End If
        gridView.DataBind()
    End Sub

    Protected Sub GridView_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gridView.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub FillDropDownList(ByVal dropDown As DropDownList)
        dropDown.DataSource = _budgetApplicationService.GetCategories()
        dropDown.DataTextField = "Name"
        dropDown.DataValueField = "Id"
        dropDown.DataBind()
    End Sub

    Private Sub ResetForm()
        txtName.Text = ""
        txtAmount.Text = ""
        txtCategory.Text = ""
    End Sub

End Class