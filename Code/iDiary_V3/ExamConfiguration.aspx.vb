Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Public Class ExamConfiguration
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("~/Logout.aspx")
            End If

        End Try

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If cboActivityGroup.Text = "" Then
            lblStatus.Text = "Invalid Subject group"
            cboActivityGroup.Focus()
            Exit Sub
        End If
        If cboHealthGroup.Text = "" Then
            lblStatus.Text = "Invalid Subject group"
            cboHealthGroup.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""

        sqlStr = "Update ExamParams Set ActivitySubjectGroupID=" & cboActivityGroup.SelectedItem.Value & ", HealthSubjectGroupID=" & cboHealthGroup.SelectedItem.Value & ", isMarksEntryAllowed=" & cboMarksEntryAllowed.SelectedItem.Value & ",isProcessingAllowed=" & cboProcessingAllowed.SelectedItem.Value & " , isPermissionApplicable=" & cboMarksEntryPermApplicable.SelectedItem.Value
        ExecuteQuery_Update(sqlStr)
        lblStatus.Text = "Information Updated Successfully..."
        InitControls()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Fee-1") = False Then
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub InitControls()

        LoadSubjectGroups(cboActivityGroup, 0)
        LoadSubjectGroups(cboHealthGroup, 0)

        Dim sqlStr As String = ""
        sqlStr = "SELECT [ActivitySubjectGroupID],[HealthSubjectGroupID],[isMarksEntryAllowed],[isProcessingAllowed],[isPermissionApplicable] FROM [ExamParams]"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                cboActivityGroup.ClearSelection()
                cboActivityGroup.Items.FindByValue(myReader("ActivitySubjectGroupID")).Selected = True
            Catch ex As Exception

            End Try
            Try
                cboHealthGroup.ClearSelection()
                cboHealthGroup.Items.FindByValue(myReader("HealthSubjectGroupID")).Selected = True
            Catch ex As Exception

            End Try
            Try
                cboMarksEntryAllowed.ClearSelection()
                cboMarksEntryAllowed.Items.FindByValue(myReader("isMarksEntryAllowed")).Selected = True
            Catch ex As Exception

            End Try
            Try
                cboProcessingAllowed.ClearSelection()
                cboProcessingAllowed.Items.FindByValue(myReader("isProcessingAllowed")).Selected = True
            Catch ex As Exception

            End Try
            Try
                cboMarksEntryPermApplicable.ClearSelection()
                cboMarksEntryPermApplicable.Items.FindByValue(myReader("isPermissionApplicable")).Selected = True
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
        cboActivityGroup.Focus()
    End Sub

End Class