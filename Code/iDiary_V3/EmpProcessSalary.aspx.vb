Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ProcessSalary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Payroll-1") = False Then
            btnPayBill.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        btnPayBill.Visible = False
        LoadMonths(cboMonth)
        LoadYears(cboYear)
        lblStatus.Text = ""
        cboMonth.Focus()
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If cboMonth.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select a month to continue...');", True)
            cboMonth.Focus()
            Exit Sub
        End If

        If cboYear.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select a Year to continue...');", True)
            cboYear.Focus()
            Exit Sub
        End If
       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Select count(*) From ProcessedSalarySummary where MonthID=" & cboMonth.SelectedIndex & " AND YearID='" & cboYear.Text & "'"

        
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)
        If rv > 0 Then
            lblStatus.Text = "Salary already processed for " & cboMonth.Text & "-" & cboYear.Text
            cboMonth.Focus()
            Exit Sub
        Else
            ProcessSalary()
        End If

        
        

    End Sub

    Private Sub ProcessSalary()
        btnPayBill.Visible = False

        Dim sqlStr As String = ""
        Dim TempStr As String = ""

        Dim i As Integer = 0

        Dim lstEmp As New ListBox
        Dim lstBasic As New ListBox
        Dim lstHead As New ListBox
        Dim lstHeadType As New ListBox
        Dim lstInsert As New ListBox

        lstEmp.Items.Clear()
        lstBasic.Items.Clear()
        lstHead.Items.Clear()
        lstHeadType.Items.Clear()
        lstInsert.Items.Clear()

       
       
       

        

        'Get List of All Employees

        sqlStr = "Select EmpID, BasicPay From EmployeeMaster"
        
        
        Dim EmpReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While EmpReader.Read
            lstEmp.Items.Add(EmpReader(0))
            lstBasic.Items.Add(EmpReader(1))
        End While
        EmpReader.Close()

        ''Get Start and End Dates of Selected Month
        'Dim StartDate As String = "", EndDate As String = ""
        'Dim WorkingDays As Double = 0

        'StartDate = cboMonth.SelectedIndex & "/01/" & cboYear.Text
        'If cboMonth.SelectedIndex = 1 Or cboMonth.SelectedIndex = 3 Or cboMonth.SelectedIndex = 5 Or cboMonth.SelectedIndex = 7 Or cboMonth.SelectedIndex = 8 Or cboMonth.SelectedIndex = 10 Or cboMonth.SelectedIndex = 12 Then
        '    EndDate = cboMonth.SelectedIndex & "/31/" & cboYear.Text
        '    WorkingDays = 31
        'ElseIf cboMonth.SelectedIndex = 4 Or cboMonth.SelectedIndex = 6 Or cboMonth.SelectedIndex = 9 Or cboMonth.SelectedIndex = 11 Then
        '    EndDate = cboMonth.SelectedIndex & "/30/" & cboYear.Text
        '    WorkingDays = 30
        'ElseIf cboMonth.SelectedIndex = 2 Then
        '    EndDate = cboMonth.SelectedIndex & "/29/" & cboYear.Text
        '    WorkingDays = 29
        'End If

        'Get List of All Salary Earning Heads
        sqlStr = "Select HeadID,HeadType From SalaryHeadMaster Where HeadType=1 Order By HeadName"
        
        
        Dim HeadEReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While HeadEReader.Read
            lstHead.Items.Add(HeadEReader(0))
            lstHeadType.Items.Add(HeadEReader(1))
        End While
        HeadEReader.Close()

        'Get List of All Salary Deduction Heads
        sqlStr = "Select HeadID,HeadType From SalaryHeadMaster Where HeadType=2 Order By HeadName"
        
        
        Dim HeadDReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While HeadDReader.Read
            lstHead.Items.Add(HeadDReader(0))
            lstHeadType.Items.Add(HeadDReader(1))
        End While
        HeadDReader.Close()

        'For Each Employee and for each Salary Head Perform Calculation and Insert Result to DB
        For i = 0 To lstEmp.Items.Count - 1

            'Get Salary Deduct Days For this Employee during Start and End Date
            'sqlStr = "Select Count(*) From vwEmployeeAttendanceLeaves Where " & _
            '    "EmpID=" & lstEmp.Items(i).Text & " AND " & _
            '    "AttDate Between '" & StartDate & "' and '" & EndDate & "' AND " & _
            '    "SalaryDeduct=1"

            '
            '

            'Dim LWPDays As Double = 0
            'Try
            '    LWPDays = ExecuteQuery_ExecuteScalar(SqlStr)
            'Catch ex As Exception
            '    LWPDays = 0
            'End Try

            TempStr = " Insert into ProcessedSalary Values(" & _
            lstEmp.Items.Item(i).Text & "," & _
            cboMonth.SelectedIndex & "," & _
            "'" & cboYear.Text & "',0," & _
            lstBasic.Items.Item(i).Text & ")"

            lstInsert.Items.Add(TempStr) 'Inserting Basic Salary with HeadID=0 (No FK Constrint in Processed Salary)

            For j = 0 To lstHead.Items.Count - 1

                sqlStr = "Select CalcType,FixedAmt From ConfigEmpSalary Where EmpID=" & lstEmp.Items.Item(i).Text & " AND HeadID=" & lstHead.Items.Item(j).Text
                
                
                Dim DetailsReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

                Dim CalcType As Integer = 1 '1-Fixed,2-%age
                Dim FixedAmt As Double = 0
                Dim myCount As Integer = 0

                While DetailsReader.Read()

                    Try
                        CalcType = DetailsReader("CalcType")
                    Catch ex As Exception
                        CalcType = 1
                    End Try

                    Try
                        FixedAmt = DetailsReader("FixedAmt")
                    Catch ex As Exception
                        FixedAmt = 0
                    End Try

                End While

                DetailsReader.Close()


                TempStr = " Insert into ProcessedSalary Values(" & _
                            lstEmp.Items.Item(i).Text & "," & _
                            cboMonth.SelectedIndex & "," & _
                            "'" & cboYear.Text & "'," & _
                            lstHead.Items.Item(j).Text & ","

                If CalcType < 1 Or CalcType > 2 Then
                    Response.Write("U r here: Check Salary Config for Employee Id=" & lstEmp.Items.Item(i).Text & " AND SalaryHeadID=" & lstHead.Items.Item(j).Text)
                    Exit Sub
                End If

                Select Case CalcType

                    Case 1  'Fixed Amount Calculation
                        TempStr &= Math.Round(FixedAmt, 0)

                    Case 2  'Percentage Based Calculation, 'Find Percentage of Current HeadID

                        sqlStr = "Select HeadValue From ConfigSalaryHeads Where HeadID=" & lstHead.Items.Item(j).Text
                        
                        
                        Dim HeadValueReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

                        Dim HeadValue As Double = 0
                        While HeadValueReader.Read()
                            Try
                                HeadValue = HeadValueReader(0)
                            Catch ex As Exception
                                HeadValue = 0
                            End Try
                        End While

                        HeadValueReader.Close()

                        TempStr &= Math.Ceiling(lstBasic.Items.Item(i).Text * HeadValue / 100)

                End Select
                TempStr &= ")"
                'TempStr &= "," & WorkingDays & "," & LWPDays & ")"
                lstInsert.Items.Add(TempStr)
            Next
        Next

        sqlStr = "Delete From ProcessedSalary Where MonthID=" & cboMonth.SelectedIndex & " AND YearID='" & cboYear.Text & "'"
        
        
        ExecuteQuery_Update(SqlStr)

        'Insert all Queries to DB
        For i = 0 To lstInsert.Items.Count - 1
            sqlStr = lstInsert.Items.Item(i).Text
            
            ExecuteQuery_Update(SqlStr)
            System.Threading.Thread.Sleep(300)
        Next

        sqlStr = "Insert into ProcessedSalarySummary Values(" & _
        cboMonth.SelectedIndex & _
        ",'" & cboYear.Text & _
        "','" & Now.Date.Day & "/" & Now.Date.Month & "/" & Now.Date.Year & "'" & _
        ")"

        
        
        ExecuteQuery_Update(SqlStr)

        
        

        lblStatus.Text = "Salary Processed Successfully..."
        btnPayBill.Visible = True
    End Sub

    Protected Sub btnPayBill_Click(sender As Object, e As EventArgs) Handles btnPayBill.Click
        response.redirect("~/GeneratePayBill.aspx?rmonth=" & cboMonth.Text & "&ryear=" & cboYear.Text)
    End Sub
End Class