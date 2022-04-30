Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class FeeTypes
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
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
        LoadMasterInfo(11, lstMasters)
        chkDuePart.Checked = False
        chkConcession.Checked = False
        chkAreearTYpe.Checked = False
        txtDispalyOrder.Text = ""
        lblStatus.Text = ""
        txtTallyHeadName.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Wrong Fee Head!"
            txtName.Focus()
            Exit Sub
        End If
        If IsNumeric(txtDispalyOrder.Text) = False Then
            lblStatus.Text = "Invalid Display Order!"
            txtDispalyOrder.Focus()
            Exit Sub
        End If
        Dim DuePart As Integer = 0, Concession As Integer = 0, Arrear As Integer = 0
        If chkDuePart.Checked = True Then DuePart = 1
        If chkConcession.Checked = True Then Concession = 1
        If chkAreearTYpe.Checked = True Then Arrear = 1

        Dim sqlStr As String = ""

        If txtID.Text = "" Then             'Insert
            sqlStr = "Insert into FeeTypes(FeeTypeName, PartOfDueProcess, Concession, TallyHeadName, FeeOrder,IsArrear) Values('" & txtName.Text & "'," & DuePart & "," & Concession & ", '" & txtTallyHeadName.Text & "', '" & Val(txtDispalyOrder.Text) & "', '" & Arrear & "')"
        Else                                'Update
            sqlStr = "Update FeeTypes Set FeeTypeName='" & txtName.Text & "',PartOfDueProcess=" & DuePart & ", Concession=" & Concession & ", TallyHeadName='" & txtTallyHeadName.Text & "', FeeOrder='" & Val(txtDispalyOrder.Text) & "', IsArrear='" & Arrear & "' Where FeeTypeID=" & Val(txtID.Text)
        End If
      ExecuteQuery_Update(sqlstr)

        InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged

        Dim sqlStr As String = "Select * From FeeTypes Where FeeTypeName='" & lstMasters.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("FeeTypeID")
            txtName.Text = myReader("FeeTypeName")
            chkDuePart.Checked = myReader("PartOfDueProcess")
            chkConcession.Checked = myReader("Concession")
            Try
                txtTallyHeadName.Text = myReader("TallyHeadName")
            Catch ex As Exception

            End Try
            Try
                txtDispalyOrder.Text = myReader("FeeOrder")
            Catch ex As Exception

            End Try
            Try
                chkAreearTYpe.Checked = myReader("IsArrear")
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
        Dim sqlStr As String = "Delete From FeeTypes Where FeeTypeID=" & Val(txtID.Text)
        Try
            ExecuteQuery_Update(sqlStr)
        Catch ex As Exception
            FinalMessage = "Record can not be deleted...Record is being used somewhere else..."
        End Try
        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


End Class