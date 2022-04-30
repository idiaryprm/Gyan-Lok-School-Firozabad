Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class updateCreditLeaves
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblEmp.Text = ""
            txtEmpCode.Text = ""
            txtLeaveCount.Text = ""
        End If
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim sqlStr As String = "Select * From vw_EmployeeLeaves Where EmpCode='" & txtEmpCode.Text & "'"
        cboLeaveType.Items.Clear()
        cboLeaveType.Items.Add("")
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            cboLeaveType.Items.Add(myReader("LeaveName"))
            lblEmp.Text = "For Employee : " & myReader("EmpName")
            lblEmpID.Text = myReader("EmpID")
        End While
        myReader.Close()
    End Sub

    Protected Sub cboLeaveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLeaveType.SelectedIndexChanged

        Dim sqlStr As String = "Select MaxLimit From vw_EmployeeLeaves Where EmpCode='" & txtEmpCode.Text & "' and EmpASID='" & Request.Cookies("EmpASID").Value & "' ANd LeaveNAme='" & cboLeaveType.Text & "'"
     
        Try
            txtLeaveCount.Text = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception
            txtLeaveCount.Text = 0
        End Try

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cboLeaveType.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select Leave Type...');", True)
            cboLeaveType.Focus()
            Exit Sub
        End If

        If IsNumeric(txtLeaveCount.Text) Then
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Leave Count should be Numeric...');", True)
            txtLeaveCount.Focus()
            Exit Sub
        End If
        Dim LeaveID As Integer = FindMasterID(32, cboLeaveType.Text)
        Dim sqlStr As String = "Update EmployeeLeaves set LeaveCount='" & txtLeaveCount.Text & "' Where EmpID='" & lblEmpID.Text & "' and EmpASID=" & Request.Cookies("EmpASID").Value & " ANd LeaveID='" & LeaveID & "'"

        ExecuteQuery_Update(sqlStr)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Credits has been updated " & lblEmp.Text & "');", True)
        lblEmp.Text = ""
        txtEmpCode.Text = ""
        txtLeaveCount.Text = ""
        lblEmpID.Text = ""
    End Sub
End Class