Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ExamSubGroupmaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Exam") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""
        If Trim(txtSubGrpName.Text) = "" Then
            FinalMessage = "Please Enter Subject Group Name..."
            txtSubGrpName.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
            Exit Sub
        End If
        If cboGroupType.Text = "Minor" And cboMajorGroups.Text = "" Then
            FinalMessage = "Please Select Major Subject Group..."
            cboMajorGroups.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
            Exit Sub
        End If
        If Val(txtID.Text) = 0 Then
            If CheckDoubleEntry(103, txtSubGrpName.Text) = True Then
                FinalMessage = "Subject Group Already Exists..."
                txtSubGrpName.Focus()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
                Exit Sub
            End If
        End If

        Dim majorGrpID As Integer = 0
        If cboGroupType.Text = "Major" Then
            majorGrpID = 0
        Else
            Try
                majorGrpID = cboMajorGroups.SelectedItem.Value
            Catch ex As Exception

            End Try

        End If
        Dim examGroupIDs As String = ""
        For i = 0 To cblExamGroups.Items.Count - 1
            If cblExamGroups.Items(i).Selected = True Then
                examGroupIDs += ":" & cblExamGroups.Items(i).Value & ":" & ","
            End If
        Next
        If examGroupIDs <> "" Then
            examGroupIDs = examGroupIDs.Substring(0, examGroupIDs.Length - 1)
        End If
        Dim PartOfCalculation As Integer = 0
        '0---NO, 1---Yes
        If cboPartOfCalculation.Text = "Yes" Then
            PartOfCalculation = 1
        Else
            PartOfCalculation = 0
        End If
        Dim IsAttendanceType As Integer = 0
        If cboIsAttendanceType.Text = "Yes" Then
            IsAttendanceType = 1
        Else
            IsAttendanceType = 0
        End If
        If Val(txtID.Text) = 0 Then    'Insert Command

            sqlStr = "Insert into ExamSubjectGroupMaster(subGroupName,isMinorGroup,majorGroupID,examGroupID,DisplayOrder,PartOfCalculation,IsAttendanceType,createdBy) Values ( " & _
                   "'" & txtSubGrpName.Text & "'," & cboGroupType.SelectedIndex & "," & majorGrpID & ",'" & examGroupIDs & "','" & txtDisplayOrder.Text & "','" & PartOfCalculation & "','" & IsAttendanceType & "','" & Request.Cookies("USerID").Value & "')"

            FinalMessage = "New Subject Group added successfully..."
        Else    'Update Command
            sqlStr = "Update ExamSubjectGroupMaster Set subGroupName='" & txtSubGrpName.Text & "', isMinorGroup=" & cboGroupType.SelectedIndex & ", " & _
                "majorGroupID=" & majorGrpID & ",examGroupID='" & examGroupIDs & "',DisplayOrder='" & txtDisplayOrder.Text & "',PartOfCalculation='" & PartOfCalculation & "',IsAttendanceType='" & IsAttendanceType & "'  Where subGrpID=" & Val(txtID.Text)
            FinalMessage = "Subject Group updated successfully..."
        End If
        ExecuteQuery_Update(sqlStr)

        'manage major groups without minors i.e. mojorgroupID=0
        sqlStr = "Update ExamSubjectGroupMaster Set majorGroupID=SubGrpID Where MajorGroupID=0"
        ExecuteQuery_Update(sqlStr)

        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        cboIsAttendanceType.Text = "No"
        LoadSubjectGroups(cboMajors, 0)
        lstSubGrp.Items.Clear()
        'LoadSubjectGroups(lstSubGrp, 2)
        'LoadMinorGroups(lstSubGrp, cboMajors.SelectedItem.Value)
        ' LoadMasterData("subGroupName", "ExamSubjectGroupMaster", lstSubGrp, "DisplayOrder")
        cboGroupType.SelectedIndex = 0
        txtID.Text = 0
        txtSubGrpName.Text = ""
        cboMajorGroups.Items.Clear()
        txtSubGrpName.Focus()
        btnRemove.Visible = False
        lblExamGroups.Visible = False
        cboMajorGroups.Visible = False
        txtSubGrpName.Focus()
        For i = 0 To cblExamGroups.Items.Count - 1
            cblExamGroups.Items(i).Selected = False
        Next
        cbAll.Checked = False
        'cblExamGroups.Items.Clear()
    End Sub

    Protected Sub lstSubGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSubGrp.SelectedIndexChanged
        Dim majorGrpID As Integer = 0
        Dim sqlStr As String = "Select * From ExamSubjectGroupMaster Where subGrpID='" & lstSubGrp.SelectedItem.Value & "'"
        Dim examGroupIDs As String = ""
        Dim PartOfCalculation As Integer = 0
        Dim IsAttendanceType As Integer = 0
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("subGrpID")
            txtSubGrpName.Text = myReader("subGroupName")
            Try
                cboGroupType.SelectedIndex = myReader("isMinorGroup")
                If cboGroupType.Text = "Major" Then
                    cboMajorGroups.Items.Clear()

                Else
                    LoadSubjectGroups(cboMajorGroups, 0)
                End If
                examGroupIDs = myReader("ExamGroupID")
                majorGrpID = myReader("majorGroupID")
                cboMajorGroups.ClearSelection()
                cboMajorGroups.Items.FindByValue(majorGrpID).Selected = True

            Catch ex As Exception

            End Try
            Try
                txtDisplayOrder.Text = myReader("DisplayOrder")
            Catch ex As Exception

            End Try
            Try
                PartOfCalculation = myReader("PartOfCalculation")
            Catch ex As Exception

            End Try
            If PartOfCalculation = 1 Then
                cboPartOfCalculation.SelectedIndex = 0
            Else
                cboPartOfCalculation.SelectedIndex = 1
            End If
            Try
                IsAttendanceType = myReader("IsAttendanceType")
            Catch ex As Exception

            End Try
            If IsAttendanceType = 1 Then
                cboIsAttendanceType.SelectedIndex = 0
            Else
                cboIsAttendanceType.SelectedIndex = 1
            End If
        End While
        myReader.Close()
        loadExamGroups()
        Try
            cboMajorGroups.ClearSelection()
            cboMajorGroups.Items.FindByValue(majorGrpID).Selected = True
        Catch ex As Exception

        End Try
        
        Dim IDs As String() = examGroupIDs.Split(",")

        cblExamGroups.DataBind()
        For i = 0 To cblExamGroups.Items.Count - 1
            If IDs.Contains(":" & cblExamGroups.Items(i).Value & ":") Then
                cblExamGroups.Items(i).Selected = True
            End If
        Next
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""

        If Val(txtID.Text) = 0 Then    'Error
            FinalMessage = "Select a Subject Group to remove..."
        Else
            sqlStr = "Delete From ExamSubjectGroupMaster Where subGrpID=" & Val(txtID.Text)
            FinalMessage = "Subect Group removed successfully..."
        End If

        ExecuteQuery_Update(sqlStr)
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)


    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub
   

    Protected Sub cboGroupType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGroupType.SelectedIndexChanged
        loadExamGroups()
        cboGroupType.Focus()
    End Sub
    Private Sub loadExamGroups()
        cboMajorGroups.Items.Clear()
        lblExamGroups.Visible = False
        cboMajorGroups.Visible = False
        If cboGroupType.Text = "Major" Then
            'cboMajorGroups.Items.Clear()
            'lblExamGroups.Visible = True
            'cblExamGroups.Visible = True
        Else
            'If Val(txtID.Text) <> 0 Then
            '    LoadSubjectGroups(cboMajorGroups, 0, Val(txtID.Text))
            'Else
            LoadSubjectGroups(cboMajorGroups, 0)
            'End If
            lblExamGroups.Visible = True
            cboMajorGroups.Visible = True
        End If
    End Sub
    Protected Sub cboMajorGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMajorGroups.SelectedIndexChanged
        lstSubGrp.Items.Clear()
        Dim tmpDDL As New DropDownList
        LoadMinorGroups(tmpDDL, cboMajorGroups.SelectedItem.Value)
        For i = 0 To tmpDDL.Items.Count - 1
            If tmpDDL.Items(i).Text <> "" Then
                If cboMajorGroups.SelectedItem.Value = tmpDDL.Items(i).Value Then
                    Continue For
                Else
                    lstSubGrp.Items.Add(tmpDDL.Items(i).Text)
                End If
            End If
        Next
    End Sub

    Protected Sub cboPartOfCalculation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPartOfCalculation.SelectedIndexChanged
        If cboPartOfCalculation.Text = "Yes" Then
            cboIsAttendanceType.Text = "No"
            cboIsAttendanceType.Enabled = False
        Else
            cboIsAttendanceType.Enabled = True
        End If
        cboPartOfCalculation.Focus()
    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbAll.CheckedChanged
        CheckAll(cbAll.Checked)
    End Sub
    Private Sub CheckAll(ByVal isChecked As Boolean)
        For i = 0 To cblExamGroups.Items.Count - 1
            cblExamGroups.Items(i).Selected = isChecked
        Next
    End Sub

    Protected Sub cboMajors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMajors.SelectedIndexChanged
        LoadMinorGroups(lstSubGrp, cboMajors.SelectedItem.Value)
        'If lstSubGrp.Items.Count = 0 Then
        Dim majorGrpID As Integer = 0
        Dim sqlStr As String = "Select * From ExamSubjectGroupMaster Where subGrpID='" & cboMajors.SelectedItem.Value & "'"
        Dim examGroupIDs As String = ""
        Dim PartOfCalculation As Integer = 0
        Dim IsAttendanceType As Integer = 0
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("subGrpID")
            txtSubGrpName.Text = myReader("subGroupName")
            Try
                cboGroupType.SelectedIndex = myReader("isMinorGroup")
                If cboGroupType.Text = "Major" Then
                    cboMajorGroups.Items.Clear()

                Else
                    LoadSubjectGroups(cboMajorGroups, 0)
                End If
                examGroupIDs = myReader("ExamGroupID")
                majorGrpID = myReader("majorGroupID")
                cboMajorGroups.ClearSelection()
                cboMajorGroups.Items.FindByValue(majorGrpID).Selected = True

            Catch ex As Exception

            End Try
            Try
                txtDisplayOrder.Text = myReader("DisplayOrder")
            Catch ex As Exception

            End Try
            Try
                PartOfCalculation = myReader("PartOfCalculation")
            Catch ex As Exception

            End Try
            If PartOfCalculation = 1 Then
                cboPartOfCalculation.SelectedIndex = 0
            Else
                cboPartOfCalculation.SelectedIndex = 1
            End If
            Try
                IsAttendanceType = myReader("IsAttendanceType")
            Catch ex As Exception

            End Try
            If IsAttendanceType = 1 Then
                cboIsAttendanceType.SelectedIndex = 0
            Else
                cboIsAttendanceType.SelectedIndex = 1
            End If
        End While
        myReader.Close()
        loadExamGroups()
        Try
            cboMajorGroups.ClearSelection()
            cboMajorGroups.Items.FindByValue(majorGrpID).Selected = True
        Catch ex As Exception

        End Try

        Dim IDs As String() = examGroupIDs.Split(",")

        cblExamGroups.DataBind()
        For i = 0 To cblExamGroups.Items.Count - 1
            If IDs.Contains(":" & cblExamGroups.Items(i).Value & ":") Then
                cblExamGroups.Items(i).Selected = True
            End If
        Next
        ' End If
    End Sub
End Class
