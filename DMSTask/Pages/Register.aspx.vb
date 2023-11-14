Imports Application

Public Class Register : Inherits BasePageOutside

    Private ReadOnly _userApplicationService As IUserApplicationService

    Sub New()
        _userApplicationService = New UserApplicationService()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtFirstName.Attributes("placeholder") = "Nombres"
        txtLastName.Attributes("placeholder") = "Apellidos"
        txtEmail.Attributes("placeholder") = "Email"
        txtPassword.Attributes("placeholder") = "Contraseña"
    End Sub

    Public Sub Register()
        Dim email As String = HttpUtility.HtmlEncode(txtEmail.Text)
        Dim password As String = HttpUtility.HtmlEncode(txtPassword.Text)
        Dim firstName As String = HttpUtility.HtmlEncode(txtFirstName.Text)
        Dim lastName As String = HttpUtility.HtmlEncode(txtLastName.Text)

        If ValidateInputsParamArray(email, password, firstName, lastName) Then
            MsgBox("Se deben de llenar todos los campos del formulario", vbCritical, Titles.TITLE_USER)
            Return
        End If

        Dim user = _userApplicationService.Register(email, password, firstName, lastName)

        If user Then
            MsgBox("Usuario creado satisfactoriamente", vbOKOnly, Titles.TITLE_USER)
            Response.Redirect("~/Pages/Authentication.aspx")
        Else
            MsgBox("No sepudo crear el usuario", vbCritical, Titles.TITLE_USER)
        End If
    End Sub

End Class