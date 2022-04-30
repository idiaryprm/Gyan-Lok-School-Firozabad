Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub InitControls()
        FillEvent()
        ViewStudents()
        ViewEmpolyees()
        ViewFeesToday()
    End Sub
    Public Function ViewTC()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) from vw_StudentTC Where DateOfIssue like '" & System.DateTime.Now.Year & "-" & System.DateTime.Now.Month & "-%'"
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try

        
        Return rv
    End Function
    Public Function ViewTCToday()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) from vw_StudentTC Where DateOfIssue ='" & System.DateTime.Now.Date.ToString("yyyy/MM/dd") & "'"
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try


        Return rv
    End Function
    Public Function ViewTCSession()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) from vw_StudentTC Where DateOfIssue like '" & System.DateTime.Now.Year & "-%'"
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try

        
        Return rv
    End Function
    Public Function ViewStudents()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
        sqlstr = "Select Count(*),Gender from vw_Student Where StatusID=1 AND ASID=" & Request.Cookies("ASID").Value & " group by gender"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            If myReader(1) = 0 Then
                lblMaleStudent.Text = myReader(0)
            Else
                lblFemaleStudent.Text = myReader(0)
            End If

            'lblFemaleStudent.Text = myReader(1)
            lblStudentStrength.Text = Val(lblMaleStudent.Text) + Val(lblFemaleStudent.Text)
        End While
        myReader.Close()
        Return rv
    End Function
    'Public Function ViewStudentsMale(ByVal gender As Integer)
    '    Dim rv As Integer = 0
    '    Dim sqlstr As String = ""
    '   
    '   
    '    
    '   
    '    sqlstr = "Select Count(*) from vw_Student Where StatusID=1 and Gender='" & gender & "'"
    '    
    '    
    '    rv = ExecuteQuery_ExecuteScalar(SqlStr)

    '    
    '    
    '    
    '    Return rv
    'End Function
    Public Function ViewStudentsPresent(ByVal AttEntry As Integer)
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) from vw_Attendance Where IsPresentM='" & AttEntry & "' And AttDate like '" & System.DateTime.Now.Year & "-" & System.DateTime.Now.Month & "-" & System.DateTime.Now.Day & "%'"
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try


        Return rv
    End Function
    Public Function ViewPendingAtt()
        Dim rv As Integer = 0
        Dim SS As Integer = ViewStudents()
        Dim P As Integer = ViewStudentsPresent(1)
        Dim A As Integer = ViewStudentsPresent(0)
        rv = P + A
        rv = SS - rv
        Return rv
    End Function
    Public Function ViewEmpolyees()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
        lblFemaleEmployee.Text = "0"
        lblMaleEmployee.Text = "0"
        sqlstr = "Select Count(*),Gender From vw_Employees Where Status=1 group by gender"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            If myReader(1) = 0 Then
                lblMaleEmployee.Text = myReader(0)
            Else
                lblFemaleEmployee.Text = myReader(0)
            End If
            lblEmployeeStrength.Text = Val(lblMaleEmployee.Text) + Val(lblFemaleEmployee.Text)
        End While
        myReader.Close()
        Return rv
    End Function

    Public Function ViewEmployeePresent(ByVal AttEntry As Integer)
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) from vw_Employee_Attendance Where Att='" & AttEntry & "' and AttDate like '" & System.DateTime.Now.Year & "-" & System.DateTime.Now.Month & "-" & System.DateTime.Now.Day & "%' And Status=1"
        
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try


        Return rv
    End Function
    Public Function ViewPendingAttEmp()
        Dim rv As Integer = 0
        Dim SS As Integer = ViewEmpolyees()
        Dim P As Integer = ViewEmployeePresent(1)
        Dim A As Integer = ViewEmployeePresent(0)
        rv = P + A
        rv = SS - rv
        Return rv
    End Function
    Public Function ViewFees()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
        sqlstr = "Select SUM(FeeDepositAmount) from vw_FeeDeposit Where DepositDate > DATEADD(year,-1,GETDATE())"
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try

        Return rv.ToString(".00")
    End Function
    Public Function ViewFeesMonthly()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select SUM(FeeDepositAmount) from vw_FeeDeposit Where DepositDate Between '" & System.DateTime.Now.AddDays(-31).ToString("yyyy/MM/dd") & "' AND '" & System.DateTime.Now.Date & "'"
        
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Return rv.ToString(".00")
        Catch ex As Exception
            rv = 0
            Return rv.ToString("0.00")
        End Try
        
5:
    End Function
    Public Function ViewFeesToday()
        Dim DepositDate(2) As Date
        Dim rv(2) As Double
        Dim sqlstr As String = ""
        Dim i As Integer = 0
        sqlstr = "Select Top 2 SUM(FeeDepositAmount), Max(DepositDate) From vw_FeeDeposit group by DepositDate order by DepositDate DESC"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            rv(i) = myReader(0)
            DepositDate(i) = myReader(1)
            i = i + 1
        End While
        myReader.Close()
        lblFeeDate.Text = DepositDate(0).ToString("dd-MMM") & " : <i class=""fa fa-inr"" style=""width:12px;height:0px;line-height:0px;text-align:left""></i>" & rv(0).ToString(".00") & "<br/>" & DepositDate(1).ToString("dd-MMM") & " : <i class=""fa fa-inr"" style=""width:12px;height:0px;line-height:0px;text-align:left""></i>" & rv(1).ToString(".00")
        Return rv
    End Function
    Public Function ViewBooks()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) From BookMaster Where BookStatusID =4"
        
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try


        Return rv
    End Function
    Public Function ViewBooksIssued()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) From BookMaster Where BookStatusID =4 and Issued='1'"
        
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try


        Return rv
    End Function
    Public Function ViewBooksIssuedStudent()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) From vw_BookTransactStudent where ActualReturnDate='' or ActualReturnDate is null"
        
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try


        Return rv
    End Function
    Public Function ViewBooksIssuedTeacher()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) From vw_BookTransactEmployee where ActualReturnDate='' or ActualReturnDate is null"
        
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try


        Return rv
    End Function

    Public Function NewAdmission()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) From vw_Student Where AdmissionDate like'" & System.DateTime.Now.Year & "-" & System.DateTime.Now.Month & "-%'"
        
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try


        Return rv
    End Function
    Public Function NewAdmissionToday()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) From vw_Student Where AdmissionDate ='" & System.DateTime.Now.Date.ToString("yyyy/MM/dd") & "'"
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try

        Return rv
    End Function
    Public Function NewAdmissionSession()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) From vw_Student Where AdmissionDate like'" & System.DateTime.Now.Year & "-%'"
        
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try


        Return rv
    End Function
    Public Function NewBooks()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
       
        sqlstr = "Select Count(*) From vw_BookMaster Where DateIn like'" & System.DateTime.Now.Year & "-" & System.DateTime.Now.Month & "-%'"
        
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try


        Return rv
    End Function
    Public Sub FillEvent()
        Dim sqlstr As String = ""
       
        sqlstr = "Select Distinct EventType From vw_Event_log"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cboEvent.Items.Clear()
        cboEvent.Items.Add("ALL")
        While myReader.Read
            cboEvent.Items.Add(myReader(0))
        End While
        myReader.Close()

    End Sub
    Public Function EventLog()
        Dim result As String = ""
        Dim ds As DataSet = Nothing

        Dim sqlstr As String = ""
       
        If cboTime.Text = "Weekly" Then
            sqlstr = "Select * From vw_Event_log where logTime between '" & System.DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd") & "' AND '" & System.DateTime.Now.Date.AddDays(+1).ToString("yyyy/MM/dd") & "'"
        ElseIf cboTime.Text = "Monthly" Then
            sqlstr = "Select * From vw_Event_log where logTime between '" & System.DateTime.Now.AddDays(-30).ToString("yyyy/MM/dd") & "' AND '" & System.DateTime.Now.Date.AddDays(+1).ToString("yyyy/MM/dd") & "'"
        ElseIf cboTime.Text = "Yearly" Then
            sqlstr = "Select * From vw_Event_log where logTime between '" & System.DateTime.Now.AddDays(-364).ToString("yyyy/MM/dd") & "' AND '" & System.DateTime.Now.Date.AddDays(+1).ToString("yyyy/MM/dd") & "'"
        ElseIf cboTime.Text = "Today" Then
            sqlstr = "Select * From vw_Event_log where logTime between '" & System.DateTime.Now.ToString("yyyy/MM/dd") & "' ANd '" & System.DateTime.Now.Date.AddDays(+1).ToString("yyyy/MM/dd") & "'"
        End If
        If cboEvent.Text = "ALL" Then
        Else
            sqlstr += " And EventType='" & cboEvent.Text & "'"
        End If
        sqlstr += " order by LogTime Desc"
        ds = New DataSet()
        ds = ExecuteQuery_DataSet(sqlstr, "t")

        Return ds
    End Function
End Class