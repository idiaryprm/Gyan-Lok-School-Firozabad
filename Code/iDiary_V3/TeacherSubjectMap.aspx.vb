Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Partial Class TeacherSubjectMap
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        cboEmpName.Items.Clear()
        LoadEmployees(0, "", cboEmpName)
        lblStatus.Text = ""
        LoadSubjects(chkSubject)
        'lblStatus.Text = ""
        chkAll.Checked = False
    End Sub

    Private Sub CheckSubjectMapping()
        For i = 0 To chkSubject.Items.Count - 1
            chkSubject.Items(i).Selected = False
        Next
        Dim EmpID As Integer = FindEmployeeID(0, "", cboEmpName.Text)
        Dim sqlCon As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ConnectionString)
        Dim sqlCmd As New SqlCommand("SELECT SubjectName from SubjectMaster INNER JOIN TeacherSubjectMap ON SubjectMaster.SubjectID=TeacherSubjectMap.SubjectID where EmpID=" & EmpID, sqlCon)
        Dim dataReader As SqlDataReader
        sqlCon.Open()
        dataReader = sqlCmd.ExecuteReader()
        While dataReader.Read
            For i = 0 To chkSubject.Items.Count - 1
                If chkSubject.Items(i).Text = dataReader(0) Then
                    chkSubject.Items(i).Selected = True
                End If
            Next
        End While
        dataReader.Close()
        sqlCon.Close()
    End Sub

    Public Shared Function LoadSubjects(ByRef myChk As CheckBoxList) As Integer
       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "SELECT SubjectName from SubjectMaster order by SubjectName"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        myChk.Items.Clear()
        While myReader.Read
            myChk.Items.Add(myReader(0))
        End While
        myReader.Close()
        
        
        Return 0
    End Function
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboEmpName.Text = "" Then
            lblStatus.Text = "Please choose a Employee..."
            cboEmpName.Focus()
            Exit Sub
        End If
       
       
       
        Dim sqlStr As String = ""
        
        'For update first Delete
        Dim EmpID As Integer = FindEmployeeID(2, "Teaching", cboEmpName.Text)
        sqlStr = "Delete from TeacherSubjectMap where EmpID=" & EmpID
        
        
        ExecuteQuery_Update(SqlStr)
        For i = 0 To chkSubject.Items.Count - 1
            Dim SubjectID As Integer = FindSubjectID(chkSubject.Items(i).Text)
            'Insert
            If chkSubject.Items(i).Selected = True Then
                sqlStr = "Insert into TeacherSubjectMap Values(" & EmpID & ", " & SubjectID & ")"
                
                
                ExecuteQuery_Update(SqlStr)
            End If
        Next
        
        
        InitControls()
        lblStatus.Text = "Mapping is Saved Successfully..."
    End Sub
    Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        Dim i As Integer = 0
        For i = 0 To chkSubject.Items.Count - 1
            If chkAll.Checked = True Then
                chkSubject.Items(i).Selected = True
            Else
                chkSubject.Items(i).Selected = False
            End If
        Next
    End Sub
    Protected Sub cboEmpName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmpName.SelectedIndexChanged
        CheckSubjectMapping()
        lblStatus.Text = ""
    End Sub
End Class
