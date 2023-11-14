Public MustInherit Class BasePage : Inherits System.Web.UI.Page

    Public Function ValidateInputsParamArray(ParamArray args() As String) As Boolean
        For Each arg As String In args
            If String.IsNullOrEmpty(arg) Then
                Return True
            End If
        Next
        Return False
    End Function

End Class
