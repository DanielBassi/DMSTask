Imports System.IO
Imports Application

Public Class Import : Inherits BasePageInside

    Private ReadOnly _taskApplicationService As ITaskApplicationService
    Private ReadOnly _userApplicationService As IUserApplicationService
    Private ReadOnly _budgetApplicationService As IBudgetApplicationService

    Sub New()
        _taskApplicationService = New TaskApplicationService()
        _userApplicationService = New UserApplicationService()
        _budgetApplicationService = New BudgetApplicationService()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub UploadFile(sender As Object, e As EventArgs)
        If fileUploadTasks.HasFile Then
            Import(fileUploadTasks, 1)

        ElseIf fileUploadBudgets.HasFile Then
            Import(fileUploadBudgets, 2)

        ElseIf fileUploadUsers.HasFile Then
            Import(fileUploadUsers, 3)

        Else
            MsgBox("Se debe deselecciona un archivo", vbCritical, Titles.TITLE_IMPORT)

        End If
    End Sub

    Private Sub Import(ByVal fileUpload As FileUpload, ByVal service As Integer)
        Dim fileName As String = Path.GetFileName(fileUpload.PostedFile.FileName)
        Dim fileExtension As String = Path.GetExtension(fileName)
        If (fileExtension = ".xls" Or fileExtension = ".xlsx") Then
            Dim filePath As String = Server.MapPath("~/Uploads/" & fileName)
            fileUpload.SaveAs(filePath)

            Dim response As Boolean

            Select Case service
                Case 1
                    response = _taskApplicationService.Import(filePath, Session("Id"))
                Case 2
                    response = _budgetApplicationService.Import(filePath, Session("Id"))
                Case 3
                    response = _userApplicationService.Import(filePath, Session("Id"))
            End Select

            If response Then
                MsgBox("Importación realizada", vbOKOnly, Titles.TITLE_IMPORT)
            Else
                MsgBox("No sepudo reaizar la importación", vbCritical, Titles.TITLE_IMPORT)
            End If

        Else
            MsgBox("El archivo no es un Excel", vbCritical, Titles.TITLE_IMPORT)
        End If
    End Sub

End Class