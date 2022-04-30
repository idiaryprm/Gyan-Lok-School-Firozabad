Imports System.Data.SqlClient
Imports iDiary_V3.iDiary_Security.CLS_iDiary_Security
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Partial Class ExamUserPermissions
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
        lblSchoolID.Text = FindMasterID(71, cboSchoolName.Text)
        gvSubjectPermission.Visible = False
        chkClassTeacher.Checked = False

        LoadMasterInfo(2, cboClass, cboSchoolName.Text)

        'cbologinID.Focus()

        chkClassTeacher.Checked = False
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.SelectedItem.Text, cboSection)
        chkClassTeacher.Checked = False
        lblExamGroupID.Text = getExamGroupIDfromClass(cboClass.Text)
        LoadSubjectGroups(cboSubjectGroup, 0, lblExamGroupID.Text)
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

    Private Function FindClassID(ByVal myClassName As String) As Integer
        Dim sqlStr As String = ""
        sqlStr = "Select MAX(ClassID) From Classes Where ClassName='" & myClassName & "'"
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try

        Return rv
    End Function

    Private Function FindSecID(ByVal mySecName As String, ByVal myClassName As String) As Integer
        Dim sqlStr As String = ""
        sqlStr = "Select MAX(SecID) From Sections Where SecName='" & mySecName & "' "
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try

        Return rv
    End Function

    Private Sub BindPermission()
        SqlDataSource1.SelectCommand = "SELECT [ClassName], [SecName], [SubjectCode], [SubjectName],[SubjectID] FROM [vw_UserSubjectPermission] WHERE ([EmpID] ='" & lblEmpID.Text & "' and SchoolID=" & lblSchoolID.Text & ")"
        gvMappedSubjects.DataBind()
    End Sub
    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        gvSubjectPermission.Visible = True
        Try
            lblCSSID.Text = FindCSSID(cboSchoolName.SelectedItem.Text, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, cboSubSection.Text)
            chkClassTeacher.Checked = IsClassTeacher(lblCSSID.Text)

            SqlDataSource2.SelectCommand = "SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_ExamSubjectMapping] Where Cssid=" & lblCSSID.Text
            gvSubjectPermission.DataBind()
        Catch ex As Exception

        End Try
       
        '  loadMappedSubjects()
    End Sub

    Protected Sub gvMappedSubjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvMappedSubjects.SelectedIndexChanged
        ' Dim CssID As Integer = FindCSSID(cboClass.Text, cboSection.Text, "")
        cboClass.Text = gvMappedSubjects.SelectedRow.Cells(1).Text
        LoadClassSection("", cboClass.Text, cboSection)
        cboSection.Text = gvMappedSubjects.SelectedRow.Cells(2).Text
        BindPermission()
        lblExamGroupID.Text = getExamGroupIDfromClass(cboClass.Text)
        LoadSubjectGroups(cboSubjectGroup, 0, lblExamGroupID.Text)
        ' loadMappedSubjects()
        ' cboSubjects.Text = GridView1.SelectedRow.Cells(4).Text
        Dim gvIndex As Integer = gvMappedSubjects.SelectedIndex

        lblCSSID.Text = FindCSSID(cboSchoolName.SelectedItem.Text, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, cboSubSection.SelectedItem.Text)
        '  Dim classID As Integer = FindClassID(cboClass.Text)
        ' Dim CssID As Integer = 
        chkClassTeacher.Checked = IsClassTeacher(lblCSSID.Text)
        gvSubjectPermission.Visible = True
        Dim sqlStr As String = ""

        Dim majorGroupID As Integer = 0
        sqlStr = "Select MajorGroupID from vw_ExamSubjectMapping where Cssid=" & lblCSSID.Text & " AND SubjectID=" & gvMappedSubjects.DataKeys(gvIndex).Value
        Try
            majorGroupID = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try
        sqlStr = "SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_ExamSubjectMapping] Where Cssid=" & lblCSSID.Text
        If majorGroupID > 0 Then
            cboSubjectGroup.ClearSelection()
            cboSubjectGroup.Items.FindByValue(majorGroupID).Selected = True
            sqlStr &= " AND majorGroupID=" & cboSubjectGroup.SelectedItem.Value
        End If

        ' SqlDataSource2.SelectCommand = "SELECT ClassName,SecName,[SubjectName], [IsClassTeacher], [IsPermissionApplicable],SubjectID FROM [vw_UserSubjectPermission] Where Cssid=" & lblCSSID.Text & " AND EmpID='" & lblEmpID.Text & "'"
        SqlDataSource2.SelectCommand = sqlStr
        gvSubjectPermission.DataBind()
        gvMappedSubjects.SelectedIndex = -1
    End Sub

    Protected Sub gvSubjectPermission_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSubjectPermission.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim cbTeaches As CheckBox = DirectCast(e.Row.FindControl("cbTeaches"), CheckBox)
            Dim cbPermission As CheckBox = DirectCast(e.Row.FindControl("cbPermission"), CheckBox)
            'Dim classID As Integer = FindClassID(cboClass.Text)
            ' Dim CssID As Integer = FindCSSID(cboClass.Text, cboSection.Text, "")
            'Dim secID As Integer = FindSecID(cboSection.Text, cboClass.Text)
            Dim SubjectID As Integer = gvSubjectPermission.DataKeys(e.Row.RowIndex).Value
            Try
                cbPermission.Checked = IsSubjectPermissionApplicable(lblEmpID.Text, lblCSSID.Text, SubjectID)
                cbTeaches.Checked = IsSubjectApplicable(lblEmpID.Text, lblCSSID.Text, SubjectID)
            Catch ex As Exception

            End Try

        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim sqlStr As String = ""
        ' Dim UserID As Integer = FindUserID(cbologinID.Text)
        '  Dim CssID As Integer = FindCSSID(cboClass.Text, cboSection.Text, "")
        Dim SubjectID As Integer = 0
        Dim IsClassTeacher As Integer = 0
        Dim ASID As Integer = 0
        Try
            ASID = Request.Cookies("ASID").Value
        Catch ex As Exception

        End Try
        If chkClassTeacher.Checked = True Then
            IsClassTeacher = 1
        Else
            IsClassTeacher = 0
        End If
        Dim IsPermissionApplicable As Integer = 0, IsTeaches As Integer = 0
        sqlStr = "Delete from usersubjectpermissions Where EmpID=" & lblEmpID.Text & " AND ASID=" & ASID & " AND CSSID=" & lblCSSID.Text & " And SchoolID=" & lblSchoolID.Text & " AND SubjectID in (Select [SubjectID] FROM [vw_ExamSubjectMapping] Where Cssid=" & lblCSSID.Text
        If cboSubjectGroup.Text <> "" Then
            sqlStr &= " AND MajorGroupID=" & cboSubjectGroup.SelectedItem.Value
        End If
        sqlStr &= ")"
        ExecuteQuery_Update(sqlStr)
       
        For Each row As GridViewRow In gvSubjectPermission.Rows
            SubjectID = gvSubjectPermission.DataKeys(row.RowIndex).Value
            Dim cbTeaches As CheckBox = DirectCast(row.FindControl("cbTeaches"), CheckBox)
            Dim cbPermission As CheckBox = DirectCast(row.FindControl("cbPermission"), CheckBox)
            If cbPermission.Checked = True Then
                IsPermissionApplicable = 1
            Else
                IsPermissionApplicable = 0
            End If
            If cbTeaches.Checked = True Then
                IsTeaches = 1
            Else
                IsTeaches = 0
            End If
            If IsTeaches = 0 And IsPermissionApplicable = 0 Then Continue For
            sqlStr = "Insert into UserSubjectPermissions(EmpID,CSSID,SubjectID,IsTeaches,IsClassTeacher,IsPermissionApplicable,ASID,SchoolID) Values(" & _
                lblEmpID.Text & "," & lblCSSID.Text & "," & SubjectID & "," & IsTeaches & "," & IsClassTeacher & "," & IsPermissionApplicable & "," & ASID & "," & lblSchoolID.Text & ")"
            ExecuteQuery_Update(sqlStr)
        Next

        gvSubjectPermission.Visible = False
        BindPermission()
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
        BindPermission()
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
            Try
                cboSchoolName.Text = myReader("SchoolName").ToString()
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
        If lblEmpID.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Employee Code does not exist!!!');", True)
            txtEmpCode.Focus()
            Exit Sub
        End If
        BindPermission()
    End Sub

    Protected Sub cboSubSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubSection.SelectedIndexChanged
        gvSubjectPermission.Visible = True
        lblCSSID.Text = FindCSSID(cboSchoolName.SelectedItem.Text, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, cboSubSection.SelectedItem.Text)
        SqlDataSource2.SelectCommand = "SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_ExamSubjectMapping] Where Cssid=" & lblCSSID.Text
        gvSubjectPermission.DataBind()
    End Sub

    Protected Sub btnEmpCode_Click(sender As Object, e As EventArgs) Handles btnEmpCode.Click
        ShowInfo(txtEmpCode.Text)
    End Sub

    Protected Sub cboSubjectGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubjectGroup.SelectedIndexChanged
        gvSubjectPermission.Visible = True
        lblCSSID.Text = FindCSSID(cboSchoolName.SelectedItem.Text, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, "")
        chkClassTeacher.Checked = IsClassTeacher(lblCSSID.Text)
        'lblCSSID.Text = FindCSSID(cboSchoolName.SelectedItem.Text, cboClass.SelectedItem.Text, cboSection.SelectedItem.Text, cboSubSection.SelectedItem.Text)
        Dim sqlStr As String = "SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_ExamSubjectMapping] Where Cssid=" & lblCSSID.Text
        If cboSubjectGroup.SelectedItem.Text = "" Then
        Else
            sqlStr &= " AND majorGroupID=" & cboSubjectGroup.SelectedItem.Value
        End If
        SqlDataSource2.SelectCommand = sqlStr
        gvSubjectPermission.DataBind()
    End Sub

    Private Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged

        lblSchoolID.Text = FindMasterID(71, cboSchoolName.Text)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        BindPermission()
    End Sub
End Class