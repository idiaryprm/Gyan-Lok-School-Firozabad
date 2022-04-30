Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports System.Drawing


Public Class Exam_MarksEntryStudent
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Exam") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("Login.aspx")
            End If

        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' lblGrpID.Text = Request.QueryString("grpID").ToString
        Session("ActiveTab") = 7
        If IsPostBack = False Then
            InitControls()
        Else
            'If ViewState("myTable") = True Then
            '    If lblInputType.Text = 1 Then   'grade
            '        CreateTableGradeEntry(lblGrpID.Text)
            '    ElseIf lblInputType.Text = 2 Then   'remark
            '        CreateTableRemarksEntry(lblGrpID.Text)
            '    Else
            '        CreateTableMarksEntry(lblGrpID.Text)    'marks
            '    End If

            'End If
        End If
    End Sub


    Private Sub InitControls()

       
        btnProceed.Visible = False
        btnSave.Visible = False
        lblStatus.Text = ""
        getMarksEntryExceptions()
        ' cboClass.Focus()
        'cboExamGroup.Focus()
        Try
            lblATT.Text = Request.QueryString("ATT").ToString
        Catch ex As Exception
            lblATT.Text = 0
        End Try
        gvmarks.Visible = False
    End Sub


    Private Sub CreateGVRemarkEntry(ByVal grpID As String)
        Dim sqlStr As String = "", subGrpID As Integer = 0
        sqlStr = "SELECT [SubjectId],DisplayType,EntryType, [SubjectCode], [SubjectName] FROM [vw_ExamSubjectMapping] Where ClassID='" & lblClassId.Text & "' AND SecID='" & lblSecID.Text & "' AND ASID=" & Request.Cookies("ASID").Value
        If cboSubSubjectGroup.SelectedItem.Text = "" Or cboSubSubjectGroup.SelectedItem.Text = "ALL" Then
            sqlStr &= " AND majorGroupID=" & cboSubjectGroup.SelectedItem.Value
        Else
            sqlStr &= " AND subGrpID=" & cboSubSubjectGroup.SelectedItem.Value
        End If
        sqlStr &= "  Order By DisplayOrder,subjectname"
        gvmarks.Visible = True
        GVCreateMarksEntry.SelectCommand = sqlStr
        gvmarks.DataBind()

    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        lblStatus.Text = ""
        Dim myVal As Integer = -1
        myVal = ValidateRecords()
        If myVal >= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Max Marks...');", True)
            lblStatus.Text = "Invalid Max Marks..."
            gvmarks.Rows(myVal).Cells(4).Focus()
            Exit Sub
        End If

        Dim SID As Integer = Val(lblSID.Text)
        Dim SubjectID As Integer = 0
        Dim termIDMaj As Integer = cboTerm.SelectedItem.Value
        Dim termIDMin As Integer = cboMinorTerm.SelectedItem.Value
        Dim inputType As Integer = 0
        Dim sqlStr As String = ""

        Dim obtM As String = "", maxM As String = "", grade As String = ""
        Dim marksExist As Integer = 0, marksFlag As Integer = 0, log1 As String = "", logType As String = ""
        For Each gvr As GridViewRow In gvmarks.Rows
            SubjectID = Convert.ToInt32(gvmarks.DataKeys(gvr.RowIndex).Values(0))
            inputType = Convert.ToInt32(gvmarks.DataKeys(gvr.RowIndex).Values(2))
            'If inputType = 0 Then
            '    Try
            '        If IsNothing(lstSID) Then
            '        Else
            '            If lstSID.Contains(SID) = False Then Continue For
            '        End If

            '    Catch ex As Exception

            '    End Try
            'End If
            Dim primID As String = SID & "-" & SubjectID & "-" & termIDMaj & "-" & termIDMin

            If inputType = 2 Then   'remark
                Try
                    obtM = DirectCast(gvr.FindControl("cboRemark"), DropDownList).SelectedItem.Value
                Catch ex As Exception
                    obtM = ""
                End Try
                maxM = "R"
                grade = obtM
            ElseIf inputType = 1 Then   'grade
                Try
                    obtM = DirectCast(gvr.FindControl("txtmarks"), TextBox).Text.ToUpper
                Catch ex As Exception
                    obtM = ""
                End Try
                maxM = "G"
                grade = obtM
            Else    'marks
                Try
                    maxM = DirectCast(gvr.FindControl("lblMaxM"), TextBox).Text
                Catch ex As Exception
                    maxM = ""
                End Try
                Try
                    obtM = DirectCast(gvr.FindControl("txtmarks"), TextBox).Text
                Catch ex As Exception
                    obtM = ""
                End Try
                Try
                    grade = DirectCast(gvr.FindControl("txtgrades"), TextBox).Text
                Catch ex As Exception
                    grade = ""
                End Try
            End If

            Dim marks As String = ""

            sqlStr = "Select Count(1) from ExamMarksEntry Where marksID='" & primID & "'"
            marksExist = ExecuteQuery_ExecuteScalar(sqlStr)
            If marksExist <= 0 Then
                sqlStr = "Insert into ExamMarksEntry (marksID,SID,SubjectID,MinorTermID,MajorTermID,obtM,maxM,grade) " & _
                    "Values('" & primID & "'," & SID & "," & SubjectID & "," & termIDMin & "," & termIDMaj & ",'" & obtM & "','" & maxM & "','" & grade & "')"
                ExecuteQuery_Update(sqlStr)
            Else
                marksFlag = 1
                sqlStr = "Update ExamMarksEntry Set obtM='" & obtM & "', maxM='" & maxM & "', grade='" & grade & "' Where marksID='" & primID & "'"
                ExecuteQuery_Update(sqlStr)
            End If
        Next
        'System.Threading.Thread.Sleep(500)

        If lblATT.Text = "1" Then
            lblStatus.Text = "Attendance Updated Successfully..."
            '   log1 = "Attendance for Class : " & cboClass.SelectedItem.Text & "-" & cboSection.SelectedItem.Text & ", Subject : " & cboSubjectGroup.SelectedItem.Text & "(" & cboSubject.SelectedItem.Text & ")" & ", Term : " & cboTerm.SelectedItem.Text & "-" & cboMinorTerm.SelectedItem.Text & " has been Feeded for " & cboStatus.SelectedItem.Text & " Students."
            logType = "ATTENDANCE"
        Else
            lblStatus.Text = "Marks Updated Successfully..."
            log1 = "Marks for the Student : " & txtSName.Text & ", Admn No. : " & txtRegNo.Text & ", Subject Group : " & cboSubjectGroup.SelectedItem.Text & ", Term : " & cboTerm.SelectedItem.Text & "-" & cboMinorTerm.SelectedItem.Text & " has been Feeded."
            logType = "MARKS"
        End If
        If marksFlag = 1 Then
            Save_Log(logType & " UPDATED", log1)
        Else
            Save_Log(logType & " INSERTED", log1)
        End If
        sqlStr = "Select CSSID from vw_examsubjectmapping Where ClassID=" & lblClassId.Text & " AND SecID=" & lblSecID.Text & " AND ASID=" & Request.Cookies("ASID").Value
        Dim CSSIDreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim sqlCSSID As String = "Update examsubjectmapping Set isProcessed=0 Where CSSID IN ("
        While CSSIDreader.Read
            sqlCSSID += CSSIDreader("CSSID") & ","
        End While
        CSSIDreader.Close()
        sqlCSSID = sqlCSSID.Substring(0, sqlCSSID.Length - 1)
        sqlCSSID += ")"
        ExecuteQuery_Update(sqlCSSID)

        btnProceed.Visible = False
        btnSave.Visible = False
        gvmarks.Visible = False
    End Sub

    Private Function MarkstoSave(ByVal marks As String, ByVal myMarksToSave As String) As String
        Dim outMarks As String = ""
        Dim termAr() As String = marks.Split("$")
        For i As Integer = 0 To termAr.Count - 1
            If termAr(i).Contains(cboMinorTerm.Text) Then
                outMarks &= cboMinorTerm.Text & "#" & myMarksToSave & "$"
                Continue For
            End If
            outMarks &= termAr(i) & "$"
        Next

        Return outMarks.Substring(0, outMarks.Length - 1)
    End Function

    Private Function MarkstoShow(ByVal marks As String) As String
        Dim outMarks As String = ""
        Dim termAr() As String = marks.Split("$")
        For i As Integer = 0 To termAr.Count - 1
            If termAr(i).Contains(cboMinorTerm.Text) Then
                outMarks = termAr(i).Split("#")(1)
                Exit For
            End If
            ' outMarks &= marks(i) & "$"
        Next

        Return outMarks
    End Function

    Private Function ValidateMarksAgainstMaxMarks() As Integer
        If lblInputType.Text = 1 Then Return 0
        Dim i As Integer = 0
        Dim rv As Integer = -1 '-1: All Fine, >=0: Something Wrong
        Dim MM As Double = 0
        Dim MF As Double = 0
        For Each gvr As GridViewRow In gvmarks.Rows
            Try
                MM = DirectCast(gvr.FindControl("lblMaxM"), Label).Text
            Catch ex As Exception
                MM = 0
            End Try
            Try
                MF = DirectCast(gvr.FindControl("txtmarks"), TextBox).Text
            Catch ex As Exception
                MF = 0
            End Try
            If MF > MM Then
                rv = gvr.RowIndex
                gvmarks.Rows(rv).BackColor = ColorTranslator.FromHtml("#FAA6A6")
            End If
        Next
        'For i = 1 To myTable.Rows.Count - 1
        '    Dim MM As Double = Val(CType(myTable.FindControl("txtMax" & i), TextBox).Text)
        '    Dim MF As Double = Val(CType(myTable.FindControl("txtMarks" & i), TextBox).Text)
        '    If MF > MM Then
        '        rv = i
        '        Exit For
        '    End If
        'Next

        Return rv
    End Function
    Private Function ValidateTotalAttendance() As Integer
        Dim i As Integer = 0
        Dim rv As Integer = -1 '-1: All Fine, >=0: Something Wrong
        Dim MM As Double = 0
        Dim MF As Double = 0
        Dim ML As Double
        For Each gvr As GridViewRow In gvmarks.Rows
            Try
                MM = DirectCast(gvr.FindControl("lblMaxM"), Label).Text
            Catch ex As Exception
                MM = 0
            End Try
            Try
                MF = DirectCast(gvr.FindControl("txtmarks"), TextBox).Text
            Catch ex As Exception
                MF = 0
            End Try
            Try
                ML = DirectCast(gvr.FindControl("txtgrades"), TextBox).Text
            Catch ex As Exception
                ML = 0
            End Try
            If MF + ML > MM Then
                rv = gvr.RowIndex
                gvmarks.Rows(rv).BackColor = ColorTranslator.FromHtml("#FAA6A6")
            End If
        Next

        'For i = 1 To myTable.Rows.Count - 1
        '    Dim MM As Double = Val(CType(myTable.FindControl("txtMax" & i), TextBox).Text)
        '    Dim MF As Double = Val(CType(myTable.FindControl("txtMarks" & i), TextBox).Text)
        '    Dim ML As Double = Val(CType(myTable.FindControl("txtGrade" & i), TextBox).Text)
        '    If MF + ML > MM Then
        '        rv = i
        '        Exit For
        '    End If
        'Next

        Return rv
    End Function

    Private Function ValidateRecords() As Integer
        Dim rv As Integer = -1
        Dim i As Integer = 0
        For Each gvr As GridViewRow In gvmarks.Rows
            Dim entryType As Integer = Convert.ToInt32(gvmarks.DataKeys(gvr.RowIndex).Values(2))
            Dim txtMaxM As TextBox = DirectCast(gvr.FindControl("lblMaxM"), TextBox)
            Dim txtGrade As TextBox = DirectCast(gvr.FindControl("txtgrades"), TextBox)
            Dim txtMarks As TextBox = DirectCast(gvr.FindControl("txtMarks"), TextBox)

            If entryType = 0 Then
                If Trim(txtMaxM.Text) = "" Then
                    txtMaxM.Text = "00"
                End If
                If Trim(txtMarks.Text) = "" Then
                    txtMarks.Text = "00"
                End If
                If Val(txtMarks.Text) > Val(txtMaxM.Text) Then
                    txtMarks.BackColor = Color.LightPink
                    rv = gvr.RowIndex
                Else
                    txtMarks.BackColor = Color.White
                    If txtMaxM.Text <> "00" Then
                        txtGrade.Text = getGrade(Val(txtMarks.Text), Val(txtMaxM.Text))
                    End If
                End If

            End If
            'Dim MM As String = DirectCast(gvr.FindControl("lblMaxM"), Label).Text
          
        Next
        Return rv
    End Function
    Private Sub ValidateBlankAttendance()
        Dim i As Integer = 0
        For Each gvr As GridViewRow In gvmarks.Rows
            Dim MM As String = DirectCast(gvr.FindControl("lblMaxM"), Label).Text
            If MM.Length <= 0 Or IsDBNull(MM) Or Trim(MM) = "" Then
                Dim lblMaxM As Label = DirectCast(gvr.FindControl("lblMaxM"), Label)
                lblMaxM.Text = "00"
            End If
            Dim MF As String = DirectCast(gvr.FindControl("txtmarks"), TextBox).Text
            If MF.Length <= 0 Or IsDBNull(MF) Or Trim(MF) = "" Then
                Dim txtmarks As TextBox = DirectCast(gvr.FindControl("txtmarks"), TextBox)
                txtmarks.Text = "00"
            End If
            Dim ML As String = DirectCast(gvr.FindControl("txtgrades"), TextBox).Text
            If ML.Length <= 0 Or IsDBNull(ML) Or Trim(ML) = "" Then
                Dim txtgrades As TextBox = DirectCast(gvr.FindControl("txtgrades"), TextBox)
                txtgrades.Text = "00"
            End If
        Next
    End Sub

    Private Function ValidateNonNumericRecords(ByVal MyType As Integer) As Integer  '1-Max Marks    2-Marks obtained
        If lblInputType.Text = 1 Then Return 0
        Dim i As Integer = 0, rv As Integer = -1
        For Each gvr As GridViewRow In gvmarks.Rows

            Select Case MyType

                Case 1
                    If IsNumeric(DirectCast(gvr.FindControl("lblMaxM"), Label).Text) = False Then
                        rv = gvr.RowIndex
                        gvmarks.Rows(rv).BackColor = ColorTranslator.FromHtml("#FAA6A6")
                    End If

                    '' check for marks entry of 32.5 for max marks 100
                    'If CType(myTable.FindControl("txtMax" & i), TextBox).Text = "100" And CType(myTable.FindControl("txtMarks" & i), TextBox).Text.Contains("32.5") Then
                    '    CType(myTable.FindControl("txtMarks" & i), TextBox).Text = "33"
                    'End If
                Case 2

                    If lstMax0.Contains((DirectCast(gvr.FindControl("txtmarks"), TextBox).Text.ToUpper)) = True Then
                        Dim lblMaxM As Label = DirectCast(gvr.FindControl("lblMaxM"), Label)
                        lblMaxM.Text = "0"
                    ElseIf lstObt0.Contains((DirectCast(gvr.FindControl("txtmarks"), TextBox).Text.ToUpper)) = True Then
                        Dim txtmarks As TextBox = DirectCast(gvr.FindControl("txtmarks"), TextBox)
                        txtmarks.Text = "AB"
                    Else
                        If IsNumeric(DirectCast(gvr.FindControl("txtmarks"), TextBox).Text) = False Then
                            rv = gvr.RowIndex
                            Exit For
                        End If
                    End If

            End Select

        Next
        Return rv
    End Function


    Protected Sub cboTerm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTerm.SelectedIndexChanged
        LoadSubjectGroupExamTermsMinor(cboMinorTerm, cboTerm.SelectedItem.Value, lblGrpID.Text, cboSubjectGroup.SelectedItem.Value)
        If cboMinorTerm.Items.Count > 1 Then
            cboMinorTerm.Enabled = True
        Else
            cboMinorTerm.Enabled = False
        End If
        cboTerm.Focus()
    End Sub

    Protected Sub cboMinorTerm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMinorTerm.SelectedIndexChanged
        cboMinorTerm.Focus()
    End Sub

    Protected Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click
        lblStatus.Text = ""
        Dim myVal As Integer = -1
        myVal = ValidateRecords()
        If myVal >= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Max Marks...');", True)
            lblStatus.Text = "Invalid Max Marks..."
            gvmarks.Rows(myVal).Cells(4).Focus()
            Exit Sub
        End If


        '  PerformMarksToGradeConversion()
        btnSave.Focus()
    End Sub
    Private Sub PerformMarksToGradeConversion()

        Dim i As Integer = 0
        For Each gvr As GridViewRow In gvmarks.Rows
            Dim txtgrades As TextBox = DirectCast(gvr.FindControl("txtgrades"), TextBox)
            txtgrades.Text = getGrade(DirectCast(gvr.FindControl("txtmarks"), TextBox).Text, DirectCast(gvr.FindControl("lblMaxM"), Label).Text)
        Next
        'For i = 1 To myTable.Rows.Count - 1
        '    CType(myTable.FindControl("txtGrade" & i), TextBox).Text = getGrade(CType(myTable.FindControl("txtMarks" & i), TextBox).Text, CType(myTable.FindControl("txtMax" & i), TextBox).Text)
        'Next

    End Sub
    Private Function getGrade(obtM As String, maxM As String) As String
        If maxM = 0 Then Return obtM
        If Not IsNumeric(obtM) Then
            Return obtM
        End If
        Dim marks As Double = 0
        Try
            marks = Math.Round(100 * CDbl(obtM) / CDbl(maxM), 2, MidpointRounding.AwayFromZero)
        Catch ex As Exception

        End Try
        Dim rv As String = "", UValue As Double = 0, LValue As Double = 0
        For i = 0 To gvGrade.Rows.Count - 1
            UValue = Val(gvGrade.Rows(i).Cells(0).Text)
            LValue = Val(gvGrade.Rows(i).Cells(1).Text)
            If marks >= LValue And marks <= UValue Then
                rv = gvGrade.Rows(i).Cells(2).Text
                Exit For
            End If
        Next

        Return rv
    End Function
    Shared lstSID As New List(Of Integer)
   
    Private Function getInputType(subID As Integer, ClassName As String, secName As String) As Integer
        Dim rv As Integer = 0
        Dim sqlStr As String = " Select top(1) EntryType From vw_ExamSubjectMapping Where SubjectID =" & subID & " AND ClassName='" & ClassName & "' AND SecNAme='" & secName & "'"
        rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Return rv
    End Function
    Protected Sub cboSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubjectGroup.SelectedIndexChanged
        'LoadSubjectGroups(cboSubSubjectGroup, 1)
        Try
            LoadMinorGroups(cboSubSubjectGroup, cboSubjectGroup.SelectedItem.Value)
            cboSubSubjectGroup.Items.Add(New ListItem("ALL", "0"))
        Catch ex As Exception

        End Try

        Try
            cboSubSubjectGroup.SelectedIndex = 1
        Catch ex As Exception

        End Try
        If cboSubSubjectGroup.Items.Count > 1 Then
            cboSubSubjectGroup.Enabled = True
        Else
            cboSubSubjectGroup.Enabled = False
        End If
        Dim subGrpID As Integer = 0
        If cboSubSubjectGroup.Text = "" Or cboSubSubjectGroup.Text = "ALL" Then
            subGrpID = cboSubjectGroup.SelectedItem.Value
        Else
            subGrpID = cboSubSubjectGroup.SelectedItem.Value
        End If
        Try
            '    LoadSubjectClassWise(cboSubject, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, Request.Cookies("ASID").Value, subGrpID)
        Catch ex As Exception

        End Try


        LoadSubjectGroupExamTermsMajor(cboTerm, cboSubjectGroup.SelectedItem.Value, lblGrpID.Text)
        Try
            LoadSubjectGroupExamTermsMinor(cboMinorTerm, cboTerm.SelectedItem.Value, lblGrpID.Text, cboSubjectGroup.SelectedItem.Value)
        Catch ex As Exception

        End Try
        If cboMinorTerm.Items.Count > 1 Then
            cboMinorTerm.Enabled = True
        Else
            cboMinorTerm.Enabled = False
        End If

        sdsGrade.SelectCommand = "SELECT [UpperValue], [LowerValue], [Grade] FROM [vw_ExamGradeMapping] where ExamgroupID=" & lblGrpID.Text & " and SubGrpID=" & cboSubjectGroup.SelectedItem.Value & " order by DisplayOrder  "
        gvGrade.DataBind()

        Try
            ' lblInputType.Text = getInputType(cboSubject.SelectedItem.Value, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text)
        Catch ex As Exception

        End Try
        If lblInputType.Text = "2" Then    'remark 
            '  LoadRemarks(cboRemark, cboSubject.SelectedItem.Value, cboClass.SelectedItem.Value)
        End If
        cboSubjectGroup.Focus()
    End Sub

    Private Sub cboSubSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubSubjectGroup.SelectedIndexChanged
        Dim subGrpID As Integer = 0
        If cboSubSubjectGroup.Text = "" Or cboSubSubjectGroup.Text = "ALL" Then
            subGrpID = cboSubjectGroup.SelectedItem.Value
        Else
            subGrpID = cboSubSubjectGroup.SelectedItem.Value
        End If
        Try
            'LoadSubjectClassWise(cboSubject, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, Request.Cookies("ASID").Value, subGrpID)
        Catch ex As Exception

        End Try

        'LoadSubjectsFromGroups(cboSubject, cboSubSubjectGroup.SelectedItem.Value)
        'LoadSubjectGroupExamTermsMajor(cboTerm, cboSubSubjectGroup.SelectedItem.Value, lblGrpID.Text)
        'LoadSubjectGroupExamTermsMinor(cboMinorTerm, cboSubjectGroup.SelectedItem.Value, lblGrpID.Text, cboSubjectGroup.SelectedItem.Value)
        Try
            '   lblInputType.Text = getInputType(cboSubject.SelectedItem.Value, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text)
        Catch ex As Exception

        End Try
        If lblInputType.Text = "2" Then    'remark 
            ' LoadRemarks(cboRemark, cboSubject.SelectedItem.Value, cboClass.SelectedItem.Value)
        End If
        '  cboSubSubjectGroup.Focus()
    End Sub

   
    Private Function getAttenddendaceGrpID() As Integer
        Dim sqlStr As String = ""
        sqlStr = "select MAX(subGrpID) from Examsubjectgroupmaster Where IsAttendanceType=1 AND ExamGroupID Like '%:" & lblGrpID.Text & ":%'"
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try
        Return rv
    End Function

    Shared lstObt0 As New List(Of String)
    Shared lstMax0 As New List(Of String)
    Private Sub getMarksEntryExceptions()
        lstMax0.Clear()
        lstObt0.Clear()
        Dim sqlStr As String = ""
        sqlStr = "select AbbName ,abbtype from ExamAbbreviation"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            If myReader(1) = 0 Then
                lstMax0.Add(myReader(0))
            Else
                lstObt0.Add(myReader(0))
            End If
        End While
    End Sub

    Private Sub getRemarks(myCbo As DropDownList)
        For i = 0 To cboRemark.Items.Count - 1
            myCbo.Items.Add(New ListItem(cboRemark.Items(i).Text, cboRemark.Items(i).Value))
        Next
    End Sub


    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        lblStatus.Text = ""
        If Trim(cboSubjectGroup.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Subject Group..');", True)
            lblStatus.Text = "Please select Subject Group.."
            cboSubjectGroup.Focus()
            Exit Sub
        End If
        If cboSubSubjectGroup.Enabled = True And Trim(cboSubjectGroup.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Sub Subject Group..');", True)
            lblStatus.Text = "Please select Sub Subject Group.."
            cboSubSubjectGroup.Focus()
            Exit Sub
        End If
        'If Trim(cboSubject.Text) = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Subject..');", True)
        '    lblStatus.Text = "Please select Subject.."
        '    cboSubject.Focus()
        '    Exit Sub
        'End If

        If Trim(cboTerm.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Term..');", True)
            lblStatus.Text = "Please select Term.."
            cboTerm.Focus()
            Exit Sub
        End If
        If cboMinorTerm.Enabled = True And Trim(cboMinorTerm.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Minor Term..');", True)
            lblStatus.Text = "Please select Minor Term.."
            cboMinorTerm.Focus()
            Exit Sub
        End If
       
        ''check for permissions
        If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            'If CheckExamConfig(4) Then
            '    If CheckPermissionSubject(cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, cboSubject.SelectedItem.Text, Request.Cookies("UID").Value) Then
            '        btnProceed.Visible = True
            '        btnSave.Visible = True
            '    Else
            '        btnProceed.Visible = False
            '        btnSave.Visible = False
            '        gvmarks.Visible = False
            '        lblStatus.Text = "Access Denied. You are not authorized for this entry."
            '        Exit Sub
            '    End If
            'End If
        End If

        'If CheckExamConfig(4) AndAlso CheckPermissionSubject(cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, cboSubject.SelectedItem.Text, Request.Cookies("UID").Value) Then
        '    btnProceed.Visible = True
        '    btnSave.Visible = True
        'Else
        '    btnProceed.Visible = False
        '    btnSave.Visible = False
        '    gvmarks.Visible = False
        '    lblStatus.Text = "Access Denied. You are not authorized for this entry."
        '    Exit Sub
        'End If

        'If lblInputType.Text = 1 Then
        '    lblMaxMarks.Text = "Enter Grade"
        '    CreateGVGradeEntry(lblGrpID.Text)     'Subject for Grade Entry
        '    txtMaxMarks.Focus()
        'ElseIf lblInputType.Text = 2 Then
        '    lblMaxMarks.Text = "Enter Remarks"
        '    CreateGVRemarkEntry(lblGrpID.Text)
        'Else
        '    If lblATT.Text = "1" Then
        '        lblMaxMarks.Text = "Total Days"
        '    Else
        '        lblMaxMarks.Text = "Max Marks"
        '    End If
        '    CreateGVMarksEntry(lblGrpID.Text)
        '    txtMaxMarks.Focus()
        '    ' CreateTableMarksEntry(lblGrpID.Text)
        'End If

        CreateGVRemarkEntry(lblGrpID.Text)
        '    txtMaxMarks.Focus()
        'If txtMaxMarks.Enabled = True Then
        '    txtMaxMarks.Focus()
        'Else
        'myTable.Focus()
        'End If
        If lblATT.Text = "1" Then
            btnProceed.Visible = False
        Else
            btnProceed.Visible = True
        End If

        btnSave.Visible = True
        '  txtMaxMarks.Enabled = True
        gvmarks.Visible = True
    End Sub

    Private Sub gvmarks_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvmarks.RowDataBound
        'If lblInputType.Text = "2" Then
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim SubjectID As Integer = Convert.ToInt32(gvmarks.DataKeys(e.Row.RowIndex).Values(0))
            Dim displayType As Integer = Convert.ToInt32(gvmarks.DataKeys(e.Row.RowIndex).Values(1))
            Dim entryType As Integer = Convert.ToInt32(gvmarks.DataKeys(e.Row.RowIndex).Values(2))
            Dim termIDMaj As Integer = cboTerm.SelectedItem.Value
            Dim termIDMin As Integer = cboMinorTerm.SelectedItem.Value
            Dim sqlStr As String = ""

            Dim SID As Integer = lblSID.Text
            Dim primID As String = SID & "-" & SubjectID & "-" & termIDMaj & "-" & termIDMin
           
            sqlStr = "Select obtM,maxM,Grade From ExamMarksEntry Where marksID='" & primID & "'"

            Dim myReader1 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            Dim txtMaxM As TextBox = DirectCast(e.Row.FindControl("lblMaxM"), TextBox)
            Dim txtGrade As TextBox = DirectCast(e.Row.FindControl("txtgrades"), TextBox)
            Dim txtMarks As TextBox = DirectCast(e.Row.FindControl("txtMarks"), TextBox)
            txtMarks.Visible = False
            Dim cboRemark As DropDownList = DirectCast(e.Row.FindControl("cboRemark"), DropDownList)
            cboRemark.Visible = True

            While myReader1.Read

                Dim myMarksString = ""
                Dim myMaxMarks As String = ""
                Dim MyMarks As String = ""
                Dim MyGrade As String = ""


                cboRemark.Attributes.Add("onkeypress", "return tabE(this,event)")
                Try
                    MyMarks = myReader1(0)
                    txtMarks.Text = MyMarks

                Catch ex As Exception

                End Try
                txtMaxM.Text = myReader1(1)
                txtGrade.Text = myReader1(2)

            End While
            myReader1.Close()

            If entryType = 2 Then
                cboRemark.Visible = True
                txtMarks.Visible = False
                LoadRemarks(cboRemark, SubjectID, lblClassId.Text)
                cboRemark.ClearSelection()
                Try
                    cboRemark.Items.FindByValue(txtMarks.Text).Selected = True
                Catch ex As Exception

                End Try

                txtMaxM.Visible = False
                txtGrade.Visible = False
            ElseIf entryType = 1 Then
                cboRemark.Visible = False
                txtMarks.Visible = True
                txtMaxM.Visible = False
                txtGrade.Visible = False
            Else
                cboRemark.Visible = False
                txtMarks.Visible = True
                txtMaxM.Visible = True
                txtGrade.Visible = True
            End If


        End If
        ' End If
    End Sub
    Private Sub Save_Log(ByVal type As String, log1 As String)
        Dim sqlStr As String = ""

        ' log1 += " #### " & "RegNo : " & txtSRNo.Text & ", Name : " & txtName.Text & ", Class : " & cboClass.Text & " Section : " & cboSection.Text
        sqlStr = "Insert Into Event_log(logTime,EventType,Details,UserId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        ExecuteQuery_Update(sqlStr)
    End Sub

    Protected Sub btnSName_Click(sender As Object, e As EventArgs) Handles btnSName.Click
        GridView1.Visible = True
        SqlDataSource1.SelectCommand = "SELECT RegNo, SName, ClassName, SecName,classrollNo, FName, MName, AdmissionDate, DOB FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " and SName Like '%" & txtSName.Text & "%'"
        GridView1.DataBind()
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        txtRegNo.Text = GridView1.SelectedRow.Cells(1).Text
        FillStudentDetail(txtRegNo.Text)
        GridView1.SelectedIndex = -1
        GridView1.Visible = False
    End Sub
    Private Sub FillStudentDetail(regno As String)
        Dim sqlStr As String = ""

        sqlStr = "Select regno,classRollno, SName, FName, MName, ClassName,SecName,SID,ClassID,SecID,ExamGroupID From vw_Student Where ASID=" & Request.Cookies("ASID").Value
        sqlStr += " and Regno='" & regno & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            lblSID.Text = myReader("SID")
            txtRegNo.Text = myReader("regno")
            txtSName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            txtRollNo.Text = myReader("classRollno")
            txtClass.Text = myReader("ClassName") & "-" & myReader("SecName")
            lblClassId.Text = myReader("ClassID")
            lblSecID.Text = myReader("SecID")
            lblGrpID.Text = myReader("ExamGroupID")
        End While
        myReader.Close()

        LoadSubjectGroups(cboSubjectGroup, 0, lblGrpID.Text)
        Dim attGrpID As Integer = getAttenddendaceGrpID()
        If lblATT.Text = "1" Then
            ' lblMaxMarks.Text = "Total Days"
            cboSubjectGroup.ClearSelection()

            cboSubjectGroup.Items.FindByValue(attGrpID).Selected = True
            cboSubjectGroup.Enabled = False
            cboSubSubjectGroup.Enabled = False
            LoadSubjectGroupExamTermsMajor(cboTerm, cboSubjectGroup.SelectedItem.Value, lblGrpID.Text)
            Try
                LoadSubjectGroupExamTermsMinor(cboMinorTerm, cboTerm.SelectedItem.Value, lblGrpID.Text, cboSubjectGroup.SelectedItem.Value)
            Catch ex As Exception

            End Try
            If cboMinorTerm.Items.Count > 1 Then
                cboMinorTerm.Enabled = True
            Else
                cboMinorTerm.Enabled = False
            End If
        Else
            cboSubjectGroup.Items.Remove(cboSubjectGroup.Items.FindByValue(attGrpID))
            cboSubjectGroup.Enabled = True
            cboSubSubjectGroup.Enabled = True
        End If
      

    End Sub

    Protected Sub btnRegNo_Click(sender As Object, e As EventArgs) Handles btnRegNo.Click
        FillStudentDetail(txtRegNo.Text)
    End Sub

    Protected Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        Dim i As Integer = 0

        For Each gvr As GridViewRow In gvmarks.Rows 'Marks
            Dim entryType As Integer = Convert.ToInt32(gvmarks.DataKeys(gvr.RowIndex).Values(2))
            Dim lblMaxM As TextBox = DirectCast(gvr.FindControl("lblMaxM"), TextBox)
            If entryType = 0 And IsNumeric(txtMaxMarks.Text) = True Then
                lblMaxM.Text = txtMaxMarks.Text
            ElseIf entryType = 1 Then 'Grades
                Dim txtmarks As TextBox = DirectCast(gvr.FindControl("txtmarks"), TextBox)
                txtmarks.Text = txtMaxMarks.Text
            Else   'Remarks
                Dim txtmarks As TextBox = DirectCast(gvr.FindControl("txtmarks"), TextBox)
                txtmarks.Text = txtMaxMarks.Text
            End If

        Next
        gvmarks.Focus()
    End Sub
End Class