Public Class Certificates1
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Try
        '    If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Student") Then
        '        'Allow
        '    Else
        '        Response.Redirect("/./AccessDenied.aspx", False)
        '    End If
        'Catch ex As Exception
        '    Response.Redirect("~/Login.aspx")
        'End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class