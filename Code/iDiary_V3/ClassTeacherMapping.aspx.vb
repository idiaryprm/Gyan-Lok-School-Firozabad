Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Admin_ClassTeacherMapping
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        ' LoadClassSections()

        ' chkIsClassTeahcer.Checked = False
        LoadMasterInfo(2, cboClass)
        LoadMasterInfo(3, cboSection)
        LoadMasterInfo(62, cboSubSection)
        'cboSection.Items.Clear()
        lstMappedTeachers.Items.Clear()
        lstTeachers.Items.Clear()
        cboClassTeacher.Items.Clear()
        cboClassTeacher.Enabled = False

    End Sub

    'Private Sub LoadClassSections()
    '   
    '    
    '   

    '    

    '    Dim sqlStr As String = "Select SecName From Sections Where SchoolID=" & Session("SchoolID")
    '    
    '    

    '    cboClass.Items.Clear()
    '    cboClass.Items.Add("")

    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    While myReader.Read
    '        cboClass.Items.Add(myReader(0))
    '    End While
    '    myReader.Close()

    '    
    '    
    'End Sub

    Private Sub LoadTeachers()
       
        
       

        

        Dim sqlStr As String = "Select EmpName,EmpCode From EmployeeMaster Where EmpCatID = 1"
        
        

        lstTeachers.Items.Clear()

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstTeachers.Items.Add(myReader("EmpName") & "[[" & myReader("EmpCode") & "]]")
        End While
        myReader.Close()

        
        
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboClass.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Class is Required...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Section is Required...');", True)
            cboSection.Focus()
            Exit Sub
        End If

        If cboClassTeacher.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Class Teacher is Required...');", True)
            cboClassTeacher.Focus()
            Exit Sub
        End If

        Dim str As String = ""
        Dim TeacherName As String = ""
        Dim empCode As String = ""
        Dim TeacherID As Integer = 0
        'Dim SecID As Integer = 0
        Dim CSSID As Integer = FindCSSID(cboClass.Text, cboSection.Text, cboSubSection.Text)
        ' Dim IsClassTeacher As Integer = 0
        '  If chkIsClassTeahcer.Checked = True Then IsClassTeacher = 1

       
        
       

        
        Dim sqlStr As String

        'SecID = 0
        sqlStr = "Delete From ClassTeachersMap Where CSSID='" & CSSID & "'"
        
        
        ExecuteQuery_Update(SqlStr)

        For j As Integer = 0 To lstMappedTeachers.Items.Count - 1

            str = lstMappedTeachers.Items.Item(j).Text
            TeacherName = str.Substring(0, str.IndexOf("[["))
            empCode = str.Substring(str.IndexOf("[[") + 2, str.IndexOf("]]") - str.IndexOf("[[") - 2)
            TeacherID = GetTeacherID(TeacherName, empCode)
            sqlStr = "Insert into ClassTeachersMap (CSSID, EmpID) Values(" & _
            CSSID & "," & _
            TeacherID & ")"

            
            
            ExecuteQuery_Update(SqlStr)

        Next

        str = cboClassTeacher.Text
        TeacherName = str.EndsWith("[[")
        empCode = str.Substring(str.IndexOf("[[") + 2, str.IndexOf("]]") - str.IndexOf("[[") - 2)
        TeacherID = GetTeacherID(TeacherName, empCode)
        'SecID = 0
        sqlStr = "Select EmpId From Employeemaster where empcode='" & empCode & "'"
        
        
        Dim empID As Integer = ExecuteQuery_ExecuteScalar(SqlStr)

        sqlStr = "Update ClassStudent Set EmpID='" & empID & "' where CSSID=" & CSSID
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Teachers Mapped Successfully...');", True)
        InitControls()

        ' LoadMappedTeachers()
        'chkIsClassTeahcer.Checked = False
    End Sub

    Private Function GetTeacherID(ByVal TeacherName As String, ByVal EmployeeCode As String) As Integer
       
        
       

        

        Dim sqlStr As String = "Select Max(EmpID) From EmployeeMaster Where EmpCode='" & EmployeeCode & "' AND EmpName='" & TeacherName & "'"
        
        
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception
            rv = 0
        End Try

        
        

        Return rv
    End Function

    'Private Function GetClassSecID(ByVal ClassSecName As String, ByVal SchoolID As Integer) As Integer
    '   
    '    
    '   

    '    

    '    Dim sqlStr As String = "Select Max(SecID) From Sections Where SchoolID=" & SchoolID & " AND SecName='" & ClassSecName & "'"
    '    
    '    
    '    Dim rv As Integer = 0
    '    Try
    '        rv = ExecuteQuery_ExecuteScalar(SqlStr)
    '    Catch ex As Exception
    '        rv = 0
    '    End Try

    '    
    '    

    '    Return rv
    'End Function

    'Protected Sub cboClassSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
    '    'LoadClassSection(cboClass.Text, cboSection)
    'End Sub

    Private Sub LoadMappedTeachers()
       
        
       

        

        Dim sqlStr As String = "Select EmpName,EmpCode From vw_Teacher Where SecName='" & cboSection.Text & "' AND ClassName='" & cboClass.Text & "'"
        
        

        lstMappedTeachers.Items.Clear()

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstMappedTeachers.Items.Add(myReader("EmpName") & "[[" & myReader("EmpCode") & "]]")
        End While
        myReader.Close()

        
        
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        LoadTeachers()
        LoadMappedTeachers()
    End Sub

    Protected Sub lstTeachers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTeachers.SelectedIndexChanged
      
    End Sub

    Protected Sub lstMappedTeachers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMappedTeachers.SelectedIndexChanged

    End Sub

    Protected Sub btnSave0_Click(sender As Object, e As EventArgs) Handles btnClassTeacher.Click
        cboClassTeacher.Enabled = True
        cboClassTeacher.Items.Clear()
        cboClassTeacher.Items.Add("")
        For i As Integer = 0 To lstMappedTeachers.Items.Count - 1
            cboClassTeacher.Items.Add(lstMappedTeachers.Items.Item(i).Text)
        Next
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If lstMappedTeachers.Items.Contains(lstTeachers.SelectedItem) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Entry already Exists...');", True)
            Exit Sub
        End If
        lstMappedTeachers.Items.Add(lstTeachers.SelectedItem.Text)
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        lstMappedTeachers.Items.RemoveAt(lstMappedTeachers.SelectedIndex)
    End Sub

    Protected Sub cboSubSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubSection.SelectedIndexChanged
        LoadTeachers()
        LoadMappedTeachers()
    End Sub
End Class
