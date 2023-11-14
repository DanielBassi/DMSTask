Imports Application

Public Class Authentication : Inherits BasePageOutside

    Private ReadOnly _userApplicationService As IUserApplicationService

    Sub New()
        _userApplicationService = New UserApplicationService()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _txtEmail.Attributes("placeholder") = "Email"
        _txtPassword.Attributes("placeholder") = "Contraseña"
    End Sub

    Public Sub Auth()
        Dim email As String = HttpUtility.HtmlEncode(_txtEmail.Text)
        Dim password As String = HttpUtility.HtmlEncode(_txtPassword.Text)

        If ValidateInputsParamArray(email, password) Then
            MsgBox("Se deben de llenar todos los campos del formulario", vbCritical, Titles.TITLE_USER)
            Return
        End If

        Dim user = _userApplicationService.Auth(email, password)

        If Not IsNothing(user) Then
            Dim fullName = user("FirstName") & user("LastName")
            FormsAuthentication.RedirectFromLoginPage(user("Id").ToString(), createPersistentCookie:=True)
            Session("Id") = user("Id")
            Session("FirstName") = user("FirstName")
            Session("LastName") = user("LastName")
            Session("FullName") = fullName
            Session("Email") = user("Email")
            Response.Redirect("~/")
        Else
            MsgBox("Credenciales incorrectas", vbCritical, Titles.TITLE_USER)
        End If
    End Sub

End Class