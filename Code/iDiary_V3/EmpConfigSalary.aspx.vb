Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ConfigEmpSalary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = True Or Request.Cookies("UType").Value.ToString.Contains("Payroll-1") = True Then
            btnSave.Enabled = True
        End If
    End Sub

    Private Sub InitControls()

        LoadMasterInfo(30, cboEmpCat)
        lstEmployees.Items.Clear()
        Try
            LoadEmployees(2, cboEmpCat.Text, lstEmployees)
        Catch ex As Exception

        End Try
        LoadMasterInfo(31, lstSalaryHeads)
        optFixed.Checked = False
        optPercent.Checked = False
        txtAmount.Text = "0"
        lblStatus.Text = ""

        cboEmpCat.Focus()
    End Sub

    Protected Sub lstSalaryHeads_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSalaryHeads.SelectedIndexChanged

        'Get Information From DB about Existing Records
        If lstEmployees.SelectedIndex < 0 Then
            lblStatus.Text = "First Select an Employee to continue..."
            lstEmployees.Focus()
            Exit Sub
        End If

        optPercent.Checked = True
        txtAmount.Text = "0"

        Dim EmpID As Integer = FindEmployeeID(2, cboEmpCat.Text, lstEmployees.Text)
        Dim HeadID As Integer = FindMasterID(31, lstSalaryHeads.Text)

        Dim sqlStr As String = ""
        Dim myCount As Integer = 0

       
        
       

        

        sqlStr = "Select * From ConfigEmpSalary Where EmpID=" & EmpID & " AND HeadID=" & HeadID
        
        

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            If myReader("CalcType") = 1 Then    'Fixed
                optPercent.Checked = False
                optFixed.Checked = True
                txtAmount.Text = myReader("FixedAmt")
            Else 'Percent
                optPercent.Checked = True
                optFixed.Checked = False
                txtAmount.Text = "0"
            End If
        End While
        myReader.Close()

        
        

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim sqlStr As String = ""

        Dim EmpID As Integer = FindEmployeeID(2, cboEmpCat.Text, lstEmployees.Text)
        Dim HeadID As Integer = FindMasterID(31, lstSalaryHeads.Text)

        Dim CalcType As Integer = 0, Amount As Double = 0
        If optFixed.Checked = True Then
            CalcType = 1
            Amount = Val(txtAmount.Text)
        End If

        If optPercent.Checked = True Then
            CalcType = 2
            Amount = 0
        End If

       
        
       

        sqlStr = "Insert into ConfigEmpSalary Values(" & _
        EmpID & "," & _
        HeadID & "," & _
        CalcType & "," & _
        Amount & ")"

        

        
        
        Try
            ExecuteQuery_Update(SqlStr)
        Catch ex As Exception
            sqlStr = "Update ConfigEmpSalary Set " & _
            "CalcType=" & CalcType & "," & _
            "FixedAmt=" & Amount & _
            " Where EmpID=" & EmpID & " AND HeadID=" & HeadID

            
            
            ExecuteQuery_Update(SqlStr)
        End Try
        
        

        Dim TempEmpCat As String = cboEmpCat.Text
        Dim TempName As String = lstEmployees.Text
        InitControls()
        cboEmpCat.Text = TempEmpCat

        LoadEmployees(2, cboEmpCat.Text, lstEmployees)
        lstEmployees.Text = TempName
    End Sub

    Protected Sub cboEmpCat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmpCat.SelectedIndexChanged
        LoadEmployees(2, cboEmpCat.Text, lstEmployees)
    End Sub

    Protected Sub lstEmployees_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstEmployees.SelectedIndexChanged
        HighlightColors()
    End Sub

    Private Sub HighlightColors()

        Dim EmpID As Integer = FindEmployeeID(2, cboEmpCat.Text, lstEmployees.Text)
        Dim i As Integer = 0
        Dim sqlStr As String = ""

       
        
       

        

        For i = 0 To lstSalaryHeads.Items.Count - 1

            Dim HeadID As Integer = FindMasterID(31, lstSalaryHeads.Items(i).Text)
            Dim myCount As Integer = 0

            sqlStr = "Select Count(*) From ConfigEmpSalary Where EmpID=" & EmpID & " AND HeadID=" & HeadID
            
            

            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                rv = 0
            End Try

            If rv <= 0 Then
                lstSalaryHeads.Items(i).Attributes("BackColor") = "Red"
            End If
        Next

        
        

    End Sub
End Class