Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class ExamGroupMaster
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""
        If Trim(txtName.Text) = "" Then
            FinalMessage = "Please Enter Group Name..."
            txtName.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
            Exit Sub
        End If
        If IsNumeric(txtDisplayOrder.Text) = False Then
            FinalMessage = "Invalid Display Order..."
            txtDisplayOrder.Focus()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
            Exit Sub
        End If
        If Val(txtID.Text) = 0 Then
            If CheckDoubleEntry(101, txtName.Text) <> 0 Then
                FinalMessage = "Group Name Already Exists..."
                txtName.Focus()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
                Exit Sub
            End If
            
        End If
        Dim SchoolID As Integer = 0

        Try
            SchoolID = FindMasterID(71, cboSchoolName.SelectedItem.Text)
        Catch ex As Exception

        End Try
        If Val(txtID.Text) = 0 Then    'Insert Command
            sqlStr = "Insert into ExamGroups(ExamGroupName,SchoolID,DisplayOrder,createdBy) Values('" & txtName.Text & "','" & SchoolID & "','" & Val(txtDisplayOrder.Text) & "','" & Request.Cookies("USerID").Value & "')"
            FinalMessage = "New Exam Group added successfully..."
        Else    'Update Command
            sqlStr = "Update ExamGroups Set ExamGroupName='" & txtName.Text & "',DisplayOrder=" & Val(txtDisplayOrder.Text) & ",SchoolID=" & SchoolID & " Where examGroupID=" & Val(txtID.Text)
            FinalMessage = "Exam Group updated successfully..."
        End If


        ExecuteQuery_Update(sqlStr)

        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()

        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(101, lstMaster, cboSchoolName.SelectedItem.Text)
        txtID.Text = 0
        txtName.Text = ""
        txtDisplayOrder.Text = lstMaster.Items.Count + 1
        txtName.Focus()
        btnRemove.Visible = False
    End Sub

    Protected Sub lstMaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMaster.SelectedIndexChanged

        Dim sqlStr As String = "Select * From vw_ExamGroups Where ExamGroupName='" & lstMaster.Text & "' AND SchoolName='" & cboSchoolName.SelectedItem.Text & "'"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("examGroupID")
            txtName.Text = myReader("ExamGroupName")
            Try
                txtDisplayOrder.Text = myReader("DisplayOrder")
            Catch ex As Exception
                txtDisplayOrder.Text = 0
            End Try

        End While
        myReader.Close()

    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = ""
        Dim FinalMessage = ""

        If Val(txtID.Text) = 0 Then    'Error
            FinalMessage = "Select a Exam Group to remove..."
        Else
            sqlStr = "Delete ExamGroup Where examGroupID=" & Val(txtID.Text) & "  AND SchoolName='" & cboSchoolName.SelectedItem.Text & "'"
        End If

        'ExecuteQuery_Update(sqlStr)
        'FinalMessage = "Exam Group removed successfully..."
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & FinalMessage & "');", True)


    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub
   
    Private Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(101, lstMaster, cboSchoolName.SelectedItem.Text)
    End Sub
End Class
