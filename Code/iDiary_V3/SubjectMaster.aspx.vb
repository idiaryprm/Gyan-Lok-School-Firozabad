Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Partial Class SubjectMaster
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
        LoadSubjectList()
        txtSubjectName.Text = ""
        LoadGradeMarks()

        txtMaxMarks.Text = ""
        txtMaxMarks.Enabled = False

        txtMaxMarksEntry.Text = ""
        txtMaxMarksEntry.Enabled = False

        PerformGradeMarksCheck()
        txtSubjectID.Text = ""
        lblStatus.Text = ""
        lblHelp.Text = ""
        txtSubjectCode.Focus()
    End Sub

    Private Sub LoadGradeMarks()
        cboGradeMarks.Items.Clear()
        cboGradeMarks.Items.Add("")
        cboGradeMarks.Items.Add("Marks")
        cboGradeMarks.Items.Add("Grade")

        cboGradeMarksEntry.Items.Clear()
        cboGradeMarksEntry.Items.Add("")
        cboGradeMarksEntry.Items.Add("Marks")
        cboGradeMarksEntry.Items.Add("Grade")

    End Sub

    Private Sub LoadSubjectList()
       
       
       

        Dim sqlstr As String = "Select SubjectName From SubjectMaster Order By SubjectName"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        lstSubjects.Items.Clear()
        While myReader.Read
            lstSubjects.Items.Add(myReader(0))
        End While
        myReader.Close()
        

        
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtSubjectName.Text) = "" Then
            lblStatus.Text = "Subject Name is Empty..."
            txtSubjectName.Focus()
            Exit Sub
        End If

        If cboGradeMarks.Text = "Marks" And IsNumeric(txtMaxMarks.Text) = False Then
            lblStatus.Text = "Maximum Marks to be provided..."
            txtMaxMarks.Focus()
            Exit Sub
        End If

        If cboGradeMarksEntry.Text = "Marks" And IsNumeric(txtMaxMarksEntry.Text) = False Then
            lblStatus.Text = "Maximum Marks to be provided..."
            txtMaxMarksEntry.Focus()
            Exit Sub
        End If

        Dim sqlstr As String = ""

       
       
       

        
        Dim FinalMessage As String = ""
        Dim SubjectType As Integer = 0
        Dim SubjectTypeEntry As Integer = 0
        Dim MaxMarks As Double = 0
        Dim MaxMarksEntry As Double = 0

        If cboGradeMarks.Text = "Marks" Then
            SubjectType = 0
        ElseIf cboGradeMarks.Text = "Grade" Then
            SubjectType = 1
        End If
        MaxMarks = CDbl(txtMaxMarks.Text)

        If cboGradeMarksEntry.Text = "Marks" Then
            SubjectTypeEntry = 0
        ElseIf cboGradeMarksEntry.Text = "Grade" Then
            SubjectTypeEntry = 1
        End If
        MaxMarksEntry = CDbl(txtMaxMarksEntry.Text)

        If txtSubjectID.Text = "" Then  'New Entry
            sqlstr = "Insert into SubjectMaster Values('" & txtSubjectCode.Text & "','" & txtSubjectName.Text & "'," & SubjectType & "," & SubjectTypeEntry & "," & MaxMarks & "," & MaxMarksEntry & ")"
            
            
            ExecuteQuery_Update(SqlStr)
            '  insertSyncLog(sqlstr, "I", Request.Cookies("UserID").Value)
            FinalMessage = "Subject: " & txtSubjectName.Text & " successfully added..."
        Else    'Update
            sqlstr = "Update SubjectMaster Set SubjectCode='" & txtSubjectCode.Text & "', SubjectName='" & txtSubjectName.Text & "', SubjectType=" & SubjectType & ", MaxMarks=" & MaxMarks & ", EntryType=" & SubjectTypeEntry & ", MaxMarksEntry=" & MaxMarksEntry & " Where SubjectID=" & Val(txtSubjectID.Text)
            
            
            ExecuteQuery_Update(SqlStr)
            '   insertSyncLog(sqlstr, "U", Request.Cookies("UserID").Value)
            FinalMessage = "Subject details successfully updated..."
        End If

        
        

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtSubjectName.Text) = "" Then
            lblStatus.Text = "Select a Subject to remove..."
            txtSubjectName.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""

       
       
       

        

        sqlStr = "Delete From SubjectMaster Where SubjectID=" & Val(txtSubjectID.Text)
        
        
        Try
            ExecuteQuery_Update(SqlStr)
            '   insertSyncLog(sqlStr, "D", Request.Cookies("UserID").Value)
        Catch ex As Exception
            lblStatus.Text = "Unable to remove selected subject..."
        End Try


        
        

        Dim TempName As String = txtSubjectName.Text

        InitControls()

        lblStatus.Text = "Subject: " & TempName & " removed successfully..." & sqlStr
    End Sub

    Protected Sub lstSubjects_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSubjects.SelectedIndexChanged
        txtSubjectName.Text = lstSubjects.Text

       
       
       

        
        dim sqlstr as string="Select * From SubjectMaster Where SubjectName='" & lstSubjects.Text & "'"
   
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            txtSubjectID.Text = myReader("SubjectID")
            txtSubjectCode.Text = myReader("SubjectCode")
            If myReader("SubjectType") = 0 Then
                cboGradeMarks.Text = "Marks"
            ElseIf myReader("SubjectType") = 1 Then
                cboGradeMarks.Text = "Grade"
            End If
            txtMaxMarks.Text = myReader("MaxMarks")

            If myReader("EntryType") = 0 Then
                cboGradeMarksEntry.Text = "Marks"
            ElseIf myReader("EntryType") = 1 Then
                cboGradeMarksEntry.Text = "Grade"
            End If
            txtMaxMarksEntry.Text = myReader("MaxMarksEntry")

            PerformGradeMarksCheck()
            PerformGradeMarksCheckForEntryType()
        End While
        myReader.Close()
        
        

        lblStatus.Text = ""
    End Sub

    Protected Sub cboGradeMarks_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGradeMarks.SelectedIndexChanged
        PerformGradeMarksCheck()
    End Sub

    Private Sub PerformGradeMarksCheck()
        If cboGradeMarks.Text = "Marks" Then
            txtMaxMarks.Enabled = True
            txtMaxMarks.Focus()
        Else
            txtMaxMarks.Enabled = False
            txtMaxMarks.Text = "0"
        End If
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnHelpSubjectType_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHelpSubjectType.Click
        lblHelp.Text = "Indicates whether subject will be trated as Grade or Marks finally during result preperation."
    End Sub

    Protected Sub btnHelpSubjectTypeMarks_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHelpSubjectTypeMarks.Click
        lblHelp.Text = "Indicates maximum marks to be displayed in result card."
    End Sub

    Protected Sub btnHelpEntryType_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHelpEntryType.Click
        lblHelp.Text = "Indicates how marks entry will be done for the subject (Through grade or marks)."
    End Sub

    Protected Sub btnHelpMaxMarksEntry_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHelpMaxMarksEntry.Click
        lblHelp.Text = "Indicates maximum marks for data entry out of which marks will be entered. In case final max marks are different from this value, a internal conversion will be done accordingly."
    End Sub

    Protected Sub cboGradeMarksEntry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGradeMarksEntry.SelectedIndexChanged
        PerformGradeMarksCheckForEntryType()
    End Sub

    Private Sub PerformGradeMarksCheckForEntryType()
        If cboGradeMarksEntry.Text = "Marks" Then
            txtMaxMarksEntry.Enabled = True
            txtMaxMarksEntry.Focus()
        Else
            txtMaxMarksEntry.Enabled = False
            txtMaxMarksEntry.Text = "0"
        End If
    End Sub

End Class
