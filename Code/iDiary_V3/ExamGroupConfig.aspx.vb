Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class ExamGroupConfig
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboExamGroup.Text = "" Then
            cboExamGroup.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select a group to continue...');", True)
            Exit Sub
        End If
        Dim sqlStr As String = ""
        Dim FinalMessage = ""

        For i = 0 To cblClasses.Items.Count - 1
            If cblClasses.Items(i).Selected = True Then
                sqlStr = "Update ClassStudent set ExamGroupID=" & Val(txtID.Text) & " Where CSSID=" & cblClasses.Items(i).Value
                ExecuteQuery_Update(sqlStr)
            End If
        Next
        'If Val(txtID.Text) = 0 Then    'Insert Command
        '    sqlStr = "Insert into ExamGroups Values('" & txtName.Text & "','" & Request.Cookies("USerID").Value & "')"
        '    FinalMessage = "New Exam Group added successfully..."
        'Else    'Update Command
        '    sqlStr = "Update ExamGroups Set ExamGroupName='" & txtName.Text & "' Where examGrpID=" & Val(txtID.Text)
        '    FinalMessage = "Exam Group updated successfully..."
        'End If
        FinalMessage = "Exam Group updated successfully..."

        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        cboExamGroup.Focus()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)

        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        ' LoadMasterInfo(101, lstMaster)
        loadClasses()
        txtID.Text = 0
        cbAll.Visible = False
        cbAll.Checked = False
    End Sub

    'Protected Sub lstMaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMaster.SelectedIndexChanged
    '    txtID.Text = FindMasterID(101, lstMaster.Text)
    '    cbAll.Visible = True
    '    cbAll.Checked = False
    '    Dim mappedClasses As String = ""
    '    Dim sqlStr As String = "Select ClassID From Classes Where ExamGroupID='" & txtID.Text & "'"

    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    While myReader.Read
    '        mappedClasses &= ":" & myReader(0) & ":"
    '    End While
    '    myReader.Close()

    '    For i = 0 To cblClasses.Items.Count - 1
    '        If mappedClasses.Contains(":" & cblClasses.Items(i).Value & ":") Then
    '            cblClasses.Items(i).Selected = True
    '        Else
    '            cblClasses.Items(i).Selected = False
    '        End If
    '    Next
    'End Sub

    Private Sub loadClasses()
        Dim sqlStr As String = "Select CSSID,CSSNAME From vw_classStudent where SchoolID=" & Val(lblSchoolID.Text) & " order by CSSNAME"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cblClasses.Items.Clear()
        While myReader.Read
            cblClasses.Items.Add(New ListItem(myReader(1), myReader(0)))
        End While
        myReader.Close()
    End Sub

    Protected Sub cbAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbAll.CheckedChanged
        CheckAll(cbAll.Checked)
    End Sub
    Private Sub CheckAll(ByVal isChecked As Boolean)
        For i = 0 To cblClasses.Items.Count - 1
            cblClasses.Items(i).Selected = isChecked
        Next
    End Sub

    Protected Sub cboExamGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboExamGroup.SelectedIndexChanged
        If cboExamGroup.Text = "" Then
            cboExamGroup.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select a group to continue...');", True)
            Exit Sub
        End If
        txtID.Text = FindMasterID(101, cboExamGroup.SelectedItem.Text)
        cbAll.Visible = True
        cbAll.Checked = False
        Dim mappedClasses As String = ""
        Dim sqlStr As String = "Select CSSID From ClassStudent Where ExamGroupID='" & txtID.Text & "'"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            mappedClasses &= ":" & myReader(0) & ":"
        End While
        myReader.Close()

        For i = 0 To cblClasses.Items.Count - 1
            If mappedClasses.Contains(":" & cblClasses.Items(i).Value & ":") Then
                cblClasses.Items(i).Selected = True
            Else
                cblClasses.Items(i).Selected = False
            End If
        Next
        cboExamGroup.Focus()
    End Sub

    Private Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
        loadClasses()

    End Sub
    Public Function getSchoolID(schoolName As String) As Integer
        Dim rv As Integer = 0
        Dim sqlStr As String = ""
        sqlStr = "select max(SchoolID) from SchoolMaster where SchoolName='" & schoolName & "'"
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try
        Return rv
    End Function

End Class
