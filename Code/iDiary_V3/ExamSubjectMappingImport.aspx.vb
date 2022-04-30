Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Partial Class ExamSubjectMappingImport
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
        btnCopy.Visible = False
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        cboSectionS.Items.Clear()
        cboSubSectionS.Items.Clear()
        lblStatus.Text = ""
        cboExamGroup.Focus()
        cblClasses.Visible = False
        cbAll.Visible = False
    End Sub

    Protected Sub cboClassS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClassS.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClassS.SelectedItem.Text, cboSectionS)
    End Sub

    Protected Sub cboSectionS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSectionS.SelectedIndexChanged
        LoadClassSubSection(cboSchoolName.Text, cboClassS.SelectedItem.Text, cboSectionS.SelectedItem.Text, cboSubSectionS)
        'RefreshList()
    End Sub

    Private Sub RefreshList(ByVal CSSID As Integer)
      
        Dim sqlStr As String = ""
        sqlStr = "Select SubjectName From vw_ExamSubjectMapping Where CSSID=" & CSSID & " AND ASID=" & Request.Cookies("ASID").Value
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        lstSelected.Items.Clear()
        While myReader.Read
            lstSelected.Items.Add(myReader(0))
        End While
        myReader.Close()

    End Sub

    Protected Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click

        Dim SecID As Integer = 0
        'FindSectionID(cboClassD.Text, cboSectionD.Text)
        Dim CSSID_S As Integer = FindCSSID(cboSchoolName.Text, cboClassS.SelectedItem.Text, cboSectionS.Text, cboSubSectionS.Text)
        Dim CSSID_D As Integer = 0

        Dim sqlstr As String = "", tmpSql As String = ""
        Dim lstInsertQuery As New List(Of String)
        Dim lstDeleteQuery As New List(Of String)
        For i = 0 To cblClasses.Items.Count - 1
            If cblClasses.Items(i).Selected = True Then
                CSSID_D = cblClasses.Items(i).Value
                sqlstr = "Delete from ExamSubjectMapping Where ASID='" & Request.Cookies("ASID").Value & "' and CSSID=" & CSSID_D
                lstDeleteQuery.Add(sqlstr)

                sqlstr = " Select [SubjectID],[DisplayType],[EntryType],[ApplicableInExam],[ApplicableInTimeTable],TTWeightage,Priority,ASID From ExamSubjectMapping " & _
                         " Where CSSID='" & CSSID_S & "' AND ASID=" & Request.Cookies("ASID").Value
                Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
                While myReader.Read
                    tmpSql = "INSERT INTO [ExamSubjectMapping] ([CSSID],[SubjectID],[DisplayType],[EntryType],[ApplicableInExam],[ApplicableInTimeTable]," & _
                        "[TTWeightage],[Priority],[ASID]) Values (" & _
                        "'" & CSSID_D & "','" & myReader(0) & "','" & myReader(1) & "','" & myReader(2) & "', '" & myReader(3) & "','" & myReader(4) & "'," & _
                        "'" & myReader(5) & "','" & myReader(6) & "','" & myReader(7) & "')"
                    lstInsertQuery.Add(tmpSql)
                End While
                myReader.Close()

            End If

        Next

        For i = 0 To lstDeleteQuery.Count - 1
            ExecuteQuery_Update(lstDeleteQuery(i))
        Next

        For i = 0 To lstInsertQuery.Count - 1
            ExecuteQuery_Update(lstInsertQuery(i))
        Next

        InitControls()

        RefreshList(CSSID_S)
        lblStatus.Text = "Subject Mapping Copied..."
    End Sub

    Protected Sub cboExamGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboExamGroup.SelectedIndexChanged
        lblGrpID.Text = FindMasterID(101, cboExamGroup.Text)
        LoadClasses(cboClassS, lblGrpID.Text, lblSchoolID.Text)
        cboExamGroup.Focus()
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click

        Dim CSSID As Integer = FindCSSID(cboSchoolName.Text, cboClassS.SelectedItem.Text, cboSectionS.SelectedItem.Text, cboSubSectionS.SelectedItem.Text)
        sdsCSSID.SelectCommand = "SELECT [CSSName], [CSSID] FROM [vw_ClassStudent] Where  ExamGroupID=" & lblGrpID.Text & " AND CSSID <>" & CSSID & " Order By ClassDisplayOrder,SectionDisplyOrder"
        cblClasses.DataBind()

        RefreshList(CSSID)
        btnCopy.Visible = True
        cblClasses.Visible = True
        cbAll.Visible = True
    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbAll.CheckedChanged
        CheckAll(cbAll.Checked)
    End Sub
    Private Sub CheckAll(ByVal isChecked As Boolean)
        For i = 0 To cblClasses.Items.Count - 1
            cblClasses.Items(i).Selected = isChecked
        Next
    End Sub

    Private Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(101, cboExamGroup, cboSchoolName.SelectedItem.Text)
        lblSchoolID.Text = getSchoolID(cboSchoolName.SelectedItem.Text)
    End Sub
End Class
