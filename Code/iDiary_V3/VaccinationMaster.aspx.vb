Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class VaccinationMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
                Exit Sub
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtCode.Text = ""
        txtID.Text = ""
        txtName.Text = ""
        cboVacType.SelectedIndex = 0
        LoadMasterInfo(42, lstMasters)
        lblStatus.Text = ""
        txtCode.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If txtCode.Text.Length <= 0 Then
            lblStatus.Text = "Wrong Input!"
            txtCode.Focus()
            Exit Sub
        End If

        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Wrong Input!"
            txtName.Focus()
            Exit Sub
        End If
        If txtID.Text = "" Then
            If CheckDoubleEntry(42, txtName.Text) > 0 Then
                lblStatus.Text = "Same Name allready Exist..."
                txtName.Focus()
                Exit Sub
            End If
        End If
        If cboVacType.SelectedIndex = 0 Then
            lblStatus.Text = "Wrong Input!"
            cboVacType.Focus()
            Exit Sub
        End If
       
       
       
        Dim sqlStr As String = ""
        

        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into VaccinationMaster Values('" & txtCode.Text & "','" & txtName.Text & "','" & cboVacType.SelectedIndex & "')"
        Else
            'Update
            sqlStr = "Update VaccinationMaster Set vacCode='" & txtCode.Text & "', vacNAme='" & txtName.Text & "', vacType='" & cboVacType.SelectedIndex & "' Where vacID=" & Val(txtID.Text)
        End If
        
        
        ExecuteQuery_Update(SqlStr)

        
        
        InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
       
       
       

        Dim sqlStr As String = "Select * From VaccinationMaster Where vacCode='" & lstMasters.Text & "'"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("vacID")
            txtCode.Text = myReader("vacCode")
            txtName.Text = myReader("vacName")
            cboVacType.SelectedIndex = myReader("vacType")
        End While
        myReader.Close()
        
        
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From VaccinationMaster Where vacID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        
        
        InitControls()
    End Sub

End Class