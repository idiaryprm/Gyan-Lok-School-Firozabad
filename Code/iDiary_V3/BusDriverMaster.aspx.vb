Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class BusDriverMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""
        txtNumber.Text = ""
        txtAddress.Text = ""
        LoadMasterInfo(46, lstMasters)
        lblStatus.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Wrong Input!"
            txtName.Focus()
            Exit Sub
        End If

       
       
       
        Dim sqlStr As String = ""
        

        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into busDriverMaster Values('" & txtName.Text & "','" & txtNumber.Text & "','" & txtAddress.Text & "')"
        Else
            'Update
            sqlStr = "Update busDriverMaster Set DriverName='" & txtName.Text & "',DriverNumber='" & txtNumber.Text & "', DriverAddress='" & txtAddress.Text & "' Where DriverID=" & Val(txtID.Text)
        End If
        
        
        ExecuteQuery_Update(SqlStr)

        
        
        InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
       
       
       

        Dim sqlStr As String = "Select * From busDriverMaster Where DriverName='" & lstMasters.Text & "'"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("DriverID")
            txtName.Text = myReader("DriverName")
            txtNumber.Text = myReader("DriverNumber")
            txtAddress.Text = myReader("DriverAddress")
        End While
        myReader.Close()
        
        
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From busDriverMaster Where DriverID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        
        
        InitControls()
    End Sub
End Class