Imports iDiary_V3.iDiary.CLS_idiary

Public Class NewsPaperReport

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        txtDateFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDateTo.Text = Now.Date.ToString("dd/MM/yyyy")
        gvAttendance.Visible = False
    End Sub
    Dim absent As Integer = 0, present As Integer = 0
    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAttendance.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim myVal As String = e.Row.Cells(2).Text
            If myVal = "0" Then
                e.Row.Cells(2).Text = "Absent"
                absent = absent + 1
            ElseIf myVal = "1" Then
                e.Row.Cells(2).Text = "Present"
                present = present + 1
            End If
        End If
    End Sub

    Protected Sub btnFind_Click1(sender As Object, e As EventArgs) Handles btnFind.Click
        If txtDateFrom.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Attendance From Date...');", True)
            txtDateFrom.Focus()
            Exit Sub
        End If
        If txtDateTo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Attendance To Date...');", True)
            txtDateTo.Focus()
            Exit Sub
        End If
        Dim Todate As Date = txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2)
        Dim Fromdate As Date = txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2)

        If Todate < Fromdate Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date Selection...');", True)
            txtDateFrom.Focus()
            Exit Sub
        End If
        'SQL Query
        Dim sqlStr As String = ""
        sqlStr &= "Select * from vwNewsPaper Where DateIn Between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "'"
        sqlStr &= " Order By DateIn,NewsPaperName"
        gvAttendance.Visible = True
        SqlDataSourceAttendance.SelectCommand = sqlStr
        gvAttendance.DataBind()
        lblAbsent.Text = "Total Absent : " & absent
        lblPresent.Text = "Total Present : " & present
    End Sub
End Class