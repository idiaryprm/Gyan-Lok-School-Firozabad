Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports iDiary_V3.iDiary.CLS_idiary

Public Class LibraryIssueReturnReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Library") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("../AccessDenied.aspx", False)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        'txtFromDay.Text = Now.Date.Day
        'txtFromMonth.Text = Now.Date.Month
        'txtFromYear.Text = Now.Date.Year
        'txtToDay.Text = Now.Date.Day
        'txtToMonth.Text = Now.Date.Month
        'txtToYear.Text = Now.Date.Year
        ReportViewer1.Visible = False
        lblStatus.Text = ""

        'txtFromDay.Focus()
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        'ProcessReport(cboType.SelectedIndex)
        If txtDateFrom.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Date From');", True)
            txtDateFrom.Focus()
            Exit Sub
        End If
        If txtDateTo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Date To');", True)
            txtDateTo.Focus()
            Exit Sub
        End If
        If cboType.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Type');", True)
            cboType.Focus()
            Exit Sub
        End If
    
        PrepareReport(cboType.SelectedIndex)
        
    End Sub
    Private Sub PrepareReport(ByVal TransType As Integer)
       
        Dim sqlStr As String = ""
        Dim sqlStr1 As String = ""
        'sqlStr = "Delete From rptLibraryTransaction"
        'ExecuteQuery_Update(SqlStr)

        'sqlStr = "Insert into rptLibraryTransaction (AccNo,BookTitle,RegNo,SName,ClassName,SecName,TransDate) "
        If TransType = 1 Then 'Issue
            sqlStr &= "Select AccNo, BookTitle, RegNo, SName, ClassName, SecName,IssueDate From vw_BookTransactStudent Where " & _
            "IssueDate Between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "'"
            If rbBook.Checked = True Then
                sqlStr += " And AccNo like 'B%'"
            ElseIf rbMagazine.Checked = True Then
                sqlStr += " And AccNo like 'M%'"
            Else
                sqlStr += " And AccNo like 'D%'"
            End If

        ElseIf TransType = 2 Then  'Return

            sqlStr &= "Select AccNo, BookTitle, RegNo, SName, ClassName, SecName,ActualReturnDate  From vw_BookTransactStudent Where " & _
            "ActualReturnDate Between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "'"
            If rbBook.Checked = True Then
                sqlStr += " And AccNo like 'B%'"
            ElseIf rbMagazine.Checked = True Then
                sqlStr += " And AccNo like 'M%'"
            Else
                sqlStr += " And AccNo like 'D%'"
            End If
        End If

        Dim ds1, ds2 As New DataSet
        ds1 = ExecuteQuery_DataSet(sqlStr, "tbl")
        'For teacher
        'sqlStr = "Insert into rptLibraryTransaction (AccNo,BookTitle,EmployeeCode,EmployeeName,DepartmentName,DesignationName,TransDate) "
        If TransType = 1 Then 'Issue
            sqlStr = "Select AccNo, BookTitle, EmpCode, EmpName, DeptName, DesgName,IssueDate From vw_BookTransactEmployee Where " & _
            "IssueDate Between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "'"
            If rbBook.Checked = True Then
                sqlStr += " And AccNo like 'B%'"
            ElseIf rbMagazine.Checked = True Then
                sqlStr += " And AccNo like 'M%'"
            Else
                sqlStr += " And AccNo like 'D%'"
            End If

        ElseIf TransType = 2 Then  'Return

            sqlStr = "Select AccNo, BookTitle, EmpCode, EmpName, DeptName, DesgName,ActualReturnDate  From vw_BookTransactEmployee Where " & _
            "ActualReturnDate Between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "'"
            If rbBook.Checked = True Then
                sqlStr += " And AccNo like 'B%'"
            ElseIf rbMagazine.Checked = True Then
                sqlStr += " And AccNo like 'M%'"
            Else
                sqlStr += " And AccNo like 'D%'"
            End If
        End If

        ds2 = ExecuteQuery_DataSet(sqlStr, "tbl")

        Dim rds1 As ReportDataSource = New ReportDataSource()
        Dim rds2 As ReportDataSource = New ReportDataSource()
        rds1.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds1.Value = ds1.Tables(0)
        rds2.Name = "DataSet2" ' Change to what you will be using when creating an objectdatasource
        rds2.Value = ds2.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report\rptLibraryTransaction.rdlc"
            .LocalReport.DataSources.Add(rds1)
            .LocalReport.DataSources.Add(rds2)
        End With
        'Dim FromDate1 As String = Val(txtFromDay.Text) & "/" & Val(txtFromMonth.Text) & "/" & Val(txtFromYear.Text)
        'Dim ToDate1 As String = Val(txtToDay.Text) & "/" & Val(txtToMonth.Text) & "/" & Val(txtToYear.Text)

        Dim myHeaderText As String = ""
        If cboType.SelectedIndex = 1 Then
            myHeaderText = "Issue Report "
        ElseIf cboType.SelectedIndex = 2 Then
            myHeaderText = "Return Report "
        End If
        myHeaderText &= txtDateFrom.Text & "-" & txtDateTo.Text
        Dim params(0) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("myHeader", myHeaderText, True)
        ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()

    End Sub
End Class