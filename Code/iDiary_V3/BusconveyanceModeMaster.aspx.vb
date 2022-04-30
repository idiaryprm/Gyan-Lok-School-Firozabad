Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class conveyanceModeMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
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
    End Sub

    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""
        LoadMasterInfo(40, lstMasters)
        lblStatus.Text = ""
        txtDispOrder.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtName.Text) = "" Then
            lblStatus.Text = "Please Enter Conveyance Mode Name!"
            txtName.Focus()
            Exit Sub
        End If
        If IsNumeric(txtDispOrder.Text) = False Then
            lblStatus.Text = "Invalid Display Order!"
            txtDispOrder.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
        Dim sqlStr As String = ""
        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update conveyanceModeMaster Set IsDefault=0"
            ExecuteQuery_Update(sqlStr)
        End If

        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into conveyanceModeMaster Values('" & txtName.Text & "'," & IsDefault & ",'" & txtDispOrder.Text & "')"
        Else
            'Update
            sqlStr = "Update conveyanceModeMaster Set conveyanceName='" & txtName.Text & "',IsDefault=" & IsDefault & ", DisplayOrder='" & txtDispOrder.Text & "'  Where conveyanceID=" & Val(txtID.Text)
        End If
        ExecuteQuery_Update(sqlStr)
        InitControls()
        lblStatus.Text = "Conveyance Mode has been Save."
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged




        Dim sqlStr As String = "Select * From conveyanceModeMaster Where conveyanceName='" & lstMasters.Text & "'"



        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("conveyanceID")
            txtName.Text = myReader("conveyanceName")
            Try
                txtDispOrder.Text = myReader("DisplayOrder")
            Catch ex As Exception

            End Try
            Try
                If myReader("IsDefault") = "1" Then
                    chkDefault.Checked = True
                Else
                    chkDefault.Checked = False
                End If
            Catch ex As Exception
                chkDefault.Checked = False
            End Try
        End While
        myReader.Close()


    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click




        Dim sqlStr As String = "Delete From conveyanceModeMaster Where conveyanceID=" & Val(txtID.Text)



        ExecuteQuery_Update(sqlStr)


        InitControls()
    End Sub

End Class