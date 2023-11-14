Public Class BasePageOutside : Inherits BasePage

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If User.Identity.IsAuthenticated Then
            Response.Redirect("~/")
        End If
    End Sub

End Class
