Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Bank
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
        If Val(txtID.Text) = 0 Then
            If CheckBank() = True Then
                FinalMessage = "Bank is Already Exists..."
                txtBankName.Focus()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
                Exit Sub
            End If
        End If

        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update Bank Set IsDefault=0"
            ExecuteQuery_Update(sqlStr)
        End If
        If Val(txtID.Text) = 0 Then    'Insert Command
            sqlStr = "Insert into Bank Values('" & txtBankName.Text & "','" & Request.Cookies("USerID").Value & "', " & IsDefault & ")"
            FinalMessage = "New Bank added successfully..."
        Else    'Update Command
            sqlStr = "Update Bank Set BankName='" & txtBankName.Text & "', IsDefault=" & IsDefault & " Where BankID=" & Val(txtID.Text)
            FinalMessage = "Bank updated successfully..."
        End If

     
        ExecuteQuery_Update(sqlStr)

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
        btnRemove.Visible = False
    End Sub

    Protected Sub lstBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBank.SelectedIndexChanged

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

        ExecuteQuery_Update(sqlStr)
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)


    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub
    Private Function CheckBank()
      
        Dim sqlStr As String = "Select Count(*) From Bank Where BankName='" & txtBankName.Text & "'"
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
     
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
        Return rv
    End Function
End Class
