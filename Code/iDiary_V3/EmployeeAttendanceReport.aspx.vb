Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports System.IO
Imports Microsoft.Reporting.WebForms

Partial Class EmployeeAttendanceReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        Else
            Dim printScript As String = "function PrintGridView() { var gridInsideDiv = document.getElementById('gvDiv');" & _
                    " var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');" & _
                    " printWindow.document.write(gridInsideDiv.innerHTML);printWindow.document.close();printWindow.focus();" & _
                    " printWindow.print();printWindow.close();}"
            Me.ClientScript.RegisterStartupScript(Page.[GetType](), "PrintGridView", printScript.ToString(), True)
            btnPrint.Attributes.Add("onclick", "PrintGridView();")
        End If
    End Sub

    Private Sub InitControls()
        txtDateFrom.Text = Now.Date().ToString("dd/MM/yyyy")
        txtDateTo.Text = Now.Date().ToString("dd/MM/yyyy")
        LoadMasterInfo(30, cboEmpCat)
        LoadMasterInfo(25, cboDepartment)
        cboEmpCat.Items.Add("ALL")
        cboDepartment.Items.Add("ALL")
        LoadMasterInfo(29, cboStatus)
        ' gvAttendance.Visible = False
        lblStatus.Text = ""
    End Sub

    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        lblStatus.Text = ""
        If Trim(txtDateFrom.Text) = "" Then
            lblStatus.Text = "Please Enter From Date..."
            txtDateFrom.Focus()
            Exit Sub
        End If
        If Trim(txtDateTo.Text) = "" Then
            lblStatus.Text = "Please Enter To Date..."
            txtDateTo.Focus()
            Exit Sub
        End If
        Dim dateFrom As Date = txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2)
        Dim dateTo As Date = txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2)

        If dateFrom > dateTo Then
            lblStatus.Text = "from date cant be greater than to date..."
            Exit Sub
        End If
        If cboEmpCat.Text = "" Then
            lblStatus.Text = "Please choose a category..."
            cboEmpCat.Focus()
            Exit Sub
        End If
        If cboStatus.Text = "" Then
            lblStatus.Text = "Please choose a status..."
            cboStatus.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
        Dim ds As New DataSet
        Select Case cboReportType.Text
            Case "Attendance Summary DayWise"
                sqlStr = "SELECT * FROM [vw_employee_Attendance] Where StatusName ='" & cboStatus.Text & "' "
                If UCase(cboEmpCat.Text) <> "ALL" Then
                    sqlStr += " and EmpCatName='" & cboEmpCat.Text & "' "
                End If
                If UCase(cboDepartment.Text) <> "ALL" Then
                    sqlStr += " and DeptName='" & cboDepartment.Text & "' "
                End If
                sqlStr += " AND AttDate between '" & dateFrom.ToString("yyyy/MM/dd") & "' AND '" & dateTo.ToString("yyyy/MM/dd") & "' order by EmpName"
                ds = ExecuteQuery_DataSet(sqlStr, "att")
                PrepareReport(ds, "rptEmpAttendance.rdlc")

            Case "Attendance Roster"
                PrepareReport(PrepareRoster(), "rptEmpAtt.rdlc")

            Case "Attendance In/Out Summary"
                PrepareReport(PrepareRoster(), "rptEmpAttInOut.rdlc")

            Case "Attendance Summary"
                sqlStr = "SELECT * FROM [vw_employee_Attendance] Where StatusName ='" & cboStatus.Text & "' "
                If UCase(cboEmpCat.Text) <> "ALL" Then
                    sqlStr += " and EmpCatName='" & cboEmpCat.Text & "' "
                End If
                If UCase(cboDepartment.Text) <> "ALL" Then
                    sqlStr += " and DeptName='" & cboDepartment.Text & "' "
                End If
                sqlStr += " AND AttDate between '" & dateFrom.ToString("yyyy/MM/dd") & "' AND '" & dateTo.ToString("yyyy/MM/dd") & "' order by EmpName"
                ds = ExecuteQuery_DataSet(sqlStr, "att")
                PrepareReport(ds, "rptEmpAttSummary.rdlc")
                '  PrepareReport(PrepareRoster(), "rptEmpAttSummary.rdlc")

        End Select

    End Sub


    Dim absent As Integer = 0, present As Integer = 0, HalfDay As Integer = 0

    Protected Sub btnReportSummary_Click(sender As Object, e As EventArgs) Handles btnReportSummary.Click
    
        Dim dateFrom As Date = txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2)
        Dim dateTo As Date = txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2)


        Dim sqlStr As String = ""
        sqlStr = "drop table rptEmpLeaveSummary"


        ExecuteQuery_Update(SqlStr)

        sqlStr = "Select LeaveID,LeaveName from LeaveMaster order by LeaveID"


        Dim lstLeaveID As New List(Of String)
        Dim lstLeaveName As New List(Of String)
        Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myreader.Read
            lstLeaveID.Add(myreader(0))
            lstLeaveName.Add(myreader(1))
        End While
        myreader.Close()

        Dim lstLeaveCount As New List(Of String)
        Dim lstEmpID As New List(Of String)
        sqlStr = "Select COUNT(leavename) LeaveCount,LeaveName,empid  from vw_employee_Attendance where AttDate between '" & dateFrom & "' and '" & dateTo & "' and leavename is not null group by empid,LeaveName order by EmpID"


        myreader = ExecuteQuery_ExecuteReader(sqlStr)

        While myreader.Read
            For i As Integer = 0 To lstLeaveID.Count - 1
                If lstLeaveName.Item(i) = myreader(1) Then
                    lstLeaveCount.Add(myreader(0))
                    lstEmpID.Add(myreader(2))
                End If
            Next
        End While



        myreader.Close()
        sqlStr = "CREATE TABLE [dbo].[rptEmpLeaveSummary]([eaSummaryID] [int] IDENTITY(1,1) NOT NULL,[EmpID] [int] NULL,[EmpName] [nvarchar](150) NULL,[EmpCode] [nvarchar](50) NULL,[WorkDays] [nchar](10) NULL,"

        For ii As Integer = 0 To lstLeaveName.Count - 1
            sqlStr &= "[" & lstLeaveName.Item(ii) & "]" & "[nchar](10) NULL,"
        Next

        'For k As Integer = 0 To lstLeaveID.Count - 1
        '    sqlStr &= "LeaveType" & k + 1 & "Count ='" & lstLeaveCount.Item(k) & "'"
        'Next
        sqlStr &= " CONSTRAINT [PK_rptEmpLeaveSummary] PRIMARY KEY CLUSTERED (  [eaSummaryID] Asc )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]"


        ExecuteQuery_Update(SqlStr)

        sqlStr = "insert into rptEmpLeaveSummary(empid,empcode,empname) select distinct empid,empcode,empname from EmployeeMaster "


        ExecuteQuery_Update(SqlStr)

        Dim tmpCount As Integer = 0

        For ii As Integer = 0 To lstEmpID.Count - 1
            For jj As Integer = 0 To lstLeaveName.Count - 1
                sqlStr = "Select Count(leaveName) from vw_Employee_Attendance where leaveName='" & lstLeaveName.Item(jj) & "' and empID='" & lstEmpID.Item(ii) & "' And  AttDate between '" & dateFrom & "' and '" & dateTo & "' "


                Try
                    tmpCount = ExecuteQuery_ExecuteScalar(SqlStr)
                Catch ex As Exception
                    tmpCount = 0
                End Try
                sqlStr = "Update rptEmpLeaveSummary Set [" & lstLeaveName.Item(jj) & "] = '" & tmpCount & "' where empID='" & lstEmpID.Item(ii) & "'"


                ExecuteQuery_Update(SqlStr)
            Next
        Next

        sqlStr = "Select Count(distinct attdate) from vw_Employee_Attendance where  AttDate between '" & dateFrom & "' and '" & dateTo & "' "


        Try
            tmpCount = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception
            tmpCount = 0
        End Try
        sqlStr = "Update rptEmpLeaveSummary Set WorkDays = '" & tmpCount & "'"


        ExecuteQuery_Update(SqlStr)

    End Sub

    Private Function PrepareRoster() As DataSet
        Dim Sql As String = "Select * from vw_Employee_Attendance Where StatusName='" & cboStatus.Text & "' and AttDate Between '" & txtDateFrom.Text.Substring(6, 4) & "-" & txtDateFrom.Text.Substring(3, 2) & "-" & txtDateFrom.Text.Substring(0, 2) & _
           "' And '" & txtDateTo.Text.Substring(6, 4) & "-" & txtDateTo.Text.Substring(3, 2) & "-" & txtDateTo.Text.Substring(0, 2) & "'"
        If cboEmpCat.Text <> "ALL" Then
            Sql += " and EmpCatName='" & cboEmpCat.Text & "' "
        End If
        If cboDepartment.Text <> "ALL" Then
            Sql += " and DeptName ='" & cboDepartment.Text & "'"
        End If

        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(Sql, "tbl")
        Dim inTime As String = "", outTime As String = ""

        Dim AttDateList As New List(Of String)
        For Each Row As DataRow In ds.Tables(0).Rows
            Try
                inTime = Row("InTime")
                Row("InTime") = inTime.Substring(0, 5)
            Catch ex As Exception
                inTime = ""
            End Try
            Try
                outTime = Row("OutTime")
                Row("OutTime") = outTime.Substring(0, 5)
            Catch ex As Exception
                outTime = ""
            End Try
            If AttDateList.Contains(Row("AttDate")) Then
            Else
                AttDateList.Add(Row("AttDate"))
            End If
        Next

        Dim startDate As DateTime = New DateTime(txtDateFrom.Text.Substring(6, 4), txtDateFrom.Text.Substring(3, 2), txtDateFrom.Text.Substring(0, 2))
        Dim EndDate As DateTime = New DateTime(txtDateTo.Text.Substring(6, 4), txtDateTo.Text.Substring(3, 2), txtDateTo.Text.Substring(0, 2))
        Dim CurrD As DateTime = startDate
        Dim DateList As New List(Of String)
        While (CurrD <= EndDate)
            DateList.Add(CurrD)
            CurrD = CurrD.AddDays(1)
        End While

        Dim HoliDayDates As New List(Of String)
        Dim sqlStr As String = "Select HolidayDate From PayrollHolidays Where HolidayDate between '" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "/" & txtDateFrom.Text.Substring(6, 4) & "' AND " & _
             "'" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "/" & txtDateTo.Text.Substring(6, 4) & "'"
        If cboEmpCat.Text <> "ALL" Then
            Dim EmpCat As Int16 = 0
            If cboEmpCat.SelectedIndex = 1 Then
                EmpCat = 0
            Else
                EmpCat = 1
            End If
            sqlStr += " AND EmpCategory='" & EmpCat & "'"
        End If
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Dim tmpdate As New Date
            tmpdate = CDate(myReader("HolidayDate"))
            HoliDayDates.Add(tmpdate.ToString("yyyy/MM/dd"))
        End While
        myReader.Close()

        Dim EmpName As New List(Of String)
        sqlStr = "Select EmpName,Empcode From vw_Employees Where StatusName='" & cboStatus.Text & "' "
        If cboEmpCat.Text <> "ALL" Then
            sqlStr += " AND EmpCatName='" & cboEmpCat.Text & "' "
        End If
        'If cboDesignation.Text <> "ALL" Then
        '    sqlStr += " and DesgName ='" & cboDesignation.Text & "'"
        'End If
        myReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            EmpName.Add(myReader("EmpName") & "-" & myReader("Empcode"))
        End While
        myReader.Close()

        For i = 0 To HoliDayDates.Count - 1
            If AttDateList.Contains(HoliDayDates.Item(i)) Then
            Else
                AttDateList.Add(HoliDayDates.Item(i))
                For j As Integer = 0 To EmpName.Count - 1
                    Dim newRow1 As DataRow = ds.Tables(0).NewRow()
                    newRow1("AttDate") = AttDateList.Item(AttDateList.Count - 1)
                    Dim EmpN As String = EmpName.Item(j).ToString
                    newRow1("EmpName") = EmpN.Split("-")(0)
                    newRow1("EmpCode") = EmpN.Split("-")(1)
                    If CDate(HoliDayDates.Item(i)).DayOfWeek.ToString = DayOfWeek.Sunday.ToString() Then
                        newRow1("Att") = "-2"
                    Else
                        newRow1("Att") = "-1"
                    End If
                    ds.Tables(0).Rows.Add(newRow1)
                Next
            End If
        Next

        For i As Integer = 0 To DateList.Count - 1
            If AttDateList.Contains(DateList.Item(i)) Then
            Else
                AttDateList.Add(DateList.Item(i))
                Dim newRow1 As DataRow = ds.Tables(0).NewRow()
                newRow1("AttDate") = AttDateList.Item(AttDateList.Count - 1)
                ds.Tables(0).Rows.Add(newRow1)
            End If
        Next

        Return ds
    End Function

    Private Sub PrepareReport(ByVal ds As DataSet, rdlcName As String)
        Dim rds As ReportDataSource = New ReportDataSource()

        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        '    End If

        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report\" & rdlcName
            .LocalReport.DataSources.Add(rds)
        End With

        Dim SchoolName As String = FindSchoolDetails(1) & ", " & FindSchoolDetails(2)
        Dim rptHeader As String = "for date : " & txtDateFrom.Text & " to " & txtDateTo.Text
        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", SchoolName, True)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("rptHeader", rptHeader, True)
        Me.ReportViewer1.LocalReport.SetParameters(params)

        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()

        If ds.Tables(0).Rows.Count = 0 Then
            lblStatus.Text = "Attendance of : " & cboEmpCat.Text & " for given date not present in Database..."
            btnPrint.Visible = False
            btnExcel.Visible = False
        Else
            btnExcel.Visible = True
        End If
    End Sub

End Class
