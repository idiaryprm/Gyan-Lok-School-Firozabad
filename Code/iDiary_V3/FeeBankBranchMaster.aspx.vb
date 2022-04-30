Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class FeeBankBranchMaster
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
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""
        LoadMasterInfo(72, cboBank)
        cboBank.Text = FindDefault(72)
        Dim BankID As Integer = FindMasterID(72, cboBank.Text)
        LoadFeeBankBranch(BankID, lstMasters)
        lblStatus.Text = ""
        chkDefault.Checked = False
        cboBank.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboBank.Text = "" Then
            lblStatus.Text = "Please Select Bank!"
            cboBank.Focus()
            Exit Sub
        End If
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Pleasse Enter Branch Name!"
            txtName.Focus()
            Exit Sub
        End If
        If txtID.Text = "" Then
            If CheckDoubleEntry(73, txtName.Text) > 0 Then
                lblStatus.Text = "Same Branch allready Exist..."
                txtName.Focus()
                Exit Sub
            End If
        End If


        Dim BankID As Integer = FindMasterID(72, cboBank.Text)
        Dim sqlStr As String = ""

        Dim IsDefault As Integer = 0
        If chkDefault.Checked = True Then
            IsDefault = 1
            sqlStr = "Update FeeBankBranchMaster Set IsDefault=0 where FeeBankID='" & BankID & "'"
            ExecuteQuery_Update(sqlStr)
        End If

        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into FeeBankBranchMaster (FeeBankID,FeeBankBranchName,IsDefault) Values('" & BankID & "','" & txtName.Text & "','" & IsDefault & "')"
        Else
            'Update
            sqlStr = "Update FeeBankBranchMaster Set FeeBankID ='" & BankID & "', FeeBankBranchName='" & txtName.Text & "', IsDefault=" & IsDefault & " Where FeeBankBranchID=" & Val(txtID.Text)
        End If


        ExecuteQuery_Update(sqlStr)

        LoadFeeBankBranch(BankID, lstMasters)
        txtName.Text = ""
        chkDefault.Checked = False
        txtID.Text = ""
        'InitControls()
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged




        Dim sqlStr As String = "Select * From FeeBankBranchMaster Where FeeBankBranchName='" & lstMasters.Text & "'"



        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtID.Text = myReader("FeeBankBranchID")
            txtName.Text = myReader("FeeBankBranchName")
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




        'Dim sqlStr As String = "update CityMaster set IsDeleted=1  Where CityID=" & Val(txtID.Text)



        'ExecuteQuery_Update(sqlStr)


        'InitControls()
    End Sub

    Protected Sub cboBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBank.SelectedIndexChanged
        Dim BankID As Integer = FindMasterID(72, cboBank.Text)
        LoadFeeBankBranch(BankID, lstMasters)
    End Sub
End Class