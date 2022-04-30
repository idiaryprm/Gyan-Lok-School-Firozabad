Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class CreditLeaves
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
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Payroll-1") = False Then
            btnCredit.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        'LoadMasterInfo(33, cboDuration)
        LoadMasterInfo(64, cboEmpType)
        LoadMasterInfo(30, cboEmpCat)
        'cboEmpCat.Items.Add("")
        'cboEmpCat.Text = ""
        btnCredit.Visible = False
        GridView1.Visible = False
        cboEmpCat.Focus()
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        If cboEmpType.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Employee Type');", True)
            cboEmpType.Focus()
            Exit Sub
        End If
        Dim EmpSessionID As Integer = Request.Cookies("EmpASID").Value

        Dim sqlStr As String = "", TempSQL As String = ""
        Dim lstInsert As New ListBox
        Dim lstEmpID As New ListBox, lstEmpCode As New ListBox, lstEmpName As New ListBox
        Dim lstDeptName As New ListBox, lstDesgName As New ListBox
        Dim lstLeaveID As New ListBox, lstLeaveName As New ListBox
        Dim lstLeaveMax As New ListBox, lstLeaveCarry As New ListBox

       
        
       

        

        'Remove Previous Report Table
        sqlStr = "DROP TABLE rptCreditLeaves"
        
        
        Try
            ExecuteQuery_Update(SqlStr)
        Catch ex As Exception
            'Do Nothing
        End Try

        Dim empTypeID As Integer = FindMasterID(64, cboEmpType.Text)

        sqlStr = "CREATE TABLE rptCreditLeaves([Emp ID] int,[Employee Code] nvarchar(255),[Employee Name] nvarchar(255), [Designation] nvarchar(100), [Department] nvarchar(100));"
        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "Select * From LeaveMaster Where (ApplicableFor=0 or ApplicableFor=" & empTypeID & ") Order by LeaveName"
        
        
        Dim LeaveReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While LeaveReader.Read
            lstLeaveID.Items.Add(LeaveReader("LeaveID"))
            lstLeaveName.Items.Add(LeaveReader("LeaveName"))
            lstLeaveMax.Items.Add(LeaveReader("MaxLimit"))
            lstLeaveCarry.Items.Add(LeaveReader("CarryForward"))
        End While
        LeaveReader.Close()

        Dim i As Integer = 0, j As Integer = 0

        For i = 0 To lstLeaveName.Items.Count - 1
            sqlStr = "Alter Table rptCreditLeaves add [" & lstLeaveName.Items(i).Text & "] float;"
            
            
            ExecuteQuery_Update(SqlStr)
        Next

        sqlStr = "Select EmpID, EmpCode, EmpName, DesgName, DeptName From vw_Employees Where EmpTypeName='" & cboEmpType.Text & "' order by EmpName"
        
        
        Dim EmpReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While EmpReader.Read
            lstEmpID.Items.Add(EmpReader("EmpID"))
            lstEmpCode.Items.Add(EmpReader("EmpCode"))
            lstEmpName.Items.Add(EmpReader("EmpName"))
            lstDesgName.Items.Add(EmpReader("DesgName"))
            lstDeptName.Items.Add(EmpReader("DeptName"))
        End While
        EmpReader.Close()

        For i = 0 To lstEmpID.Items.Count - 1

            sqlStr = "Insert into rptCreditLeaves Values(" & _
            lstEmpID.Items(i).Text & "," & _
            "'" & lstEmpCode.Items(i).Text & "'," & _
            "'" & SQLFixup(lstEmpName.Items(i).Text) & "'," & _
            "'" & SQLFixup(lstDesgName.Items(i).Text) & "'," & _
            "'" & SQLFixup(lstDeptName.Items(i).Text) & "',"

            For j = 0 To lstLeaveID.Items.Count - 1
                sqlStr &= lstLeaveMax.Items(j).Text & ","
            Next
            sqlStr = sqlStr.Substring(0, sqlStr.Length - 1) & ")"

            
            
            ExecuteQuery_Update(SqlStr)

        Next

        
        

        If GridView1.Rows.Count > 0 Then
            'GridView1.Columns(6)
            GridView1.Visible = True
            btnCredit.Visible = True
        Else
            GridView1.Visible = False
            btnCredit.Visible = False
        End If

        GridView1.DataBind()
    End Sub

    Protected Sub btnCredit_Click(sender As Object, e As EventArgs) Handles btnCredit.Click
        'If cboDuration.Text = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Duration');", True)
        '    cboDuration.Focus()
        '    Exit Sub
        'End If

        Dim EmpSessionID As Integer = Request.Cookies("EmpASID").Value

        Dim sqlStr As String = ""
        Dim lstEmpID As New ListBox
        Dim lstLeaveID As New ListBox, lstLeaveMax As New ListBox

       
        
       

        

        sqlStr = "Select * From LeaveMaster Order by LeaveName"
        
        
        Dim LeaveReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While LeaveReader.Read
            lstLeaveID.Items.Add(LeaveReader("LeaveID"))
            lstLeaveMax.Items.Add(LeaveReader("MaxLimit"))
        End While
        LeaveReader.Close()

        Dim i As Integer = 0, j As Integer = 0

        sqlStr = "Select EmpID, EmpCode, EmpName, DesgName, DeptName From vw_Employees Where EmpTypeName='" & cboEmpType.Text & "'  order by EmpName"
        
        
        Dim EmpReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While EmpReader.Read
            lstEmpID.Items.Add(EmpReader("EmpID"))
        End While
        EmpReader.Close()

        For i = 0 To lstEmpID.Items.Count - 1

            For j = 0 To lstLeaveID.Items.Count - 1

                'To be used when saving the records
                sqlStr = "Select Count(*) From EmployeeLeaves Where EmpASID=" & EmpSessionID & " AND EmpID=" & lstEmpID.Items(i).Text & " AND LeaveID=" & lstLeaveID.Items(j).Text
                
                
                Dim rv As Integer = 0
                Try
                    rv = ExecuteQuery_ExecuteScalar(sqlStr)
                Catch ex As Exception
                    rv = 0
                End Try

                If rv > 0 Then Continue For

                sqlStr = "Insert into EmployeeLeaves Values(" & _
                    EmpSessionID & "," & _
                    lstEmpID.Items(i).Text & "," & _
                    lstLeaveID.Items(j).Text & "," & _
                    lstLeaveMax.Items(j).Text & ")"

                
                
                ExecuteQuery_Update(SqlStr)

            Next

        Next

        
        

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Leaves Credited Successfully...');", True)

        InitControls()

    End Sub
End Class