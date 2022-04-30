Public Class PayrollTransaction
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
        Try
            If Request.Cookies("EmpASName").Value = Nothing Then
                Response.Redirect("~/EmpAcademicSession.aspx")
            Else
                btnFSName.Text = "Financial Year:" & Request.Cookies("EmpASName").Value
            End If
        Catch ex As Exception
            Response.Redirect("~/EmpAcademicSession.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnFSName_Click(sender As Object, e As EventArgs) Handles btnFSName.Click
        Response.Redirect("EmpAcademicSession.aspx")
    End Sub
End Class