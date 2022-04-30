Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class SMSTemplates
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("SMS") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
            btnRemove.Enabled = False
        End If
    End Sub

    Private Sub InitControls()

        txtCode.Text = ""
        txtMessage.Text = ""
        txtID.Text = ""
        LoadSMSTemplate(lstMasters)
        lblStatus.Text = ""
        txtCode.Focus()

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtCode.Text.Length <= 0 Then
            lblStatus.Text = "Invalid Code!"
            txtCode.Focus()
            Exit Sub
        End If
        If txtMessage.Text.Length <= 0 Then
            lblStatus.Text = "Invalid Message!"
            txtMessage.Focus()
            Exit Sub
        End If
        If txtID.Text = "" And IsTemplateCodeExist(txtCode.Text) = True Then
            lblStatus.Text = "Template Code Already Exist..."
            txtCode.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into SMSTemplates Values('" & txtCode.Text & "','" & txtMessage.Text & "')"
        Else
            'Update
            sqlStr = "Update SMSTemplates Set TemplateCode='" & txtCode.Text & "', TemplateMessage='" & txtMessage.Text & "' Where TemplateID=" & Val(txtID.Text)
        End If
        
        ExecuteQuery_Update(SqlStr)
        InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
       
        Dim sqlStr As String = "Select * From SMSTemplates Where TemplateCode='" & lstMasters.Text & "'"
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("TemplateID")
            txtCode.Text = myReader("TemplateCode")
            txtMessage.Text = myReader("TemplateMessage")
        End While
        myReader.Close()
        
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
        Dim sqlStr As String = "Delete From SMSTemplates Where TemplateID=" & Val(txtID.Text)
        
        ExecuteQuery_Update(SqlStr)
        
        InitControls()
    End Sub
End Class