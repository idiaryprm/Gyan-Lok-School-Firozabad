Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class LeaveMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
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
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(32, lstMasters)
        LoadMasterInfo(64, cboEmpType)
        cboEmpType.Items.Add("ALL")
        txtID.Text = ""
        txtName.Text = ""
        txtMaxLimit.Text = ""
        chkCarryForward.Checked = False
        chkSalaryDeduct.Checked = False
        txtName.Focus()
        btnRemove.Visible = False

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Wrong Leave Name');", True)
            txtName.Focus()
            Exit Sub
        End If
        If IsNumeric(txtMaxLimit.Text) = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Wrong Max Limit');", True)
            txtName.Focus()
            Exit Sub
        End If
        If cboEmpType.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Employee Type');", True)
            cboEmpType.Focus()
            Exit Sub
        End If
        If Val(txtID.Text) = 0 Then
            If CheckLeave() = True Then
                txtName.Focus()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Leave Already Exists...');", True)
                Exit Sub
            End If
        End If
        Dim empTypeID As Integer = -1
        If cboEmpType.Text = "ALL" Then
            empTypeID = 0
        Else
            empTypeID = FindMasterID(64, cboEmpType.Text)
        End If
        Dim CarryForward As Integer = 0
        If chkCarryForward.Checked = True Then CarryForward = 1

        Dim SalaryDeduct As Integer = 0
        If chkSalaryDeduct.Checked = True Then SalaryDeduct = 1

       
       
       
        Dim sqlStr As String = ""
        

        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into LeaveMaster Values('" & txtName.Text & "'," & _
                Val(txtMaxLimit.Text) & "," & _
                CarryForward & "," & _
                 SalaryDeduct & "," & _
                empTypeID & ")"
        Else
            'Update
            sqlStr = "Update LeaveMaster Set " & _
                "LeaveName='" & txtName.Text & "'," & _
                "MaxLimit=" & Val(txtMaxLimit.Text) & "," & _
                "CarryForward=" & CarryForward & "," & _
                "SalaryDeduct=" & SalaryDeduct & "," & _
                  "ApplicableFor=" & empTypeID & " " & _
                "Where LeaveID = " & Val(txtID.Text)
        End If
        
        
        ExecuteQuery_Update(SqlStr)

        
        
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record Saved Successfully...');", True)
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
        ShowLeaveData()
    End Sub

    Private Sub ShowLeaveData()

       
       
       

        Dim sqlStr As String = "Select * From LeaveMaster Where LeaveName='" & lstMasters.Text & "'"
        
        
        
        Dim empTypeID As Integer = -1
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("LeaveID")
            txtName.Text = myReader("LeaveName")
            txtMaxLimit.Text = myReader("MaxLimit")
            chkCarryForward.Checked = myReader("CarryForward")
            chkSalaryDeduct.Checked = myReader("SalaryDeduct")
            Try
                empTypeID = myReader("ApplicableFor")
            Catch ex As Exception

            End Try

        End While
        myReader.Close()
        
        
        If empTypeID = 0 Then
            cboEmpType.Text = "ALL"
        Else
            cboEmpType.Text = FindMasterName(64, empTypeID)
        End If
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
       
       
       

        Dim sqlStr As String = "Delete From leaveMaster Where LeaveID=" & Val(txtID.Text)
        
        
        
        ExecuteQuery_Update(SqlStr)
        
        
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record Removed Successfully...');", True)
    End Sub
    Private Function CheckLeave()
       
       
       

        Dim sqlStr As String = "Select Count(*) From Status_Payroll Where StatusName='" & txtName.Text & "'"
        
        
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
        
        
        Return rv
    End Function
End Class