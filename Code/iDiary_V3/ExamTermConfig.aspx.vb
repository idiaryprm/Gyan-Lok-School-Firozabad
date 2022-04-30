Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Partial Class ExamTermConfig
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        btnSaveMinor.Visible = False
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        lblSchoolID.Text = FindMasterID(71, cboSchoolName.Text)
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.Text)
        'LoadMasterInfo(101, lstMaster)
        txtGrpID.Text = 0
        lblHead.Text = ""
        cboExamGroup.Focus()
    End Sub


    Protected Sub btnSaveMinor_Click(sender As Object, e As EventArgs) Handles btnSaveMinor.Click
        If cboExamGroup.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Exam Group...');", True)
            cboExamGroup.Focus()
            Exit Sub
        End If
        If cboExamSubjectGroup.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Subject Group...');", True)
            cboExamSubjectGroup.Focus()
            Exit Sub
        End If
        Dim TermCount As Integer = 0
        Dim TermName As String = ""
        For Each row As GridViewRow In gvTerms.Rows
            Dim cbMajorTerm As CheckBox = DirectCast(row.FindControl("cbMajorTerm"), CheckBox)
            Dim lblMajorTerm As Label = DirectCast(row.FindControl("lblMajorTermID"), Label)
            If cbMajorTerm.Checked = True Then
                TermCount = 1
                'TermName = lblMajorTerm.Text
                Exit For
            End If
        Next
        If TermCount = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select atleast one Term...');", True)
            gvTerms.Focus()
            Exit Sub
        End If
        TermName = ""
        TermCount = 0
        For Each row As GridViewRow In gvTerms.Rows
            TermCount += 1
            Dim cbMajorTerm As CheckBox = DirectCast(row.FindControl("cbMajorTerm"), CheckBox)
            Dim lblMajorTerm As Label = DirectCast(row.FindControl("lblMajorTermID"), Label)
            Dim txtWeight As TextBox = DirectCast(row.FindControl("txtWeightage"), TextBox)
            If cbMajorTerm.Checked = True And Val(txtWeight.Text) = 0 Then
                TermName = cbMajorTerm.Text
                Exit For
            End If
        Next
        If TermName <> "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Provide valid  Weightage for Term: " & TermName & "');", True)
            gvTerms.Focus()
            Exit Sub
        End If
        TermName = ""
        TermCount = 0
        For Each row As GridViewRow In gvTerms.Rows
            TermCount = 0
            Dim cbMajorTerm As CheckBox = DirectCast(row.FindControl("cbMajorTerm"), CheckBox)
            Dim lblMajorTerm As Label = DirectCast(row.FindControl("lblMajorTermID"), Label)
            Dim cblMinorTerm As CheckBoxList = DirectCast(row.FindControl("cblMinorTerm"), CheckBoxList)
            TermName = cbMajorTerm.Text
            If cbMajorTerm.Checked = True Then
                For i = 0 To cblMinorTerm.Items.Count - 1
                    If cblMinorTerm.Items(i).Selected = True Then
                        TermCount = 1
                        TermName = cbMajorTerm.Text
                        Exit For
                    End If
                Next
                If TermCount = 0 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Provide atleast one Minor term for Term: " & TermName & "');", True)
                    gvTerms.Focus()
                    Exit Sub
                End If
            End If
        Next

        Dim sqlStr As String = ""
        Dim FinalMessage = ""

        For Each row As GridViewRow In gvTerms.Rows
            Dim cbMajorTerm As CheckBox = DirectCast(row.FindControl("cbMajorTerm"), CheckBox)
            Dim lblMajorTerm As Label = DirectCast(row.FindControl("lblMajorTermID"), Label)
            Dim txtWeight As TextBox = DirectCast(row.FindControl("txtWeightage"), TextBox)
            sqlStr = "Delete from ExamTermConfig Where ExamGroupID=" & Val(txtGrpID.Text) & " AND ExamMajorTermID=" & lblMajorTerm.Text & " AND SubGrpID=" & txtExmGrpID.Text
            ExecuteQuery_Update(sqlStr)

            Dim cblMinorTerm As CheckBoxList = DirectCast(row.FindControl("cblMinorTerm"), CheckBoxList)

            For i = 0 To cblMinorTerm.Items.Count - 1
                If cblMinorTerm.Items(i).Selected = True Then
                    sqlStr = "Insert Into ExamTermConfig (SubGrpID,examGroupID,ExamMajorTermID,termWeightage,ExamMinorTermID,CreatedBy) Values " & _
                        "(" & txtExmGrpID.Text & "," & txtGrpID.Text & "," & lblMajorTerm.Text & "," & Val(txtWeight.Text) & "," & cblMinorTerm.Items(i).Value & "," & Request.Cookies("UserID").Value & ")"
                    ExecuteQuery_Update(sqlStr)
                End If
            Next
        Next

        FinalMessage = "Exam Terms Configured successfully..."

        'LoadMasterInfo(101, cboExamGroup)

        'gvTerms.Visible = False
        'btnSaveMinor.Visible = False
        'cboExamSubjectGroup.Items.Clear()
        'lblHead.Text = ""
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
    End Sub


    'Public dsMajor As DataSet = ExecuteQuery_DataSet("Select ExamTermName,ExamTermID From examTermMaster where isMinor=0 order by DisplayOrder", "tbMajor")
    Public dsMinor As DataSet = ExecuteQuery_DataSet("Select ExamTermName,ExamTermID From examTermMaster where isMinor=1 order by DisplayOrder", "tbMinor")

    Private Sub gvTerms_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTerms.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cbMajorTerm As CheckBox = DirectCast(e.Row.FindControl("cbMajorTerm"), CheckBox)

            Dim lblMajorTerm As Label = DirectCast(e.Row.FindControl("lblMajorTermID"), Label)
            Dim SelectedTermID As Integer = 0
            If lblSelectedIDMajor.Text.Contains(":" & lblMajorTerm.Text & ":") Then
                cbMajorTerm.Checked = True
                SelectedTermID = lblMajorTerm.Text
                DirectCast(e.Row.FindControl("txtWeightage"), TextBox).Text = getTermWeightage(lblMajorTerm.Text)
            End If

            Dim cblMinorTerm As CheckBoxList = DirectCast(e.Row.FindControl("cblMinorTerm"), CheckBoxList)
            '   Dim dsMinor As DataSet = ExecuteQuery_DataSet("Select ExamTermName,ExamTermID From examTermMaster where isMinor=1 order by DisplayOrder", "tbMinor")
            cblMinorTerm.DataSource = dsMinor
            cblMinorTerm.DataTextField = "ExamTermName"
            cblMinorTerm.DataValueField = "ExamTermID"
            cblMinorTerm.DataBind()

            Dim mappedIDs As String = getMappedMinorTerms(SelectedTermID)
            For i = 0 To cblMinorTerm.Items.Count - 1
                If mappedIDs.Contains(":" & cblMinorTerm.Items(i).Value & ":") Then
                    cblMinorTerm.Items(i).Selected = True
                Else
                    cblMinorTerm.Items(i).Selected = False
                End If
            Next
        End If
    End Sub

    Private Function getMappedMinorTerms(majorID As Integer) As String
        Dim rv As String = ""
        Dim sqlStr As String = "Select ExamMinorTermID From ExamTermConfig Where examGroupID='" & txtGrpID.Text & "' AND SubgrpID=" & txtExmGrpID.Text & " AND ExamMajorTermID=" & majorID
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            rv &= ":" & myReader(0) & ":"
        End While
        myReader.Close()
        Return rv
    End Function

    Private Function getTermWeightage(majorID As Integer) As String
        Dim rv As String = ""
        Dim sqlStr As String = "Select top(1) termWeightage From ExamTermConfig Where SubGrpID='" & txtExmGrpID.Text & "' AND examGroupID='" & txtGrpID.Text & "' AND ExamMajorTermID=" & majorID
        rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Return rv
    End Function

    Protected Sub cboExamSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboExamSubjectGroup.SelectedIndexChanged

    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If cboExamGroup.Text = "" Then
            'FinalMessage = "Please Select Exam Group Name..."
            cboExamGroup.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Exam Group Name...');", True)
            Exit Sub
        End If
        If cboExamSubjectGroup.Text = "" Then
            'FinalMessage = "Please Select Exam Group Name..."
            cboExamSubjectGroup.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Subject Group Name...');", True)
            Exit Sub
        End If
        gvTerms.Visible = True
        txtGrpID.Text = FindMasterID(101, cboExamGroup.Text)
        txtExmGrpID.Text = FindMasterID(103, cboExamSubjectGroup.SelectedItem.Text)
        SqlDataSource1.SelectCommand = "SELECT * FROM [ExamTermMaster] Where isMinor=0 order by DisplayOrder"
        gvTerms.DataBind()
        Dim mappedTerms As String = ""
        Dim sqlStr As String = "Select distinct ExamMajorTermID From ExamTermConfig Where SubGrpID=" & txtExmGrpID.Text & " AND examGroupID='" & txtGrpID.Text & "'"
        lblSelectedIDMajor.Text = ""
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lblSelectedIDMajor.Text &= ":" & myReader(0) & ":"
        End While
        myReader.Close()

        gvTerms.DataBind()
        btnSaveMinor.Visible = True
        'lblHead.Text = "Term Configuration for : " & cboExamGroup.Text & ", " & cboExamSubjectGroup.SelectedItem.Text
    End Sub

    Private Sub cboExamGroup_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboExamGroup.SelectedIndexChanged
        Dim ExamGroupID As Integer = FindMasterID(101, cboExamGroup.SelectedItem.Text)
        LoadSubjectGroups(cboExamSubjectGroup, 0, ExamGroupID)
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        lblSchoolID.Text = FindMasterID(71, cboSchoolName.Text)
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.Text)
    End Sub
End Class
