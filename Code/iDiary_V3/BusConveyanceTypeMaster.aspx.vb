Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Public Class BusConveyanceTypeMaster
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
       
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(69, lstMasters)
        txtID.Text = ""
        txtName.Text = ""
        lblStatus.Text = ""
        txtDispOrder.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Please Enter Head Name!"
            txtName.Focus()
            Exit Sub
        End If
        If IsNumeric(txtDispOrder.Text) = False Then
            lblStatus.Text = "Please Enter Display Order!"
            txtDispOrder.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
        Dim sqlStr As String = ""

        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update BusTypeMaster Set IsDefault=0"
            ExecuteQuery_Update(sqlStr)
        End If
        If txtID.Text = "" Then             'Insert
            sqlStr = "Insert into BusTypeMaster Values(" & _
                "'" & txtName.Text & "'," & IsDefault & ",'" & txtDispOrder.Text & "')"
            
        Else                                'Update
            sqlStr = "Update BusTypeMaster Set " & _
                "ConveyanceTypeName='" & txtName.Text & "',IsDefault=" & IsDefault & ", DispOrder='" & txtDispOrder.Text & "' Where ConveyanceTypeID=" & Val(txtID.Text)
        End If
        ExecuteQuery_Update(sqlStr)
       
        InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged

        Dim sqlStr As String = "Select * From BusTypeMaster Where ConveyanceTypeName='" & lstMasters.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("ConveyanceTypeID")
            txtName.Text = myReader("ConveyanceTypeName")
            Try
                If myReader("IsDefault") = "1" Then
                    chkDefault.Checked = True
                Else
                    chkDefault.Checked = False
                End If
            Catch ex As Exception
                chkDefault.Checked = False
            End Try
            txtDispOrder.Text = myReader("Disporder")
        End While
        myReader.Close()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click




        Dim sqlStr As String = "Delete From BusTypeMaster Where FCTID=" & Val(txtID.Text)



        ExecuteQuery_Update(sqlStr)


        InitControls()
    End Sub
End Class