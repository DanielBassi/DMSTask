Imports System.Web.Security
Public Class SiteMaster : Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Public Sub SignOut()
        FormsAuthentication.SignOut()
        Response.Redirect("/Default.aspx")
    End Sub

End Class