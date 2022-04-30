
Partial Class ExamHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsPostBack = False Then
        '    InitControls()
        'Else
        '    If ViewState("myTable") = True Then
        '        CreateTable()
        '    End If
        'End If
    End Sub

    'Private Sub InitControls()
    '    LoadClasses()
    '    cboSection.Items.Clear()
    '    LoadTerms()
    '    LoadSubjects()
    '    cboExam.Items.Clear()
    '    LoadStatus()
    '    txtMaxMarks.Text = ""

    '    myTable.Rows.Clear()
    '    lblStatus.Text = ""

    '    cboClass.Focus()
    'End Sub

    'Private Sub LoadClasses()
    '    Dim sqlStr As String = "Select ClassName From Classes Where ClassGroupID=1"

    '    Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
    '    Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
    '    myConn.Open()

    '    Dim myCommand As New SqlCommand
    '    myCommand.CommandText = sqlStr
    '    myCommand.Connection = myConn
    '    Dim myReader As SqlDataReader = myCommand.ExecuteReader
    '    cboClass.Items.Clear()
    '    cboClass.Items.Add("")
    '    While myReader.Read
    '        cboClass.Items.Add(myReader(0))
    '    End While
    '    myReader.Close()
    '    myCommand.Dispose()
    '    myConn.Dispose()
    'End Sub

    'Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
    '    LoadClassSections()
    'End Sub

    'Private Sub LoadClassSections()
    '    Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
    '    Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
    '    myConn.Open()

    '    Dim sqlStr As String = "Select SecName From vw_Class_Section Where ClassName='" & cboClass.Text & "'"
    '    Dim myCommand As New SqlCommand
    '    myCommand.CommandText = sqlStr
    '    myCommand.Connection = myConn
    '    Dim myReader As SqlDataReader = myCommand.ExecuteReader
    '    cboSection.Items.Clear()
    '    cboSection.Items.Add("")
    '    While myReader.Read
    '        Try
    '            cboSection.Items.Add(myReader(0))
    '        Catch ex As Exception

    '        End Try
    '    End While
    '    myReader.Close()
    '    myCommand.Dispose()

    '    myConn.Dispose()
    'End Sub

    'Private Sub LoadTerms()
    '    cboTerm.Items.Clear()
    '    cboTerm.Items.Add("")
    '    cboTerm.Items.Add("1")
    '    cboTerm.Items.Add("2")
    'End Sub

    'Private Sub LoadStatus()
    '    Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
    '    Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
    '    myConn.Open()

    '    Dim sqlstr As String = "Select StatusName From StatusMaster"
    '    Dim myCommand As New SqlCommand(sqlstr, myConn)
    '    Dim myReader As SqlDataReader = myCommand.ExecuteReader
    '    cboStatus.Items.Clear()
    '    While myReader.Read
    '        cboStatus.Items.Add(myReader(0))
    '    End While
    '    myReader.Close()
    '    myCommand.Dispose()

    '    myConn.Dispose()
    'End Sub

    'Private Sub LoadExams()
    '    If cboTerm.SelectedIndex = 0 Then Exit Sub
    '    If cboSubject.SelectedIndex = 0 Then Exit Sub

    '    cboExam.Items.Clear()
    '    cboExam.Items.Add("")

    '    If cboTerm.Text.Contains("1") Then
    '        cboExam.Items.Add("FA1")
    '        cboExam.Items.Add("FA2")
    '        cboExam.Items.Add("SA1")
    '    ElseIf cboTerm.Text.Contains("2") Then
    '        cboExam.Items.Add("FA3")
    '        cboExam.Items.Add("FA4")
    '        cboExam.Items.Add("SA2")
    '    End If

    'End Sub

    'Private Sub LoadSubjects()
    '    cboSubject.Items.Clear()
    '    cboSubject.Items.Add("")
    '    cboSubject.Items.Add("01-English")
    '    cboSubject.Items.Add("02-Hindi")
    '    cboSubject.Items.Add("03-EVS")
    '    cboSubject.Items.Add("04-Mathematics")
    '    cboSubject.Items.Add("05-Social Science")
    '    cboSubject.Items.Add("06-Science")
    '    cboSubject.Items.Add("07-Moral Science")
    '    cboSubject.Items.Add("08-Gen Knowledge")
    '    cboSubject.Items.Add("09-Sanskrit")
    '    cboSubject.Items.Add("10-Computer")
    'End Sub

    'Protected Sub cboTerm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTerm.SelectedIndexChanged
    '    LoadExams()
    'End Sub

    'Protected Sub cboSubject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSubject.SelectedIndexChanged
    '    LoadExams()
    'End Sub

    'Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
    '    'Check Permissions
    '    Dim myObj As New ReportCardClass
    '    If Request.Cookies("UType").Value.ToString.Contains("Exam") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
    '        'Do Nothing (Allow)
    '    ElseIf myObj.CheckPermission(cboClass.Text, cboSection.Text, Request.Cookies("UID").Value) = False Then
    '        Response.Redirect("AccessDenied.aspx")
    '    End If
    '    myObj = Nothing

    '    '----Validation
    '    If cboClass.Text = "" Then
    '        lblStatus.Text = "Invalid Class..."
    '        cboClass.Focus()
    '        Exit Sub
    '    End If

    '    If cboSection.Text = "" Then
    '        lblStatus.Text = "Invalid Section..."
    '        cboSection.Focus()
    '        Exit Sub
    '    End If

    '    If cboTerm.Text = "" Or cboTerm.Text.Length <= 0 Or cboTerm.SelectedIndex = 0 Then
    '        lblStatus.Text = "Invalid Term..."
    '        cboTerm.Focus()
    '        Exit Sub
    '    End If

    '    If cboSubject.SelectedIndex = 0 Then
    '        lblStatus.Text = "Invalid Subject..."
    '        cboSubject.Focus()
    '        Exit Sub
    '    End If

    '    If cboExam.SelectedIndex = 0 Then
    '        lblStatus.Text = "Invalid Exam Type..."
    '        cboExam.Focus()
    '        Exit Sub
    '    End If

    '    lblStatus.Text = ""

    '    CreateTable()
    'End Sub

    'Private Function GetSID(ByVal myAdminNo As String) As Integer
    '    Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
    '    Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
    '    myConn.Open()

    '    Dim sqlStr As String = "Select Max(SID) From Student Where RegNo='" & myAdminNo & "' AND ASID=" & Request.Cookies("ASID").Value
    '    Dim myCommand As New SqlCommand(sqlStr, myConn)
    '    Dim rv As Integer = 0
    '    rv = myCommand.ExecuteScalar
    '    myCommand.Dispose()
    '    myConn.Dispose()
    '    Return rv
    'End Function

    'Private Sub CreateTable()
    '    myTable.Rows.Clear()

    '    Dim tr1 As New TableRow

    '    Dim td10 As New TableCell
    '    td10.Text = "<B>Sr. No.</B>"
    '    td10.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td10)

    '    Dim td11 As New TableCell
    '    td11.Text = "<B>Admission No.</B>"
    '    td11.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td11)

    '    Dim td12 As New TableCell
    '    td12.Text = "<B>&nbsp;&nbsp;&nbsp;Student Name</B>"
    '    td12.HorizontalAlign = HorizontalAlign.Left
    '    tr1.Cells.Add(td12)

    '    Dim td13 As New TableCell
    '    td13.Text = "<B>Max Marks</B>"
    '    td13.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td13)

    '    Dim td14 As New TableCell
    '    td14.Text = "<B>Marks</B>"
    '    td14.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td14)

    '    myTable.Rows.Add(tr1)

    '    Dim sqlStr As String = ""

    '    Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
    '    Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
    '    myConn.Open()

    '    Dim myCommand As New SqlCommand

    '    sqlStr = "Select RegNo, SName From vw_Student Where ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' Order By SName"
    '    myCommand.CommandText = sqlStr
    '    myCommand.Connection = myConn
    '    Dim myReader As SqlDataReader = myCommand.ExecuteReader
    '    Dim myTxtBoxNumber As Integer = 1
    '    While myReader.Read
    '        Dim trx As New TableRow

    '        Dim tdx0 As New TableCell
    '        tdx0.Text = myTxtBoxNumber
    '        tdx0.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdx0)

    '        Dim tdx1 As New TableCell
    '        tdx1.Text = myReader(0)
    '        tdx1.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdx1)

    '        Dim tdx2 As New TableCell
    '        tdx2.Text = "&nbsp;&nbsp;&nbsp;" & myReader(1)
    '        tdx2.HorizontalAlign = HorizontalAlign.Left
    '        trx.Cells.Add(tdx2)

    '        Dim txtMax As New TextBox()
    '        txtMax.ID = "txtMax" & myTxtBoxNumber
    '        txtMax.Width = 70
    '        txtMax.TabIndex = -1
    '        Dim tdx3 As New TableCell
    '        tdx3.Controls.Add(txtMax)
    '        tdx3.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdx3)

    '        Dim txtMarks As New TextBox()
    '        txtMarks.ID = "txtMarks" & myTxtBoxNumber
    '        txtMarks.Width = 70     'For Piyush -> to Make changes
    '        Dim tdx4 As New TableCell
    '        tdx4.Controls.Add(txtMarks)
    '        tdx4.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdx4)

    '        myTable.Rows.Add(trx)

    '        myTxtBoxNumber += 1
    '    End While
    '    myReader.Close()

    '    ChangeMaxMarks()

    '    Dim myCode As String = cboSubject.Text.Substring(0, 2)
    '    Dim myFields As String = cboExam.Text
    '    Dim i As Integer = 0
    '    For i = 1 To myTable.Rows.Count - 1

    '        Dim myAdminNo As String = myTable.Rows(i).Cells(1).Text

    '        sqlStr = "Select " & myFields & " From vw_Group1_MarksEntry_Academic Where RegNo='" & myAdminNo & "' AND TermNo=" & CInt(cboTerm.Text) & " AND SubjectCode='" & myCode & "'"
    '        myCommand.CommandText = sqlStr
    '        myCommand.Connection = myConn

    '        Dim myReader1 As SqlDataReader = myCommand.ExecuteReader
    '        While myReader1.Read

    '            Dim myMarksString = ""
    '            Dim myMaxMarks As String = ""
    '            Dim MyMarks As String = ""
    '            Try
    '                myMarksString = myReader1(0)
    '                'Find Location of First / and Last /"
    '                Dim Loc1 As Integer = myMarksString.IndexOf("/")

    '                MyMarks = myMarksString.Substring(0, Loc1)
    '                myMaxMarks = myMarksString.Substring(Loc1 + 1, myMarksString.Length - 1 - Loc1)

    '                CType(myTable.FindControl("txtMax" & i), TextBox).Text = myMaxMarks
    '                CType(myTable.FindControl("txtMarks" & i), TextBox).Text = MyMarks
    '            Catch ex As Exception
    '                myMarksString = ""
    '            End Try

    '        End While
    '        myReader1.Close()

    '    Next

    '    myCommand.Dispose()
    '    myConn.Dispose()
    '    myTable.EnableViewState = True
    '    ViewState("myTable") = True

    '    myTable.Focus()

    'End Sub

    'Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    Dim myVal As Integer = ValidateMarksAgainstMaxMarks()
    '    If myVal > 0 Then
    '        lblStatus.Text = "Invalid Marks...In place of blank, ZERO may be used"
    '        CType(myTable.FindControl("txtMarks" & myVal), TextBox).Focus()
    '        Exit Sub
    '    End If

    '    ValidateBlankRecords()

    '    '----Validate Non Numeric Records----------
    '    myVal = ValidateNonNumericRecords(1)
    '    If myVal > 0 Then
    '        lblStatus.Text = "Invalid Marks..."
    '        CType(myTable.FindControl("txtMax" & myVal), TextBox).Focus()
    '        Exit Sub
    '    End If

    '    myVal = ValidateNonNumericRecords(2)
    '    If myVal > 0 Then
    '        lblStatus.Text = "Invalid Marks..."
    '        CType(myTable.FindControl("txtMarks" & myVal), TextBox).Focus()
    '        Exit Sub
    '    End If
    '    '------------------------------------------

    '    Dim i As Integer = 0
    '    Dim sqlStr As String = ""
    '    Dim myCode As String = cboSubject.Text.Substring(0, 2)
    '    Dim SubjectName As String = cboSubject.Text.Substring(3, cboSubject.Text.Length - 3)

    '    For i = 1 To myTable.Rows.Count - 1

    '        Dim SID As Integer = GetSID(myTable.Rows(i).Cells(1).Text)

    '        'Format to Save Marks (Marks Obrained/MaxMarks/Grade) Example: 4.2/5/A1
    '        Dim myMarksToSave As String = CType(myTable.FindControl("txtMarks" & i), TextBox).Text & "/" & CType(myTable.FindControl("txtMax" & i), TextBox).Text
    '        If IsDBNull(myMarksToSave) Then myMarksToSave = ""

    '        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
    '        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
    '        myConn.Open()

    '        Dim myCommand As New SqlCommand

    '        sqlStr = "Select Count(*) From Group1_MarksEntry_Academic Where SID=" & SID & " AND TermNo=" & Val(cboTerm.Text) & " AND SubjectCode='" & myCode & "'"
    '        myCommand.CommandText = sqlStr
    '        myCommand.Connection = myConn
    '        Dim rv As Integer = myCommand.ExecuteScalar

    '        If rv <= 0 Then 'Insert Default Record Values
    '            sqlStr = "Insert into Group1_MarksEntry_Academic (SID,TermNo,SubjectCode, SubjectName) Values(" & SID & "," & CInt(cboTerm.Text) & ",'" & myCode & "','" & SubjectName & "')"
    '            myCommand.CommandText = sqlStr
    '            myCommand.Connection = myConn
    '            myCommand.ExecuteNonQuery()
    '        End If

    '        'Update Fields as per Subject Selected in Tree View and ID Obtained
    '        sqlStr = "Update Group1_MarksEntry_Academic Set " & cboExam.Text & "='" & myMarksToSave & "' " & _
    '        "Where SID=" & SID & " AND TermNo=" & Val(cboTerm.Text) & " AND SubjectCode='" & myCode & "'"

    '        myCommand.CommandText = sqlStr
    '        myCommand.Connection = myConn
    '        myCommand.ExecuteNonQuery()

    '        myCommand.Dispose()
    '        myConn.Dispose()
    '    Next

    '    System.Threading.Thread.Sleep(500)

    '    ViewState("myTable") = False
    '    InitControls()
    '    lblStatus.Text = "Marks Updated Successfully..."

    'End Sub

    'Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click
    '    ChangeMaxMarks()
    'End Sub

    'Private Sub ChangeMaxMarks()
    '    Dim i As Integer = 0
    '    For i = 1 To myTable.Rows.Count - 1
    '        CType(myTable.FindControl("txtMax" & i), TextBox).Text = txtMaxMarks.Text
    '    Next
    'End Sub

    'Private Function ValidateMarksAgainstMaxMarks() As Integer
    '    Dim i As Integer = 0
    '    Dim rv As Integer = -1 '-1: All Fine, >=0: Something Wrong

    '    For i = 1 To myTable.Rows.Count - 1
    '        Dim MM As Double = Val(CType(myTable.FindControl("txtMax" & i), TextBox).Text)
    '        Dim MF As Double = Val(CType(myTable.FindControl("txtMarks" & i), TextBox).Text)
    '        If MF > MM Then
    '            rv = i
    '            Exit For
    '        End If
    '    Next

    '    Return rv
    'End Function

    'Private Sub ValidateBlankRecords()
    '    Dim i As Integer = 0
    '    For i = 1 To myTable.Rows.Count - 1
    '        Dim MM As String = CType(myTable.FindControl("txtMax" & i), TextBox).Text
    '        If MM.Length <= 0 Or IsDBNull(MM) Or Trim(MM) = "" Then
    '            CType(myTable.FindControl("txtMax" & i), TextBox).Text = "00"
    '        End If
    '        Dim MF As String = CType(myTable.FindControl("txtMarks" & i), TextBox).Text
    '        If MF.Length <= 0 Or IsDBNull(MF) Or Trim(MF) = "" Then
    '            CType(myTable.FindControl("txtMarks" & i), TextBox).Text = "00"
    '        End If
    '    Next
    'End Sub

    'Private Function ValidateNonNumericRecords(ByVal MyType As Integer) As Integer  '1-Max Marks    2-Marks obtained
    '    Dim i As Integer = 0, rv As Integer = 0
    '    For i = 1 To myTable.Rows.Count - 1

    '        Select Case MyType

    '            Case 1
    '                If IsNumeric(CType(myTable.FindControl("txtMax" & i), TextBox).Text) = False Then
    '                    rv = i
    '                    Exit For
    '                End If

    '            Case 2

    '                If IsNumeric(CType(myTable.FindControl("txtMarks" & i), TextBox).Text) = False Then
    '                    rv = i
    '                    Exit For
    '                End If

    '        End Select

    '    Next
    '    Return rv
    'End Function
End Class

