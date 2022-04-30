Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient


Public Class LibraryMembership
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            initcontrols()
        End If
    End Sub
    Private Sub initcontrols()
        rblMembership.SelectedIndex = 0
        gvEmployee.Visible = False
        gvStudent.Visible = False
        LoadMasterInfo(65, cboMembership)
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch.Click
        If rblMembership.SelectedIndex = 0 Then
            gvEmployee.Visible = False
            gvStudent.Visible = True

            SqlDataSourceStudent.SelectCommand = "SELECT [RegNo], [SName], [ClassName], [SecName] FROM [vw_Student] WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtName.Text & "%'"
            gvStudent.DataBind()

        Else
            gvStudent.Visible = False
            gvEmployee.Visible = True
            SqlDataSourceEmployee.SelectCommand = "SELECT  [EmpCode], [EmpName], [DeptName], [DesgName]  FROM [vw_Employees] WHERE EmpName Like '%" & txtName.Text & "%'"
            gvEmployee.DataBind()
        End If
    End Sub
    Private Function getMembership(type As Integer, name As String) As String
        ' type 0--student
        ' type 1 --Employee
        Dim rv As String = ""
       
       
       

        Dim sqlStr As String = ""
        
        If type = 0 Then
            sqlStr = "Select top(1)  MemberShipName from vw_Student Where regno='" & txtRegCode.Text & "' AND ASID =" & Request.Cookies("ASID").Value
        Else
            sqlStr = "Select top(1)  MemberShipName from vw_Employees Where EmpCode='" & txtRegCode.Text & "' "
        End If

        
        

        Try
            rv = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception

        End Try

        
        

        Return rv
    End Function

    Protected Sub rblMembership_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblMembership.SelectedIndexChanged
        If rblMembership.SelectedIndex = 0 Then
            lblName.Text = "Student Name"
            lblregCode.Text = "Admn. No."
        Else
            lblName.Text = "Employee Name"
            lblregCode.Text = "Employee Code"
        End If
    End Sub

    Protected Sub gvStudent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStudent.SelectedIndexChanged
        txtRegCode.Text = gvStudent.SelectedRow.Cells(1).Text
        txtName.Text = gvStudent.SelectedRow.Cells(2).Text
        gvStudent.SelectedIndex = -1
        gvStudent.Visible = False
        cboMembership.Text = getMembership(0, txtName.Text)
    End Sub

    Protected Sub gvEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEmployee.SelectedIndexChanged
        txtRegCode.Text = gvEmployee.SelectedRow.Cells(1).Text
        txtName.Text = gvEmployee.SelectedRow.Cells(2).Text
        gvEmployee.SelectedIndex = -1
        gvEmployee.Visible = False
        cboMembership.Text = getMembership(1, txtName.Text)
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If Trim(txtName.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Name cant be left blank..');", True)
            txtName.Focus()
            Exit Sub
        End If
        If Trim(txtRegCode.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Code/ Admn. No. is required..');", True)
            txtRegCode.Focus()
            Exit Sub
        End If
       
       
       

        Dim sqlStr As String = ""
        Dim LibMebershipID As Integer = FindMasterID(65, cboMembership.Text)
        
        If rblMembership.SelectedIndex = 0 Then
            sqlStr = "Update Student Set LibraryMemID=" & LibMebershipID & " Where regno='" & txtRegCode.Text & "' AND ASID =" & Request.Cookies("ASID").Value
        Else
            sqlStr = "Update EmployeeMaster Set LibraryMemID=" & LibMebershipID & " Where EmpCode='" & txtRegCode.Text & "' "
        End If

        
        
        ExecuteQuery_Update(SqlStr)

        
        

    End Sub

   
End Class