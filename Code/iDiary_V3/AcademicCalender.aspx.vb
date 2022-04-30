Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Partial Class Admin_AcademicCalender
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        txtDate.Text = Now.Date
        txtDateTo.Text = Now.Date
        txtDesc.Text = ""
        lblStatus.Text = ""
        txtDate.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtDate.Text.Length <= 0 Then
            lblStatus.Text = "Please Provide Event Date..."
            txtDate.Focus()
            Exit Sub
        End If
        If txtDateTo.Text.Length <= 0 Then
            lblStatus.Text = "Please Provide Date To..."
            txtDateTo.Focus()
            Exit Sub
        End If
        If txtDesc.Text.Length <= 0 Then
            lblStatus.Text = "Please Provide Description..."
            txtDesc.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
        If txtID.Text = "" Then
            sqlStr = "Insert into AcademicCalender ( ACDate, ACDateTo, ACDetails) Values (" & _
        "'" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "'," & _
        "'" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "'," & _
        "'" & txtDesc.Text & "'" & _
        ")"
        Else
            sqlStr = "Update AcademicCalender Set ACDate='" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & _
                "', ACDateTo='" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & _
                "', ACDetails='" & txtDesc.Text & "' where ACID=" & txtID.Text

        End If
        
        ExecuteQuery_Update(sqlStr)

        Dim myDate As String = txtDate.Text
        Dim myDesc As String = txtDesc.Text

        InitControls()
        lblStatus.Text = "Event (" & myDesc & ") Dated: " & myDate & " successfully added..."
        gvAcademicCal.DataBind()
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = ""
        sqlStr = "Delete From AcademicCalender Where ACID=" & gvAcademicCal.SelectedRow.Cells(1).Text
        ExecuteQuery_Update(sqlStr)
        gvAcademicCal.DataBind()
    End Sub

    Protected Sub gvAcademicCal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAcademicCal.SelectedIndexChanged
        txtID.Text = gvAcademicCal.SelectedRow.Cells(1).Text
        txtDate.Text = Trim(gvAcademicCal.SelectedRow.Cells(2).Text)
        txtDateTo.Text = Trim(gvAcademicCal.SelectedRow.Cells(3).Text)
        txtDesc.Text = gvAcademicCal.SelectedRow.Cells(4).Text
    End Sub
End Class
