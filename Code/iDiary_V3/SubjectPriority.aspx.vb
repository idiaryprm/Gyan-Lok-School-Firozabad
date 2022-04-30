Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Partial Class SubjectPriority
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
        LoadClasses()
        cboSection.Items.Clear()
        lstSubjects.Items.Clear()
        lblStatus.Text = ""
        cboClass.Focus()
    End Sub

    Private Sub LoadClasses()
       
       
       

        Dim sqlstr As String = "Select * From Classes"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cboClass.Items.Clear()
        cboClass.Items.Add("")
        While myReader.Read
            cboClass.Items.Add(myReader("ClassName"))
        End While
        myReader.Close()
        
        
    End Sub

    'Private Sub LoadClassSections()
    '   
    '   
    '   

    '    Dim sqlstr As String = "Select * From vw_ClassSections Where ClassName='" & cboClass.Text & "'"
    '    
    '    
    '    
    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    cboSection.Items.Clear()
    '    cboSection.Items.Add("")
    '    While myReader.Read
    '        cboSection.Items.Add(myReader("SecName"))
    '    End While
    '    myReader.Close()
    '    
    '    
    'End Sub

    Private Sub RefreshList(ByVal className As String, ByVal secName As String, ByVal SubSecName As String)
        Dim sqlStr As String = ""
       
       
       

        If SubSecName <> "" Then
            sqlStr = "Select Distinct SubjectName,Priority From vwSubjectMapping Where ClassName='" & className & "' AND SecName='" & secName & "' AND SubSecName='" & SubSecName & "' Order By Priority,SubjectName"
        Else
            sqlStr = "Select Distinct SubjectName,Priority From vwSubjectMapping Where ClassName='" & className & "' AND SecName='" & secName & "' Order By Priority,SubjectName"
        End If
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        lstSubjects.Items.Clear()
        While myReader.Read
            lstSubjects.Items.Add(myReader(0))
        End While
        myReader.Close()
        
        
    End Sub

    Protected Sub cboClassSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.Text, cboSection)
        cboClass.Focus()
    End Sub

    Private Function GetSubjectID(ByVal SubjectName As String) As Integer
       
       
       

        Dim sqlstr As String = "Select Max(SubjectID) From SubjectMaster Where SubjectName='" & SubjectName & "'"

        Dim rv As Integer = 0
        rv = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        Return rv
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sqlStr As String = ""
        Dim i As Integer = 0
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        Dim cssID As Integer = FindCSSID("", cboClass.Text, cboSection.Text, cboSubSection.Text)

       
       
       

        

        For i = 0 To lstSubjects.Items.Count - 1
            Dim SubjectID As Integer = GetSubjectID(lstSubjects.Items(i).Text)
            sqlStr = "Update SubjectMapping Set Priority=" & i + 1 & " Where CSSID=" & cssID & "  AND SubjectID=" & SubjectID
            
            
            ExecuteQuery_Update(SqlStr)
        Next

        System.Threading.Thread.Sleep(500)
        
        

        InitControls()
    End Sub

    'Private Function GetClassID(ByVal ClassName As String) As Integer
    '   
    '   
    '   

    '    Dim sqlstr As String = "Select Max(ClassID) From Classes Where ClassName='" & ClassName & "'"
    '    (sqlstr, myConn)
    '    Dim rv As Integer = 0
    '    rv = ExecuteQuery_ExecuteScalar(SqlStr)
    '    
    '    
    '    Return rv
    'End Function

    'Private Function GetSecID(ByVal SecName As String, ByVal ClassName As String) As Integer
    '   
    '   
    '   

    '    Dim sqlstr As String = "Select Max(SecID) From vw_Class_Section Where ClassName='" & ClassName & "' and SecName='" & SecName & "'"
    '    (sqlstr, myConn)
    '    Dim rv As Integer = 0
    '    rv = ExecuteQuery_ExecuteScalar(SqlStr)
    '    
    '    
    '    Return rv
    'End Function

    Protected Sub btnDown_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDown.Click
        If lstSubjects.SelectedIndex < lstSubjects.Items.Count - 1 Then
            Dim myIndex As Integer = lstSubjects.SelectedIndex
            Dim TempStr As String = lstSubjects.Items(myIndex + 1).Text
            lstSubjects.Items(myIndex + 1).Text = lstSubjects.Items(myIndex).Text
            lstSubjects.Items(myIndex).Text = TempStr
            lstSubjects.Items(myIndex).Selected = False
            lstSubjects.Items(myIndex + 1).Selected = True
        End If
    End Sub

    Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUp.Click
        If lstSubjects.SelectedIndex > 0 Then
            Dim myIndex As Integer = lstSubjects.SelectedIndex
            Dim TempStr As String = lstSubjects.Items(myIndex - 1).Text
            lstSubjects.Items(myIndex - 1).Text = lstSubjects.Items(myIndex).Text
            lstSubjects.Items(myIndex).Text = TempStr
            lstSubjects.Items(myIndex).Selected = False
            lstSubjects.Items(myIndex - 1).Selected = True
        End If

    End Sub

    Protected Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSection.SelectedIndexChanged
        LoadClassSubSection("", cboClass.Text, cboSection.Text, cboSubSection)
        RefreshList(cboClass.Text, cboSection.Text, "")
        cboSection.Focus()
    End Sub

    Protected Sub cboSubSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubSection.SelectedIndexChanged
        RefreshList(cboClass.Text, cboSection.Text, cboSubSection.Text)
    End Sub
End Class
