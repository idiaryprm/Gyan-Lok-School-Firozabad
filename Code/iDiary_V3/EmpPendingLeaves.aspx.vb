Public Class PendingLeaves
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
        SqlDataSource1.SelectCommand = "SELECT [EmpID], [EmpCode], [EmpName], [DesgName], [DeptName], [AttDate],[Att] FROM [vw_Employee_Attendance] Where Att=0 AND LeaveID is null and EmpASID=" & Request.Cookies("EmpASID").Value
        GridView1.DataBind()
    End Sub

    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        SqlDataSource1.SelectCommand = "SELECT [EmpID], [EmpCode], [EmpName], [DesgName], [DeptName], [AttDate],[Att] FROM [vw_Employee_Attendance] Where Att=0 AND LeaveID is null and EmpASID=" & Request.Cookies("EmpASID").Value
        GridView1.DataBind()
    End Sub

    'Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim myVal As String = e.Row.Cells(5).Text
    '        If myVal = "0" Then
    '            e.Row.Cells(5).Text = "AB"
    '            'absent = absent + 1
    '        ElseIf myVal = "0.5" Then
    '            e.Row.Cells(5).Text = "Half Day"
    '            'present = present + 1
    '        End If
    '    End If
    'End Sub
End Class