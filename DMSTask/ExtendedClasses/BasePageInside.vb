Public MustInherit Class BasePageInside : Inherits BasePage

    Public dataTable As DataTable
    Public dataRow As DataRow

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~/Pages/Authentication")
        End If

        If ValidateInputsParamArray(Session("Id")?.ToString(), Session("FullName"), Session("FirstName"), Session("LastName")) Then
            FormsAuthentication.SignOut()
            Response.Redirect("/Default.aspx")
        End If
    End Sub

End Class
