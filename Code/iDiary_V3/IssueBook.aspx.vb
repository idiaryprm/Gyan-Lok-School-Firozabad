Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.clsLibrary

Public Class IssueBook
    Inherits System.Web.UI.Page

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("Index.aspx")
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        lblStatus.Text = ""
        SearchBookInfo()
        txtDDEDOR.Text = ExpectedDate.Day
        txtMMEDOR.Text = ExpectedDate.Month
        txtYYEDOR.Text = ExpectedDate.Year
    End Sub

    Private Sub SearchBookInfo()
        If Trim(txtBookAccNo.Text).Length <= 0 Then
            lblStatus.Text = "Accession No is BLANK..."
            txtBookAccNo.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
        Dim Issued As Integer = -1
        Dim myCount As Integer = 0
        

        sqlStr = "Select * From vw_BookMaster Where BookAccNo='" & txtBookAccNo.Text & "' AND BookStatusName='Available'"
        
        
        txtAuthor.Text = ""
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            myCount += 1
            txtAccNo.Text = myReader("AccNo")
            txtTitle.Text = myReader("BookTitle")
            Try
                txtAuthor.Text &= myReader("AuthorName")
            Catch ex As Exception

            End Try

            If myCount > 0 Then txtAuthor.Text &= ","
            Try
                txtPub.Text = myReader("PubName")
            Catch ex As Exception

            End Try
            txtBookCat.Text = myReader("BookCatName")
            Issued = myReader("Issued")
        End While
        Try
            txtAuthor.Text = txtAuthor.Text.Substring(0, txtAuthor.Text.Length - 1)
        Catch ex As Exception

        End Try

        myReader.Close()

        Select Case Issued
            Case -1
                InitControls()
            Case 0
                txtRegNo.Text = ""
                txtStudentName.Text = ""
                txtClassSection.Text = ""
                lblIssueDate.Text = Now.ToString("dd/MM/yyyy")
                txtDDEDOR.Text = ExpectedDate.Day
                txtMMEDOR.Text = ExpectedDate.Month
                txtYYEDOR.Text = ExpectedDate.Year
                lblActualReturnDate.Text = Now.ToString("dd/MM/yyyy")
                txtDDEDOR.Enabled = True
                txtMMEDOR.Enabled = True
                txtYYEDOR.Enabled = True

                txtRegNo.Enabled = True
                btnRegNoNext.Enabled = True
                txtStudentName.Enabled = True
                btnSearch.Enabled = True
                btnIssue.Enabled = True
                btnReturn.Enabled = False
            Case 1
                'Display Member Information
                'Get Category Student or teacher
                sqlStr = "Select BookTransactID,Category From BookTransact Where AccNo='" & txtAccNo.Text & "' AND ActualReturnDate is NULL"
                
                
                Dim CategoryReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                Dim Category As Integer = 0
                While CategoryReader.Read
                    txtBookTransactID.Text = CategoryReader("BookTransactID")
                    Category = CategoryReader("Category")
                End While
                CategoryReader.Close()
                If Category = 0 And Category = Request.QueryString("type") Then
                    sqlStr = "Select top(1) * From vw_BookTransactStudent Where AccNo='" & txtAccNo.Text & "' AND ActualReturnDate is NULL AND ASID=" & Request.Cookies("ASID").Value & " order by booktransactid desc"
                    
                    

                    Dim TransactReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                    While TransactReader.Read
                        txtRegNo.Text = TransactReader("UniqueID")
                        txtStudentName.Text = TransactReader("SName")
                        txtClassSection.Text = TransactReader("ClassName") & "/" & TransactReader("SecName")
                        lblClass.Text = FindMasterID(2, TransactReader("ClassName"))
                        Dim a As Date = TransactReader("IssueDate").ToString

                        lblIssueDate.Text = a.ToString("dd/MM/yyyy")
                        'lblIssueDate.Text = CDate(TransactReader("IssueDate")).Day & "/" & CDate(TransactReader("IssueDate")).Month & "/" & CDate(TransactReader("IssueDate")).Year
                        Dim TempDate As Date = TransactReader("ExpectedReturnDate")
                        txtDDEDOR.Enabled = False
                        txtMMEDOR.Enabled = False
                        txtYYEDOR.Enabled = False
                        txtDDEDOR.Text = TempDate.Day.ToString("00")
                        txtMMEDOR.Text = TempDate.Month.ToString("00")
                        txtYYEDOR.Text = TempDate.Year
                        lblActualReturnDate.Text = Now.Day.ToString("00") & "/" & Now.Month.ToString("00") & "/" & Now.Year
                    End While

                    TransactReader.Close()
                ElseIf Category = 1 And Category = Request.QueryString("type") Then
                    sqlStr = "Select top(1) * From vw_BookTransactEmployee Where AccNo='" & txtAccNo.Text & "' AND ActualReturnDate is NULL AND ASID=" & Request.Cookies("ASID").Value & " order by booktransactid desc"
                    
                    

                    Dim TransactReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                    While TransactReader.Read
                        txtRegNo.Text = TransactReader("UniqueID")
                        txtStudentName.Text = TransactReader("EmpName")
                        txtClassSection.Text = TransactReader("DeptName") & "/" & TransactReader("DesgName")

                        Dim a As Date = TransactReader("IssueDate").ToString

                        lblIssueDate.Text = a.ToString("dd/MM/yyyy")
                        'lblIssueDate.Text = CDate(TransactReader("IssueDate")).Day & "/" & CDate(TransactReader("IssueDate")).Month & "/" & CDate(TransactReader("IssueDate")).Year
                        Dim TempDate As Date = TransactReader("ExpectedReturnDate")
                        txtDDEDOR.Enabled = False
                        txtMMEDOR.Enabled = False
                        txtYYEDOR.Enabled = False
                        txtDDEDOR.Text = TempDate.Day.ToString("00")
                        txtMMEDOR.Text = TempDate.Month.ToString("00")
                        txtYYEDOR.Text = TempDate.Year
                        lblActualReturnDate.Text = Now.Day.ToString("00") & "/" & Now.Month.ToString("00") & "/" & Now.Year
                    End While
                    TransactReader.Close()
                Else
                    Response.Redirect("IssueBook.aspx?type=" & Category)
                End If
               
                lblFine.Visible = True

                Dim fine As Double = GetFineAmount(lblIssueDate.Text, lblActualReturnDate.Text)
                lblFine.Text = "Total Fine: " & fine & " Rs Only"
                If fine > 0 Then
                    chkFine.Visible = True
                End If
                txtRegNo.Enabled = False
                btnRegNoNext.Enabled = False
                btnIssue.Enabled = False
                btnReturn.Enabled = True
        End Select

    End Sub
    Public Function GetFineAmount(SmallDate As String, GreaterDate As String) As Double

        Dim DateNow As Date = New DateTime(GreaterDate.Substring(6, 4), GreaterDate.Substring(3, 2), GreaterDate.Substring(0, 2))
        Dim DateDOB As Date = New DateTime(SmallDate.Substring(6, 4), SmallDate.Substring(3, 2), SmallDate.Substring(0, 2))
        Dim result As TimeSpan = DateNow.Subtract(DateDOB)
        Dim days As Integer = result.TotalDays
        'Dim NumberOfYears As Integer = days / 365
        'days = days - (NumberOfYears * 365)
        'Dim NumberOfMonths As Integer = days / 30
        'days = days - (NumberOfMonths * 30)
        'Dim Age As String = ""
        'If NumberOfYears = 0 Then
        'Else
        '    Age += NumberOfYears & " वर्ष "
        'End If
        'If NumberOfMonths = 0 Then
        'Else
        '    Age += NumberOfMonths & " माह "
        'End If
        'If days = 0 Then
        'Else
        '    Age += days & " दिन"
        'End If
        Dim ObjLib As New iDiary.clsLibrary
        Dim DayLimit As Integer = ObjLib.GetDayLimit(Request.QueryString("type"), lblClass.Text)
        'txtBookLimit.Text = ObjLib.GetBookLimit(cboCategory.SelectedIndex)
        Dim Fine As Double = ObjLib.GetAmountFine(Request.QueryString("type"), lblClass.Text)
        If days > DayLimit Then
            Fine = Fine * (days - DayLimit)
        Else
            Fine = 0
        End If
        Return Fine
    End Function
    Protected Sub btnRegNoNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegNoNext.Click
        lblStatus.Text = ""
        Dim ObjLib As New iDiary.clsLibrary
        If Request.QueryString("type") = 0 Then
            SearchStudentInfo()
            
        Else
            SearchTeacherInfo()
            
        End If

        lstBooks.Visible = True
        ObjLib.LoadBooksAsList(lstBooks, txtRegNo.Text)
        ObjLib = Nothing

    End Sub

    Private Sub SearchStudentInfo()
        If Trim(txtRegNo.Text).Length <= 0 Then
            lblStatus.Text = "Reg. No. is BLANK..."
            txtRegNo.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Select * From vw_Student Where RegNo='" & txtRegNo.Text & "' AND ASID=" & Request.Cookies("ASID").Value
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtStudentName.Text = myReader("SName")
            txtClassSection.Text = myReader("ClassName") & "/" & myReader("SecName")
            txtSID.Text = myReader("SID")
            lblClass.Text = FindMasterID(2, myReader("ClassName"))
        End While
        myReader.Close()
        
        
        txtDDEDOR.Text = ExpectedDate.Day
        txtMMEDOR.Text = ExpectedDate.Month
        txtYYEDOR.Text = ExpectedDate.Year

    End Sub

    Private Sub SearchTeacherInfo()
        If Trim(txtRegNo.Text).Length <= 0 Then
            lblStatus.Text = "Emp Code is BLANK..."
            txtAccNo.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Select EmpId,EmpName,DeptName,DesgName From vw_Employees Where EmpCode='" & txtRegNo.Text & "'"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtStudentName.Text = myReader("EmpName")
            txtClassSection.Text = myReader("DeptName") & "/" & myReader("DesgName")
            txtSID.Text = myReader("EmpId")
        End While
        myReader.Close()
        
        

        lblClass.Text = 0
    End Sub

    Protected Sub btnIssue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIssue.Click
        Dim memType As String = "S"
        Try
            If Request.QueryString("type") = 0 Then
                memType = "S"
            Else
                memType = "E"
            End If
        Catch ex As Exception

        End Try
        'Dim libMember As Integer = checkLibraryMembership(txtSID.Text, memType)
        'If libMember = 0 Then
        '    lblStatus.Text = "Library Membership not alloted."
        '    Exit Sub
        'ElseIf libMember = 2 Then
        '    lblStatus.Text = "Library Membership Blocked."
        '    Exit Sub
        'End If

        IssueBook()
    End Sub

    Private Sub IssueBook()
        If Trim(txtBookAccNo.Text).Length <= 0 Then
            lblStatus.Text = "Accession No. is BLANK..."
            txtAccNo.Focus()
            Exit Sub
        End If
        If Trim(txtRegNo.Text).Length <= 0 Then
            lblStatus.Text = lblCode.Text & " is BLANK..."
            txtRegNo.Focus()
            Exit Sub
        End If
        Dim ObjLib As New iDiary.clsLibrary
        Dim LimitType As Integer = 0
        LimitType = Request.QueryString("type")
        Dim BookLilimt As Integer = ObjLib.GetBookLimit(LimitType, lblClass.Text)
        ObjLib = Nothing
        If lstBooks.Items.Count >= BookLilimt Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Books Limit is Over');", True)
            txtRegNo.Focus()
            Exit Sub
        End If
       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Insert into BookTransact Values(" & _
        "'" & txtAccNo.Text & "'," & _
        "'" & txtRegNo.Text & "'," & _
        "'" & Now.Date.Month & "/" & Now.Date.Day & "/" & Now.Date.Year & "'," & _
        "'" & txtMMEDOR.Text & "/" & txtDDEDOR.Text & "/" & txtYYEDOR.Text & "'," & _
        "NULL," & _
        Request.Cookies("ASID").Value & "," & _
        Val(txtSID.Text) & "," & _
        0 & "," & _
        Request.QueryString("type") & ")"

        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "Update BookMaster Set Issued=1 Where AccNo='" & txtAccNo.Text & "'"

        
        
        ExecuteQuery_Update(SqlStr)

        Dim TempAccNo As String = txtBookAccNo.Text
        Dim TempUID As String = txtRegNo.Text

        
        

        InitControls()
        lblStatus.Text = "Accession No: " & TempAccNo & " issued to " & TempUID
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Library") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("AccessDenied.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        '0 for student
        '  else teacher
        If Request.QueryString("type") = 0 Then
            lblCode.Text = "Reg No"
            lblCourseDept.Text = "Class/Section"
            btnSearch.Visible = True
        Else
            lblCode.Text = "Emp Code"
            lblCourseDept.Text = "Dept/Desg"
            btnSearch.Visible = False
        End If
        txtAccNo.Text = ""
        txtTitle.Text = ""
        txtAuthor.Text = ""
        txtPub.Text = ""
        txtBookCat.Text = ""

        txtRegNo.Text = ""
        txtStudentName.Text = ""
        txtClassSection.Text = ""
        txtSID.Text = ""
        txtBookTransactID.Text = ""
        chkFine.Visible = False
        chkFine.Checked = False
        lblIssueDate.Text = Now.Date.ToString("dd/MM/yyyy")
        'txtDDEDOR.Text = ExpectedDate.Day
        'txtMMEDOR.Text = ExpectedDate.Month
        'txtYYEDOR.Text = ExpectedDate.Year
        lblClass.Text = 0
        lblActualReturnDate.Text = Now.Date.ToString("dd/MM/yyyy")
        lblFine.Text = ""
        lblFine.Visible = False
        txtRegNo.Enabled = False
        btnRegNoNext.Enabled = False
        txtStudentName.Enabled = False
        btnSearch.Enabled = False
        btnIssue.Enabled = False
        btnReturn.Enabled = False
        lstBooks.Visible = False
        txtDDEDOR.Enabled = True
        txtMMEDOR.Enabled = True
        txtYYEDOR.Enabled = True
        GridView1.Visible = False
        txtAccNo.Focus()
    End Sub

    Private Function NowDate() As String
        Return Now.Date.Day.ToString("00") & "/" & Now.Date.Month.ToString("00") & "/" & Now.Date.Year.ToString("0000")
    End Function

    Private Function ExpectedDate() As Date
        Dim ObjLib As New iDiary.clsLibrary
        If lblClass.Text = "" Then Return Now.Date
        Dim d As Integer = ObjLib.GetDayLimit(Request.QueryString("type"), lblClass.Text)
        ObjLib = Nothing
        Return Now.Date.AddDays(d)

    End Function

    Protected Sub btnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        If txtBookAccNo.Text.Length <= 0 Then
            lblStatus.Text = "Accession No is Blank..."
            txtBookAccNo.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        
        If chkFine.Checked = True Then
            sqlStr = "insert into libraryfine values('" & txtRegNo.Text & "'," & txtBookTransactID.Text & ",'" & Now.Month & "/" & Now.Day & "/" & Now.Year & "')"
            
            
            ExecuteQuery_Update(SqlStr)
        End If
        sqlStr = "Update BookTransact Set ActualReturnDate='" & Now.Year & "/" & Now.Month & "/" & Now.Day & "', Fine= " & GetFineAmount(lblIssueDate.Text, lblActualReturnDate.Text) & "" & _
        " Where AccNo='" & txtAccNo.Text & "' AND UniqueID='" & txtRegNo.Text & "' AND ASID=" & Request.Cookies("ASID").Value & _
        " AND ActualReturnDate is NULL"
        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "Update BookMaster Set Issued=0 Where AccNo='" & txtAccNo.Text & "'"
        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "select top(1) RackName from vw_bookmaster where BookAccNo='" & txtBookAccNo.Text & "'"
        
        
        lblStatus.Text = "Book return to Rack : " & ExecuteQuery_ExecuteScalar(SqlStr)

        
        

        InitControls()
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        txtRegNo.Text = GridView1.SelectedRow.Cells(1).Text
        GridView1.SelectedIndex = -1
        GridView1.Visible = False
        SearchStudentInfo()

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GridView1.Visible = True
        SqlDataSourceStudent.SelectCommand = "SELECT [RegNo], [SName], [ClassName], [SecName] FROM [vw_Student] WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtStudentName.Text & "%'"
        GridView1.DataBind()
    End Sub
End Class