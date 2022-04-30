Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class LibraryBookHistory
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Library") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("AccessDenied.aspx")
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
        'ReportViewer1.Visible = False
        lblStatus.Text = ""
        txtBookAcc.Text = ""
        gvStudent.Visible = False
        gvTeacher.Visible = False
        Try
            txtBookAcc.Text = Request.QueryString("AccNo")
            btnFind_Click(btnFind, New EventArgs())
        Catch ex As Exception
            txtBookAcc.Text = ""
        End Try
        'txtFromDay.Focus()
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
       
       
       

        Dim sqlStr As String = "", regno As String = "", TName As String = ""
        
        Dim FinalMessage As String = ""
        Dim transactCat As Integer = 2
        Try
            sqlStr = "Select Top(1) category From booktransact Where AccNo='B" & txtBookAcc.Text & "' and actualreturndate is NULL order by issuedate"
            
            
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    transactCat = myReader(0)
                Catch ex As Exception

                End Try
            End While
            myReader.Close()
            'transactCat = ExecuteQuery_ExecuteScalar(SqlStr)

            If transactCat = 0 Then
                sqlStr = "Select top(1) regno,Sname from vwbooktransactstudent where AccNo='B" & txtBookAcc.Text & "' and actualreturndate is NULL order by issuedate"
            Else
                sqlStr = "Select top(1) EmpCode,EmpName from vwbooktransactEmployee where AccNo='B" & txtBookAcc.Text & "' and actualreturndate is NULL order by issuedate"
            End If
            
            
            myReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                regno = myReader(0)
                TName = myReader(1)
            End While
            myReader.Close()

            If transactCat = 0 Then
                lblIssuedTo.Text = "Student : " & TName & "(" & regno & ")"
            ElseIf transactCat = 1 Then
                lblIssuedTo.Text = "Employee : " & TName & "(" & regno & ")"
            Else
                lblIssuedTo.Text = "Issued to none."

            End If
            gvStudent.Visible = True
            gvTeacher.Visible = True
            SqlDataSourceStudent.SelectCommand = "SELECT [AccNo], [IssueDate], [ExpectedReturnDate], [ActualReturnDate], [SName], [ClassName], [SecName], [RegNo], [BookTitle], [BookAccNo] FROM [vwBookTransactStudent] WHERE  AccNo='B" & txtBookAcc.Text & "' order by issuedate desc"
            gvStudent.DataSource = SqlDataSourceStudent
            gvStudent.DataBind()

            SqlDataSourceTeaacher.SelectCommand = "SELECT [BookAccNo], [BookTitle], [EmpCode], [EmpName], [IssueDate], [ExpectedReturnDate], [ActualReturnDate] FROM [vwBookTransactEmployee] WHERE  AccNo='B" & txtBookAcc.Text & "' order by issuedate desc"
            gvTeacher.DataSource = SqlDataSourceTeaacher
            gvTeacher.DataBind()

            ' FinalMessage = "Accession No: " & txtAccNo.Text & " successfully removed..."
        Catch ex As Exception
            'FinalMessage = "Unable to remove Accession No: " & txtAccNo.Text
        End Try
        
        
    End Sub
End Class