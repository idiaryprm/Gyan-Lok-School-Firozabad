Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class Department_Payroll
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
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
        LoadMasterInfo(25, lstMasters)
        txtName.Focus()
        chkDefault.Checked = False
        btnRemove.Visible = False
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Wrong Input');", True)
            txtName.Focus()
            Exit Sub
        End If
        Dim FinalMessage = ""
        If Val(txtID.Text) = 0 Then
            If CheckDepartment() = True Then
                FinalMessage = "Department is Already Exists..."
                txtName.Focus()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
                Exit Sub
            End If
        End If

       
       
       
        Dim sqlStr As String = ""
        
        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update Department_Payroll Set IsDefault=0"
            
            
            ExecuteQuery_Update(SqlStr)
        End If
        If Val(txtID.Text) = 0 Then
            'Insert
            sqlStr = "Insert into Department_Payroll Values('" & txtName.Text & "','" & Request.Cookies("UserID").Value & "','" & IsDefault & "')"
            FinalMessage = "New Department added successfully..."
        Else
            'Update
            sqlStr = "Update Department_Payroll Set DeptName='" & txtName.Text & "',CreatedBy='" & Request.Cookies("UserID").Value & "',IsDefault=" & IsDefault & " Where DeptID=" & Val(txtID.Text)
            FinalMessage = "Department Updated successfully..."
        End If
        
        
        ExecuteQuery_Update(SqlStr)

        
        
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
        txtID.Text = FindMasterID(25, lstMasters.Text)
        txtName.Text = lstMasters.Text

       
       
       

        Dim sqlStr As String = "Select * From Department_payroll Where DeptName='" & lstMasters.Text & "'"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
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
        
        

        'btnRemove.Visible = True
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From Department_payroll Where DeptID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        
        
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record Removed Successfully...');", True)
    End Sub
    Private Function CheckDepartment()
       
       
       

        Dim sqlStr As String = "Select Count(*) From Department_payroll Where DeptName='" & txtName.Text & "'"
        
        
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
        
        
        Return rv
    End Function
End Class