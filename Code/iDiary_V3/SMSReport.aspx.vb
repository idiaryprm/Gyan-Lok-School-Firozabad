Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class SMSReport
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("SMS") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        If txtDateFrom.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Date From ..');", True)
            txtDateFrom.Focus()
            Return
        End If
        If txtDateTo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Date To ..');", True)
            txtDateTo.Focus()
            Return
        End If
        Dim StartDate As Date = New Date(txtDateFrom.Text.Substring(6, 4), txtDateFrom.Text.Substring(3, 2), txtDateFrom.Text.Substring(0, 2))
        Dim EndDate As Date = New Date(txtDateTo.Text.Substring(6, 4), txtDateTo.Text.Substring(3, 2), txtDateTo.Text.Substring(0, 2))
        If StartDate > EndDate Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('To Date can not less than From Date ..');", True)

            txtDateTo.Focus()
            Return
        End If
        'If DDLTeacherName.Text = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Teacher ..');", True)
        '    DDLTeacherName.Focus()
        '    Return
        'End If
        FillGrid()
        If DBGrid.Rows.Count > 0 Then
            lblDelivered.Text = "Total Deliverd: " & DeliveredCount(1)
            lblUnDelivered.Text = "Total Un-Deliverd: " & DeliveredCount(2)
        Else
            lblDelivered.Text = ""
            lblUnDelivered.Text = ""
        End If
        Clear()
    End Sub
    Public Function DeliveredCount(type As Integer) As Integer
        'type=1 for delivered and 2 for undelivered

       
        
       

        

        Dim sqlStr As String = "SELECT Count(*) FROM vw_SMSDLR"
        sqlStr += " where  SMSDate between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "'"
        If type = 1 Then
            sqlStr += " and DLRStatusCode='1701'"
        Else
            sqlStr += " and DLRStatusCode<>'1701'"
        End If

        
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)

        
        
        
        Return rv
    End Function
    Private Sub Clear()
        'DDLTeacherName.Text = ""
        'txtDateFrom.Text = ""
        'txtDateTo.Text = ""
    End Sub

    Private Sub FillGrid()
        Dim sql As String = "SELECT [SMSDate], [MobileNo], [Message], [DLRStatusCode], [Detail] FROM vw_SMSDLR"
        sql += " where  SMSDate between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "' "
        If cboStatus.Text = "Delivered" Then
            sql += " and DLRStatusCode='1701'"
        ElseIf cboStatus.Text = "UnDelivered" Then
            sql += " and DLRStatusCode<>'1701'"
        ElseIf cboStatus.Text = "Missed" Then
            sql += " and DLRStatusCode='1026'"
        End If
        sql += " order by DLRID"
        'If DDLTeacherName.Text = "All" Then
        '    sql += " where TNDate between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "' order by TNDate desc"
        'Else

        'End If


        SqlDataSource1.SelectCommand = sql
        DBGrid.DataSource = SqlDataSource1
        DBGrid.DataBind()

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'FillTeacher(DDLTeacherName, "")
            'DDLTeacherName.Items.Add("All")
            txtDateFrom.Focus()
        End If
    End Sub
    'Public Sub FillTeacher(ByVal cmb As DropDownList, ByVal type As String)
    '    Dim sql As String = ""
    '    sql = "Select EmpID, EmpName From EmployeeMaster order by EmpName"
    '    FillComboBox(sql, "EmpName", "EmpID", cmb, "", Trim(type))
    'End Sub
  
End Class