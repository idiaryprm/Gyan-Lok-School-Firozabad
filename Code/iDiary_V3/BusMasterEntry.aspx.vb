Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class BusMasterEntry
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Try
        '    If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
        '        'Allow
        '    Else
        '        Response.Redirect("/./AccessDenied.aspx", False)
        '    End If
        'Catch ex As Exception
        '    response.redirect("~/Login.aspx")
        'End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""
        txtNumber.Text = ""
        txtCapacity.Text = ""
        LoadMasterInfo(45, lstMasters)
        lblStatus.Text = ""
        txtName.Focus()
        txtEmpCode.Text = ""
        txtDName.Text = ""
        lblEmpCode.Text = ""
        lblEmpName.Text = ""
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Wrong Input!"
            txtName.Focus()
            Exit Sub
        End If
        If Trim(txtNumber.Text) = "" Then
            lblStatus.Text = "Please Enter Bus Number!"
            txtNumber.Focus()
            Exit Sub
        End If
        If IsNumeric(txtCapacity.Text) = False Then
            lblStatus.Text = "Please Enter Bus Capacity!"
            txtCapacity.Focus()
            Exit Sub
        End If
        If lblEmpCode.Text.Length <= 0 Then
            lblStatus.Text = "Invalid Employee!"
            txtDName.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
        Dim EmpID As Integer = FindEmployeeIDfromCode(txtEmpCode.Text)

        Dim sqlStr As String = ""


        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into busMaster Values('" & txtName.Text & "','" & txtNumber.Text & "','" & txtCapacity.Text & "','" & EmpID & "')"
            ExecuteQuery_Update(sqlStr)
        Else
            'Update
            sqlStr = "Update busMaster Set busName='" & txtName.Text & "',busNumber='" & txtNumber.Text & "', busCapacity='" & txtCapacity.Text & "', EmpID='" & EmpID & "' Where busID=" & Val(txtID.Text)
            ExecuteQuery_Update(sqlStr)
        End If
        'Dim BusID As Integer = ExecuteQuery_ExecuteScalar("Select BusID from BusMaster where BusName='" & txtName.Text & "'")
        ' Insert Track Table
        'sqlStr = "Insert into busMaster Values('" & BusID & "','" & EmpID & "')"
        InitControls()
    End Sub
    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
        Dim EmpID As String = ""
       
       
       

        Dim sqlStr As String = "Select * From busMaster Where busName='" & lstMasters.Text & "'"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("busID")
            txtName.Text = myReader("busName")
            txtNumber.Text = myReader("busNumber")
            txtCapacity.Text = myReader("busCapacity")
            EmpID = myReader("EmpID")
        End While
        myReader.Close()
        
        
        txtEmpCode.Text = FindCodefromEmployeeID(EmpID)
        'txtDName.Text = FindEmployeeName(txtEmpCode.Text)
        lblEmpCode.Text = "  Emplyee Code - " & FindCodefromEmployeeID(EmpID)
        lblEmpName.Text = "-  " & FindEmployeeName(txtEmpCode.Text)
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From busMaster Where busID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        
        
        InitControls()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim EmpCode As String = GridView1.SelectedRow.Cells(1).Text
        txtEmpCode.Text = EmpCode
        lblEmpCode.Text = "  Emplyee Code - " & EmpCode
        txtDName.Text = ""
        lblEmpName.Text = "-  " & GridView1.SelectedRow.Cells(2).Text
        GridView1.Visible = False
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        SqlDataSource1.SelectCommand = "SELECT EmpCode, EmpName, DeptName, DesgName, Mob FROM vw_Employees WHERE  EmpName Like '%" & txtDName.Text & "%'"
        GridView1.DataBind()
        GridView1.Visible = True
    End Sub
End Class