Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class GradePayMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Or Request.Cookies("UType").Value.ToString.Contains("Student") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""
        If Val(txtID.Text) = 0 Then
            If CheckGradePay() = True Then
                FinalMessage = "Grade Pay is Already Exists..."
                txtAGPName.Focus()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
                Exit Sub
            End If
        End If
       
        
       
        
        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update GradePay Set IsDefault=0"
            
            
            ExecuteQuery_Update(SqlStr)
        End If

        If Val(txtID.Text) = 0 Then    'Insert Command
            sqlStr = "Insert into GradePay Values('" & txtAGPName.Text & "','" & Request.Cookies("UserID").Value & "','" & IsDefault & "')"
            FinalMessage = "New Grade Pay added successfully..."
        Else    'Update Command
            sqlStr = "Update GradePay Set AGPName='" & txtAGPName.Text & "',CreatedBy='" & Request.Cookies("UserID").Value & "',IsDefault=" & IsDefault & " Where AGPID=" & Val(txtID.Text)
            FinalMessage = "Grade Pay updated successfully..."
        End If
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        InitControls()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()

        LoadMasterInfo(21, lstAGP)
        txtID.Text = 0
        txtAGPName.Text = ""
        txtAGPName.Focus()
        btnRemove.Visible = False
        chkDefault.Checked = False

    End Sub

    Protected Sub lstAGP_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstAGP.SelectedIndexChanged

        txtID.Text = FindMasterID(21, lstAGP.Text)
        txtAGPName.Text = lstAGP.Text

       
       
       

        Dim sqlStr As String = "Select * From GradePay Where AGPName='" & lstAGP.Text & "'"
        
        
        
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
        
        

        btnRemove.Visible = True
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""

        If Val(txtID.Text) = 0 Then    'Error
            FinalMessage = "Select a AGP to remove..."
        Else
            sqlStr = "Delete GradePay Where AGPID=" & Val(txtID.Text)
            FinalMessage = "AGP removed successfully..."
        End If

       
        
       

        
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub
    Private Function CheckGradePay()
       
       
       

        Dim sqlStr As String = "Select Count(*) From GradePay Where AGPName='" & txtAGPName.Text & "'"
        
        
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
        
        
        Return rv
    End Function
End Class