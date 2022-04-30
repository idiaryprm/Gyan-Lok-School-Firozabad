Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports System.Drawing

Partial Class ExamSubjectMapping
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Exam") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)

        cboSection.Items.Clear()
        btnSave.Visible = False
        cboExamGroup.Focus()
    End Sub

    Protected Sub cboClassSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.SelectedItem.Text, cboClass.SelectedItem.Text, cboSection)
        cboClass.Focus()
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSection.SelectedIndexChanged
        LoadClassSubSection(cboSchoolName.Text, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, cboSubSection)
        cboSection.Focus()
    End Sub

    Private Sub GvCreateTable()
        GvMyTable.Visible = True
        Dim sqlstr As String = ""
        Dim displayType As Integer = 0
        Dim entryType As Integer = 0
        Dim ExamVal As Integer = 0
        Dim Priority As Integer = 0
        Dim TTVal As Integer = 0
        Dim TTwt As Integer = 0

        Dim rv As String = ""
        Dim CSSID As Integer = FindCSSID(cboSchoolName.SelectedItem.Text, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, cboSubSection.SelectedItem.Text)
        Dim ASID As Integer = Request.Cookies("ASID").Value

        For Each gvr As GridViewRow In GvMyTable.Rows
            Dim SubjectID As String = GvMyTable.DataKeys(gvr.RowIndex).Value
            sqlstr = "select DisplayType,EntryType,ApplicableInExam,ApplicableInTimeTable,TTWeightage,Priority from ExamSubjectMapping where subjectID=" & SubjectID & " AND CSSID=" & CSSID & " AND ASID=" & ASID
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            While myReader.Read
                Try
                    displayType = myReader("DisplayType")
                Catch ex As Exception
                    displayType = 0
                End Try
                Try
                    entryType = myReader("EntryType")
                Catch ex As Exception
                    entryType = 0
                End Try
                Try
                    ExamVal = myReader("ApplicableInExam")
                Catch ex As Exception
                    ExamVal = 0
                End Try
                Try
                    TTVal = myReader("ApplicableInTimeTable")
                Catch ex As Exception
                    TTVal = 0
                End Try
                Try
                    TTwt = myReader("TTWeightage")
                Catch ex As Exception
                    TTwt = 0
                End Try
                Try
                    Priority = myReader("Priority")
                Catch ex As Exception
                    Priority = 0
                End Try
                Try
                    Dim cboDisplay As DropDownList = DirectCast(gvr.FindControl("cboDisplayType"), DropDownList)
                    Dim cboEntry As DropDownList = DirectCast(gvr.FindControl("cboEntryType"), DropDownList)
                    Dim txtPriority As TextBox = DirectCast(gvr.FindControl("txtPriority"), TextBox)
                    Dim chkApplicableExam As CheckBox = DirectCast(gvr.FindControl("chkApplicableExam"), CheckBox)
                    Dim chkApplicableTT As CheckBox = DirectCast(gvr.FindControl("chkApplicableTT"), CheckBox)
                    Dim cboTTweightage As DropDownList = DirectCast(gvr.FindControl("cboTTWeightage"), DropDownList)

                    DirectCast(gvr.FindControl("chkSelect"), CheckBox).Checked = True
                    gvr.BackColor = ColorTranslator.FromHtml("#CCFFCC")
                    cboDisplay.Enabled = True
                    cboDisplay.SelectedIndex = displayType
                    cboEntry.Enabled = True
                    cboEntry.SelectedIndex = entryType
                    txtPriority.Text = Priority
                    chkApplicableExam.Enabled = True
                    If ExamVal = 1 Then
                        chkApplicableExam.Checked = True
                    Else
                        chkApplicableExam.Checked = False
                    End If
                    chkApplicableTT.Enabled = True
                    If TTVal = 1 Then
                        chkApplicableTT.Checked = True
                    Else
                        chkApplicableTT.Checked = False
                    End If
                    cboTTweightage.Enabled = True
                    cboTTweightage.Text = TTwt

                Catch ex As Exception

                End Try

            End While
            myReader.Close()
        Next
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cboExamGroup.Text = "" Then
            lblStatus.Text = "Please Select Exam Group..."
            cboExamGroup.Focus()
            Exit Sub
        End If
        If cboClass.Text = "" Then
            lblStatus.Text = "Please Select Class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            lblStatus.Text = "Please Select Section..."
            cboSection.Focus()
            Exit Sub
        End If
        'If cboSubSection.Text = "" Then
        '    lblStatus.Text = "Please Select Sub Section..."
        '    cboSection.Focus()
        '    Exit Sub
        'End If
        If cboSubjectGroup.Text = "" Then
            lblStatus.Text = "Please Select Subject Group..."
            cboSubjectGroup.Focus()
            Exit Sub
        End If
        Dim SubCount As Integer = 0
        For Each gvr As GridViewRow In GvMyTable.Rows
            Dim chkSelect As CheckBox = DirectCast(gvr.FindControl("chkSelect"), CheckBox)
            If chkSelect.Checked = True Then
                SubCount = 1
                Exit For
            End If
        Next
      
        GvSaveData()
        GvMyTable.Visible = False
        If SubCount = 0 Then
            lblStatus.Text = "No mapping Saved/Mapping deleted Successfully."
        Else
            lblStatus.Text = "Mapping Saved Successfully."
        End If
        cboSubjectGroup.SelectedIndex = 0
        cboSubjectGroup.Focus()
        btnSave.Visible = False
    End Sub

    Private Sub GvSaveData()

        Dim sqlstr As String = ""
        Dim displayType As Integer = 0
        Dim entryType As Integer = 0
        Dim Priority As Integer = 0
        Dim ExamVal As Integer = 0
        Dim TTVal As Integer = 0
        Dim TTwt As Integer = 0
        Dim rv As String = ""
        Dim CSSID As Integer = FindCSSID(cboSchoolName.SelectedItem.Text, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, cboSubSection.SelectedItem.Text)
        If CSSID = 0 Then
            lblStatus.Text = "No Class-Section Found."
            Exit Sub
        Else
            lblStatus.Text = ""
        End If
        Dim ASID As Integer = Request.Cookies("ASID").Value

        For Each gvr As GridViewRow In GvMyTable.Rows
            Dim SubjectID As String = GvMyTable.DataKeys(gvr.RowIndex).Value
            ExamVal = 0
            TTVal = 0
            displayType = 0
            entryType = 0
            Dim chkSelect As CheckBox = DirectCast(gvr.FindControl("chkSelect"), CheckBox)
            Dim cboDisplay As DropDownList = DirectCast(gvr.FindControl("cboDisplayType"), DropDownList)
            Dim cboEntry As DropDownList = DirectCast(gvr.FindControl("cboEntryType"), DropDownList)
            Dim txtPriority As TextBox = DirectCast(gvr.FindControl("txtPriority"), TextBox)
            Dim chkApplicableExam As CheckBox = DirectCast(gvr.FindControl("chkApplicableExam"), CheckBox)
            Dim chkApplicableTT As CheckBox = DirectCast(gvr.FindControl("chkApplicableTT"), CheckBox)
            Dim cboTTweightage As DropDownList = DirectCast(gvr.FindControl("cboTTWeightage"), DropDownList)
            Priority = Val(txtPriority.Text)
            sqlstr = "Delete From ExamSubjectMapping Where CSSID=" & CSSID & " AND SubjectID=" & SubjectID & " AND ASID=" & ASID
            ExecuteQuery_Update(sqlstr)

            If chkSelect.Checked = True Then
                displayType = cboDisplay.SelectedIndex
                entryType = cboEntry.SelectedIndex
                If chkApplicableExam.Checked = True Then ExamVal = 1
                If chkApplicableTT.Checked = True Then TTVal = 1
                TTwt = cboTTweightage.Text

                sqlstr = "Insert into ExamSubjectMapping (CSSID, SubjectID,DisplayType,EntryType,ApplicableInExam,ApplicableInTimeTable,TTWeightage,Priority,ASID) Values " & _
                    "(" & CSSID & "," & SubjectID & "," & displayType & "," & entryType & "," & ExamVal & ",'" & TTVal & "','" & TTwt & "'," & Priority & "," & ASID & ")"
                ExecuteQuery_Update(sqlstr)

            End If
        Next
    End Sub

    Protected Sub cboExamGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboExamGroup.SelectedIndexChanged
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)
        LoadClasses(cboClass, lblGrpID.Text, lblSchoolID.Text)
        LoadSubjectGroups(cboSubjectGroup, 2, lblGrpID.Text)
        cboExamGroup.Focus()
    End Sub

    Protected Sub chkSelect_CheckedChanged(sender As Object, e As EventArgs)
        Dim rowVal As CheckBox = TryCast(sender, CheckBox)
        Dim gvRowIndex As Integer = Convert.ToInt32(rowVal.Attributes("RowIndex"))
        CheckGvMyTable(gvRowIndex)
    End Sub
    Private Sub CheckGvMyTable(gvRowIndex As Integer)

        Dim chkSelect As CheckBox = DirectCast(GvMyTable.Rows(gvRowIndex).FindControl("chkSelect"), CheckBox)
        Dim cboDisplay As DropDownList = DirectCast(GvMyTable.Rows(gvRowIndex).FindControl("cboDisplayType"), DropDownList)
        Dim cboEntry As DropDownList = DirectCast(GvMyTable.Rows(gvRowIndex).FindControl("cboEntryType"), DropDownList)
        Dim txtPriority As TextBox = DirectCast(GvMyTable.Rows(gvRowIndex).FindControl("txtPriority"), TextBox)
        Dim chkApplicableExam As CheckBox = DirectCast(GvMyTable.Rows(gvRowIndex).FindControl("chkApplicableExam"), CheckBox)
        Dim chkApplicableTT As CheckBox = DirectCast(GvMyTable.Rows(gvRowIndex).FindControl("chkApplicableTT"), CheckBox)
        Dim cboTTweightage As DropDownList = DirectCast(GvMyTable.Rows(gvRowIndex).FindControl("cboTTWeightage"), DropDownList)
        If chkSelect.Checked = True Then
            GvMyTable.Rows(gvRowIndex).BackColor = ColorTranslator.FromHtml("#CCFFCC")
            cboDisplay.Enabled = True
            cboEntry.Enabled = True
            chkApplicableExam.Enabled = True
            chkApplicableExam.Checked = True
            chkApplicableTT.Enabled = True
            ' chkApplicableTT.Checked = True
            cboTTweightage.Enabled = True
            txtPriority.Enabled = True
        Else
            GvMyTable.Rows(gvRowIndex).BackColor = Nothing
            cboDisplay.Enabled = False
            cboEntry.Enabled = False
            chkApplicableExam.Enabled = False
            chkApplicableExam.Checked = False
            chkApplicableTT.Enabled = False
            chkApplicableTT.Checked = False
            cboTTweightage.Enabled = False
            txtPriority.Enabled = True
        End If
        chkSelect.Focus()
    End Sub

    Protected Sub cboSubSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubSection.SelectedIndexChanged

    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If cboExamGroup.Text = "" Then
            lblStatus.Text = "Please Select Exam Group..."
            cboExamGroup.Focus()
            Exit Sub
        End If
        If cboClass.Text = "" Then
            lblStatus.Text = "Please Select Class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            lblStatus.Text = "Please Select Section..."
            cboSection.Focus()
            Exit Sub
        End If
        'If cboSubSection.Text = "" Then
        '    lblStatus.Text = "Please Select Sub Section..."
        '    cboSection.Focus()
        '    Exit Sub
        'End If
        If cboSubjectGroup.Text = "" Then
            lblStatus.Text = "Please Select Subject Group..."
            cboSubjectGroup.Focus()
            Exit Sub
        End If
        Dim sqlStr As String = "", rv As Integer = -1
        sqlStr = "Select isMinorGroup from ExamSubjectGroupMaster Where subGrpID=" & cboSubjectGroup.SelectedItem.Value
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception
            rv = -1
        End Try
        If rv = 0 Then
            sqlStr = "Select * From vw_ExamSubjects where majorGroupID=" & cboSubjectGroup.SelectedItem.Value & " AND ExamGroupID Like '%:" & lblGrpID.Text & ":%' Order by SubGroupName,SubjectName"
        Else
            sqlStr = "Select * From vw_ExamSubjects where subgrpID=" & cboSubjectGroup.SelectedItem.Value & " AND ExamGroupID Like '%:" & lblGrpID.Text & ":%' Order by SubjectName"
        End If
        sdsgvMytable.SelectCommand = sqlStr
        GvMyTable.DataBind()
        If GvMyTable.Rows.Count > 0 Then
            btnSave.Visible = True
        Else
            btnSave.Visible = False
        End If
        GvCreateTable()
        ' CheckGvMyTable()
        'cboSubjectGroup.Focus()
        lblStatus.Text = ""
    End Sub

    Private Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
    End Sub
End Class
