Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class MarkLeaves
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = True Or Request.Cookies("UType").Value.ToString.Contains("Payroll-1") = True Then
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        'LoadMasterInfo(33, cboEmpSession)
        LoadMasterInfo(30, cboEmpCat)
        cboEmpCat.Items.Add("")
        cboEmpCat.Text = ""
        cboEmpName.Items.Clear()
        txtEmpID.Text = ""
        txtEmpCode.Text = ""
        txtDOJ.Text = ""
        txtDeptName.Text = ""
        txtDesgName.Text = ""
        LoadMasterInfo(32, cboLeaveType)
        txtBalanceLeaves.Text = ""
        txtMobile.Text = ""
        txtFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        txtTo.Text = Now.Date.ToString("dd/MM/yyyy")
        txtEmpCode.Focus()
    End Sub

    Protected Sub cboLeaveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLeaveType.SelectedIndexChanged

        'If cboEmpSession.Text = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select Session');", True)
        '    cboEmpSession.Focus()
        '    Exit Sub
        'End If
        If cboEmpName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select Employee');", True)
            cboEmpName.Focus()
            Exit Sub
        End If

        txtBalanceLeaves.Text = ShowLeaveBalance(txtEmpID.Text, Request.Cookies("EmpASID").Value, FindMasterID(32, cboLeaveType.Text))
        cboLeaveType.Focus()
    End Sub

    Protected Sub cboEmpCat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmpCat.SelectedIndexChanged
        LoadEmployees(2, cboEmpCat.Text, cboEmpName)
        cboEmpCat.Focus()
    End Sub

    Protected Sub cboEmpName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmpName.SelectedIndexChanged
        txtEmpCode.Text = ""
       
       
       

        Dim sqlstr As String = ""
        

        sqlstr = "Select EmpID, EmpCode, EmpName, DOJ, DeptName, DesgName,EmpCatName,Mob From vw_Employees Where EmpName='" & cboEmpName.Text & "'"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtEmpID.Text = myReader("EmpID")
            txtEmpCode.Text = myReader("EmpCode")
            txtDeptName.Text = myReader("DeptName")
            txtDesgName.Text = myReader("DesgName")
            txtDOJ.Text = CDate(myReader("DOJ")).Day & "/" & CDate(myReader("DOJ")).Month & "/" & CDate(myReader("DOJ")).Year
            txtMobile.Text = myReader("Mob")
        End While
        myReader.Close()
        
        
        GridView2.DataBind()
        GridView3.DataBind()
        cboEmpName.Focus()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'If cboEmpSession.Text = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select Session');", True)
        '    cboEmpSession.Focus()
        '    Exit Sub
        'End If
        If cboEmpCat.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Employee Category');", True)
            cboEmpCat.Focus()
            Exit Sub
        End If
        If cboEmpName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select Employee');", True)
            cboEmpName.Focus()
            Exit Sub
        End If
        If cboLeaveType.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Leave Type');", True)
            cboLeaveType.Focus()
            Exit Sub
        End If
        If txtFrom.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid From Date');", True)
            txtFrom.Focus()
            Exit Sub
        End If
        If txtTo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid To Date');", True)
            txtTo.Focus()
            Exit Sub
        End If
        Dim fromDate As Date = txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2)
        Dim toDate As Date = txtTo.Text.Substring(6, 4) & "/" & txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2)

        If fromDate > toDate Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('To Date can not be less than From Date');", True)
            txtFrom.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = "", mydate As Date = Nothing
        Dim myCount As Integer = 0


        

        While mydate <> toDate
            mydate = fromDate.AddDays(myCount)

            sqlStr = "Update EmployeeAttendance Set LeaveID=" & FindMasterID(32, cboLeaveType.Text) & _
                " Where EmpID=" & txtEmpID.Text & " AND " & _
                "AttDate='" & CDate(mydate).Year & "/" & CDate(mydate).Month & "/" & CDate(mydate).Day & "'"
            ExecuteQuery_Update(sqlStr)
            myCount += 1
        End While

        'Insert Summary Here

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Leave saved successfully...');", True)

        GridView2.DataBind()
        GridView3.DataBind()

    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        txtEmpID.Text = ""

        Dim sqlstr As String = ""


        sqlstr = "Select EmpID, EmpCode, EmpName, DOJ, DeptName, DesgName,EmpCatName,Mob From vw_Employees Where EmpCode='" & txtEmpCode.Text & "'"
     
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            cboEmpCat.Text = myReader("EmpCatName")
            LoadEmployees(2, cboEmpCat.Text, cboEmpName)
            cboEmpName.Text = myReader("EmpName")
            txtEmpID.Text = myReader("EmpID")
            txtEmpCode.Text = myReader("EmpCode")
            txtDeptName.Text = myReader("DeptName")
            txtDesgName.Text = myReader("DesgName")
            txtDOJ.Text = CDate(myReader("DOJ")).Day & "/" & CDate(myReader("DOJ")).Month & "/" & CDate(myReader("DOJ")).Year
            txtMobile.Text = myReader("Mob")
        End While
        myReader.Close()
        SqlDataSource1.SelectCommand = "SELECT  [AttDate] FROM [vw_Employee_Attendance] Where Att=0 AND LeaveID is null AND EmpCode='" & txtEmpCode.Text & "' and EmpASID=" & Request.Cookies("EmpASID").Value
        GridView2.DataBind()
        SqlDataSource2.SelectCommand = "SELECT [AttDate], [LeaveName], [Remark] FROM [vw_Employee_Attendance] Where EmpCode='" & txtEmpCode.Text & "' and EmpASID=" & Request.Cookies("EmpASID").Value & " and LeaveID>0 Order By AttDate DESC"
        GridView3.DataBind()
        cboEmpName.Focus()
        If txtEmpID.Text = "" Then
            InitControls()
            lblStatus.Text = "Emp Code does not Exist..."
        Else
            lblStatus.Text = ""
        End If
    End Sub
End Class