Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class GeneratePayBill
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()

        Else
            'For Grid View Printing. Must have a blank HTM Page (gview.htm)
            Dim printScript As String = "function PrintGridView() { var gridInsideDiv = document.getElementById('gvDiv');" & _
            " var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');" & _
            " printWindow.document.write(gridInsideDiv.innerHTML);printWindow.document.close();printWindow.focus();" & _
            " printWindow.print();printWindow.close();}"
            Me.ClientScript.RegisterStartupScript(Page.[GetType](), "PrintGridView", printScript.ToString(), True)
            btnPrint.Attributes.Add("onclick", "PrintGridView();")
        End If
    End Sub

    Private Sub InitControls()
        LoadMonths(cboMonth)
        LoadYears(cboYear)
        LoadMasterInfo(30, cboEmpCat)
        Try
            GridView1.DataBind()
        Catch ex As Exception

        End Try
        GridView1.Visible = False
        btnPrint.Visible = False
        btnExcel.Visible = False

        cboMonth.Focus()
    End Sub

    Protected Sub btnPayBill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPayBill.Click

        If cboMonth.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Month Selection');", True)
            cboMonth.Focus()
            Exit Sub
        End If
        If cboYear.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Year Selection');", True)
            cboMonth.Focus()
            Exit Sub
        End If

       
        
       

        Dim sqlStr As String = "Select Count(*) From ProcessedSalarySummary Where MonthID=" & cboMonth.SelectedIndex & " ANd YearID='" & cboYear.Text & "'"
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(sqlStr)

        If rv <= 0 Then 'Salary Not Processed till now
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Bill can not be generated for " & cboMonth.Text & "-" & cboYear.Text & "');", True)
            
            cboMonth.Focus()
            Exit Sub
        End If

        
        

        GeneratePayBill()
        GridView1.DataBind()
    End Sub

    Private Sub GeneratePayBill()

       
        
       

        
        Dim sqlStr As String = ""

        Dim EmpCatID As Integer = 0
        If cboEmpCat.Text <> "" Then
            EmpCatID = FindMasterID(30, cboEmpCat.Text)
        End If

        Dim lstHeadID As New ListBox
        Dim lstHead As New ListBox
        Dim lstHeadType As New ListBox
        Dim i As Integer = 0

        'Remove Previous Report Table
        sqlStr = "DROP TABLE rptPayBill"
        
        
        Try
            ExecuteQuery_Update(SqlStr)
        Catch ex As Exception

        End Try

        'Re-Create Report Table
        sqlStr = "CREATE TABLE rptPayBill(" & _
        "[EmpID] int," & _
        "[Name] nvarchar(200)," & _
        "[Pay Band] nvarchar(200)," & _
        "[Grade] nvarchar(200)," & _
        "[Acc No] nvarchar(200)," & _
        "[Basic] float," & _
        "[Working Days] float)"

        
        
        ExecuteQuery_Update(SqlStr)

        'Earning Heads
        sqlStr = "Select HeadID, HeadName, HeadType From SalaryHeadMaster Where HeadType=1 Order By HeadID, HeadName"
        
        
        Dim HeadEReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While HeadEReader.Read
            lstHeadID.Items.Add(HeadEReader(0))
            lstHead.Items.Add(HeadEReader(1))
            lstHeadType.Items.Add(HeadEReader(2))
        End While
        HeadEReader.Close()

        'Alter Report Table according to Head Type
        For i = 0 To lstHead.Items.Count - 1
            If lstHeadType.Items(i).Text = 1 Then
                sqlStr = "Alter Table rptPayBill Add [" & lstHead.Items(i).Text & "] float;"
                
                
                ExecuteQuery_Update(SqlStr)
            End If
        Next

        sqlStr = "Alter Table rptPayBill Add [Gross Pay] float;"
        
        
        ExecuteQuery_Update(SqlStr)

        'Earning Heads
        sqlStr = "Select HeadID, HeadName, HeadType From SalaryHeadMaster Where HeadType=2 Order By HeadID, HeadName"
        
        
        Dim HeadDReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While HeadDReader.Read
            lstHeadID.Items.Add(HeadDReader(0))
            lstHead.Items.Add(HeadDReader(1))
            lstHeadType.Items.Add(HeadDReader(2))
        End While
        HeadDReader.Close()

        'Alter Report Table according to Head Type
        For i = 0 To lstHead.Items.Count - 1
            If lstHeadType.Items(i).Text = 2 Then
                sqlStr = "Alter Table rptPayBill Add [" & lstHead.Items(i).Text & "] float;"
                
                
                ExecuteQuery_Update(SqlStr)
            End If
        Next

        sqlStr = "Alter Table rptPayBill Add [Total Deductions] float;"
        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "Alter Table rptPayBill Add [Net Pay] float;"
        
        
        ExecuteQuery_Update(SqlStr)


        sqlStr = "Alter Table rptPayBill Add [Sign] float;"
        
        
        ExecuteQuery_Update(SqlStr)

        'Get Start and End Dates of Selected Month
        Dim StartDate As String = "", EndDate As String = ""
        Dim WorkingDays As Double = 0

        StartDate = cboMonth.SelectedIndex & "/01/" & cboYear.Text
        If cboMonth.SelectedIndex = 1 Or cboMonth.SelectedIndex = 3 Or cboMonth.SelectedIndex = 5 Or cboMonth.SelectedIndex = 7 Or cboMonth.SelectedIndex = 8 Or cboMonth.SelectedIndex = 10 Or cboMonth.SelectedIndex = 12 Then
            EndDate = cboMonth.SelectedIndex & "/31/" & cboYear.Text
            WorkingDays = 31
        ElseIf cboMonth.SelectedIndex = 4 Or cboMonth.SelectedIndex = 6 Or cboMonth.SelectedIndex = 9 Or cboMonth.SelectedIndex = 11 Then
            EndDate = cboMonth.SelectedIndex & "/30/" & cboYear.Text
            WorkingDays = 30
        ElseIf cboMonth.SelectedIndex = 2 Then
            EndDate = cboMonth.SelectedIndex & "/29/" & cboYear.Text
            WorkingDays = 29
        End If

        sqlStr = "Insert into rptPayBill (EmpID, [Name], [Pay Band], Grade, Basic, [Acc No]) " & _
        "Select EmpID, EmpName, PayScaleName, AGPName, BasicPay, AccNo From vw_Employees "
        If cboEmpCat.Text = "" Then
            'Do Nothing (For All)
        Else
            sqlStr &= "Where EmpCatName='" & cboEmpCat.Text & "'"
        End If

        
        
        ExecuteQuery_Update(SqlStr)

        Dim lstEmpID As New ListBox
        Dim lstBasic As New ListBox

        sqlStr = "Select EmpID,Basic From rptPayBill"
        
        
        Dim EmpReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While EmpReader.Read
            lstEmpID.Items.Add(EmpReader(0))
            lstBasic.Items.Add(EmpReader(1))
        End While
        EmpReader.Close()

        Dim GrossPay As Double = 0
        Dim TotalDed As Double = 0
        Dim NetPay As Double = 0
        Dim ExistCount As Integer = 0

        For i = 0 To lstEmpID.Items.Count - 1

            ExistCount = 0
            GrossPay = 0
            TotalDed = 0
            NetPay = 0

            GrossPay = lstBasic.Items.Item(i).Text

            For j = 0 To lstHeadID.Items.Count - 1

                sqlStr = "Select sum(HeadAmount) From ProcessedSalary Where " & _
                " EmpID=" & lstEmpID.Items.Item(i).Text & _
                " AND MonthID=" & cboMonth.SelectedIndex & _
                " AND YearID='" & cboYear.Text & "'" & _
                " AND HeadID=" & lstHeadID.Items.Item(j).Text

                
                
                Dim HeadAmt As Double = 0
                Try
                    HeadAmt = ExecuteQuery_ExecuteScalar(sqlStr)
                    ExistCount += 1
                Catch ex As Exception
                    HeadAmt = 0
                End Try

                If lstHeadType.Items(j).Text = "1" Then GrossPay += HeadAmt
                If lstHeadType.Items(j).Text = "2" Then TotalDed += HeadAmt

                sqlStr = "Update rptPayBill Set [" & lstHead.Items(j).Text & "]=" & HeadAmt & " Where EmpID=" & lstEmpID.Items(i).Text
                
                
                ExecuteQuery_Update(SqlStr)

            Next

            If ExistCount > 0 Then

                'Salary Deductions
                sqlStr = "Select Count(*) From vwEmployeeAttendanceLeaves Where " & _
                "EmpID=" & lstEmpID.Items(i).Text & " AND " & _
                "AttDate Between '" & StartDate & "' and '" & EndDate & "' AND " & _
                "SalaryDeduct=1"

                
                

                Dim LWPDays As Double = 0
                Try
                    LWPDays = ExecuteQuery_ExecuteScalar(SqlStr)
                Catch ex As Exception
                    LWPDays = 0
                End Try

                Dim EffectiveWorkingDays As Double = WorkingDays - LWPDays
                GrossPay = Math.Round(GrossPay - (GrossPay / WorkingDays) * LWPDays, 2)

                NetPay = GrossPay - TotalDed

                sqlStr = "Update rptPayBill Set [Working Days]=" & EffectiveWorkingDays & ", [Gross Pay]=" & GrossPay & ",[Total Deductions]=" & TotalDed & ",[Net Pay]=" & NetPay & " Where EmpID=" & lstEmpID.Items(i).Text
                
                
                ExecuteQuery_Update(SqlStr)

                'Prepare Bank Statement for Employee
                '------------------------------------
                sqlStr = "Select count(*) From BankStatements Where EmpID=" & lstEmpID.Items.Item(i).Text & " AND MonthID=" & cboMonth.SelectedIndex & " AND YearID='" & cboYear.Text & "'"
                
                
                Dim BankCount As Integer = ExecuteQuery_ExecuteScalar(SqlStr)

                If BankCount <= 0 Then
                    sqlStr = "Insert into BankStatements Values(" & lstEmpID.Items.Item(i).Text & "," & cboMonth.SelectedIndex & ",'" & cboYear.Text & "'," & NetPay & ")"
                Else
                    sqlStr = "Update BankStatements Set NetPay=" & NetPay & " Where EmpID=" & lstEmpID.Items.Item(i).Text & " AND MonthID=" & cboMonth.SelectedIndex & " AND YearID='" & cboYear.Text & "'"
                End If

                
                
                ExecuteQuery_Update(SqlStr)

            Else

                sqlStr = "Delete From rptPayBill Where EmpID=" & lstEmpID.Items(i).Text
                
                
                ExecuteQuery_Update(SqlStr)

            End If

        Next

        
        

        GridView1.DataBind()

        If GridView1.Rows.Count > 0 Then
            GridView1.Visible = True
            btnPrint.Visible = True
            btnExcel.Visible = True
        Else
            GridView1.Visible = False
            btnPrint.Visible = False
            btnExcel.Visible = False
        End If

    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "PayBill_" & cboMonth.Text & "_" & cboYear.Text & ".xls"

        Page.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        Page.Response.ContentType = "application/vnd.ms-excel"
        Page.Response.Charset = ""
        Page.EnableViewState = False
        frm.Attributes("runat") = "server"
        Controls.Add(frm)
        frm.Controls.Add(GridView1)
        frm.RenderControl(hw)
        Response.Write(sw.ToString())
        Response.End()

    End Sub
End Class