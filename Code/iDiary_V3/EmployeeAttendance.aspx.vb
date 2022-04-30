Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class EmployeeAttendance
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
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        'LoadMasterInfo(33, cboSession)
        txtAttDate.Text = Now.Date.ToString("dd/MM/yyyy")
        LoadMasterInfo(30, cboEmpCat)
        LoadMasterInfo(29, cboStatus)
        GridView1.DataBind()

        lblStatus.Text = ""
        txtAttDate.Focus()
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        ShowAttendanceData(True)
    End Sub

    Private Sub ShowAttendanceData(ByVal MessageStatus As Boolean)
        GridView1.Visible = True
        GridView1.DataBind()

        Dim i As Integer = 0, ExistCount As Integer = 0
        Dim sqlStr As String = ""

        sqlStr = "Select Count(*) From vw_Employee_Attendance Where EmpCatName='" & cboEmpCat.Text & "' and StatusName='" & cboStatus.Text & "' and AttDate='" & txtAttDate.Text.Substring(6, 4) & "/" & txtAttDate.Text.Substring(3, 2) & "/" & txtAttDate.Text.Substring(0, 2) & "'"

       
        
       

        

        
        
        ExistCount = ExecuteQuery_ExecuteScalar(SqlStr)

        If ExistCount > 0 Then
            If MessageStatus = True Then Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Attendance Already Marked');", True)
            lblStatus.Text = "Attendance Already Marked"
        Else
            lblStatus.Text = ""
        End If

        For i = 0 To GridView1.Rows.Count - 1

            'Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            Dim ddlAtt As DropDownList = DirectCast(GridView1.Rows(i).FindControl("ddlAtt"), DropDownList)

            sqlStr = "Select Att,IsLate From EmployeeAttendance Where " & _
                "EmpID=" & GridView1.Rows(i).Cells(1).Text & " AND " & _
                "AttDate='" & txtAttDate.Text.Substring(6, 4) & "/" & txtAttDate.Text.Substring(3, 2) & "/" & txtAttDate.Text.Substring(0, 2) & "'"
            
            
            Dim AttStatus As Double = 0
            Dim IsLate As Integer = 0
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    AttStatus = myReader(0)
                Catch ex As Exception

                End Try
                Try
                    IsLate = myReader(1)
                Catch ex As Exception

                End Try
            End While
            myReader.Close()

            If ExistCount > 0 And AttStatus = 1 And IsLate = 0 Then
                GridView1.Rows(i).BackColor = Drawing.ColorTranslator.FromHtml("#C8FF5C")
                ddlAtt.Text = "Pr"
                'chk.Checked = True
            ElseIf ExistCount > 0 And AttStatus = 1 And IsLate = 1 Then
                GridView1.Rows(i).BackColor = Drawing.ColorTranslator.FromHtml(RGB(192, 251, 171))
                'chk.Checked = False
                ddlAtt.Text = "Lt"
            ElseIf ExistCount > 0 And AttStatus = 0 Then
                GridView1.Rows(i).BackColor = Drawing.ColorTranslator.FromHtml("#FFFF50")
                'chk.Checked = False
                ddlAtt.Text = "Ab"
            ElseIf ExistCount > 0 And AttStatus = 0.5 Then
                GridView1.Rows(i).BackColor = Drawing.ColorTranslator.FromHtml("#F5B658")
                'chk.Checked = False
                ddlAtt.Text = "Hd"
            ElseIf ExistCount <= 0 Then
                GridView1.Rows(i).BackColor = Drawing.Color.Aquamarine
                'chk.Checked = True
            End If

        Next

        
        

    End Sub
    Private Function IsAttendanceExist(ByVal EmpID As Integer) As Integer
        Dim ExistCount As Integer = 0
        Dim sqlStr As String = ""

        sqlStr = "Select Count(*) From EmployeeAttendance Where EmpID=" & EmpID & " and AttDate='" & txtAttDate.Text.Substring(6, 4) & "/" & txtAttDate.Text.Substring(3, 2) & "/" & txtAttDate.Text.Substring(0, 2) & "'"

       
        
       

        

        
        
        ExistCount = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        Return ExistCount
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'If cboSession.Text = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Session');", True)
        '    cboSession.Focus()
        '    Exit Sub
        'End If
        If txtAttDate.Text.Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date');", True)
            lblStatus.Text = "Invalid Date..."
            txtAttDate.Focus()
            Exit Sub
        End If
        If cboEmpCat.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Employee Category...');", True)
            lblStatus.Text = "Invalid Employee Category..."
            cboEmpCat.Focus()
            Exit Sub
        End If
        If cboStatus.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Status');", True)
            lblStatus.Text = "Invalid Status..."
            cboStatus.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
        'Dim EmpSessionID As Integer = FindMasterID(33, cboSession.Text)

        Dim sqlStr As String = ""
        Dim i As Integer = 0

       
        
       

        

        For i = 0 To GridView1.Rows.Count - 1

            Dim Att As Double = 0
            Dim IsLate As Integer = 0
            Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            Dim ddlAtt As DropDownList = DirectCast(GridView1.Rows(i).FindControl("ddlAtt"), DropDownList)

            '    If chk.Checked = True Then Att = 1
            If ddlAtt.Text = "Ab" Then
                Att = 0
            ElseIf ddlAtt.Text = "Pr" Then
                Att = 1
                IsLate = 0
            ElseIf ddlAtt.Text = "Hd" Then
                Att = 0.5
            End If
            If ddlAtt.Text = "Lt" Then
                Att = 1
                IsLate = 1
            End If
            'If GridView1.Rows(i).BackColor = Drawing.Color.Aquamarine Then 'Already Present
            If IsAttendanceExist(GridView1.Rows(i).Cells(1).Text) = 0 Then
                sqlStr = "Insert into EmployeeAttendance (EmpASID,EmpID, AttDate,Att,IsLate) Values (" & _
                                       Request.Cookies("EmpASID").Value & "," & GridView1.Rows(i).Cells(1).Text & "," & _
                   "'" & txtAttDate.Text.Substring(6, 4) & "/" & txtAttDate.Text.Substring(3, 2) & "/" & txtAttDate.Text.Substring(0, 2) & "'," & _
                   Att & "," & IsLate & ")"
                'EmpSessionID & "," & _
            Else
                sqlStr = "Update EmployeeAttendance Set EmpASID=" & Request.Cookies("EmpASID").Value & ", Att=" & Att & _
                    ",IsLate=" & IsLate & " Where EmpID=" & GridView1.Rows(i).Cells(1).Text & " AND " & _
                    "AttDate='" & txtAttDate.Text.Substring(6, 4) & "/" & txtAttDate.Text.Substring(3, 2) & "/" & txtAttDate.Text.Substring(0, 2) & "'"
            End If

            
            
            ExecuteQuery_Update(SqlStr)

        Next

        System.Threading.Thread.Sleep(500)

        
        

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Attendance saved successfully...');", True)
        lblStatus.Text = "Attendance saved successfully..."
        ShowAttendanceData(False)

    End Sub
End Class