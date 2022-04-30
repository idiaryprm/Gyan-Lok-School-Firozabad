Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports System.Drawing


Public Class Exam_MarksEntry
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
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)
        ' Literal1.Text = "Marks Entry : " & Request.QueryString("grpSubject")
        cboSection.Items.Clear()
        '  LoadMasterInfo(18, cboTerm)
        cboSubject.Items.Clear()
        LoadMasterInfo(10, cboStatus)
        myTable.Rows.Clear()
        txtMaxMarks.Text = ""
        btnProceed.Visible = False
        btnSave.Visible = False
        btnDelete.Visible = False
        lblStatus.Text = ""
        getMarksEntryExceptions()
        ' cboClass.Focus()
        cboExamGroup.Focus()
        Try
            lblATT.Text = Request.QueryString("ATT").ToString
        Catch ex As Exception
            lblATT.Text = 0
        End Try
        Try
            lblActivitySubID.Text = getActivitySubType()
        Catch ex As Exception
            lblActivitySubID.Text = ""
        End Try
        gvmarks.Visible = False
    End Sub
    Private Function getActivitySubType() As String
        Dim rv As String = ""
        rv = ExecuteQuery_ExecuteScalar("SELECT [ActivitySubjectGroupID] FROM [ExamParams]")
        Return rv
    End Function
    Private Function getHealthSubType() As String
        Dim rv As String = ""
        rv = ExecuteQuery_ExecuteScalar("SELECT [HealthSubjectGroupID] FROM [ExamParams]")
        Return rv
    End Function
    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSections(cboClass.SelectedItem.Text, cboSection)
        cboClass.Focus()
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSection.SelectedIndexChanged
        myTable.Rows.Clear()
        Dim subGrpID As Integer = 0
        If cboSubSubjectGroup.Text = "" Then
            subGrpID = cboSubjectGroup.SelectedItem.Value
        Else
            subGrpID = cboSubSubjectGroup.SelectedItem.Value
        End If
        LoadSubjectClassWise(cboSubject, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, Request.Cookies("ASID").Value, subGrpID, 0, lblSchoolID.Text)
        If cboSubject.Items.Count = 1 Then
            cboSubject.Enabled = False
        Else
            cboSubject.Enabled = True
        End If
        Try
            lblInputType.Text = getInputType(cboSubject.SelectedItem.Value, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, lblSchoolID.Text)
        Catch ex As Exception

        End Try
        If lblInputType.Text = "2" Then    'remark 
            LoadRemarks(cboRemark, cboSubject.SelectedItem.Value, cboClass.SelectedItem.Value)
        End If
        If Val(lblActivitySubID.Text) = cboSubjectGroup.SelectedItem.Value Then
            lstSID = getMappedSIDwithActivity(cboSubject.SelectedItem.Value, cboSubSubjectGroup.SelectedItem.Value)
        Else
            If checkSubSectionExists(cboClass.SelectedItem.Value, cboSection.SelectedItem.Value) > 0 Then
                lstSID = getMappedSIDwithSubjects(cboClass.SelectedItem.Value, cboSection.SelectedItem.Value, cboSubject.SelectedItem.Value)
            Else
                lstSID = Nothing
            End If

        End If
        'If checkSubSectionExists(cboClass.SelectedItem.Value, cboSection.SelectedItem.Value) > 0 Then
        '    lstSID = getMappedSIDwithSubjects(cboClass.SelectedItem.Value, cboSection.SelectedItem.Value, cboSubject.SelectedItem.Value)
        'Else
        '    lstSID = Nothing
        'End If
        cboSection.Focus()
    End Sub

    Public Function GetSID(ByVal myAdminNo As String) As Integer
        Dim sqlStr As String = "Select Max(SID) From Student Where RegNo='" & myAdminNo & "' AND ASID=" & Request.Cookies("ASID").Value
        Dim rv As Integer = 0
        rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Return rv
    End Function

    Private Sub CreateGVRemarkEntry(ByVal grpID As String)

        If lblInputType.Text = "2" Then
            gvmarks.Columns(2).HeaderText = "Class Roll No"

        End If
        If lblInputType.Text = "2" Then
            gvmarks.Columns(5).HeaderText = "Remarks"
        End If
        gvmarks.Columns(4).Visible = False
        gvmarks.Columns(6).Visible = False
        gvmarks.Visible = True
        GVCreateMarksEntry.SelectCommand = "Select SID,RegNo,ClassRollNo, SName From vw_Student Where ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' and SchoolName='" & cboSchoolName.SelectedItem.Text & "' Order By Convert(int,ClassRollNo),sname"
        gvmarks.DataBind()

    End Sub

    Private Sub writeRemark(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cbo As DropDownList
        cbo = CType(sender, DropDownList)
        Dim txtRID As String = cbo.ID.Substring(4, cbo.ID.Length - 4)
        If cbo.SelectedItem.Text = "Other" Then
            CType(myTable.FindControl("txtR" & Convert.ToInt32(txtRID)), TextBox).Visible = True
        Else
            CType(myTable.FindControl("txtR" & Convert.ToInt32(txtRID)), TextBox).Visible = False
        End If

    End Sub

    Private Sub CreateGVGradeEntry(ByVal grpID As String)
        If lblInputType.Text = "1" Then
            gvmarks.Columns(1).HeaderText = "Admission No"

        End If
        If lblInputType.Text = "1" Then
            If Val(lblIsHealthType.Text) > 0 Then
                gvmarks.Columns(5).HeaderText = cboSubject.SelectedItem.Text
            Else
                gvmarks.Columns(5).HeaderText = "Grade"
            End If
        End If
        gvmarks.Columns(4).Visible = False
        gvmarks.Columns(6).Visible = False
        gvmarks.Visible = True
        GVCreateMarksEntry.SelectCommand = "Select SID,RegNo,ClassRollNo, SName From vw_Student Where ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' and SchoolName='" & cboSchoolName.SelectedItem.Text & "' Order By Convert(int,ClassRollNo),sname"
        gvmarks.DataBind()

        Dim SubjectID As Integer = cboSubject.SelectedItem.Value
        Dim termIDMaj As Integer = cboTerm.SelectedItem.Value
        Dim termIDMin As Integer = cboMinorTerm.SelectedItem.Value
        Dim sqlStr As String = ""
        For Each gvr As GridViewRow In gvmarks.Rows
            Dim SID As Integer = gvmarks.DataKeys(gvr.RowIndex).Value.ToString()
            Dim primID As String = SID & "-" & SubjectID & "-" & termIDMaj & "-" & termIDMin
            sqlStr = "Select obtM,maxM,grade From ExamMarksEntry Where marksID='" & primID & "'"

            Dim myReader1 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

            While myReader1.Read

                Dim myMarksString = ""
                Dim myMaxMarks As String = ""
                Dim MyMarks As String = ""
                Dim MyGrade As String = ""
                Dim lblMaxM As Label = DirectCast(gvr.FindControl("lblMaxM"), Label)
                Try
                    lblMaxM.Text = myReader1(1)
                Catch ex As Exception
                    lblMaxM.Text = ""
                End Try
                Dim txtmarks As TextBox = DirectCast(gvr.FindControl("txtmarks"), TextBox)
                txtmarks.Attributes.Add("onkeypress", "return tabE(this,event)")
                Try
                    txtmarks.Text = myReader1(0)
                Catch ex As Exception
                    txtmarks.Text = ""
                End Try
                Dim txtgrades As TextBox = DirectCast(gvr.FindControl("txtgrades"), TextBox)
                Try
                    txtgrades.Text = myReader1(2)
                Catch ex As Exception
                    txtgrades.Text = ""
                End Try

            End While
            myReader1.Close()

        Next

    End Sub


    Private Sub CreateGVMarksEntry(ByVal grpID As String)

        If lblATT.Text = "1" Then
            gvmarks.Columns(4).HeaderText = "Total Days"
            gvmarks.Columns(5).HeaderText = "Present Days"
            gvmarks.Columns(6).HeaderText = "Medical"
        Else
            gvmarks.Columns(4).HeaderText = "Max Marks"
            gvmarks.Columns(5).HeaderText = "Marks"
            gvmarks.Columns(6).HeaderText = "Grades"
        End If


        gvmarks.Columns(4).Visible = True
        gvmarks.Columns(6).Visible = True
        gvmarks.Visible = True
        GVCreateMarksEntry.SelectCommand = "Select SID,RegNo,ClassRollNo, SName From vw_Student Where ClassID='" & cboClass.SelectedItem.Value & "' AND SecID='" & cboSection.SelectedItem.Value & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' and SchoolName='" & cboSchoolName.SelectedItem.Text & "' Order By Convert(int,ClassRollNo),sname"
        gvmarks.DataBind()
        Dim SubjectID As Integer = cboSubject.SelectedItem.Value
        Dim termIDMaj As Integer = cboTerm.SelectedItem.Value
        Dim termIDMin As Integer = cboMinorTerm.SelectedItem.Value
        Dim sqlStr As String = ""
        Dim lblMaxM As Label
        Dim txtMarks As TextBox
        Dim txtGrades As TextBox
        For Each gvr As GridViewRow In gvmarks.Rows
            Dim SID As Integer = gvmarks.DataKeys(gvr.RowIndex).Value.ToString()
            Dim primID As String = SID & "-" & SubjectID & "-" & termIDMaj & "-" & termIDMin
            sqlStr = "Select obtM,maxM,grade From ExamMarksEntry Where marksID='" & primID & "'"

            Dim myReader1 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            lblMaxM = DirectCast(gvr.FindControl("lblMaxM"), Label)
            txtMarks = DirectCast(gvr.FindControl("txtmarks"), TextBox)
            txtGrades = DirectCast(gvr.FindControl("txtgrades"), TextBox)
            While myReader1.Read

                Dim myMarksString = ""
                Dim myMaxMarks As String = ""
                Dim MyMarks As String = ""
                Dim MyGrade As String = ""

                Try
                    lblMaxM.Text = myReader1(1)
                Catch ex As Exception
                    lblMaxM.Text = ""
                End Try

                Try
                    txtmarks.Text = myReader1(0)
                Catch ex As Exception
                    txtmarks.Text = "0"
                End Try

                Try
                    txtgrades.Text = myReader1(2)
                Catch ex As Exception
                    txtgrades.Text = ""
                End Try

            End While
            myReader1.Close()

            If lblATT.Text = "1" Then
                txtMarks.Attributes.Add("onkeypress", "return tabE(this,event)")
                txtGrades.Attributes.Add("onkeypress", "return tabE(this,event)")
            Else
                txtGrades.Enabled = False
                txtMarks.Attributes.Add("onkeypress", "return tabEnter(this,event)")
                txtGrades.Attributes.Add("onkeypress", "return tabEnter(this,event)")
            End If
            'If lblATT.Text = "1" Then
            '    lblMaxM.Enabled = True
            '    txtMarks.Attributes.Add("onkeypress", "return tabE(this,event)")
            '    txtGrades.Attributes.Add("onkeypress", "return tabE(this,event)")
            'Else
            '    lblMaxM.Enabled = False
            '    txtGrades.Enabled = False
            '    txtMarks.Attributes.Add("onkeypress", "return tabEnter(this,event)")
            '    txtGrades.Attributes.Add("onkeypress", "return tabEnter(this,event)")
            'End If

        Next
        ' If lblATT.Text = "1" Then
        '    For Each gvr As GridViewRow In gvmarks.Rows
        '        Dim txtmarks As TextBox = DirectCast(gvr.FindControl("txtmarks"), TextBox)
        '        Dim txtgrades As TextBox = DirectCast(gvr.FindControl("txtgrades"), TextBox)
        '        txtmarks.Attributes.Add("onkeypress", "return tabE(this,event)")
        '        txtgrades.Attributes.Add("onkeypress", "return tabE(this,event)")
        '    Next
        'Else
        '    For Each gvr As GridViewRow In gvmarks.Rows
        '        Dim txtgrades As TextBox = DirectCast(gvr.FindControl("txtgrades"), TextBox)
        '        Dim txtmarks As TextBox = DirectCast(gvr.FindControl("txtmarks"), TextBox)

        '        '    txtgrades.Enabled = False
        '        txtmarks.Attributes.Add("onkeypress", "return tabE(this,event)")

        '    Next
        'End If

    End Sub


    Private Sub ChangeMaxMarks()
        Dim i As Integer = 0
        For i = 1 To myTable.Rows.Count - 1
            '  CType(myTable.FindControl("txtMax" & i), TextBox).Text = txtMaxMarks.Text
        Next
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        lblStatus.Text = ""
        Dim myVal As Integer = 0
        Dim message As String = ""
        If lblInputType.Text = 0 Then
            '.........................vikash..........................................
            If lblATT.Text = "1" Then
                ValidateBlankAttendance()
            Else
                ValidateBlankRecords()
            End If

            myVal = ValidateNonNumericRecords(2)
            If myVal >= 0 Then
                If lblATT.Text = "1" Then
                    message = "Invalid Present Days."
                Else
                    message = "Invalid Obtained Marks..."
                End If

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & message & "');", True)
                lblStatus.Text = message
                gvmarks.Rows(myVal).Cells(5).Focus()
                Exit Sub
            End If
            '------------------------------------------
            myVal = ValidateMarksAgainstMaxMarks()
            If myVal >= 0 Then
                If lblATT.Text = "1" Then
                    message = "Invalid Present Days! Present days can not be greated than Total days..."
                Else
                    message = "Invalid Marks! Obtained Marks can not be greated than Max. Marks..."
                End If

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & message & "');", True)
                lblStatus.Text = message
                gvmarks.Rows(myVal).Cells(5).Focus()
                Exit Sub
            End If

            If lblATT.Text = "1" Then
                myVal = ValidateTotalAttendance()
                If myVal >= 0 Then
                    message = "Invalid Entry! Present days + Medical leaves can not be greater than Total days..."
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Entry! Present days + Medical leaves can not be greater than Total days...');", True)
                    lblStatus.Text = message
                    gvmarks.Rows(myVal).Cells(5).Focus()
                    Exit Sub
                End If
            Else
                PerformMarksToGradeConversion()
            End If
        ElseIf lblInputType.Text = 1 Then
            If Val(lblIsHealthType.Text) > 0 Then
            Else
                myVal = ValidateGrades()
                If myVal >= 0 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Grdae...');", True)
                    lblStatus.Text = "Invalid Grade Marks..."
                    gvmarks.Rows(myVal).Cells(5).Focus()
                    Exit Sub
                End If
            End If
        End If

        Dim SubjectID As Integer = cboSubject.SelectedItem.Value
        Dim termIDMaj As Integer = cboTerm.SelectedItem.Value
        Dim termIDMin As Integer = cboMinorTerm.SelectedItem.Value

        Dim sqlStr As String = ""

        Dim obtM As String = "", maxM As String = "", grade As String = ""
        Dim marksExist As Integer = 0, marksFlag As Integer = 0, log1 As String = "", logType As String = ""
        For Each gvr As GridViewRow In gvmarks.Rows
            Dim SID As Integer = gvmarks.DataKeys(gvr.RowIndex).Value.ToString()
            If Val(lblActivitySubID.Text) = cboSubjectGroup.SelectedItem.Value Then
                Try
                    If IsNothing(lstSID) Then
                    Else
                        If lstSID.Contains(SID) = False Then Continue For
                    End If

                Catch ex As Exception

                End Try
            Else
                If lblInputType.Text = 0 Then
                    Try
                        If IsNothing(lstSID) Then
                        Else
                            If lstSID.Contains(SID) = False Then Continue For
                        End If

                    Catch ex As Exception

                    End Try
                End If
            End If

            Dim primID As String = SID & "-" & SubjectID & "-" & termIDMaj & "-" & termIDMin

            If lblInputType.Text = 2 Then   'remark
                Try
                    obtM = DirectCast(gvr.FindControl("cboRemark"), DropDownList).SelectedItem.Value
                Catch ex As Exception
                    obtM = ""
                End Try
                maxM = "R"
                grade = obtM
            ElseIf lblInputType.Text = 1 Then   'grade
                Try
                    obtM = DirectCast(gvr.FindControl("txtmarks"), TextBox).Text.ToUpper
                Catch ex As Exception
                    obtM = ""
                End Try
                maxM = "G"
                grade = obtM
            Else    'marks
                Try
                    maxM = DirectCast(gvr.FindControl("lblMaxM"), Label).Text
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
            log1 = "Attendance for Class : " & cboClass.SelectedItem.Text & "-" & cboSection.SelectedItem.Text & ", Subject : " & cboSubjectGroup.SelectedItem.Text & "(" & cboSubject.SelectedItem.Text & ")" & ", Term : " & cboTerm.SelectedItem.Text & "-" & cboMinorTerm.SelectedItem.Text & " has been Feeded for " & cboStatus.SelectedItem.Text & " Students."
            logType = "ATTENDANCE"
        Else
            lblStatus.Text = "Marks Updated Successfully..."
            log1 = "Marks for Class : " & cboClass.SelectedItem.Text & "-" & cboSection.SelectedItem.Text & ", Subject : " & cboSubjectGroup.SelectedItem.Text & "(" & cboSubject.SelectedItem.Text & ")" & ", Term : " & cboTerm.SelectedItem.Text & "-" & cboMinorTerm.SelectedItem.Text & " has been Feeded for " & cboStatus.SelectedItem.Text & " Students."
            logType = "MARKS"
        End If
        If marksFlag = 1 Then
            Save_Log(logType & " UPDATED", log1)
        Else
            Save_Log(logType & " INSERTED", log1)
        End If
        sqlStr = "Select CSSID from vw_examsubjectmapping Where ClassID=" & cboClass.SelectedItem.Value & " AND SecID=" & cboSection.SelectedItem.Value & " AND ASID=" & Request.Cookies("ASID").Value
        Dim CSSIDreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim sqlCSSID As String = "Update examsubjectmapping Set isProcessed=0 Where CSSID IN ("
        While CSSIDreader.Read
            sqlCSSID += CSSIDreader("CSSID") & ","
        End While
        CSSIDreader.Close()
        sqlCSSID = sqlCSSID.Substring(0, sqlCSSID.Length - 1)
        sqlCSSID += ")"
        ExecuteQuery_Update(sqlCSSID)

        cboSubject.Focus()
        btnProceed.Visible = False
        btnSave.Visible = False
        btnDelete.Visible = False
        txtMaxMarks.Enabled = False
        txtMaxMarks.Text = ""
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

    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click
        If lblInputType.Text = 0 Then
            If IsNumeric(txtMaxMarks.Text) = False Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Only Numeric Value');", True)
                Exit Sub
            End If
        End If
        Dim i As Integer = 0

        For Each gvr As GridViewRow In gvmarks.Rows 'Marks
            Dim lblMaxM As Label = DirectCast(gvr.FindControl("lblMaxM"), Label)
            If lblInputType.Text = 0 Then
                lblMaxM.Text = txtMaxMarks.Text
            ElseIf lblInputType.Text = 1 Then 'Grades
                Dim txtmarks As TextBox = DirectCast(gvr.FindControl("txtmarks"), TextBox)
                txtmarks.Text = txtMaxMarks.Text
            Else   'Remarks
                Dim txtmarks As TextBox = DirectCast(gvr.FindControl("txtmarks"), TextBox)
                txtmarks.Text = txtMaxMarks.Text
            End If

        Next
        gvmarks.Focus()
    End Sub
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

    Private Sub ValidateBlankRecords()
        If lblInputType.Text = 1 Then Exit Sub
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
        Next
    End Sub
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
                        'commented for taking any input
                        ' txtmarks.Text = "AB"
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
    Private Function ValidateGrades() As Integer
        Dim i As Integer = 0, rv As Integer = -1
        For Each gvr As GridViewRow In gvmarks.Rows
            Dim txtGrade As TextBox = DirectCast(gvr.FindControl("txtmarks"), TextBox)
            If lstGrading.Contains(UCase(Trim(txtGrade.Text))) = True Then
            Else
                rv = gvr.RowIndex
                Exit For
            End If
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
        myTable.Rows.Clear()
        cboTerm.Focus()
    End Sub

    Protected Sub cboMinorTerm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMinorTerm.SelectedIndexChanged
        cboMinorTerm.Focus()
    End Sub

    Protected Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click
        lblStatus.Text = ""
        Dim myVal As Integer = 0
        If lblInputType.Text = 0 Then

            ValidateBlankRecords()

            '----Validate Non Numeric Records----------

            myVal = ValidateNonNumericRecords(1)
            If myVal >= 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Max Marks...');", True)
                lblStatus.Text = "Invalid Max Marks..."
                gvmarks.Rows(myVal).Cells(4).Focus()
                Exit Sub
            End If

            myVal = ValidateNonNumericRecords(2)
            If myVal >= 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Obtained Marks...');", True)
                lblStatus.Text = "Invalid Obtained Marks..."

                gvmarks.Rows(myVal).Cells(5).Focus()
                Exit Sub
            End If
            '------------------------------------------
            myVal = ValidateMarksAgainstMaxMarks()
            If myVal >= 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Marks! Obtained Marks can not be greated than Max. Marks...');", True)
                lblStatus.Text = "Invalid Marks! Obtained Marks can not be greated than Max. Marks..."
                gvmarks.Rows(myVal).Cells(5).Focus()
                Exit Sub
            End If
            lblStatus.Text = ""
            PerformMarksToGradeConversion()
        ElseIf lblInputType.Text = 1 Then   'grade
            If Val(lblIsHealthType.Text) > 0 Then
            Else
                myVal = ValidateGrades()
                If myVal >= 0 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Grdae...');", True)
                    lblStatus.Text = "Invalid Grade Marks..."
                    gvmarks.Rows(myVal).Cells(5).Focus()
                    Exit Sub
                End If
            End If
        End If
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
    Protected Sub cboSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubject.SelectedIndexChanged
        myTable.Rows.Clear()

        cboSubject.Focus()
        '0-marks, 1-grade, 2-Remark

        Try
            lblInputType.Text = getInputType(cboSubject.SelectedItem.Value, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, lblSchoolID.Text)
        Catch ex As Exception

        End Try
        If lblInputType.Text = "2" Then    'remark 
            LoadRemarks(cboRemark, cboSubject.SelectedItem.Value, cboClass.SelectedItem.Value)
        End If

        If Val(lblActivitySubID.Text) = cboSubjectGroup.SelectedItem.Value Then
            lstSID = getMappedSIDwithActivity(cboSubject.SelectedItem.Value, cboSubSubjectGroup.SelectedItem.Value)
        Else
            If checkSubSectionExists(cboClass.SelectedItem.Value, cboSection.SelectedItem.Value) > 0 Then
                lstSID = getMappedSIDwithSubjects(cboClass.SelectedItem.Value, cboSection.SelectedItem.Value, cboSubject.SelectedItem.Value)
            Else
                lstSID = Nothing
            End If

        End If

    End Sub
    Private Function getInputType(subID As Integer, ClassName As String, secName As String, Optional ByVal SchoolID As String = "") As Integer
        Dim rv As Integer = 0
        Dim sqlStr As String = " Select top(1) EntryType From vw_ExamSubjectMapping Where SubjectID =" & subID & " AND ASID='" & Request.Cookies("ASID").Value & "' AND ClassName='" & ClassName & "' AND SecNAme='" & secName & "' and SchoolID='" & SchoolID & "'"
        rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Return rv
    End Function
    Protected Sub cboSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubjectGroup.SelectedIndexChanged
        'LoadSubjectGroups(cboSubSubjectGroup, 1)
        Try
            LoadMinorGroups(cboSubSubjectGroup, cboSubjectGroup.SelectedItem.Value)
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
        If cboSubSubjectGroup.Text = "" Then
            subGrpID = cboSubjectGroup.SelectedItem.Value
        Else
            subGrpID = cboSubSubjectGroup.SelectedItem.Value
        End If
        Try
            LoadSubjectClassWise(cboSubject, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, Request.Cookies("ASID").Value, subGrpID)
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
            lblInputType.Text = getInputType(cboSubject.SelectedItem.Value, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, lblSchoolID.Text)
        Catch ex As Exception

        End Try
        If lblInputType.Text = "2" Then    'remark 
            LoadRemarks(cboRemark, cboSubject.SelectedItem.Value, cboClass.SelectedItem.Value)
        End If
        cboSubjectGroup.Focus()
    End Sub

    Private Sub cboSubSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubSubjectGroup.SelectedIndexChanged
        Dim subGrpID As Integer = 0
        If cboSubSubjectGroup.Text = "" Then
            subGrpID = cboSubjectGroup.SelectedItem.Value
        Else
            subGrpID = cboSubSubjectGroup.SelectedItem.Value
        End If
        Try
            LoadSubjectClassWise(cboSubject, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, Request.Cookies("ASID").Value, subGrpID)
        Catch ex As Exception

        End Try

        'LoadSubjectsFromGroups(cboSubject, cboSubSubjectGroup.SelectedItem.Value)
        'LoadSubjectGroupExamTermsMajor(cboTerm, cboSubSubjectGroup.SelectedItem.Value, lblGrpID.Text)
        'LoadSubjectGroupExamTermsMinor(cboMinorTerm, cboSubjectGroup.SelectedItem.Value, lblGrpID.Text, cboSubjectGroup.SelectedItem.Value)
        Try
            lblInputType.Text = getInputType(cboSubject.SelectedItem.Value, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, lblSchoolID.Text)
        Catch ex As Exception

        End Try
        If lblInputType.Text = "2" Then    'remark 
            LoadRemarks(cboRemark, cboSubject.SelectedItem.Value, cboClass.SelectedItem.Value)
        End If
        '  cboSubSubjectGroup.Focus()
    End Sub

    Private Sub cboExamGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboExamGroup.SelectedIndexChanged
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)
        LoadSubjectGroups(cboSubjectGroup, 0, lblGrpID.Text)
        Dim attGrpID As Integer = getAttenddendaceGrpID()
        If lblATT.Text = "1" Then
            lblMaxMarks.Text = "Total Days"
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
        LoadClasses(cboClass, lblGrpID.Text, lblSchoolID.Text)
        cboExamGroup.Focus()
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
    Shared lstGrading As New List(Of String)
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
        If Trim(cboExamGroup.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Exam Group..');", True)
            lblStatus.Text = "Please select Exam Group.."
            cboExamGroup.Focus()
            Exit Sub
        End If
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
        If Trim(cboClass.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Class..');", True)
            lblStatus.Text = "Please select Class.."
            cboClass.Focus()
            Exit Sub
        End If
        If Trim(cboSection.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Section..');", True)
            lblStatus.Text = "Please select Section.."
            cboSection.Focus()
            Exit Sub
        End If
        If Trim(cboSubject.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Subject..');", True)
            lblStatus.Text = "Please select Subject.."
            cboSubject.Focus()
            Exit Sub
        End If

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
        If Trim(cboStatus.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Status..');", True)
            lblStatus.Text = "Please select Status.."
            cboStatus.Focus()
            Exit Sub
        End If
        ''check for permissions
        lblSchoolID.Text = FindMasterID(71, cboSchoolName.SelectedItem.Text)
        If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
            btnDelete.Visible = True
        Else
            If CheckExamConfig(4) Then
                If CheckPermissionSubject(cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, cboSubject.SelectedItem.Text, Request.Cookies("UID").Value, Val(lblSchoolID.Text)) Then
                    btnProceed.Visible = True
                    btnSave.Visible = True

                Else
                    btnProceed.Visible = False
                    btnSave.Visible = False
                    gvmarks.Visible = False
                    lblStatus.Text = "Access Denied. You are not authorized for this entry."
                    Exit Sub
                End If
            End If
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

        If lblInputType.Text = 1 Then
            'grade entry exceptions
            lstGrading = getGradingForGroup(cboSubjectGroup.SelectedItem.Value, Val(lblGrpID.Text))
            For i = 0 To lstMax0.Count - 1
                lstGrading.Add(lstMax0.Item(i))
            Next
            For i = 0 To lstObt0.Count - 1
                lstGrading.Add(lstObt0.Item(i))
            Next
            lblIsHealthType.Text = getHealthSubType()
            lblMaxMarks.Text = "Enter Grade"
            CreateGVGradeEntry(lblGrpID.Text)     'Subject for Grade Entry
            txtMaxMarks.Focus()
        ElseIf lblInputType.Text = 2 Then
            lblMaxMarks.Text = "Enter Remarks"
            CreateGVRemarkEntry(lblGrpID.Text)
        Else
            If lblATT.Text = "1" Then
                lblMaxMarks.Text = "Total Days"
            Else
                lblMaxMarks.Text = "Max Marks"
            End If
            CreateGVMarksEntry(lblGrpID.Text)
            txtMaxMarks.Focus()
            ' CreateTableMarksEntry(lblGrpID.Text)
        End If
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
        If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            btnDelete.Visible = True
        Else
            btnDelete.Visible = False
        End If
        txtMaxMarks.Enabled = True
        gvmarks.Visible = True
    End Sub

    Private Sub gvmarks_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvmarks.RowDataBound
        If lblInputType.Text = "2" Then
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim SubjectID As Integer = cboSubject.SelectedItem.Value
                Dim termIDMaj As Integer = cboTerm.SelectedItem.Value
                Dim termIDMin As Integer = cboMinorTerm.SelectedItem.Value
                Dim sqlStr As String = ""

                Dim SID As Integer = gvmarks.DataKeys(e.Row.RowIndex).Value.ToString()
                Dim primID As String = SID & "-" & SubjectID & "-" & termIDMaj & "-" & termIDMin
                sqlStr = "Select obtM From ExamMarksEntry Where marksID='" & primID & "'"

                Dim myReader1 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                Dim txtMarks As TextBox = DirectCast(e.Row.FindControl("txtMarks"), TextBox)
                txtMarks.Visible = False
                Dim cboRemark As DropDownList = DirectCast(e.Row.FindControl("cboRemark"), DropDownList)
                cboRemark.Visible = True
                getRemarks(cboRemark)

                While myReader1.Read

                    Dim myMarksString = ""
                    Dim myMaxMarks As String = ""
                    Dim MyMarks As String = ""
                    Dim MyGrade As String = ""


                    '  LoadRemarks(cboRemark, SubjectID, cboClass.SelectedItem.Value)

                    'Dim txtMarks As TextBox = DirectCast(gvr.FindControl("txtMarks"), TextBox)
                    'txtMarks.Visible = False
                    '  Dim cboRemark As DropDownList = DirectCast(gvr.FindControl("cboRemark"), DropDownList)
                    ' cboRemark.Visible = True
                    cboRemark.Attributes.Add("onkeypress", "return tabE(this,event)")
                    Try
                        MyMarks = myReader1(0)
                        ' cboRemark.Text = MyMarks
                        cboRemark.ClearSelection()
                        cboRemark.Items.FindByValue(MyMarks).Selected = True

                    Catch ex As Exception

                    End Try

                End While
                myReader1.Close()



            End If
        End If
    End Sub
    Private Sub Save_Log(ByVal type As String, log1 As String)
        Dim sqlStr As String = ""

        ' log1 += " #### " & "RegNo : " & txtSRNo.Text & ", Name : " & txtName.Text & ", Class : " & cboClass.Text & " Section : " & cboSection.Text
        sqlStr = "Insert Into Event_log(logTime,EventType,Details,UserId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        ExecuteQuery_Update(sqlStr)
    End Sub
    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim sqlStr As String = "", log1 As String = "", logType As String = ""
        Dim SubjectID As Integer = cboSubject.SelectedItem.Value
        Dim termIDMaj As Integer = cboTerm.SelectedItem.Value
        Dim termIDMin As Integer = cboMinorTerm.SelectedItem.Value
        sqlStr = "delete from ExamMarksEntry where SubjectID=" & SubjectID & " AND MajorTermID=" & termIDMaj & " AND MinorTermID=" & termIDMin & " AND SID in(select sid from vw_Student where ClassID='" & cboClass.Text & "' AND SecID='" & cboSection.Text & "' AND StatusName='" & cboStatus.Text & "' AND ASID='" & Request.Cookies("ASID").Value & "')"
        ExecuteQuery_Update(sqlStr)
        gvmarks.Visible = False
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Marks Removed Successfully..');", True)

        lblStatus.Text = "Marks Removed Successfully..."
        log1 = "Marks for Class : " & cboClass.SelectedItem.Text & "-" & cboSection.SelectedItem.Text & ", Subject : " & cboSubjectGroup.SelectedItem.Text & "(" & cboSubject.SelectedItem.Text & ")" & ", Term : " & cboTerm.SelectedItem.Text & "-" & cboMinorTerm.SelectedItem.Text & " have Removed for " & cboStatus.SelectedItem.Text & " Students."
        logType = "MARKS "

        Save_Log(logType & "DELETED", log1)


    End Sub

    Private Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
    End Sub
End Class