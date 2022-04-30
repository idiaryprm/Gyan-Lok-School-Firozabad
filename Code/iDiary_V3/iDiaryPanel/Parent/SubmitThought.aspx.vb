Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Parent_SubmitThought
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            txtThought.Text = ""
            'txtName.Text = ""
            'txtClass.Text = ""
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try

            Dim sqlStr As String = ""

            sqlStr = "Insert into Thoughts Values ('" & txtThought.Text & "'," & Request.Cookies("SID").Value & ",'" & Now.Year & "/" & Now.Month & "/" & Now.Day & "')"
            ExecuteQuery_Update(sqlStr)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Your Thought is Saved...');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        txtThought.Text = ""
        'response.redirect("~/ViewAllThoughts.aspx")
    End Sub

End Class
