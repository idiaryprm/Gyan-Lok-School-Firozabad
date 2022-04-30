Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class BankStudent
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Or Request.Cookies("UType").Value.ToString.Contains("Student") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
       
        
       

        
        Dim sqlStr As String = ""
        Dim FinalMessage = ""
        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update Bank Set IsDefault=0"
            
            
            ExecuteQuery_Update(SqlStr)
        End If
        If Val(txtID.Text) = 0 Then    'Insert Command
            sqlStr = "Insert into Bank Values('" & txtBankName.Text & "','U', " & IsDefault & ")"
            FinalMessage = "New Bank added successfully..."
        Else    'Update Command
            sqlStr = "Update Bank Set BankName='" & txtBankName.Text & "', IsDefault=" & IsDefault & " Where BankID=" & Val(txtID.Text)
            FinalMessage = "Bank updated successfully..."
        End If


        
        
        ExecuteQuery_Update(SqlStr)

        
        

        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(22, lstBank)
        txtID.Text = 0
        txtBankName.Text = ""
        chkDefault.Checked = False
        txtBankName.Focus()

    End Sub

    Protected Sub lstBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBank.SelectedIndexChanged

        'txtID.Text = FindMasterID(22, lstBank.Text)
        'txtBankName.Text = lstBank.Text
       
       
       

        Dim sqlStr As String = "Select * From Bank Where BankName='" & lstBank.Text & "'"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("BankID")
            txtBankName.Text = myReader("BankName")
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

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""

        If Val(txtID.Text) = 0 Then    'Error
            FinalMessage = "Select a Bank to remove..."
        Else
            sqlStr = "Delete Bank Where BankID=" & Val(txtID.Text)
            FinalMessage = "Bank removed successfully..."
        End If

       
        
       

        
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)


    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

End Class