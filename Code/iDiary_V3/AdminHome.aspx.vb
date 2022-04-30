Imports System.Data.SqlClient

Partial Class Admin_AdminHome
    Inherits System.Web.UI.Page

    Dim sqlstr As String = ""
   
   


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'loadDashData()
        End If
    End Sub

    'Private Sub FillList()
    '   
    '    
    '    sqlstr = "Select top(8) logTime,EventType,Details,loginID From Event_Log Where Visible=1 Order By logID desc"
    '    
    '    
    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    BulletedListLOG.Items.Clear()
    '    While myReader.Read
    '        BulletedListLOG.Items.Add(myReader("EventType") & " By : " & myReader("loginID") & " At Time : " & myReader("logTime") & " Details : " & myReader("Details") & vbCrLf & vbCrLf)

    '    End While
    '    myReader.Close()
    '    

    'End Sub

    'Protected Sub BulletedListLOG_Click(sender As Object, e As BulletedListEventArgs) Handles BulletedListLOG.Click

    'End Sub


    Private Sub loadDashData()
        'Dim sqlstr As String = ""
        'Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        'Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        '
        'myConn.Open()
        'sqlstr = "Select Count(*) from vw_Student Where ASID='" & Request.Cookies("ASID").Value & "' AND StatusID=1"
        '
        '
        'lblStudentStrength.Text = "Total Student Strength : " & ExecuteQuery_ExecuteScalar(SqlStr)

        'sqlstr = "Select Count(*) from vw_Student Where ASID='" & Request.Cookies("ASID").Value & "' AND StatusID=1 AND Gender=0"
        '
        '
        'lblStudentDetail.Text = "Male Student Strength : " & ExecuteQuery_ExecuteScalar(SqlStr)

        'sqlstr = "Select Count(*) from vw_Student Where ASID='" & Request.Cookies("ASID").Value & "' AND StatusID=1 AND Gender=1"
        '
        '
        'lblStudentDetail1.Text = "Female Student Strength : " & ExecuteQuery_ExecuteScalar(SqlStr)

        'sqlstr = "Select Count(*) From vw_Student_Attendance Where  IsPresentM=1 AND AttDate='" & System.DateTime.Today.ToString("yyyy/MM/dd") & "'"
        '
        '
        'lblStudentDetail2.Text = "Total Present : " & ExecuteQuery_ExecuteScalar(SqlStr)

        'sqlstr = "Select Count(*) From vw_Student_Attendance Where  IsPresentM=0 AND AttDate='" & System.DateTime.Today.ToString("yyyy/MM/dd") & "'"
        '
        '
        'lblStudentDetail3.Text = "Total Absent : " & ExecuteQuery_ExecuteScalar(SqlStr)

        'sqlstr = "Select SUM(FeeDepositAmount) from vw_FeeDeposit Where ASID='" & Request.Cookies("ASID").Value & "' AND isDeposit=1"
        '
        '
        'Dim feedeposited As Double = 0
        'Try
        '    feedeposited = ExecuteQuery_ExecuteScalar(SqlStr)
        'Catch ex As Exception

        'End Try
        'lblFee.Text = "Total Fee Collection  : " & ExecuteQuery_ExecuteScalar(SqlStr)

        'sqlstr = "Select SUM(FeeDepositAmount) from vw_FeeDeposit Where DepositDate='" & System.DateTime.Today.ToString("yyyy/MM/dd") & "' AND isDeposit=1"
        '
        '
        'feedeposited = 0
        'Try
        '    feedeposited = ExecuteQuery_ExecuteScalar(SqlStr)
        'Catch ex As Exception

        'End Try
        'lblFeeDetail.Text = "Today's Collection : " & feedeposited


        'sqlstr = "Select SUM(FeeDepositAmount) from vw_FeeDeposit Where MONTH(DepositDate)='" & System.DateTime.Now.Month & "' AND isDeposit=1"
        '
        '
        'feedeposited = 0
        'Try
        '    feedeposited = ExecuteQuery_ExecuteScalar(SqlStr)
        'Catch ex As Exception

        'End Try
        'lblFeeDetail1.Text = "Month's Collection : " & ExecuteQuery_ExecuteScalar(SqlStr)


        'sqlstr = "Select Count(*) From vw_Employees Where Status=1"
        '
        '
        'lblEmployee.Text = "Total Employees : " & ExecuteQuery_ExecuteScalar(SqlStr)

        'sqlstr = "Select Count(*) From vwEmployeeAttendance Where  Att=1 AND AttDate='" & System.DateTime.Today.ToString("yyyy/MM/dd") & "'"
        '
        '
        'lblEmployeeDetail.Text = "Present Employees : " & ExecuteQuery_ExecuteScalar(SqlStr)

        'sqlstr = "Select Count(*) From vwEmployeeAttendance Where  Att=0 AND AttDate='" & System.DateTime.Today.ToString("yyyy/MM/dd") & "'"
        '
        '
        'lblEmployeeDetail1.Text = "Absent Employees : " & ExecuteQuery_ExecuteScalar(SqlStr)

        ''sqlstr = "Select SUM(TransAmount) From vwPettyCashTransaction Where TransTypeID=1"
        ''
        ''
        ''lblPettyCash.Text = "Petty Cash Balance : " & ExecuteQuery_ExecuteScalar(SqlStr)

        'sqlstr = "Select SUM(TransAmount) From vwPettyCashTransaction Where TransTypeID=2"
        '
        '
        'Dim PettyIncome As Double = 0
        'Try
        '    PettyIncome = ExecuteQuery_ExecuteScalar(SqlStr)
        'Catch ex As Exception

        'End Try
        'lblPettyCash1.Text = "Total Income : " & PettyIncome

        'sqlstr = "Select SUM(TransAmount) From vwPettyCashTransaction Where TransTypeID=1"
        '
        '
        'Dim PettyExpen As Double = 0
        'Try
        '    PettyExpen = ExecuteQuery_ExecuteScalar(SqlStr)
        'Catch ex As Exception

        'End Try
        'lblPettyCash2.Text = "Total Expenditure : " & PettyExpen
        'lblPettyCash.Text = "Petty Cash Balance : " & PettyIncome - PettyExpen


        '
        '

    End Sub
End Class
