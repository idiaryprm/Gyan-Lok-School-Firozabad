Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports System.Data.SqlClient

Public Class ExamGradeMapping
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        cboExamGroup.Focus()
        FillDataBox()
        lblStatus.Text = ""
        cboExamGroup.SelectedIndex = 0
        cboSubjectGroup.SelectedIndex = 0
        cboGradePoint.SelectedIndex = 0
        GridView1.Visible = False
        ' GridView2.Visible = False
    End Sub
    Protected Sub FillDataBox()
        Dim sqlstr As String = "SELECT GradeName,GradeID FROM [ExamGradeMaster]"
        cboGradePoint.Items.Clear()
        cboGradePoint.Items.Add("  ")
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            cboGradePoint.Items.Add(New ListItem(myReader(0), myReader(1)))
        End While
        myReader.Close()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cboExamGroup.SelectedItem.Text = "" Then
            lblStatus.Text = "Please Select Exam Group"
            cboExamGroup.Focus()
            Exit Sub
        End If
        If cboSubjectGroup.SelectedItem.Text = "" Then
            lblStatus.Text = "Please Select Exam Subject Group"
            cboSubjectGroup.Focus()
            Exit Sub
        End If
        If Trim(cboGradePoint.SelectedItem.Text) = "" Then
            lblStatus.Text = "Please Select Grade Scale Name"
            cboGradePoint.Focus()
            Exit Sub
        End If

        Dim ExamGroupID As Integer = FindMasterID(101, cboExamGroup.SelectedItem.Text)
        'Dim ExamSubGroupID As Integer = FindMasterID(103, cboSubSubjectGroup.SelectedItem.Text)
        Dim subGrpID As Integer = 0

        subGrpID = cboSubjectGroup.SelectedItem.Value

        Dim sqlstr As String = ""
        If lblStatus.Text = "" Then
            sqlstr = "Insert into ExamGradeMapping values('" & ExamGroupID & "','" & subGrpID & "','" & cboGradePoint.SelectedItem.Value & "')"
        Else
            sqlstr = "Update ExamGradeMapping Set GradePointID='" & cboGradePoint.SelectedItem.Value & "' WHERE EGroupID='" & ExamGroupID & "' AND subGrpID='" & subGrpID & "'"
        End If
        ExecuteQuery_Update(sqlstr)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Saved Successfully');", True)
        InitControls()
    End Sub

    Protected Sub cboGradePoint_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGradePoint.SelectedIndexChanged
        SqlDataSource1.SelectCommand = "Select DisplayOrder,[UpperValue], [LowerValue], [Grade], [GradePoint] FROM [ExamGradeDetails] Where GradeID = '" & cboGradePoint.SelectedItem.Value & "' Order By DisplayOrder"
        GridView1.DataBind()
        GridView1.Visible = True
    End Sub

    Protected Sub cboSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubjectGroup.SelectedIndexChanged

        Dim sqlStr As String = "SELECT DISTINCT [GradeName], [ExamGroupName], [subGroupName] FROM [vw_ExamGradeMapping] WHERE ExamGroupName='" & cboExamGroup.SelectedValue & "' AND subGrpID='" & cboSubjectGroup.Text & "'"
        Dim gradeName As String = ""
        Try
            gradeName = ExecuteQuery_ExecuteScalar(sqlStr)
            'cboGradePoint.SelectedValue = gradeName
            cboGradePoint.ClearSelection()
            cboGradePoint.Items.FindByText(gradeName).Selected = True
            SqlDataSource1.SelectCommand = "Select DisplayOrder,[UpperValue], [LowerValue], [Grade], [GradePoint] FROM [ExamGradeDetails] Where GradeID = '" & cboGradePoint.SelectedItem.Value & "' Order By DisplayOrder"
            GridView1.DataBind()
            GridView1.Visible = True
        Catch ex As Exception

        End Try

        If gradeName <> "" Then
            lblStatus.Text = gradeName & " Already Mapped"
        Else
            lblStatus.Text = ""
        End If
       
    End Sub

    Private Sub cboExamGroup_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboExamGroup.SelectedIndexChanged
        Dim ExamGroupID As Integer = FindMasterID(101, cboExamGroup.SelectedItem.Text)
        LoadSubjectGroups(cboSubjectGroup, 0, ExamGroupID)
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
    End Sub
End Class