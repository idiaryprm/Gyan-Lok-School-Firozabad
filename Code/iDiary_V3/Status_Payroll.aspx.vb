Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class Status_Payroll
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
        LoadMasterInfo(29, lstMasters)
        txtName.Focus()
        btnRemove.Visible = False
        chkDefault.Checked = False
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Wrong Input');", True)
            txtName.Focus()
            Exit Sub
        End If
        Dim FinalMessage = ""
        If Val(txtID.Text) = 0 Then
            If CheckStatus() = True Then
                FinalMessage = "Status is Already Exists..."
                txtName.Focus()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
                Exit Sub
            End If
        End If
       
       
       
        Dim sqlStr As String = ""
        
        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update Status_Payroll Set IsDefault=0"
            
            
            ExecuteQuery_Update(SqlStr)
        End If
        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into Status_Payroll Values('" & txtName.Text & "','" & Request.Cookies("UserID").Value & "','" & IsDefault & "')"
            FinalMessage = "New Status added successfully..."
        Else
            'Update
            sqlStr = "Update Status_Payroll Set StatusName='" & txtName.Text & "',CreatedBy='" & Request.Cookies("UserID").Value & "',IsDefault=" & IsDefault & " Where StatusID=" & Val(txtID.Text)
            FinalMessage = "Status updated successfully..."
        End If
        
        
        ExecuteQuery_Update(SqlStr)

        
        
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
        txtID.Text = FindMasterID(29, lstMasters.Text)
        txtName.Text = lstMasters.Text

       
       
       

        Dim sqlStr As String = "Select * From Status_Payroll Where StatusName='" & lstMasters.Text & "'"
        
        
        
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
       
       
       

        Dim sqlStr As String = "Delete From Status_Payroll Where StatusID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        
        
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record Removed Successfully...');", True)
    End Sub
    Private Function CheckStatus()
       
       
       

        Dim sqlStr As String = "Select Count(*) From Status_Payroll Where StatusName='" & txtName.Text & "'"
        
        
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
        
        
        Return rv
    End Function
End Class