Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class HouseMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""
        LoadMasterInfo(4, lstMasters)
        lblStatus.Text = ""
        chkDefault.Checked = False
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Wrong Input!"
            txtName.Focus()
            Exit Sub
        End If
        If txtID.Text = "" Then
            'Insert
            If CheckDoubleEntry(4, txtName.Text) > 0 Then
                lblStatus.Text = "Same House allready Exist..."
                txtName.Focus()
                Exit Sub
            End If
        End If
       
       
       
        Dim sqlStr As String = ""
        
        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update House Set IsDefault=0"
            
            
            ExecuteQuery_Update(SqlStr)
        End If
        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into House Values('" & txtName.Text & "','" & IsDefault & "')"
        Else
            'Update
            sqlStr = "Update House Set HouseName='" & txtName.Text & "',IsDefault=" & IsDefault & " Where HouseID=" & Val(txtID.Text)
        End If
        
        
        ExecuteQuery_Update(SqlStr)

        
        
        InitControls()

    End Sub
   
    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
       
       
       

        Dim sqlStr As String = "Select * From House Where HouseName='" & lstMasters.Text & "'"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("HouseID")
            txtName.Text = myReader("HouseName")
            Try
                If myReader("IsDefault") = "1" Then
                    chkDefault.Checked = True
                Else
                    chkDefault.Checked = False
                End If
            Catch ex As Exception
                chkDefault.Checked = False
            End Try
        End While
        myReader.Close()
        
        
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From House Where HouseID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        
        
        InitControls()
    End Sub

End Class