Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Partial Class ExamSubjectPriority
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
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

        ' LoadSubjectGroups(cboSubjectGroup, 0)
        '   cboSection.Items.Clear()
        lstSubjects.Items.Clear()
        lblStatus.Text = ""
        cboClass.Focus()
    End Sub


    Private Sub RefreshList(ByVal className As String, SubGrpID As Integer)
        Dim sqlStr As String = ""
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        sqlStr = "Select Distinct SubjectName,SubJectID,Priority From vw_ExamSubjectMapping Where ClassName='" & className & "' AND ASID=" & Request.Cookies("ASID").Value & " AND (SubGrpID=" & SubGrpID & " or majorGroupID=" & SubGrpID & ") Order By Priority,SubJectID,SubjectName"
      
        Dim myCommand As New SqlCommand(sqlStr, myConn)
        Dim myReader As SqlDataReader = myCommand.ExecuteReader
        lstSubjects.Items.Clear()
        While myReader.Read
            lstSubjects.Items.Add(New ListItem(myReader(0), myReader(1)))
        End While
        myReader.Close()
        myCommand.Dispose()
        myConn.Dispose()
    End Sub

    Protected Sub cboClassSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadSubjectGroups(cboSubjectGroup, 0, getExamGroupIDfromClass(cboClass.Text))
        cboClass.Focus()
    End Sub

    Private Function GetSubjectID(ByVal SubjectName As String) As Integer
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = "Select Max(SubjectID) From ExamSubjectMaster Where SubjectName='" & SubjectName & "'"
        Dim myCommand As New SqlCommand(sqlstr, myConn)
        Dim rv As Integer = 0
        rv = myCommand.ExecuteScalar
        myCommand.Dispose()
        myConn.Dispose()
        Return rv
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sqlStr As String = ""
        Dim i As Integer = 0
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        ' Dim cssID As Integer = FindCSSID(cboClass.Text, cboSection.Text, cboSubSection.Text)

        For i = 0 To lstSubjects.Items.Count - 1
            Dim SubjectID As Integer = lstSubjects.Items(i).Value
            sqlStr = "Update ExamSubjectMapping Set Priority=" & i + 1 & " Where CSSID IN(select CSSID from classstudent Where ClassID=" & ClassID & ") AND ASID=" & Request.Cookies("ASID").Value & " AND SubjectID=" & SubjectID
            ExecuteQuery_Update(sqlStr)
        Next

        System.Threading.Thread.Sleep(500)
        RefreshList(cboClass.Text, getExamGroupIDfromClass(cboClass.Text))
        'InitControls()
    End Sub

    Protected Sub btnDown_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDown.Click
        If lstSubjects.SelectedIndex < lstSubjects.Items.Count - 1 Then
            Dim myIndex As Integer = lstSubjects.SelectedIndex
            Dim TempText As String = lstSubjects.Items(myIndex + 1).Text
            Dim TempValue As String = lstSubjects.Items(myIndex + 1).Value
            lstSubjects.Items(myIndex + 1).Text = lstSubjects.Items(myIndex).Text
            lstSubjects.Items(myIndex).Text = TempText
            lstSubjects.Items(myIndex + 1).Value = lstSubjects.Items(myIndex).Value
            lstSubjects.Items(myIndex).Value = TempValue
            lstSubjects.Items(myIndex).Selected = False
            lstSubjects.Items(myIndex + 1).Selected = True
        End If
    End Sub

    Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUp.Click
        If lstSubjects.SelectedIndex > 0 Then
            Dim myIndex As Integer = lstSubjects.SelectedIndex
            Dim TempStr As String = lstSubjects.Items(myIndex - 1).Text
            Dim TempValue As String = lstSubjects.Items(myIndex - 1).Value
            lstSubjects.Items(myIndex - 1).Text = lstSubjects.Items(myIndex).Text
            lstSubjects.Items(myIndex).Text = TempStr
            lstSubjects.Items(myIndex - 1).Value = lstSubjects.Items(myIndex).Value
            lstSubjects.Items(myIndex).Value = Tempvalue
            lstSubjects.Items(myIndex).Selected = False
            lstSubjects.Items(myIndex - 1).Selected = True
        End If

    End Sub

    Protected Sub cboSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubjectGroup.SelectedIndexChanged
        RefreshList(cboClass.Text, cboSubjectGroup.SelectedItem.Value)
    End Sub
End Class
