Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports System.Drawing

Partial Class Admin_AttendanceReport
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 2
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
        'If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
        '    btnSave.Enabled = False
        'End If



    End Sub

    Private Sub InitControls()
        txtFromDate.Text = Now.Date().ToString("dd/MM/yyyy")
        txtToDate.Text = Now.Date().ToString("dd/MM/yyyy")
        'txtTime.Text = Now.TimeOfDay.Hours & ":" & Now.TimeOfDay.Minutes
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        ' LoadClassSections()
        'cboStudents.Items.Clear()
        'chkAll.Checked = False

        'chkSMS.Checked = False
        'chkEmail.Checked = False
        cboSection.Items.Clear()
        gvAttendance.Visible = False
        lblStatus.Text = ""
    End Sub


    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        If Trim(txtFromDate.Text) = "" Then
            lblStatus.Text = "Please Enter From Date..."
            txtFromDate.Focus()
            Exit Sub
        End If
        If Trim(txtToDate.Text) = "" Then
            lblStatus.Text = "Please Enter To Date..."
            txtToDate.Focus()
            Exit Sub
        End If
        Dim FromDate As Date
        Try
            FromDate = txtFromDate.Text.Substring(6, 4) & "/" & txtFromDate.Text.Substring(3, 2) & "/" & txtFromDate.Text.Substring(0, 2)
        Catch ex As Exception
            lblStatus.Text = "Please Enter valid From Date..."
            txtFromDate.Focus()
            Exit Sub
        End Try
        Dim ToDate As Date
        Try
            ToDate = txtToDate.Text.Substring(6, 4) & "/" & txtToDate.Text.Substring(3, 2) & "/" & txtToDate.Text.Substring(0, 2)
        Catch ex As Exception
            lblStatus.Text = "Please Enter valid To Date..."
            txtToDate.Focus()
            Exit Sub
        End Try
        If FromDate > ToDate Then
            lblStatus.Text = "From date should be less than To date..."
            txtFromDate.Focus()
            Exit Sub
        End If

        If cboSchoolName.Text = "" Then
            lblStatus.Text = "Please choose School..."
            cboSchoolName.Focus()
            Exit Sub
        End If
        If cboClass.Text = "" Then
            lblStatus.Text = "Please choose a class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            lblStatus.Text = "Please choose a section..."
            cboSection.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
        sqlStr = "Select AttDate,RegNo,SNAme,ClassName,SecName,"
        If cboShift.Text = "Morning" Then
            sqlStr &= " IsPresentM,IsPresentE"
        Else
            sqlStr &= " IsPresentM as IsPresentE,IsPresentE"
        End If
        sqlStr &= " , MobNo From vw_Attendance Where SchoolName='" & cboSchoolName.Text & "' and ClassName='" & cboClass.Text & "' and SecName ='" & cboSection.Text & "'   AND AttDate between '" & FromDate.ToString("yyyy-MM-dd") & "' and '" & ToDate.ToString("yyyy-MM-dd") & "'"
        Dim ds As DataSet = ExecuteQuery_DataSet(sqlStr, "att")

        If ds.Tables(0).Rows.Count = 0 Then
            lblStatus.Text = "Attendance of Class: " & cboClass.Text & " From : " & txtFromDate.Text & " to :" & txtToDate.Text & "  not present in Database..."
            btnPrint.Visible = False
            btnExcel.Visible = False
        Else
            SqlDataSource1.SelectCommand = sqlStr
            gvAttendance.Visible = True
            gvAttendance.DataBind()
            lblAbsent.Text = "Total Absent : " & absent
            lblPresent.Text = "Total Present : " & present
            'btnPrint.Visible = True
            btnExcel.Visible = True
        End If
        lblAbsent.Visible = True
        lblPresent.Visible = True
        ReportViewer1.Visible = False

    End Sub

    Dim absent As Integer = 0, present As Integer = 0
    Protected Sub gvAttendance_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAttendance.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim myVal As String = e.Row.Cells(2).Text
            If myVal = "0" Then
                e.Row.Cells(2).Text = "Absent"
                absent = absent + 1
                e.Row.BackColor = Drawing.ColorTranslator.FromHtml("#FF8F8F")
            ElseIf myVal = "1" Then
                e.Row.Cells(2).Text = "Present"
                present = present + 1
                e.Row.BackColor = Drawing.ColorTranslator.FromHtml("#C8FF5C")
            End If
        End If
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim sw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()
        Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
        Dim cell As New TableHeaderCell()
        cell.Text = "From Date : " & txtFromDate.Text.Substring(6, 4) & "/" & txtFromDate.Text.Substring(3, 2) & "/" & txtFromDate.Text.Substring(0, 2) & " To :  " & txtToDate.Text.Substring(6, 4) & "/" & txtToDate.Text.Substring(3, 2) & "/" & txtToDate.Text.Substring(0, 2) & " for " & cboSchoolName.Text & ""
        cell.ColumnSpan = 7
        row.Controls.Add(cell)


        row.BackColor = ColorTranslator.FromHtml("#3AC0F2")
        gvAttendance.HeaderRow.Parent.Controls.AddAt(0, row)
        Dim filename As String = "Attendance_" + DateTime.Now.ToString() + ".xls"

        Page.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        Page.Response.ContentType = "application/vnd.ms-excel"
        Page.Response.Charset = ""
        Page.EnableViewState = False
        frm.Attributes("runat") = "server"
        Controls.Add(frm)
        frm.Controls.Add(gvAttendance)
        frm.RenderControl(hw)
        Response.Write(sw.ToString())
        Response.End()
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboClass.Focus()
    End Sub

    Protected Sub btnReport0_Click(sender As Object, e As EventArgs) Handles btnSummaryReport.Click
        '................................................vikash..........17/06/2016.........................................
        Dim sqlstr As String = ""
        sqlstr = "Select * from Params"
        Dim SchoolName As String = ""
        Dim Address As String = ""
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)

        While myReader.Read
            SchoolName = myReader("SchoolName")
            Address = myReader("SchoolDetails")
        End While
        myReader.Close()
        '.........................................................................................................................
        Dim ds As New DataSet
        sqlstr = "Select * From vw_Attendance Where  AttDate between '" & txtFromDate.Text.Substring(6, 4) & "/" & txtFromDate.Text.Substring(3, 2) & "/" & txtFromDate.Text.Substring(0, 2) & "' and '" & txtToDate.Text.Substring(6, 4) & "/" & txtToDate.Text.Substring(3, 2) & "/" & txtToDate.Text.Substring(0, 2) & "' and SchoolName='" & cboSchoolName.Text & "' and ClassName='" & SQLFixup(cboClass.Text) & "' and SecName='" & SQLFixup(cboSection.Text) & "'"

        ds = ExecuteQuery_DataSet(sqlstr, "tbl")
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report/rptStudentAttendanceSummary.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With
        Dim ClassHeader As String = "Session: " & Request.Cookies("ASName").Value

        Dim params(5) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("ASName", ClassHeader, True)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("ClassName", cboClass.Text, True)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("SecName", cboSection.Text, True)
        params(3) = New Microsoft.Reporting.WebForms.ReportParameter("ClassHeader", ClassHeader, True)
        params(4) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", cboSchoolName.Text, True)
        params(5) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolAddress", Address, True)
        Me.ReportViewer1.LocalReport.SetParameters(params)

        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
        gvAttendance.Visible = False
        lblAbsent.Visible = False
        lblPresent.Visible = False
        btnExcel.Visible = False

    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboSchoolName.Focus()
    End Sub



    Protected Sub gvAttendance_DataBound(sender As Object, e As EventArgs)
        Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
        Dim cell As New TableHeaderCell()
        cell.Text = "From Date : " & txtFromDate.Text.Substring(6, 4) & "/" & txtFromDate.Text.Substring(3, 2) & "/" & txtFromDate.Text.Substring(0, 2) & " To :  " & txtToDate.Text.Substring(6, 4) & "/" & txtToDate.Text.Substring(3, 2) & "/" & txtToDate.Text.Substring(0, 2) & " for " & cboSchoolName.Text & ""
        cell.ColumnSpan = 7
        row.Controls.Add(cell)


        row.BackColor = ColorTranslator.FromHtml("#3AC0F2")
        gvAttendance.HeaderRow.Parent.Controls.AddAt(0, row)
    End Sub

    Protected Sub btnRoster_Click(sender As Object, e As EventArgs) Handles btnRoster.Click
        Dim Sql As String = "Select * from vw_Attendance Where AttDate Between '" & txtFromDate.Text.Substring(3, 2) & "/" & txtFromDate.Text.Substring(0, 2) & "/" & txtFromDate.Text.Substring(6, 4) & _
            "' And '" & txtToDate.Text.Substring(3, 2) & "/" & txtToDate.Text.Substring(0, 2) & "/" & txtToDate.Text.Substring(6, 4) & "' and SchoolName='" & cboSchoolName.SelectedItem.Text & "' and ClassName='" & cboClass.Text & "' and SecName='" & cboSection.Text & "' and StatusName='Active' and ASID='" & Request.Cookies("ASID").Value & "'"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(Sql, "tbl")

        Dim AttDateList As New List(Of String)
        For Each Row As DataRow In ds.Tables(0).Rows
            If AttDateList.Contains(Row("AttDate")) Then
            Else
                AttDateList.Add(Row("AttDate"))
            End If
        Next

        Dim startDate As DateTime = New DateTime(txtFromDate.Text.Substring(6, 4), txtFromDate.Text.Substring(3, 2), txtFromDate.Text.Substring(0, 2))
        Dim EndDate As DateTime = New DateTime(txtToDate.Text.Substring(6, 4), txtToDate.Text.Substring(3, 2), txtToDate.Text.Substring(0, 2))
        Dim CurrD As DateTime = startDate
        Dim DateList As New List(Of String)
        While (CurrD <= EndDate)
            DateList.Add(CurrD)
            CurrD = CurrD.AddDays(1)
        End While

        'Get School ID
        Dim SchoolID As String = FindMasterID(71, cboSchoolName.Text)

        Dim HoliDayDates As New List(Of String)
        Dim sqlStr As String = "Select HolidayDate From Holidays Where HolidayDate between '" & startDate.ToString("yyyy/MM/dd") & "' AND " & _
             "'" & EndDate.ToString("yyyy/MM/dd") & "' and SchoolID='" & SchoolID & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Dim tmpdate As New Date
            tmpdate = CDate(myReader("HolidayDate"))
            HoliDayDates.Add(tmpdate.ToString("yyyy/MM/dd"))
        End While
        myReader.Close()

        Dim SName As New List(Of String)
        sqlStr = "Select SName,SID From vw_Student Where SchoolName='" & cboSchoolName.SelectedItem.Text & "' and ClassName='" & cboClass.Text & "' and SecName='" & cboSection.Text & "' and StatusName='Active' and ASID='" & Request.Cookies("ASID").Value & "'"
        myReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            SName.Add(myReader("Sname") & "-" & myReader("SID"))
        End While
        myReader.Close()


        For i = 0 To HoliDayDates.Count - 1
            If AttDateList.Contains(HoliDayDates.Item(i)) Then
            Else
                AttDateList.Add(HoliDayDates.Item(i))
                For j As Integer = 0 To SName.Count - 1
                    Dim newRow1 As DataRow = ds.Tables(0).NewRow()
                    newRow1("AttDate") = AttDateList.Item(AttDateList.Count - 1)
                    Dim EmpN As String = SName.Item(j).ToString
                    newRow1("SName") = EmpN.Split("-")(0)
                    newRow1("SID") = EmpN.Split("-")(1)
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

        Dim rds As ReportDataSource = New ReportDataSource()

        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        '    End If

        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report/rptAtt.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With

        Dim SchoolName As String = FindSchoolDetails(1) & ", " & FindSchoolDetails(2)
        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", SchoolName, True)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("DateRange", startDate.ToString("dd/MM/yyyy") & "-" & EndDate.ToString("dd/MM/yyyy"), True)
        Me.ReportViewer1.LocalReport.SetParameters(params)

        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub
End Class