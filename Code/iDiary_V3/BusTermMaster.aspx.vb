Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient

Public Class BusTermMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub InitControls()
        txtTermName.Text = ""
        txtTermNo.Text = ""
        txtID.Text = ""
        LoadMasterInfo(67, lstTerm)
        txtID.Visible = False
        txtDispOrder.Text = ""
        txtTermName.Focus()
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Trim(txtTermName.Text) = "" Then
            lblStatus.Text = "Invalid Term Name"
            txtTermName.Focus()
            Exit Sub
        End If
        If Trim(txtTermNo.Text) = "" Then
            lblStatus.Text = "Invalid Term No"
            txtTermNo.Focus()
            Exit Sub
        End If
        If IsNumeric(txtDispOrder.Text) = False Then
            lblStatus.Text = "Invalid Display Order"
            txtDispOrder.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
        Dim IsDefault As Integer
        If chkDefault.Checked = True Then
            IsDefault = 1
        Else
            IsDefault = 0
        End If
        If txtID.Text = "" Then
            If CheckDuplicateEntry(txtTermName.Text, "BusTermMaster", "BusTermName") = 0 Then
            Else
                lblStatus.Text = "Term Name Already Exists"
                Exit Sub
            End If
            If CheckDuplicateEntry(txtTermNo.Text, "BusTermMaster", "BusTermNo") = 0 Then
            Else
                lblStatus.Text = "Term No Already Exists"
                Exit Sub
            End If
        End If

        Dim sqlstr As String = ""
        If txtID.Text = "" Then
            sqlstr = "Insert Into BusTermMaster Values('" & txtTermName.Text & "', '" & txtTermNo.Text & "', " & IsDefault & ", '" & txtDispOrder.Text & "')"
            ExecuteQuery_Update(sqlstr)
        Else
            sqlstr = "Update BusTermMaster Set BusTermName='" & txtTermName.Text & "', BusTermNo='" & txtTermNo.Text & "', Isdefault=" & IsDefault & ", DispOrder='" & txtDispOrder.Text & "' Where BusTermID=" & txtID.Text
            ExecuteQuery_Update(sqlstr)
        End If
        InitControls()
    End Sub

    Protected Sub lstTerm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTerm.SelectedIndexChanged
        Dim sqlStr As String = "Select * From busTermMaster Where busTermName='" & lstTerm.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtTermName.Text = myReader("BusTermName")
            txtTermNo.Text = myReader("BusTermNo")
            txtID.Text = myReader("BusTermID")
            txtDispOrder.Text = myReader("DispOrder")
        End While
        myReader.Close()
    End Sub
End Class