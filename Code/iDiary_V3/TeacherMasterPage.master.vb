Public Class TeacherMasterPage
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            'Dim tmp As String = Request.Cookies("UID").Value
            ' If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Exam") Or Request.Cookies("UType").Value.ToString.Contains("Fee") Then
            'If Request.Cookies("UType").Value.ToString.Contains("Admin-1") Or Request.Cookies("UType").Value.ToString.Contains("Teacher-1") Or Request.Cookies("UType").Value.ToString.Contains("Exam") Then
            '    'Allow
            'Else
            '    Response.Redirect("/./AccessDenied.aspx", False)
            'End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class