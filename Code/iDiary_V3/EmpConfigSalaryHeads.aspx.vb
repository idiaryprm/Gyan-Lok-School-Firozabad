Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ConfigSalaryHeads
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Payroll-1") = False Then
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(31, lstHeads)
        txtAmount.Text = "0.0"
        lblStatus.Text = ""
        lstHeads.Focus()
    End Sub

    Protected Sub lstHeads_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstHeads.SelectedIndexChanged
        Dim HeadID As Integer = 0
        Dim sqlStr As String = ""

        HeadID = FindMasterID(31, lstHeads.Text)
        sqlStr = "Select sum(HeadValue) From ConfigSalaryHeads Where HeadID=" & HeadID

       
       
       

        
        
        
        Dim myValue As Double = 0.0
        Try
            myValue = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception
            myValue = 0
        End Try

        
        

        txtAmount.Text = myValue
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sqlStr As String = "", FinalMessage As String = ""

        If IsNumeric(txtAmount.Text) = False Then
            lblStatus.Text = "Please provide valid input..."
            txtAmount.Focus()
            Exit Sub
        End If

        Dim HeadID As Integer = FindMasterID(31, lstHeads.Text)

       
       
       

        

        sqlStr = "Insert into ConfigSalaryHeads(HeadID, HeadValue, EmpASID, UserID) Values(" & _
        HeadID & "," & Val(txtAmount.Text) & ")"

        
        

        Try
            ExecuteQuery_Update(SqlStr)
            FinalMessage = "Information about " & lstHeads.Text & " saved successfully..."
        Catch ex As Exception
            sqlStr = "Update ConfigSalaryHeads Set " & _
            "HeadValue=" & Val(txtAmount.Text) & _
            " Where HeadID=" & HeadID

            
            
            ExecuteQuery_Update(SqlStr)
            FinalMessage = "Information about " & lstHeads.Text & " updated successfully..."
        End Try
        
        

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub
End Class