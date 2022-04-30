Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Parent_ChangePassword
    Inherits System.Web.UI.Page


    Protected Sub btnChangePassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangePassword.Click

        If txtNew.Text.Length <= 0 Then
            lblStatus.Text = "Please provide new passowrd..."
            txtNew.Focus()
            Exit Sub
        End If
        If txtNew.Text = txtRe.Text Then
        Else
            lblStatus.Text = "Password does not match with retyped password"
            txtNew.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""

        sqlStr = "Update ParentLogins Set ParentPassword='" & txtNew.Text & "' Where SID=" & Request.Cookies("SID").Value
        ExecuteQuery_Update(sqlStr)

        InitControls()
        lblStatus.Text = "Password Changed Successfully..."
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        txtNew.Text = ""
        txtRe.Text = ""
        lblStatus.Text = ""
    End Sub
End Class
