Imports System.Data.SqlClient
Imports iDiary_V3.iDiary_Security.CLS_iDiary_Security
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Partial Class UserPermissions
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
        gvSubjectPermission.Visible = False
        chkClassTeacher.Checked = False
        LoadUsers(cbologinID)
        ' txtLoginID.Text = Request.QueryString("LoginID")
        '   ShowPermissionList()
        LoadMasterInfo(2, cboClass)

        cbologinID.Focus()
        chkClassTeacher.Checked = False
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.Text, cboSection)
        chkClassTeacher.Checked = False
        ' chkClassPermission.Checked = False
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
        Dim UserID As Integer = FindUserID(cbologinID.Text)
        SqlDataSource1.SelectCommand = "SELECT [ClassName], [SecName], [SubjectCode], [SubjectName] FROM [vw_UserSubjectPermission] WHERE ([UserID] ='" & UserID & "')"
        GridView1.DataBind()
    End Sub
    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        gvSubjectPermission.Visible = True
        Dim CssID As Integer = FindCSSID("", cboClass.Text, cboSection.Text, "")
        chkClassTeacher.Checked = IsClassTeacher(CssID)

        SqlDataSource2.SelectCommand = "SELECT EntryType as [IsClassTeacher],EntryType as [IsPermissionApplicable], [ClassName], [SecName], [SubjectName],[SubjectID] FROM [vw_SubjectMapping] Where Cssid=" & CssID
        gvSubjectPermission.DataBind()
        '  loadMappedSubjects()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ' Dim CssID As Integer = FindCSSID(cboClass.Text, cboSection.Text, "")
        cboClass.Text = GridView1.SelectedRow.Cells(1).Text
        LoadClassSection("", cboClass.Text, cboSection)
        cboSection.Text = GridView1.SelectedRow.Cells(2).Text
        ' loadMappedSubjects()
        ' cboSubjects.Text = GridView1.SelectedRow.Cells(4).Text
        GridView1.SelectedIndex = -1

        '  Dim classID As Integer = FindClassID(cboClass.Text)
        Dim CssID As Integer = FindCSSID("", cboClass.Text, cboSection.Text, "")
        chkClassTeacher.Checked = IsClassTeacher(CssID)
        gvSubjectPermission.Visible = True
        SqlDataSource2.SelectCommand = "SELECT ClassName,SecName,[SubjectName], [IsClassTeacher], [IsPermissionApplicable],SubjectID FROM [vw_UserSubjectPermission] Where Cssid=" & CssID & " AND UserID='" & lblUserID.Text & "'"
        gvSubjectPermission.DataBind()
        'Dim SubjectID As Integer = FindSubjectID(cboSubjects.Text)
        '  chkClassPermission.Checked = IsPermissionApplicable(classID, secID, SubjectID)
    End Sub

    
    Protected Sub cbologinID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbologinID.SelectedIndexChanged
        lblUserID.Text = FindUserID(cbologinID.Text)
        txtUName.Text = getEmployeeNamefromCode(cbologinID.Text)
        BindPermission()
    End Sub

    Protected Sub gvSubjectPermission_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSubjectPermission.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim cbTeaches As CheckBox = DirectCast(e.Row.FindControl("cbTeaches"), CheckBox)
            Dim cbPermission As CheckBox = DirectCast(e.Row.FindControl("cbPermission"), CheckBox)
            'Dim classID As Integer = FindClassID(cboClass.Text)
            Dim CssID As Integer = FindCSSID("", cboClass.Text, cboSection.Text, "")
            'Dim secID As Integer = FindSecID(cboSection.Text, cboClass.Text)
            Dim SubjectID As Integer = gvSubjectPermission.DataKeys(e.Row.RowIndex).Value
            Try
                cbPermission.Checked = IsSubjectPermissionApplicable(lblUserID.Text, CssID, SubjectID)
                cbTeaches.Checked = IsSubjectApplicable(lblUserID.Text, CssID, SubjectID)
            Catch ex As Exception

            End Try
           
        End If
    End Sub

    Protected Sub btnAddSubject0_Click(sender As Object, e As EventArgs) Handles btnAddSubject0.Click
        Dim sqlStr As String = ""
        Dim UserID As Integer = FindUserID(cbologinID.Text)
        Dim CssID As Integer = FindCSSID("", cboClass.Text, cboSection.Text, "")
        Dim SubjectID As Integer = 0
        Dim IsClassTeacher As Integer = 0
        If chkClassTeacher.Checked = True Then
            IsClassTeacher = 1
        Else
            IsClassTeacher = 0
        End If
        Dim IsPermissionApplicable As Integer = 0, IsTeaches As Integer = 0
        sqlStr = "Delete from usersubjectpermissions Where UserID=" & UserID & " AND CSSID=" & CssID
        ExecuteQuery_Update(sqlStr)
        For Each row As GridViewRow In gvSubjectPermission.Rows
            SubjectID = FindSubjectID(row.Cells(2).Text)
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
            sqlStr = "Insert into UserSubjectPermissions(UserID,CSSID,SubjectID,IsTeaches,IsClassTeacher,IsPermissionApplicable) Values(" & _
                UserID & "," & CssID & "," & SubjectID & "," & IsTeaches & "," & IsClassTeacher & "," & IsPermissionApplicable & ")"
            ExecuteQuery_Update(sqlStr)
        Next

        InitControls()
    End Sub

End Class