Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Public Class FeeConcessionTypeMaster
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
        Response.Cookies("ActiveTab").Value = 3
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadFeeConcessionTypeMaster(lstMasters)
        txtID.Text = ""
        txtName.Text = ""
        'LoadFeeConcessionType(cboConcessionType)
        txtAmount.Text = ""
        LoadFeeTypes(chkFeeType)
        chkAll.checked = False
        lblStatus.Text = ""
        txtName.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Invalid Head Name!"
            txtName.Focus()
            Exit Sub
        End If

        If cboConcessionType.Text = "" Then
            lblStatus.Text = "Invalid Concession Type!"
            cboConcessionType.Focus()
            Exit Sub
        End If

        If IsNumeric(txtAmount.Text) = False Then
            lblStatus.Text = "Invalid Amount!"
            txtAmount.Focus()
            Exit Sub
        End If

        Dim FeeTypeList As String = ""
        For k = 0 To chkFeeType.Items.Count - 1
            If chkFeeType.Items(k).Selected = True Then
                FeeTypeList += FindMasterID(11, chkFeeType.Items(k).Text) & ","
            End If
        Next
        If FeeTypeList = "" Then
            lblStatus.Text = "Please Select atleast one Fee Head"
            'txtDispOrder.Focus()
            Exit Sub
        Else
            FeeTypeList = FeeTypeList.Substring(0, FeeTypeList.Length - 1)
        End If
        lblStatus.Text = ""
        Dim sqlStr As String = ""
      
        If txtID.Text = "" Then             'Insert
            sqlStr = "Insert into FeeConcessionTypeMaster Values(" & _
                "'" & txtName.Text & "'," & _
                cboConcessionType.SelectedIndex & "," & _
                Val(txtAmount.Text) & "," & _
           "'" & FeeTypeList & "')"
        Else                                'Update
            sqlStr = "Update FeeConcessionTypeMaster Set " & _
                "FCTypeName='" & txtName.Text & "'," & _
                "FCType=" & cboConcessionType.SelectedIndex & "," & _
                "FCTypeAmount=" & Val(txtAmount.Text) & "," & _
           "FCFeeType='" & FeeTypeList & "'" & _
        " Where FCTID=" & Val(txtID.Text)
        End If

      ExecuteQuery_Update(sqlstr)

        InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
       
        Dim sqlStr As String = "Select * From FeeConcessionTypeMaster Where FCTypeName='" & lstMasters.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim FeeTypeList As String = ""
        While myReader.Read
            txtID.Text = myReader("FCTID")
            txtName.Text = myReader("FCTypeName")
            cboConcessionType.SelectedIndex = myReader("FCType")
            txtAmount.Text = myReader("FCTypeAmount")
            FeeTypeList = myReader("FCFeeType")
        End While
        Dim FeeTypeListtmp() As String
        FeeTypeListtmp = FeeTypeList.Split(",")

        For k = 0 To chkFeeType.Items.Count - 1
            chkFeeType.Items(k).Selected = False
        Next
        For k = 0 To chkFeeType.Items.Count - 1
            For j = 0 To FeeTypeListtmp.Count - 1
                If FindMasterID(11, chkFeeType.Items(k).Text) = FeeTypeListtmp(j) Then
                    chkFeeType.Items(k).Selected = True
                End If
            Next
        Next
        myReader.Close()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim sqlStr As String = "Delete From FeeConcessionTypeMaster Where FCTID=" & Val(txtID.Text)
        ExecuteQuery_Update(sqlStr)
        InitControls()
    End Sub


    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        For k = 0 To chkFeeType.Items.Count - 1
            If chkAll.checked = True Then
                chkFeeType.Items(k).Selected = True
            Else
                chkFeeType.Items(k).Selected = False
            End If
        Next
    End Sub
End Class