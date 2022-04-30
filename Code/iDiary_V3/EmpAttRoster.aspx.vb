Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports System.IO

Partial Class EmpAttRoster
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub
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
        LoadMasterInfo(29, cboStatus)
        gvAttendance.Visible = False
        lblStatus.Text = ""
    End Sub

    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        gvAttendance.Visible = True
        'GridView1.Visible = False

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
        sqlStr = "SELECT [EmpCode], [EmpName], [AttDate], [Att], [LeaveName] FROM [vw_employee_Attendance] Where EmpCatName='" & cboEmpCat.Text & "' and StatusName ='" & cboStatus.Text & "'   AND AttDate between '" & dateFrom & "' AND '" & dateTo & "'"
        Dim ds As DataSet = ExecuteQuery_DataSet(sqlStr, "att")

        If ds.Tables(0).Rows.Count = 0 Then
            lblStatus.Text = "Attendance of : " & cboEmpCat.Text & " for given date not present in Database..."
            btnPrint.Visible = False
            btnExcel.Visible = False
            lblPresent.Visible = False
            lblAbsent.Visible = False
        Else
            'SqlDataSource1.SelectCommand = sqlStr
            gvAttendance.Visible = True
            gvAttendance.DataBind()
            lblPresent.Visible = True
            lblAbsent.Visible = True
            lblAbsent.Text = "Total Absent : " & absent
            lblPresent.Text = "Total Present : " & present
            'btnPrint.Visible = True
            btnExcel.Visible = True
        End If
    End Sub

    Dim absent As Integer = 0, present As Integer = 0
    Protected Sub gvAttendance_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAttendance.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim myVal As String = e.Row.Cells(3).Text
            If myVal = "0" Then
                e.Row.Cells(3).Text = "Absent"
                absent = absent + 1
            ElseIf myVal = "1" Then
                e.Row.Cells(3).Text = "Present"
                present = present + 1
            End If
            If e.Row.Cells(4).Text = "&nbsp;" Then
                e.Row.Cells(4).Text = "Pending"
            End If
        End If
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim sw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

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


    'Protected Sub btnReportSummary_Click(sender As Object, e As EventArgs) Handles btnReportSummary.Click
    '    gvAttendance.Visible = False
    '    GridView1.Visible = True

    '    Dim dateFrom As Date = txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2)
    '    Dim dateTo As Date = txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2)


    '   
    '    
    '   

    '    

    '    Dim sqlStr As String = ""
    '    sqlStr = "drop table rptEmpLeaveSummary"
    '    
    '    
    '    ExecuteQuery_Update(SqlStr)

    '    sqlStr = "Select LeaveID,LeaveName from LeaveMaster order by LeaveID"
    '    
    '    
    '    Dim lstLeaveID As New List(Of String)
    '    Dim lstLeaveName As New List(Of String)
    '    Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    While myreader.Read
    '        lstLeaveID.Add(myreader(0))
    '        lstLeaveName.Add(myreader(1))
    '    End While
    '    myreader.Close()

    '    Dim lstLeaveCount As New List(Of String)
    '    Dim lstEmpID As New List(Of String)
    '    sqlStr = "Select COUNT(leavename) LeaveCount,LeaveName,empid  from vw_employee_Attendance where AttDate between '" & dateFrom & "' and '" & dateTo & "' and leavename is not null group by empid,LeaveName order by EmpID"
    '    
    '    
    '    myreader = ExecuteQuery_ExecuteReader(sqlStr)

    '    While myreader.Read
    '        For i As Integer = 0 To lstLeaveID.Count - 1
    '            If lstLeaveName.Item(i) = myreader(1) Then
    '                lstLeaveCount.Add(myreader(0))
    '                lstEmpID.Add(myreader(2))
    '            End If
    '        Next
    '    End While



    '    myreader.Close()
    '    sqlStr = "CREATE TABLE [dbo].[rptEmpLeaveSummary]([eaSummaryID] [int] IDENTITY(1,1) NOT NULL,[EmpID] [int] NULL,[EmpName] [nvarchar](150) NULL,[EmpCode] [nvarchar](50) NULL,[WorkDays] [nchar](10) NULL,"

    '    For ii As Integer = 0 To lstLeaveName.Count - 1
    '        sqlStr &= "[" & lstLeaveName.Item(ii) & "]" & "[nchar](10) NULL,"
    '    Next

    '    'For k As Integer = 0 To lstLeaveID.Count - 1
    '    '    sqlStr &= "LeaveType" & k + 1 & "Count ='" & lstLeaveCount.Item(k) & "'"
    '    'Next
    '    sqlStr &= " CONSTRAINT [PK_rptEmpLeaveSummary] PRIMARY KEY CLUSTERED (  [eaSummaryID] Asc )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]"
    '    
    '    
    '    ExecuteQuery_Update(SqlStr)

    '    sqlStr = "insert into rptEmpLeaveSummary(empid,empcode,empname) select distinct empid,empcode,empname from EmployeeMaster "
    '    
    '    
    '    ExecuteQuery_Update(SqlStr)

    '    Dim tmpCount As Integer = 0

    '    For ii As Integer = 0 To lstEmpID.Count - 1
    '        For jj As Integer = 0 To lstLeaveName.Count - 1
    '            sqlStr = "Select Count(leaveName) from vw_Employee_Attendance where leaveName='" & lstLeaveName.Item(jj) & "' and empID='" & lstEmpID.Item(ii) & "' And  AttDate between '" & dateFrom & "' and '" & dateTo & "' "
    '            
    '            
    '            Try
    '                tmpCount = ExecuteQuery_ExecuteScalar(SqlStr)
    '            Catch ex As Exception
    '                tmpCount = 0
    '            End Try
    '            sqlStr = "Update rptEmpLeaveSummary Set [" & lstLeaveName.Item(jj) & "] = '" & tmpCount & "' where empID='" & lstEmpID.Item(ii) & "'"
    '            
    '            
    '            ExecuteQuery_Update(SqlStr)
    '        Next
    '    Next

    '    sqlStr = "Select Count(distinct attdate) from vw_Employee_Attendance where  AttDate between '" & dateFrom & "' and '" & dateTo & "' "
    '    
    '    
    '    Try
    '        tmpCount = ExecuteQuery_ExecuteScalar(SqlStr)
    '    Catch ex As Exception
    '        tmpCount = 0
    '    End Try
    '    sqlStr = "Update rptEmpLeaveSummary Set WorkDays = '" & tmpCount & "'"
    '    
    '    
    '    ExecuteQuery_Update(SqlStr)

    '    
    '    

    '    GridView1.DataBind()
    '    GridView1.Columns(0).Visible = False
    '    GridView1.Columns(1).Visible = False
    '    lblAbsent.Visible = False
    '    lblPresent.Visible = False
    'End Sub


End Class
