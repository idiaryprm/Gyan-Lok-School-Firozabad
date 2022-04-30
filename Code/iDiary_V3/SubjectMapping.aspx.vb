Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Partial Class SubjectMapping
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
        LoadMasterInfo(2, cboClass)
        cboSection.Items.Clear()
        LoadSubjects()
        lstSelected.Items.Clear()
        lblStatus.Text = ""
        cboClass.Focus()
    End Sub

    Private Sub LoadClasses()
       
        Dim sqlstr As String = "Select * From Classes"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        cboClass.Items.Clear()
        cboClass.Items.Add("")
        While myReader.Read
            cboClass.Items.Add(myReader("ClassName"))
        End While
        myReader.Close()
    End Sub

    Private Sub LoadClassSections()
      
        Dim sqlstr As String = "Select * From vw_Class_Section Where ClassName='" & cboClass.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        cboSection.Items.Clear()
        cboSection.Items.Add("")
        While myReader.Read
            cboSection.Items.Add(myReader("SecName"))
        End While
       
    End Sub

    Private Sub LoadSubjects()
       
        Dim sqlstr As String = "Select SubjectName From SubjectMaster Order By SubjectName"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        cboSubjects.Items.Clear()
        cboSubjects.Items.Add("")
        While myReader.Read
            cboSubjects.Items.Add(myReader(0))
        End While
        myReader.Close()
    End Sub

    Private Function GetSubjectID(ByVal SubjectName As String) As Integer

        Dim sqlstr As String = "Select Max(SubjectID) From SubjectMaster Where SubjectName='" & SubjectName & "'"
        Dim rv As Integer = 0
        rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Return rv
    End Function

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        '0
        If cbTimetable.Checked = False And cbExam.Checked = False Then
            lblStatus.Text = "Exam should be part of atleast one (Exam,timetable)"
            Exit Sub
        Else
            lblStatus.Text = ""
        End If
        Dim SubjectID As Integer = GetSubjectID(cboSubjects.Text)
        Dim CSSID As Integer = FindCSSID("", cboClass.Text, cboSection.Text, cboSubSection.Text)
        Dim sqlStr As String = ""

        sqlStr = "Select Count(*) From SubjectMapping Where CSSID=" & CSSID & " AND SubjectID=" & SubjectID & " AND ASID =" & Request.Cookies("ASID").Value
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        If rv > 0 Then
            lblStatus.Text = "Subject Already Mapped..."
            cboSubjects.Focus()
            Exit Sub
        End If
        Dim SubType As Integer = 0
        If cbTimetable.Checked = True And cbExam.Checked = True Then
            '0-exam,1-timetable,2-both
            SubType = 2
        ElseIf cbExam.Checked = True And cbTimetable.Checked = False Then
            SubType = 0
        ElseIf cbExam.Checked = False And cbTimetable.Checked = True Then
            SubType = 1
        End If
        sqlStr = "Insert into SubjectMapping (CSSID, SubjectID,SubjectType,TTWeightage,ASID) Values (" & CSSID & "," & SubjectID & "," & SubType & ",'" & cboWeightage.Text & "'," & Request.Cookies("ASID").Value & ")"
        ExecuteQuery_Update(sqlStr)
        RefreshList()
    End Sub

    Private Sub RefreshList()
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        '0
        Dim cssid As Integer = FindCSSID("", cboClass.Text, cboSection.Text, cboSubSection.Text)
        Dim sqlStr As String = ""

        sqlStr = "Select SubjectName From vwSubjectMapping Where CSSID=" & cssid & " AND ASID=" & Request.Cookies("ASID").Value
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        lstSelected.Items.Clear()
        While myReader.Read
            lstSelected.Items.Add(myReader(0))
        End While
        myReader.Close()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If lstSelected.Text.Length <= 0 Then
            lblStatus.Text = "Select a subject to remove..."
            lstSelected.Focus()
            Exit Sub
        End If
        Dim CSSID As Integer = FindCSSID("", cboClass.Text, cboSection.Text, cboSubSection.Text)
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        '0
        Dim SubjectID As Integer = GetSubjectID(lstSelected.Text)
        Dim sqlStr As String = ""
       
        sqlStr = "Delete From SubjectMapping Where CSSID=" & CSSID & " AND  SubjectID=" & SubjectID
        ExecuteQuery_Update(sqlStr)
      
        RefreshList()
        lblStatus.Text = ""
    End Sub

    Protected Sub cboClassSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        'LoadClassSections()
        LoadClassSection("", cboClass.Text, cboSection)
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSection.SelectedIndexChanged
        LoadClassSubSection("", cboClass.Text, cboSection.Text, cboSubSection)
        RefreshList()
    End Sub

    Protected Sub cboSubSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubSection.SelectedIndexChanged
        RefreshList()
    End Sub

    Protected Sub lstSelected_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSelected.SelectedIndexChanged
        Dim sqlStr As String = ""
        Dim SubjectID As Integer = GetSubjectID(lstSelected.Text)
        Dim Cssid As Integer = FindCSSID("", cboClass.Text, cboSection.Text, cboSubSection.Text)
        Dim subType As Integer = 0
        sqlStr = "Select SubjectType,TTWeightage From SubjectMapping Where CSSID=" & Cssid & " And Subjectid=" & SubjectID
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                subType = myReader("SubjectType")
                cboWeightage.Text = myReader("TTWeightage")
            Catch ex As Exception

            End Try
        End While
        myReader.Close()

        If subType = 2 Then
            '0-exam,1-timetable,2-both
            cbTimetable.Checked = True
            cbExam.Checked = True
        ElseIf subType = 0 Then
            cbExam.Checked = True
            cbTimetable.Checked = False
        ElseIf subType = 1 Then
            cbExam.Checked = False
            cbTimetable.Checked = True
        End If
    End Sub
End Class
