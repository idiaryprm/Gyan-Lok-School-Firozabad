Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Parent_ApplyLeave
    Inherits System.Web.UI.Page

    Protected Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        'Dim fromDate As Date = txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2)
        ' Dim toDate As Date = txtTo.Text.Substring(6, 4) & "/" & txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2)
        If txtReason.Text.Length <= 0 Then
            lblStatus.Text = "Please enter the Reason..."
            txtReason.Focus()
            Exit Sub
        End If

        Dim sqlStr = "Insert into StudentLeave Values(" & _
            "'" & txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2) & "'," & _
            "'" & txtTo.Text.Substring(6, 4) & "/" & txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2) & "'," & _
            "'" & txtReason.Text & "'," & _
            "'" & txtMessage.Text & "'," & _
            "'" & Request.Cookies("SID").Value & "')"

        ExecuteQuery_Update(sqlStr)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Application Sent Successfully ..');", True)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub InitControls()
        txtFrom.Text = ""
        txtMessage.Text = ""
        txtTo.Text = ""
        txtReason.Text = ""
    End Sub
End Class
