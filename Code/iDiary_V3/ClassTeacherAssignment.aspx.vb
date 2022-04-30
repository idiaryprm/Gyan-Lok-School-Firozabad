Imports System.Data.SqlClient
Imports iDiary_V3.iDiary_Security.CLS_iDiary_Security
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Partial Class ClassTeacherAssignment
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'If Request.Cookies("UType").Value.ToString.Contains("Exam") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
        If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("AccessDenied.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        BindClassTeacher()
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboClass.Focus()
    End Sub

    Private Function getEmployeeNamefromCode(empcode As String) As String
        Dim sqlStr As String = ""
        Dim rv As String = ""
        sqlStr = "Select top(1) EmpName From Employeemaster Where EmpCode='" & empcode & "'"
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try

        Return rv
    End Function

    Private Function getEmpIDfromCode(ByVal empCode As String) As Integer
        Dim sqlStr As String = ""
        Dim empID As Integer = 0
        sqlStr = "Select top(1) EMPID  From [EmployeeMaster] Where EmpCode='" & empCode & "'"
        Try
            empID = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try

        Return empID
    End Function

    Private Function FindUserID(ByVal myLoginID As String) As Integer
        Dim sqlStr As String = ""
        sqlStr = "Select MAX(userID) From Users Where LoginID='" & myLoginID & "'"
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try

        Return rv
    End Function

    'Private Function FindClassID(ByVal myClassName As String) As Integer
    '    Dim sqlStr As String = ""
    '    sqlStr = "Select MAX(ClassID) From Classes Where ClassName='" & myClassName & "'"
    '    Dim rv As Integer = 0
    '    Try
    '        rv = ExecuteQuery_ExecuteScalar(sqlStr)
    '    Catch ex As Exception

    '    End Try

    '    Return rv
    'End Function

    'Private Function FindSecID(ByVal mySecName As String, ByVal myClassName As String) As Integer
    '    Dim sqlStr As String = ""
    '    sqlStr = "Select MAX(SecID) From Sections Where SecName='" & mySecName & "' "
    '    Dim rv As Integer = 0
    '    Try
    '        rv = ExecuteQuery_ExecuteScalar(sqlStr)
    '    Catch ex As Exception

    '    End Try

    '    Return rv
    'End Function
    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        'lblCSSID.Text = FindCSSID(cboClass.Text, cboSection.Text, "")
        'LoadClassSubSection(cboClass.Text, cboSection.Text, cboSubSection)
        'chkClassTeacher.Checked = IsClassTeacher(lblCSSID.Text)

        'SqlDataSource2.SelectCommand = "SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_ExamSubjectMapping] Where Cssid=" & lblCSSID.Text
        'gvSubjectPermission.DataBind()
        '  loadMappedSubjects()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtEmpCode.Text = "" Or lblEmpID.Text = "" Then
            txtEmpCode.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Employee..');", True)
            Exit Sub
        End If
        If cboSchoolName.Text = "" Then
            cboSchoolName.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select School Name..');", True)
            Exit Sub
        End If
        If cboClass.Text = "" Then
            cboClass.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Class..');", True)
            Exit Sub
        End If
        If cboSection.Text = "" Then
            cboSection.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Section..');", True)
            Exit Sub
        End If
        Dim flagMarks As Integer = 0
        Dim SchoolID As Integer = FindMasterID(71, cboSchoolName.Text)
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = FindMasterID(3, cboSection.Text)
        'For Each row As GridViewRow In gvSubjectPermission.Rows
        '    Dim cbPermission As CheckBox = DirectCast(row.FindControl("cbPermission"), CheckBox)
        '    If cbPermission.Checked = True Then
        '        flagMarks = 1
        '        Exit For
        '    End If
        'Next

        'If flagMarks = 0 Then
        '    gvSubjectPermission.Focus()
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select atleast one Subject..');", True)
        '    Exit Sub
        'End If
        Dim sqlStr As String = ""
        ' Dim UserID As Integer = FindUserID(cbologinID.Text)
        '  Dim CssID As Integer = FindCSSID(cboClass.Text, cboSection.Text, "")
        'Dim SubjectID As Integer = 0
        'Dim IsClassTeacher As Integer = 0
        Dim ASID As Integer = 0
        Try
            ASID = Request.Cookies("ASID").Value
        Catch ex As Exception

        End Try
        'If chkClassTeacher.Checked = True Then
        '    IsClassTeacher = 1
        'Else
        '    IsClassTeacher = 0
        'End If
        'Dim IsPermissionApplicable As Integer = 0, IsTeaches As Integer = 0
        sqlStr = "update ClassStudent set EmpID=" & lblEmpID.Text & " where SchoolID='" & SchoolID & "' and ClassId=" & ClassID & " AND SecID=" & SecID
        ' & " AND CSSID=" & lblCSSID.Text & " And SubjectID in (Select [SubjectID] FROM [vw_ExamSubjectMapping] Where Cssid=" & lblCSSID.Text
        'sqlStr &= ")"
        ExecuteQuery_Update(sqlStr)

        
        Dim msg As String = ""
       
        msg = "Class Teacher Saved Successfully..."

        lblStatus.Text = msg
        BindClassTeacher()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & msg & "');", True)

        
    End Sub

    Protected Sub btnNameSearch_Click(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        sdsEmployee.SelectCommand = "SELECT EmpCode,EmpID, EmpName, DeptName, DesgName FROM vw_Employees WHERE EmpName Like '%" & txtName.Text & "%'"
        GridView1.DataBind()
        GridView1.Visible = True
        If GridView1.Rows.Count = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('No Employee Found!!!');", True)
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ShowInfo(GridView1.SelectedRow.Cells(1).Text)
        'BindPermission()
        GridView1.Visible = False
        GridView1.SelectedIndex = -1
    End Sub
    Private Sub ShowInfo(ByVal myEmpCode As String)
        If myEmpCode = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Employee');", True)
            txtName.Focus()
            Exit Sub
        End If
        lblEmpID.Text = ""
        Dim sqlStr As String = ""

        sqlStr = "Select * From vw_Employees Where EmpCode='" & myEmpCode & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            lblEmpID.Text = myReader("EmpID")
            Try
                txtEmpCode.Text = myReader("EmpCode")
            Catch ex As Exception

            End Try
            Try
                txtName.Text = myReader("EmpName")
            Catch ex As Exception

            End Try
            Try
                txtEmpDepartment.Text = myReader("DeptName")
            Catch ex As Exception

            End Try
            Try
                txtEmpDesignation.Text = myReader("DesgName")
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
        If lblEmpID.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Employee Code does not exist!!!');", True)
            txtEmpCode.Focus()
            Exit Sub
        End If
        'BindPermission()
    End Sub

    'Protected Sub cboSubSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubSection.SelectedIndexChanged
    '    gvSubjectPermission.Visible = True
    '    lblCSSID.Text = FindCSSID(cboClass.Text, cboSection.Text, cboSubSection.Text)
    '    SqlDataSource2.SelectCommand = "SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_ExamSubjectMapping] Where Cssid=" & lblCSSID.Text
    '    gvSubjectPermission.DataBind()
    'End Sub

    Protected Sub btnEmpCode_Click(sender As Object, e As EventArgs) Handles btnEmpCode.Click
        ShowInfo(txtEmpCode.Text)
    End Sub
    Private Sub BindClassTeacher()
        SqlDataSource1.SelectCommand = "SELECT distinct ClassName,SecName,EmpCode,EmpName,Displayorder, SecDisplayOrder FROM [vw_ClassTeacher] where SchoolName='" & cboSchoolName.Text & "' order by Displayorder, SecDisplayOrder"
        gvClassTeacher.DataBind()
    End Sub
    'Protected Sub cboSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubjectGroup.SelectedIndexChanged
    '    gvSubjectPermission.Visible = True
    '    lblCSSID.Text = FindCSSID(cboClass.Text, cboSection.Text, "")
    '    chkClassTeacher.Checked = IsClassTeacher(lblCSSID.Text)
    '    lblCSSID.Text = FindCSSID(cboClass.Text, cboSection.Text, cboSubSection.Text)
    '    Dim sqlStr As String = "SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_ExamSubjectMapping] Where Cssid=" & lblCSSID.Text
    '    If cboSubjectGroup.SelectedItem.Text = "" Then
    '    Else
    '        sqlStr &= " AND majorGroupID=" & cboSubjectGroup.SelectedItem.Value
    '    End If
    '    SqlDataSource2.SelectCommand = sqlStr
    '    gvSubjectPermission.DataBind()
    'End Sub
    'Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbAll.CheckedChanged
    '    CheckAll(cbAll.Checked)
    'End Sub
    'Private Sub CheckAll(ByVal isChecked As Boolean)
    '    For Each row As GridViewRow In gvSubjectPermission.Rows
    '        Dim cbPermission As CheckBox = DirectCast(row.FindControl("cbPermission"), CheckBox)
    '        cbPermission.Checked = isChecked
    '    Next
    'End Sub

    'Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
    '    gvSubjectPermission.Visible = True
    '    lblCSSID.Text = FindCSSID(cboClass.Text, cboSection.Text, "")
    '    chkClassTeacher.Checked = IsClassTeacher(lblCSSID.Text)
    '    lblCSSID.Text = FindCSSID(cboClass.Text, cboSection.Text, cboSubSection.Text)
    '    Dim sqlStr As String = "SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_ExamSubjectMapping] Where Cssid=" & lblCSSID.Text
    '    If cboSubjectGroup.SelectedItem.Text = "" Then
    '    Else
    '        sqlStr &= " AND majorGroupID=" & cboSubjectGroup.SelectedItem.Value
    '    End If
    '    SqlDataSource2.SelectCommand = sqlStr
    '    gvSubjectPermission.DataBind()
    'End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        BindClassTeacher()
    End Sub
End Class