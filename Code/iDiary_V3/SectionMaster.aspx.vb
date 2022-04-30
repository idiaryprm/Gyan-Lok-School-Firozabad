Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class SectionMaster
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

        txtID.Text = ""
        'LoadMasterInfo(2, cboClassName)
        lblStatus.Text = ""
        'lstMasters.Items.Clear()
        txtName.Text = ""
        txtDisplayOrder.Text = ""
        LoadMasterInfo(3, lstMasters)
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Please Enter Section!"
            txtName.Focus()
            Exit Sub
        End If

        If txtID.Text = "" Then
            If CheckDoubleEntry(3, txtName.Text) > 0 Then
                lblStatus.Text = "Same Section allready Exist..."
                txtName.Focus()
                Exit Sub
            End If
        End If
        If Trim(txtDisplayOrder.Text) = "" Or IsNumeric(txtDisplayOrder.Text) = False Then
            lblStatus.Text = "Invalid Display Order No..."
            txtDisplayOrder.Focus()
            Exit Sub
        End If
       
        Dim sqlStr As String = ""

        If txtID.Text = "" Then
            'Insert Query
            sqlStr = "Insert into Sections Values('" & txtName.Text & "'," & txtDisplayOrder.Text & ")"
        Else
            'Update Query
            sqlStr = "Update Sections Set SecName='" & txtName.Text & "',DisplayOrder=" & txtDisplayOrder.Text & " Where SecID=" & Val(txtID.Text)
        End If
      ExecuteQuery_Update(sqlStr)

        'Dim TempCourseName As String = cboClassName.Text
        InitControls()
        'cboClassName.Text = TempCourseName

    End Sub
  
    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged

        Dim sqlStr As String = "Select * From Sections Where SecName='" & lstMasters.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("SecID")
            txtName.Text = myReader("SecName")
            txtDisplayOrder.Text = myReader("DisplayOrder")
        End While
        myReader.Close()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = "Delete From Sections Where SecID=" & Val(txtID.Text)
       ExecuteQuery_Update(sqlStr)
        InitControls()
        
    End Sub

End Class