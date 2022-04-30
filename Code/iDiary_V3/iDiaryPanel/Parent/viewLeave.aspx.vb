Public Class viewLeave
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim myVal As String = e.Row.Cells(1).Text
            If myVal = "0" Then
                e.Row.Cells(1).Text = "Absent"
            ElseIf myVal = "1" Then
                e.Row.Cells(1).Text = "Present"
            End If
        End If
    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        SqlDataSourceLeave.SelectCommand = "SELECT [fromDate], [toDate], [Reason], [Message] FROM [StudentLeave] WHERE ([SID] = @SID) AND fromDate Between '" & txtFromDate.Text.Substring(6, 4) & "/" & txtFromDate.Text.Substring(3, 2) & "/" & txtFromDate.Text.Substring(0, 2) & "' And '" & txtToDate.Text.Substring(6, 4) & "/" & txtToDate.Text.Substring(3, 2) & "/" & txtToDate.Text.Substring(0, 2) & "'"
        GridView1.DataBind()
    End Sub

End Class