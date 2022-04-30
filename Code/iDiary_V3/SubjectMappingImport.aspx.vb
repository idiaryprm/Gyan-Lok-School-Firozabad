Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class SubjectMappingImport
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Exam") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("Login.aspx")
            End If

        End Try

    End Sub

    Protected Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        If cboClassSource.Text = "" Then
            lblStatus.Text = "Invalid Source Class..."
            cboClassSource.Focus()
            Exit Sub
        End If

        If cboSectionSource.Text = "" Then
            lblStatus.Text = "Invalid Source Section..."
            cboSectionSource.Focus()
            Exit Sub
        End If

        If cboClassTarget.Text = "" Then
            lblStatus.Text = "Invalid Target Class..."
            cboClassTarget.Focus()
            Exit Sub
        End If

        If cboSectionTarget.Text = "" Then
            lblStatus.Text = "Invalid Target Section..."
            cboSectionTarget.Focus()
            Exit Sub
        End If

        Dim SourceClassID As Integer = FindMasterID(2, cboClassSource.Text)
        Dim SourceSecID As Integer = 0
        'FindSectionID(cboClassSource.Text, cboSectionSource.Text)
        Dim TargetClassID As Integer = FindMasterID(2, cboClassTarget.Text)
        Dim TargetSecID As Integer = 0
        'FindSectionID(cboClassTarget.Text, cboSectionTarget.Text)

        Dim sqlStr As String = ""
        sqlStr = "Delete From ImportSubjectMapping "
        
        
        ExecuteQuery_Update(SqlStr)

        'Move Selected Record to Import Table
        sqlStr = "Insert into ImportSubjectMapping " & _
        " Select ClassID, SecID, SubjectID, Priority, ASID From SubjectMapping Where ClassID=" & SourceClassID & " AND SecID=" & SourceSecID
        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "Update ImportSubjectMapping " & _
        " Set ClassID=" & TargetClassID & ", SecID=" & TargetSecID
        
        
        ExecuteQuery_Update(SqlStr)


        Dim lstSubjectID As New ListBox
        lstSubjectID.Items.Clear()

        sqlStr = "Select * From ImportSubjectMapping"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstSubjectID.Items.Add(myReader("SubjectID"))
        End While
        myReader.Close()

        Dim i As Integer = 0
        For i = 0 To lstSubjectID.Items.Count - 1

            sqlStr = "Select Count(*) From SubjectMapping Where SubjectID=" & lstSubjectID.Items(i).Text & " AND ClassID=" & TargetClassID & " AND SecID=" & TargetSecID & " AND ASID=" & Request.Cookies("ASID").Value
            
            
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                rv = 0
            End Try

            If rv <= 0 Then
                sqlStr = "Insert into SubjectMapping Select * From ImportSubjectMapping Where SubjectID=" & lstSubjectID.Items(i).Text
                
                
                ExecuteQuery_Update(SqlStr)
            End If

        Next

        
        

        InitControls()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(2, cboClassSource)
        cboClassSource.Text = Request.QueryString("ClassName")
        'LoadClassSection(cboClassSource.Text, cboSectionSource)
        cboSectionSource.Text = Request.QueryString("SecName")

        LoadMasterInfo(2, cboClassTarget)
        cboSectionTarget.Items.Clear()

        lblStatus.Text = ""
        cboClassTarget.Focus()
    End Sub

    Protected Sub cboClassTarget_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClassTarget.SelectedIndexChanged
        'LoadClassSection(cboClassTarget.Text, cboSectionTarget)
    End Sub

    Protected Sub cboClassSource_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClassSource.SelectedIndexChanged
        'LoadClassSection(cboClassSource.Text, cboSectionSource)
    End Sub

End Class