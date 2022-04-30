Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class FeeGroupMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 3
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""
        LoadMasterInfo(60, lstMasters)
        lblStatus.Text = ""
        txtDisplayOrder.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Plase Enter Fee Group Name!"
            txtName.Focus()
            Exit Sub
        End If
        If Trim(txtDisplayOrder.Text) = "" Or IsNumeric(txtDisplayOrder.Text) = False Then
            lblStatus.Text = "Plase Enter Valid Display Order!"
            txtDisplayOrder.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
       
        If txtID.Text = "" Then             'Insert
            sqlStr = "Insert into FeeGroupMaster Values('" & txtName.Text & "','" & Val(txtDisplayOrder.Text) & "')"
        Else                                'Update
            sqlStr = "Update FeeGroupMaster Set FeeGroupName='" & txtName.Text & "',DisplayOrder='" & txtDisplayOrder.Text & "' Where FGID=" & Val(txtID.Text)
        End If
        ExecuteQuery_Update(sqlStr)

        InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
        Dim sqlStr As String = "Select * From FeeGroupMaster Where FeeGroupName='" & lstMasters.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("FGID")
            txtName.Text = myReader("FeeGroupName")
            Try
                txtDisplayOrder.Text = myReader("DisplayOrder")
            Catch ex As Exception

            End Try

        End While
        myReader.Close()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim FinalMessage As String = ""
       
        Dim sqlStr As String = "Delete From FeeGroupName Where FGID=" & Val(txtID.Text)
        Try
            ExecuteQuery_Update(sqlStr)
        Catch ex As Exception
            FinalMessage = "Record can not be deleted...Record is being used somewhere else..."
        End Try
        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


End Class