Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports Microsoft.Reporting.WebForms
Imports System.Web.Util
Imports System.Data.SqlClient

Public Class ExamActivityMapping
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        lblActivityGrpID.Text = getExamParams(1)
        If lblActivityGrpID.Text = "" Then
            lblmsg.Text = "Map the Activity Subject Group first."
            Exit Sub
        Else
            lblmsg.Text = ""
        End If
        loadActivityMappedClasses(cboClass, lblActivityGrpID.Text)
        Try
            LoadClassSections(cboClass.SelectedItem.Text, cboSection)
        Catch ex As Exception

        End Try
        LoadMasterInfo(10, cboStatus)
        LoadMinorGroups(cboMinorSubjectGroups, lblActivityGrpID.Text)
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.SelectedItem.Text, cboSection)
        cboClass.Focus()
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        LoadSubjectsFromGroups(cboActivity, cboMinorSubjectGroups.SelectedItem.Value)
        CreateGVActivity()
    End Sub

    Private Sub loadActivityMappedClasses(cboClass As DropDownList, ActivityGrpID As Integer)
        cboClass.Items.Clear()
        Dim sqlStr As String = "Select Distinct ClassName,ClassID From vw_ExamSubjectMapping Where majorGroupID=" & ActivityGrpID
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            cboClass.Items.Add(New ListItem(myReader(0), myReader(1)))
        End While
        myReader.Close()
    End Sub
    Private Sub CreateGVActivity()
       gvmarks.Visible = True
        GVCreateMarksEntry.SelectCommand = "Select SID,RegNo,ClassRollNo, SName From vw_Student Where ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' Order By Convert(int,ClassRollNo),sname"
        gvmarks.DataBind()

    End Sub
    Private Sub gvmarks_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvmarks.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim sqlStr As String = ""

            Dim SID As Integer = gvmarks.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim subjectID As Integer = 0
            sqlStr = "Select * from ExamStudentActivityMapping Where SID='" & SID & "' AND SubGrpID =" & cboMinorSubjectGroups.SelectedItem.Value

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            Dim cboActivityOne As DropDownList = DirectCast(e.Row.FindControl("cboActivityOne"), DropDownList)
            Dim cboActivityTwo As DropDownList = DirectCast(e.Row.FindControl("cboActivityTwo"), DropDownList)
            Dim myMarks As Integer = 0
            getRemarks(cboActivityOne)
            getRemarks(cboActivityTwo)

            While myReader.Read
                Try
                    subjectID = myReader("Act1SubjectID")
                Catch ex As Exception

                End Try
                Try
                    cboActivityOne.ClearSelection()
                    cboActivityOne.Items.FindByValue(subjectID).Selected = True
                Catch ex As Exception

                End Try
                Try
                    subjectID = myReader("Act2SubjectID")
                Catch ex As Exception

                End Try
                Try
                    cboActivityTwo.ClearSelection()
                    cboActivityTwo.Items.FindByValue(subjectID).Selected = True
                Catch ex As Exception

                End Try
            End While
            myReader.Close()
        End If
    End Sub
    Private Sub getRemarks(myCbo As DropDownList)
        For i = 0 To cboActivity.Items.Count - 1
            myCbo.Items.Add(New ListItem(cboActivity.Items(i).Text, cboActivity.Items(i).Value))
        Next
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim sqlStr As String = ""
        For Each gvr As GridViewRow In gvmarks.Rows
            Dim SID As Integer = gvmarks.DataKeys(gvr.RowIndex).Value.ToString()
            Dim subjectID As Integer = 0
            sqlStr = "Delete from ExamStudentActivityMapping Where SID='" & SID & "' AND SubGrpID =" & cboMinorSubjectGroups.SelectedItem.Value
            ExecuteQuery_Update(sqlStr)

            Dim cboActivityOne As DropDownList = DirectCast(gvr.FindControl("cboActivityOne"), DropDownList)
            Dim cboActivityTwo As DropDownList = DirectCast(gvr.FindControl("cboActivityTwo"), DropDownList)
           
            sqlStr = "Insert into ExamStudentActivityMapping (SID,Act1subjectID,Act2subjectID,SubGrpID) Values " & _
                "(" & SID & "," & cboActivityOne.SelectedItem.Value & "," & cboActivityTwo.SelectedItem.Value & "," & cboMinorSubjectGroups.SelectedItem.Value & ")"
            ExecuteQuery_Update(sqlStr)

        Next
    End Sub

    Protected Sub btnSet_Click(sender As Object, e As EventArgs) Handles btnSet.Click
        Dim activityNo As String = "", SubID As Integer
        If cboAct.SelectedIndex = 1 Then
            activityNo = "Two"
        Else
            activityNo = "One"
        End If
        Try
            SubID = cboActivity.SelectedItem.Value
        Catch ex As Exception

        End Try
        For Each gvr As GridViewRow In gvmarks.Rows
            Dim cboActivityOne As DropDownList = DirectCast(gvr.FindControl("cboActivity" & activityNo), DropDownList)
            Try
                cboActivityOne.ClearSelection()
                cboActivityOne.Items.FindByValue(SubID).Selected = True
            Catch ex As Exception

            End Try
        Next
    End Sub
End Class