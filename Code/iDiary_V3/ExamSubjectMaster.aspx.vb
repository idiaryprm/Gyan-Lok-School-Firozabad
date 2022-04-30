Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Partial Class ExamSubjectMaster
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
        txtSubjectCode.Text = ""
        LoadSubjectGroups(cboSubJectGroupMajor, 0)
        txtSubjectName.Text = ""
        cboSubJectGroupMinor.Items.Clear()
        txtSubjectID.Text = ""
        lblStatus.Text = ""
        lblHelp.Text = ""
        LoadSubjectsFromGroups(lstSubjects, cboSubJectGroupMajor.SelectedItem.Value)
        cboSubJectGroupMajor.Focus()
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtSubjectName.Text) = "" Then
            lblStatus.Text = "Please Enter Subject Name..."
            txtSubjectName.Focus()
            Exit Sub
        End If
        If Trim(txtSubjectCode.Text) = "" Then
            lblStatus.Text = "Please Enter Subject Code..."
            txtSubjectCode.Focus()
            Exit Sub
        End If

        Dim sqlstr As String = ""


        Dim FinalMessage As String = ""
        'Dim SubjectType As Integer = cboGradeMarks.SelectedIndex
        'Dim SubjectTypeEntry As Integer = cboGradeMarksEntry.SelectedIndex
        Dim MaxMarks As Double = 0
        Dim MaxMarksEntry As Double = 0

        Dim subgrpID As Integer = 0
        If Trim(cboSubJectGroupMinor.SelectedItem.Text) = "" Then
            subgrpID = cboSubJectGroupMajor.SelectedItem.Value
        Else
            subgrpID = cboSubJectGroupMinor.SelectedItem.Value
        End If
        If txtSubjectID.Text = "" Then  'New Entry
            If CheckDoubleEntrySubject("ExamSubjectMaster", "SubjectName", SQLFixup(Trim(txtSubjectName.Text)), subgrpID) > 0 Then
                lblStatus.Text = "Same Subject already Exist..."
                txtSubjectName.Focus()
                Exit Sub
            End If
            If CheckDoubleEntryQuery("ExamSubjectMaster", "SubjectCode", Trim(txtSubjectCode.Text)) > 0 Then
                lblStatus.Text = "Same Subject Code already Exist..."
                txtSubjectCode.Focus()
                Exit Sub
            End If

            sqlstr = "Insert into ExamSubjectMaster(SubjectCode,SubjectName,subGrpID,CreatedBy) Values( " & _
                "'" & txtSubjectCode.Text & "','" & SQLFixup(Trim(txtSubjectName.Text)) & "'," & subgrpID & "," & Request.Cookies("UserID").Value & ")"
            ExecuteQuery_Update(sqlstr)
            '  insertSyncLog(sqlstr, "I", Request.Cookies("UserID").Value)
            FinalMessage = "Subject: " & txtSubjectName.Text & " successfully added..."
        Else    'Update
            If CheckDoubleEntrySubject("ExamSubjectMaster", "SubjectName", SQLFixup(Trim(txtSubjectName.Text)), subgrpID) > 0 Then
                lblStatus.Text = "Same Subject already Exist..."
                txtSubjectName.Focus()
                Exit Sub
            End If
            If CheckDoubleEntryQuery("ExamSubjectMaster", "SubjectCode", Trim(txtSubjectCode.Text)) > 1 Then
                lblStatus.Text = "Same Subject Code already Exist..."
                txtSubjectCode.Focus()
                Exit Sub
            End If
            sqlstr = "Update ExamSubjectMaster Set SubjectCode='" & txtSubjectCode.Text & "', SubjectName='" & SQLFixup(Trim(txtSubjectName.Text)) & "',subGrpID=" & subgrpID & "  Where SubjectID=" & Val(txtSubjectID.Text)
            ExecuteQuery_Update(sqlstr)
            '   insertSyncLog(sqlstr, "U", Request.Cookies("UserID").Value)
            FinalMessage = "Subject details successfully updated..."
        End If
        Dim tmpVal As String = cboSubJectGroupMajor.Text
        txtSubjectName.Focus()
        ' InitControls()
        txtSubjectID.Text = ""
        txtSubjectCode.Text = ""
        txtSubjectName.Text = ""
        LoadSubjectsFromGroups(lstSubjects, subgrpID)
        lblStatus.Text = FinalMessage
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtSubjectName.Text) = "" Then
            lblStatus.Text = "Select a Subject to remove..."
            txtSubjectName.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
        Dim TempName As String = txtSubjectName.Text
        sqlStr = "Delete From ExamSubjectMaster Where SubjectID=" & Val(txtSubjectID.Text)
        Try
            ExecuteQuery_Update(sqlStr)
            InitControls()
            lblStatus.Text = "Subject: " & TempName & " removed successfully..."
        Catch ex As Exception
            lblStatus.Text = "Unable to remove selected subject..."
        End Try




    End Sub

    Protected Sub lstSubjects_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSubjects.SelectedIndexChanged
        txtSubjectName.Text = lstSubjects.SelectedItem.Text

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()
        Dim isMinor As Integer = 0, subGrpID As Integer = 0
        Dim myCommnand As New SqlCommand
        myCommnand.CommandText = "Select * From vw_ExamSubjectMaster Where SubjectID='" & lstSubjects.Text & "'"
        myCommnand.Connection = myConn
        Dim myReader As SqlDataReader = myCommnand.ExecuteReader
        While myReader.Read
            txtSubjectID.Text = myReader("SubjectID")
            txtSubjectCode.Text = myReader("SubjectCode")
            Try
                isMinor = myReader("isMinorGroup")
                subGrpID = myReader("subGrpID")
            Catch ex As Exception

            End Try
            If isMinor = 0 Then
                cboSubJectGroupMajor.ClearSelection()
                cboSubJectGroupMajor.Items.FindByValue(subGrpID).Selected = True
            Else
                cboSubJectGroupMinor.ClearSelection()
                cboSubJectGroupMinor.Items.FindByValue(subGrpID).Selected = True
            End If
        End While
        myReader.Close()
        myCommnand.Dispose()
        myConn.Dispose()

        lblStatus.Text = ""
    End Sub

    'Protected Sub btnHelpSubjectType_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHelpSubjectType.Click
    '    lblHelp.Text = "Indicates whether subject will be trated as Grade or Marks finally during result preperation."
    'End Sub


    'Protected Sub btnHelpEntryType_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHelpEntryType.Click
    '    lblHelp.Text = "Indicates how marks entry will be done for the subject (Through grade or marks)."
    'End Sub

    Protected Sub cboSubJectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubJectGroupMajor.SelectedIndexChanged
        Dim subgrpID As Integer = cboSubJectGroupMajor.SelectedItem.Value
        LoadSubjectsFromGroups(lstSubjects, subgrpID)
        LoadMinorGroups(cboSubJectGroupMinor, cboSubJectGroupMajor.SelectedItem.Value)
        cboSubJectGroupMajor.Focus()
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub cboSubJectGroup0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubJectGroupMinor.SelectedIndexChanged
        Dim subgrpID As Integer = cboSubJectGroupMinor.SelectedItem.Value
        LoadSubjectsFromGroups(lstSubjects, subgrpID)
        cboSubJectGroupMinor.Focus()
    End Sub
End Class
